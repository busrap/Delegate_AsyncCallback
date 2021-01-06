using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Delegate_11
{
    public partial class Form1 : Form
    {
        public delegate void SayHandler(); //Metodu çalıştıracak olan delegate
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        public void Sayac()
        {
            DateTime baslangic = DateTime.Now;
            for (int i = 0; i < 5; i++)
            {
                listBox1.Items.Add(i);
            }
            TimeSpan sonuc = DateTime.Now - baslangic;
            MessageBox.Show(sonuc.TotalMilliseconds.ToString());
        }

        public void Sayac2()
        {
            DateTime baslangic = DateTime.Now;
            for (int i = 0; i < 5; i++)
            {
                listBox2.Items.Add(i);
            }
            TimeSpan sonuc = DateTime.Now - baslangic;
            MessageBox.Show(sonuc.TotalMilliseconds.ToString());
        }
        void Ajan(IAsyncResult sonuc)//Asenkron Call back  için oluşturulan takipci
        {
            SayHandler say = (SayHandler)sonuc.AsyncState;
            say.EndInvoke(sonuc);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SayHandler saydirici = Sayac;

            AsyncCallback callback = new AsyncCallback(Ajan);

            saydirici.BeginInvoke(callback,saydirici);
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SayHandler saydirici2 = Sayac2;


            AsyncCallback AjanCallBack = new AsyncCallback(Ajan);

            saydirici2.BeginInvoke(AjanCallBack, saydirici2);
            //BeginInvoke ile Sayac2'nin arkasına soyut bir takipçi gönderiyoruz

        }
    }
}
