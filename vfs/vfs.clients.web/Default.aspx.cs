﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using vfs.core;
using vfs.core.indexing;
using vfs.exceptions;

namespace vfs.clients.web {

    public partial class _Default : Page {

        private void showPage() {
            ls();
            checkOperationsEnabled(null, null);
        }

        private void ls() {
            filesView.DataSource = Global.vfsSession.ListCurrentDirectory();
            filesView.DataBind();
        }

        protected void checkOperationsEnabled(object sender, EventArgs e) {
            bool atLeastOne = false;
            foreach(GridViewRow r in filesView.Rows) {
                CheckBox c = (CheckBox) r.Cells[0].Controls[1];
                if(c.Checked) {
                    atLeastOne = true;
                    break;
                }
            }
            copy.Enabled = cut.Enabled = delete.Enabled = atLeastOne;

            paste.Enabled = Global.vfsSession.clipBoardNonEmpty();
        }

        protected void Page_Load(object sender, EventArgs e) {
            Master.checkSession();

            if(!Page.IsPostBack) {
                showPage();
            }
        }

        protected void up(object sender, EventArgs e) {
            Master.checkSession();

            Global.vfsSession.MoveBack();

            showPage();
        }

        protected void cd(object sender, EventArgs e) {
            Master.checkSession();

            LinkButton b = (LinkButton) sender;
            Global.vfsSession.MoveInto(Server.HtmlDecode(b.Text), false);

            showPage();
        }

        private string[] getSelectedListViewItemTexts() {
            List<string> names = new List<string>();
            foreach(GridViewRow r in filesView.Rows) {
                CheckBox c = (CheckBox) r.FindControl("selectBox");
                if(c.Checked) {
                    Label l = (Label) r.FindControl("fileName");
                    names.Add(Server.HtmlDecode(l.Text));
                }
            }
            return names.ToArray();
        }

        protected void makeCopy(object sender, EventArgs e) {
            try {
                Master.checkSession();

                var names = getSelectedListViewItemTexts();
                Global.vfsSession.Copy(names);
            }
            catch(Exception ex) {
                Master.errorText = ex.ToString();
            }

            showPage();
        }

        protected void makeCut(object sender, EventArgs e) {
            try {
                Master.checkSession();

                var names = getSelectedListViewItemTexts();
                Global.vfsSession.Cut(names);

                //TODO show item with greyed out icon
            }
            catch(Exception ex) {
                Master.errorText = ex.ToString();
            }

            showPage();
        }

        protected void makePaste(object sender, EventArgs e) {
            try {
                Master.checkSession();

                Global.vfsSession.Paste();
                ls();
            }
            catch(Exception ex) {
                Master.errorText = ex.ToString();
            }

            showPage();
        }

        protected void makeDelete(object sender, EventArgs e) {
            try {
                Master.checkSession();

                var names = getSelectedListViewItemTexts();

                Global.vfsSession.Delete(names);
            }
            catch(Exception ex) {
                Master.errorText = ex.ToString();
            }

            showPage();
        }

        protected void makeCreateFolder(object sender, EventArgs e) {
            string newFolderName = "New Folder";
            uint index = 1;
            bool success = false;
            while(!success) {
                try {
                    Global.vfsSession.CreateDir(newFolderName);
                    success = true;
                }
                catch(FileAlreadyExistsException ex) {
                    newFolderName = String.Format("New Folder ({0})", index++);
                }
            }

            showPage();

            DirectoryEntry[] contents = Global.vfsSession.ListCurrentDirectory();
            for(int i = 0; i < contents.Length; i++) {
                if(contents[i].Name == newFolderName) {
                    RowEditing(i);
                    break;
                }
            }
        }

        protected void RowEditing(object sender, GridViewEditEventArgs e) {
            RowEditing(e.NewEditIndex);
        }

        private void RowEditing(int rowIndex) {
            Label l = (Label) filesView.Rows[rowIndex].FindControl("fileName");
            HttpContext.Current.Session["editOldName"] = Server.HtmlDecode(l.Text);

            filesView.EditIndex = rowIndex;
            showPage();
            filesView.Rows[rowIndex].FindControl("changeFileName").Focus();
            Page.Form.DefaultButton = filesView.Rows[rowIndex].FindControl("saveButton").UniqueID;
        }

        protected void RowCancelingEditing(object sender, GridViewCancelEditEventArgs e) {
            e.Cancel = true;
            filesView.EditIndex = -1;
            showPage();
        }

        protected void RowUpdating(object sender, GridViewUpdateEventArgs e) {
            string newName = e.NewValues["Name"].ToString();

            char[] invalid = System.IO.Path.GetInvalidPathChars();

            foreach(char c in newName) { //Ugly, slow, works (probably)
                newName = newName.Replace(c.ToString(), "");
            }

            filesView.EditIndex = -1;

            try {
                Global.vfsSession.Rename((string) HttpContext.Current.Session["editOldName"], newName);
            }
            catch(Exception ex) {
                Master.errorText = "While trying to rename \"" + HttpContext.Current.Session["editOldName"] + "\" to \""
                    + newName + "\"\n" + ex.ToString();
            }

            showPage();
        }
    }
}