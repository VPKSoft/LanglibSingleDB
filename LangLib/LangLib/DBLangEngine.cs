#region License
/*
LangLib

A program and library for application localization.
Copyright (C) 2020 VPKSoft, Petteri Kautonen

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
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace VPKSoft.LangLib
{
    /// <summary>
    /// The application type. 
    /// <para/>WPF (for Windows Presentation Foundation)
    /// <para/>WinForms (for Windows Forms application)
    /// <para/>Undefined (for throwing exceptions)
    /// </summary>
    public enum AppType {
        /// <summary>
        /// WPF (for Windows Presentation Foundation)
        /// </summary>
        // ReSharper disable once InconsistentNaming
        WPF, 
        /// <summary>
        /// WinForms (for Windows Forms application)
        /// </summary>
        // ReSharper disable once IdentifierTypo
        Winforms, 
        /// <summary>
        /// Undefined (for throwing exceptions)
        /// </summary>
        Undefined };

    /// <summary>
    /// A class to enumerate Form / Window objects and properties.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class DBLangEngine: GuiObjectsEnum
    {
        /// <summary>
        /// The application type. 
        /// <para/>WPF (for Windows Presentation Foundation)
        /// <para/>WinForms (for Windows Forms application)
        /// <para/>Undefined (for throwing exceptions)
        /// </summary>
        private AppType appType;

        /// <summary>
        /// The constructor for Windows Forms application.
        /// </summary>
        /// <param name="form">The base form for the DBLangEngine to use.</param>
        /// <param name="appType">The application type. Should be AppType.Winforms.</param>
        public DBLangEngine(System.Windows.Forms.Form form, AppType appType):
            base(form)
        {
            this.appType = appType;
            ThisForm = form;
            // int the data directory with default
            _dataDir = string.IsNullOrWhiteSpace(DataDir) ? GetAppSettingsFolder(appType) : _dataDir;
        }

        /// <summary>
        /// Whether to use x:Uid's to reference to a FrameworkElement class instance.
        /// </summary>
        private bool useUids = true;

        /// <summary>
        /// The constructor for Windows Presentation Foundation (WPF) application.
        /// </summary>
        /// <param name="window">The base window for the DBLangEngine to use.</param>
        /// <param name="appType">The application type. Should be AppType.WPF.</param>
        /// <param name="useUids">Whether to use x:Uid's to reference to a FrameworkElement class instance.</param>
        public DBLangEngine(System.Windows.Window window, AppType appType, bool useUids = true) :
            base(window, useUids)
        {
            this.appType = appType;
            this.useUids = useUids;
            ThisWindow = window;
            // int the data directory with default
            _dataDir = string.IsNullOrWhiteSpace(DataDir) ? GetAppSettingsFolder(appType) : _dataDir;
        }        

        /// <summary>
        /// A writable data directory.
        /// </summary>
        private static string _dataDir;

        /// <summary>
        /// A default database name.
        /// </summary>
        private static string _dbName = "lang.sqlite";

        /// <summary>
        /// Just returns the default writable data directory for "non-roaming" applications.
        /// </summary>
        /// <returns>A writable data directory for "non-roaming" applications.</returns>
        static string GetAppSettingsFolder(AppType appType)
        {
            if (appType == AppType.Winforms)
            {
                string appName = System.Windows.Forms.Application.ProductName;
                foreach (char chr in Path.GetInvalidFileNameChars())
                {
                    appName = appName.Replace(chr, '_');
                }

                return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\" + appName + @"\";
            }
            else if (appType == AppType.WPF)
            {
                string appName = System.Windows.Application.ResourceAssembly.GetName().Name;

                return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\" + appName + @"\";
            }
            else
            {
                // nice to throw something :-)
                throw new TypeInitializationException("Missing application type.", new Exception());
            }
        }

        /// <summary>
        /// Get an assembly of the application depending of
        /// <para/>the application type (<paramref name="appType"/>).
        /// </summary>
        /// <param name="appType">The application type.</param>
        /// <returns>The assembly of the application.</returns>
        private static string GetAssemblyName(AppType appType)
        {
            if (appType == AppType.Winforms)
            {
                return System.Windows.Forms.Application.ProductName;
            }
            else if (appType == AppType.WPF)
            {
                return System.Windows.Application.ResourceAssembly.GetName().Name;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the SQLite database file name residing in DataDir.
        /// <para/>The default is lang.sqlite.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static string DBName
        {
            get
            {
                return _dbName;
            }

            set
            {
                if (value == string.Empty)
                {
                    throw new ArgumentException("Empty string is not allowed.");
                }

                _dbName = value;
                foreach (char chr in Path.GetInvalidFileNameChars())
                {
                    _dbName = _dbName.Replace(chr, '_');
                }
            }
        }

        /// <summary>
        /// Gets or sets a writable directory where the settings should be saved.
        /// <para/>The default is "[...]\AppData\Local\[Assembly product name]."
        /// </summary>
        public static string DataDir
        {
            get
            {
                return _dataDir;
            }

            set
            {
                if (value == string.Empty)
                {
                    throw new ArgumentException("Empty string is not allowed.");
                }

                _dataDir = value;
                foreach (char chr in Path.GetInvalidPathChars())
                {
                    _dataDir = _dataDir.Replace(chr, '_');
                }

                _dataDir = _dataDir.EndsWith(@"\") ? _dataDir : _dataDir + @"\";
            }            
        }

        /// <summary>
        /// If the messages were saved to the database 
        /// <para/>using the FallBackCulture property.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private static bool messagesSaved;

        /// <summary>
        /// If the INUSE field int the database was updated.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private static bool langUseUpdated;

        /// <summary>
        /// If the entire culture list supported by
        /// <para/>the .NET Framework were inserted into to
        /// <para/>language database.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private static bool culturesInserted;

        /// <summary>
        /// If all the database tables required by the
        /// <para/>library where created.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private static bool tablesCreated;

        /// <summary>
        /// Application product name and the
        /// <para/>underlying form name combined with a dot (.).
        /// </summary>
        private string parentItem = string.Empty;

        /// <summary>
        /// The SQLite database connection to used for this library.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private static SQLiteConnection DBLangConnnection;

        /// <summary>
        /// If the library should buffer the language items
        /// <para/>to be inserted into the database.
        /// <para/>This is to avoid slowness created by
        /// <para/>multiple transactions instead of one.
        /// </summary>
        private bool buffer;

        /// <summary>
        /// Used as a buffer for SQL sentences to avoid
        /// <para/>too small transactions.
        /// </summary>
        private string sqlEntry = string.Empty;

        /// <summary>
        /// A list of forms / windows that already
        /// <para/>have been enumerated to avoid
        /// <para/>redoing the enumeration process.
        /// </summary>
        private static List<string> formNames = new List<string>();

        /// <summary>
        /// A buffer for translated messages. This is to avoid querying
        /// <para/>them from the database multiple times.
        /// </summary>
        private List<StringMessages> messages = new List<StringMessages>();

        /// <summary>
        /// A static buffer for translated messages. 
        /// <para/>This is to avoid querying
        /// <para/>them from the database multiple times.
        /// </summary>
        private static List<StringMessages> statMessages = new List<StringMessages>();

        /// <summary>
        /// The fall-back culture to be used if the 
        /// <para/>current culture is not found from the language database.
        /// <para/>The default is "en-US" (1033) which should be used
        /// <para/>to ease up the localization process.
        /// </summary>
        public static CultureInfo FallBackCulture { get; set; } = new CultureInfo(1033);

        // a value to be used with the UseCulture property..
        private static CultureInfo _useCulture;

        // and indicator if a culture which should be used instead of the 
        // system's culture has been read from the command line arguments..
        private static bool _paramCultureChecked;

        /// <summary>
        /// Gets or sets the culture to be used instead of the system's culture.
        /// </summary>
        public static CultureInfo UseCulture
        {
            get
            {
                // if the property value "holder" is null and no command line argument check has been done..
                if (_useCulture == null && !_paramCultureChecked)
                {
                    // ..check the command line arguments and if an instruction for the culture to be used
                    // has been given..
                    foreach (string arg in Environment.GetCommandLineArgs())
                    {
                        // this is the argument..
                        if (arg.StartsWith("--language="))
                        {
                            try // avoid crash if the argument is miss-formatted..
                            {
                                // get the name or number for the culture to be used instead of the
                                // system's current culture..
                                string cultureName = arg.Split('=')[1];

                                // if the value is an integer..
                                if (int.TryParse(arg.Split('=')[1], out _))
                                {
                                    // ..use the integer value to create a culture from a locale identifier (LCID)..
                                    _useCulture = CultureInfo.GetCultureInfo(int.Parse(arg.Split('=')[1]));
                                }
                                else
                                {
                                    // try to create a culture from a given name (BCP-47 tag == IETF language tag):
                                    // https://en.wikipedia.org/wiki/IETF_language_tag
                                    _useCulture = CultureInfo.GetCultureInfo(cultureName);
                                }
                            }
                            catch
                            {
                                // an exception occurred, so do fall back to the current system's culture..
                                _useCulture = CultureInfo.CurrentCulture;
                            }
                        }
                    }
                    // set the flag to not the check the culture again from command line arguments..
                    _paramCultureChecked = true;
                }
                // return the value..
                return _useCulture;
            }

            set
            {
                // set the value of the culture to override the system's current culture..
                _useCulture = value;

                // set the flag to not the check the culture again from command line arguments..
                _paramCultureChecked = true;
            }
        }

        /// <summary>
        /// Gets the culture to be used if overridden by the UseCulture property.
        /// </summary>
        /// <param name="ciOverride">This CultureInfo is returned if the UseCulture is not assigned (a fall-back).</param>
        /// <returns>A culture to be along side the default culture.</returns>
        private static CultureInfo GetUseCulture(CultureInfo ciOverride)
        {
            // not overridden, so just return the given parameter..
            if (UseCulture == null)
            {
                // another check to avoid a null parameter..
                if (ciOverride == null)
                {
                    // ..so if null then just return the system's current culture..
                    return CultureInfo.CurrentCulture;
                }
                else
                {
                    // return the value of the parameter..
                    return ciOverride;
                }
            }
            else
            {
                // the override culture is set so return it's value..
                return UseCulture;
            }
        }

        /// <summary>
        /// The most important method in LangLib. This creates the database,
        /// <para/>the tables to it (MESSAGES, FORMITEMS, CULTURES).
        /// <para/><para/>Also the FallBackCulture is updated for the underlying form/window.
        /// <para/>Messages from the given <paramref name="messageResource"/> are inserted to
        /// <para/>the database if their don't exists.
        /// <para/><para/>The table fields FORMITEMS.INUSE and MESSAGES.INUSE are updated
        /// <para/>for the FallBackCulture.
        /// </summary>
        /// <param name="messageResource">A resource name that contains the application
        /// <para/>messages in the fall FallBackCulture language.
        /// <para/>For example if I have an application which assembly name is
        /// <para/>LangLibTestWinforms and it has a .resx file called Messages
        /// <para/>I would give this parameter a value of "LangLibTestWinforms.Messages".</param>
        /// <param name="culture">The culture to use for the localization.
        /// <para/>If no culture is given the current system culture is used and
        /// <para/>the FallBackCulture is used as fall-back culture.</param>
        /// <param name="loadItems">To load language items or not.</param>
        public void InitializeLanguage(string messageResource, CultureInfo culture = null, bool loadItems = true)
        {
            if (!Design)
            {
                try
                {
                    DateTime dt = DateTime.Now;
                    if (!Directory.Exists(DataDir))
                    {
                        Directory.CreateDirectory(DataDir);
                    }

                    if (DBLangConnnection == null)
                    {
                        DBLangConnnection = new SQLiteConnection("Data Source=" + DataDir + DBName + ";Pooling=true;FailIfMissing=false");
                        DBLangConnnection.Open();
                    }

                    if (!tablesCreated)
                    {
                        using (SQLiteCommand command = new SQLiteCommand(DBLangConnnection))
                        {
                            command.CommandText = "CREATE TABLE IF NOT EXISTS MESSAGES( " +
                                                  "CULTURE TEXT NOT NULL, " +
                                                  "MESSAGENAME TEXT NOT NULL, " +
                                                  "VALUE TEXT NULL, " +
                                                  "COMMENT_EN_US TEXT NULL, " +
                                                  "INUSE INTEGER NULL) ";
                            command.ExecuteNonQuery();
                        }

                        using (SQLiteCommand command = new SQLiteCommand(DBLangConnnection))
                        {
                            command.CommandText = "CREATE TABLE IF NOT EXISTS FORMITEMS( " +
                                                  "APP_FORM TEXT NOT NULL, " +
                                                  "ITEM TEXT NOT NULL, " +
                                                  "CULTURE TEXT NOT NULL, " +
                                                  "PROPERTYNAME TEXT NOT NULL, " +
                                                  "VALUETYPE TEXT NOT NULL, " +
                                                  "VALUE TEXT NULL, " +
                                                  "INUSE INTEGER NULL) ";
                            command.ExecuteNonQuery();
                        }

                        using (SQLiteCommand command = new SQLiteCommand(DBLangConnnection))
                        {
                            command.CommandText = "CREATE TABLE IF NOT EXISTS CULTURES( " +
                                                  "CULTURE TEXT NOT NULL, " +
                                                  "NATIVENAME TEXT NULL, " +
                                                  "LCID INTEGER NULL) ";
                            command.ExecuteNonQuery();
                        }
                        tablesCreated = true;
                    }

                    if (!culturesInserted)
                    {
                        string sql = string.Empty;
                        CultureInfo[] allCultures;
                        allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

                        foreach (CultureInfo ci in allCultures)
                        {
                            sql += "INSERT INTO CULTURES (CULTURE, NATIVENAME, LCID) " +
                                   "SELECT '" + ci.Name.Replace("'", "''") + "', '" + ci.NativeName.Replace("'", "''") + "', " + ci.LCID + " " +
                                   "WHERE NOT EXISTS(SELECT 1 FROM CULTURES WHERE CULTURE = '" + ci.Name.Replace("'", "''") + "'); ";

                        }

                        using (SQLiteTransaction trans = DBLangConnnection.BeginTransaction())
                        {
                            using (SQLiteCommand command = new SQLiteCommand(DBLangConnnection))
                            {
                                command.CommandText = sql;
                                command.ExecuteNonQuery();
                            }
                            trans.Commit();
                        }
                        culturesInserted = true;
                    }

                    if (!loadItems)
                    {
                        GetGuiObjets(FallBackCulture);
                        if (!langUseUpdated)
                        {
                            using (SQLiteTransaction trans = DBLangConnnection.BeginTransaction())
                            {
                                using (SQLiteCommand command = new SQLiteCommand(DBLangConnnection))
                                {
                                    command.CommandText = "UPDATE FORMITEMS SET INUSE = 0 WHERE CULTURE = '" + FallBackCulture + "' ";
                                    command.ExecuteNonQuery();
                                }
                                trans.Commit();
                            }
                            langUseUpdated = true;
                        }
                        SaveLanguageItems(this.BaseInstance, FallBackCulture);
                    }

                    if (loadItems)
                    {
                        List<string> localProps = LocalizedProps(AppForm, CultureInfo.CurrentCulture);
                        GetGuiObjets(CultureInfo.CurrentCulture, localProps);
                        LoadLanguageItems(CultureInfo.CurrentCulture);
                    }

                    if (!loadItems)
                    {
                        SaveMessages(messageResource, ref DBLangConnnection);
                    }

                    ts = ts.Add((DateTime.Now - dt));
                }
                catch (Exception ex)
                {
                    // invalid processor architecture or missing library parts
                    if (ex.GetType() == typeof(BadImageFormatException))
                    {
                        throw new InvalidSQLIteLibException();
                    }
                    else if (ex.GetType() == typeof(MissingManifestResourceException)) // This is fun. The user actually gets some help from the exception message ;-)
                    {
                        throw new LangLibException(string.Format("Missing resource '{0}'.{1}Perhaps {2}.[Resource file name] would do,{3}without the .resx file extension.",
                            messageResource, Environment.NewLine, GetAssemblyName(this.appType), Environment.NewLine));
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// A base exception type for the LangLib
        /// for general exceptions.
        /// </summary>
        public class LangLibException: Exception
        {
            /// <summary>
            /// The LangLibException class constructor.
            /// </summary>
            /// <param name="message">Initializes a new instance of the LangLibException class with a specified error message.</param>
            public LangLibException(string message):
                base(message)
            {

            }
        }

        /// <summary>
        /// An exception that is thrown if The SQLite library may be:
        /// wrong version/wrong processor architecture/missing SQLite.Interop.dll/etc..
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public class InvalidSQLIteLibException: Exception
        {
            /// <summary>
            /// The InvalidSQLIteLibException class constructor.
            /// </summary>
            public InvalidSQLIteLibException()
                : base("The SQLite library may be:" + Environment.NewLine +
                       "wrong version/wrong processor architecture/missing SQLite.Interop.dll/etc..")
            {

            }
        }

        /// <summary>
        /// The total time the library has spent in the
        /// <para/>InitializeLanguage method.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private static TimeSpan ts;

        /// <summary>
        /// The total time the library has spent in the
        /// <para/>InitializeLanguage method in seconds. 
        /// <para/>This property is mostly for testing and optimization purposes.
        /// </summary>
        public double InitTime
        {
            get
            {
                return ts.TotalSeconds;
            }
        }

        /// <summary>
        /// Gets the total time the library has spent in the
        /// <para/>InitializeLanguage method.
        /// <para/>This property is mostly for testing and optimization purposes.
        /// </summary>
        public TimeSpan InitTimeSpan
        {
            get
            {
                return ts;
            }
        }

        /// <summary>
        /// Gets a message based on a name and culture from cache.
        /// <para/>If the cache does not have the message, a database search is executed
        /// <para/>and <see cref="FallBackCulture"/> culture is used as fall-back.
        /// <para/>If the database does not have a message the default message is used.
        /// </summary>
        /// <param name="name">The name of the message to get</param>
        /// <param name="ci">Culture for the message</param>
        /// <param name="defaultMessage">The default message</param>
        /// <param name="items">Parameters for formatting the message.</param>
        /// <returns>A message based on the rules in the summary.</returns>
        public static string GetStatMessage(string name, CultureInfo ci, string defaultMessage, params object[] items)
        {
            foreach (StringMessages m in statMessages)
            {
                if (m.MessageName == name)
                {
                    try
                    {
                        return string.Format(m.Message, items);
                    }
                    catch
                    {
                        return m.Message;
                    }
                }
            }

            SQLiteConnection statDbLangConnection = new SQLiteConnection("Data Source=" + DataDir + DBName + ";Pooling=true;FailIfMissing=false");
            statDbLangConnection.Open();
            using (statDbLangConnection)
            {
                using (SQLiteCommand command = new SQLiteCommand(statDbLangConnection))
                {
                    command.CommandText = string.Format("SELECT VALUE, 0 AS SORT FROM MESSAGES " +
                                                        "WHERE CULTURE = {0} AND MESSAGENAME = {1} " +
                                                        "UNION " +
                                                        "SELECT VALUE, 1 AS SORT FROM MESSAGES " +
                                                        "WHERE CULTURE = {2} AND MESSAGENAME = {1} " +
                                                        "ORDER BY SORT ",
                                                        DbUtils.MkStr(GetUseCulture(ci).Name),
                                                        DbUtils.MkStr(name),
                                                        DbUtils.MkStr(FallBackCulture.Name));
                    using (SQLiteDataReader dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (!statMessages.Contains(new StringMessages(name, dr.GetString(0))))
                            {
                                statMessages.Add(new StringMessages(name, dr.GetString(0)));
                            }

                            try
                            {
                                return string.Format(dr.GetString(0), items);
                            }
                            catch
                            {
                                return dr.GetString(0);
                            }
                        }
                        else
                        {
                            SplitMessage(defaultMessage, out var value, out _);
                            try
                            {
                                return string.Format(value, items);
                            }
                            catch
                            {
                                return value;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets a message based on a name and current culture from cache.
        /// <para/>If the cache does not have the message, a database search is executed
        /// <para/>and <see cref="FallBackCulture"/> culture is used as fall-back.
        /// <para/>If the database does not have a message the default message is used.
        /// </summary>
        /// <param name="name">The name of the message to get</param>
        /// <param name="defaultMessage">The default message</param>
        /// <param name="items">Parameters for formatting the message.</param>
        /// <returns>A message based on the rules in the summary.</returns>
        public static string GetStatMessage(string name, string defaultMessage, params object[] items)
        {            
            return GetStatMessage(name, CultureInfo.CurrentCulture, defaultMessage, items);
        }

        /// <summary>
        /// Gets a message based on a name and current culture from cache.
        /// <para/>If the cache does not have the message, a database search is executed
        /// <para/>and FallBackCulture culture is used as fall-back.
        /// <para/>If the database does not have a message the default message is used.
        /// </summary>
        /// <param name="name">The name of the message to get</param>
        /// <param name="defaultMessage">The default message</param>
        /// <param name="items">Parameters for formatting the message.</param>
        /// <returns>A message based on the rules in the summary.</returns>
        public string GetMessage(string name, string defaultMessage, params object[] items)
        {
            return GetMessage(name, CultureInfo.CurrentCulture, defaultMessage, items);
        }

        /// <summary>
        /// Gets a list of CultureInfo class instances which are localized.
        /// <para/>(Exists in the database).
        /// </summary>
        /// <returns>A list of CultureInfo class instances which are localized</returns>
        public List<CultureInfo> GetLocalizedCultures()
        {
            List<CultureInfo> retVal = new List<CultureInfo>();
            using (SQLiteCommand command = new SQLiteCommand(DBLangConnnection))
            {
                command.CommandText = "SELECT DISTINCT CULTURE FROM FORMITEMS ";
                using (SQLiteDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        retVal.Add(CultureInfo.GetCultureInfo(dr.GetString(0)));
                    }
                }
            }
            return retVal;
        }


        /// <summary>
        /// Gets a message based on a name and culture from cache.
        /// <para/>If the cache does not have the message, a database search is executed
        /// <para/>and <see cref="FallBackCulture"/> culture is used as fall-back.
        /// <para/>If the database does not have a message the default message is used.
        /// </summary>
        /// <param name="name">The name of the message to get</param>
        /// <param name="ci">Culture for the message</param>
        /// <param name="defaultMessage">The default message</param>
        /// <param name="items">Parameters for formatting the message.</param>
        /// <returns>A message based on the rules in the summary.</returns>
        public string GetMessage(string name, CultureInfo ci, string defaultMessage, params object[] items)
        {
            foreach (StringMessages m in messages)
            {
                if (m.MessageName == name)
                {
                    try
                    {
                        return string.Format(m.Message, items);
                    }
                    catch
                    {
                        return m.Message;
                    }
                }
            }
            using (SQLiteCommand command = new SQLiteCommand(DBLangConnnection))
            {
                command.CommandText = string.Format("SELECT VALUE, 0 AS SORT FROM MESSAGES " +
                                                    "WHERE CULTURE = {0} AND MESSAGENAME = {1} " +
                                                    "UNION " +
                                                    "SELECT VALUE, 1 AS SORT FROM MESSAGES " +
                                                    "WHERE CULTURE = {2} AND MESSAGENAME = {1} " +
                                                    "ORDER BY SORT ",
                                                    DbUtils.MkStr(GetUseCulture(ci).Name),
                                                    DbUtils.MkStr(name),
                                                    DbUtils.MkStr(FallBackCulture.Name));
                using (SQLiteDataReader dr = command.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        if (!messages.Contains(new StringMessages(name, dr.GetString(0))))
                        {
                            messages.Add(new StringMessages(name, dr.GetString(0)));
                        }

                        try
                        {
                            return string.Format(dr.GetString(0), items);
                        }
                        catch
                        {
                            return dr.GetString(0);
                        }

                    }
                    else
                    {
                        SplitMessage(defaultMessage, out var value, out _);
                        try
                        {
                            return string.Format(value, items);
                        }
                        catch
                        {
                            return value;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Begins a buffer for SQL sentences to avoid
        /// <para/>small transactions. The "buffer" is executed
        /// <para/>when the EndBuffer method is called and
        /// <para/>then cleared.
        /// </summary>
        public void BeginBuffer()
        {
            buffer = true;
            sqlEntry = string.Empty;
        }

        /// <summary>
        /// Executes a transaction (a sequence of SQL sentences) from
        /// <para/>the "buffer". After completion of the "buffer" execution,
        /// <para/>the "buffer" is cleared and buffering is disabled.
        /// </summary>
        public void EndBuffer()
        {
            if (buffer)
            {
                buffer = false;
                using (SQLiteTransaction trans = DBLangConnnection.BeginTransaction())
                {
                    using (SQLiteCommand command = new SQLiteCommand(DBLangConnnection))
                    {
                        command.CommandText = sqlEntry;
                        command.ExecuteNonQuery();
                    }
                    trans.Commit();
                }
                sqlEntry = string.Empty;
            }
        }

        /// <summary>
        /// Inserts a single language item to the database or buffers
        /// <para/>it if the BeginBuffer method has been called before.
        /// </summary>
        /// <param name="appForm">A combination of the applications assembly name and
        /// <para/>the underlying form / window name.</param>
        /// <param name="item">An item name. E.g. "Form1".</param>
        /// <param name="propertyName">A property name. E.g. "Text".</param>
        /// <param name="valueType">A value type. E.g. "System.String".</param>
        /// <param name="value">A property value. E.g. "Form1".</param>
        /// <param name="ci">The culture in which language the item is.
        /// <para/>The database field FORMITEMS.INUSE is also updated to value of 1.</param>
        public void InsertLangItem(string appForm, string item, string propertyName, string valueType, string value, CultureInfo ci)
        {
            if (buffer)
            {
                sqlEntry += string.Format("INSERT INTO FORMITEMS (APP_FORM, ITEM, CULTURE, PROPERTYNAME, VALUETYPE, VALUE) " +
                                           "SELECT {0}, {1}, {2}, {3}, {4}, {5} " +
                                           "WHERE NOT EXISTS (SELECT 1 FROM FORMITEMS WHERE APP_FORM = {0} AND ITEM = {1} AND " +
                                           "CULTURE = {2} AND PROPERTYNAME = {3}); " +
                                           "UPDATE FORMITEMS SET INUSE = 1 WHERE APP_FORM = {0} AND ITEM = {1} AND " +
                                           "PROPERTYNAME = {3}; ",
                                           DbUtils.MkStr(appForm),
                                           DbUtils.MkStr(item),
                                           DbUtils.MkStr(ci.Name),
                                           DbUtils.MkStr(propertyName),
                                           DbUtils.MkStr(valueType),
                                           DbUtils.MkStr(value));
            }
            else
            {
                using (SQLiteCommand command = new SQLiteCommand(DBLangConnnection))
                {
                    command.CommandText = string.Format("INSERT INTO FORMITEMS (APP_FORM, ITEM, CULTURE, PROPERTYNAME, VALUETYPE, VALUE) " +
                                                        "SELECT {0}, {1}, {2}, {3}, {4}, {5} " +
                                                        "WHERE NOT EXISTS (SELECT 1 FROM FORMITEMS WHERE APP_FORM = {0} AND ITEM = {1} AND " +
                                                        "CULTURE = {2} AND PROPERTYNAME = {3}); " +
                                                        "UPDATE FORMITEMS SET INUSE = 1 WHERE APP_FORM = {0} AND ITEM = {1} AND " +
                                                        "PROPERTYNAME = {3}; ",
                                                        DbUtils.MkStr(appForm),
                                                        DbUtils.MkStr(item),
                                                        DbUtils.MkStr(ci.Name),
                                                        DbUtils.MkStr(propertyName),
                                                        DbUtils.MkStr(valueType),
                                                        DbUtils.MkStr(value));
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Splits a message into value and comment part. A message is split
        /// <para/>from the last occurrence of an or character (|). 
        /// <para/>E.g. "A test message. Or characters (|) may also exist in the message.|As in a test message."
        /// </summary>
        /// <param name="i">A DictionaryEntry class instance which Value part is
        /// <para/>split into value and comment part.</param>
        /// <param name="value">A string where the message part of the message is placed.</param>
        /// <param name="comment">A string where the comment part of the message is placed.</param>
        private static void SplitMessage(DictionaryEntry i, out string value, out string comment)
        {            
            SplitMessage(i.Value.ToString(), out value, out comment);
        }

        /// <summary>
        /// Splits a message into value and comment part. A message is split
        /// <para/>from the last occurrence of an or character (|). 
        /// <para/>E.g. "A test message. Or characters (|) may also exist in the message.|As in a test message."
        /// </summary>
        /// <param name="msg">A string which is split into value and comment part.</param>
        /// <param name="value">A string where the message part of the message is placed.</param>
        /// <param name="comment">A string where the comment part of the message is placed.</param>
        private static void SplitMessage(string msg, out string value, out string comment)
        {
            try
            {
                int ind = msg.LastIndexOf('|');
                if (ind != -1)
                {

                }
                value = msg.Substring(0, ind);
                comment = msg.Substring(ind + 1);
            }
            catch
            {
                value = msg;
                comment = string.Empty;
            }
        }

        /// <summary>
        /// Saved the application messages if the weren't already saved.
        /// <para/>The database field MESSAGES.INUSE is also updated for all saved cultures.
        /// </summary>
        /// <param name="resourcefile">A resource name that contains the application
        /// <para/>messages in the fall FallBackCulture language.
        /// <para/>For example if I have an application which assembly name is
        /// <para/>LangLibTestWinforms and it has a .resx file called Messages
        /// <para/>I would give this parameter a value of "LangLibTestWinforms.Messages"</param>
        /// <param name="conn">Reference to a SQLiteConnection class instance.</param>
        public static void SaveMessages(string resourcefile, ref SQLiteConnection conn)
        {
            if (messagesSaved)
            {
                return;
            }

            if (!messagesSaved)
            {
                messagesSaved = true;
            }

            string sql = string.Empty;
            sql += "UPDATE MESSAGES SET INUSE = 0 WHERE CULTURE = " + DbUtils.MkStr(FallBackCulture.Name) + "; ";

            var assembly = Assembly.GetEntryAssembly();

            if (assembly != null)
            {
                ResourceManager rm = new ResourceManager(resourcefile, assembly);
                ResourceSet rs = rm.GetResourceSet(CultureInfo.InvariantCulture, true, true);
                foreach (DictionaryEntry i in rs)
                {
                    string value, comment;
                    SplitMessage(i, out value, out comment);
                    sql += string.Format("INSERT INTO MESSAGES(CULTURE, MESSAGENAME, VALUE, COMMENT_EN_US) " +
                                         "SELECT {0}, {1}, {2}, {3} " +
                                         "WHERE NOT EXISTS(SELECT 1 FROM MESSAGES WHERE CULTURE = {0} AND MESSAGENAME = {1}); " +
                                         "UPDATE MESSAGES SET INUSE = 1 WHERE MESSAGENAME = {1}; ",
                        DbUtils.MkStr(FallBackCulture.Name),
                        DbUtils.MkStr(i.Key.ToString()),
                        DbUtils.MkStr(value),
                        DbUtils.MkStr(comment));
                }
            }

            using (SQLiteTransaction trans = conn.BeginTransaction())
            {
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
                trans.Commit();
            }
        }

        /// <summary>
        /// Internal class for caching localization items.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private class DBCacheHolder
        {
            /// <summary>
            /// Item name.
            /// </summary>
            public string Item = string.Empty;

            /// <summary>
            /// Property name.
            /// </summary>
            public string PropertyName = string.Empty;

            /// <summary>
            /// Value type.
            /// </summary>
            public string ValueType = string.Empty;

            /// <summary>
            /// Value.
            /// </summary>
            public string Value = string.Empty;

            /// <summary>
            /// If the item is in use or not as in the database "point of view".
            /// </summary>
            public bool InUse = true;

            /// <summary>
            /// If item from the database matches the culture
            /// <para/>one wished to get.
            /// </summary>
            public bool NotFallBackLang = true;

            /// <summary>
            /// Culture as in Culture.ToString().
            /// </summary>
            public string Culture = string.Empty;

            /// <summary>
            /// A combination of the applications assembly name and
            /// <para/>the underlying form / window name.
            /// </summary>
            public string AppForm = string.Empty;

            /// <summary>
            /// Basic constructor.
            /// </summary>
            public DBCacheHolder()
            {

            }

            /// <summary>
            /// A constructor that initializes DBCacheHolder
            /// <para/>properties with values gotten from a SQLiteDataReader instance.
            /// </summary>
            /// <param name="dr">A SQLiteDataReader instance.</param>
            public DBCacheHolder(SQLiteDataReader dr)
            {
                Item = dr.GetString(3);
                PropertyName = dr.GetString(2);
                ValueType = dr.GetString(0);
                Value = dr.GetString(1);
                InUse = dr.GetInt32(4) == 1;
                NotFallBackLang = dr.GetInt32(7) == 0;
                Culture = dr.GetString(5);
                AppForm = dr.GetString(6);
            }

            /// <summary>
            /// Checks if the AppForm is already in the cache.
            /// </summary>
            /// <param name="appForm">A combination of the applications assembly name and
            /// <para/>the underlying form / window name.</param>
            /// <param name="list">A list of DBCacheHolder instances.</param>
            /// <returns>True if the AppForm is already in the given list.</returns>
            public static bool ListContains(string appForm, List<DBCacheHolder> list)
            {
                foreach (DBCacheHolder dc in list)
                {
                    if (dc.AppForm == appForm)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Internal cache for form / window objects to avoid running same SQL queries.
        /// </summary>
        private static List<DBCacheHolder> DBCache = new List<DBCacheHolder>();

        /// <summary>
        /// Run this to clear the internal cache.
        /// <para/>This is useful if you want change the
        /// <para/>UI language on the fly.
        /// </summary>
        public static void ClearInternalCache()
        {
            DBCache.Clear();
        }

        /// <summary>
        /// Gets all items for the given <paramref name="appForm"/> bases on the given culture.
        /// <para/>If the given culture does not exist the FallBackCulture is used.
        /// </summary>
        /// <param name="appForm">A combination of the applications assembly name and
        /// <para/>the underlying form / window name.</param>
        /// <param name="ci">A culture to use for getting the form / window items.</param>
        // ReSharper disable once InconsistentNaming
        public void RunDBCache(string appForm, CultureInfo ci)
        {
            // Let's not use database connection if already cached.
            if (DBCacheHolder.ListContains(appForm, DBCache))
            {
                foreach (GuiObject go in this)
                {
                    try
                    {
                        DBCacheHolder dc = DBCache.First(first => first.PropertyName == go.PropertyName && first.Item == go.Item && first.AppForm == appForm);
                        if (dc != null)
                        {
                            if (dc.ValueType == "System.String")
                            {
                                go.Value = dc.Value;
                            }
                        }
                    } 
                    catch
                    {
                        // something wrong?
                    }
                }
                return;
            }

            List<string> handled = new List<string>();
            using (SQLiteCommand command = new SQLiteCommand(DBLangConnnection))
            {
                command.CommandText = string.Format("SELECT VALUETYPE, VALUE, PROPERTYNAME, ITEM, INUSE, CULTURE, APP_FORM, 0 AS SORT " +
                                                    "FROM FORMITEMS " +
                                                    "WHERE APP_FORM = {0} AND CULTURE = {1} " +
                                                    "UNION " +
                                                    "SELECT VALUETYPE, VALUE, PROPERTYNAME, ITEM, INUSE, CULTURE, APP_FORM, 1 AS SORT " +
                                                    "FROM FORMITEMS " +
                                                    "WHERE APP_FORM = {0} AND CULTURE = {2} " +
                                                    "ORDER BY SORT ",
                                                    DbUtils.MkStr(appForm),
                                                    DbUtils.MkStr(GetUseCulture(ci).Name),
                                                    DbUtils.MkStr(FallBackCulture.Name));
                using (SQLiteDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string item = dr.GetString(3);
                        string propertyName = dr.GetString(2);
                        string valueType = dr.GetString(0);
                        string value = dr.GetString(1);

                        foreach (GuiObject go in this)
                        {
                            if (go.PropertyName == propertyName &&
                                go.Item == item)
                            {
                                if (handled.Contains(go.PropertyName + "." + go.Item))
                                {
                                    continue;
                                }

                                try
                                {
                                    DBCache.Add(new DBCacheHolder(dr));
                                }
                                catch
                                {
                                    // Database connection error or internal logic failure? Well we can't let the application fall.
                                }

                                handled.Add(go.PropertyName + "." + go.Item);
                                if (valueType == "System.String")
                                {                                    
                                    go.Value = value;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets a list of strings of properties to localize from
        /// <para/>from the language database.
        /// </summary>
        /// <param name="appForm">Application product name concatenated with dot (.)
        /// <para/>a form or window name.</param>
        /// <param name="ci">A culture to use for getting the form / window items.</param>
        /// <returns>A list of strings of properties to localize from
        /// <para/>from the language database.</returns>
        public List<string> LocalizedProps(string appForm, CultureInfo ci)
        {
            List<string> handled = new List<string>();
            using (SQLiteCommand command = new SQLiteCommand(DBLangConnnection))
            {
                command.CommandText = string.Format("SELECT VALUETYPE, VALUE, PROPERTYNAME, ITEM, INUSE, CULTURE, APP_FORM, 0 AS SORT " +
                                                    "FROM FORMITEMS " +
                                                    "WHERE APP_FORM = {0} AND CULTURE = {1} " +
                                                    "UNION " +
                                                    "SELECT VALUETYPE, VALUE, PROPERTYNAME, ITEM, INUSE, CULTURE, APP_FORM, 1 AS SORT " +
                                                    "FROM FORMITEMS " +
                                                    "WHERE APP_FORM = {0} AND CULTURE = {2} " +
                                                    "ORDER BY SORT ",
                                                    DbUtils.MkStr(appForm),
                                                    DbUtils.MkStr(GetUseCulture(ci).Name),
                                                    DbUtils.MkStr(FallBackCulture.Name));
                using (SQLiteDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string item = dr.GetString(3);
                        string propertyName = dr.GetString(2);
                        if (handled.Contains(item + "." + propertyName))
                        {
                            continue;
                        }
                        handled.Add(item + "." + propertyName);
                    }
                }
            }
            return handled;
        }

        /// <summary>
        /// Load all the language items for the underlying 
        /// <para/>form / window using the system's current culture.
        /// <para/>If the culture is not found in the database, the FallBackCulture is used.
        /// </summary>
        public void LoadLanguageItems()
        {
            LoadLanguageItems(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Load all the language items for the underlying 
        /// form / window using the given culture.
        /// </summary>
        /// <param name="ci">Culture to use. If the given culture 
        /// <para/>is not found in the database, the FallBackCulture is used.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        public bool LoadLanguageItems(CultureInfo ci)
        {            
            RunDBCache(AppForm, ci);
            bool retVal = true;
            foreach (GuiObject go in this)
            {
                if (!go.SetValue())
                {
                    retVal = false;
                }
            }
            return retVal;
        }

        /// <summary>
        /// Saves the object's items to be localized into the language database.
        /// The system's current culture is used.
        /// </summary>
        /// <param name="obj">An object instance which items should be saved.</param>
        public void SaveLanguageItems(object obj)
        {
            SaveLanguageItems(obj, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Gets the object of the underlying form / window
        /// <para/>as GuiObject class instances and "marks" them with
        /// <para/>given culture.
        /// </summary>
        /// <param name="ci">A culture to "mark" the GuiObject class instance.</param>
        /// <param name="propertyNames">A names of the properties to include in the object list.
        /// <para/>If the value is null, no property names are prevented.</param>
        public void GetGuiObjets(CultureInfo ci, List<string> propertyNames = null)
        {
            if (this.appType == AppType.Winforms)
            {
                GetObjects(this.BaseInstance as System.Windows.Forms.Form, ci, true, propertyNames);
            }
            else if (this.appType == AppType.WPF)
            {
                GetObjects(this.BaseInstance as System.Windows.Window, ci, true, propertyNames);
            }

            if (!formNames.Contains(BaseInstanceName))
            {
                formNames.Add(BaseInstanceName);
                parentItem = BaseInstanceProduct + "." + BaseInstanceName;
            }
        }

        /// <summary>
        /// Saves the object's items to be localized into the language database.
        /// The given current culture is used.
        /// </summary>
        /// <param name="obj">An object instance which items should be saved.</param>
        /// <param name="ci">Culture to use.</param>
        public void SaveLanguageItems(object obj, CultureInfo ci)
        {
            BeginBuffer();

            foreach (GuiObject go in this)
            {
                InsertLangItem(go.AppForm, go.Item, go.PropertyName, go.ValueType, go.Value.ToString(), ci);
            }

            EndBuffer();
        }
    }
}
