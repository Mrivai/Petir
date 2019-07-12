using CefSharp;
using System;
using System.IO;

namespace Petir
{
    public class RequestWrapper
    {
        public byte[] Data
        {
            get;
            private set;
        }

        public ulong Identifier
        {
            get;
            private set;
        }

        public string MimeType
        {
            get;
            private set;
        }

        public string RequestUrl
        {
            get;
            private set;
        }

        public ResourceType ResourceType
        {
            get;
            private set;
        }

        public RequestWrapper(string requestUrl, ResourceType resourceType, string mimeType, byte[] data, ulong id)
        {
            RequestUrl = requestUrl;
            ResourceType = resourceType;
            MimeType = mimeType;
            Data = data;
            Identifier = id;
        }

        public override bool Equals(object obj)
        {
            RequestWrapper requestWrapper = obj as RequestWrapper;
            if (requestWrapper == null)
            {
                return false;
            }
            return RequestUrl == requestWrapper.RequestUrl;
        }

        public string FileName()
        {
            string safeFilename = "";
            switch (ResourceType)
            {
                case ResourceType.MainFrame:
                    {
                        safeFilename = "mainframe.html";
                        return safeFilename;
                    }
                case ResourceType.SubFrame:
                    {
                        safeFilename = GetSafeFilename(safeFilename);
                        return safeFilename;
                    }
                case ResourceType.Stylesheet:
                case ResourceType.Script:
                case ResourceType.Image:
                case ResourceType.FontResource:
                case ResourceType.Object:
                case ResourceType.Xhr:
                    {
                        safeFilename = GetSafeFilename(safeFilename);
                        return safeFilename;
                    }
                case ResourceType.SubResource:
                case ResourceType.Media:
                case ResourceType.Worker:
                case ResourceType.SharedWorker:
                case ResourceType.Prefetch:
                case ResourceType.Favicon:
                    {
                        return safeFilename;
                    }
                default:
                    {
                        return safeFilename;
                    }
            }
        }

        public override int GetHashCode()
        {
            return RequestUrl.GetHashCode() ^ MimeType.GetHashCode();
        }

        private string GetSafeFilename(string filename)
        {
            DateTime now;
            try
            {
                filename = Path.GetFileName(RequestUrl);
                filename = string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
                if (filename.Length > 150)
                {
                    filename = filename.Substring(0, 150);
                }
                if (filename == "")
                {
                    now = DateTime.Now;
                    filename = string.Concat("blank_file_", now.ToString("ddMMyyyyHHmmss"));
                }
            }
            catch
            {
                now = DateTime.Now;
                filename = string.Concat("error_file_", now.ToString("ddMMyyyyHHmmss"));
            }
            return filename;
        }

        public bool IsStringRequest()
        {
            if (ResourceType == ResourceType.MainFrame || ResourceType == ResourceType.SubFrame || ResourceType == ResourceType.Script)
            {
                return true;
            }
            return ResourceType == ResourceType.Stylesheet;
        }
    }
}