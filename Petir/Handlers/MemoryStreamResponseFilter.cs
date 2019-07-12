using CefSharp;
using System;
using System.IO;

namespace Petir
{
    public class MemoryStreamResponseFilter : IResponseFilter, IDisposable
    {
        private MemoryStream memoryStream;

        public byte[] Data
        {
            get
            {
                return memoryStream.ToArray();
            }
        }

        public MemoryStreamResponseFilter()
        {
        }

        FilterStatus IResponseFilter.Filter(Stream dataIn, out long dataInRead, Stream dataOut, out long dataOutWritten)
        {
            if (dataIn == null)
            {
                dataInRead = 0;
                dataOutWritten = 0;
                return FilterStatus.Done;
            }
            dataInRead = dataIn.Length;
            if (dataIn.Length <= dataOut.Length)
            {
                dataOutWritten = Math.Min(dataInRead, dataOut.Length);
                try
                {
                    dataIn.CopyTo(dataOut);
                }
                catch
                {
                }
                dataIn.Position = 0;
                dataIn.CopyTo(memoryStream);
                return FilterStatus.Done;
            }
            byte[] numArray = new byte[checked(dataOut.Length)];
            dataIn.Seek(0, SeekOrigin.Begin);
            dataIn.Read(numArray, 0, numArray.Length);
            dataOut.Write(numArray, 0, numArray.Length);
            dataInRead = dataOut.Length;
            dataOutWritten = dataOut.Length;
            return FilterStatus.NeedMoreData;
        }

        bool IResponseFilter.InitFilter()
        {
            memoryStream = new MemoryStream();
            return true;
        }

        void IDisposable.Dispose()
        {
            memoryStream.Dispose();
            memoryStream = null;
        }
    }
}