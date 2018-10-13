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

namespace DBLocalization
{
    public partial class FormEditCell : Form
    {
        public FormEditCell()
        {
            InitializeComponent();
        }
        public static bool Execute(ref string valueStr)
        {
            FormEditCell frm = new FormEditCell();
            frm.tbEdit.Text = valueStr;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                valueStr = frm.tbEdit.Text;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btFont_Click(object sender, EventArgs e)
        {
            if (tbEdit.Font.FontFamily.Name == "Courier New")
            {
                tbEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btFont.Text = "Microsoft Sans Serif";
            }
            else
            {
                tbEdit.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btFont.Text = "Courier New";
            }
        }
    }

}
