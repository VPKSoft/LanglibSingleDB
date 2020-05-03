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
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using VPKSoft.LangLib;

namespace LangLibTestWinforms
{
    public partial class FormMain : DBLangEngineWinforms
    {
        public FormMain()
        {
            InitializeComponent();
            if (DesignMode)
            {
                return;
            }

            DBLangEngine.DBName = "LangLibTestWinforms.sqlite";
            if (Utils.ShouldLocalize() != null)
            {
                DBLangEngine.InitializeLanguage("LangLibTestWinforms.Messages", Utils.ShouldLocalize(), false);
                return; // After localization don't do anything more.
            }


            DBLangEngine.InitializeLanguage("LangLibTestWinforms.Messages");

            lbFallbackCulture.Text = DBLangEngine.FallBackCulture.ToString();
            lbCurrentSysCulture.Text = System.Globalization.CultureInfo.CurrentCulture.ToString();
            try
            {
                Text = "LangLibTestWinforms - " + ((AssemblyCopyrightAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright;
            }
            catch
            {

            }
            lbLoadSec.Text = DBLangEngine.InitTime.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormAbout().ShowDialog();
            lbLoadSec.Text = DBLangEngine.InitTime.ToString();
        }

        private void btMessageTest_Click(object sender, EventArgs e)
        {
            MessageBox.Show(DBLangEngine.GetMessage("msgTest", "A test message. The last or (|) character in the string {0} splits the message into message and comment part.|A test message (this is the message comment part).", Environment.NewLine));
        }

        /// <summary>
        /// A class to use LangLib for a <see cref="DisplayNameAttribute"/> translation.
        /// Implements the <see cref="System.ComponentModel.DisplayNameAttribute" />
        /// </summary>
        /// <seealso cref="System.ComponentModel.DisplayNameAttribute" />
        public class LangLibDisplayNameAttribute : DisplayNameAttribute
        {
            private readonly string messageName;
            private readonly string defaultText;

            /// <summary>
            /// Initializes a new instance of the <see cref="LangLibDisplayNameAttribute"/> class.
            /// </summary>
            /// <param name="messageName">
            /// The text to look up for a translation from the message resource.
            /// This is also used as the default value for the <see cref="DisplayName"/> property.
            /// </param>
            public LangLibDisplayNameAttribute(string messageName)
            {
                this.messageName = messageName;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="LangLibDisplayNameAttribute"/> class.
            /// </summary>
            /// <param name="defaultText">The default text to use as a <see cref="DisplayName"/> property value.</param>
            /// <param name="messageName">The name of the translated text within the message resource.</param>
            public LangLibDisplayNameAttribute(string defaultText, string messageName)
            {
                this.messageName = messageName;
                this.defaultText = defaultText;
            }

            public override string DisplayName => // this might need a try / catch block..
                // assume the message name to be also the fallback localization..
                DBLangEngine.GetStatMessage(messageName, defaultText ?? messageName);
        }
    }
}
