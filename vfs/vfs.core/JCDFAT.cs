using System;
using System.IO;
using System.Runtime.InteropServices;
using vfs.exceptions;

namespace vfs.core
{
    public class JCDVFS : IJCDBasicVFS
    {
        private JCDFAT fat;

        public static JCDVFS Create(string hfsPath, ulong size)
        {
            // Make sure the directory exists.
            if (File.Exists(Path.GetDirectoryName(hfsPath)))
            {
                throw new DirectoryNotFoundException();
            }

            // Make sure the file doesn't already exist.
            if (File.Exists(hfsPath))
            {
                throw new FileAlreadyExistsException();
            }

            if (size >= JCDFAT.globalMaxFSSize)
            {
                throw new InvalidSizeException();
            }

            // Create fsfile.
            try
            {
                var fs = new FileStream(hfsPath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None);
                return new JCDVFS(fs, size);
            }
            catch (IOException)
            {
                // The file possibly already exists or the stream has been unexpectedly closed
                throw new FileAlreadyExistsException(); //throw not enough space in parent fs
            }
        }

        public static JCDVFS Open(string hfsPath)
        {
            FileStream fs;
            try
            {
                fs = new FileStream(hfsPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException();
            }

            return new JCDVFS(fs);
        }

        public static void Delete(string hfsPath)
        {
            return;
        }

        public JCDVFS(FileStream fs, ulong size)
        {
            fat = new JCDFAT(fs, size);
        }

        public JCDVFS(FileStream fs)
        {
            fat = new JCDFAT(fs);
        }

        public void Close()
        {
            fat.Close();
        }

        //Interface methods
        public ulong Size()
        {
            return 0L;
        }
        public ulong OccupiedSpace()
        {
            return 0L;
        }
        public ulong FreeSpace()
        {
            return 0L;
        }
        public void CreateDirectory(string vfsPath, bool createParents)
        {
            return;
        }
        public void ImportFile(string hfsPath, string vfsPath)
        {
            return;
        }
        public void ExportFile(string vfsPath, string hfsPath)
        {
            return;
        }
        public void DeleteFile(string vfsPath, bool recursive)
        {
            return;
        }
        public void RenameFile(string vfsPath, string newName)
        {
            return;
        }
        public void MoveFile(string vfsPath, string newVfsPath)
        {
            return;
        }
        public JCDDirEntry[] ListDirectory(string vfsPath)
        {
            return null;
        }
        public void SetCurrentDirectory(string vfsPath)
        {
            return;
        }
        public string GetCurrentDirectory()
        {
            return null;
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct ByteToUintConverter
    {
        [FieldOffset(0)]
        public byte[] bytes;

        [FieldOffset(0)]
        public uint[] uints;
    }

    internal class JCDFAT : IDisposable
    {
        private bool initialized = false;

        private const uint magicNumber = 0x13371337;
        private const uint freeBlock = 0xFFFFFFFF;
        private const uint endOfChain = 0xFFFFFFFE;

        public const uint rootDirBlock = 0;
        private const uint searchFileBlock = 1;

        // All sizes in this class are given in bytes.
        private const uint reservedBlockNumbers = 2; //End-of-chain and free
        private const uint availableBlockNumbers = unchecked((1 << 32) - reservedBlockNumbers); //32-bit block numbers
        private const uint metaDataBlocks = 1; // Number of blocks used for meta data (doesn't include the FAT)
        private const uint blockSize = 1 << 12; //4KB blocks
        public const uint globalMaxFSSize = unchecked(availableBlockNumbers * blockSize + (1 << 32) * 4 + metaDataBlocks * blockSize);
        //numBlocks * (blockSize + fatEntrySize) + metaDataSize. FAT size is rounded up to a whole number of blocks, assuming reservedBlockNumbers < blockSize/4.
        public const uint fileEntrySize = 1 << 8; //256B
        private const uint fatEntriesPerBlock = blockSize / 4;
        public const uint filesEntriesPerBlock = blockSize / fileEntrySize;

        private const int freeBlocksOffset = 12;
        private const int firstFreeBlockOffset = 16;

        private ulong currentSize, maxSize;
        private ulong currentNumBlocks; //Can actually exceed a uint!
        private uint maxNumBlocks;
        private uint currentNumDataBlocks, maxNumDataBlocks;
        private uint fatBlocks;
        private uint dataOffsetBlocks;
        private uint freeBlocks;
        private uint firstFreeBlock;
        private uint[] fat;

        private void setFreeBlocks(uint newVal)
        {
            freeBlocks = newVal;
            bw.Seek(freeBlocksOffset, SeekOrigin.Begin);
            bw.Write(newVal);
        }
        private void setfirstFreeBlock(uint newVal)
        {
            firstFreeBlock = newVal;
            bw.Seek(firstFreeBlockOffset, SeekOrigin.Begin);
            bw.Write(newVal);
        }

        private JCDFolder rootFolder;
        private JCDFolder currentFolder;

        private FileStream fs;
        private BinaryWriter bw;
        private BinaryReader br;

        public void Write(ulong offset, byte[] data)
        {
            fs.Seek((long)offset, SeekOrigin.Begin);
            bw.Write(data);
        }
        public void Write(ulong offset, ushort data)
        {
            fs.Seek((long)offset, SeekOrigin.Begin);
            bw.Write(data);
        }
        public void Write(ulong offset, uint data)
        {
            fs.Seek((long)offset, SeekOrigin.Begin);
            bw.Write(data);
        }

        private long fatOffset(uint index)
        {
            return metaDataBlocks * blockSize + index * 4;
        }
        public void fatSet(uint index, uint value)
        {
            fat[index] = value;
            fs.Seek(fatOffset(index), SeekOrigin.Begin);
            bw.Write(value);
        }
        public void fatSetEOC(uint index)
        {
            fatSet(index, endOfChain);
        }
        public void fatSetFree(uint index)
        {
            fatSet(index, freeBlock);
        }

        public ulong offset(uint dataBlock, uint blockOffset)
        {
            return (dataOffsetBlocks + dataBlock) * blockSize + blockOffset;
        }

        public ulong offset(uint firstBlock, uint blockIndex, uint blockOffset)
        {
            for (uint i = 0; i < blockIndex; i++)
            {
                firstBlock = fat[firstBlock];
            }
            return offset(firstBlock, blockOffset);
        }

        private ulong ruid(ulong num, ulong den)
        { //Round up integer division
            return (num + den - 1) / den;
        }

        private void newFSSetSize(ulong size)
        {
            size = ruid(size, blockSize); //Round up to whole blocks
            size -= metaDataBlocks; //Without metadata blocks
            ulong sizeMultiple = 1 + fatEntriesPerBlock; //One FAT block + number of data blocks it represents
            fatBlocks = (uint)ruid(size, sizeMultiple); //Number of FAT blocks
            fat = new uint[fatBlocks * fatEntriesPerBlock];

            initSize(true);

            //These are written in newFSWriteMetaData()
            freeBlocks = 0;
            firstFreeBlock = endOfChain;

            fs.SetLength((long)currentSize);
        }

        private void initSize(bool newFile)
        {
            dataOffsetBlocks = metaDataBlocks + fatBlocks;
            maxNumDataBlocks = Math.Min(fatBlocks * fatEntriesPerBlock, availableBlockNumbers);
            maxNumBlocks = dataOffsetBlocks + maxNumDataBlocks;
            if (newFile)
            {
                currentNumDataBlocks = 2; //We start with an empty root folder and a empty search file.
                currentNumBlocks = dataOffsetBlocks + currentNumDataBlocks;
            }
            else
            {
                currentNumBlocks = (ulong)(fs.Length) / blockSize;
                currentNumDataBlocks = (uint)(currentNumBlocks - fatBlocks - metaDataBlocks);
            }
            currentSize = currentNumBlocks * blockSize;
            maxSize = maxNumBlocks * blockSize;
        }

        private void readFAT()
        {
            fs.Seek(metaDataBlocks * blockSize, SeekOrigin.Begin);
            ByteToUintConverter cnv = new ByteToUintConverter
            {
                bytes = br.ReadBytes((int)(fatBlocks * blockSize))
            }; //We're assuming the FAT size in bytes fits into an int, since that's what ReadBytes accepts.
            //This means the FS can't be more than 2TB. This should probably be changed.
            fat = cnv.uints; //Don't trust fat.Length after this
            //http://stackoverflow.com/questions/619041/what-is-the-fastest-way-to-convert-a-float-to-a-byte/619307#619307
        }

        private void newFSWriteMetaData()
        {
            bw.Write(magicNumber);
            bw.Write(blockSize); //Currently unused
            bw.Write(fatBlocks);
            bw.Write(freeBlocks); //Number of free blocks, 0
            bw.Write(firstFreeBlock); //First free block, endOfChain
            bw.Write(rootDirBlock); //Currently unused
            bw.Write(searchFileBlock); //Currently unused
        }

        private void parseMetaData()
        {
            if (br.ReadUInt32() != magicNumber)
            {
                throw new InvalidFileException();
            }
            /*blockSize = */
            br.ReadUInt32(); //Unused.
            fatBlocks = br.ReadUInt32();
            freeBlocks = br.ReadUInt32();
            firstFreeBlock = br.ReadUInt32();
            //rootDirBlock = br.ReadUInt32(); //Unused
            //searchFileBlock = br.ReadUInt32(); //Unused
        }

        private void newFSCreateRootFolder()
        {
            rootFolder = JCDFolder.createRootFolder(this);
            currentFolder = rootFolder;
        }
        private void initRootFolder()
        {
            rootFolder = JCDFolder.rootFolder(this);
            currentFolder = rootFolder;
        }

        private void newFSCreateSearchFile()
        {
            //Not implemented
            fatSetEOC(searchFileBlock);
        }
        private void initSearchFile()
        {
            //Not implemented
        }

        public JCDFAT(FileStream fs, ulong size)
        {
            this.fs = fs;

            newFSSetSize(size);

            bw = new BinaryWriter(fs);
            br = new BinaryReader(fs);

            newFSWriteMetaData();
            newFSCreateRootFolder();
            newFSCreateSearchFile();

            initialized = true;
        }

        public JCDFAT(FileStream fs)
        {
            this.fs = fs;

            bw = new BinaryWriter(fs);
            br = new BinaryReader(fs);

            parseMetaData();
            initSize(false);
            initRootFolder();

            readFAT();

            initialized = true;
        }

        public void Dispose()
        {
            Close();
        }

        public void Close()
        {
            bw.Flush();
            fs.Flush();
            bw.Dispose();
            br.Dispose();
            fs.Dispose();
        }
    }
}