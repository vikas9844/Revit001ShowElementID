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
                IList<Reference> pickedObject = uiDoc.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element);

                StringBuilder allIDs = new StringBuilder();

                if (pickedObject.Count > 0)
                {
                    foreach(Reference item in pickedObject)
                    {
                        allIDs.Append(item.ElementId.ToString());
                        allIDs.Append("\n");
                    }
                }

                TaskDialog.Show("Element ID", allIDs.ToString());
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
