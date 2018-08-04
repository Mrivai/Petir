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
            this.XDock = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2005Theme1 = new WeifenLuo.WinFormsUI.Docking.VS2005Theme();
            this.SuspendLayout();
            // 
            // XDock
            // 
            this.XDock.AllowDrop = true;
            this.XDock.AutoSize = true;
            this.XDock.BackColor = System.Drawing.Color.White;
            this.XDock.BackgroundImage = global::Petir.Properties.Resources.Bing_background_1920x1200_2013_10_01;
            this.XDock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.XDock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.XDock.DockBackColor = System.Drawing.Color.Transparent;
            this.XDock.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.XDock.Location = new System.Drawing.Point(0, 0);
            this.XDock.Name = "XDock";
            this.XDock.ShowAutoHideContentOnHover = false;
            this.XDock.Size = new System.Drawing.Size(771, 450);
            this.XDock.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(771, 450);
            this.Controls.Add(this.XDock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.IsMdiContainer = true;
            this.MaximumSize = new System.Drawing.Size(1400, 748);
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "PelitaBangsa";
            this.TransparencyKey = System.Drawing.Color.White;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WeifenLuo.WinFormsUI.Docking.DockPanel XDock;
        private WeifenLuo.WinFormsUI.Docking.VS2005Theme vS2005Theme1;
    }
}