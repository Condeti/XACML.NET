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
using pol = Xacml.Core.Policy;
using rtm = Xacml.Core.Runtime;

namespace Xacml.Core.Context
{
	/// <summary>
	/// Represents an attribute found in the context document. This class extends the abstract base class 
	/// StringValue which represents a value found in the context document as a string which must be converted to 
	/// a supported data type.
	/// </summary>
	public class AttributeElement : AttributeElementReadWrite
	{
		#region Constructors

		/// <summary>
		/// Creates an Attribute with the values specified.
		/// </summary>
		/// <param name="attributeId">The attribute id.</param>
		/// <param name="dataType">The data type id.</param>
		/// <param name="issuer">The issuer name.</param>
		/// <param name="issueInstant">The issuer instant.</param>
		/// <param name="value">The value of the attribute.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public AttributeElement( string attributeId, string dataType, string issuer, string issueInstant, string value, XacmlVersion schemaVersion )
			: base( attributeId, dataType, issuer, issueInstant, value, schemaVersion )
		{
		}

		/// <summary>
		/// Clones an Attribute from another attribute.
		/// </summary>
		/// <param name="attributeElement">The attribute id.</param>
		public AttributeElement( AttributeElement attributeElement )
			: base( attributeElement )
		{
		}
		/// <summary>
		/// Creates an Attribute instance using the XmlReader provided.
		/// </summary>
		/// <param name="reader">The XmlReader instance positioned at the Attribute node.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public AttributeElement( XmlReader reader, XacmlVersion schemaVersion )
			: base( reader, schemaVersion )
		{
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The attribute id.
		/// </summary>
		public override string AttributeId
		{
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// The data type of the attribute.
		/// </summary>
		public override string DataType
		{
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// The issuer of the attribute.
		/// </summary>
		public override string Issuer
		{
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// The time when the attribute was issued.
		/// </summary>
		public override string IssueInstant
		{
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// The attribute values.
		/// </summary>
		public override AttributeValueElementReadWriteCollection AttributeValues
		{
			set{ throw new NotSupportedException(); }
			get
			{
				return new AttributeValueElementCollection( base.AttributeValues );
			}
		}

		/// <summary>
		/// The value of the attribute.
		/// </summary>
		public override string Value
		{
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// The data type for the attribute value.
		/// </summary>
		public override string DataTypeValue
		{
			set{ throw new NotSupportedException(); }
		}
		/// <summary>
		/// Whether the instance is a read only version.
		/// </summary>
		public override bool IsReadOnly
		{
			get{ return true; }
		}
		#endregion
	}
}
