using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

namespace HatiraDeneme
{

    
    public partial class Form1 : Form
    {

        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int myfunc(string a, string b, int c, int d);
        private static string fileLocation = "C:\\Users\\Emirhan\\source\\repos\\HatiraDeneme\\records";
        private Timer timer;

        public Form1()
        {
            InitializeComponent();
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
            dataGridView1.Columns.Add("Dosya Adı", "Dosya Adı");
            dataGridView1.Columns.Add("Boyut", "Boyut (byte)");
            timer = new Timer();
            timer.Interval = 5000; // 5 saniye
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        public static string RemoveWhitespacesUsingLinq(string source)
        {
            return new string(source.Where(c => !char.IsWhiteSpace(c)).ToArray());
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            string[] fileList = Directory.GetFiles(fileLocation);
            // DataGridView'i güncelleyin
            dataGridView1.Rows.Clear();

            foreach (string fileWay in fileList)
            {
                string fileName = Path.GetFileName(fileWay);
                long fileSize = new FileInfo(fileWay).Length;
                dataGridView1.Rows.Add(fileName, fileSize);
            }
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Çift tıklanan hücreyi kontrol edin
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Dosya yolu bilgisini alın
                string file = dataGridView1.Rows[e.RowIndex].Cells["Dosya Adı"].Value.ToString();
                string filePath = fileLocation+"\\"+file;

                // Dosyayı açmak için işlemler yapın
                // Örneğin:
                Process.Start(filePath); // Varsayılan uygulama ile dosyayı açar
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {   
            myfunc("open new Type waveaudio Alias recsound", "", 0, 0);
            myfunc("record recsound", "", 0, 0);
            System.Threading.Thread.Sleep(10000);
            string username = RemoveWhitespacesUsingLinq(textBox1.Text);
            myfunc("save recsound C:\\Users\\Emirhan\\source\\repos\\HatiraDeneme\\records" + "\\" + username + ".wav", "", 0, 0);
            myfunc("close recsound", "", 0, 0);
            MessageBox.Show(username+" Ses Dosyası Kaydedildi.");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
