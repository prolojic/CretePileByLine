using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CretePileByLine.GeterElement
{
    internal class ElemensOfClassDataProvider
    {
        public ICollection<Element> GetCollection(Document doc, Type type)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<Element> ElemensCollection = collector.OfClass(type).WhereElementIsNotElementType().ToElements();

            return ElemensCollection;
        }
    }
}
