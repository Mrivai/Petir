namespace Petir
{
    partial class XBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XBrowser));
            this.MainPanel = new System.Windows.Forms.SplitContainer();
            this.BrowserPnl = new System.Windows.Forms.Panel();
            this.ToolbarBawah = new System.Windows.Forms.Panel();
            this.XcodePnl = new System.Windows.Forms.Panel();
            this.SaveXcode = new System.Windows.Forms.Button();
            this.OpenXcode = new System.Windows.Forms.Button();
            this.AddXcode = new System.Windows.Forms.Button();
            this.RunXcode = new System.Windows.Forms.Button();
            this.FindPnl = new System.Windows.Forms.Panel();
            this.CloseFind = new System.Windows.Forms.Button();
            this.FindNext = new System.Windows.Forms.Button();
            this.FindPrev = new System.Windows.Forms.Button();
            this.FindBtn = new System.Windows.Forms.Button();
            this.Find = new System.Windows.Forms.TextBox();
            this.ToolbarAtas = new System.Windows.Forms.Panel();
            this.XMenu = new System.Windows.Forms.Button();
            this.XDomain = new System.Windows.Forms.TextBox();
            this.Download = new System.Windows.Forms.Button();
            this.Next = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.XRefresh = new System.Windows.Forms.Button();
            this.Xgo = new System.Windows.Forms.Button();
            this.XcodeToggle = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Log = new System.Windows.Forms.TabPage();
            this.Xcode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.ConsoleLog = new FastColoredTextBoxNS.FastColoredTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.MainPanel)).BeginInit();
            this.MainPanel.Panel1.SuspendLayout();
            this.MainPanel.Panel2.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.BrowserPnl.SuspendLayout();
            this.ToolbarBawah.SuspendLayout();
            this.XcodePnl.SuspendLayout();
            this.FindPnl.SuspendLayout();
            this.ToolbarAtas.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.Log.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Xcode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConsoleLog)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.Transparent;
            this.MainPanel.CausesValidation = false;
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // MainPanel.Panel1
            // 
            this.MainPanel.Panel1.Controls.Add(this.BrowserPnl);
            this.MainPanel.Panel1.Controls.Add(this.ToolbarAtas);
            // 
            // MainPanel.Panel2
            // 
            this.MainPanel.Panel2.Controls.Add(this.tabControl1);
            this.MainPanel.Panel2Collapsed = true;
            this.MainPanel.Size = new System.Drawing.Size(709, 450);
            this.MainPanel.SplitterDistance = 300;
            this.MainPanel.TabIndex = 8;
            // 
            // BrowserPnl
            // 
            this.BrowserPnl.BackColor = System.Drawing.Color.Transparent;
            this.BrowserPnl.Controls.Add(this.ToolbarBawah);
            this.BrowserPnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BrowserPnl.Location = new System.Drawing.Point(0, 30);
            this.BrowserPnl.Name = "BrowserPnl";
            this.BrowserPnl.Size = new System.Drawing.Size(709, 420);
            this.BrowserPnl.TabIndex = 4;
            // 
            // ToolbarBawah
            // 
            this.ToolbarBawah.BackColor = System.Drawing.Color.Blue;
            this.ToolbarBawah.Controls.Add(this.XcodePnl);
            this.ToolbarBawah.Controls.Add(this.FindPnl);
            this.ToolbarBawah.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ToolbarBawah.Location = new System.Drawing.Point(0, 390);
            this.ToolbarBawah.Name = "ToolbarBawah";
            this.ToolbarBawah.Size = new System.Drawing.Size(709, 30);
            this.ToolbarBawah.TabIndex = 4;
            this.ToolbarBawah.Visible = false;
            // 
            // XcodePnl
            // 
            this.XcodePnl.Controls.Add(this.SaveXcode);
            this.XcodePnl.Controls.Add(this.OpenXcode);
            this.XcodePnl.Controls.Add(this.AddXcode);
            this.XcodePnl.Controls.Add(this.RunXcode);
            this.XcodePnl.Location = new System.Drawing.Point(6, 0);
            this.XcodePnl.Name = "XcodePnl";
            this.XcodePnl.Size = new System.Drawing.Size(132, 30);
            this.XcodePnl.TabIndex = 12;
            this.XcodePnl.Visible = false;
            // 
            // SaveXcode
            // 
            this.SaveXcode.BackColor = System.Drawing.Color.Transparent;
            this.SaveXcode.BackgroundImage = global::Petir.Properties.Resources.Save;
            this.SaveXcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SaveXcode.FlatAppearance.BorderSize = 0;
            this.SaveXcode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.SaveXcode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.SaveXcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveXcode.Location = new System.Drawing.Point(96, 2);
            this.SaveXcode.Name = "SaveXcode";
            this.SaveXcode.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.SaveXcode.Size = new System.Drawing.Size(25, 25);
            this.SaveXcode.TabIndex = 12;
            this.SaveXcode.UseVisualStyleBackColor = false;
            this.SaveXcode.Click += new System.EventHandler(this.SaveXcode_Click);
            // 
            // OpenXcode
            // 
            this.OpenXcode.BackColor = System.Drawing.Color.Transparent;
            this.OpenXcode.BackgroundImage = global::Petir.Properties.Resources.OpenFolder;
            this.OpenXcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.OpenXcode.FlatAppearance.BorderSize = 0;
            this.OpenXcode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.OpenXcode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.OpenXcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenXcode.Location = new System.Drawing.Point(65, 2);
            this.OpenXcode.Name = "OpenXcode";
            this.OpenXcode.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.OpenXcode.Size = new System.Drawing.Size(25, 25);
            this.OpenXcode.TabIndex = 11;
            this.OpenXcode.UseVisualStyleBackColor = false;
            this.OpenXcode.Click += new System.EventHandler(this.OpenXcode_Click);
            // 
            // AddXcode
            // 
            this.AddXcode.BackColor = System.Drawing.Color.Transparent;
            this.AddXcode.BackgroundImage = global::Petir.Properties.Resources.Add;
            this.AddXcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddXcode.FlatAppearance.BorderSize = 0;
            this.AddXcode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.AddXcode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.AddXcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddXcode.Location = new System.Drawing.Point(34, 2);
            this.AddXcode.Name = "AddXcode";
            this.AddXcode.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.AddXcode.Size = new System.Drawing.Size(25, 25);
            this.AddXcode.TabIndex = 10;
            this.AddXcode.UseVisualStyleBackColor = false;
            this.AddXcode.Click += new System.EventHandler(this.AddXcode_Click);
            // 
            // RunXcode
            // 
            this.RunXcode.BackColor = System.Drawing.Color.Transparent;
            this.RunXcode.BackgroundImage = global::Petir.Properties.Resources.Run;
            this.RunXcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RunXcode.FlatAppearance.BorderSize = 0;
            this.RunXcode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.RunXcode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.RunXcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RunXcode.Location = new System.Drawing.Point(3, 2);
            this.RunXcode.Name = "RunXcode";
            this.RunXcode.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.RunXcode.Size = new System.Drawing.Size(25, 25);
            this.RunXcode.TabIndex = 9;
            this.RunXcode.UseVisualStyleBackColor = false;
            this.RunXcode.Click += new System.EventHandler(this.XcodeRun_Click);
            // 
            // FindPnl
            // 
            this.FindPnl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FindPnl.BackColor = System.Drawing.Color.Transparent;
            this.FindPnl.Controls.Add(this.CloseFind);
            this.FindPnl.Controls.Add(this.FindNext);
            this.FindPnl.Controls.Add(this.FindPrev);
            this.FindPnl.Controls.Add(this.FindBtn);
            this.FindPnl.Controls.Add(this.Find);
            this.FindPnl.Location = new System.Drawing.Point(412, 0);
            this.FindPnl.Name = "FindPnl";
            this.FindPnl.Size = new System.Drawing.Size(297, 30);
            this.FindPnl.TabIndex = 11;
            this.FindPnl.Visible = false;
            // 
            // CloseFind
            // 
            this.CloseFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseFind.BackColor = System.Drawing.Color.Transparent;
            this.CloseFind.BackgroundImage = global::Petir.Properties.Resources.Cancel;
            this.CloseFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseFind.FlatAppearance.BorderSize = 0;
            this.CloseFind.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.CloseFind.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CloseFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseFind.Location = new System.Drawing.Point(4, 4);
            this.CloseFind.Name = "CloseFind";
            this.CloseFind.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.CloseFind.Size = new System.Drawing.Size(15, 15);
            this.CloseFind.TabIndex = 12;
            this.CloseFind.UseVisualStyleBackColor = false;
            this.CloseFind.Click += new System.EventHandler(this.CloseFind_Click);
            // 
            // FindNext
            // 
            this.FindNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FindNext.BackColor = System.Drawing.Color.Transparent;
            this.FindNext.BackgroundImage = global::Petir.Properties.Resources.Next;
            this.FindNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FindNext.FlatAppearance.BorderSize = 0;
            this.FindNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.FindNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.FindNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FindNext.Location = new System.Drawing.Point(272, 2);
            this.FindNext.Name = "FindNext";
            this.FindNext.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.FindNext.Size = new System.Drawing.Size(25, 25);
            this.FindNext.TabIndex = 11;
            this.FindNext.UseVisualStyleBackColor = false;
            this.FindNext.Click += new System.EventHandler(this.FindNext_Click);
            // 
            // FindPrev
            // 
            this.FindPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FindPrev.BackColor = System.Drawing.Color.Transparent;
            this.FindPrev.BackgroundImage = global::Petir.Properties.Resources.Previous;
            this.FindPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FindPrev.FlatAppearance.BorderSize = 0;
            this.FindPrev.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.FindPrev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.FindPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FindPrev.Location = new System.Drawing.Point(241, 3);
            this.FindPrev.Name = "FindPrev";
            this.FindPrev.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.FindPrev.Size = new System.Drawing.Size(25, 25);
            this.FindPrev.TabIndex = 10;
            this.FindPrev.UseVisualStyleBackColor = false;
            this.FindPrev.Click += new System.EventHandler(this.FindPrev_Click);
            // 
            // FindBtn
            // 
            this.FindBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FindBtn.BackColor = System.Drawing.Color.Transparent;
            this.FindBtn.BackgroundImage = global::Petir.Properties.Resources.Search;
            this.FindBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FindBtn.FlatAppearance.BorderSize = 0;
            this.FindBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.FindBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.FindBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FindBtn.Location = new System.Drawing.Point(210, 3);
            this.FindBtn.Name = "FindBtn";
            this.FindBtn.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.FindBtn.Size = new System.Drawing.Size(25, 25);
            this.FindBtn.TabIndex = 9;
            this.FindBtn.UseVisualStyleBackColor = false;
            this.FindBtn.Click += new System.EventHandler(this.FindBtn_Click);
            // 
            // Find
            // 
            this.Find.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Find.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Find.Location = new System.Drawing.Point(28, 4);
            this.Find.Name = "Find";
            this.Find.Size = new System.Drawing.Size(179, 23);
            this.Find.TabIndex = 4;
            this.Find.Click += new System.EventHandler(this.Find_TextChanged);
            // 
            // ToolbarAtas
            // 
            this.ToolbarAtas.BackColor = System.Drawing.Color.Black;
            this.ToolbarAtas.Controls.Add(this.XMenu);
            this.ToolbarAtas.Controls.Add(this.XDomain);
            this.ToolbarAtas.Controls.Add(this.Download);
            this.ToolbarAtas.Controls.Add(this.Next);
            this.ToolbarAtas.Controls.Add(this.Back);
            this.ToolbarAtas.Controls.Add(this.XRefresh);
            this.ToolbarAtas.Controls.Add(this.Xgo);
            this.ToolbarAtas.Controls.Add(this.XcodeToggle);
            this.ToolbarAtas.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolbarAtas.Location = new System.Drawing.Point(0, 0);
            this.ToolbarAtas.Name = "ToolbarAtas";
            this.ToolbarAtas.Size = new System.Drawing.Size(709, 30);
            this.ToolbarAtas.TabIndex = 2;
            // 
            // XMenu
            // 
            this.XMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.XMenu.BackColor = System.Drawing.Color.Transparent;
            this.XMenu.BackgroundImage = global::Petir.Properties.Resources.Menu;
            this.XMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.XMenu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.XMenu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.XMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.XMenu.Location = new System.Drawing.Point(676, 2);
            this.XMenu.Name = "XMenu";
            this.XMenu.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.XMenu.Size = new System.Drawing.Size(25, 25);
            this.XMenu.TabIndex = 9;
            this.XMenu.UseVisualStyleBackColor = false;
            this.XMenu.Click += new System.EventHandler(this.Menu_Click);
            // 
            // XDomain
            // 
            this.XDomain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.XDomain.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XDomain.Location = new System.Drawing.Point(133, 4);
            this.XDomain.Name = "XDomain";
            this.XDomain.Size = new System.Drawing.Size(475, 23);
            this.XDomain.TabIndex = 3;
            this.XDomain.Click += new System.EventHandler(this.XDomain_Click);
            this.XDomain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.XDomain_KeyDown);
            // 
            // Download
            // 
            this.Download.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Download.BackColor = System.Drawing.Color.Transparent;
            this.Download.BackgroundImage = global::Petir.Properties.Resources.Download;
            this.Download.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Download.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Download.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Download.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Download.Location = new System.Drawing.Point(645, 2);
            this.Download.Name = "Download";
            this.Download.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.Download.Size = new System.Drawing.Size(25, 25);
            this.Download.TabIndex = 8;
            this.Download.UseVisualStyleBackColor = false;
            this.Download.Click += new System.EventHandler(this.Download_Click);
            // 
            // Next
            // 
            this.Next.BackColor = System.Drawing.Color.Transparent;
            this.Next.BackgroundImage = global::Petir.Properties.Resources.Next;
            this.Next.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Next.Enabled = false;
            this.Next.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Next.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Next.Location = new System.Drawing.Point(102, 2);
            this.Next.Name = "Next";
            this.Next.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.Next.Size = new System.Drawing.Size(25, 25);
            this.Next.TabIndex = 7;
            this.Next.UseVisualStyleBackColor = false;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // Back
            // 
            this.Back.BackColor = System.Drawing.Color.Transparent;
            this.Back.BackgroundImage = global::Petir.Properties.Resources.Previous;
            this.Back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Back.Enabled = false;
            this.Back.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Back.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Back.Location = new System.Drawing.Point(71, 2);
            this.Back.Name = "Back";
            this.Back.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.Back.Size = new System.Drawing.Size(25, 25);
            this.Back.TabIndex = 6;
            this.Back.UseVisualStyleBackColor = false;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // XRefresh
            // 
            this.XRefresh.BackColor = System.Drawing.Color.Transparent;
            this.XRefresh.BackgroundImage = global::Petir.Properties.Resources.Reload;
            this.XRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.XRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.XRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.XRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.XRefresh.Location = new System.Drawing.Point(40, 2);
            this.XRefresh.Name = "XRefresh";
            this.XRefresh.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.XRefresh.Size = new System.Drawing.Size(25, 25);
            this.XRefresh.TabIndex = 5;
            this.XRefresh.UseVisualStyleBackColor = false;
            this.XRefresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // Xgo
            // 
            this.Xgo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Xgo.BackColor = System.Drawing.Color.Transparent;
            this.Xgo.BackgroundImage = global::Petir.Properties.Resources.go;
            this.Xgo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Xgo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Xgo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Xgo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Xgo.Location = new System.Drawing.Point(614, 2);
            this.Xgo.Name = "Xgo";
            this.Xgo.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.Xgo.Size = new System.Drawing.Size(25, 25);
            this.Xgo.TabIndex = 4;
            this.Xgo.UseVisualStyleBackColor = false;
            this.Xgo.Click += new System.EventHandler(this.Xgo_Click);
            // 
            // XcodeToggle
            // 
            this.XcodeToggle.BackColor = System.Drawing.Color.Transparent;
            this.XcodeToggle.BackgroundImage = global::Petir.Properties.Resources.code;
            this.XcodeToggle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.XcodeToggle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.XcodeToggle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.XcodeToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.XcodeToggle.Location = new System.Drawing.Point(6, 2);
            this.XcodeToggle.Name = "XcodeToggle";
            this.XcodeToggle.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.XcodeToggle.Size = new System.Drawing.Size(25, 25);
            this.XcodeToggle.TabIndex = 3;
            this.XcodeToggle.UseVisualStyleBackColor = false;
            this.XcodeToggle.Click += new System.EventHandler(this.XcodeToggle_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.Log);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(709, 146);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Xcode);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(701, 120);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Autobot";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Log
            // 
            this.Log.Controls.Add(this.ConsoleLog);
            this.Log.Location = new System.Drawing.Point(4, 22);
            this.Log.Name = "Log";
            this.Log.Padding = new System.Windows.Forms.Padding(3);
            this.Log.Size = new System.Drawing.Size(701, 120);
            this.Log.TabIndex = 1;
            this.Log.Text = "Log";
            this.Log.UseVisualStyleBackColor = true;
            // 
            // Xcode
            // 
            this.Xcode.AutoCompleteBrackets = true;
            this.Xcode.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.Xcode.AutoScrollMinSize = new System.Drawing.Size(83, 14);
            this.Xcode.AutoSize = true;
            this.Xcode.BackBrush = null;
            this.Xcode.CharHeight = 14;
            this.Xcode.CharWidth = 8;
            this.Xcode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Xcode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.Xcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Xcode.IsReplaceMode = false;
            this.Xcode.Location = new System.Drawing.Point(3, 3);
            this.Xcode.Name = "Xcode";
            this.Xcode.Paddings = new System.Windows.Forms.Padding(0);
            this.Xcode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.Xcode.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("Xcode.ServiceColors")));
            this.Xcode.Size = new System.Drawing.Size(695, 114);
            this.Xcode.TabIndex = 2;
            this.Xcode.Text = "autobot";
            this.Xcode.Zoom = 100;
            // 
            // ConsoleLog
            // 
            this.ConsoleLog.AutoCompleteBrackets = true;
            this.ConsoleLog.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.ConsoleLog.AutoScrollMinSize = new System.Drawing.Size(83, 14);
            this.ConsoleLog.AutoSize = true;
            this.ConsoleLog.BackBrush = null;
            this.ConsoleLog.CharHeight = 14;
            this.ConsoleLog.CharWidth = 8;
            this.ConsoleLog.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ConsoleLog.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.ConsoleLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConsoleLog.IsReplaceMode = false;
            this.ConsoleLog.Location = new System.Drawing.Point(3, 3);
            this.ConsoleLog.Name = "ConsoleLog";
            this.ConsoleLog.Paddings = new System.Windows.Forms.Padding(0);
            this.ConsoleLog.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.ConsoleLog.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("ConsoleLog.ServiceColors")));
            this.ConsoleLog.Size = new System.Drawing.Size(695, 114);
            this.ConsoleLog.TabIndex = 3;
            this.ConsoleLog.Text = "autobot";
            this.ConsoleLog.Zoom = 100;
            // 
            // XBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(709, 450);
            this.Controls.Add(this.MainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "XBrowser";
            this.Opacity = 0.5D;
            this.Tag = "";
            this.Text = "XBrowser";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.ControlDarkDark;
            this.SizeChanged += new System.EventHandler(this.XBrowser_SizeChanged);
            this.MainPanel.Panel1.ResumeLayout(false);
            this.MainPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainPanel)).EndInit();
            this.MainPanel.ResumeLayout(false);
            this.BrowserPnl.ResumeLayout(false);
            this.ToolbarBawah.ResumeLayout(false);
            this.XcodePnl.ResumeLayout(false);
            this.FindPnl.ResumeLayout(false);
            this.FindPnl.PerformLayout();
            this.ToolbarAtas.ResumeLayout(false);
            this.ToolbarAtas.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.Log.ResumeLayout(false);
            this.Log.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Xcode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConsoleLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer MainPanel;
        private System.Windows.Forms.Panel BrowserPnl;
        private System.Windows.Forms.Panel ToolbarBawah;
        private System.Windows.Forms.Panel XcodePnl;
        private System.Windows.Forms.Button SaveXcode;
        private System.Windows.Forms.Button OpenXcode;
        private System.Windows.Forms.Button AddXcode;
        private System.Windows.Forms.Button RunXcode;
        private System.Windows.Forms.Panel FindPnl;
        private System.Windows.Forms.Button CloseFind;
        private System.Windows.Forms.Button FindNext;
        private System.Windows.Forms.Button FindPrev;
        private System.Windows.Forms.Button FindBtn;
        private System.Windows.Forms.TextBox Find;
        private System.Windows.Forms.Panel ToolbarAtas;
        private System.Windows.Forms.Button XMenu;
        private System.Windows.Forms.TextBox XDomain;
        private System.Windows.Forms.Button Download;
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Button XRefresh;
        private System.Windows.Forms.Button Xgo;
        private System.Windows.Forms.Button XcodeToggle;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private FastColoredTextBoxNS.FastColoredTextBox Xcode;
        private System.Windows.Forms.TabPage Log;
        private FastColoredTextBoxNS.FastColoredTextBox ConsoleLog;
    }
}