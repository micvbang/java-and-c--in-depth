﻿namespace vfs.clients.desktop
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.formFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.createButton = new System.Windows.Forms.Button();
            this.openButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.loginButton = new System.Windows.Forms.Button();
            this.serverListButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.syncButton = new System.Windows.Forms.Button();
            this.vfsLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.backButton = new System.Windows.Forms.Button();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.searchOptionButton = new System.Windows.Forms.Button();
            this.searchMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.searchLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentSubfoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.everywhereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.caseSensitivityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sensitiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insensitiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.directoryListView = new System.Windows.Forms.ListView();
            this.nameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.typeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sizeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.formFlowLayoutPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.searchMenuStrip.SuspendLayout();
            this.lvMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // formFlowLayoutPanel
            // 
            this.formFlowLayoutPanel.Controls.Add(this.tableLayoutPanel1);
            this.formFlowLayoutPanel.Controls.Add(this.flowLayoutPanel3);
            this.formFlowLayoutPanel.Controls.Add(this.flowLayoutPanel1);
            this.formFlowLayoutPanel.Controls.Add(this.directoryListView);
            this.formFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.formFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.formFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(2, 1, 1, 1);
            this.formFlowLayoutPanel.Name = "formFlowLayoutPanel";
            this.formFlowLayoutPanel.Size = new System.Drawing.Size(695, 408);
            this.formFlowLayoutPanel.TabIndex = 0;
            this.formFlowLayoutPanel.WrapContents = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.21511F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.78489F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 3);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(689, 30);
            this.tableLayoutPanel1.TabIndex = 21;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.createButton);
            this.flowLayoutPanel4.Controls.Add(this.openButton);
            this.flowLayoutPanel4.Controls.Add(this.deleteButton);
            this.flowLayoutPanel4.Controls.Add(this.closeButton);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(367, 27);
            this.flowLayoutPanel4.TabIndex = 15;
            this.flowLayoutPanel4.WrapContents = false;
            // 
            // createButton
            // 
            this.createButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createButton.Location = new System.Drawing.Point(0, 0);
            this.createButton.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(86, 23);
            this.createButton.TabIndex = 0;
            this.createButton.Text = "Create VFS";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // openButton
            // 
            this.openButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openButton.Location = new System.Drawing.Point(92, 0);
            this.openButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(86, 23);
            this.openButton.TabIndex = 1;
            this.openButton.Text = "Open VFS";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.Location = new System.Drawing.Point(184, 0);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(86, 23);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "Delete VFS";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Enabled = false;
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.Location = new System.Drawing.Point(276, 0);
            this.closeButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(86, 23);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close VFS";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.syncButton);
            this.flowLayoutPanel2.Controls.Add(this.loginButton);
            this.flowLayoutPanel2.Controls.Add(this.serverListButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(376, 3);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel2.Size = new System.Drawing.Size(310, 27);
            this.flowLayoutPanel2.TabIndex = 18;
            this.flowLayoutPanel2.WrapContents = false;
            // 
            // loginButton
            // 
            this.loginButton.Enabled = false;
            this.loginButton.Location = new System.Drawing.Point(128, 0);
            this.loginButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(86, 23);
            this.loginButton.TabIndex = 13;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // serverListButton
            // 
            this.serverListButton.Location = new System.Drawing.Point(36, 0);
            this.serverListButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.serverListButton.Name = "serverListButton";
            this.serverListButton.Size = new System.Drawing.Size(86, 23);
            this.serverListButton.TabIndex = 10;
            this.serverListButton.Text = "Server VFS";
            this.serverListButton.UseVisualStyleBackColor = true;
            this.serverListButton.Click += new System.EventHandler(this.serverListButton_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanel5);
            this.flowLayoutPanel3.Controls.Add(this.vfsLabel);
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(2, 33);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(688, 30);
            this.flowLayoutPanel3.TabIndex = 23;
            this.flowLayoutPanel3.WrapContents = false;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Controls.Add(this.searchTextBox);
            this.flowLayoutPanel5.Controls.Add(this.searchOptionButton);
            this.flowLayoutPanel5.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(412, 0);
            this.flowLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(276, 30);
            this.flowLayoutPanel5.TabIndex = 14;
            // 
            // syncButton
            // 
            this.syncButton.Enabled = false;
            this.syncButton.Location = new System.Drawing.Point(221, 0);
            this.syncButton.Margin = new System.Windows.Forms.Padding(3, 0, 4, 3);
            this.syncButton.Name = "syncButton";
            this.syncButton.Size = new System.Drawing.Size(86, 23);
            this.syncButton.TabIndex = 13;
            this.syncButton.Text = "Synchronize";
            this.syncButton.UseVisualStyleBackColor = true;
            this.syncButton.Click += new System.EventHandler(this.syncButton_Click);
            // 
            // vfsLabel
            // 
            this.vfsLabel.Enabled = false;
            this.vfsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vfsLabel.Location = new System.Drawing.Point(4, 6);
            this.vfsLabel.Margin = new System.Windows.Forms.Padding(0, 6, 3, 3);
            this.vfsLabel.Name = "vfsLabel";
            this.vfsLabel.Size = new System.Drawing.Size(405, 18);
            this.vfsLabel.TabIndex = 17;
            this.vfsLabel.Text = "No VFS mounted..";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.backButton);
            this.flowLayoutPanel1.Controls.Add(this.pathTextBox);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 63);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(688, 30);
            this.flowLayoutPanel1.TabIndex = 10;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // backButton
            // 
            this.backButton.AutoSize = true;
            this.backButton.Enabled = false;
            this.backButton.Location = new System.Drawing.Point(3, 1);
            this.backButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(44, 23);
            this.backButton.TabIndex = 5;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.BackButton_Click);
            this.backButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.backButton_KeyDown);
            // 
            // pathTextBox
            // 
            this.pathTextBox.AllowDrop = true;
            this.pathTextBox.Enabled = false;
            this.pathTextBox.Location = new System.Drawing.Point(53, 3);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(634, 20);
            this.pathTextBox.TabIndex = 6;
            this.pathTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pathTextBox_KeyDown);
            // 
            // searchOptionButton
            // 
            this.searchOptionButton.ContextMenuStrip = this.searchMenuStrip;
            this.searchOptionButton.Enabled = false;
            this.searchOptionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchOptionButton.Location = new System.Drawing.Point(4, 3);
            this.searchOptionButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.searchOptionButton.Name = "searchOptionButton";
            this.searchOptionButton.Size = new System.Drawing.Size(20, 22);
            this.searchOptionButton.TabIndex = 8;
            this.searchOptionButton.Text = "v";
            this.searchOptionButton.UseVisualStyleBackColor = true;
            this.searchOptionButton.Click += new System.EventHandler(this.searchOptionButton_Click);
            // 
            // searchMenuStrip
            // 
            this.searchMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchLocationToolStripMenuItem,
            this.caseSensitivityToolStripMenuItem});
            this.searchMenuStrip.Name = "searchMenuStrip";
            this.searchMenuStrip.Size = new System.Drawing.Size(159, 48);
            this.searchMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.searchMenuStrip_Opening);
            // 
            // searchLocationToolStripMenuItem
            // 
            this.searchLocationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currentFolderToolStripMenuItem,
            this.currentSubfoldersToolStripMenuItem,
            this.everywhereToolStripMenuItem});
            this.searchLocationToolStripMenuItem.Name = "searchLocationToolStripMenuItem";
            this.searchLocationToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.searchLocationToolStripMenuItem.Text = "Search Location";
            // 
            // currentFolderToolStripMenuItem
            // 
            this.currentFolderToolStripMenuItem.CheckOnClick = true;
            this.currentFolderToolStripMenuItem.Name = "currentFolderToolStripMenuItem";
            this.currentFolderToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.currentFolderToolStripMenuItem.Text = "Current Folder";
            this.currentFolderToolStripMenuItem.Click += new System.EventHandler(this.currentFolderToolStripMenuItem_Click);
            // 
            // currentSubfoldersToolStripMenuItem
            // 
            this.currentSubfoldersToolStripMenuItem.CheckOnClick = true;
            this.currentSubfoldersToolStripMenuItem.Name = "currentSubfoldersToolStripMenuItem";
            this.currentSubfoldersToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.currentSubfoldersToolStripMenuItem.Text = "Curr + Subfolders";
            this.currentSubfoldersToolStripMenuItem.Click += new System.EventHandler(this.currentSubfoldersToolStripMenuItem_Click);
            // 
            // everywhereToolStripMenuItem
            // 
            this.everywhereToolStripMenuItem.CheckOnClick = true;
            this.everywhereToolStripMenuItem.Name = "everywhereToolStripMenuItem";
            this.everywhereToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.everywhereToolStripMenuItem.Text = "Everywhere";
            this.everywhereToolStripMenuItem.Click += new System.EventHandler(this.everywhereToolStripMenuItem_Click);
            // 
            // caseSensitivityToolStripMenuItem
            // 
            this.caseSensitivityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sensitiveToolStripMenuItem,
            this.insensitiveToolStripMenuItem});
            this.caseSensitivityToolStripMenuItem.Name = "caseSensitivityToolStripMenuItem";
            this.caseSensitivityToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.caseSensitivityToolStripMenuItem.Text = "Case Sensitivity";
            // 
            // sensitiveToolStripMenuItem
            // 
            this.sensitiveToolStripMenuItem.CheckOnClick = true;
            this.sensitiveToolStripMenuItem.Name = "sensitiveToolStripMenuItem";
            this.sensitiveToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.sensitiveToolStripMenuItem.Text = "Sensitive";
            this.sensitiveToolStripMenuItem.Click += new System.EventHandler(this.sensitiveToolStripMenuItem_Click);
            // 
            // insensitiveToolStripMenuItem
            // 
            this.insensitiveToolStripMenuItem.CheckOnClick = true;
            this.insensitiveToolStripMenuItem.Name = "insensitiveToolStripMenuItem";
            this.insensitiveToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.insensitiveToolStripMenuItem.Text = "Insensitive";
            this.insensitiveToolStripMenuItem.Click += new System.EventHandler(this.insensitiveToolStripMenuItem_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Enabled = false;
            this.searchTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchTextBox.Location = new System.Drawing.Point(24, 4);
            this.searchTextBox.Margin = new System.Windows.Forms.Padding(0, 4, 0, 3);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(252, 20);
            this.searchTextBox.TabIndex = 7;
            this.searchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchTextBox_KeyDown);
            // 
            // directoryListView
            // 
            this.directoryListView.AllowDrop = true;
            this.directoryListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.typeColumn,
            this.sizeColumn});
            this.directoryListView.ContextMenuStrip = this.lvMenuStrip;
            this.directoryListView.Enabled = false;
            this.directoryListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.directoryListView.HideSelection = false;
            this.directoryListView.Location = new System.Drawing.Point(6, 99);
            this.directoryListView.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.directoryListView.Name = "directoryListView";
            this.directoryListView.Size = new System.Drawing.Size(684, 304);
            this.directoryListView.SmallImageList = this.imageList1;
            this.directoryListView.TabIndex = 4;
            this.directoryListView.UseCompatibleStateImageBehavior = false;
            this.directoryListView.View = System.Windows.Forms.View.Details;
            this.directoryListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.directoryListView_ColumnClick);
            this.directoryListView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.directoryListView_ItemDrag);
            this.directoryListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.directoryListView_DragDrop);
            this.directoryListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.directoryListView_DragEnter);
            this.directoryListView.DragOver += new System.Windows.Forms.DragEventHandler(this.directoryListView_DragOver);
            this.directoryListView.DragLeave += new System.EventHandler(this.directoryListView_DragLeave);
            this.directoryListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.directoryListView_KeyDown);
            this.directoryListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.directoryListView_MouseDoubleClick);
            // 
            // nameColumn
            // 
            this.nameColumn.Text = "Name";
            this.nameColumn.Width = 100;
            // 
            // typeColumn
            // 
            this.typeColumn.Text = "Type";
            // 
            // sizeColumn
            // 
            this.sizeColumn.Text = "Size";
            // 
            // lvMenuStrip
            // 
            this.lvMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.deleteStripMenuItem1});
            this.lvMenuStrip.Name = "dirMenuStrip";
            this.lvMenuStrip.Size = new System.Drawing.Size(118, 180);
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createFileToolStripMenuItem,
            this.createDirectoryToolStripMenuItem});
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.createToolStripMenuItem.Text = "Create";
            // 
            // createFileToolStripMenuItem
            // 
            this.createFileToolStripMenuItem.Name = "createFileToolStripMenuItem";
            this.createFileToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.createFileToolStripMenuItem.Text = "Create File";
            this.createFileToolStripMenuItem.Click += new System.EventHandler(this.createFileToolStripMenuItem_Click);
            // 
            // createDirectoryToolStripMenuItem
            // 
            this.createDirectoryToolStripMenuItem.Name = "createDirectoryToolStripMenuItem";
            this.createDirectoryToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.createDirectoryToolStripMenuItem.Text = "Create Directory";
            this.createDirectoryToolStripMenuItem.Click += new System.EventHandler(this.createDirectoryToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importFileToolStripMenuItem,
            this.importFolderToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // importFileToolStripMenuItem
            // 
            this.importFileToolStripMenuItem.Name = "importFileToolStripMenuItem";
            this.importFileToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.importFileToolStripMenuItem.Text = "Import File";
            this.importFileToolStripMenuItem.Click += new System.EventHandler(this.importFileToolStripMenuItem_Click);
            // 
            // importFolderToolStripMenuItem
            // 
            this.importFolderToolStripMenuItem.Name = "importFolderToolStripMenuItem";
            this.importFolderToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.importFolderToolStripMenuItem.Text = "Import Folder";
            this.importFolderToolStripMenuItem.Click += new System.EventHandler(this.importFolderToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // deleteStripMenuItem1
            // 
            this.deleteStripMenuItem1.Name = "deleteStripMenuItem1";
            this.deleteStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.deleteStripMenuItem1.Text = "Delete";
            this.deleteStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Folder.png");
            this.imageList1.Images.SetKeyName(1, "Icon-Document.png");
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 408);
            this.Controls.Add(this.formFlowLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "VFS Ravioli";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.formFlowLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.searchMenuStrip.ResumeLayout(false);
            this.lvMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel formFlowLayoutPanel;
        private System.Windows.Forms.ContextMenuStrip lvMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ListView directoryListView;
        private System.Windows.Forms.ColumnHeader nameColumn;
        private System.Windows.Forms.ColumnHeader typeColumn;
        private System.Windows.Forms.ColumnHeader sizeColumn;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFolderToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip searchMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem searchLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem currentFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem currentSubfoldersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem everywhereToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem caseSensitivityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sensitiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insensitiveToolStripMenuItem;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button searchOptionButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label vfsLabel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button serverListButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button syncButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;

    }
}

