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
using cor = Xacml.Core;

namespace Xacml.Core.Context
{
	/// <summary>
	/// Represents an attribute value element found in the context document. This class extends the abstract base 
	/// class StringValue because this class contains string data which may be converted to a typed value.
	/// </summary>
	public class AttributeValueElementReadWrite : pol.StringValueBase
	{
		#region Private members

		/// <summary>
		/// The contents of the attribute value as string.
		/// </summary>
		private string _contents;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an instance of the AttributeValue using the XmlReader provided.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the AttributeValue node.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public AttributeValueElementReadWrite( XmlReader reader, XacmlVersion schemaVersion )
			: base( XacmlSchema.Context, schemaVersion )
		{
            if (reader == null) throw new ArgumentNullException("reader");
			if( reader.LocalName == ContextSchema.AttributeElement.AttributeValue &&
				ValidateSchema( reader, schemaVersion ) )
			{
				// Load the node contents
				_contents = reader.ReadInnerXml();
			}
			else
			{
				throw new Exception( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_node_name, reader.LocalName ] );
			}
		}

		/// <summary>
		/// Creates an instance of an attribute value using the given string as its contents.
		/// </summary>
		/// <param name="value">The string value for this attribute element.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public AttributeValueElementReadWrite( string value, XacmlVersion schemaVersion )
			: base( XacmlSchema.Context, schemaVersion )
		{
			_contents = value;
		}

		/// <summary>
		/// Clones an attribute value element into a new element.
		/// </summary>
		/// <param name="attributeValueElement">The value element to clone.</param>
		public AttributeValueElementReadWrite( AttributeValueElementReadWrite attributeValueElement )
			: base( XacmlSchema.Context, attributeValueElement.SchemaVersion )
		{
            if (attributeValueElement == null) throw new ArgumentNullException("attributeValueElement");
			_contents = attributeValueElement._contents;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The contents as string value.
		/// </summary>
		public virtual string Contents
		{
			get{ return _contents; }
			set{ _contents = value; }
		}

		/// <summary>
		/// The contents as string value.
		/// </summary>
		public override string Value
		{
			set{ _contents = value; }
			get{ return _contents; }
		}

		/// <summary>
		/// The data type of the value, it returns null, since the AttributeValue node does specify the data type.
		/// </summary>
		public override string DataTypeValue
		{
			set{  }
			get{ return null; }
		}
		/// <summary>
		/// Whether the instance is a read only version.
		/// </summary>
		public override bool IsReadOnly
		{
			get{ return false; }
		}
		#endregion
	}
}
