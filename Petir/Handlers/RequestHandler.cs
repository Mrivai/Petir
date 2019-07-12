using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace Petir
{
    internal class RequestHandler : IRequestHandler {
		MainForm myForm;
        private Control parent;
        private string oldAddress = "";
        private Dictionary<ulong, MemoryStreamResponseFilter> responseDictionary = new Dictionary<ulong, MemoryStreamResponseFilter>();
        public List<HeaderWrapper> RequestHeaders { get; private set; } = new List<HeaderWrapper>();
        public HashSet<RequestWrapper> Resources { get; private set; } = new HashSet<RequestWrapper>();
        public List<HeaderWrapper> ResponseHeaders { get; private set; } = new List<HeaderWrapper>();

        public RequestHandler(MainForm form) {
			myForm = form;
		}

        public bool CanGetCookies(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request)
        {
            return true;
        }

        public bool CanSetCookie(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, Cookie cookie)
        {
            return true;
        }
        
        // Summary:
        //     Called when the browser needs credentials from the user.
        //
        // Parameters:
        //   frame:
        //     The frame object that needs credentials (This will contain the URL that is
        //     being requested.)
        //
        //   isProxy:
        //     indicates whether the host is a proxy server
        //
        //   callback:
        //     Callback interface used for asynchronous continuation of authentication requests.
        //
        // Returns:
        //     Return true to continue the request and call CefAuthCallback::Continue()
        //     when the authentication information is available. Return false to cancel
        //     the request.
        public bool GetAuthCredentials(IWebBrowser browserControl, IBrowser browser, IFrame frame, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback) {
			
			return false;
		}
		//
		// Summary:
		//     Called on the CEF IO thread to optionally filter resource response content.
		//
		// Parameters:
		//   frame:
		//     The frame that is being redirected.
		//
		//   request:
		//     the request object - cannot be modified in this callback
		//
		//   response:
		//     the response object - cannot be modified in this callback
		//
		// Returns:
		//     Return an IResponseFilter to intercept this response, otherwise return null
		public IResponseFilter GetResourceResponseFilter(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response) {
            MemoryStreamResponseFilter memoryStreamResponseFilter = new MemoryStreamResponseFilter();
            responseDictionary.Add(request.Identifier, memoryStreamResponseFilter);
            return memoryStreamResponseFilter;
        }
		//
		// Summary:
		//     Called before browser navigation.  If the navigation is allowed CefSharp.IWebBrowser.FrameLoadStart
		//     and CefSharp.IWebBrowser.FrameLoadEnd will be called. If the navigation is
		//     canceled CefSharp.IWebBrowser.LoadError will be called with an ErrorCode
		//     value of CefSharp.CefErrorCode.Aborted.
		//
		// Parameters:
		//   frame:
		//     The frame the request is coming from
		//
		//   request:
		//     the request object - cannot be modified in this callback
		//
		//   isRedirect:
		//     has the request been redirected
		//
		// Returns:
		//     Return true to cancel the navigation or false to allow the navigation to
		//     proceed.
        public bool OnBeforeBrowse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool userGesture, bool isRedirect)
        {
            return false;
        }

        //
        // Summary:
        //     Called before a resource request is loaded. For async processing return CefSharp.CefReturnValue.ContinueAsync
        //     and execute CefSharp.IRequestCallback.Continue(System.Boolean) or CefSharp.IRequestCallback.Cancel()
        //
        // Parameters:
        //   frame:
        //     The frame object
        //
        //   request:
        //     the request object - can be modified in this callback.
        //
        //   callback:
        //     Callback interface used for asynchronous continuation of url requests.
        //
        // Returns:
        //     To cancel loading of the resource return CefSharp.CefReturnValue.Cancel or
        //     CefSharp.CefReturnValue.Continue to allow the resource to load normally.
        //     For async return CefSharp.CefReturnValue.ContinueAsync
        public CefReturnValue OnBeforeResourceLoad(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback) {
            //
            // Summary:
            //     dipanggil untuk menganti user-agent ke mobile, ketika ukuran form webbrowser == 300 px
            //
            var ch = (ChromiumWebBrowser)browserControl;
            parent = ch.Parent;
            var x = parent.FindForm() as XBrowser;
            if (x.Ua != null)
            {
                var headers = request.Headers;
                var userAgent = headers["User-Agent"];
                headers["User-Agent"] = x.Ua;
                request.Headers = headers;
            }
            /*
             * if (myForm.X.Ua != string.Empty)
            {
                var headers = request.Headers;
                var userAgent = headers["User-Agent"];
                headers["User-Agent"] = myForm.X.Ua;
                request.Headers = headers;
            }
            var ch = (ChromiumWebBrowser)browserControl;
            parent = ch.Parent;
            var x = parent.FindForm() as XBrowser;
            if (x.Ua !=null)
            {
                var headers = request.Headers;
                var userAgent = headers["User-Agent"];
                headers["User-Agent"] = x.Ua;
                request.Headers = headers;
            }
            */
            return CefReturnValue.Continue;
		}
        //
        // Summary:
        //     Called to handle requests for URLs with an invalid SSL certificate.  Return
        //     true and call CefSharp.IRequestCallback.Continue(System.Boolean) either in
        //     this method or at a later time to continue or cancel the request. If CefSettings.IgnoreCertificateErrors
        //     is set all invalid certificates will be accepted without calling this method.
        //
        // Parameters:
        //   errorCode:
        //     the error code for this invalid certificate
        //
        //   requestUrl:
        //     the url of the request for the invalid certificate
        //
        //   sslInfo:
        //     ssl certificate information
        //
        //   callback:
        //     Callback interface used for asynchronous continuation of url requests.  If
        //     empty the error cannot be recovered from and the request will be canceled
        //     automatically.
        //
        // Returns:
        //     Return false to cancel the request immediately. Return true and use CefSharp.IRequestCallback
        //     to execute in an async fashion.
        public bool OnCertificateError(IWebBrowser browserControl, IBrowser browser, CefErrorCode errorCode, string requestUrl, ISslInfo sslInfo, IRequestCallback callback) {
			return true;
		}
		//
		// Summary:
		//     Called on the UI thread before OnBeforeBrowse in certain limited cases where
		//     navigating a new or different browser might be desirable. This includes user-initiated
		//     navigation that might open in a special way (e.g.  links clicked via middle-click
		//     or ctrl + left-click) and certain types of cross-origin navigation initiated
		//     from the renderer process (e.g.  navigating the top-level frame to/from a
		//     file URL).
		//
		// Parameters:
		//   frame:
		//     The frame object
		//
		//   targetDisposition:
		//     The value indicates where the user intended to navigate the browser based
		//     on standard Chromium behaviors (e.g. current tab, new tab, etc).
		//
		//   userGesture:
		//     The value will be true if the browser navigated via explicit user gesture
		//     (e.g. clicking a link) or false if it navigated automatically (e.g. via the
		//     DomContentLoaded event).
		//
		// Returns:
		//     Return true to cancel the navigation or false to allow the navigation to
		//     proceed in the source browser's top-level frame.
		public bool OnOpenUrlFromTab(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture) {
			return false;
		}
		//
		// Summary:
		//     Called when a plugin has crashed
		//
		// Parameters:
		//   pluginPath:
		//     path of the plugin that crashed
		public void OnPluginCrashed(IWebBrowser browserControl, IBrowser browser, string pluginPath) {
		}
		//
		// Summary:
		//     Called on the UI thread to handle requests for URLs with an unknown protocol
		//     component. SECURITY WARNING: YOU SHOULD USE THIS METHOD TO ENFORCE RESTRICTIONS
		//     BASED ON SCHEME, HOST OR OTHER URL ANALYSIS BEFORE ALLOWING OS EXECUTION.
		//
		// Parameters:
		//   url:
		//     the request url
		//
		// Returns:
		//     return to true to attempt execution via the registered OS protocol handler,
		//     if any. Otherwise return false.
		public bool OnProtocolExecution(IWebBrowser browserControl, IBrowser browser, string url) {
			return true;
		}
		//
		// Summary:
		//     Called when JavaScript requests a specific storage quota size via the webkitStorageInfo.requestQuota
		//     function.  For async processing return true and execute CefSharp.IRequestCallback.Continue(System.Boolean)
		//     at a later time to grant or deny the request or CefSharp.IRequestCallback.Cancel()
		//     to cancel.
		//
		// Parameters:
		//   originUrl:
		//     the origin of the page making the request
		//
		//   newSize:
		//     is the requested quota size in bytes
		//
		//   callback:
		//     Callback interface used for asynchronous continuation of url requests.
		//
		// Returns:
		//     Return false to cancel the request immediately. Return true to continue the
		//     request and call CefSharp.IRequestCallback.Continue(System.Boolean) either
		//     in this method or at a later time to grant or deny the request.
		public bool OnQuotaRequest(IWebBrowser browserControl, IBrowser browser, string originUrl, long newSize, IRequestCallback callback) {
			callback.Continue(true);
			return true;
		}
		//
		// Summary:
		//     Called when the render process terminates unexpectedly.
		//
		// Parameters:
		//   status:
		//     indicates how the process terminated.
		public void OnRenderProcessTerminated(IWebBrowser browserControl, IBrowser browser, CefTerminationStatus status) {

		}
		//
		// Summary:
		//     Called on the CEF UI thread when the render view associated with browser
		//     is ready to receive/handle IPC messages in the render process.
		public void OnRenderViewReady(IWebBrowser browserControl, IBrowser browser) {
            //
        }
		//
		// Summary:
		//     Called on the CEF IO thread when a resource load has completed.
		//
		// Parameters:
		//   frame:
		//     The frame that is being redirected.
		//
		//   request:
		//     the request object - cannot be modified in this callback
		//
		//   response:
		//     the response object - cannot be modified in this callback
		//
		//   status:
		//     indicates the load completion status
		//
		//   receivedContentLength:
		//     is the number of response bytes actually read.
		public void OnResourceLoadComplete(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response, UrlRequestStatus status, long receivedContentLength) {
            
            if (request.Url.Contains(".user.js"))
            {
                var code = browserControl.GetSourceAsync().ToString();
                myForm.monyet.InstallScript(request.Url, code);
            }
            if (oldAddress != browserControl.Address || oldAddress == "")
            {
                oldAddress = browserControl.Address;
                Resources.Clear();
                ResponseHeaders.Clear();
                RequestHeaders.Clear();
            }
            Dictionary<string, string> dictionary = response.Headers.AllKeys.ToDictionary((string x) => x, (string x) => response.Headers[x]);
            ResponseHeaders.Add(new HeaderWrapper(request.Identifier, dictionary));
            Dictionary<string, string> strs = request.Headers.AllKeys.ToDictionary((string x) => x, (string x) => request.Headers[x]);
            RequestHeaders.Add(new HeaderWrapper(request.Identifier, strs));
            if (responseDictionary.TryGetValue(request.Identifier, out MemoryStreamResponseFilter memoryStreamResponseFilter))
            {
                byte[] data = memoryStreamResponseFilter.Data;
                Resources.Add(new RequestWrapper(request.Url, request.ResourceType, response.MimeType, data, request.Identifier));
            }
            //var x = response.MimeType;
            //scrape all link on current web here
            //myForm.X.Appender(request.Url.GetQuery("path"));

            //myForm.X.Appender(request.Url + " = " + status);
            /*
            if (request.Url.Contains(myForm.ViewsourceURL))
            {
                
                var code = myForm.viewer.View(request.Url.GetQuery("path"));
                MessageBox.Show(code.ToString());
                var t = string.Format("var x = @{0}{1}{0};", '"', code);
                t += string.Format("$({0}#xmpTagId{0}).text(x);", '"');
                myForm.X.ExecuteJs(t);
                
            }
            */
        }
        //
        // Summary:
        //     Called on the IO thread when a resource load is redirected. The CefSharp.IRequest.Url
        //     parameter will contain the old URL and other request-related information.
        //
        // Parameters:
        //   frame:
        //     The frame that is being redirected.
        //
        //   request:
        //     the request object - cannot be modified in this callback
        //
        //   newUrl:
        ////     the new URL and can be changed if desired
        public void OnResourceRedirect(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response, ref string newUrl)
        {
            if (myForm.fastmode && !request.Url.Contains("google.com") && !request.Url.Contains(myForm.GoogleWeblightUrl))
            {
                newUrl = myForm.GoogleWeblightUrl + request.Url;
            }
            if (newUrl.Contains("restricted.tri.co.id"))
            {
                newUrl = myForm.GoogleWeblightUrl + request.Url;
            }
        }

        //
        // Summary:
        //     Called on the CEF IO thread when a resource response is received.  To allow
        //     the resource to load normally return false.  To redirect or retry the resource
        //     modify request (url, headers or post body) and return true.  The response
        //     object cannot be modified in this callback.
        //
        // Parameters:
        //   frame:
        //     The frame that is being redirected.
        //
        //   request:
        //     the request object
        //
        //   response:
        //     the response object - cannot be modified in this callback
        //
        // Returns:
        //     To allow the resource to load normally return false.  To redirect or retry
        //     the resource modify request (url, headers or post body) and return true.
        public bool OnResourceResponse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response) {

			int code = response.StatusCode;
            if (code == 504)
            {
                request.Url = myForm.CannotConnectURL + "?path=" + request.Url.EncodeURL();
                return true;
            }
            
            /*
            // if NOT FOUND
            if (code == 404) {

                if (!request.Url.IsURLLocalhost()) {

                    // redirect to web archive to try and find older version
                    request.Url = '' + request.Url;
                } else {

                    // show offline "file not found" page
                    request.Url = myForm.FileNotFoundURL + "?path=" + request.Url.EncodeURL();
                }

                return false;
            }


             if FILE NOT FOUND
            if (code == 0 && request.Url.IsURLOfflineFile()) {
                string path = request.Url.FileURLToPath();
                if (path.FileNotExists()) {

                    // show offline "file not found" page
                    request.Url = myForm.FileNotFoundURL + "?path=" + path.EncodeURL();
                    return true;

                }
            } else {

                // if CANNOT CONNECT
                if (code == 0 || code == 444 || (code >= 500 && code <= 599)) {
                    // show offline "cannot connect to server" page
                    request.Url = myForm.CannotConnectURL + "?path=" + request.Url.EncodeURL();
                    return true;
                }

            }
            */
            return false;
		}

        public bool OnSelectClientCertificate(IWebBrowser browserControl, IBrowser browser, bool isProxy, string host, int port, X509Certificate2Collection certificates, ISelectClientCertificateCallback callback)
        {
            throw new System.NotImplementedException();
        }
    }
}
