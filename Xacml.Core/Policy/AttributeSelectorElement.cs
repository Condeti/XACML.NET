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
using ctx = Xacml.Core.Context;
using cor = Xacml.Core;

namespace Xacml.Core.Policy
{
	/// <summary>
	/// Represents an AttributeSelector node found in the Policy document. This element defines an XPath query that
	/// is executed over the Context document and the results are used as values during the evaluation.
	/// </summary>
	public class AttributeSelectorElement : AttributeReferenceBase
	{
		#region Private members

		/// <summary>
		/// Mantains the XPath query used to search for the value.
		/// </summary>
		private string _requestContextPath;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an instance of the AttributeSelector.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the AttributeSelector node.</param>
		/// <param name="version">The version of the schema that was used to validate.</param>
		public AttributeSelectorElement( XmlReader reader, XacmlVersion version ) 
			: base( reader, version )
		{
			if( reader.LocalName == PolicySchema1.AttributeSelectorElement.AttributeSelector && 
				ValidateSchema( reader, version ) )
			{
				_requestContextPath = reader.GetAttribute( PolicySchema1.AttributeSelectorElement.RequestContextPath );
			}
			else
			{
				throw new Exception( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_node_name, reader.LocalName ] );
			}
		}

		/// <summary>
		/// Creates an instance of the AttibuteSelector with the provided values
		/// </summary>
		/// <param name="dataType"></param>
		/// <param name="mustBePresent"></param>
		/// <param name="requestContextPath"></param>
		/// <param name="schemaVersion"></param>
		public AttributeSelectorElement( string dataType, bool mustBePresent, string requestContextPath, XacmlVersion schemaVersion ) : base( dataType, mustBePresent, schemaVersion )
		{
			_requestContextPath = requestContextPath;
		}

		#endregion
		
		#region Public properties

		/// <summary>
		/// Gets the XPath query used to search for the Context node values.
		/// </summary>
		public string RequestContextPath
		{
			get{ return _requestContextPath; }
			set{ _requestContextPath = value; }
		}
		/// <summary>
		/// Whether the instance is a read only version.
		/// </summary>
		public override bool IsReadOnly
		{
			get{ return false; }
		}
		#endregion

		#region Public methods

		/// <summary>
		/// Writes the XML of the current element
		/// </summary>
		/// <param name="writer">The XmlWriter in which the element will be written</param>
		public override void WriteDocument(XmlWriter writer)
		{
			writer.WriteStartElement(PolicySchema1.AttributeSelectorElement.AttributeSelector);
			writer.WriteAttributeString(PolicySchema1.AttributeSelectorElement.RequestContextPath,this._requestContextPath);
			writer.WriteAttributeString(PolicySchema1.AttributeSelectorElement.DataType,this.DataType);
			writer.WriteEndElement();
		}

		#endregion
	}
}
