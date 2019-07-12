using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Petir
{
    internal class Password
    {
        private static string AppsPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        private static string Filepass = AppsPath + @"storage\pwd.json";

        public List<PasswordItem> ListPassword = new List<PasswordItem>();

        public Password()
        {
            if (!File.Exists(Filepass))
            {
                PasswordItem h = new PasswordItem();
                h.Text = "Google";
                h.Pwd = "satria89";
                ListPassword.Add(h);
                var pass = JsonConvert.SerializeObject(ListPassword);
                Utils.WriteFileAsync(Filepass, pass);
            }
            else
            {
                Load();
            }
        }

        public void Add(string text, string pwd)
        {
            PasswordItem h = new PasswordItem();
            h.Text = text;
            h.Pwd = pwd;
            ListPassword.Add(h);
            Save();
        }

        public void Delete(string text, string pwd)
        {
            foreach (PasswordItem item in ListPassword)
            {
                if (item.Text.Contains(text) && item.Pwd.Contains(pwd))
                {
                    ListPassword.Remove(item);
                    Save();
                }
            }
        }

        public void Save()
        {
            var pass = JsonConvert.SerializeObject(ListPassword);
            Utils.WriteFileAsync(Filepass, pass);
        }

        private void Load()
        {
            try
            {
                var data = Utils.ReadFile(Filepass);
                ListPassword = JsonConvert.DeserializeObject<List<PasswordItem>>(data);
            }
            catch (Exception ex)
            {
                Logger.w("Cannot Load History", ex);
            }
        }
    }

    public class PasswordItem
    {
        public string Text { get; set; }
        public string Pwd { get; set; }
    }
}
