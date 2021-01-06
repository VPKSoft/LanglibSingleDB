using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPKSoft.LangLib
{
    /// <summary>
    /// An interface in case the <see cref="Form"/> form is not it's base class.
    /// </summary>
    public interface IDBLangEngineWinforms
    {
        /// <summary>
        /// The actual localization engine (DBLangEngine) for
        /// <para/>wrapper class.
        /// </summary>
        DBLangEngine DBLangEngine { get; set; }


        /// <summary>
        /// Initializes the <see cref="DBLangEngine"/> property value.
        /// </summary>
        /// <param name="inheritForm">The class instance inherited from the <see cref="Form"/> class.</param>
        void InitFormLocalization(Form inheritForm);
    }
}
