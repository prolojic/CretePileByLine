using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;


namespace CretePileByLine
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class ExternalExecute : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIApplication App = commandData.Application;
            Document doc = App.ActiveUIDocument.Document;
            UIDocument uidoc = new UIDocument(doc);

            using (Transaction tx01 = new Transaction(doc))
            {
                tx01.Start("Family activate");
                    new GetColumnsByLines().GetColumnsCurve(doc);
                tx01.Commit();
            }
            
            return Result.Succeeded;

        }

    }
}



