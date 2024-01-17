using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using StructuralType = Autodesk.Revit.DB.Structure.StructuralType;
using Group = Autodesk.Revit.DB.Group;
using CretePileByLine.GeterElement;

namespace CretePileByLine
{
    public class GetColumnsByLines
    {
        List<Element> DeletElement = new List<Element>();

        public void GetColumnsCurve(Document doc)
        {
            View view = doc.ActiveView;
            Level elementLevel = new ElemensCollectionDataProvider().GetCollection(doc, BuiltInCategory.OST_Levels).Where(v => v.LookupParameter("Фасад").AsDouble() == 0).FirstOrDefault() as Level;
            FamilySymbol pile = new ElemensTypeCollectionDataProvider().GetCollection(doc, BuiltInCategory.OST_StructuralFoundation).Where(v => v.Name.Contains("С 120.30-8")).FirstOrDefault() as FamilySymbol;
            pile.Activate();
            string[] pileName= new string[] { "K020", "сваи", "K151" };
            List<Element> lineArray = new ElemensCollectionDataProvider().GetCollection(doc, BuiltInCategory.OST_Lines).Where(v => pileName.Contains(v.GetParameters("Стиль линий")[0].AsValueString())).ToList();
           
            double distance1 = 0;
           
            for (int i = 0; i < lineArray.Count; i++)
            {
                LocationCurve location0 = lineArray[i].Location as LocationCurve;
                Line line0 = location0.Curve as Line;
                List<ElementId> elIdList = new List<ElementId>();
                elIdList.Add(lineArray[i].Id);
                for (int j = 1; j < lineArray.Count; j++)
                {
                    LocationCurve location = lineArray[j].Location as LocationCurve;
                    Line line1 = location.Curve as Line;
                    distance1 = line0.Distance(line1.Origin);
                    if (distance1 < 550 / 304.8 && lineArray[j].GroupId.IntegerValue==-1 )
                    {
                        elIdList.Add(lineArray[j].Id); 
                    }

                }
                if (elIdList.Count > 3)
                {
                    Group group = doc.Create.NewGroup(elIdList.ToArray());
                    LocationPoint point = group.Location as LocationPoint;
                    FamilyInstance newOpening = doc.Create.NewFamilyInstance(point.Point, pile, line0.Direction, elementLevel, StructuralType.NonStructural);
                    newOpening.LookupParameter("Смещение от уровня").Set(-2000 / 304.8);
                }
            }
        }
    }
}
