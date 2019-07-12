using CefSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Petir
{
    internal class DownloadManager
    {
        private static string AppsPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        private static string downloadlog = AppsPath + @"storage\download.json";

        public Dictionary<int, DownloadItem> downloads;
        public Dictionary<int, string> downloadNames;
        public List<int> downloadCancelRequests;
        public List<int> downloadPauseRequests;
        public List<int> downloadResumeRequests;


        public DownloadManager()
        {
            downloads = new Dictionary<int, DownloadItem>();
            downloadNames = new Dictionary<int, string>();
            downloadCancelRequests = new List<int>();
            downloadPauseRequests = new List<int>();
            downloadResumeRequests = new List<int>();
            Load();
        }

        public void Add(DownloadItem item)
        {
            lock (downloads)
            {
                if (item.SuggestedFileName != "")
                {
                    downloadNames[item.Id] = item.SuggestedFileName;
                }
                if (item.SuggestedFileName == "" && downloadNames.ContainsKey(item.Id))
                {
                    item.SuggestedFileName = downloadNames[item.Id];
                }
                downloads[item.Id] = item;

                //UpdateSnipProgress();
            }
        }

        public void Delete(int key)
        {
            foreach (DownloadItem item in downloads.Values)
            {
                if (item.Id.Equals(key))
                {
                    downloads.Remove(key);
                }
            }
        }

        public void Save()
        {
            var dl = JsonConvert.SerializeObject(downloads);
            Utils.WriteFile(downloadlog, dl);
        }

        private void Load()
        {
            try
            {
                if (!File.Exists(downloadlog))
                {
                    var dl = JsonConvert.SerializeObject(downloads);
                    Utils.WriteFileAsync(downloadlog, dl);
                }
                var data = Utils.ReadFile(downloadlog);
                downloads = JsonConvert.DeserializeObject<Dictionary<int, DownloadItem>>(data);
            }
            catch (Exception ex)
            {
                Logger.w("Cannot Load Download History", ex);
            }
        }

        public Dictionary<int, DownloadItem> Downloaded
        {
            get
            {
                return downloads;
            }
        }

        public string CalcDownloadPath(DownloadItem item)
        {

            string itemName = item.SuggestedFileName != null ? item.SuggestedFileName.GetAfterLast(".") + " file" : "downloads";

            string path = null;
            if (path != null)
            {
                return path;
            }

            return null;
        }

        public string DownloadedPath(string url)
        {
            foreach (DownloadItem item in downloads.Values)
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
        public List<int> PauseRequests
        {
            get
            {
                return downloadCancelRequests;
            }
        }
        public List<int> ResumeRequests
        {
            get
            {
                return downloadCancelRequests;
            }
        }

    }
}
