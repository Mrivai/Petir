using CefSharp;

namespace Petir
{
    public class VideoResourceHandlerFactory : IResourceHandlerFactory
    {
        bool IResourceHandlerFactory.HasHandlers
        {
            get { return true; }
        }

        IResourceHandler IResourceHandlerFactory.GetResourceHandler(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request)
        {
            if (request.Url.Contains(".mp4"))
            {
                return new VideoResourceHandler();
            }
            return null;
        }
    }
}