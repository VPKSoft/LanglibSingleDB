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

namespace VPKSoft.LangLib
{
    /// <summary>
    /// A class to store a single message used in the LangLib.
    /// </summary>
    public class StringMessages
    {
        /// <summary>
        /// The name of the message.
        /// </summary>
        private string messageName;

        /// <summary>
        /// The message itself.
        /// </summary>
        private string message;

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message
        {
            get
            {
                return message;
            }
        }

        /// <summary>
        /// Gets the name of the message.
        /// </summary>
        public string MessageName
        {
            get
            {
                return messageName;
            }
        }

        /// <summary>
        /// StringMessages class constructor.
        /// </summary>
        /// <param name="messageName">The name of the message.</param>
        /// <param name="message">The message itself.</param>
        public StringMessages(string messageName, string message)
        {
            this.message = message;
            this.messageName = messageName;
        }
    }
}
