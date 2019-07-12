using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Petir
{
    internal class Bookmark
    {
        private static string AppsPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        private static string FileBookmark = AppsPath + @"storage\bookmark.json";

        public List<BookmarkItem> ListBookmark = new List<BookmarkItem>();

        public Bookmark()
        {
            if (!File.Exists(FileBookmark))
            {
                BookmarkItem h = new BookmarkItem();
                h.Title = "Google";
                h.Url = "https:\\www.google.com";
                ListBookmark.Add(h);
                var book = JsonConvert.SerializeObject(ListBookmark);
                Utils.WriteFileAsync(FileBookmark, book);
                Load();
            }
            else
            {
                Load();
            }
        }

        public void Add(string title, string url)
        {
            BookmarkItem h = new BookmarkItem();
            h.Title = title;
            h.Url = url;
            ListBookmark.Add(h);
            Save();
        }

        public void Delete(string url, string title)
        {
            foreach (BookmarkItem item in ListBookmark)
            {
                if (item.Url.Contains(url) && item.Title.Contains(title))
                {
                    ListBookmark.Remove(item);
                    Save();
                }
            }
        }

        public void Save()
        {
            var book = JsonConvert.SerializeObject(ListBookmark);
            Utils.WriteFile(FileBookmark, book);
        }

        private void Load()
        {
            try
            {
                var data = Utils.ReadFile(FileBookmark);
                ListBookmark = JsonConvert.DeserializeObject<List<BookmarkItem>>(data);
            }
            catch (Exception ex)
            {
                Logger.w("Cannot Load History", ex);
            }
        }
    }

    public class BookmarkItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
