#region License
/*
LangLib

A program and library for application localization.
Copyright (C) 2015 VPKSoft, Petteri Kautonen

Contact: vpksoft@vpksoft.net

This file is part of LangLib.

LangLib is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

LangLib is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with LangLib.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections;
using System.Windows.Forms;
using System.Reflection;
using System.Windows;
using System.ComponentModel;
using System.Security.Cryptography;


namespace VPKSoft.LangLib
{
    /// <summary>
    /// A class to enumerate given form / window
    /// items / properties for localization.
    /// </summary>
    public class GuiObjectsEnum : IEnumerable
    {
        /// <summary>
        /// Internal list for the GuiObject class instances.
        /// </summary>
        private List<GuiObject> wfObjects = new List<GuiObject>();

        /// <summary>
        /// Internal list of name spaces to use to
        /// <para/>separate which object to enumerate.
        /// </summary>
        private List<string> nameSpaces = new List<string>();

        /// <summary>
        /// A list of denied objects which should not be enumerated.
        /// </summary>
        public List<string> DeniedObjectNames = new List<string>(new string[] {"Name", "ImageKey", "WindowText", "Uid", "SelectedText"});

        /// <summary>
        /// A place holder for a System.Windows.Forms.Form
        /// <para/>class instance.
        /// </summary>
        protected System.Windows.Forms.Form thisForm = null;

        /// <summary>
        /// A place holder for a System.Windows.Window
        /// <para/>class instance.
        /// </summary>
        protected System.Windows.Window thisWindow = null;


        /// <summary>
        /// The class constructor based on a 
        /// <para/>System.Windows.Forms.Form class instance.
        /// </summary>
        /// <param name="form">An instance of System.Windows.Forms.Form class.</param>
        public GuiObjectsEnum(System.Windows.Forms.Form form)
        {
            InitNameSpaces();
            thisForm = form;
        }

        /// <summary>
        /// Whether to use x:Uid's to reference to a FrameworkElement class instance.
        /// </summary>
        private bool useUids = true;

        /// <summary>
        /// The class constructor based on a 
        /// <para/>System.Windows.Window class instance.
        /// </summary>
        /// <param name="window">An instance of System.Windows.Window class.</param>
        /// <param name="useUids">Whether to use x:Uid's to reference to a FrameworkElement class instance.</param>
        public GuiObjectsEnum(System.Windows.Window window, bool useUids = true)
        {
            InitNameSpaces();
            thisWindow = window;
        }

        /// <summary>
        /// Initializes the default name spaces
        /// allowed for object enumeration.
        /// </summary>
        public void InitNameSpaces()
        {
            nameSpaces.Add("System.Windows.Forms");
            nameSpaces.Add("System.Windows.Controls.*");
        }

        /// <summary>
        /// This exception is thrown if this class 
        /// is initialized with "nameless" form / window.
        /// </summary>
        public class NamelessWindowException: Exception
        {
            /// <summary>
            /// The NamelessWindowException constructor.
            /// </summary>
            public NamelessWindowException():
                base("Window has no name.")
            {

            }
        }

        /// <summary>
        /// Gets the name of the form / window this
        /// <para/>class was initialized with.
        /// </summary>
        public string BaseInstanceName
        {
            get
            {
                if (thisWindow != null)
                {
                    if (thisWindow.Name == string.Empty && thisWindow.Uid == string.Empty) // Added x:Uid support
                    {
                        throw new NamelessWindowException();
                    }

                    return thisWindow.Uid == string.Empty ? thisWindow.Name : thisWindow.Uid;
                }
                else if (thisForm != null)
                {
                    if (thisForm.Name == string.Empty)
                    {
                        throw new NamelessWindowException();
                    }
                    return thisForm.Name;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets a value indication if the program is in
        /// design mode. 
        /// </summary>
        public bool Design
        {
            get
            {
                if (thisWindow != null)
                {                    
                    return  (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
                }
                else if (thisForm != null)
                {
                    return (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets the type of the window / form this
        /// <para/>class instance was initialized with.
        /// </summary>
        public Type BaseInstanceType
        {
            get
            {
                return BaseInstance.GetType();
            }
        }

        /// <summary>
        /// Adds a new GuiObject class instance to the internal list.
        /// </summary>
        /// <param name="go">A GuiObject class instance.</param>
        public void AddGuiObject(GuiObject go)
        {
            wfObjects.Add(go);
        }

        /// <summary>
        /// Gets the window / form this
        /// <para/>class instance was initialized with.
        /// </summary>
        public object BaseInstance
        {
            get
            {
                if (thisWindow != null)
                {
                    return thisWindow;
                }
                else if (thisForm != null)
                {
                    return thisForm;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the applications product name.
        /// </summary>
        public string BaseInstanceProduct
        {
            get
            {
                if (thisWindow != null)
                {
                    return System.Windows.Application.ResourceAssembly.GetName().Name;
                }
                else if (thisForm != null)
                {
                    return System.Windows.Forms.Application.ProductName;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets the applications product name combined 
        /// <para/>with the window / form name this
        /// <para/>class instance was initialized with (e.g. "TestApp.Form1").
        /// </summary>
        public string AppForm
        {
            get
            {
                return BaseInstanceProduct + "." + BaseInstanceName;
            }
        }

        /// <summary>
        /// Clears the object cache if the <paramref name="clear"/> is
        /// <para/>as true.
        /// </summary>
        /// <param name="clear">Whether to clear the object cache or not.</param>
        public void Clear(bool clear = true)
        {
            if (clear)
            {
                wfObjects.Clear();
            }
        }

        /// <summary>
        /// Adds a new allowed name spaces for
        /// <para/>object enumeration.
        /// <para/><para/>The default name spaces are
        /// <para/>System.Windows.Forms and
        /// <para/>System.Windows.Controls.*, so wild cards
        /// <para/>are allowed at the end of the name space name.
        /// </summary>
        /// <param name="nameSpace">A names space name to add to the allowed name space list.</param>
        public void AddNameSpace(string nameSpace)
        {
            if (!nameSpaces.Contains(nameSpace))
            {
                nameSpaces.Add(nameSpace);
            }
        }

        /// <summary>
        /// Returns the list of allowed name spaces for
        /// <para/>object enumeration.
        /// </summary>
        public List<string> NameSpaces
        {
            get
            {
                return nameSpaces;
            }
        }

        /// <summary>
        /// Clears the list of allowed name spaces for
        /// <para/>object enumeration.
        /// </summary>
        public void ClearNamespaces()
        {
            nameSpaces.Clear();
        }

        /// <summary>
        /// Gets the items and properties of a given form / window
        /// <para/>based on the allowed name spaces and allowed type lists.
        /// </summary>
        /// <param name="form">An instance of System.Windows.Forms.Form class
        /// <para/>which items / properties to get.</param>
        /// <param name="culture">A culture to "mark" the internal object list.</param>
        /// <param name="clear">Whether to clear previously enumerated objects or not.</param>
        /// <param name="propertyNames">A names of the properties to include in the object list.
        /// <para/>If the value is null, no property names are prevented.</param>
        public void GetObjects(System.Windows.Forms.Form form, CultureInfo culture, bool clear, List<string> propertyNames = null)
        {
            FieldInfo[] fieldInfos = form.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            Clear(clear);
            ListObjects(System.Windows.Forms.Application.ProductName, form.Name, ref fieldInfos, form, culture, propertyNames);
        }

        /// <summary>
        /// Gets all FrameworkElement class instance children of a given 
        /// <para/>FrameworkElement class instance recursively, which have an assigned x:Uid.
        /// </summary>
        /// <param name="element">A class instance which is or is inherited from a FrameworkElement class.</param>
        /// <returns>A list of FrameworkElement class instances with an assigned x:Uid.</returns>
        public static List<FrameworkElement> GetUidElements(FrameworkElement element)
        {
            List<FrameworkElement> elements = new List<FrameworkElement>();
            foreach (var e in LogicalTreeHelper.GetChildren(element))
            {
                if (e is FrameworkElement)
                {
                    FrameworkElement el = (FrameworkElement)e;
                    if (el.Uid != string.Empty)
                    {
                        elements.Add(el);
                    }
                    elements.AddRange(GetUidElements(el));
                }
            }
            return elements;
        }

        /// <summary>
        /// Gets the items and properties of a given form / window
        /// <para/>based on the allowed name spaces and allowed type lists.
        /// </summary>
        /// <param name="window">An instance of System.Windows.Window class
        /// <para/>which items / properties to get.</param>
        /// <param name="culture">A culture to "mark" the internal object list.</param>
        /// <param name="clear">Whether to clear previously enumerated objects or not.</param>
        /// <param name="propertyNames">A names of the properties to include in the object list.
        /// <para/>If the value is null, no property names are prevented.</param>
        public void GetObjects(System.Windows.Window window, CultureInfo culture, bool clear, List<string> propertyNames = null)
        {
            FieldInfo[] fieldInfos = window.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            Clear(clear);
            ListObjects(System.Windows.Application.ResourceAssembly.GetName().Name, window.Name, ref fieldInfos, window, culture, propertyNames);
        }
        
        /// <summary>
        /// Checks if the given name space name matches
        /// <para/>the classes internal list.
        /// </summary>
        /// <param name="nameSpace">The name of the name space to the
        /// <para/>internal name space list.</param>
        /// <returns>True if the given name space name matches the
        /// <para/>the internal name space list, otherwise false.</returns>
        private bool NameSpaceMatch(string nameSpace)
        {
            foreach (string str in nameSpaces)
            {
                string ns = str;
                if (ns.EndsWith(".*"))
                {
                    ns = ns.Substring(0, ns.Length - 2);
                }
                if (nameSpace.StartsWith(ns) || nameSpace == ns)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Sets the items and properties of a given object
        /// <para/>based on the allowed name spaces and allowed type lists.
        /// </summary>
        /// <param name="baseObject">The base object which to enumerate.</param>
        /// <param name="item">A name of the object which named value to set.</param>
        /// <param name="valueName">A value name of which value to set.</param>
        /// <param name="value">A value to set if found by item and valueName.</param>
        public void SetObject(object baseObject, string item, string valueName, object value)
        {
            string name = GetObjectName(GetNameProps(baseObject), baseObject);
            if (name == "Name" || name == string.Empty)
            {
                return;
            }

            FieldInfo[] infos = baseObject.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            
            PropertyInfo[] piArr = baseObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            HandlePropertiesSet(piArr, baseObject, item, valueName, value, name);

            foreach (FieldInfo info in infos)
            {
                if (!NameSpaceMatch(info.FieldType.Namespace))
                {
                    continue;
                }
                object obj = info.GetValue(baseObject) as object;// baseObject.GetType().GetField(info.Name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(baseObject) as object;
                if (obj == null)
                {
                    continue;
                }
                name = GetObjectName(GetNameProps(obj), obj);
                if (name == "Name" || name == string.Empty)
                {
                    return;
                }

                piArr = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                HandlePropertiesSet(piArr, obj, item, valueName, value, name);
            }
        }

        /// <summary>
        /// Handles the internal logic of the SetObject method.
        /// </summary>
        /// <param name="piArr">An array of PropertyInfo class instances.</param>
        /// <param name="obj">The object to enumerate.</param>
        /// <param name="item">A name of the object which named value to set.</param>
        /// <param name="valueName">A value name of which value to set.</param>
        /// <param name="value">A value to set if found by item and valueName.</param>
        /// <param name="name">A name of the object which value(s) to set.</param>
        private void HandlePropertiesSet(PropertyInfo[] piArr, object obj, string item, string valueName, object value, string name)
        {
            if (item != name)
            {
                return;
            }
            foreach (PropertyInfo pi in piArr)
            {
                if (!pi.CanWrite)
                {
                    continue;
                }

                try
                {
                    if (pi.PropertyType != typeof(string))
                    {
                        continue;
                    }

                    object val = pi.GetValue(obj);
                    if (DeniedObjectNames.Contains(pi.Name))
                    {
                        continue;
                    }

                    if (pi.Name == valueName)
                    {
                        pi.SetValue(obj, value);
                        return;
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Gets the items and properties of a object
        /// <para/>based on the allowed name spaces and allowed type lists.
        /// </summary>
        /// <param name="productName">The application product name.</param>
        /// <param name="formWindowName">The name of the form / window,
        /// <para/>which objects to get.</param>
        /// <param name="infos">An array of FieldInfo class instances belonging to the base object.</param>
        /// <param name="baseObject">The base object (usually a form / window).</param>
        /// <param name="culture">A culture to "mark" the internal object list.</param>
        /// <param name="propertyNames">A names of the properties to include in the object list.
        /// <para/>If the value is null, no property names are prevented.</param>
        private void ListObjects(string productName, string formWindowName, ref FieldInfo[] infos, object baseObject, CultureInfo culture, List<string> propertyNames = null)
        {
            string name = GetObjectName(GetNameProps(baseObject), baseObject);
            if (name == "Name" || name == string.Empty)
            {
                return;
            }
            PropertyInfo[] piArr = baseObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            HandlePropertiesGet(piArr, baseObject, productName, formWindowName, culture, name, propertyNames);

            foreach (FieldInfo info in infos)
            {
                if (!NameSpaceMatch(info.FieldType.Namespace))
                {
                    continue;
                }
                object obj = info.GetValue(baseObject) as object;// baseObject.GetType().GetField(info.Name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(baseObject) as object;
                if (obj == null)
                {
                    continue;
                }
                name = GetObjectName(GetNameProps(obj), obj);
                if (name == "Name" || name == string.Empty)
                {
                    continue;
                }
                piArr = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                HandlePropertiesGet(piArr, obj, productName, formWindowName, culture, name, propertyNames);
            }

            if (baseObject is Window && useUids) // x:Uid support added
            {
                ListObjects(productName, formWindowName, GetUidElements((Window)baseObject), culture, propertyNames);
            }
        }

        /// <summary>
        /// Gets the items and properties of a object
        /// <para/>based on the allowed name spaces and allowed type lists.
        /// </summary>
        /// <param name="productName">The application product name.</param>
        /// <param name="formWindowName">The name of the form / window,
        /// <para/>which objects to get.</param>
        /// <param name="elements">A list of FrameworkElement or it's descendant class instances.</param>
        /// <param name="culture">A culture to "mark" the internal object list.</param>
        /// <param name="propertyNames">A names of the properties to include in the object list.
        /// <para/>If the value is null, no property names are prevented.</param>
        private void ListObjects(string productName, string formWindowName, List<FrameworkElement> elements, CultureInfo culture, List<string> propertyNames = null) // x:Uid support added
        {
            string name;
            PropertyInfo[] piArr;

            foreach (FrameworkElement element in elements)
            {
                if (!NameSpaceMatch(element.GetType().GetTypeInfo().Namespace))
                {
                    continue;
                }

                object obj = element;
                if (obj == null)
                {
                    continue;
                }
                name = GetObjectName(GetNameProps(obj), obj);
                if (name == "Name" || name == string.Empty)
                {
                    continue;
                }
                piArr = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                HandlePropertiesGet(piArr, obj, productName, formWindowName, culture, name, propertyNames);
            }
        }

        /// <summary>
        /// Gets the properties that can be associated with the object name
        /// <para/>such as in Tag and Name.
        /// </summary>
        /// <param name="obj">An instance of an object which properties to get.</param>
        /// <returns>A list of PropertyInfo instances.</returns>
        private List<PropertyInfo> GetNameProps(object obj)
        {
            List<PropertyInfo> piArrSmall = new List<PropertyInfo>();
            try
            {
                piArrSmall.Add(obj.GetType().GetProperty("Tag"));
            }
            catch
            {

            }

            try
            {
                piArrSmall.Add(obj.GetType().GetProperty("Name"));
            }
            catch
            {

            }

            try
            {
                piArrSmall.Add(obj.GetType().GetProperty("Uid"));
            }
            catch
            {

            }
            return piArrSmall;
        }

        /// <summary>
        /// Handles the internal logic of the ListObjects method.
        /// </summary>
        /// <param name="piArr">An array of PropertyInfo class instances.</param>
        /// <param name="obj">The object to enumerate.</param>
        /// <param name="productName">The application product name.</param>
        /// <param name="formWindowName">The name of the form / window,
        /// <para/>which objects to set.</param>
        /// <param name="culture">A culture to "mark" the internal object list.</param>
        /// <param name="name">A name of the object which value(s) to get.</param>
        /// <param name="propertyNames">A names of the properties to include in the object list.
        /// <para/>If the value is null, no property names are prevented.</param>
        private void HandlePropertiesGet(PropertyInfo[] piArr, object obj, string productName, string formWindowName, CultureInfo culture, string name, List<string> propertyNames = null)
        {
            foreach (PropertyInfo pi in piArr)
            {
                if (!pi.CanWrite)
                {
                    continue;
                }

                try
                {
                    if (pi.PropertyType != typeof(string) && pi.PropertyType != typeof(object))
                    {
                        continue;
                    }

                    if (DeniedObjectNames.Contains(pi.Name))
                    {
                        continue;
                    }

                    string item = name;
                    if (propertyNames != null && !propertyNames.Contains(item + "." + pi.Name))
                    {
                        continue;
                    }

                    object val = pi.GetValue(obj);
                    if (val == null)
                    {
                        continue;
                    }

                    if (pi.PropertyType == typeof(object) && val.GetType() != typeof(string))
                    {
                        continue;
                    }

                    string appForm = productName + "." + formWindowName;
                    string propertyName = pi.Name;

                    object propertyValue = val.ToString();
                    if (item == string.Empty)
                    {
                        continue;
                    }
                    wfObjects.Add(new GuiObject(appForm, item, culture, propertyName, val.GetType().ToString(), propertyValue, true, obj));
                }
                catch
                {
                }
            }
        }


        /// <summary>
        /// A helper function to get all types of component type names in
        /// <para/>a window or a form which have no name property outside the
        /// <para/>UI designer. 
        /// <para/>An unnamed object can be name by giving it a tag "Name=componentName".
        /// </summary>
        /// <returns>A list of unnamed component type names.</returns>
        public List<string> GetNamelessObjects()
        {
            List<string> retVal = new List<string>();
            FieldInfo[] fieldInfos;
            object baseObject;
            if (thisForm != null)
            {
                fieldInfos = thisForm.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                baseObject = thisForm;
            }
            else if (thisWindow != null)
            {
                fieldInfos = thisWindow.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                baseObject = thisWindow;
            }
            else
            {
                return retVal;
            }


            foreach (FieldInfo info in fieldInfos)
            {
                object obj = baseObject.GetType().GetField(info.Name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(baseObject) as object;
                if (obj == null)
                {
                    continue;
                }

                List<PropertyInfo> piArrSmall = new List<PropertyInfo>();
                try
                {
                    piArrSmall.Add(obj.GetType().GetProperty("Tag"));
                }
                catch
                {

                }

                try
                {
                    piArrSmall.Add(obj.GetType().GetProperty("Name"));
                }
                catch
                {

                }

                try
                {
                    piArrSmall.Add(obj.GetType().GetProperty("Uid"));
                }
                catch
                {

                }

                if (piArrSmall.Count == 0)
                {
                    continue;
                }

                string name = GetObjectName(piArrSmall, baseObject);

                PropertyInfo[] piArr = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                if (!NameSpaceMatch(info.FieldType.Namespace))
                {
                    continue;
                }

                if (name == string.Empty)
                {
                    if (!retVal.Contains(obj.GetType().ToString()))
                    {
                        retVal.Add(obj.GetType().ToString());
                    }
                }
            }
            return retVal;
        }

        /// <summary>
        /// Gets an object name if it has a Name property or
        /// <para/>a Tag property set with string "Name=componentName"
        /// </summary>
        /// <param name="piArr">An array of PropertyInfo class instances.</param>
        /// <param name="obj">The object which name to get if any.</param>
        /// <returns>An object name if such is defined, otherwise string.Empty.</returns>
        private string GetObjectName(List<PropertyInfo> piArr, object obj)
        {
            if (piArr.Count == 0)
            {
                return string.Empty;
            }

            foreach (PropertyInfo pi in piArr)
            {
                try
                {
                    if (pi.Name == "Name")
                    {
                        if (pi.GetValue(obj).ToString() != string.Empty)
                        {
                            return pi.GetValue(obj).ToString();
                        }
                    }
                    if (pi.Name == "Uid") // x:Uid support added
                    {
                        if (pi.GetValue(obj).ToString() != string.Empty)
                        {
                            return pi.GetValue(obj).ToString();
                        }
                    }
                    if (pi.Name == "Tag")
                    {
                        string tagName = pi.GetValue(obj).ToString();
                        if (tagName.StartsWith("Name="))
                        {
                            return tagName.Substring(tagName.IndexOf('=') + 1);
                        }
                    }
                }
                catch
                {

                }
            }
            return string.Empty;
        }

        /// <summary>
        /// An enumerator for a GuiObject class instance collection.
        /// </summary>
        /// <returns>An enumerator for a GuiObject class instance collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        /// <summary>
        /// The actual enumerator class instance.
        /// </summary>
        /// <returns>The actual enumerator class instance.</returns>
        public GuiEnum GetEnumerator()
        {
            return new GuiEnum(wfObjects.ToArray());
        }
    }

    /// <summary>
    /// An enumerator class for listing GuiObject class instances.
    /// </summary>
    public class GuiEnum : IEnumerator
    {
        /// <summary>
        /// An internal array of GuiObject class instances.
        /// </summary>
        private List<GuiObject> wfObjects = new List<GuiObject>();

        /// <summary>
        /// Enumerators are positioned before the first element 
        /// <para/>until the first MoveNext() call.
        /// </summary>
        int position = -1;

        /// <summary>
        /// The GuiEnum class constructor.
        /// <para/>The enumerator instance is initialized 
        /// <para/>with the given array of GuiObject instances.
        /// </summary>
        /// <param name="list">A given array of GuiObject instances
        /// <para/>to initialize the enumerator.</param>
        public GuiEnum(GuiObject[] list)
        {
            if (list != null)
            {
                wfObjects.AddRange(list);
            }
        }

        /// <summary>
        /// Moves to a next GuiObject instance in the enumerator.
        /// </summary>
        /// <returns>True if there are more GuiObject instances
        /// <para/>in the classes internal collection, otherwise false.</returns>
        public bool MoveNext()
        {
            position++;
            return (position < wfObjects.Count);
        }

        /// <summary>
        /// Resets the enumerator to the first
        /// <para/>GuiObject instance in the class internal
        /// <para/>collection if any GuiObject instances exist.
        /// </summary>
        public void Reset()
        {
            position = -1;
        }

        /// <summary>
        /// Gets the current GuiObject instance in the 
        /// <para/>class internal collection.
        /// </summary>
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        /// <summary>
        /// Gets the current GuiObject instance in the 
        /// <para/>class internal collection.
        /// </summary>
        public GuiObject Current
        {
            get
            {
                try
                {
                    return wfObjects[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }

    /// <summary>
    /// A class to define a single GUI object.
    /// </summary>
    public class GuiObject
    {
        /// <summary>
        /// A combination of the applications assembly name and
        /// <para/>the underlying form / window name.
        /// </summary>
        private string appForm = string.Empty;

        /// <summary>
        /// An item name. E.g. "Form1".
        /// </summary>
        private string item = string.Empty;

        /// <summary>
        /// The culture of the GuiObject instance.
        /// </summary>
        private CultureInfo culture = CultureInfo.CurrentCulture;

        /// <summary>
        /// A property name. E.g. "Text".
        /// </summary>
        private string propertyName = string.Empty;

        /// <summary>
        /// A value type. E.g. "System.String".
        /// </summary>
        private string valueType = typeof(string).ToString();

        /// <summary>
        /// An object instance place holder representing
        /// <para/>the value of referring GuiObject instance.
        /// </summary>
        private object value = new object();

        /// <summary>
        /// If the object is in use or is not found.
        /// </summary>
        private bool inUse = false;

        /// <summary>
        /// A reference to the underlying object.
        /// </summary>
        private object objectRef = new object();

        private PropertyInfo pi = null;

        /// <summary>
        /// The constructor of the GuiObject class.
        /// </summary>
        /// <param name="appForm">A combination of the applications assembly name and
        /// <para/>the underlying form / window name.</param>
        /// <param name="item">An item name. E.g. "Form1".</param>
        /// <param name="culture">The culture in which the GuiObject instance is.</param>
        /// <param name="propertyName">A property name. E.g. "Text".</param>
        /// <param name="valueType">A value type. E.g. "System.String".</param>
        /// <param name="value">A property value. E.g. "Form1".</param>
        /// <param name="inUse">If the GuiObject instance is in use.
        /// <para/>E.g. found from the appForm.</param>
        /// <param name="objectRef">A reference to the underlying object.</param>
        public GuiObject(string appForm, string item, CultureInfo culture, string propertyName, string valueType, object value, bool inUse, object objectRef)
        {
            this.appForm = appForm;
            this.item = item;
            this.culture = culture;
            this.propertyName = propertyName;
            this.valueType = valueType;
            this.value = value;
            this.inUse = inUse;
            this.objectRef = objectRef;
            if (objectRef != null)
            {
                try
                {
                    if (objectRef is PropertyInfo)
                    {
                        pi = objectRef as PropertyInfo;
                    }
                    else
                    {
                        pi = objectRef.GetType().GetProperty(propertyName);
                    }
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// Gets or sets a reference to the underlying object.
        /// </summary>
        public object ObjectRef
        {
            get
            {
                return objectRef;
            }

            set
            {
                objectRef = value;
                if (objectRef != null)
                {
                    try
                    {
                        pi = objectRef.GetType().GetProperty(propertyName);
                    }
                    catch
                    {

                    }
                }
            }
        }

        /// <summary>
        /// Set the value of the underlying object reference. 
        /// </summary>
        /// <param name="val">An optional value to set. 
        /// <para/>If null the internal value is used.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public bool SetValue(object val = null)
        {
            try
            {
                if (objectRef != null)
                {
                    if (pi != null)
                    {
                        pi.SetValue(objectRef, val == null ? this.value : val);
                    }
                    else
                    {
                        pi = objectRef.GetType().GetProperty(propertyName);
                        pi.SetValue(objectRef, val == null ? this.value : val);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// A combination of the applications assembly name and
        /// <para/>the underlying form / window name.
        /// </summary>
        public string AppForm
        {
            get
            {
                return appForm;
            }

            set
            {
                appForm = value;
            }
        }

        /// <summary>
        /// An item name. E.g. "Form1".
        /// </summary>
        public string Item
        {
            get
            {
                return item;
            }

            set
            {
                item = value;
            }
        }

        /// <summary>
        /// The culture of the GuiObject instance.
        /// </summary>
        public CultureInfo Culture
        {
            get
            {
                return culture;
            }

            set
            {
                culture = value;
            }
        }

        /// <summary>
        /// A property name. E.g. "Text".
        /// </summary>
        public string PropertyName
        {
            get
            {
                return propertyName;
            }

            set
            {
                propertyName = value;
            }
        }

        /// <summary>
        /// An object instance place holder representing
        /// <para/>the value of referring GuiObject instance.
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        /// <summary>
        /// A value type. E.g. "System.String".
        /// </summary>
        public string ValueType
        {
            get
            {
                return valueType;
            }

            set
            {
                valueType = value;
            }
        }

        /// <summary>
        /// If the object is in use or is not found.
        /// </summary>
        public bool InUse
        {
            get
            {
                return inUse;
            }

            set
            {
                inUse = value;
            }
        }     
    }
}
