using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RedHopSharp;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                test();
            }
            catch  (Exception g)
            {
                HoptoadClient client = new HoptoadClient();
                MessageBox.Show(client.Send(g));
            }

        }
        private void test()
        {
            kill();
        }

        private void kill()
        {
            breakme();
        }

        private void breakme()
        {
            Exception r = null;
            string s = r.Source;
            throw new InsufficientMemoryException("Your memory sucks");
        }



    }
}
