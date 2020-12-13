using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;

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

using System.Data.OleDb;
using Microsoft.Office.Interop;

namespace ElementIdFunction
{
    public partial class ElementIdFunctionWF : Form
    {
        Command _databuffer = new Command();

        public ElementIdFunctionWF(Command dataBuffer)
        {
            InitializeComponent();

            // Set the data source of the ListBox control
            _databuffer = dataBuffer;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void pickerButton_Click(object sender, EventArgs e)
        {
            var list = _databuffer.Element;
            functionListBox.DataSource = list;
            string value = list[0].ToString();
            MessageBox.Show(value, "Parameter : ");
        }

        //private void ElementIdFunctionWF_Load(object sender, EventArgs e)
        //{
        //    string name = "Foglio1";
        //    String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
        //                "C:\\DatiLDB\\ExcelData\\DBProva1.xlsx" +
        //                ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

        //    OleDbConnection con = new OleDbConnection(constr);
        //    OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
        //    con.Open();

        //    OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
        //    DataTable data = new DataTable();
        //    sda.Fill(data);
        //    dataGridView1.DataSource = data;
        //}




        //#region Mouse Events
        //private Mutex checking = new Mutex(false);
        //private AutoResetEvent are = new AutoResetEvent(false);

        //// You could create just one handler, but this is to show what you need to link to
        //private void ElementIdFunctionWF_Click(object sender, EventArgs e) => StartWaitingForClickFromOutside();
        //private void ElementIdFunctionWF_MouseLeave(object sender, EventArgs e) => StartWaitingForClickFromOutside();
        //private void ElementIdFunctionWF_Leave(object sender, EventArgs e) => StartWaitingForClickFromOutside();
        //private void ElementIdFunctionWF_Deactivate(object sender, EventArgs e) => StartWaitingForClickFromOutside();
        //private void StartWaitingForClickFromOutside()
        //{
        //    if (!checking.WaitOne(10)) return;

        //    var ctx = new SynchronizationContext();
        //    are.Reset();

        //    Task.Factory.StartNew(() =>
        //    {
        //        while (true)
        //        {
        //            if (are.WaitOne(1)) break;
        //            if (MouseButtons == MouseButtons.Left)
        //            {
        //                ctx.Send(ClickFromOutside, null);
        //                // You might need to put in a delay here and not break depending on what you want to accomplish
        //                break;
        //            }
        //        }
        //        checking.ReleaseMutex();
        //    });
        //}
        //private void ClickFromOutside(object state) => MessageBox.Show("Clicked from outside of the window");
        //private void ElementIdFunctionWF_MouseEnter(object sender, EventArgs e) => are.Set();
        //private void ElementIdFunctionWF_Activated(object sender, EventArgs e) => are.Set();
        //private void ElementIdFunctionWF_Enter(object sender, EventArgs e) => are.Set();
        //private void ElementIdFunctionWF_VisibleChanged(object sender, EventArgs e)
        //{
        //    if (Visible) are.Set();
        //    else StartWaitingForClickFromOutside();
        //}
        //#endregion
    }
}
