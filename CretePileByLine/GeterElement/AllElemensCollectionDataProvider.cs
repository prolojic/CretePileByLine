using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CretePileByLine.GeterElement
{
    internal class AllElemensCollectionDataProvider
    {
        public ICollection<ElementId> GetCollection(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<ElementId> ElemenIdsCollection = collector.WhereElementIsNotElementType().ToElementIds();

            return ElemenIdsCollection;
        }
    }
}
