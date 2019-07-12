using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Petir
{
    public class AutomationHandler
    {
        XBrowser MyBrowser;
        private string selector = "$(";
        private string atribut = ").attr('value','";
        private string endatribut = "');";
        private char hastag = '"';
        private string upload = "Upload(";
        private string xfill = "Fill(";
        private string xcheck = "Check(";
        private string xwrite = "Write(";
        private string xselect = "Select(";
        private string name = "[name='";
        private string endname = "']";
        private string tagclosing = ");";
        private string spacer = ",";

        internal AutomationHandler(XBrowser form)
        {
            MyBrowser = form;
        }

        #region Autobot
        /// <summary>
        /// Autobot handler
        /// </summary>
        public void addcommand(string xtag, string xtype, string xname, string xvalue)
        {
            if (xtag == "input")
            {
                if (xtype == "text")
                {
                    var x = xfill + hastag + xtag + name + xname + endname + hastag;
                    x += spacer + hastag + xvalue + hastag + tagclosing + Environment.NewLine;
                    MyBrowser.Appender(x);
                }
                else if (xtype == "password")
                {
                    var x = xfill + hastag + xtag + name + xname + endname + hastag;
                    x += spacer + hastag + xvalue + hastag + tagclosing + Environment.NewLine;
                    MyBrowser.Appender(x);
                }
                else if (xtype == "file")
                {
                    var file = @"C:\Users\mrivai89\Pictures\Screenshots\Foto0271.jpg";
                    var x = upload + hastag + file + hastag + tagclosing + Environment.NewLine;
                    MyBrowser.Appender(x);
                }
                else if (xtype == "checkbox" || xtype == "radio")
                {
                    var x = xcheck + hastag + xtag + name + xname + endname + hastag;
                    x += spacer + hastag + xvalue + hastag + tagclosing + Environment.NewLine;
                    MyBrowser.Appender(x);
                }
            }
            else if (xtag == "textarea")
            {
                var x = xwrite + hastag + xtag + name + xname + endname + hastag;
                x += spacer + hastag + xvalue + hastag + tagclosing + Environment.NewLine;
                MyBrowser.Appender(x);
            }
            else if (xtag == "select")
            {
                var x = xselect + hastag + xtag + name + xname + endname + hastag;
                x += spacer + hastag + xvalue + hastag + tagclosing + Environment.NewLine;
                MyBrowser.Appender(x);
            }
        }
        public void setTarget(string target)
        {
            var x = "go(" + target + ")";
            MyBrowser.Appender(x);
        }
        public void alert(string text)
        {
            MyBrowser.Xecute("alert('" + text + "')");
        }
        public void fill(string input, string value)
        {
            var xc = selector + hastag + input + hastag + atribut + value + endatribut;
            execute(xc);
        }
        public void write(string input, string value)
        {
            var xc = selector + hastag + input + hastag + atribut + value + endatribut;
            execute(xc);
        }
        public void select(string input, string value)
        {
            var xc = selector + hastag + input + hastag + atribut + value + endatribut;
            execute(xc);
        }
        public void check(string input, string value)
        {
            var xc = selector + hastag + input + hastag + atribut + value + endatribut;
            execute(xc);
        }
        public void selectFile(string path)
        {
            //var sendKeyTask = Task.Delay(500).ContinueWith((_) => { SendKeys.SendWait(location + "{ENTER}"); }, TaskScheduler.FromCurrentSynchronizationContext());
            //await sendKeyTask;
            SendKeys.SendWait(path + "{ENTER}");
        }
        public void submit()
        {
            var x = "Submit();" + Environment.NewLine;
            MyBrowser.Appender(x);
        }
        public void execute(string code)
        {
            MyBrowser.Xecute(code);
        }
        public void append(string x)
        {
            MyBrowser.Appender(x);
        }
        
        #endregion

        #region ScriptMonkey
        /// <summary>
        /// ScriptMonkey handler
        /// </summary>
        public void deleteScriptValue(string name, int index)
        {
            try
            {
                MyBrowser.Xform.monyet[index].SavedValues.Remove(name);
                MyBrowser.Xform.monyet.Save();
            }
            catch(Exception ex)
            {
                Logger.w("Tamper_monkey_deleteScriptValue ERROR", ex);
                throw;
            }
        }
        public string getScriptValue(string name, string value, int index)
        {
            try
            {
                var o = value;
                if (MyBrowser.Xform.monyet[index].SavedValues?.TryGetValue(name, out o) == true)
                    return o;
            }
            catch (Exception ex)
            {
                Logger.w("Tamper_monkey_deleteScriptValue ERROR", ex);
                throw;
            }
            return value;
        }
        public void setScriptValue(string name, string value, int index)
        {
            //MyBrowser.Xform.monyet
            try
            {
                if (MyBrowser.Xform.monyet[index].SavedValues == null)
                    MyBrowser.Xform.monyet[index].SavedValues = new Dictionary<string, string>();
                if (MyBrowser.Xform.monyet[index].SavedValues.ContainsKey(name))
                    MyBrowser.Xform.monyet[index].SavedValues.Add(name, value);
                else
                    MyBrowser.Xform.monyet[index].SavedValues[name] = value;
                MyBrowser.Xform.monyet.Save();
            }
            catch (Exception ex)
            {
                Logger.w("Tamper_monkey_deleteScriptValue ERROR", ex);
                throw;
            }
        }
        public string getScriptResourceText(string name, int index)
        {
            if (MyBrowser.Xform.monyet[index].Resources.ContainsKey(name))
                return null;
            var filepath = Scriptmonkey.ResourcePath + MyBrowser.Xform.monyet[index].Path + "." + name;
            if (!File.Exists(filepath))
            {
                MyBrowser.Downloadinbackground(MyBrowser.Xform.monyet[index].Resources[name], filepath);
            }

            StreamReader str = new StreamReader(filepath);
            string o = null;
            try
            {
                o = str.ReadToEnd();
            }catch(Exception ex)
            {
                Logger.w("autobot_getScriptResourceText_ERROR", ex);
                throw;
            }
            str.Close();
            return o;
        }
        public string getScriptResourceUrl(string name, int index)
        {
            try
            {
                return MyBrowser.Xform.monyet[index].Resources[name];
            }
            catch (Exception ex)
            {
                Logger.w("Tamper_monkey_deleteScriptValue ERROR", ex);
                return null;
            }
        }
        public void open(string url)
        {
            MyBrowser.LoadURL(url);
        }
        public void setClipboard(string text)
        {
            Clipboard.SetText(text);
        }
        
        #endregion
        
        #region Download
        /// <summary>
        /// javascript download handler
        /// </summary>
        public string getDownloads()
        {
            lock (MyBrowser.Xform.Idm.downloads)
            {
                string x = JSON.Instance.ToJSON(MyBrowser.Xform.Idm.downloads);
                return x;
            }
        }
        public bool opendirDownload(string path)
        {
            if (File.Exists(path))
            {
                System.Diagnostics.Process.Start("explorer.exe", "/select, " + path);
                return true;
            }
            else
            {
                return false;
            }
                
        }
        public bool deleteDownload(int downloadId)
        {
            MyBrowser.Xform.Idm.Delete(downloadId);
            return true;
        }
        public bool stopDownload(int downloadId)
        {
            lock (MyBrowser.Xform.Idm.downloadPauseRequests)
            {
                if (!MyBrowser.Xform.Idm.downloadPauseRequests.Contains(downloadId))
                {
                    MyBrowser.Xform.Idm.downloadPauseRequests.Add(downloadId);
                }
            }
            return true;
        }
        public bool resumeDownload(int downloadId)
        {
            lock (MyBrowser.Xform.Idm.downloadResumeRequests)
            {
                if (!MyBrowser.Xform.Idm.downloadResumeRequests.Contains(downloadId))
                {
                    MyBrowser.Xform.Idm.downloadResumeRequests.Add(downloadId);
                }
            }
            return true;
        }
        public bool cancelDownload(int downloadId)
        {
            lock (MyBrowser.Xform.Idm.downloadCancelRequests)
            {
                if (!MyBrowser.Xform.Idm.downloadCancelRequests.Contains(downloadId))
                {
                    MyBrowser.Xform.Idm.downloadCancelRequests.Add(downloadId);
                }
            }
            return true;
        }
        #endregion

        #region ViewSource
        /// <summary>
        /// Plugins handler
        /// </summary>
        public string getSource(string x)
        {
            return MyBrowser.Xform.viewer.View(x);
        }
        #endregion

        #region History
        /// <summary>
        /// History handler
        /// </summary>
        public string getHistory()
        {
            string x = JSON.Instance.ToJSON(MyBrowser.Xform.history.ListHistory);
            return x;
        }
        public void deleteHistory(int id)
        {
            MyBrowser.DeleteHistory(id);
        }
        public void clearHistory()
        {
            MyBrowser.Xform.history.Clear();
        }
        #endregion

        #region Bookmark
        /// <summary>
        /// Bookmark handler
        /// </summary>
        public string getBookmark()
        {
            string x = JSON.Instance.ToJSON(MyBrowser.Xform.bookmark.ListBookmark);
            return x;
        }
        public void deleteBookmark(string title, string url)
        {
            MyBrowser.Xform.bookmark.Delete(url, title);
        }
        #endregion

        #region Adblock
        /// <summary>
        /// Adblock handler
        /// </summary>
        #endregion

        #region Plugins
        /// <summary>
        /// Plugins handler
        /// </summary>
        #endregion
    }
}