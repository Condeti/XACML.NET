/* ***** BEGIN LICENSE BLOCK *****
 * Version: MPL 1.1/GPL 2.0/LGPL 2.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is www.com code.
 *
 * The Initial Developer of the Original Code is
 * Lagash Systems SA.
 * Portions created by the Initial Developer are Copyright (C) 2004
 * the Initial Developer. All Rights Reserved.
 *
 * Contributor(s):
 *   Diego Gonzalez <diegog@com>
 *
 * Alternatively, the contents of this file may be used under the terms of
 * either of the GNU General Public License Version 2 or later (the "GPL"),
 * or the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
 * in which case the provisions of the GPL or the LGPL are applicable instead
 * of those above. If you wish to allow use of your version of this file only
 * under the terms of either the GPL or the LGPL, and not to allow others to
 * use your version of this file under the terms of the MPL, indicate your
 * decision by deleting the provisions above and replace them with the notice
 * and other provisions required by the GPL or the LGPL. If you do not delete
 * the provisions above, a recipient may use your version of this file under
 * the terms of any one of the MPL, the GPL or the LGPL.
 *
 * ***** END LICENSE BLOCK ***** */

using System;
using System.Xml;
using inf = Xacml.Core.Interfaces;

namespace Xacml.Core.Policy
{
    /// <summary>
    /// Represents a generic attribute referencing element found in the Policy document.  The only elements that are
    /// referencing other elements are: AttributeDesignator and AttributeSelector.
    /// </summary>
    public abstract class AttributeReferenceBase : XacmlElement, inf.IExpression
    {
        #region Private members

        /// <summary>
        /// The data type of the referenced attribute.
        /// </summary>
        private string _dataType;

        /// <summary>
        /// Whether the attribute must be present or not.
        /// </summary>
        private bool _mustBePresent;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instance of the AttributeReference using the data type and the flag specified.
        /// </summary>
        /// <param name="dataType">The data type id.</param>
        /// <param name="mustBePresent">Whether the attribute must be present.</param>
        /// <param name="schemaVersion">The version of the schema that was used to validate.</param>
        protected AttributeReferenceBase(string dataType, bool mustBePresent, XacmlVersion schemaVersion)
            : base(XacmlSchema.Policy, schemaVersion)
        {
            _dataType = dataType;
            _mustBePresent = mustBePresent;
        }

        /// <summary>
        /// Creates an instance of the AttributeReference using the XmlNode specified.
        /// </summary>
        /// <param name="reader">The XmlReader positioned at the attribute reference node.</param>
        /// <param name="schemaVersion">The version of the schema that was used to validate.</param>
        protected AttributeReferenceBase(XmlReader reader, XacmlVersion schemaVersion)
            : base(XacmlSchema.Policy, schemaVersion)
        {
            if (reader == null) throw new ArgumentNullException("reader");
            _dataType = reader.GetAttribute(PolicySchema1.AttributeSelectorElement.DataType);
            string mustBePresent = reader.GetAttribute(PolicySchema1.AttributeSelectorElement.MustBePresent);
            if (mustBePresent == null || mustBePresent.Length == 0)
            {
                _mustBePresent = false;
            }
            else
            {
                _mustBePresent = bool.Parse(mustBePresent);
            }
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the data type of the referenced node
        /// </summary>
        public string DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }

        /// <summary>
        /// Whether the referenced value must be present or not.
        /// </summary>
        public bool MustBePresent
        {
            get { return _mustBePresent; }
            set { _mustBePresent = value; }
        }

        #endregion

        #region Abstract methods

        /// <summary>
        /// Writes the XML of the current element
        /// </summary>
        /// <param name="writer">The XmlWriter in which the element will be written</param>
        public abstract void WriteDocument(XmlWriter writer);

        #endregion
    }
}