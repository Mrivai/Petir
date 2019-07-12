namespace Petir
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.XDock = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2005Theme1 = new WeifenLuo.WinFormsUI.Docking.VS2005Theme();
            this.SuspendLayout();
            // 
            // XDock
            // 
            this.XDock.AllowDrop = true;
            this.XDock.AutoSize = true;
            this.XDock.BackColor = System.Drawing.Color.White;
            this.XDock.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("XDock.BackgroundImage")));
            this.XDock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.XDock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.XDock.DockBackColor = System.Drawing.Color.Transparent;
            this.XDock.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.XDock.Location = new System.Drawing.Point(0, 0);
            this.XDock.Name = "XDock";
            this.XDock.ShowAutoHideContentOnHover = false;
            this.XDock.Size = new System.Drawing.Size(774, 459);
            this.XDock.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(774, 459);
            this.Controls.Add(this.XDock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(1400, 748);
            this.MinimumSize = new System.Drawing.Size(790, 498);
            this.Name = "MainForm";
            this.Text = "PelitaBangsa";
            this.TransparencyKey = System.Drawing.Color.White;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WeifenLuo.WinFormsUI.Docking.DockPanel XDock;
        private WeifenLuo.WinFormsUI.Docking.VS2005Theme vS2005Theme1;
    }
}