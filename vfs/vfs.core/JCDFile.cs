﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using vfs.core.visitor;

namespace vfs.core {
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct JCDDirEntry {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = JCDDirEntry.StructSize())]
        public string Name;
        public ulong Size;
        public bool IsFolder;
        public uint FirstBlock;

        public static JCDDirEntry FromByteArr(byte[] byteArr) {
            int size = StructSize();
            if(byteArr.Length != size) {
                throw new InvalidCastException();
            }

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(byteArr, 0, ptr, size);
            JCDDirEntry ret = (JCDDirEntry)Marshal.PtrToStructure(ptr, typeof(JCDDirEntry));
            Marshal.FreeHGlobal(ptr);
            return ret;
        }

        public static int StructSize() {
            return Marshal.SizeOf(typeof(JCDDirEntry));   
        }
    }

    internal class JCDFile {
        protected JCDDirEntry entry;
        /*private string name;
        private ulong size;
        private bool isFolder;
        private uint firstBlock;*/

        protected JCDFAT container;
        protected JCDFolder parent;
        protected ulong parentIndex;
        protected string path;

        public static JCDFile FromDirEntry(JCDFAT container, JCDDirEntry entry, JCDFolder parent, ulong parentIndex, string path) {
            if(entry.IsFolder) {
                return new JCDFolder(container, entry, parent, parentIndex, path);
            }
            else {
                return new JCDFile(container, entry, parent, parentIndex, path);
            }
        }

        protected JCDFile(JCDFAT container, JCDDirEntry entry, JCDFolder parent, ulong parentIndex, string path) {
            this.entry = entry;
            /*name = entry.name;
            size = entry.size;
            isFolder = entry.isFolder;
            firstBlock = entry.firstBlock;*/

            this.container = container;
            this.parent = parent;
            this.parentIndex = parentIndex;
            this.path = path;
        }

        public void Delete()
        {
            // If this is a folder, delete all dir entries recursively.
            if (entry.IsFolder)
            {
                var folder = (JCDFolder)this;
                var dirEntries = folder.GetDirEntries(entry.FirstBlock);
                foreach (var dirEntry in dirEntries)
                {
                    // How do we get the index of this entry? We want to pass it to our child.
                    ulong parentIndex = 0;
                    string entryPath = System.IO.Path.Combine(path, dirEntry.Name);
                    JCDFile.FromDirEntry(container, dirEntry, folder, parentIndex, entryPath).Delete();
                }
            }

            // Delete this instance, whether folder or file. All (potential) sub-entries have been deleted at this point.
            container.WalkFATChain(entry.FirstBlock, new FileDeleterVisitor());
        }
    }
}
