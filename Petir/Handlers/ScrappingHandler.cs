using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.WinForms;

namespace Petir
{
    public class ScrappingHandler
    {
        private MainForm XForm;

        private ChromiumWebBrowser Browser;

        internal ScrappingHandler(MainForm form)
        {
            XForm = form;
            Browser = XForm.X.Browser;
        }
        /// <summary>
        /// javascript download handler
        /// </summary>
        public List<string> Links()
        {
            //Properties.Resources.inject
            List<string> result = new List<string>();
            var task = Browser.EvaluateScriptAsync(Properties.Resources.scraplink);
            var complete = task.ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    var response = t.Result;
                    if (response.Success && response.Result != null)
                    {
                         result = (List<string>)response.Result;
                    }
                }
            }, TaskScheduler.Default);
            complete.Wait();
            return result;
        }

        //method scrap(xpath, type, tag)
        //result
        //tag = type(img,link,text)
        //contoh
        //title= target scrape
    }

}
