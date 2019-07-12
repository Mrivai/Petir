using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Petir
{
    internal class History
    {
        private static string AppsPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        private static string HistoryFile = AppsPath + @"storage\history.json";

        public List<HistoryItem> ListHistory = new List<HistoryItem>();

        public History()
        {
            if (!File.Exists(HistoryFile))
            {
                HistoryItem h = new HistoryItem();
                h.Title = "Google";
                h.Url = "https:\\www.google.com";
                h.Tanggal = DateTime.UtcNow;
                h.Id = Utils.GenerateNewUCID();
                ListHistory.Add(h);
                var history = JsonConvert.SerializeObject(ListHistory);
                Utils.WriteFileAsync(HistoryFile, history);
                Load();
            }
            else
            {
                Load();
            }
        }

        public void Add(string title, string url)
        {
            foreach (HistoryItem item in ListHistory)
            {
                if (item.Url == url && item.Title == title)
                {
                    ListHistory.Remove(item);
                }
            }
            HistoryItem h = new HistoryItem();
            h.Title = title;
            h.Url = url;
            h.Tanggal = DateTime.UtcNow;
            h.Id = Utils.GenerateNewUCID();
            ListHistory.Add(h);
        }
        public void Delete(int id)
        {
            foreach (HistoryItem item in ListHistory)
            {
                if (item.Id.Equals(id))
                {
                    ListHistory.Remove(item);
                }
            }
        }
        public void Clear()
        {
            ListHistory.Clear();
            HistoryItem h = new HistoryItem();
            h.Title = "Google";
            h.Url = "https:\\www.google.com";
            h.Tanggal = DateTime.UtcNow;
            h.Id = Utils.GenerateNewUCID();
            ListHistory.Add(h);
            Save();
            Load();
        }
        public void Save()
        {
            var history = JsonConvert.SerializeObject(ListHistory);
            Utils.WriteFileAsync(HistoryFile, history);
            ListHistory.Clear();
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
        private bool Find(int id)
        {
            foreach (HistoryItem item in ListHistory)
            {
                if (item.Id == id)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class HistoryItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public int Id { get; set; }
        public DateTime Tanggal { get; set; }
    }
}
