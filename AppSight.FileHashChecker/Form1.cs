using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppSight.FileHashChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Minimize();
            Text = $"{Application.ProductName} {Application.ProductVersion}";
            string[] args = Environment.GetCommandLineArgs();
            MessageBox.Show(string.Join(",", args));
        }

        private void Minimize()
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
        }
    }
}
