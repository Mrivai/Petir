using CefSharp;
using CefSharp.WinForms;
using System;
using System.IO;
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
        private string SearchURL = "https://www.google.com/#q=";
        private string lastSearch = "";
        internal ChromiumWebBrowser Browser;
        internal  MainForm Xform;
        public bool Refres;
        public bool Stop;

        internal AutomationHandler Autobot;
        public static XBrowser Instance;
        public string Ua;

        public XBrowser(MainForm main, string u)
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            Xform = main;
            //InitChrome(u);
            XDomain.Text = u;
            Browser = new ChromiumWebBrowser(u);
            Browser.Dock = DockStyle.Fill;
            Browser.BackgroundImage = global::Petir.Properties.Resources.Bing_background_1920x1200_2013_10_01;
            BrowserPnl.Controls.Add(Browser);
            Browser.BringToFront();
            ConfigureBrowser(Browser);
            Browser.StatusMessage += Browser_StatusMessage;
            Browser.LoadingStateChanged += Browser_LoadingStateChanged;
            //inject javascript when finish loading
            Browser.TitleChanged += Browser_TitleChanged;
            Browser.LoadError += Browser_LoadError;
            Browser.AddressChanged += Browser_URLChanged;
            Browser.DownloadHandler = Xform.DHandler;
            Browser.MenuHandler = Xform.MHandler;
            Browser.LifeSpanHandler = Xform.LHandler;
            Browser.KeyboardHandler = Xform.KHandler;
            Browser.RequestHandler = Xform.RHandler;

            Autobot = new AutomationHandler(this);

            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            Browser.RegisterAsyncJsObject("autobot", Autobot, null);

            if (u.StartsWith("sharpbrowser:"))
            {
                Browser.RegisterAsyncJsObject("host", Xform.HHandler, null);
            }
            else
            {
                Browser.FrameLoadEnd += (sender, args) =>
                {
                    if (args.Frame.IsMain)
                    {
                        args.Frame.EvaluateScriptAsync(File.ReadAllText(Js.inject));
                    }
                };
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
                XDomain.SelectAll();
            }
        }

        private void XDomain_KeyDown(object sender, KeyEventArgs e)
        {
            // if ENTER or CTRL+ENTER pressed
            if (e.IsHotkey(Keys.Enter) || e.IsHotkey(Keys.Enter, true))
            {
                LoadURL(XDomain.Text);
                // im handling this
                e.Handled = true;
                e.SuppressKeyPress = true;
                // defocus from url textbox
                //this.Focus();
            }
            // if full URL copied
            if (e.IsHotkey(Keys.C, true) && Utils.IsFullySelected(XDomain))
            {
                // copy the real URL, not the pretty one
                Clipboard.SetText(Browser.Address, TextDataFormat.UnicodeText);
                // im handling this
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
            if (Refres)
            {
                Browser.Reload();
            }
            else if (Stop)
            {
                Browser.Stop();
            }
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            Browser.Stop();
        }
        public void ComeBack()
        {
            Browser.Reload();
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

        }

        private void EnableBackButton(bool canGoBack)
        {
            InvokeIfNeeded(() => Back.Enabled = canGoBack);
        }

        private void EnableForwardButton(bool canGoForward)
        {
            InvokeIfNeeded(() => Next.Enabled = canGoForward);
        }
        

        //region Browser
        #region Browser

        private void ConfigureBrowser(ChromiumWebBrowser browser)
        {
            BrowserSettings config = new BrowserSettings
            {
                FileAccessFromFileUrls = (!CrossDomainSecurity).ToCefState(),
                UniversalAccessFromFileUrls = (!CrossDomainSecurity).ToCefState(),
                WebSecurity = WebSecurity.ToCefState(),
                BackgroundColor = 0000,
                WebGl = WebGL.ToCefState()
                
            };
            browser.BrowserSettings = config;
        }

        private void Browser_LoadError(object sender, LoadErrorEventArgs e)
        {
            //throw new NotImplementedException();
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
                    Text = "loading";
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
                        Enabldisable(false, true);
                    }

                });
            }
        }

        public void Enabldisable(bool refres, bool stop)
        {
            //global::Petir.Properties.Resources.Cancel;
            if (refres || !stop)
            {
                Refres = true;
                Stop = false;
                XRefresh.BackgroundImage = global::Petir.Properties.Resources.Reload;
            }
            else if (!refres || stop)
            {
                Refres = false;
                Stop = true;
                XRefresh.BackgroundImage = global::Petir.Properties.Resources.Cancel;
            }
        }

        private void Browser_StatusMessage(object sender, StatusMessageEventArgs e)
        {
            //throw new NotImplementedException();
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
                    Enabldisable(false, true);
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
            if (IsBlank(text))
            {
                text = "New Tab";
            }
            Browser.Tag = text;
            
            this.InvokeOnParent(delegate ()
            {
                Text = text;
            });
        }

        private bool IsBlank(string url)
        {
            return (url == "" || url == "about:blank");
        }

        private void SetFormURL(string url)
        {
            var Cleanurl = RemovePrefix(url);
            XDomain.Text = Cleanurl;
        }

        private string RemovePrefix(string url)
        {
            if (url.BeginsWith("about:"))
            {
                return "";
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

                if (!(urlLower.StartsWith("http") || urlLower.StartsWith("sharpbrowser")))
                {
                    if (outUri == null || outUri.Scheme != Uri.UriSchemeFile) newUrl = "http://" + url;
                }

                if (urlLower.StartsWith("sharpbrowser:") || (Uri.TryCreate(newUrl, UriKind.Absolute, out outUri) && ((outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps) && newUrl.Contains(".") || outUri.Scheme == Uri.UriSchemeFile)))
                {
                }
                else
                {
                    newUrl = SearchURL + HttpUtility.UrlEncode(url);
                }
            }
            Browser.Load(newUrl);
            SetFormURL(newUrl);
            EnableBackButton(true);
            EnableForwardButton(false);
        }
        #endregion

        //region Xbrowser Control
        #region Bottom ToolBar

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
                    Showtoolbar(false);
                    FindPnl.Visible = false;
                }

            });

        }
        private void CloseFind_Click(object sender, EventArgs e)
        {
            OpenCloseFind();
        }

        #endregion

        //region Xcode
        #region Xcode
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

        public void Appender(string code)
        {
            InvokeIfNeeded(() =>
            {
                Xcode.AppendText(code);
            });
        }

        public string hasSelectedElement()
        {
            var result = Browser.EvaluateScriptAsync("document.activeElement.localName").ToString();
            return result;
        }
        public void AddNewBrowserTab(string u)
        {
            Xform.AddNewBrowserTab(u);
        }
        public void xecute(string x)
        {
            Browser.EvaluateScriptAsync(x);
        }

        private void XBrowser_SizeChanged(object sender, EventArgs e)
        {
            if(Width <= 400 )
            {
                Ua = "Mozilla/5.0 (Linux; Android; en-us;) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.110 Mobile Safari/537.36";
            }
            else if(Width >= 400)
            {
                if(Ua != null)
                {
                    Ua = null;
                }
            }
        }

        #region hotkey
        private void Print()
        {
            Browser.Print();
        }

        private void CloseActiveTab()
        {
            Browser.Dispose();
            Dispose();
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
            Xform.AddNewBrowser( "https://www.google.com/");
        }
        #endregion
        
    }
}
//#endregion
