using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EOM_v3_M
{
    public partial class ViewForm : Form
    {
        string mhtFilePath = string.Empty;

        public ViewForm(string _data)
        {
            mhtFilePath = _data;
            InitializeComponent();
        }

        private void ViewForm_Load(object sender, EventArgs e)
        {
            Text = string.Empty; //MainForm.subjectData;

            webBrowser1.Navigate(mhtFilePath);
            MainForm.dc.Delay(100);
            SendKeys.Send("{F5}");

            // 위치 조정
            Location = MainForm.dc.CenterLocation(Width, Height);
        }

        private void ViewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            string tmpPath = @"C:\" + Application.ProductName + @"\mht_data.mht";

            FileInfo fi = new FileInfo(tmpPath);

            if (fi.Exists)
            {
                // 파일 삭제
                fi.Delete();
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            /*
            HtmlElement elem;

            if (webBrowser1.Document != null)
            {
                HtmlElementCollection elems = webBrowser1.Document.GetElementsByTagName("HTML");

                if (elems.Count == 1)
                {
                    elem = elems[0];
                    string pageSource = elem.OuterHtml;
                }
            }
            */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            webBrowser1.Navigate("about:blank");
            HtmlDocument doc = webBrowser1.Document;
            doc.Write(string.Empty);
            webBrowser1.Navigate(MainForm.eoViewData);
            */
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string convertFileName = MainForm.dc.FileNameTextDeleteConvert(Text);

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.FileName = convertFileName;
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            saveFileDialog.Filter = "mht files (*.mht)|*.mht";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(saveFileDialog.FileName);

                if (fi.Exists)
                {
                    fi.Delete();
                }

                File.Copy(@"C:\" + Application.ProductName + @"\mht_data.mht", saveFileDialog.FileName);
                
                //File.WriteAllText(saveFileDialog.FileName, elem.OuterHtml, Encoding.Unicode);
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
