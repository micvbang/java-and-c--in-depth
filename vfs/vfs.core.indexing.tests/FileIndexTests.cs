﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using vfs.common;
using System.Diagnostics.CodeAnalysis;

namespace vfs.core.indexing.tests {
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class FileIndexTests {
        private delegate void CorrectFileFound(IndexedFile x, FileIndex fi);

        [TestMethod]
        public void TestSingleFileInsertion() {
            /// Set up
            var testName = TestHelpers.GetOwnFunctionName();
            var fileIndex = GetIndex(testName);
            var path = "/path/file";
            var fileName = Helpers.PathGetFileName(path);
            fileIndex.Put(path);

            /// Test
            Assert.AreEqual(path, fileIndex.Get(fileName, true)[0].Path); // Make sure file is there.
            CleanUp(fileIndex, testName);
        }

        [TestMethod]
        [ExpectedException(typeof(FileAlreadyIndexedException), "A file can't be added twice.")]
        public void TestDuplicateFileInsertion() {
            /// Set up
            var testName = TestHelpers.GetOwnFunctionName();
            var fileIndex = GetIndex(testName);
            var file = new IndexedFile("/path/file");
            fileIndex.Put(file);
            fileIndex.Put(file);

            // Can't run cleanup here, because exception is thrown above.
            CleanUp(fileIndex, testName);
        }

        [TestMethod]
        public void TestMultipleFileInsertionOneFileName() {
            /// Set up
            var testName = TestHelpers.GetOwnFunctionName();
            var fileIndex = GetIndex(testName);
            string fileName = "file";
            var files = GenerateFilesArray(5, fileName, true);
            foreach (var file in files) {
                fileIndex.Put(file);
            }

            /// Test
            var vals = fileIndex.Get(fileName, true);
            Assert.IsTrue(FilesAreAllThere(files, vals));
            CleanUp(fileIndex, testName);
        }

        [TestMethod]
        public void TestMultipleFileInsertionMultipleFileNames() {
            /// Set up
            var testName = TestHelpers.GetOwnFunctionName();
            var fileIndex = GetIndex(testName);
            var files = GenerateFilesArray(5, "file", false);
            foreach (var file in files) {
                fileIndex.Put(file);
            }

            CorrectFileFound correctFile = (IndexedFile f, FileIndex fIndex) => {
                var val = fIndex.Get(f.Name, true);
                Assert.AreEqual(val.Length, 1); // Make sure that only one file is found.
                Assert.AreEqual(f, val[0]); // Make sure that the file is the correct file.
            };

            /// Test
            // Verify that all files can be correctly retrieved.
            for (int i = 0; i < files.Length; i += 1) {
                correctFile(files[i], fileIndex);
            }
            CleanUp(fileIndex, testName);
        }

        [TestMethod]
        public void TestSingleFileRetrieval() {
            var testName = TestHelpers.GetOwnFunctionName();
            var fileIndex = GetIndex(testName);
            fileIndex.Put(new IndexedFile("/var/file"));
            var vals = fileIndex.Get("file", true);
            Assert.AreEqual(1, vals.Length);
            CleanUp(fileIndex, testName);
        }

        [TestMethod]
        public void TestNonExistentFile() {
            /// Set up
            var testName = TestHelpers.GetOwnFunctionName();
            var fileIndex = GetIndex(testName);

            /// Test
            var vals = fileIndex.Get("non existent file", true);
            Assert.IsNull(vals); // Make sure file isn't there.
            CleanUp(fileIndex, testName);
        }

        [TestMethod]
        public void TestMultipleFileRetrieval() {
            /// Set up
            var testName = TestHelpers.GetOwnFunctionName();
            var fileIndex = GetIndex(testName);
            var fileName = "file";
            var files = GenerateFilesArray(5, fileName, true);
            foreach (var file in files) {
                fileIndex.Put(file);
            }

            /// Test
            var vals = fileIndex.Get(fileName, true);
            Assert.IsTrue(FilesAreAllThere(files, vals));
            CleanUp(fileIndex, testName);
        }

        [TestMethod]
        public void TestRemoveNonExistentFile() {
            /// Set up
            var testName = TestHelpers.GetOwnFunctionName();
            var fileIndex = GetIndex(testName);
            var file = new IndexedFile("/path/file");
            var file2 = new IndexedFile("/path/file2");
            fileIndex.Put(file);

            /// Test
            Assert.AreEqual(file, fileIndex.Get(file.Name, true)[0]); // Make sure `file` is there.
            fileIndex.Remove(file2); // Try to remove file that wasn't added.
            Assert.AreEqual(file, fileIndex.Get(file.Name, true)[0]); // Make sure `file` is still there.
            CleanUp(fileIndex, testName);
        }

        [TestMethod]
        public void TestSingleFileSingleRemoval() {
            /// Set up
            var testName = TestHelpers.GetOwnFunctionName();
            var fileIndex = GetIndex(testName);
            var file = new IndexedFile("/path/file");
            fileIndex.Put(file);

            /// Test
            Assert.AreEqual(file, fileIndex.Get(file.Name, true)[0]); // Make sure file is there.
            fileIndex.Remove(file);
            Assert.IsNull(fileIndex.Get(file.Name, true)); // Make sure file has been removed.
            CleanUp(fileIndex, testName);
        }

        [TestMethod]
        public void TestMultipleFilesSingleRemoval() {
            /// Set up
            var testName = TestHelpers.GetOwnFunctionName();
            var fileIndex = GetIndex(testName);
            var fileName = "file";
            var files = GenerateFilesArray(5, fileName, true);
            // Insert `files` in to index.
            foreach (var f in files) {
                fileIndex.Put(f);
            }

            var index = 2;
            var file = files[index];
            // Make sure file has been inserted.
            Assert.AreEqual(file, fileIndex.Get(fileName, true)[index]);

            // Remove `file` from `index`.
            fileIndex.Remove(file);

            // Remove `file` from `files`.
            files = files.Where(x => { return x != file; }).ToArray();

            /// Test
            var vals = fileIndex.Get(fileName, true);
            // Make sure that only files that weren't deleted are still there.
            Assert.IsTrue(FilesAreAllThere(files, vals));
            CleanUp(fileIndex, testName);
        }

        [TestMethod]
        public void TestMultipleFilesMultipleRemoval() {
            /// Set up
            var testName = TestHelpers.GetOwnFunctionName();
            var fileIndex = GetIndex(testName);
            var fileName = "file";
            var files = GenerateFilesArray(5, fileName, true);
            // Insert `files` in to index.
            foreach (var f in files) {
                fileIndex.Put(f);
            }

            // Remove files from `index` and `files`.
            var indexes = new int[] { 0, 1, 2 };
            foreach (var index in indexes) {
                var file = files[index];
                // Make sure file has been inserted.
                Assert.AreEqual(file, fileIndex.Get(fileName, true)[index]);

                // Remove `file` from `index`.
                fileIndex.Remove(file);

                // Remove `file` from `files`.
                // This is where the indexes are "refreshed".
                files = files.Where(x => { return x != file; }).ToArray();
            }

            /// Test
            var vals = fileIndex.Get(fileName, true);
            // Make sure that only files that weren't deleted are still there.
            Assert.IsTrue(FilesAreAllThere(files, vals));
            CleanUp(fileIndex, testName);
        }

        [TestMethod]
        public void TestMassiveMultipleFilesMultipleRemoval() {
            /// Set up
            var testName = TestHelpers.GetOwnFunctionName();
            var fileIndex = GetIndex(testName);
            var fileName = "file";
            int numFiles = 10000;
            var files = GenerateFilesArray(numFiles, fileName, false);
            // Insert `files` in to index.
            foreach (var f in files) {
                fileIndex.Put(f);
            }

            // Remove every other file from `index` and `files`.
            for (var i = 0; i < numFiles; i += 2) {
                fileIndex.Remove(files[i]);
            }

            // Create an array with every other file from `files` in it.
            // This should yield an array with the files that weren't deleted
            // in the loop above.
            var newFiles = new IndexedFile[numFiles / 2];
            for (int i = 0; i < newFiles.Length; i += 1) {
                newFiles[i] = files[i * 2 + 1];
            }

            /// Test
            // Make sure that only files that weren't deleted are still there.
            for (int i = 0; i < newFiles.Length; i += 1) {
                var file = newFiles[i];
                var vals = fileIndex.Get(file.Name, true);
                Assert.AreEqual(file, vals[0]);
            }
            CleanUp(fileIndex, testName);
        }

        [TestMethod]
        public void TestCreateAndReopenFile() {
            var testName = TestHelpers.GetOwnFunctionName();
            var fileIndex = GetIndex(testName);
            fileIndex.Close();
            fileIndex = ReopenIndex(testName);
            CleanUp(fileIndex, testName);
        }

        [TestMethod]
        public void TestCreateAndReopenFileAndReadIndex() {
            // Set up
            var testName = TestHelpers.GetOwnFunctionName();
            var fileIndex = GetIndex(testName);
            var f1 = new IndexedFile("/var/file");
            fileIndex.Put(f1);
            fileIndex.Close();

            // Test
            fileIndex = ReopenIndex(testName);
            var f2 = fileIndex.Get(f1.Name, true)[0];
            Assert.AreEqual(f1, f2);
            CleanUp(fileIndex, testName);
        }


        [TestMethod]
        public void TestCreateAndReopenFileAndReadManyIndexes() {
            // Set up
            var testName = TestHelpers.GetOwnFunctionName();
            var fileIndex = GetIndex(testName);
            var fileName = "file";
            var files = GenerateFilesArray(50, fileName, false);
            foreach (var file in files) {
                fileIndex.Put(file);
            }
            fileIndex.Close();

            // Test
            fileIndex = ReopenIndex(testName);

            foreach (var file in files) {
                var val = fileIndex.Get(file.Name, true)[0];
                Assert.AreEqual(file, val);
            }
            CleanUp(fileIndex, testName);
        }

        [TestMethod]
        public void TestCreateAndReopenFileAndReadManyIndexesSameName() {
            // Set up
            var testName = TestHelpers.GetOwnFunctionName();
            var fileIndex = GetIndex(testName);
            var fileName = "file";
            var files = GenerateFilesArray(50, fileName, true);
            foreach (var file in files) {
                fileIndex.Put(file);
            }
            fileIndex.Close();

            // Test
            fileIndex = ReopenIndex(testName);

            var fetchedFiles = fileIndex.Get(fileName, true);
            for (int i = 0; i < files.Length; i += 1) {
                Assert.AreEqual(files[i], fetchedFiles[i]);
            }
            CleanUp(fileIndex, testName);
        }

        [TestMethod]
        public void TestCaseInsensitive() {
            // Set up
            var testName = TestHelpers.GetOwnFunctionName();
            var fileIndex = GetIndex(testName);
            var file = new IndexedFile("/path/CaSeInSenSiTiVe");
            fileIndex.Put(file);

            var val1 = fileIndex.Get(file.Name.ToLower(), false);
            Assert.IsTrue(val1.Length == 1);
            Assert.AreEqual(file, val1[0]);

            var val2 = fileIndex.Get(file.Name.ToUpper(), false);
            Assert.IsTrue(val2.Length == 1);
            Assert.AreEqual(file, val2[0]);
            CleanUp(fileIndex, testName);
        }

        [TestMethod]
        public void TestCaseSensitive() {
            // Set up
            var testName = TestHelpers.GetOwnFunctionName();
            var fileIndex = GetIndex(testName);
            var file = new IndexedFile("/path/CaSeSenSiTiVe");
            fileIndex.Put(file);

            // Test
            var isNull = fileIndex.Get(file.Name.ToLower(), true);
            Assert.IsNull(isNull);

            isNull = fileIndex.Get(file.Name.ToUpper(), true);
            Assert.IsNull(isNull);

            var isSame = fileIndex.Get(file.Name, true)[0];
            Assert.AreEqual(file, isSame);
            CleanUp(fileIndex, testName);
        }

        private bool FilesAreAllThere(IndexedFile[] arr1, IndexedFile[] arr2) {
            if (arr1.Length != arr2.Length) {
                return false;
            }

            // Make sure that all of the files are retrived back (in same order).
            for (int i = 0; i < arr1.Length; i += 1) {
                if (!(arr1[i] == arr2[i])) {
                    return false;
                }
            }
            return true;
        }

        private string[] CreateTestFileNames(string testName) {
            return new string[] {String.Format("__{0}_tree.test", testName),
                                 String.Format("__{0}_data.test", testName),};
        }

        private FileIndex GetIndex(string testName) {
            // Expects test names to be unique, so that they will operate on different files.
            // This is necessary because the tests are run in parallel.
            var files = CreateTestFileNames(testName);
            TestHelpers.DeleteFiles(files);
            return FileIndex.Initialize(files[0], files[1]);
        }

        private FileIndex ReopenIndex(string testName) {
            var files = CreateTestFileNames(testName);
            return FileIndex.Initialize(files[0], files[1]);
        }

        private void CleanUp(FileIndex fileIndex, string testName) {
            fileIndex.Close();
            TestHelpers.DeleteFiles(CreateTestFileNames(testName));
        }


        private IndexedFile[] GenerateFilesArray(int numElements, string fileName, bool sameName) {
            var files = new IndexedFile[numElements];
            for (var i = 0; i < numElements; i += 1) {
                var name = sameName ? fileName : fileName + i;
                // Make sure that files aren't exactly the same, i.e. same name AND path.
                var file = new IndexedFile(String.Format("/path/{0}/{1}", i, name));
                files[i] = file;
            }
            return files;
        }
    }
}

