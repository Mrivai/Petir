using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Petir
{
    internal class Webshell
    {
        private static string AppsPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        private static string Filewebshell = AppsPath + @"storage\webshell.json";

        public List<WebshellItem> ListWebshell = new List<WebshellItem>();

        public Webshell()
        {
            if (!File.Exists(Filewebshell))
            {
                WebshellItem h = new WebshellItem();
                h.Title = "Google";
                h.Url = "https:\\www.google.com";
                h.Pwd = "satria89";
                ListWebshell.Add(h);
                var webshell = JsonConvert.SerializeObject(ListWebshell);
                Utils.WriteFileAsync(Filewebshell, webshell);
            }
            else
            {
                Load();
            }
        }

        public void Add(string title, string url, string pwd)
        {
            WebshellItem h = new WebshellItem();
            h.Title = title;
            h.Url = url;
            h.Pwd = pwd;
            ListWebshell.Add(h);
            Save();
        }

        public void Delete(string url, string title)
        {
            foreach (WebshellItem item in ListWebshell)
            {
                if (item.Url.Contains(url) && item.Title.Contains(title))
                {
                    ListWebshell.Remove(item);
                    Save();
                }
            }
        }

        public void Save()
        {
            var webshell = JsonConvert.SerializeObject(ListWebshell);
            Utils.WriteFileAsync(Filewebshell, webshell);
        }

        private void Load()
        {
            try
            {
                var data = Utils.ReadFile(Filewebshell);
                ListWebshell = JsonConvert.DeserializeObject<List<WebshellItem>>(data);
            }
            catch (Exception ex)
            {
                Logger.w("Cannot Load History", ex);
            }
        }
    }

    public class WebshellItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Pwd { get; set; }
    }
}
