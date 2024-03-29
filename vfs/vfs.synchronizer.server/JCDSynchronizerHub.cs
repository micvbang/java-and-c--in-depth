﻿using System;
using vfs.core;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR.Hubs;
using vfs.synchronizer.common;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Linq;

namespace vfs.synchronizer.server
{
    [HubName(JCDSynchronizerSettings.HubName)]
    public class JCDSynchronizerHub : Hub, IJCDSynchronizerServer
    {
        JCDSynchronizerDatabase db = new JCDSynchronizerDatabase();
        private static readonly ConcurrentDictionary<string, long> userIds = new ConcurrentDictionary<string, long>();

        /// <summary>
        /// Called every time a user connects. We want to store user's session across
        /// TCP sessions. This method lets us just that.
        /// 
        /// This method was found on 
        /// http://stackoverflow.com/questions/20520874/signalr-how-to-survive-accidental-page-refresh/20521466#20521466
        /// </summary>
        public override Task OnConnected() {
            // Users are not required to authenticate when they connect in order to register.
            if (Context.User == null) {
                Console.WriteLine("User connected in order to register.");
                return base.OnConnected();
            }

            string username = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;
            Console.WriteLine("User {0} connected", username);

            Groups.Add(connectionId, username);

            return base.OnConnected();
        }

        public JCDSynchronizerReply Register(string username, string password)
        {
            Console.WriteLine("Client called Register({0}, {1})", username, password);

            var userId = db.Register(username, password);
            userIds.GetOrAdd(username, userId);
            //TODO add somewhere if logged in automatically

            if (userId > 0)
                return new JCDSynchronizerReply("Registered successfully", JCDSynchronizerStatusCode.OK);
            else
                return new JCDSynchronizerReply("Registration failed", JCDSynchronizerStatusCode.FAILED);
        }

        [Authorize]
        public JCDSynchronizerReply LogIn(string username, string password)
        {
            Console.WriteLine("{0} called LogIn({1}, {2})", Context.User.Identity.Name, username, password);
            userIds.GetOrAdd(Context.User.Identity.Name, db.Login(username, password));
            // Logging in is handled a layer above.
            return new JCDSynchronizerReply("Logged in successfully", JCDSynchronizerStatusCode.OK);
        }

        /************************************************************************
         * The following functions assume that the user is logged in, and that  *
         * the server knows which VFSes belong to a particular user.            *
         ************************************************************************/
        [Authorize]
        public JCDSynchronizerReply LogOut()
        {
            Console.WriteLine("Client called LogOut()");

            // Do something so that the user is logged out.
            long dummy;
            userIds.TryRemove(Context.User.Identity.Name, out dummy);

            return new JCDSynchronizerReply("OK", JCDSynchronizerStatusCode.OK);
        }

        public JCDSynchronizerReply ListVFSes(string username, string password)
        {
            Console.WriteLine("Client called ListVFSes");

            var userId = db.Login(username, password);
            var list = db.ListVFSes(userId);

            if (list != null)
                return new JCDSynchronizerReply("OK", JCDSynchronizerStatusCode.OK, list);
            else
                return new JCDSynchronizerReply("Fail", JCDSynchronizerStatusCode.FAILED);
        }

        [Authorize]
        public JCDSynchronizerReply AddVFS(string vfsName, byte[] data)
        {
            Console.WriteLine("Client called AddVFS({0}, [data])", vfsName);

            var userId = GetUserId(Context);

            var vfsId = db.AddVFS(vfsName, userId, data);

            if (vfsId != null)
                return new JCDSynchronizerReply("OK", JCDSynchronizerStatusCode.OK, Convert.ToInt64(vfsId));
            else
                return new JCDSynchronizerReply("Fail", JCDSynchronizerStatusCode.FAILED);
        }

        [Authorize]
        public JCDSynchronizerReply DeleteVFS(long vfsId)
        {
            Console.WriteLine("Client called DeleteVFS");

            if (db.DeleteVFS(vfsId))
                return new JCDSynchronizerReply("OK", JCDSynchronizerStatusCode.OK);
            else
                return new JCDSynchronizerReply("FAIL", JCDSynchronizerStatusCode.FAILED);
        }

        [Authorize]
        public JCDSynchronizerReply RetrieveVFS(long vfsId)
        {
            Console.WriteLine("Client called RetrieveVFS");

            var tuple = db.RetrieveVFS(vfsId);

            if (tuple != null)
                return new JCDSynchronizerReply("OK", JCDSynchronizerStatusCode.OK, tuple);
            else
                return new JCDSynchronizerReply("FAIL", JCDSynchronizerStatusCode.FAILED);
        }

        [Authorize]
        public JCDSynchronizerReply RetrieveChanges(long vfsId, long lastVersionId)
        {
            Console.WriteLine("Client called RetrieveChanges({0}, {1})", vfsId, lastVersionId);
            Tuple<long, List<Tuple<int, byte[]>>> changes = null;

            try {
                changes = db.RetrieveChanges(vfsId, lastVersionId);
            }
            catch (Exception e) {
                return new JCDSynchronizerReply(e.Message, JCDSynchronizerStatusCode.FAILED);
            }

            return new JCDSynchronizerReply("OK", JCDSynchronizerStatusCode.OK, changes);
        }

        [Authorize]
        public JCDSynchronizerReply FileAdded(long vfsId, string path, long size, bool isFolder)
        {
            Console.WriteLine("Client called FileAdded ({0}, {1}, {2}, {3})", vfsId, path, size, isFolder);

            var id = db.AddFile(vfsId, path, size, isFolder);
            
            // Inform other clients.
            SendGroupMessage(JCDSynchronizationEventType.Added, id, path, size, isFolder);

            if (id != -1)
                return new JCDSynchronizerReply("OK", JCDSynchronizerStatusCode.OK, id);
            else
                return new JCDSynchronizerReply("FAIL", JCDSynchronizerStatusCode.FAILED);
        }

        [Authorize]
        public JCDSynchronizerReply FileDeleted(long vfsId, string path)
        {
            Console.WriteLine("Client called FileDeleted");

            var id = db.DeleteFile(vfsId, path);

            // Inform other clients.
            SendGroupMessage(JCDSynchronizationEventType.Deleted, id, path);

            if (id != -1)
                return new JCDSynchronizerReply("OK", JCDSynchronizerStatusCode.OK, id);
            else
                return new JCDSynchronizerReply("FAIL", JCDSynchronizerStatusCode.FAILED);
        }

        [Authorize]
        public JCDSynchronizerReply FileMoved(long vfsId, string oldPath, string newPath)
        {
            Console.WriteLine("Client called FileMoved");

            var id = db.MoveFile(vfsId, oldPath, newPath);
            // Inform other clients.
            SendGroupMessage(JCDSynchronizationEventType.Moved, id, oldPath, newPath);

            if (id != -1)
                return new JCDSynchronizerReply("OK", JCDSynchronizerStatusCode.OK, id);
            else
                return new JCDSynchronizerReply("FAIL", JCDSynchronizerStatusCode.FAILED);
        }

        [Authorize]
        public JCDSynchronizerReply FileModified(long vfsId, string path, long offset, byte[] data)
        {
            Console.WriteLine("Client called FileModified");

            var id = db.ModifyFile(vfsId, path, offset, data);

            // Inform other clients.
            SendGroupMessage(JCDSynchronizationEventType.Modified, id, path, offset, data);

            if (id != -1)
                return new JCDSynchronizerReply("OK", JCDSynchronizerStatusCode.OK, id);
            else
                return new JCDSynchronizerReply("FAIL", JCDSynchronizerStatusCode.FAILED);
        }

        [Authorize]
        public JCDSynchronizerReply FileResized(long vfsId, string path, long newSize)
        {
            Console.WriteLine("Client called FileResized({0}, {1}, {2}", vfsId, path, newSize);

            var id = db.ResizeFile(vfsId, path, newSize);

            // Inform other clients.
            SendGroupMessage(JCDSynchronizationEventType.Resized, id, path, newSize);

            if (id != -1)
                return new JCDSynchronizerReply("OK", JCDSynchronizerStatusCode.OK, id);
            else
                return new JCDSynchronizerReply("FAIL", JCDSynchronizerStatusCode.FAILED);
        }

        private JCDSynchronizerReply NotLoggedInMessage() {
            return new JCDSynchronizerReply("You are not logged in!", JCDSynchronizerStatusCode.FAILED);
        }

        private string GetUsername(HubCallerContext context) {
            return context.User.Identity.Name;
        }

        private long GetUserId(HubCallerContext context) {
            long userId = -1;
            userIds.TryGetValue(context.User.Identity.Name, out userId);
            return userId;
        }

        private void SendGroupMessage(JCDSynchronizationEventType type, params object[] args) {
            // Name of the group is the user name.
            var group = GetUsername(Context);
            switch (type) {
                case JCDSynchronizationEventType.Added:
                    ClientFileAdded(group, args);
                    break;
                case JCDSynchronizationEventType.Deleted:
                    ClientFileDeleted(group, args);
                    break;
                case JCDSynchronizationEventType.Moved:
                    ClientFileMoved(group, args);
                    break;
                case JCDSynchronizationEventType.Modified:
                    ClientFileModified(group, args);
                    break;
                case JCDSynchronizationEventType.Resized:
                    ClientFileResized(group, args);
                    break;
                default:
                    Console.WriteLine(String.Format("Execution of a change of type {0} failed", type));
                    break;
            }
        }

        private void ClientFileAdded(string group, object[] args) {
            var id = (long)args[0];
            var path = (string)args[1];
            var size = (long)args[2];
            var isFolder = (bool)args[3];
            Clients.OthersInGroup(group).FileAdded(id, path, size, isFolder);
        }

        private void ClientFileModified(string group, object[] args) {
            var id = (long)args[0];
            var path = (string)args[1];
            long offset = (long)args[2];
            byte[] data = (byte[])args[3];
            Clients.OthersInGroup(group).FileModified(id, path, offset, data);
        }

        private void ClientFileResized(string group, object[] args) {
            var id = (long)args[0];
            var path = (string)args[1];
            long newSize = (long)args[2];
            Clients.OthersInGroup(group).FileResized(id, path, newSize);
        }

        private void ClientFileMoved(string group, object[] args) {
            var id = (long)args[0];
            var oldPath = (string)args[1];
            string newPath = (string)args[2];
            Clients.OthersInGroup(group).FileMoved(id, oldPath, newPath);
        }

        private void ClientFileDeleted(string group, object[] args) {
            var id = (long)args[0];
            var path = (string)args[1];
            Clients.OthersInGroup(group).FileDeleted(id, path);
        }
    }
}
