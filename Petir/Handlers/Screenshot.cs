using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Petir
{
    public class ScreenShot
    {
        private XBrowser myBrowser;
        private string name;
        private string tempname;
        private static string Basepath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"screenshot\";
        private string Savepath;
        private readonly int MaxWait = 750;
        private List<Bitmap> images = new List<Bitmap>();

        public int DocHeight { get; private set; }

        public ScreenShot(XBrowser b)
        {
            myBrowser = b;
        }
        
        public async void GetFullScreenshotAsync()
        {
            GetDocHeight();
            await PutTaskDelay();
            name = myBrowser.Address.GetHostname().Replace("www.", null).Replace(".", "_");
            int scrollHeight = DocHeight;
            Size size = myBrowser.ClientRectangle.Size;
            int height = size.Height;
            int width = size.Width;
            int scrollLeft = scrollHeight;
            int num = 0;
            bool atBottom = false;
            try
            {
                if (scrollHeight != 0)
                {
                    myBrowser.Xecute("(function() { document.documentElement.style.overflow = 'hidden'; })();");
                    while (!atBottom)
                    {
                        if (scrollLeft > height)
                        {
                            num++;
                            tempname = string.Concat(name, "_", num, ".png");
                            if (num != 1)
                            {
                                myBrowser.Xecute(string.Concat("(function() { window.scroll(0,", num * height - 30, "); })();"));
                            }
                            using (Bitmap Vs = GetCurrentViewScreenshot())
                            {
                                if (num == 1)
                                {
                                    SaveScreenshot(Vs, tempname);
                                }
                                else
                                {
                                    Rectangle rectangle = new Rectangle(new Point(0, 30), new Size(width, height));
                                    using (Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height))
                                    {
                                        using (Graphics graphic = Graphics.FromImage(bitmap))
                                        {
                                            graphic.DrawImage(Vs, new Rectangle(0, 0, bitmap.Width, bitmap.Height), rectangle, GraphicsUnit.Pixel);
                                            SaveScreenshot(bitmap, tempname);
                                        }
                                    }
                                }
                            }
                            if (myBrowser.Address.StartsWith("http://www.google") || myBrowser.Address.StartsWith("https://www.google") || myBrowser.Address.StartsWith("http://google"))
                            {
                                myBrowser.Xecute("(function() { var elements = document.querySelectorAll('*'); for (var i = 0; i < elements.length; i++) { var position = window.getComputedStyle(elements[i]).position; if (position === 'fixed') { elements[i].style.visibility = 'hidden'; } } })(); ");
                            }
                        }
                        else
                        {
                            myBrowser.Xecute(string.Concat("(function() { window.scrollBy(0,", height, "); })();"));
                            atBottom = true;
                            num++;
                            tempname = string.Concat(name, "_", num, ".png");
                            await PutTaskDelay();
                            Rectangle rectangle = new Rectangle(new Point(0, height - scrollLeft - 30), new Size(width, scrollLeft + 30));
                            using (Bitmap currentViewScreenshot = GetCurrentViewScreenshot())
                            {
                                using (Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height))
                                {
                                    using (Graphics graphic = Graphics.FromImage(bitmap))
                                    {
                                        graphic.DrawImage(currentViewScreenshot, new Rectangle(0, 0, bitmap.Width, bitmap.Height), rectangle, GraphicsUnit.Pixel);
                                        SaveScreenshot(bitmap, tempname);
                                    }
                                }
                            }
                        }
                        scrollLeft = scrollLeft - height;
                    }
                    myBrowser.Xecute("(function() { document.documentElement.style.overflow = 'auto'; })();");
                    myBrowser.Xecute("javascript:var s = function() { document.body.scrollTop = document.documentElement.scrollTop = 0;}; s();");
                    myBrowser.Xecute("(function() { var elements = document.querySelectorAll('*'); for (var i = 0; i < elements.length; i++) { var position = window.getComputedStyle(elements[i]).position; if (position === 'fixed') { elements[i].style.visibility = 'visible'; } } })(); ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Cannot take screenshot", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public async Task PutTaskDelay()
        {
            await Task.Delay(MaxWait);
        }

        public void GetScreenshot()
        {
            name = myBrowser.Address.GetHostname().Replace("www.", null).Replace(".", "_");
            tempname = string.Concat(name, ".png");
            Rectangle bounds = myBrowser.Bounds;
            using (Bitmap b = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                }
                SaveScreenshot(b, tempname);
            }
        }

        private void SaveScreenshot(Image img, string filename)
        {
            Savepath = Path.Combine(Basepath, name);
            var file = Path.Combine(Savepath, filename);
            using (img)
            {
                try
                {
                    if (!(new DirectoryInfo(Savepath)).Exists)
                    {
                        Directory.CreateDirectory(Savepath);
                    }
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                    img.Save(file, ImageFormat.Png);
                    img.Dispose();
                }
                catch (ExternalException ex)
                {
                    MessageBox.Show("Unable to save screenshot." + ex.ToString(), "Cannot save screenshot", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch (IOException oException)
                {
                    MessageBox.Show(oException.ToString());
                }
            }
        }
        
        /*DirectoryInfo directory = new DirectoryInfo(Savepath);
        if (directory != null )
        {
            FileInfo[] files = directory.GetFiles();
            CombineImages(files);
        }
        */
        public void CombineImages(FileInfo[] files)
        {
            List<Bitmap> images = new List<Bitmap>();
            string finalImage = Path.Combine(Savepath, string.Concat(name, "_final.png"));
            try
            {
                int width = 0;
                int height = 0;

                foreach (FileInfo file in files)
                {
                    Bitmap bitmap = new Bitmap(file.FullName);
                    if (bitmap.Width > width)
                    {
                        width = bitmap.Width;
                    }
                    height += bitmap.Height;
                    images.Add(bitmap);
                    File.Delete(file.FullName);
                }
                Bitmap img = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.Clear(Color.Black);
                    int offset = 0;
                    foreach (Bitmap image in images)
                    {
                        g.DrawImage(image,
                          new Rectangle(0, offset, image.Width, image.Height));
                        offset += image.Height;
                    }
                }
                SaveScreenshot(img, finalImage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                foreach (Bitmap image in images)
                {
                    image.Dispose();
                }
            }
        }

        private Bitmap GetCurrentViewScreenshot()
        {
            Bitmap bitmap;
            Rectangle clientRectangle = myBrowser.ClientRectangle;
            using (Bitmap b = new Bitmap(myBrowser.ClientRectangle.Width, clientRectangle.Height))
            {
                using (Graphics graphic = Graphics.FromImage(b))
                {
                    Point point = new Point(0, 0);
                    point = myBrowser.PointToScreen(new Point(0, 0));
                    Point point1 = new Point(0, 0);
                    clientRectangle = myBrowser.ClientRectangle;
                    graphic.CopyFromScreen(point, point1, clientRectangle.Size);
                }
                bitmap = new Bitmap(b);
            }
            return bitmap;
        }

        /*private void CombineImages(FileInfo[] files)
        {
            string finalImage = Path.Combine(Savepath, string.Concat(name, "_final.png"));
            List<int> imageHeights = new List<int>();
            int nIndex = 0;
            int width = 0;
            foreach (FileInfo file in files)
            {
                Image img = Image.FromFile(file.FullName);
                imageHeights.Add(img.Height);
                width += img.Width;
                img.Dispose();
            }
            imageHeights.Sort();
            int height = imageHeights[imageHeights.Count - 1];
            Bitmap img3 = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(img3);
            g.Clear(SystemColors.AppWorkspace);

            foreach (FileInfo file in files)
            {
                Image img = Image.FromFile(file.FullName);
                if (nIndex == 0)
                {
                    g.DrawImage(img, new Point(0, 0));
                    nIndex++;
                    width = img.Width;
                }
                else
                {
                    g.DrawImage(img, new Point(width, 0));
                    width += img.Width;
                }
                img.Dispose();
            }
            g.Dispose();
            SaveScreenshot(img3, finalImage);
            img3.Dispose();
            //rectangle = Empat persegi panjang
            //
        }*/
        private void GetDocHeight()
        {
            var task = myBrowser.Browser.GetBrowser().MainFrame.EvaluateScriptAsync("(function() { var body = document.body, html = document.documentElement; return  Math.max( body.scrollHeight, body.offsetHeight, html.clientHeight, html.scrollHeight, html.offsetHeight ); })();", null);
            task.ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    DocHeight = (int)t.Result.Result;
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public async void GetFullScreenshot()
        {
            GetDocHeight();
            await PutTaskDelay();
            name = myBrowser.Address.GetHostname().Replace("www.", null).Replace(".", "_");
            int scrollHeight = DocHeight;
            Size size = myBrowser.ClientRectangle.Size;
            int height = size.Height;
            int width = size.Width;
            int scrollLeft = scrollHeight;
            int num = 0;
            bool atBottom = false;
            try
            {
                if (scrollHeight != 0)
                {
                    myBrowser.Xecute("(function() { document.documentElement.style.overflow = 'hidden'; })();");
                    while (!atBottom)
                    {
                        if (scrollLeft > height)
                        {
                            num++;
                            if (num != 1)
                            {
                                myBrowser.Xecute(string.Concat("(function() { window.scroll(0,", num * height, "); })();"));
                            }
                            await PutTaskDelay();
                            using (Bitmap Vs = GetCurrentViewScreenshot())
                            {
                                if (num == 1)
                                {
                                    images.Add(Vs);
                                }
                                else
                                {
                                    Rectangle rectangle = new Rectangle(new Point(0, 30), new Size(width, height + 30));
                                    using (Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height))
                                    {
                                        using (Graphics graphic = Graphics.FromImage(bitmap))
                                        {
                                            graphic.DrawImage(Vs, new Rectangle(0, 0, bitmap.Width, bitmap.Height), rectangle, GraphicsUnit.Pixel);
                                            images.Add(bitmap);
                                        }
                                    }
                                }
                            }
                            if (myBrowser.Address.StartsWith("http://www.google") || myBrowser.Address.StartsWith("https://www.google") || myBrowser.Address.StartsWith("http://google"))
                            {
                                myBrowser.Xecute("(function() { var elements = document.querySelectorAll('*'); for (var i = 0; i < elements.length; i++) { var position = window.getComputedStyle(elements[i]).position; if (position === 'fixed') { elements[i].style.visibility = 'hidden'; } } })(); ");
                            }
                        }
                        else
                        {
                            myBrowser.Xecute(string.Concat("(function() { window.scrollBy(0,", height, "); })();"));
                            atBottom = true;
                            num++;
                            await PutTaskDelay();
                            Rectangle rectangle = new Rectangle(new Point(0, height - scrollLeft - 30), new Size(width, scrollLeft + 30));
                            using (Bitmap currentViewScreenshot = GetCurrentViewScreenshot())
                            {
                                using (Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height))
                                {
                                    using (Graphics graphic = Graphics.FromImage(bitmap))
                                    {
                                        graphic.DrawImage(currentViewScreenshot, new Rectangle(0, 0, bitmap.Width, bitmap.Height), rectangle, GraphicsUnit.Pixel);
                                        images.Add(bitmap);
                                    }
                                }
                            }
                        }
                        scrollLeft = scrollLeft - height;
                    }
                    myBrowser.Xecute("(function() { document.documentElement.style.overflow = 'auto'; })();");
                    myBrowser.Xecute("javascript:var s = function() { document.body.scrollTop = document.documentElement.scrollTop = 0;}; s();");
                    myBrowser.Xecute("(function() { var elements = document.querySelectorAll('*'); for (var i = 0; i < elements.length; i++) { var position = window.getComputedStyle(elements[i]).position; if (position === 'fixed') { elements[i].style.visibility = 'visible'; } } })(); ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Cannot take screenshot", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                CombineAsync();
            }
        }

        private async void CombineAsync()
        {
            Savepath = Path.Combine(Basepath, name);
            tempname = string.Concat(name, "_final.png");
            var file = Path.Combine(Savepath, tempname);
            int height = 0;
            int width = 0;
            int offset = 0;
            foreach (Bitmap image in images)
            {
                width = image.Width > width ? image.Width : width;
                height += image.Height;
            }
            await PutTaskDelay();
            Bitmap img = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(img))
            {
                foreach (Bitmap image in images)
                {
                    Image bitmap = image;
                    g.DrawImage(image, new Rectangle(0, offset, bitmap.Width, bitmap.Height));
                    offset += image.Height;
                }
            }
            await PutTaskDelay();
            img.Save(file, ImageFormat.Png);
            foreach (Bitmap image in images)
            {
                image.Dispose();
            }
            await PutTaskDelay();
            images.Clear();
        }
    }
}
