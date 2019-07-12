using CefSharp;

namespace Petir
{
    internal class DownloadHandler : IDownloadHandler
    {
        MainForm myForm;

        public DownloadHandler(MainForm form)
        {
            myForm = form;
        }

        private DownloadItem item;

        public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem item, IBeforeDownloadCallback callback)
        {
            if (!callback.IsDisposed)
            {
                using (callback)
                {
                    myForm.Idm.Add(item);
                    if(myForm.X.downloadpath != null)
                    {
                        callback.Continue(myForm.X.downloadpath, true);
                    }
                    else
                    {
                        callback.Continue(item.SuggestedFileName, true);
                    }
                    /*
                    // ask browser what path it wants to save the file into
                    string path = myForm.CalcDownloadPath(item);
                    
                    // if file should not be saved, path will be null, so skip file
                    if (path != null) {
                        // skip file
                        //callback.Continue(item.SuggestedFileName, true);
                        callback.Continue(path, false);
                    } else {
                        // download file
                        callback.Dispose();
                        //open the downloads tab
                        myForm.OpenDownloadsTab();
                    }
                    */
                }
            }
        }
        

        public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {

            myForm.Idm.downloads.TryGetValue(downloadItem.Id, out item);

            if (downloadItem.IsInProgress)
            {
                if (myForm.Idm.CancelRequests.Contains(downloadItem.Id)) callback.Cancel();
                else if (myForm.Idm.downloadPauseRequests.Contains(downloadItem.Id)) callback.Pause();
                else if (myForm.Idm.downloadResumeRequests.Contains(downloadItem.Id)) callback.Resume();
                else
                {
                    item = downloadItem;
                }
            }
            //Console.WriteLine(downloadItem.Url + " %" + downloadItem.PercentComplete + " complete");
        }
    }
}
