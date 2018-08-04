
namespace Petir {

	/// <summary>
	/// functions in this class are accessible by JS using the code `host.X()`
	/// </summary>
	internal class HostHandler {
		MainForm myForm;

		public HostHandler(MainForm form) {
			myForm = form;
		}

		public void addNewBrowserTab(string url, bool focusNewTab = true) {
			myForm.AddNewBrowserTab(url);
		}

		public string getDownloads() {
			lock (myForm.downloads) {
				string x = JSON.Instance.ToJSON(myForm.downloads);
				//MessageBox.Show(x);
				return x;
			}
		}

		public bool cancelDownload(int downloadId) {
			lock (myForm.downloadCancelRequests) {
				if (!myForm.downloadCancelRequests.Contains(downloadId)) {
					myForm.downloadCancelRequests.Add(downloadId);
				}
			}
			return true;
		}
	}

}
