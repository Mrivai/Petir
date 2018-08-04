using System;
using System.Windows.Forms;

namespace Petir
{
    public class AutomationHandler
    {
        XBrowser myForm;
        private string selector = "$(";
        private string atribut = ").attr('value','";
        private string endatribut = "');";
        private char hastag = '"';
        private string upload = "Upload(";
        private string xfill = "Fill(";
        private string xcheck = "Check(";
        private string xwrite = "Write(";
        private string xselect = "Select(";
        private string name = "[name='";
        private string endname = "']";
        private string tagclosing = ");";
        private string spacer = ",";

        internal AutomationHandler(XBrowser form)
        {
            myForm = form;
        }

        public void addcommand(string xtag, string xtype, string xname, string xvalue)
        {
            if (xtag == "input")
            {
                if (xtype == "text")
                {
                    var x = xfill + hastag + xtag + name + xname + endname + hastag;
                    x += spacer + hastag + xvalue + hastag + tagclosing + Environment.NewLine;
                    myForm.Appender(x);
                }
                else if (xtype == "password")
                {
                    var x = xfill + hastag + xtag + name + xname + endname + hastag;
                    x += spacer + hastag + xvalue + hastag + tagclosing + Environment.NewLine;
                    myForm.Appender(x);
                }
                else if (xtype == "file")
                {
                    var file = @"C:\Users\mrivai89\Pictures\Screenshots\Foto0271.jpg";
                    var x = upload + hastag + file + hastag + tagclosing + Environment.NewLine;
                    myForm.Appender(x);
                }
                else if (xtype == "checkbox" || xtype == "radio")
                {
                    var x = xcheck + hastag + xtag + name + xname + endname + hastag;
                    x += spacer + hastag + xvalue + hastag + tagclosing + Environment.NewLine;
                    myForm.Appender(x);
                }
            }
            else if (xtag == "textarea")
            {
                var x = xwrite + hastag + xtag + name + xname + endname + hastag;
                x += spacer + hastag + xvalue + hastag + tagclosing + Environment.NewLine;
                myForm.Appender(x);
            }
            else if (xtag == "select")
            {
                var x = xselect + hastag + xtag + name + xname + endname + hastag;
                x += spacer + hastag + xvalue + hastag + tagclosing + Environment.NewLine;
                myForm.Appender(x);
            }
        }

        public void setTarget(string target)
        {
            var x = "go(" + target + ")";
            myForm.Appender(x);
        }

        public void alert(string text)
        {
            myForm.xecute("alert('satria')");
        }

        public void fill(string input, string value)
        {
            var xc = selector + hastag + input + hastag + atribut + value + endatribut;
            execute(xc);
        }

        public void write(string input, string value)
        {
            var xc = selector + hastag + input + hastag + atribut + value + endatribut;
            execute(xc);
        }

        public void select(string input, string value)
        {
            var xc = selector + hastag + input + hastag + atribut + value + endatribut;
            execute(xc);
        }

        public void check(string input, string value)
        {
            var xc = selector + hastag + input + hastag + atribut + value + endatribut;
            execute(xc);
        }

        public void selectFile(string path)
        {
            //var sendKeyTask = Task.Delay(500).ContinueWith((_) => { SendKeys.SendWait(location + "{ENTER}"); }, TaskScheduler.FromCurrentSynchronizationContext());
            //await sendKeyTask;
            SendKeys.SendWait(path + "{ENTER}");
        }
        public void submit()
        {
            var x = "Submit();" + Environment.NewLine;
            myForm.Appender(x);
        }

        public void execute(string code)
        {
            myForm.xecute(code);
        }
    }
}