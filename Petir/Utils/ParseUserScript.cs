using CefSharp;
using CefSharp.Internals;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Petir
{
    internal static class ParseUserScript
    {
        private const string Value = @"( |\t)+([a-zA-Z\d :.,/\*_\+\?!\-\(\)\[\]]+)";
        private const string Name = @"@name" + Value;
        private const string Description = @"@description" + Value;
        private const string Author = @"@author" + Value;
        private const string Version = @"@version" + Value;
        private const string Match = @"@match" + Value;
        private const string Include = @"@include" + Value;
        private const string Exclude = @"@exclude" + Value;
        private const string Require = @"@require" + Value;
        private const string UpdateUrl = @"@updateURL" + Value;
        private const string DownloadUrl = @"@downloadURL" + Value;
        private const string Resource = @"@resource" + Value + Value;
        private const string InstallDisabled = @"@install-disabled";
        
        public static UserScript Parse(string contents, bool isCss)
        {
            var usercript = new UserScript();
            try
            {
                usercript.Type = isCss ? UserScript.ValueType.StyleSheet : UserScript.ValueType.Script;

                usercript.Name = GetContents(contents, Name, usercript.Name);

                usercript.Description = GetContents(contents, Description, usercript.Description);

                usercript.Author = GetContents(contents, Author, usercript.Author);

                if (!isCss)
                    usercript.Version = GetContents(contents, Version);

                Regex reg = new Regex(Match);
                Regex reg2 = new Regex(Include);
                MatchCollection matches = reg.Matches(contents);
                MatchCollection matches2 = reg2.Matches(contents);
                // Merge into single list
                List<Match> matches3 = new List<Match>();
                for (var i = 0; i < matches.Count; i++)
                {
                    matches3.Add(matches[i]);
                }
                for (var i = 0; i < matches2.Count; i++)
                {
                    matches3.Add(matches2[i]);
                }
                // If script has matches, use the parsed values. Otherwise, preserve current matches
                if (matches3.Count > 0)
                {
                    usercript.Include = new string[matches3.Count];
                    for (int i = 0; i < matches3.Count; i++)
                    {
                        usercript.Include[i] = matches3[i].Groups[2].Value;
                    }
                }

                reg = new Regex(Exclude);
                matches = reg.Matches(contents);
                if (matches.Count > 0)
                {
                    usercript.Exclude = new string[matches.Count];
                    for (int i = 0; i < matches.Count; i++)
                    {
                        usercript.Exclude[i] = matches[i].Groups[2].Value;
                    }
                }

                if (!isCss)
                {
                    usercript = UpdateParsedData(usercript, contents, isCss);

                    usercript.UpdateUrl = GetContents(contents, DownloadUrl);

                    if (usercript.UpdateUrl == String.Empty) usercript.UpdateUrl = GetContents(contents, UpdateUrl);
                }

                usercript.SavedValues = new Dictionary<string, string>();

                usercript.Enabled = !contents.Contains(InstallDisabled);

                return usercript;
            }
            catch (Exception ex)
            {
                Logger.w("Error parsing script metadata", ex);
            }
            return usercript; // return CSS 
        }

        public static UserScript UpdateParsedData(UserScript s, string contents, bool isCss)
        {
            //Requires API (should the API wrapper be added to the script)
            s.RequiresApi = contents.Contains("GM_");

            // Resources
            var reg = new Regex(Resource);
            var matches = reg.Matches(contents);
            s.Resources = new Dictionary<string, string>();
            foreach (Match match in matches)
            {
                s.Resources.Add(match.Groups[2].Value, match.Groups[3].Value);
            }

            // Require
            // Get new values
            if (s.Require == null)
                s.Require = new Dictionary<string, string>();

            reg = new Regex(Require);
            matches = reg.Matches(contents);
            var req = new List<string>();
            for (int i = 0; i < matches.Count; i++)
            {
                req.Add(matches[i].Groups[2].Value);
            }

            for (int i = 0; i < s.Require.Count; i++)
            {
                var key = s.Require.Keys.ToArray()[i];
                if (!req.Contains(key))
                {
                    s.Require.Remove(key);
                    i--;
                }
            }

            foreach (string t in req.Where(t => !s.Require.ContainsKey(t)))
            {
                s.Require.Add(t, ScriptDownloader.DownloadDefendencies(t));
            }
            return s;
        }
        
        private static string GetContents(string c, string regex, string defaultValue = "")
        {
            var r = new Regex(regex);
            var m = r.Match(c);
            if (m == null)
                return defaultValue;
            else
            {
                var v = m.Groups[2].Value;
                return String.IsNullOrEmpty(v) ? defaultValue : v;
            }
        }
    }
}

