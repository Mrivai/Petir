
using System.Collections.Generic;

namespace Petir
{
    internal class ViewSource
    {
        private List<SourceItem> ListSource = new List<SourceItem>();

        public ViewSource()
        {

        }
        public void Add(string url, string source)
        {
            SourceItem h = new SourceItem();
            h.Url = url;
            h.Source = source;
            ListSource.Add(h);
        }

        public void Delete(string url)
        {
            foreach (SourceItem item in ListSource)
            {
                if (item.Url.Contains(url))
                {
                    ListSource.Remove(item);
                }
            }
        }

        public string View(string url)
        {
            string x = "";
            foreach (SourceItem item in ListSource)
            {
                if (item.Url.Contains(url))
                {
                    x = item.Source;
                }
            }
            return x;
        }

        public void Clear()
        {
            ListSource.Clear();
        }
    }

    public class SourceItem
    {
        public string Url { get; set; }
        public string Source { get; set; }
    }
}
