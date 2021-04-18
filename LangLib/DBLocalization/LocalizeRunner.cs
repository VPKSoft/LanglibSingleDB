using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPKSoft.DBLocalization
{
    /// <summary>
    /// A class for launch the localization window from another application.
    /// </summary>
    public static class LocalizeRunner
    {
        /// <summary>
        /// Displays the localization window for the application.
        /// </summary>
        /// <param name="databasePath">The localization database file name.</param>
        /// <returns></returns>
        public static LocalizeMainWindow RunLocalizeWindow(string databasePath)
        {
            var result = new LocalizeMainWindow(databasePath);
            result.Show();
            return result;
        }
    }
}
