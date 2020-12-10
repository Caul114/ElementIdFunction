using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElementIdFunction
{
    public partial class ElementIdFunctionWF : Form
    {
        public ElementIdFunctionWF()
        {
            InitializeComponent();
        }

        public ElementIdFunctionWF(Command dataBuffer)
        {
            InitializeComponent();

            // Set the data source of the ListBox control
            functionListBox.DataSource = dataBuffer.Elements;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Mouse Events
        private Mutex checking = new Mutex(false);
        private AutoResetEvent are = new AutoResetEvent(false);

        // You could create just one handler, but this is to show what you need to link to
        private void Form1_MouseLeave(object sender, EventArgs e) => StartWaitingForClickFromOutside();
        private void Form1_Leave(object sender, EventArgs e) => StartWaitingForClickFromOutside();
        private void Form1_Deactivate(object sender, EventArgs e) => StartWaitingForClickFromOutside();
        private void StartWaitingForClickFromOutside()
        {
            if (!checking.WaitOne(10)) return;

            var ctx = new SynchronizationContext();
            are.Reset();

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (are.WaitOne(1)) break;
                    if (MouseButtons == MouseButtons.Left)
                    {
                        ctx.Send(ClickFromOutside, null);
                        // You might need to put in a delay here and not break depending on what you want to accomplish
                        break;
                    }
                }
                checking.ReleaseMutex();
            });
        }
        private void ClickFromOutside(object state) => MessageBox.Show("Clicked from outside of the window");
        private void Form1_MouseEnter(object sender, EventArgs e) => are.Set();
        private void Form1_Activated(object sender, EventArgs e) => are.Set();
        private void Form1_Enter(object sender, EventArgs e) => are.Set();
        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible) are.Set();
            else StartWaitingForClickFromOutside();
        }
        #endregion
    }
}
