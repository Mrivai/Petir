using CefSharp;
using NReco.VideoConverter;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Petir
{
    /// <summary>
    /// Class used to implement a custom resource handler. The methods of this class will always be called on the CEF IO thread.
    /// Blocking the CEF IO thread will adversely affect browser performance. We suggest you execute your code in a Task (or similar).
    /// To implement async handling, spawn a new Task (or similar), keep a reference to the callback. When you have a 
    /// fully populated stream, execute the callback. Once the callback Executes, 
    /// GetResponseHeaders will be called where you
    /// can modify the response including headers, or even redirect to a new Url. Set your responseLength and headers
    /// Populate the dataOut stream in ReadResponse. For those looking for a sample implementation or upgrading from 
    /// a previous version <see cref="ResourceHandler"/>. For those upgrading, inherit from ResourceHandler instead of IResourceHandler
    /// add the override keywoard to existing methods e.g. ProcessRequestAsync.
    /// </summary>
    public class VideoResourceHandler : IResourceHandler
    {
        private FFMpegConverter _converter = new FFMpegConverter();
        //private ConvertLiveMediaTask FFmpeg;
        private Stream outputStream ;

        /// <summary>
        /// Begin processing the request.  
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <param name="callback">The callback used to Continue or Cancel the request (async).</param>
        /// <returns>To handle the request return true and call
        /// <see cref="ICallback.Continue"/> once the response header information is available
        /// <see cref="ICallback.Continue"/> can also be called from inside this method if
        /// header information is available immediately).
        /// To cancel the request return false.</returns>
        public bool ProcessRequest(IRequest request, ICallback callback)
        {
            Task.Run(() =>
            {
                using (callback)
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(request.Url);

                    var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    // Get the stream associated with the response.
                    var receiveStream = httpWebResponse.GetResponseStream();
                    var mime = httpWebResponse.ContentType;

                    var stream = new MemoryStream();
                    receiveStream.CopyTo(stream);
                    httpWebResponse.Close();
                    stream.Position = 0;

                    //FFmpeg =
                    _converter.ConvertLiveMedia(stream,Format.mp4, outputStream, Format.webm, new ConvertSettings()
                    {
                        VideoCodec = "libvpx",
                        AudioCodec = "libvorbis",
                        CustomInputArgs = "-preset ultrafast"
                    });
                    //FFmpeg.Start();
                    //FFmpeg.Wait();

                    callback.Continue();
                }
            });
            return true;
        }

        /// <summary>
        /// Retrieve response header information. If the response length is not known
        /// set responseLength to -1 and ReadResponse() will be called until it
        /// returns false. If the response length is known set responseLength
        /// to a positive value and ReadResponse() will be called until it returns
        /// false or the specified number of bytes have been read. 
        /// If an error occured while setting up the request you can set <see cref="IResponse.ErrorCode"/>
        /// to indicate the error condition.
        /// </summary>
        /// <param name="response">Use the response object to set the mime type, http status code and other optional header values.</param>
        /// <param name="responseLength">If the response length is not known set responseLength to -1</param>
        /// <param name="redirectUrl">To redirect the request to a new URL set redirectUrl to the new Url.</param>
        public void GetResponseHeaders(IResponse response, out long responseLength, out string redirectUrl)
        {
            response.Headers.Add("Content-Type", "video/mp4");
            responseLength = outputStream.Length;
            response.MimeType = "video/mp4";
            response.StatusCode = 200;
            response.StatusText = "OK";
            redirectUrl = null;
        }

        /// <summary>
        /// Read response data. If data is available immediately copy to
        /// dataOut, set bytesRead to the number of bytes copied, and return true.
        /// To read the data at a later time set bytesRead to 0, return true and call ICallback.Continue() when the
        /// data is available. To indicate response completion return false.
        /// </summary>
        /// <param name="dataOut">Stream to write to</param>
        /// <param name="bytesRead">Number of bytes copied to the stream</param>
        /// <param name="callback">The callback used to Continue or Cancel the request (async).</param>
        /// <returns>If data is available immediately copy to dataOut, set bytesRead to the number of bytes copied,
        /// and return true.To indicate response completion return false.</returns>
        /// <remarks>Depending on this size of your response this method may be called multiple times</remarks>
        public bool ReadResponse(Stream dataOut, out int bytesRead, ICallback callback)
        {
            callback.Dispose();
            if (outputStream == null)
            {
                bytesRead = 0;
                return false;
            }
            outputStream.CopyTo(dataOut);
            var buffer = new byte[dataOut.Length];
            bytesRead = outputStream.Read(buffer, 0, buffer.Length);
            dataOut.Write(buffer, 0, buffer.Length);

            return bytesRead > 0;
        }

        /// <summary>
        /// Request processing has been canceled.
        /// </summary>
        public void Cancel()
        {
            //FFmpeg.Abort();
        }

        public bool CanGetCookie(CefSharp.Cookie cookie)
        {
            return true;
        }

        public bool CanSetCookie(CefSharp.Cookie cookie)
        {
            return true;
        }

        public void Dispose()
        {
            //FFmpeg.Abort();
            if(outputStream != null)
            {
                outputStream.Dispose();
            }
        }
    }
}