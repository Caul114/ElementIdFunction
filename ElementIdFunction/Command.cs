using Autodesk.Revit.ApplicationServices;
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


namespace ElementIdFunction
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        #region Private data members
        Autodesk.Revit.DB.Element m_slab = null;      // Store the selected element
        private ArrayList elementsList;               // Store the list of the elements selected
        #endregion


        #region class public property
        /// <summary>
        /// With the selected elements, export the list of all its id
        /// </summary>
        public ArrayList Elements
        {
            get
            {
                return elementsList;
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
            elementsList = new ArrayList();
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
            UIApplication revit = commandData.Application;

            // Get the selected view
            UIDocument uidoc = revit.ActiveUIDocument;
            Selection choices = uidoc.Selection;
            ElementSet collection = new ElementSet();
            foreach (ElementId elementId in choices.GetElementIds())
            {
                collection.Insert(uidoc.Document.GetElement(elementId));
            }

            // If the item is not selected, it prompts you to select at least one
            foreach (Element e in collection)
            {
                m_slab = e as Element;
                if (null == m_slab)
                {
                    message = "Please select an element.";
                    return Autodesk.Revit.UI.Result.Failed;
                }
            }

            // Get all elements id
            foreach (Element e in collection)
            {
                // With the element selected, judge if the Id of the element exists, 
                // if it doesn't exist, it should be zero.                
                if (e.Id == null)
                {
                    elementsList.Add("No element");
                }
                else
                {
                    elementsList.Add(e.Id.ToString());
                }

            }

            // Display them in a form
            ElementIdFunctionWF displayForm = new ElementIdFunctionWF(this);
            displayForm.ShowDialog();

            return Autodesk.Revit.UI.Result.Succeeded;
        }
        #endregion
    }
}
