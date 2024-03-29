﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vfs.core;

namespace console.client
{
    public class VFSConsole
    {
        private bool mounted = false;
        private JCDFAT mountedJCDFAT;

        public void Start()
        {
            var res = 0;
            Console.WriteLine();
            Console.WriteLine("Welcome to the console client of the RAVIOLI VFS!");
            Console.WriteLine("Enter command (help to display help):");
            while (res >= 0)
            {
                //TODO write the current path if mounted
                /*if (mounted)
                    Console.Write(mountedJCDFAT.GetCurrentDirectory() + ">");*/
                var command = Parser.Parse(Console.ReadLine());
                res = command.Execute(this);
                Console.WriteLine("");
            }
        }

        #region ICommand implementations

        #region Executable all the time

        public class HelpCommand : ICommand
        {

            public int Execute(VFSConsole console)
            {
                HelpText.Show();
                return 0;
            }

        }

        public class ExitCommand : ICommand
        {
            public int Execute(VFSConsole console)
            {
                try
                {
                    if (console.mounted)
                    {
                        console.mountedJCDFAT.Close();
                        console.mounted = false;
                        console.mountedJCDFAT = null;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("An exception happened when closing the mounted VFS.");
                }
                return -1;
            }
        }

        public class NULLCommand : ICommand
        {
            public int Execute(VFSConsole console)
            {
                //TODO implement something useful
                Console.WriteLine("Unknown command!");
                return 0;
            }
        }

        #endregion

        #region Executable only when NOT mounted

        public class CreateCommand : ICommand
        {
            private bool valid;
            private string path;
            private ulong size;

            public CreateCommand(List<string> args)
            {
                if (args.Count < 2)
                {
                    valid = false;
                    return;
                }

                try
                {
                    path = args[0];
                    size = Convert.ToUInt64(args[1]);
                    valid = true;
                }
                catch (Exception e)
                {
                    valid = false;
                    Console.WriteLine(e.ToString());
                    return;
                }

            }

            public int Execute(VFSConsole console)
            {
                if (!valid)
                {
                    Console.WriteLine("Invalid command, check help for more details.");
                    return 0;
                }

                if (console.mounted)
                {
                    Console.WriteLine("Command can only be executed when no VFS is mounted.");
                    return 0;
                }


                try
                {
                    JCDFAT fat = JCDFAT.Create(path, size);
                    fat.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return 0;
                }

                Console.WriteLine(String.Format("Created VFS {0} with size {1}. The VFS has not been mounted.", path, size));

                return 0;
            }

        }

        public class OpenCommand : ICommand
        {
            private bool valid;
            private string path;

            public OpenCommand(List<string> args)
            {
                if (args.Count < 1)
                {
                    valid = false;
                    return;
                }

                try
                {
                    path = args[0];
                    valid = true;
                }
                catch (Exception e)
                {
                    valid = false;
                    Console.WriteLine(e.ToString());
                    return;
                }

            }

            public int Execute(VFSConsole console)
            {
                if (!valid)
                {
                    Console.WriteLine("Invalid command, check help for more details.");
                    return 0;
                }

                if (console.mounted)
                {
                    Console.WriteLine("Command can only be executed when no VFS is mounted.");
                    return 0;
                }

                try
                {
                    JCDFAT fat = JCDFAT.Open(path);
                    if (fat != null)
                    {
                        console.mountedJCDFAT = fat;
                        console.mounted = true;
                    }
                    else
                    {
                        Console.WriteLine(String.Format("Could not open vfs {0}.", path));
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return 0;
                }

                Console.WriteLine(String.Format("Opened VFS {0} successfully.", path));

                return 0;
            }

        }

        public class DeleteCommand : ICommand
        {
            private bool valid;
            private string path;

            public DeleteCommand(List<string> args)
            {
                if (args.Count < 1)
                {
                    valid = false;
                    return;
                }

                try
                {
                    path = args[0];
                    valid = true;
                }
                catch (Exception e)
                {
                    valid = false;
                    Console.WriteLine(e.ToString());
                    return;
                }

            }

            public int Execute(VFSConsole console)
            {
                if (!valid)
                {
                    Console.WriteLine("Invalid command, check help for more details.");
                    return 0;
                }

                if (console.mounted)
                {
                    Console.WriteLine("Command can only be executed when no VFS is mounted.");
                    return 0;
                }

                try
                {
                    JCDFAT.Delete(path);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return 0;
                }

                Console.WriteLine(String.Format("Deleted VFS {0}.", path));

                return 0;
            }

        }

        #endregion

        #region Executable only when mounted

        public class CloseCommand : ICommand
        {

            public int Execute(VFSConsole console)
            {

                if (!console.mounted)
                {
                    Console.WriteLine("No vfs mounted.");
                    return 0;
                }

                try
                {
                    console.mountedJCDFAT.Close();
                    console.mountedJCDFAT = null;
                    console.mounted = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return 0;
                }

                Console.WriteLine(String.Format("Closed VFS successfully."));

                return 0;
            }
        }

        public class LsCommand : ICommand
        {
            private string path;

            public LsCommand()
            {
                path = "";
            }

            public LsCommand(List<string> args)
            {
                if (args.Count == 0)
                    path = "";
                else
                    path = args[0];
            }

            public int Execute(VFSConsole console)
            {
                if (!console.mounted)
                {
                    Console.WriteLine("No VFS mounted.");
                    return 0;
                }

                try
                {
                    var list = console.mountedJCDFAT.ListDirectory(path);
                    Console.WriteLine("Number of entries: {0}", list.Length);
                    //TODO make output
                    Console.WriteLine("Name\tSize\tType");
                    foreach (var file in list)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}", file.Name, file.Size, file.IsFolder ? "DIR" : "FIL");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return 0;
                }

                return 0;
            }

        }

        public class CdCommand : ICommand
        {
            private bool valid;
            private string path;

            public CdCommand(List<string> args)
            {
                if (args.Count < 1)
                {
                    valid = false;
                    return;
                }

                try
                {
                    // TODO: This doesn't accept file names with spaces.
                    path = args[0];
                    valid = true;
                }
                catch (Exception e)
                {
                    valid = false;
                    Console.WriteLine(e.ToString());
                    return;
                }
            }

            public int Execute(VFSConsole console)
            {
                if (!valid)
                {
                    Console.WriteLine("Invalid command, check help for more details.");
                    return 0;
                }

                if (!console.mounted)
                {
                    Console.WriteLine("No VFS mounted.");
                    return 0;
                }

                try
                {
                    console.mountedJCDFAT.SetCurrentDirectory(path);
                    Console.WriteLine("Current folder: " + console.mountedJCDFAT.GetCurrentDirectory());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return 0;
                }

                return 0;
            }

        }

        public class RmCommand : ICommand
        {
            private bool valid;
            private string path;
            private bool recursive = false;

            public RmCommand(List<string> args)
            {
                if (args.Count < 1)
                {
                    valid = false;
                    return;
                }

                try
                {
                    if (args[0] == "-r")
                    {
                        if (args.Count < 2)
                        {
                            valid = false;
                            return;
                        }

                        recursive = true;
                        path = args[1];
                        valid = true;
                    }
                    else
                    {
                        path = args[0];
                        valid = true;
                    }
                }
                catch (Exception e)
                {
                    valid = false;
                    Console.WriteLine(e.ToString());
                    return;
                }
            }

            public int Execute(VFSConsole console)
            {
                if (!valid)
                {
                    Console.WriteLine("Invalid command, check help for more details.");
                    return 0;
                }

                if (!console.mounted)
                {
                    Console.WriteLine("No VFS mounted.");
                    return 0;
                }

                try
                {
                    console.mountedJCDFAT.DeleteFile(path, recursive);
                    Console.WriteLine(String.Format("Deleted {0} successfully.", path));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return 0;
                }

                return 0;
            }

        }

        public class MkCommand : ICommand
        {
            private bool valid;
            private string path;
            private bool parents = false;
            private ulong size;

            public MkCommand(List<string> args)
            {
                if (args.Count < 2)
                {
                    valid = false;
                    return;
                }

                try
                {
                    if (args[0] == "-p")
                    {
                        if (args.Count < 2)
                        {
                            valid = false;
                            return;
                        }

                        parents = true;
                        path = args[1];
                        size = Convert.ToUInt64(args[2]);
                        valid = true;
                    }
                    else
                    {
                        path = args[0];
                        size = Convert.ToUInt64(args[1]);
                        valid = true;
                    }
                }
                catch (Exception e)
                {
                    valid = false;
                    Console.WriteLine(e.ToString());
                    return;
                }
            }

            public int Execute(VFSConsole console)
            {
                if (!valid)
                {
                    Console.WriteLine("Invalid command, check help for more details.");
                    return 0;
                }

                if (!console.mounted)
                {
                    Console.WriteLine("No VFS mounted.");
                    return 0;
                }

                try
                {
                    console.mountedJCDFAT.CreateFile(path, size, parents);
                    Console.WriteLine(String.Format("Created file {0} successfully.", path));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return 0;
                }

                return 0;
            }

        }

        public class MkdirCommand : ICommand
        {
            private bool valid;
            private string path;
            private bool parents = false;

            public MkdirCommand(List<string> args)
            {
                if (args.Count < 1)
                {
                    valid = false;
                    return;
                }

                try
                {
                    if (args[0] == "-p")
                    {
                        if (args.Count < 2)
                        {
                            valid = false;
                            return;
                        }

                        parents = true;
                        path = args[1];
                        valid = true;
                    }
                    else
                    {
                        path = args[0];
                        valid = true;
                    }
                }
                catch (Exception e)
                {
                    valid = false;
                    Console.WriteLine(e.ToString());
                    return;
                }
            }

            public int Execute(VFSConsole console)
            {
                if (!valid)
                {
                    Console.WriteLine("Invalid command, check help for more details.");
                    return 0;
                }

                if (!console.mounted)
                {
                    Console.WriteLine("No VFS mounted.");
                    return 0;
                }

                try
                {
                    console.mountedJCDFAT.CreateDirectory(path, parents);
                    Console.WriteLine(String.Format("Created directory {0} successfully.", path));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return 0;
                }

                return 0;
            }

        }

        public class MvCommand : ICommand
        {
            private bool valid;
            private string sourcePath;
            private string targetPath;
            private bool sourceOnVFS;
            private bool targetOnVFS;

            public MvCommand(List<string> args)
            {
                if (args.Count < 3)
                {
                    valid = false;
                    return;
                }

                try
                {
                    string mode = args[0];
                    if (mode == "-hv")
                    {
                        sourceOnVFS = false;
                        targetOnVFS = true;
                    }
                    else if (mode == "-vh")
                    {
                        sourceOnVFS = true;
                        targetOnVFS = false;
                    }
                    else if (mode == "-vv")
                    {
                        sourceOnVFS = true;
                        targetOnVFS = true;
                    }
                    else
                    {
                        valid = false;
                        Console.WriteLine("Invalid mv mode, only -hv, -vh and -vv are supported.");
                        return;
                    }

                    sourcePath = args[1];
                    targetPath = args[2];

                    valid = true;
                }
                catch (Exception e)
                {
                    valid = false;
                    Console.WriteLine(e.ToString());
                    return;
                }
            }

            public int Execute(VFSConsole console)
            {
                if (!valid)
                {
                    Console.WriteLine("Invalid command, check help for more details.");
                    return 0;
                }

                if (!console.mounted)
                {
                    Console.WriteLine("No VFS mounted.");
                    return 0;
                }

                try
                {
                    if (sourceOnVFS && targetOnVFS)
                    {
                        console.mountedJCDFAT.MoveFile(sourcePath, targetPath);
                        Console.WriteLine(String.Format("Moved successfully from {0} to {1}.", sourcePath, targetPath));
                    }
                    else if (sourceOnVFS && !targetOnVFS)
                    {
                        console.mountedJCDFAT.ExportFile(sourcePath, targetPath);
                        Console.WriteLine(String.Format("Exported successfully from {0} to {1}.", sourcePath, targetPath));
                    }
                    else if (!sourceOnVFS && targetOnVFS)
                    {
                        console.mountedJCDFAT.ImportFile(sourcePath, targetPath);
                        Console.WriteLine(String.Format("Imported successfully from {0} to {1}.", sourcePath, targetPath));
                    }
                    else
                        Console.WriteLine("Invalid mv mode, only -hv, -vh and -vv are supported.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return 0;
                }

                return 0;
            }

        }

        public class CpCommand : ICommand
        {
            private bool valid;
            private string sourcePath;
            private string targetPath;

            public CpCommand(List<string> args)
            {
                if (args.Count < 2)
                {
                    valid = false;
                    return;
                }

                try
                {
                    sourcePath = args[0];
                    targetPath = args[1];

                    valid = true;
                }
                catch (Exception e)
                {
                    valid = false;
                    Console.WriteLine(e.ToString());
                    return;
                }
            }

            public int Execute(VFSConsole console)
            {
                if (!valid)
                {
                    Console.WriteLine("Invalid command, check help for more details.");
                    return 0;
                }

                if (!console.mounted)
                {
                    Console.WriteLine("No VFS mounted.");
                    return 0;
                }

                try
                {
                    console.mountedJCDFAT.CopyFile(sourcePath, targetPath);
                    Console.WriteLine(String.Format("Copied successfully from {0} to {1}.", sourcePath, targetPath));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return 0;
                }

                return 0;
            }

        }

        public class RnCommand : ICommand
        {
            private bool valid;
            private string path;
            private string newName;

            public RnCommand(List<string> args)
            {
                if (args.Count < 2)
                {
                    valid = false;
                    return;
                }

                try
                {
                    path = args[0];
                    newName = args[1];
                    valid = true;
                }
                catch (Exception e)
                {
                    valid = false;
                    Console.WriteLine(e.ToString());
                    return;
                }
            }

            public int Execute(VFSConsole console)
            {
                if (!valid)
                {
                    Console.WriteLine("Invalid command, check help for more details.");
                    return 0;
                }

                if (!console.mounted)
                {
                    Console.WriteLine("No VFS mounted.");
                    return 0;
                }

                try
                {
                    console.mountedJCDFAT.RenameFile(path, newName);
                    Console.WriteLine(String.Format("Renaming of {0} to {1} done successfully.", path, newName));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return 0;
                }

                return 0;
            }

        }

        public class FreeCommand : ICommand
        {

            public int Execute(VFSConsole console)
            {

                if (!console.mounted)
                {
                    Console.WriteLine("No vfs mounted.");
                    return 0;
                }

                try
                {
                    ulong free = console.mountedJCDFAT.FreeSpace();
                    Console.WriteLine(String.Format("The amount of free space on the mounted VFS is: {0} bytes.", free));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return 0;
                }

                return 0;
            }
        }

        public class OccupiedCommand : ICommand
        {

            public int Execute(VFSConsole console)
            {

                if (!console.mounted)
                {
                    Console.WriteLine("No vfs mounted.");
                    return 0;
                }

                try
                {
                    ulong occupied = console.mountedJCDFAT.OccupiedSpace();
                    Console.WriteLine(String.Format("The amount of occupied space on the mounted VFS is: {0} bytes.", occupied));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return 0;
                }

                return 0;
            }
        }

        public class SizeCommand : ICommand
        {

            public int Execute(VFSConsole console)
            {

                if (!console.mounted)
                {
                    Console.WriteLine("No vfs mounted.");
                    return 0;
                }

                try
                {
                    ulong size = console.mountedJCDFAT.Size();
                    Console.WriteLine(String.Format("The size of the mounted VFS is: {0} bytes.", size));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return 0;
                }

                return 0;
            }
        }

        public class SearchCommand : ICommand {
            private bool valid = false; // Invalid by default.
            private bool caseSensitive = true; // Case sensitive by default.
            private bool recursive = true; // Resursive search by default.
            private string fileName;
            private bool startFromRoot = false; // Searches only current directory by default.
            private int parsedArgs = 0;

            public SearchCommand(List<string> args) {
                if (args.Count < 1) {
                    return;
                }

                var unparsedArg = 0;
                // Parse modifiers
                for (int i = 0; i < args.Count; i += 1) {
                    switch (args[i]) {
                        case "-i":
                            caseSensitive = false;
                            parsedArgs += 1;
                            break;
                        case "-a":
                            startFromRoot = true;
                            parsedArgs += 1;
                            break;
                        case "-n":
                            recursive = false;
                            parsedArgs += 1;
                            break;
                        default:
                            unparsedArg = i;
                            break;
                    }
                }
                // Only valid if there's exactly one unparsed argument, the file name.
                if (parsedArgs != args.Count - 1) {
                    return;
                }

                fileName = args[unparsedArg];
                valid = true;
            }

            public int Execute(VFSConsole console) {
                if (!valid) {
                    Console.WriteLine("Invalid command, check help for more details.");
                    return 0;
                }

                if (!console.mounted) {
                    Console.WriteLine("No VFS mounted.");
                    return 0;
                }

                try {
                    string searchDir = console.mountedJCDFAT.GetCurrentDirectory();
                    if (startFromRoot) {
                        searchDir = "/";
                    }
                    var files = console.mountedJCDFAT.Search(searchDir, fileName, caseSensitive, recursive);
                    if (files.Length == 0) {
                        Console.WriteLine("Sorry, no files matched \"{0}\"", fileName);
                        return 0;
                    }

                    Console.WriteLine("Found the following files:");
                    foreach (var file in files) {
                        Console.WriteLine(file);
                    }
                }
                catch (Exception e) {
                    Console.WriteLine(e.ToString());
                    return 0;
                }

                return 0;
            }

        }


        #endregion

    }

        #endregion

}
