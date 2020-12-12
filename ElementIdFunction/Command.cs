﻿using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElementIdFunction
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        #region Private data members
        Element m_slab = null;      // Store the selected element
        private ArrayList elementsList;                 // Store the list of the elements selected
        private string _prova;
        #endregion


        #region class public property
        /// <summary>
        /// With the selected elements, export the list of all its id
        /// </summary>
        public string Element
        {
            get
            {
                return _prova;
            }
        }
        #endregion


        #region class public method
        /// <summary>
        /// Default constructor of Command
        /// </summary>
        public Command()
        {
            // Construct the data members for the property
            _prova = Element;
        }
        #endregion


        #region Interface implemetation
        /// <summary>
        /// Implement this method as an external command for Revit.
        /// </summary>
        /// <param name="commandData">An object that is passed to the external application 
        /// which contains data related to the command, 
        /// such as the application object and active view.</param>
        /// <param name="message">A message that can be set by the external application 
        /// which will be displayed if a failure or cancellation is returned by 
        /// the external command.</param>
        /// <param name="elements">A set of elements to which the external application 
        /// can add elements that are to be highlighted in case of failure or cancellation.</param>
        /// <returns>Return the status of the external command. 
        /// A result of Succeeded means that the API external method functioned as expected. 
        /// Cancelled can be used to signify that the user cancelled the external operation 
        /// at some point. Failure should be returned if the application is unable to proceed with 
        /// the operation.</returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            string ciao = GetPickObject(commandData.Application);
            MessageBox.Show(ciao, "Parameter : ");

            // Display them in a form
            ElementIdFunctionWF displayForm = new ElementIdFunctionWF(this);
            displayForm.Show();
            displayForm.TopMost = true;

            return Result.Succeeded;
        }
        #endregion

        public string GetPickObject(UIApplication uiapp)
        {
            string risultato = null; 

            // Get the selected view
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Selection choices = uidoc.Selection;

            Reference pickedObj = uidoc.Selection.PickObject(ObjectType.Element);
            ElementId eleId = pickedObj.ElementId;
            Element ele = uidoc.Document.GetElement(eleId);

            if (pickedObj != null)
            {
                Parameter pardistinta = ele.LookupParameter("BOLD_Distinta");
                return risultato = pardistinta.AsString();
            }
            else
            {
                return risultato = "Non hai selezionato nulla";
            }
        }
    }
}
