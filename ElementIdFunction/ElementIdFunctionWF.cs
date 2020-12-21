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

        public ElementIdFunctionWF(Command dataBuffer)
        {
            InitializeComponent();

            // Set the data source of the ListBox control
            functionListBox1.DataSource = dataBuffer.GetElParMet;
            functionListBox2.DataSource = dataBuffer.GetElParProp;
            functionlistBox3.DataSource = dataBuffer.GetFamilyPar;
            functionListBox4.DataSource = dataBuffer.Elements;
            familyTypeTestBox.Text = dataBuffer.GetFamilyType;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }     
    }
}
