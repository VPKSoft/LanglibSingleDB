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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPKSoft.LangLib;

namespace DBLocalization
{
    public partial class AddFromCulture : Form
    {
        public AddFromCulture()
        {
            InitializeComponent();
        }

        public static bool Execute(List<Culture> cultures, Culture current, out Culture selected)
        {
            AddFromCulture ac = new AddFromCulture();
            ac.cbCulture.Items.AddRange(cultures.ToArray());
            for (int i = ac.cbCulture.Items.Count - 1; i >= 0; i--)
            {
                if (((Culture)ac.cbCulture.Items[i]).LCID == current.LCID)
                {
                    ac.cbCulture.Items.RemoveAt(i);
                    break;
                }
            }

            for (int i = 0; i < ac.cbCulture.Items.Count; i++)
            {
                ac.cbCulture.AutoCompleteCustomSource.Add((ac.cbCulture.Items[i] as Culture).NativeName);
            }

            bool retVal = ac.ShowDialog() == DialogResult.OK;
            selected = (Culture)ac.cbCulture.SelectedItem;
            return retVal;
        }

        private void cbCulture_SelectedIndexChanged(object sender, EventArgs e)
        {
            btOK.Enabled = true;
        }
    }
}
