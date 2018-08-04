using System.Collections.Generic;

namespace Petir
{
    public class Script
    {
        public bool AllowUserEdit = true;

        public bool Enabled = true;

        public List<UserScript> ListScript = new List<UserScript>();

        public bool CheckForUpdates = true;

        public bool RefreshOnSave = true;

        public bool RunOnPageRefresh = false;

        public bool AutoDownloadScripts = true;

        public bool UsePublicAPI = false;

        public bool InjectAPI = true;

        public bool InjectNotificationAPI = true;

        public bool CacheScripts = true;

        public bool LogScriptContentsOnRunError = false;

        public bool LogJScriptErrors = false;

        public bool UpdateDisabledScripts = false;

        public string[] BlacklistedUrls = new string[0];

        public int ReloadAfterPages = 5;

        public bool UseScriptmonkeyLink = true;

        public string ScriptmonkeyLinkUrl = "http://localhost:32888/";

        public int ScriptmonkeyLinkPollDelay = 3000;

        //public AllowedScriptmonkeyLinkCommands AllowedScriptmonkeyLinkCommands = new AllowedScriptmonkeyLinkCommands();
    }

    class ScriptWithContent
    {
        public UserScript ScriptData { get; set; }

        public string Content { get; set; }

        public ScriptWithContent(UserScript s)
        {
            ScriptData = s;
        }
    }
}
