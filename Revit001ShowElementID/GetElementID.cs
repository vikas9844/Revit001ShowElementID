using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;



namespace Revit001ShowElementID
{
    [Transaction(TransactionMode.ReadOnly)]
    public class GetElementID : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIDocument uiDoc = commandData.Application.ActiveUIDocument;
                Document doc = uiDoc.Document;

                IList<Reference> pickedObject = uiDoc.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element);

                StringBuilder allIDs = new StringBuilder();

                if (pickedObject.Count > 0)
                {
                    //Getting all selected elements
                    List<Element> selectedElements = new List<Element>();
                    foreach (Reference item in pickedObject)
                    {
                        selectedElements.Add(doc.GetElement(item));
                    }
                    
                    ////Getting all selected elementIDs                    
                    //foreach (Reference item in pickedObject)
                    //{
                    //    allIDs.Append(item.ElementId.ToString());
                    //    allIDs.Append("\n");
                    //}

                    //Collect Selected Object's Details
                    for(int i = 0; i<selectedElements.Count; i++)
                    {

                        ElementType eleType = doc.GetElement(selectedElements[i].GetTypeId()) as ElementType;
                        allIDs.Append (eleType.FamilyName + " " + selectedElements[i].Name + "[" + selectedElements[i].Id + "]\n");
                    }
                }

                TaskDialog.Show("Selected Items", allIDs.ToString());
                return Result.Succeeded;
            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }
            
        }
    }
}
