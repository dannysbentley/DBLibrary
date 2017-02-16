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
    public class LibraryGetItems
    {
        ///  Author:   Danny Bentley
        ///  Date  :   09/23/2016
        ///  Objective : My Revit Library. Reusable Code. 
        
        #region GET ELEMENTS, INSTANCE, SYMBOLS AND PARAMETERS.
        LibraryMoving libaryMoving = new LibraryMoving();
        /// <summary>
        /// Get family instance from document using the Build In Category
        /// </summary>
        /// <param name="doc">Document</param>
        /// <param name="category">BuiltInCategory</param>
        /// <returns>List FamilyInstance</returns>
        public List<FamilyInstance> GetFamilyInstance(Document doc, BuiltInCategory category)
        {
            List<FamilyInstance> List_FamilyInstance = new List<FamilyInstance>();

            ElementClassFilter familyInstanceFilter = new ElementClassFilter(typeof(FamilyInstance));
            // Category filter 
            ElementCategoryFilter Categoryfilter = new ElementCategoryFilter(category);
            // Instance filter 
            LogicalAndFilter InstancesFilter = new LogicalAndFilter(familyInstanceFilter, Categoryfilter);

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            // Colletion Array of Elements
            ICollection<Element> Elements = collector.WherePasses(InstancesFilter).ToElements();

            foreach (Element e in Elements)
            {
                FamilyInstance familyInstance = e as FamilyInstance;

                if (null != familyInstance)
                {
                    try
                    {
                        List_FamilyInstance.Add(familyInstance);
                    }
                    catch (Exception ex)
                    {
                        string x = ex.Message;
                    }
                }
            }
            return List_FamilyInstance;
        }

        /// <summary>
        /// Get family Symbols from a name. 
        /// </summary>
        /// <param name="doc">Document</param>
        /// <param name="familyName">String</param>
        /// <param name="category">BuiltInCategory</param>
        /// <returns>FamilySymbol</returns>
        public FamilySymbol GetFamilySymbol(Document doc, String familyName, BuiltInCategory category)
        {

            var structuralFramingType = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).OfCategory(category);
            List<FamilySymbol> structuralFramingTypeList = structuralFramingType.ToElements().Cast<FamilySymbol>().ToList();

            FamilySymbol symbol = structuralFramingTypeList.Find(x => x.Name == familyName);

            return symbol;
        }

        /// <summary>
        /// Get a list of Family Symbols. 
        /// </summary>
        /// <param name="doc">Document</param>
        /// <param name="familyName">String</param>
        /// <param name="category">BuiltInCategory</param>
        /// <returns>List FamilySymbol</returns>
        public List<FamilySymbol> GetFamilySymbols(Document doc, String familyName, BuiltInCategory category)
        {

            var structuralFramingType = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).OfCategory(category);
            List<FamilySymbol> structuralFramingTypeList = structuralFramingType.ToElements().Cast<FamilySymbol>().ToList();

            return structuralFramingTypeList;
        }

        /// <summary>
        /// Get Element from document using the Build In Category
        /// </summary>
        /// <param name="doc">Document</param>
        /// <param name="category">BuiltInCategory</param>
        /// <returns>Element</returns>
        public List<Element> GetFamilyElement(Document doc, BuiltInCategory category)
        {
            List<Element> List_Element = new List<Element>();

            ElementClassFilter familyInstanceFilter = new ElementClassFilter(typeof(FamilyInstance));
            // Category filter 
            ElementCategoryFilter Categoryfilter = new ElementCategoryFilter(category);
            // Instance filter 
            LogicalAndFilter InstancesFilter = new LogicalAndFilter(familyInstanceFilter, Categoryfilter);

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            // Colletion Array of Elements
            ICollection<Element> Elements = collector.WherePasses(InstancesFilter).ToElements();

            foreach (Element e in Elements)
            {
                if (null != Elements)
                {
                    try
                    {
                        List_Element.Add(e);
                    }
                    catch (Exception ex)
                    {
                        string x = ex.Message;
                    }
                }
            }
            return List_Element;
        }


        /// <summary>
        /// Select and element from the Revit model. 
        /// </summary>
        /// <param name="uidoc">UIDocument</param>
        /// <param name="doc">Document</param>
        /// <returns>Element</returns>
        public Element SelectElement(UIDocument uidoc, Document doc)
        {
            Reference reference = uidoc.Selection.PickObject(ObjectType.Element);
            Element returnElement = uidoc.Document.GetElement(reference);
            return returnElement;
        }

        /// <summary>
        /// Get all levels 
        /// </summary>
        /// <param name="doc">Document</param>
        /// <returns>levels</returns>
        public List<Level> GetLevels(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<Element> levels = collector.OfClass(typeof(Level)).ToElements();
            List<Level> List_levels = new List<Level>();
            foreach (Level level in levels)
            {
                List_levels.Add(level);
            }
            return List_levels;
        }
        
        /// <summary>
        /// Get all floors and slabs from project. 
        /// </summary>
        /// <param name="doc">Document</param>
        /// <returns>List Floors</returns>
        public List<Floor> GetAllFloors(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<Element> floors = collector.OfClass(typeof(Floor)).ToElements();
            List<Floor> List_Floors = new List<Floor>();
            foreach (Floor floor in floors)
            {
                List_Floors.Add(floor);
            }
            return List_Floors;
        }

        /// <summary>
        /// Get workset from element 
        /// </summary>
        /// <param name="doc">Document</param>
        /// <param name="e">Element</param>
        /// <returns>string</returns>
        public string GetWorkset(Document doc, Element e)
        {
            Parameter wsparam = e.get_Parameter(BuiltInParameter.ELEM_PARTITION_PARAM);
            return GetParameterValue(wsparam);
        }

        /// <summary>
        /// Get all view from a project. 
        /// </summary>
        /// <param name="doc">Document</param>
        /// <returns>List View</returns>
        public List<View> GetAllViews(Document doc)
        {
            FilteredElementCollector viewcollector = new FilteredElementCollector(doc);
            viewcollector.OfClass(typeof(View));
            ICollection<Element> List_ViewElements = viewcollector.ToElements();
            List<View> List_Views = new List<View>();
            foreach (View v in List_ViewElements)
            {
                List_Views.Add(v);
            }
            return List_Views;
        }

        /// <summary>
        /// Get all viewports in project.
        /// </summary>
        /// <param name="doc">Document</param>
        /// <returns>List Viewport</returns>
        public List<Viewport> GetAllViewport(Document doc)
        {
            var filtercollector = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Viewports)
                .WhereElementIsNotElementType().ToElements();
            List<Viewport> List_Viewrports = new List<Viewport>();
            foreach (Element e in filtercollector)
            {
                Viewport vp = e as Viewport;
                List_Viewrports.Add(vp);
            }
            return List_Viewrports;
        }
        /// <summary>
        /// Find an element in the model by name 
        /// </summary>
        /// <param name="doc">Document</param>
        /// <param name="targetType">Type</param>
        /// <param name="targetName">string</param>
        /// <returns>element of searched by name</returns>
        public Element FindElementByName(Document doc, Type targetType, string targetName)
        {
            return new FilteredElementCollector(doc)
              .OfClass(targetType)
              .FirstOrDefault<Element>(
                e => e.Name.Equals(targetName));
        }

        /// <summary>
        /// Get all walls
        /// </summary>
        /// <param name="doc">Document</param>
        /// <returns>List Walls</returns>
        public List<Wall> GetWalls(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<Element> Walls = collector.OfClass(typeof(Wall)).ToElements();
            List<Wall> List_Walls = new List<Wall>();
            foreach (Wall w in Walls)
            {
                List_Walls.Add(w);
            }
            return List_Walls;
        }

        /// <summary>
        /// get paramter values
        /// </summary>
        /// <param name="parameter">Paramter</param>
        /// <returns>string</returns>
        public string GetParameterValue(Parameter parameter)
        {
            switch (parameter.StorageType)
            {
                case StorageType.Double:
                    //get value with unit, AsDouble() can get value without unit
                    return parameter.AsValueString();
                case StorageType.ElementId:
                    return parameter.AsElementId().IntegerValue.ToString();
                case StorageType.Integer:
                    //get value with unit, AsInteger() can get value without unit
                    return parameter.AsValueString();
                case StorageType.None:
                    return parameter.AsValueString();
                case StorageType.String:
                    return parameter.AsString();
                default:
                    return "";
            }
        }

        /// <summary>
        /// get parameter values
        /// </summary>
        /// <param name="parameter">Parameter</param>
        /// <returns>Object</returns>
        /// Parameter comment = dc.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);
        public Object GetParameterValueObject(Parameter parameter)
        {
            switch (parameter.StorageType)
            {
                case StorageType.Double:
                    //get value with unit, AsDouble() can get value without unit
                    return parameter.AsValueString();
                case StorageType.ElementId:
                    return parameter.AsElementId().IntegerValue.ToString();
                case StorageType.Integer:
                    //get value with unit, AsInteger() can get value without unit
                    return parameter.AsValueString();
                case StorageType.None:
                    return parameter.AsValueString();
                case StorageType.String:
                    return parameter.AsString();
                default:
                    return "";
            }
        }
        #endregion
    }
}