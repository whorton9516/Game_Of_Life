using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public delegate void ApplyEventHandler(object sender, ApplyEventArgs e);


    public partial class TemplateIOModal : Form
    {
        public event ApplyEventHandler Apply;
        public TemplateIOModal()
        {
            InitializeComponent();
        }

        public string templateName { get; set; }

        private void okButton_Click(object sender, EventArgs e)
        {
            templateName = ioTextBox.Text;
            if (Apply != null) Apply(this, new ApplyEventArgs(this.templateName));
        }
    }


    public class ApplyEventArgs : EventArgs
    {
        public string name { get; set; }

        public ApplyEventArgs(string templateName)
        {
            this.name = templateName;
        }
    }
}
