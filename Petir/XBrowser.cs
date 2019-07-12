using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Petir
{
    public partial class XBrowser : DockContent
    {
        public bool WebSecurity = true;
        public bool CrossDomainSecurity = true;
        public bool WebGL = true;
        public bool aktif = false;
        private ScreenShot Screenshot;
        private string SearchURL = "https://www.google.co.id/search?client=google&q=";
        private string lastSearch = "";
        internal ChromiumWebBrowser Browser;
        internal MainForm Xform;

        internal AutomationHandler Autobot;
        //internal ScrappingHandler scraper;
        public static XBrowser Instance;
        public string Ua;
        
        public string downloadpath { get; private set; }
        private bool Refres { get; set; }
        private string Source { get; set; }
        public string Address { get { return Browser.GetMainFrame().Url; } }//Browser.Text
        
        public XBrowser(MainForm main, string u)
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            Xform = main;
            XDomain.Text = u;
            Browser = new ChromiumWebBrowser(u)
            {
                Dock = DockStyle.Fill,
                BackgroundImage = Properties.Resources.Background
            };
            BrowserPnl.Controls.Add(Browser);
            Browser.BringToFront();
            ConfigureBrowser(Browser);
            //Browser.StatusMessage += Browser_StatusMessage;
            Browser.ConsoleMessage += Browser_ConsoleMessage;
            Browser.LoadingStateChanged += Browser_LoadingStateChanged;
            //inject javascript when finish loading
            Browser.TitleChanged += Browser_TitleChanged;
            Browser.AddressChanged += Browser_URLChanged;
            Browser.DownloadHandler = Xform.DHandler;
            Browser.MenuHandler = Xform.MHandler;
            Browser.LifeSpanHandler = Xform.LHandler;
            Browser.KeyboardHandler = Xform.KHandler;
            Browser.RequestHandler = Xform.RHandler;
            Autobot = new AutomationHandler(this);
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            Browser.RegisterAsyncJsObject("autobot", Autobot);
            Screenshot = new ScreenShot(this);
            Browser.ResourceHandlerFactory = new VideoResourceHandlerFactory();
            /*Browser.JavascriptObjectRepository.ResolveObject += (sender, e) =>
            {
                var repo = e.ObjectRepository;
                if (e.ObjectName == "autobot")
                {
                    BindingOptions bindingOptions = null;
                    bindingOptions = BindingOptions.DefaultBinder;
                    repo.Register("autobot", Autobot, isAsync: true, options: bindingOptions);
                }
            };
            */
            Browser.FrameLoadEnd += (sender, args) =>
            {
                if (args.Frame.IsMain)
                {
                    if (u.StartsWith("pelitabangsa:") == false && u.StartsWith("chrome:") == false)
                    {
                        args.Frame.EvaluateScriptAsync(Properties.Resources.inject);
                        RunScriptMonkey(Browser.GetMainFrame().Url);
                    }
                }
                //AddHistory(Browser.Tag.ToString(), Browser.GetMainFrame().Url);
            };
        }
        
        #region Browser

        private void ConfigureBrowser(ChromiumWebBrowser browser)
        {
            BrowserSettings config = new BrowserSettings
            {
                FileAccessFromFileUrls = (!CrossDomainSecurity).ToCefState(),
                UniversalAccessFromFileUrls = (!CrossDomainSecurity).ToCefState(),
                WebSecurity = WebSecurity.ToCefState(),
                BackgroundColor = 0000,
                DefaultEncoding = "UTF-8",
                WebGl = WebGL.ToCefState()
            };
            browser.BrowserSettings = config;
        }

        private void Browser_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            SetTabTitle((ChromiumWebBrowser)sender, e.Title);
        }

        private void Browser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            EnableBackButton(e.CanGoBack);
            EnableForwardButton(e.CanGoForward);

            if (e.IsLoading)
            {
                this.InvokeOnParent(delegate ()
                {
                    Text = "Loading...";
                    Enabldisable(e.CanReload);
                });
                //Text = "Loading...";
            }
            else
            {
                InvokeIfNeeded(() =>
                {
                    // if current tab
                    if (sender == Browser)
                    {
                        Enabldisable(e.CanReload);
                    }
                });
            }
        }
        private void Browser_ConsoleMessage(object sender, ConsoleMessageEventArgs e)
        {
            Appender(string.Format("Line: {0}, Source: {1}, Message: {2}", e.Line, e.Source, e.Message));
        }
        public void Enabldisable(bool refres)
        {
            if (refres == true)
            {
                Refres = true;
                XRefresh.BackgroundImage = Properties.Resources.Reload;
            }
            else
            {
                Refres = false;
                XRefresh.BackgroundImage = Properties.Resources.Cancel;
            }
        }

        private void Browser_URLChanged(object sender, AddressChangedEventArgs e)
        {
            InvokeIfNeeded(() =>
            {
                // if current tab
                if (sender == Browser)
                {
                    if (!Utils.IsFocussed(XDomain))
                    {
                        SetFormURL(e.Address);
                    }
                    EnableBackButton(Browser.CanGoBack);
                    EnableForwardButton(Browser.CanGoForward);
                    SetFormURL(e.Address);
                    SetTabTitle((ChromiumWebBrowser)sender, "Loading...");
                    //Refresh.Visible = false;
                    Enabldisable(false);
                }

            });
        }

        public void InvokeIfNeeded(Action action)
        {
            if (InvokeRequired)
            {
                BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }

        private void SetTabTitle(ChromiumWebBrowser sender, string text)
        {
            text = text.Trim();
            if ((text == "") || (text == "about:blank"))
            {
                text = "New Tab";
            }
            Browser.Tag = text;
            this.InvokeOnParent(delegate ()
            {
                Text = text;
            });
        }

        private void SetFormURL(string url)
        {
            Browser.Text = url;
            XDomain.Text = RemovePrefix(url);
        }

        private string RemovePrefix(string url)
        {
            if (url.BeginsWith("about:"))
            {
                return "";
            }
            if (url.Contains(Xform.GoogleWeblightUrl))
            {
                url.RemovePrefix(Xform.GoogleWeblightUrl);
            }
            url = url.RemovePrefix("http://");
            url = url.RemovePrefix("https://");
            url = url.RemovePrefix("file://");
            url = url.RemovePrefix("/"); 
            return url.DecodeURL();
        }

        public void LoadURL(string url)
        {
            string newUrl = url;
            string urlLower = url.Trim().ToLower();
            
            // load page
            if (url.Contains("localhost"))
            {
                if (url.Contains("http://"))
                {
                    newUrl = url;
                }
                else
                {
                    newUrl = "http://" + url;
                }
            }
            else
            {
                Uri.TryCreate(url, UriKind.Absolute, out Uri outUri);

                if (!(urlLower.StartsWith("http") || urlLower.StartsWith("pelitabangsa")))
                {
                    if (outUri == null || outUri.Scheme != Uri.UriSchemeFile) newUrl = "http://" + url;
                }
                if (urlLower.StartsWith("pelitabangsa:") || urlLower.StartsWith("chrome:") || (Uri.TryCreate(newUrl, UriKind.Absolute, out outUri) && ((outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps) && newUrl.Contains(".") || outUri.Scheme == Uri.UriSchemeFile)))
                {

                }
                else
                {
                    newUrl = SearchURL + HttpUtility.UrlEncode(url);
                }
            }
            Browser.Load(newUrl);
            SetFormURL(newUrl);
            EnableBackButton(Browser.CanGoBack);
            EnableForwardButton(Browser.CanGoForward);
        }

        public void CloseTab()
        {
            Browser.Dispose();
            Dispose();
        }

        public void Downloadinbackground(string url, string path)
        {
            downloadpath = path;
            Browser.GetBrowser().GetHost().StartDownload(url);
            downloadpath = null;
        }

        public string HasSelectedElement()
        {
            var result = Browser.EvaluateScriptAsync("document.activeElement.localName").ToString();
            return result;
        }

        public void Xecute(string x)
        {
            Browser.EvaluateScriptAsync(x);
        }

        public object EvalJs(string x)
        {
            object result = null;
            var task = Browser.GetMainFrame().EvaluateScriptAsync(x); //Browser.EvaluateScriptAsync(x);
            var complete = task.ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    var response = t.Result;
                    result = response.Success ? (response.Result ?? "null") : response.Message;
                }
            }, TaskScheduler.Default);
            complete.Wait();

            return result;
        }

        public string GetSource()
        {
            if (Browser.GetMainFrame().Url.Contains("pelitabangsa://browser/") == false)
            {
                Browser.GetSourceAsync().ContinueWith(v =>
                {
                    Source = v.Result;//.EncodedHtml();
                });
            }
            return Source;
        }

        public void TakeScreenshot()
        {
            Enabled = false;
            Screenshot.GetScreenshot();
            Enabled = true;
        }

        public void TakeFullScreenshot()
        {
            Enabled = false;
            Screenshot.GetFullScreenshotAsync();
            Enabled = true;
        }
        

        #endregion

        #region BrowserForm

        private void XBrowser_SizeChanged(object sender, EventArgs e)
        {
            if (Width <= 400)
            {
                Ua = "Mozilla/5.0 (Linux; Android; en-us;) AppleWebKit/537.36 (KHTML, like Gecko) Chrome / 74.0.3729.131 Mobile Safari/537.36";
            }
            else
            {
                Ua = null;
            }
        }

        private void XcodeToggle_Click(object sender, EventArgs e)
        {
            if (MainPanel.Panel2Collapsed == true)
            {
                Showtoolbar(true);
                XcodePnl.Visible = true;
                MainPanel.Panel2Collapsed = false;
            }
            else if (MainPanel.Panel2Collapsed == false)
            {
                Showtoolbar(false);
                XcodePnl.Visible = false;
                MainPanel.Panel2Collapsed = true;
            }
        }

        private void XDomain_Click(object sender, EventArgs e)
        {
            if (!Utils.HasSelection(XDomain))
            {
                // XDomain.SelectAll();
            }
        }

        private void XDomain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.IsHotkey(Keys.Enter) || e.IsHotkey(Keys.Enter, true))
            {
                LoadURL(XDomain.Text);
                e.Handled = true;
                e.SuppressKeyPress = true;
                Focus();
            }
            if (e.IsHotkey(Keys.C, true) && Utils.IsFullySelected(XDomain))
            {
                Clipboard.SetText(Browser.Address, TextDataFormat.UnicodeText);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void Xgo_Click(object sender, EventArgs e)
        {
            LoadURL(XDomain.Text);
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            if (Refres == true)
            {
                Browser.Reload();
            }
            else
            {
                Browser.Stop();
            }
        }
        
        private void Back_Click(object sender, EventArgs e)
        {
            Browser.Back();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            Browser.Forward();
        }

        private void Download_Click(object sender, EventArgs e)
        {
            Xform.OpenDownloadsTab();
        }

        private void Menu_Click(object sender, EventArgs e)
        {
            if (Width <= 115)
            {
                XMenu.Enabled = false;
            }
            else
            {
                MainPanel.Width = MainPanel.Width - 115;
            }
        }

        private void EnableBackButton(bool canGoBack)
        {
            InvokeIfNeeded(() => Back.Enabled = canGoBack);
        }

        private void EnableForwardButton(bool canGoForward)
        {
            InvokeIfNeeded(() => Next.Enabled = canGoForward);
        }
        
        private void FindBtn_Click(object sender, EventArgs e)
        {
            FindTextOnPage(true);
        }

        private void Find_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.IsHotkey(Keys.Enter))
            {
                FindTextOnPage(true);
            }
            if (e.IsHotkey(Keys.Enter, true) || e.IsHotkey(Keys.Enter, false, true))
            {
                FindTextOnPage(false);
            }
        }

        private void FindTextOnPage(bool v = true)
        {
            bool first = lastSearch != Find.Text;
            lastSearch = Find.Text;
            if (lastSearch.CheckIfValid())
            {
                Browser.GetBrowser().Find(0, lastSearch, true, false, !first);
            }
            else
            {
                Browser.GetBrowser().StopFinding(true);
            }
            Find.Focus();
        }

        private void Find_TextChanged(object sender, EventArgs e)
        {
            FindTextOnPage(true);
        }

        private void FindPrev_Click(object sender, EventArgs e)
        {
            FindTextOnPage(false);
        }

        private void FindNext_Click(object sender, EventArgs e)
        {
            FindTextOnPage(true);
        }

        private void Showtoolbar(bool onoff)
        {
            ToolbarBawah.Visible = onoff;
        }

        public void OpenCloseFind()
        {
            InvokeIfNeeded(() =>
            {
                if (!FindPnl.Visible)
                {
                    Showtoolbar(true);
                    FindPnl.Visible = true;
                    Find.Focus();
                    Find.SelectAll();
                }
                else if (FindPnl.Visible)
                {
                    if(XcodePnl.Visible == false) Showtoolbar(false);
                    FindPnl.Visible = false;
                }

            });
        }

        private void CloseFind_Click(object sender, EventArgs e)
        {
            OpenCloseFind();
        }

        #endregion

        #region screenshot

        #endregion

        #region Xcode
        public void Appender(string code)
        {
            InvokeIfNeeded(() =>
            {
                Xcode.AppendText(code);
            });
        }

        private void XcodeRun_Click(object sender, EventArgs e)
        {
            Browser.ExecuteScriptAsync(Xcode.Text);
        }

        private void AddXcode_Click(object sender, EventArgs e)
        {
            //safe last edit
            //clear xcode
        }

        private void OpenXcode_Click(object sender, EventArgs e)
        {

        }

        private void SaveXcode_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region ScriptMonkey
        private void RunScriptMonkey(string url)
        {
            var toRun = new List<int>();
            for (int i = 0; i < Xform.monyet.AllScripts.Count; i++)
            {
                if (Xform.monyet.ShouldRunScript(i, url.ToString()))
                    toRun.Add(i);
            }
            foreach (var i in toRun)
            {
                if (Xform.monyet[i].Type == UserScript.ValueType.Script)
                {
                    var scriptcode = Xform.monyet.GetBanana(i);
                    var content = "function Scriptmonkey_S" + i + "_proto() {";

                }
                // else
                // TryInjectCss(i, window);
            }
        }

        #endregion

        #region hotkey
        private void Print()
        {
            Browser.Print();
        }
        
        public void RefreshActiveTab()
        {
            Browser.Reload();
        }

        private void OpenDeveloperTools()
        {
            Browser.ShowDevTools();
        }

        private void NextUrl()
        {
            Browser.Forward();
        }

        private void PrevUrl()
        {
            Browser.Back();
        }

        private void CloseSearch()
        {
            OpenCloseFind();
        }

        private void OpenSearch()
        {
            OpenCloseFind();
        }
        public void AddBlankTab()
        {
            Xform.AddNewBrowser(SearchURL);
        }
        #endregion
        
        #region history
        public void AddHistory(string title, string url)
        {
            Xform.history.Add(title, url);
        }
        public void DeleteHistory(int id)
        {
            Xform.history.Delete(id);
        }
        #endregion

        #region bookmark
        public void AddBookmark(string title, string url)
        {
            Xform.bookmark.Add(title, url);
        }
        public void DeleteBookmark(string url, string title)
        {
            Xform.bookmark.Delete(title, url);
        }
        #endregion

        #region password
        public void AddPassword(string text, string pwd)
        {
            Xform.password.Add(text, pwd);
        }
        public void DeletePassword(string text, string pwd)
        {
            Xform.password.Delete(text, pwd);
        }
        #endregion

        #region webshell
        public void AddWebshell(string title, string url, string pwd)
        {
            Xform.webshell.Add(title, url, pwd);
        }
        public void DeleteWebshell(string url, string title)
        {
            Xform.webshell.Delete(url, title);
        }



        #endregion
        
    }
}
//#endregion
