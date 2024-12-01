using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_Forms
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new Domain.User("JonM");

        }

        private void btnProcessFile_Click(object sender, EventArgs e)
        {
            string filePath = @"F:\temp\Import files\Test1.xlsx";

            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var p =  new ExcellProcessor.ProcessFile("Test1.xlsx", fs);


                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }


        }
    }
}
