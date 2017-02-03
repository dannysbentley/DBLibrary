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
    public class LibraryGeometry
    {
        public const double _eps = 1.0e-9;

        public XYZ Midpoint(XYZ p, XYZ q)
        {
            return 0.5 * (p + q);
        }

        public XYZ Midpoint(Line line)
        {
            return Midpoint(line.GetEndPoint(0),
              line.GetEndPoint(1));
        }
        public bool IsZero(double a, double tolerance)
        {
            return tolerance > Math.Abs(a);
        }

        public bool IsZero(double a)
        {
            return IsZero(a, _eps);
        }

        public bool IsEqual(double a, double b)
        {
            return IsZero(b - a);
        }

        public int Compare(double a, double b)
        {
            return IsEqual(a, b) ? 0 : (a < b ? -1 : 1);
        }

        public int Compare(XYZ p, XYZ q)
        {
            int d = Compare(p.X, q.X);

            if (0 == d)
            {
                d = Compare(p.Y, q.Y);

                if (0 == d)
                {
                    d = Compare(p.Z, q.Z);
                }
            }
            return d;
        }

    }
}
