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
using cor = Xacml.Core;

namespace Xacml.Core.Context
{
    /// <summary>
    /// Represents the ResourceContent node found in the context document.
    /// </summary>
    public class ResourceContentElementReadWrite : XacmlElement
    {
        #region Private members

        /// <summary>
        /// The contents of the resource content as a string.
        /// </summary>
        private string _contents;

        /// <summary>
        /// The XmlDocument for the ResourceContent contents.
        /// </summary>
        private XmlDocument _document;

        /// <summary>
        /// The name table allows a faster reading if the xml document.
        /// </summary>
        private XmlNameTable _nameTable;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new resource content using the specified document.
        /// </summary>
        /// <param name="document">The XmlDocument instantiated with resource content.</param>
        /// <param name="schemaVersion">The version of the schema used to validate this document.</param>
        public ResourceContentElementReadWrite(XmlDocument document, XacmlVersion schemaVersion)
            : base(XacmlSchema.Context, schemaVersion)
        {
            _document = document;
        }

        /// <summary>
        /// Creates a new ResourceContent using the provided XmlReader instance.
        /// </summary>
        /// <param name="reader">The XmlReader instance positioned at the ResourceContent node.</param>
        /// <param name="schemaVersion">The version of the schema used to validate this document.</param>
        public ResourceContentElementReadWrite(XmlReader reader, XacmlVersion schemaVersion)
            : base(XacmlSchema.Context, schemaVersion)
        {
            if (reader == null) throw new ArgumentNullException("reader");
            if (reader.LocalName == ContextSchema.ResourceElement.ResourceContent)
            {
                // Load the node contents
                _contents = reader.ReadInnerXml();
                _nameTable = reader.NameTable;
            }
            else
            {
                throw new Exception(Resource.ResourceManager[Resource.MessageKey.exc_invalid_node_name, reader.LocalName]);
            }
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the XmlDocument for the resource content contents.
        /// </summary>
        public virtual XmlDocument XmlDocument
        {
            get
            {
                if (_document == null)
                {
                    if (_nameTable != null)
                    {
                        _document = new XmlDocument(_nameTable);
                    }
                    else
                    {
                        _document = new XmlDocument();
                    }
                    if (_contents != null && _contents.Length != 0)
                    {
                        _document.LoadXml(_contents);
                    }
                }
                return _document;
            }
            set
            {
                _document = value;
            }
        }
        /// <summary>
        /// Whether the instance is a read only version.
        /// </summary>
        public override bool IsReadOnly
        {
            get { return false; }
        }
        #endregion
    }
}
