#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Structure;
#endregion

namespace DBLibrary
{
    public class LibraryCreate
    {
        public void CreateDetailLine(Document doc ,XYZ p1, XYZ p2)
        {
            using (Transaction t = new Transaction(doc, "Create detail Line"))
            {
                t.Start("Create Line");
                Line line = Line.CreateBound(p1, p2);
                doc.Create.NewDetailCurve(doc.ActiveView, line);
                t.Commit();
            }
        }

        public void CreateDetailLine(Document doc, Line line)
        {
            using (Transaction t = new Transaction(doc, "Create detail Line"))
            {
                t.Start("Create Line");
                doc.Create.NewDetailCurve(doc.ActiveView, line);
                t.Commit();
            }
        }
    }
}
