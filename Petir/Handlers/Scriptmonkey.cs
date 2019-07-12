using CefSharp;
using CefSharp.Internals;
using CefSharp.WinForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Petir
{
    class Scriptmonkey
    {

        public static string AppsPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        public static string ScriptPath = AppsPath + "scripts_monkey" + Path.DirectorySeparatorChar;
        public static string ResourcePath = AppsPath + "resources" + Path.DirectorySeparatorChar;
        public string  ScriptFile = AppsPath + "settings.json";
        private Dictionary<string, string> _scriptCache = new Dictionary<string, string>();
        public Script Userscript;


        public Scriptmonkey()
        {
            InitScriptmonkey();
        }
        // cek jika file scriptmonkey sudah ada, jika tidak buat baru
        private void InitScriptmonkey()
        {
            Script s = new Script();
            if (!File.Exists(ScriptFile))
                Utils.WriteFile(ScriptFile, JsonConvert.SerializeObject(s));
            if (!Directory.Exists(ScriptPath))
                Directory.CreateDirectory(ScriptPath);
            if (!Directory.Exists(ResourcePath))
                Directory.CreateDirectory(ResourcePath);
            ReloadDataAsync();
        }

        // load file scriptmonkey secara simultan di latar belakang
        public void ReloadDataAsync()
        {
            Task.Run(() => ReloadData());
        }
        // load file scriptmonkey 
        public void ReloadData()
        {
            try
            {
                var data = Utils.ReadFile(ScriptFile); //read setting.json
                Userscript = JsonConvert.DeserializeObject<Script>(data);
            }
            catch (Exception ex)
            {
                Logger.w("Load data error", ex);
            }
        }

        public void Run(string url)
        {
            var toRun = new List<int>();
            for (int i = 0; i < AllScripts.Count; i++)
            {
                if (ShouldRunScript(i, url.ToString()))
                    toRun.Add(i);
            }
            //if (Userscript.InjectAPI) jika true  eval notification.js 
            foreach (var i in toRun)//foreach angka di list angka
            {
                //jika db[i].tipe == script dan bukan stylesheet
                //if (this[i].Type == UserScript.ValueType.Script)
                    //TryRunScript(i, window, ref useMenuCommands, ref menuContent);
                //else
                    //TryInjectCss(i, window);
            }
        }
        

        public bool ShouldRunScript(int i, string url)
        {
            if (!this[i].Enabled) return false;

            if (this[i].Include.Length > 0 && !Regex.IsMatch(url, WildcardToRegex(this[i].Include)))
                return false;

            if (this[i].Exclude.Length > 0 && Regex.IsMatch(url, WildcardToRegex(this[i].Exclude)))
                return false;

            return true;
        }
        
        public List<UserScript> AllScripts
        {
            get
            {
                return Userscript.ListScript;
            }

            set
            {
                Userscript.ListScript = value;
            }
        }

        public void InstallScript(string url,string code)
        {
            var x = ParseUserScript.Parse(code, false);
            var name = Utils.GetFileNameFromUrl(url);
            Utils.WriteFileAsync(ScriptPath + name, code);
            x.Path = ScriptPath + name;
            if (x.Name == String.Empty)
                x.Name = "Userscript from " + url;
            if (String.IsNullOrWhiteSpace(x.UpdateUrl))
                x.UpdateUrl = url;
            AddScript(x);
        }

        public string GetBanana(int i)
        {
            if (Userscript.CacheScripts && _scriptCache.ContainsKey(this[i].Path))
                return _scriptCache[this[i].Path];
            else
            {
                string scriptcontent = Utils.ReadFile(ScriptPath + this[i].Path);
                var content = "function Scriptmonkey_S" + i + "_proto() {";
                if (Userscript.InjectAPI && this[i].RequiresApi)
                {
                    content += scriptcontent;
                }
                if (Userscript.CacheScripts)
                    _scriptCache.Add(this[i].Path, scriptcontent);
                return content;
            }
        }

        public void AddScript(UserScript S)
        {
            Userscript.ListScript.Add(S);
            Save(true); //simpan ke setting.json secepatnya
        }

        public void Save(bool immediate = false)
        {
            try
            {
                var data = JsonConvert.SerializeObject(Userscript);
                if (immediate)
                    Utils.WriteFile(ScriptFile, data);
                else
                    Utils.WriteFileAsync(ScriptFile, data);
            }
            catch (Exception ex)
            {
                Logger.w("Unable To Save", ex);
            }
        }

        private void CheckScriptUpdate()
        {
            DateTime now = DateTime.UtcNow;
            var updatedScriptData = false;
            var toUpdate = new List<UserScript>();
            var toUpdateTo = new List<ScriptWithContent>();

            foreach (UserScript s in AllScripts)
            {
                if (!Userscript.UpdateDisabledScripts && !s.Enabled)
                    continue;

                if (!String.IsNullOrWhiteSpace(s.UpdateUrl) && s.InstallDate < now - TimeSpan.FromDays(7))
                {
                    try
                    {
                        var content = ScriptDownloader.DownloadDefendencies(s.UpdateUrl); //SendWebRequest(s.UpdateUrl);
                        if (content == String.Empty) // Request failed, try again later
                            continue;
                        var newScript = ParseUserScript.Parse(content, false);

                        if (newScript.Version != s.Version)
                        {
                            var scriptContents = new ScriptWithContent(newScript) { Content = content };
                            toUpdate.Add(s);
                            toUpdateTo.Add(scriptContents);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.w("Script update check failed for: " + s.Name, ex);
                    }
                    s.InstallDate = now;
                    updatedScriptData = true;
                }
            }

            if (updatedScriptData)
            {
                Save();
            }

            if (toUpdate.Count > 0)
                AskUpdateScriptsAsync(toUpdate, toUpdateTo);
        }

        private void AskUpdateScriptsAsync(List<UserScript> toUpdate, List<ScriptWithContent> toUpdateTo)
        {
            Thread t = new Thread(() =>
            {
                //var frm = new AskUpdateScriptFrm();

                //for (var i = 0; i < toUpdate.Count; i++) { frm.listBox1.Items.Add($"{toUpdate[i].Name} - {toUpdate[i].Version} to {toUpdateTo[i].ScriptData.Version}");}

                //if (frm.ShowDialog() != DialogResult.OK) return;

                if (toUpdate.Count == 0) return;

                for (int i = 0; i < toUpdate.Count; i++)
                {
                    // Delete existing backups
                    if (File.Exists(ScriptPath + toUpdate[i].Path + @".backup"))
                        File.Delete(ScriptPath + toUpdate[i].Path + @".backup");

                    // Move current file to backup
                    File.Move(ScriptPath + toUpdate[i].Path, ScriptPath + toUpdate[i].Path + @".backup");

                    try
                    {
                        // Save updated script
                        Utils.WriteFile(ScriptPath + toUpdate[i].Path, toUpdateTo[i].Content);
                    }
                    catch (Exception ex)
                    {
                        Logger.w("Unable to update script: ", ex);
                        // Something went wrong. Restore from backup
                        if (File.Exists(ScriptPath + toUpdate[i].Path))
                            File.Delete(ScriptPath + toUpdate[i].Path);

                        File.Copy(ScriptPath + toUpdate[i].Path + @".backup", ScriptPath + toUpdate[i].Path);
                    }

                    // Remove from cache
                    if (_scriptCache.ContainsKey(toUpdate[i].Path))
                        _scriptCache.Remove(toUpdate[i].Path);

                    // Update stored script with metadata of script updated to
                    for (int j = 0; j < AllScripts.Count; j++)
                    {
                        if (this[j].Path == toUpdate[i].Path)
                        {
                            toUpdateTo[i].ScriptData.Path = toUpdate[i].Path;
                            this[j] = toUpdateTo[i].ScriptData;
                            break;
                        }
                    }
                }
                Save(true);
                ReloadData();
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        public UserScript this[int index]
        {
            get
            {
                return Userscript.ListScript[index];
            }
            set
            {
                Userscript.ListScript[index] = value;
                Save(true);
            }
        }
        
        private static string WildcardToRegex(IReadOnlyList<string> pattern)
        {
            var _out = String.Empty;

            for (int i = 0; i < pattern.Count; i++)
            {
                // Allow regex in include/match (http://wiki.greasespot.net/Include_and_exclude_rules#Regular_Expressions)
                if (pattern[i].Length > 3 && pattern[i].StartsWith("/") && pattern[i].EndsWith("/"))
                    _out += pattern[i].Substring(1, pattern[i].Length - 1); // Remove leading and trailing '/'
                else
                {
                    _out += Regex.Escape(pattern[i]).
                        Replace("\\*", ".*").
                        Replace("\\?", ".");
                }
                if (i < pattern.Count - 1) // If not the last item
                    _out += "|";
            }
            return _out;
        }

    }

    #region logger
    internal static class Logger
    {

        private static string Path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log\\";
        private static string ErrorLogPath = Path + string.Format("{0}@{1}.txt", "Xiaomilib_log", DateTime.Now.ToString("yyyyMd"));
        internal static bool w(string Title, Exception ex)
        {
            var Message = ex.Message;
            var StackTrace = ex.StackTrace;
            var report = string.Format("[{0}]:{1}   {2} {3}", DateTime.Now.ToLongTimeString(), Title, Message, StackTrace);
            try
            {
                if (!(new DirectoryInfo(Path)).Exists)
                {
                    Directory.CreateDirectory(Path);
                }
                if (!File.Exists(ErrorLogPath))
                    File.Create(ErrorLogPath).Close();
                else
                    Utils.WriteFileAsync(ErrorLogPath, report);
                    
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
    #endregion

    #region Downloader Script
    internal static class ScriptDownloader
    {
        
        public static string DownloadDefendencies(string url)
        {
            var settings = new BrowserSettings();
            settings.WindowlessFrameRate = 1;

            using (var b = new ChromiumWebBrowser(url))
            {
                LoadPageAsync(b);
                var source = b.GetSourceAsync();
                return source.ToString();
            }
        }

        public static Task LoadPageAsync(IWebBrowser browser, string address = null)
        {
            //If using .Net 4.6 then use TaskCreationOptions.RunContinuationsAsynchronously
            //and switch to tcs.TrySetResult below - no need for the custom extension method
            var tcs = new TaskCompletionSource<bool>();
            EventHandler<LoadingStateChangedEventArgs> handler = null;
            handler = (sender, args) =>
            {
                //Wait for while page to finish loading not just the first frame
                if (!args.IsLoading)
                {
                    browser.LoadingStateChanged -= handler;
                    //This is required when using a standard TaskCompletionSource
                    //Extension method found in the CefSharp.Internals namespace
                    tcs.TrySetResultAsync(true);
                }
            };

            browser.LoadingStateChanged += handler;

            if (!string.IsNullOrEmpty(address))
            {
                browser.Load(address);
            }
            return tcs.Task;
        }
    }
    #endregion
}
