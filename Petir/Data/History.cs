using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Petir
{

    internal class History
    {
        private static string AppsPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        private static string HistoryFile = AppsPath + "storage" + Path.DirectorySeparatorChar + "history.json";

        public List<HistoryItem> ListHistory = new List<HistoryItem>();

        public History()
        {
            if (!File.Exists(HistoryFile))
            {
                HistoryItem h = new HistoryItem();
                h.Title = "Google";
                h.Url = "https:\\www.google.com";
                h.Tanggal = DateTime.UtcNow;
                ListHistory.Add(h);
                var history = JsonConvert.SerializeObject(ListHistory);
                Utils.WriteFile(HistoryFile, history);
            }
            else
            {
                Load();
            }
        }

        public void Add(string title, string url)
        {
            HistoryItem h = new HistoryItem();
            h.Title = title;
            h.Url = url;
            h.Tanggal = DateTime.UtcNow;
            ListHistory.Add(h);
            Save();
        }

        public void Delete(string url, DateTime dt)
        {
            foreach (HistoryItem item in ListHistory)
            {
                if (item.Url.Contains(url) && item.Tanggal.Equals(dt))
                {
                    ListHistory.Remove(item);
                    Save();
                }
            }
        }

        public void Save()
        {
            var history = JsonConvert.SerializeObject(ListHistory);
            Utils.WriteFile(HistoryFile, history);
        }

        private void Load()
        {
            try
            {
                var data = Utils.ReadFile(HistoryFile);
                ListHistory = JsonConvert.DeserializeObject<List<HistoryItem>>(data);
            }
            catch (Exception ex)
            {
                Logger.w("Cannot Load History", ex);
            }
        }
    }

    public class HistoryItem
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public DateTime Tanggal { get; set; }
    }
}
