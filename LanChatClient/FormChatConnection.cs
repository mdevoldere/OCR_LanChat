using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LanChatClient
{
    public partial class FormChatConnection : Form
    {
        public FormChatConnection()
        {
            InitializeComponent();
        }

        private void FormChatConnection_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection c = new AutoCompleteStringCollection();

            foreach(string item in Enum.GetNames(typeof(LanChatData)))
            {
                c.Add(item);
                //MessageBox.Show(item);
            }

            txtUsername.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtUsername.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtUsername.AutoCompleteCustomSource = c;
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            FormClient form = new FormClient();
            form.SetCredentials(this);
            this.Hide();
            form.Show();
            
        }
    }
}
