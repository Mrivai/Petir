using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Petir
{
    public partial class MainForm : Form
    {
        
        private string UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.110 Safari/537.36";
        private string Branding = "Petir";
        private string Homepage = "https://www.google.com";
        //private string Homepage = "xcode.com/register";
        private string DownloadsURL = "sharpbrowser://storage/downloads.html";
        public string FileNotFoundURL = "sharpbrowser://storage/errors/notFound.html";


        public string CannotConnectURL = "sharpbrowser://storage/errors/cannotConnect.html";
        public string SearchURL = "https://www.google.com/#q=";
        public string NewTabURL = "about:blank";

        public Dictionary<int, DownloadItem> downloads;
        public Dictionary<int, string> downloadNames;
        public List<int> downloadCancelRequests;


        internal HostHandler HHandler;
        internal DownloadHandler DHandler;
        internal ContextMenuHandler MHandler;
        internal LifeSpanHandler LHandler;

        internal void Appender(string x)
        {
            X.Appender(x);
        }

        internal KeyboardHandler KHandler;
        internal RequestHandler RHandler;
        internal Scriptmonkey monyet;
        internal ChromiumWebBrowser Browser;

        internal XBrowser X
        {
            get
            {
                if (XDock.ActiveDocument != null)
                {
                    return XDock.ActiveDocument.DockHandler.Form as XBrowser;
                }
                return null;
            }
        }
        
        public MainForm()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            InitCef();
            InitHotkeys();
            Cef.EnableHighDPISupport();
            AddNewBrowser(Homepage);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //AddNewBrowser(Homepage);
        }
        
        private void InitCef()
        {
            CefSettings settings = new CefSettings();
            settings.RegisterScheme(new CefCustomScheme
            {
                SchemeName = "sharpbrowser",
                SchemeHandlerFactory = new SchemeHandlerFactory()
            });
            settings.UserAgent = UserAgent;
            settings.IgnoreCertificateErrors = true;
            settings.ResourcesDirPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            settings.CachePath = GetAppDir("Cache");
            Cef.Initialize(settings);

            HHandler = new HostHandler(this);
            DHandler = new DownloadHandler(this);
            LHandler = new LifeSpanHandler(this);
            MHandler = new ContextMenuHandler(this);
            KHandler = new KeyboardHandler(this);
            RHandler = new RequestHandler(this);

            InitDownloads();
        }

        internal void Execute(string v)
        {
            X.xecute(v);
        }

        private string GetAppDir(string name)
        {
            string winXPDir = @"C:\Documents and Settings\All Users\Application Data\";
            if (Directory.Exists(winXPDir))
            {
                return winXPDir + Branding + @"\" + name + @"\";
            }
            return @"C:\ProgramData\" + Branding + @"\" + name + @"\";
        }

        #region Hotkey
        private void InitHotkeys()
        {
            // browser hotkeys
            //KeyboardHandler.AddHotKey(this, CloseActiveTab, Keys.W, true);
            //KeyboardHandler.AddHotKey(this, CloseActiveTab, Keys.Escape, true);
            KeyboardHandler.AddHotKey(this, AddBlankTab, Keys.T, true);
            KeyboardHandler.AddHotKey(this, RefreshActiveTab, Keys.F5);
            KeyboardHandler.AddHotKey(this, OpenDeveloperTools, Keys.F12);
            KeyboardHandler.AddHotKey(this, NextUrl, Keys.Right, true);
            KeyboardHandler.AddHotKey(this, PrevUrl, Keys.Left, true);
            KeyboardHandler.AddHotKey(this, Print, Keys.P, true);

            // search hotkeys
            KeyboardHandler.AddHotKey(this, OpenSearch, Keys.F, true);
            KeyboardHandler.AddHotKey(this, CloseSearch, Keys.Escape);
            KeyboardHandler.AddHotKey(this, CloseActiveTab, Keys.Escape);
        }

        private void Print()
        {
            X.Browser.Print();
        }

        private void CloseActiveTab()
        {
            X.Browser.Dispose();
            X.Dispose();
        }

        public void RefreshActiveTab()
        {
            X.Browser.Reload();
        }

        private void OpenDeveloperTools()
        {
            X.Browser.ShowDevTools();
        }

        private void NextUrl()
        {
            X.Browser.Forward();
        }

        private void PrevUrl()
        {
            X.Browser.Back();
        }
        
        private void CloseSearch()
        {
            X.OpenCloseFind();
        }

        private void OpenSearch()
        {
            X.OpenCloseFind();
        }
        public void AddBlankTab()
        {
            AddNewBrowser(NewTabURL);
        }
        #endregion

        //private void AddBlankTab() { AddNewBrowser(Homepage); }

        public ChromiumWebBrowser AddNewBrowserTab(string url)
        {
            return (ChromiumWebBrowser)this.Invoke((Func<ChromiumWebBrowser>)delegate
            {
                XBrowser xyz = new XBrowser(this, url);
                return xyz.Browser;
            });
        }

        public void AddNewBrowser(string url)
        {
            XBrowser X = new XBrowser(this, url);
            if (XDock.DocumentStyle == DocumentStyle.SystemMdi)
            {
                X.MdiParent = this;
                X.Show();
            }
            else
                X.Show(XDock);
        }

        internal void Tes()
        {
            if (XDock.ActiveDocument.DockHandler.IsActivated)
            {
                var x = XDock.ActiveDocument.DockHandler.Form as XBrowser;
                Appender(x.Text + x.IsActivated + Environment.NewLine);
            }
            if (XDock.ActiveDocumentPane.IsActivated)
            {
                var x = XDock.ActiveDocumentPane.ActiveContent.DockHandler.Form as XBrowser;
                Appender(x.Text + x.IsActivated + Environment.NewLine);
            }
            if (XDock.ActivePane.IsActivated)
            {
                var x = XDock.ActivePane.ActiveContent.DockHandler.Form as XBrowser;
                Appender(x.Text + x.IsActivated + Environment.NewLine);
            }
            if (XDock.ActiveContent.DockHandler.IsActivated)
            {
                var x = XDock.ActiveContent.DockHandler.Form as XBrowser;
                Appender(x.Text + x.IsActivated + Environment.NewLine);
            }
        }


        #region Download
        /// <summary>
        /// we must store download metadata in a list, since CefSharp does not
        /// </summary>
        private void InitDownloads()
        {
            downloads = new Dictionary<int, DownloadItem>();
            downloadNames = new Dictionary<int, string>();
            downloadCancelRequests = new List<int>();
        }

        public Dictionary<int, DownloadItem> Downloads
        {
            get
            {
                return downloads;
            }
        }

        public void UpdateDownloadItem(DownloadItem item)
        {
            lock (downloads)
            {
                // SuggestedFileName comes full only in the first attempt so keep it somewhere
                if (item.SuggestedFileName != "")
                {
                    downloadNames[item.Id] = item.SuggestedFileName;
                }

                // Set it back if it is empty
                if (item.SuggestedFileName == "" && downloadNames.ContainsKey(item.Id))
                {
                    item.SuggestedFileName = downloadNames[item.Id];
                }

                downloads[item.Id] = item;

                //UpdateSnipProgress();
            }
        }

        public string DownloadedPath(string url)
        {
            foreach(DownloadItem item in downloads.Values)
            {
                if (item.SuggestedFileName.Contains(url))
                {
                    return item.FullPath;
                }
            }
            return null;
        }

        public bool DownloadsInProgress()
        {
            foreach (DownloadItem item in downloads.Values)
            {
                if (item.IsInProgress)
                {
                    return true;
                }
            }
            return false;
        }

        public List<int> CancelRequests
        {
            get
            {
                return downloadCancelRequests;
            }
        }
        
        public void OpenDownloadsTab()
        {
            AddNewBrowserTab(DownloadsURL);
        }


        #endregion
        
    }
}
