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
        private ArrayList arrayList;                          // Store the list of the parameters selected
        private ArrayList elementParWithMeth;                 // Store the list of the element parameters selected
        private ArrayList elementParWithProp;                 // Store the list of the element parameters selected
        private ArrayList familyPar;                          // Store the list of the element parameters selected
        private string familyType;                            // Store the string value of the family type

        #endregion


        #region class public property
        /// <summary>
        /// With the selected elements, export the list of all its id
        /// </summary>
        public ArrayList Elements
        {
            get
            {
                return arrayList;
            }
        }

        /// <summary>
        /// With the selected elements, export the list of all its id
        /// </summary>
        public ArrayList GetElParMet
        {
            get
            {
                return elementParWithMeth;
            }
        }

        /// <summary>
        /// With the selected elements, export the list of all its id
        /// </summary>
        public ArrayList GetElParProp
        {
            get
            {
                return elementParWithProp;
            }
        }

        /// <summary>
        /// With the selected elements, export the list of all its id
        /// </summary>
        public ArrayList GetFamilyPar
        {
            get
            {
                return familyPar;
            }
        }

        /// <summary>
        /// With the selected elements, export the list of all its id
        /// </summary>
        public string GetFamilyType
        {
            get
            {
                return familyType;
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
            arrayList = new ArrayList();
            elementParWithMeth = new ArrayList();
            elementParWithProp = new ArrayList();
            familyPar = new ArrayList();
        }
        #endregion


        #region Interface implementation
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
            Reference reference = GetPickObject(commandData.Application);
            if(reference == null) { return Result.Failed; }

            GetParametersWithMethod(commandData.Application, reference);
            GetParametersWithProperty(commandData.Application, reference);
            GetParametersOfFamily(commandData.Application, reference);
            GetDimensionsList(commandData.Application, reference);
            GetTypeParameterOfFamily(commandData.Application, reference);

            
            // Display the form
            ElementIdFunctionWF displayForm = new ElementIdFunctionWF(this);
            displayForm.Show();
            //displayForm.TopMost = true;

            return Result.Succeeded;
        }
        #endregion

        /// <summary>
        ///   La subroutine che cattura un singolo oggetto
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="uiapp">L'oggetto Applicazione di Revit</param>m>
        /// 
        private Reference GetPickObject(UIApplication uiapp)
        {
            // Get the selected view
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Selection choices = uidoc.Selection;

            // Get the single element
            Reference pickedObj = uidoc.Selection.PickObject(ObjectType.Element);

            if (pickedObj != null)
            {
                return pickedObj;
            }
            else
            {
                return null;
            }
        }        


        /// <summary>
        ///   La subroutine che cattura i parametri dell'ELEMENTO scelto con un METODO
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="uiapp">L'oggetto Applicazione di Revit</param>m>
        /// 
        private void GetParametersWithMethod(UIApplication uiapp, Reference reference)
        {
            // Chiamo la vista attiva e seleziono gli elementi che mi servono
            UIDocument uidoc = uiapp.ActiveUIDocument;
            ElementId eleId = reference.ElementId;
            Element ele = uidoc.Document.GetElement(eleId);
            elementParWithMeth = GetParamValuesFromMethod(ele);
        }

        /// <summary>
        /// Restituisce tutti i valori dei parametri ritenuti rilevanti per l'elemento dato sotto forma di ArrayList.
        /// </summary>
        private ArrayList GetParamValuesFromMethod(Element e)
        {
            // Two choices: 
            // Element.Parameters property -- Retrieves a set containing all the parameters.
            // GetOrderedParameters method -- Gets the visible parameters in order.

            //IList<Parameter> ps = e.Parameters as IList<Parameter>;
            IList<Parameter> ps = e.GetOrderedParameters();

            ArrayList param_values = new ArrayList(ps.Count);

            foreach (Parameter p in ps)
            {
                // AsValueString visualizza il valore così come lo vede l'utente. 
                // In alcuni casi, il valore del database sottostante 
                // restituito da AsInteger, AsDouble, ecc., potrebbe essere più rilevante.

                param_values.Add(string.Format("{0} - {1}", p.Definition.Name, p.AsValueString()));
            }

            // Ordina i parametri in ordine alfabetico

            // Dichiara una List<string> temporanea e la riempio 
            List<string> temp = new List<string>();
            foreach (var item in param_values)
            {
                temp.Add((string)item);
            }
            // Crea un IOrderedEnumerable per ordinare la List<string>
            var ordered = temp.OrderBy(x => x);
            // Dichiara e riempie una nuova ArrayList per il risultato finale
            ArrayList stringsResult = new ArrayList();
            foreach (var item in ordered)
            {
                stringsResult.Add(item);
            }

            // Ritorna l'ArrayList
            return stringsResult;
        }

        /// <summary>
        ///   La subroutine che cattura i parametri dell'ELEMENTO scelto con con una PROPRIETA
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="uiapp">L'oggetto Applicazione di Revit</param>m>
        /// 
        private void GetParametersWithProperty(UIApplication uiapp, Reference reference)
        {
            // Chiamo la vista attiva e seleziono gli elementi che mi servono
            UIDocument uidoc = uiapp.ActiveUIDocument;
            ElementId eleId = reference.ElementId;
            Element ele = uidoc.Document.GetElement(eleId);
            familyPar = GetParamValuesFromProperty(ele);
        }

        /// <summary>
        /// Restituisce tutti i valori dei parametri ritenuti rilevanti per l'elemento dato sotto forma di ArrayList.
        /// </summary>
        private ArrayList GetParamValuesFromProperty(Element e)
        {
            // Two choices: 
            // Element.Parameters property -- Retrieves a set containing all the parameters.
            // GetOrderedParameters method -- Gets the visible parameters in order.

            ParameterSet ps = e.Parameters;
            //IList<Parameter> ps = e.GetOrderedParameters();

            ArrayList param_values = new ArrayList(ps.Size);

            foreach (Parameter p in ps)
            {
                // AsValueString displays the value as the 
                // user sees it. In some cases, the underlying
                // database value returned by AsInteger, AsDouble,
                // etc., may be more relevant.
                // AsValueString visualizza il valore così come lo vede l'utente. 
                // In alcuni casi, il valore del database sottostante 
                // restituito da AsInteger, AsDouble, ecc., potrebbe essere più rilevante.

                param_values.Add(string.Format("{0} -- {1}", p.Definition.Name, p.AsValueString()));
            }

            // Ordina i parametri in ordine alfabetico

            // Dichiara una List<string> temporanea e la riempio 
            List<string> temp = new List<string>();
            foreach (var item in param_values)
            {
                temp.Add((string)item);
            }
            // Crea un IOrderedEnumerable per ordinare la List<string>
            var ordered = temp.OrderBy(x => x);
            // Dichiara e riempie una nuova ArrayList per il risultato finale
            ArrayList stringsResult = new ArrayList();
            foreach (var item in ordered)
            {
                stringsResult.Add(item);
            }

            // Ritorna l'ArrayList
            return stringsResult;
        }

        /// <summary>
        ///   La subroutine che cattura i parametri della FINESTRA relativa all'elemento scelto
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="uiapp">L'oggetto Applicazione di Revit</param>m>
        /// 
        private void GetParametersOfFamily(UIApplication uiapp, Reference reference)
        {
            // Chiamo la vista attiva e seleziono gli elementi che mi servono
            UIDocument uidoc = uiapp.ActiveUIDocument;
            ElementId eleId = reference.ElementId;
            Element ele = uidoc.Document.GetElement(eleId);
            elementParWithProp = GetParametersElementType(uiapp, ele);
        }
        
        /// <summary>
        /// Restituisce tutti i valori dei parametri ritenuti rilevanti per l'elemento dato sotto forma di ArrayList.
        /// </summary>
        private ArrayList GetParametersElementType(UIApplication uiapp, Element e)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            //ParameterSet parameters = e.Parameters;
            ElementType eleType = doc.GetElement(e.GetTypeId()) as ElementType;
            ParameterSet parameters = eleType.Parameters;

            ArrayList arr = new ArrayList();
            foreach (Parameter param in parameters)
            {
                 arr.Add(String.Format("{0} --- {1}\n", param.Definition.Name, param.AsValueString()));
            }

            // Ordina i parametri in ordine alfabetico

            // Dichiara una List<string> temporanea e la riempio 
            List<string> temp = new List<string>();
            foreach (var item in arr)
            {
                temp.Add((string)item);
            }
            // Crea un IOrderedEnumerable per ordinare la List<string>
            var ordered = temp.OrderBy(x => x);
            // Dichiara e riempie una nuova ArrayList per il risultato finale
            ArrayList stringsResult = new ArrayList();
            foreach (var item in ordered)
            {
                stringsResult.Add(item);
            }

            return stringsResult;
        }

        /// <summary>
        ///   La subroutine che cattura i parametri dimensionali dell'oggetto selezionato
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="uiapp">L'oggetto Applicazione di Revit</param>m>
        /// 
        private void GetDimensionsList(UIApplication uiapp, Reference reference)
        {
            // Chiamo la vista attiva e seleziono gli elementi che mi servono
            UIDocument uidoc = uiapp.ActiveUIDocument;
            ElementId eleId = reference.ElementId;
            Element element = uidoc.Document.GetElement(eleId);

            // GetOrderedParameters method -- Ottiene i parametri visibili in ordine.
            //IList<Parameter> parIList = element.GetOrderedParameters();

            // Ottiene tutti i Paramtetri contenuti nei singoli Gruppi di Paramtetri
            Dictionary<BuiltInParameterGroup, List<BuiltInParameter>> dict = GroupBuiltInParameters(element);
            // Estrae i parametri che appartengono a Dimensions
            List<Parameter> pList = ParametersInGroup(element, BuiltInParameterGroup.PG_GEOMETRY);
            // Li ordina in modo crescente
            List<Parameter> pListOrdered = pList.OrderBy(x => x.Definition.Name).ToList();

            // Lista dei nomi dei parametri contenuti nella Gruppo Dimensioni
            //List<string> parametriDimensionali = new List<string> {
            //        "Lunghezza",
            //        "Area",
            //        "Volume",
            //        "CellH",
            //        "CellH2",
            //        "CellL",
            //        "CellDxH",
            //        "CellSxH"
            //    };

            // Se i parametri dimensionali sono presenti, ricava i loro valori e li aggiunge alla lista, 
            // altrimenti scrive una stringa vuota
            string ctrl = "";
            foreach (Parameter par in pListOrdered)
            {
                // se il nome del parametro è già presente o uguale a BOLD_Distinta, salta a quello dopo
                if (par.Definition.Name != ctrl && par.Definition.Name != "BOLD_Distinta")
                {
                    arrayList.Add(par.Definition.Name + ":");
                    if (par.AsValueString() == null)
                    {
                        arrayList.Add("-----");
                    }
                    else if(par.Definition.Name =="Area")
                    {
                        double MyString = par.AsDouble();
                        double newvalueMyString = UnitUtils.ConvertFromInternalUnits(MyString, DisplayUnitType.DUT_SQUARE_METERS);
                        arrayList.Add(newvalueMyString + " m^2");
                    }
                    else
                    {
                        arrayList.Add(par.AsValueString());
                    }
                    arrayList.Add("");

                    ctrl = par.Definition.Name;
                }
                else { continue; } 
            }

            //foreach (Parameter par in pList)
            //{
            //    foreach (string str in parametriDimensionali)
            //    {
            //        if (par.Definition.Name == str)
            //        {
            //            arrayList.Add(par.Definition.Name + ":");
            //    if (par.AsValueString() == null)
            //    {
            //        arrayList.Add("-----");
            //    }
            //    else
            //    {
            //        arrayList.Add(par.AsValueString());
            //    }
            //    arrayList.Add("");
            //}
        }          

        public static Dictionary<BuiltInParameterGroup, List<BuiltInParameter>> GroupBuiltInParameters(Element e)
        {
            Dictionary<BuiltInParameterGroup, List<BuiltInParameter>> dict =
                new Dictionary<BuiltInParameterGroup, List<BuiltInParameter>>();

            foreach (Parameter p in e.Parameters)
            {
                if (p.IsShared)
                    continue;

                if (p.Definition == null)
                    break;

                if (!dict.ContainsKey(p.Definition.ParameterGroup))
                {
                    dict.Add(p.Definition.ParameterGroup, new List<BuiltInParameter>());
                }

                BuiltInParameter biParam = (p.Definition as InternalDefinition).BuiltInParameter;
                if (!dict[p.Definition.ParameterGroup].Contains(biParam))
                {
                    dict[p.Definition.ParameterGroup].Add(biParam);
                }
            }
            return dict;
        }

        public static List<Parameter> ParametersInGroup(Element e, BuiltInParameterGroup g)
        {
            Dictionary<BuiltInParameterGroup, List<Parameter>> groupDict = GroupParameters(e);
            return groupDict.Keys.Contains(g) ? groupDict[g] : null;
        }

        public static Dictionary<BuiltInParameterGroup, List<Parameter>> GroupParameters(Element e)
        {
            Dictionary<BuiltInParameterGroup, List<Parameter>> dict =
                new Dictionary<BuiltInParameterGroup, List<Parameter>>();

            foreach (Parameter p in e.Parameters)
            {
                if (!dict.ContainsKey(p.Definition.ParameterGroup))
                {
                    dict.Add(p.Definition.ParameterGroup, new List<Parameter>());
                }

                dict[p.Definition.ParameterGroup].Add(p);
            }

            return dict;
        }



        /// <summary>
        ///   La subroutine che cattura il parametro TIPO della FAMIGLIA scelta in formato stringa
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="uiapp">L'oggetto Applicazione di Revit</param>m>
        /// 
        private void GetTypeParameterOfFamily(UIApplication uiapp, Reference reference)
        {
            // Chiamo la vista attiva e seleziono gli elementi che mi servono
            UIDocument uidoc = uiapp.ActiveUIDocument;
            ElementId eleId = reference.ElementId;
            Element ele = uidoc.Document.GetElement(eleId);
            familyType = GetTypeParameterElementType(ele);
        }

        /// <summary>
        /// Restituisce tutti i valori dei parametri ritenuti rilevanti per l'elemento dato sotto forma di ArrayList.
        /// </summary>
        private string GetTypeParameterElementType(Element e)
        {
            ParameterSet ps = e.Parameters;

            string singleString = null;
            foreach (Parameter param in ps)
            {
                if(param.Definition.Name == "Tipo")
                    singleString = param.AsValueString();
            }  
            return singleString;        }
    }
}
