﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using vfs.common;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

namespace vfs.core.tests {
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class JCDFATEventTests {
        private const uint MB1 = 1000000;
        private const uint MB50 = 50 * MB1;

        [TestMethod]
        public void TestModifiedEvent() {
            // Set up
            var testName = MethodBase.GetCurrentMethod().Name;
            var vfs = CreateVFS(testName);
            var fileName = "file";
            var fileSize = MB1;
            var fs = vfs.CreateFile(fileName, (uint)fileSize, false);


            // Test
            var data = TestHelpers.GenerateRandomData((int)fileSize, 1);
            // Add function to be called on FileModified event.
            var callbackCalled = false;
            vfs.FileModified += (path, startByte, inData) => {
                TestHelpers.AreEqual(data, inData);
                callbackCalled = true;
            };
            fs.Write(data, 0, (int)fileSize);
            Assert.IsTrue(callbackCalled);

            CloseVFS(vfs, testName);
        }

        [TestMethod]
        public void TestResizedSmallerEvent() {
            // Set up
            var testName = MethodBase.GetCurrentMethod().Name;
            var vfs = CreateVFS(testName);
            var fileName = "file";
            var fileSize = MB1;
            var fs = vfs.CreateFile(fileName, (ulong)fileSize, false);
            var newFileSize = fileSize / 2;

            // Test
            var callbackCalled = false;
            vfs.FileResized += (path, newSize) => {
                Assert.AreEqual(newFileSize, newSize);
                callbackCalled = true;
            };
            fs.SetLength(newFileSize);
            Assert.IsTrue(callbackCalled);

            CloseVFS(vfs, testName);
        }

        [TestMethod]
        public void TestResizedBiggerEvent() {
            // Set up
            var testName = MethodBase.GetCurrentMethod().Name;
            var vfs = CreateVFS(testName);
            var fileName = "file";
            var fileSize = MB1;
            var fs = vfs.CreateFile(fileName, (ulong)fileSize, false);
            var newFileSize = fileSize / 2;

            // Test
            var callbackCalled = false;
            vfs.FileResized += (path, newSize) => {
                Assert.AreEqual(newFileSize, newSize);
                callbackCalled = true;
            };
            fs.SetLength(newFileSize);
            Assert.IsTrue(callbackCalled);

            CloseVFS(vfs, testName);
        }

        private JCDFAT CreateVFS(string testName, uint size) {
            TestHelpers.DeleteFiles(new string[] { testName });
            return JCDFAT.Create(testName, size);
        }

        private JCDFAT CreateVFS(string testName) {
            return CreateVFS(testName, MB50);
        }

        private void CloseVFS(JCDFAT vfs, string testName) {
            vfs.Close();
            TestHelpers.DeleteFiles(new string[] { testName });
        }
    }
}
