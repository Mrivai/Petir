using CefSharp;
using CefSharp.WinForms;
using System;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Petir
{
    public partial class MainForm : Form
    {
        private static string AppsPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        private static string FileBookmark = AppsPath + @"storage\bookmark.json";
        //internal string GoogleTranslateURL = "https://translate.google.com/translate?hl=en&sl=auto&tl=en&prev=search&u=";
        public string UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/73.0.3683.75 Safari/537.36";
        public string GoogleWeblightUserAgent = "Mozilla/5.0 (Linux; Android 4.2.1; en-us; Nexus 5 Build/JOP40D) AppleWebKit/535.19 (KHTML, like Gecko; googleweblight) Chrome/38.0.1025.166 Mobile Safari/535.19";
        private string Branding = "Petir";
        private string Homepage = "https://www.google.com";
        internal string DownloadsURL = "pelitabangsa://browser/downloads.html";
        internal string ViewsourceURL = "pelitabangsa://browser/view.html";
        //internal string HistoryURL = "pelitabangsa://browser/history.html";
        internal string BookmarkURL = "pelitabangsa://browser/bookmark.html";
        internal string HistoryURL = "pelitabangsa://browser/video.html";
        public string FileNotFoundURL = "pelitabangsa://browser/errors/notFound.html";
        public string CannotConnectURL = "pelitabangsa://browser/errors/cannotConnect.html";
        public string SearchURL = "https://www.google.co.id/search?client=google&q=";
		public string SearchGoogleTranslate= "https://translate.google.com/translate?hl=id&sl=en&u=";

        public string GoogleWeblightUrl = "https://googleweblight.com/?lite_url=";
        public string NewTabURL = "https://www.google.com/";
       
        internal DownloadHandler DHandler;
        internal ContextMenuHandler MHandler;
        internal LifeSpanHandler LHandler;
        internal DownloadManager Idm = new DownloadManager();
        internal ViewSource viewer;
        internal History history;
        internal Bookmark bookmark;
        internal Password password;
        internal Webshell webshell;
        public bool fastmode = false;

       

        internal KeyboardHandler KHandler;
        internal RequestHandler RHandler;
        internal Scriptmonkey monyet;

        internal XBrowser X
        {
            get
            {
                return XDock.ActiveDocument.DockHandler.Form as XBrowser;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            Cef.EnableHighDPISupport();
            AutoScaleMode = AutoScaleMode.Dpi;
            InitCef();
            InitHotkeys();
            monyet = new Scriptmonkey();
            AddNewBrowser(Homepage);
            history = new History();
            bookmark = new Bookmark();
            password = new Password();
            webshell = new Webshell();
            viewer = new ViewSource();
        }

        private void InitCef()
        {
            CefSharpSettings.ShutdownOnExit = false;
            CefSettings settings = new CefSettings();

            settings.RegisterScheme(new CefCustomScheme
            {
                SchemeName = SchemeHandlerFactory.SchemeName,
                SchemeHandlerFactory = new SchemeHandlerFactory()
            });
            settings.LogSeverity = LogSeverity.Error;
            settings.CefCommandLineArgs.Add("debug-plugin-loading", "1");
            //settings.CefCommandLineArgs.Add("debug-plugin-loading", "1");
            settings.UserAgent = UserAgent;
            settings.CefCommandLineArgs.Add("proxy-server", "127.0.0.1:9666");
            settings.CefCommandLineArgs.Add("proxy-auto-detect", "1");
            settings.CefCommandLineArgs.Add("winhttp-proxy-resolver", "1");
            //settings.CefCommandLineArgs.Add("no-proxy-server", "1");
            settings.CefCommandLineArgs.Add("ppapi-flash-path", AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"PepperFlash\pepflashplayer.dll");
            settings.CefCommandLineArgs.Add("ppapi-flash-version", "32.0.0.192");
            settings.CefCommandLineArgs.Add("enable-npapi", "1");
            //settings.CefCommandLineArgs.Add("enable-media-stream", "1");
            settings.CefCommandLineArgs.Add("disable-gpu", "1");
            //settings.CefCommandLineArgs.Add("disable-gpu-vsync", "1");
            settings.IgnoreCertificateErrors = true;
            settings.ResourcesDirPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            settings.CachePath = GetAppDir("Cache");
            //.AddWebPluginPath(@"C:\Program Files (x86)\VideoLAN\VLC\npvlc.dll");
            //CefRuntime.AddWebPluginDirectory(@"C:\Program Files (x86)\VideoLAN\VLC");
            Cef.Initialize(settings);
            
            DHandler = new DownloadHandler(this);
            LHandler = new LifeSpanHandler(this);
            MHandler = new ContextMenuHandler(this);
            KHandler = new KeyboardHandler(this);
            RHandler = new RequestHandler(this);
            
        }

        internal void Execute(string v)
        {
            X.Xecute(v);
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
            KeyboardHandler.AddHotKey(this, OpenDownloadsTab, Keys.J, true);
            KeyboardHandler.AddHotKey(this, OpenHistoryTab, Keys.H, true);
            KeyboardHandler.AddHotKey(this, OpenBookmarkTab, Keys.B, true);
            KeyboardHandler.AddHotKey(this, ViewSource, Keys.U, true);
            // search hotkeys
            KeyboardHandler.AddHotKey(this, OpenSearch, Keys.F, true);
            KeyboardHandler.AddHotKey(this, CloseSearch, Keys.Escape);
            KeyboardHandler.AddHotKey(this, CloseActiveTab, Keys.Escape);

            KeyboardHandler.AddHotKey(this, FullScreenshot, Keys.S, true, true);
            KeyboardHandler.AddHotKey(this, Screenshot, Keys.S, true);
        }

        private void FullScreenshot()
        {
            X.TakeFullScreenshot();
        }

        private void Screenshot()
        {
            X.TakeScreenshot();
        }

        private void Print()
        {
            X.Browser.Print();
        }

        public void CloseActiveTab()
        {
            X.Browser.GetBrowserHost().CloseBrowser(true);
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

        public void ViewSource()
        {
            var domain = X.Browser.Address;
            var doc = X.GetSource();
            if (doc != null)
            {
                viewer.Add(domain, doc);
                AddNewBrowser(ViewsourceURL + "?path=" + domain);
            }
        }

        public void OpenDownloadsTab()
        {
            AddNewBrowser(DownloadsURL);
        }

        private void OpenBookmarkTab()
        {
            AddNewBrowser(BookmarkURL);
        }

        private void OpenHistoryTab()
        {
            AddNewBrowser(HistoryURL);
        }

        #endregion

        public ChromiumWebBrowser AddNewBrowserTab(string url)
        {
            return (ChromiumWebBrowser)this.Invoke((Func<ChromiumWebBrowser>)delegate
            {
                XBrowser xyz = new XBrowser(this, url);
                if (XDock.DocumentStyle == DocumentStyle.SystemMdi)
                {
                    xyz.MdiParent = this;
                    xyz.Show();
                }
                else
                    xyz.Show(XDock);
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
        
        #region Download
        /// <summary>
        /// we must store download metadata in a list, since CefSharp does not
        /// </summary>
        public void startDownloads(string url)
        {
            X.Browser.GetBrowser().GetHost().StartDownload(url);
        }
        
        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            history.Save();
            bookmark.Save();
            password.Save();
            webshell.Save();
            viewer.Clear();
            Idm.Save();
            Cef.Shutdown();
        }
    }
}