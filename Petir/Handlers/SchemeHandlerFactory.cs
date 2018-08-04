using CefSharp;

namespace Petir {
    internal class SchemeHandlerFactory : ISchemeHandlerFactory
    {
        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            return new SchemeHandler(XBrowser.Instance);
        }
    }
}