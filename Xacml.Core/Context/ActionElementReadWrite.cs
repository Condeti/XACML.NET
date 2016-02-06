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

namespace Xacml.Core.Context
{
	/// <summary>
	/// Represents an Action node found in the context document. This class extends the abstract base class 
	/// TargetItem which loads the "target item" definition.
	/// </summary>
	public class ActionElementReadWrite : TargetItemBase
	{
		#region Constructors

		/// <summary>
		/// Creates a Action using the specified arguments.
		/// </summary>
		/// <param name="attributes">The attribute list.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public ActionElementReadWrite( AttributeReadWriteCollection attributes, XacmlVersion schemaVersion ) 
			: base( attributes, schemaVersion )
		{
		}

		/// <summary>
		/// Creates an instance of the Action class using the XmlReader instance provided.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the Action node.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public ActionElementReadWrite( XmlReader reader, XacmlVersion schemaVersion ) : 
			base( reader, ContextSchema.RequestElement.Action, schemaVersion )
		{
		}

		#endregion

		#region Protected methods

		/// <summary>
		/// This method is called by the TargetItem class when an attribute is found. This class ignores this method.
		/// </summary>
		/// <param name="namespaceName">The namespace for the attrbute.</param>
		/// <param name="attributeName">The attribute name found.</param>
		/// <param name="attributeValue">The attribute value found.</param>
		protected override void AttributeFound( string namespaceName, string attributeName, string attributeValue )
		{
		}

		/// <summary>
		/// This method is called by the TargetItem class when an element is found. This class ignores this method.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the element.</param>
		protected override void NodeFound(XmlReader reader)
		{
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Writes the element in the provided writer.
		/// </summary>
		/// <param name="writer">The instance to write the Xml to.</param>
		public void WriteDocument( XmlWriter writer )
		{
            if (writer == null) throw new ArgumentNullException("writer");
			writer.WriteStartElement( ContextSchema.RequestElement.Action);
			foreach( AttributeElementReadWrite attr in this.Attributes )
			{
				writer.WriteStartElement( ContextSchema.AttributeElement.Attribute );
				writer.WriteAttributeString( ContextSchema.AttributeElement.AttributeId, attr.AttributeId  );
				writer.WriteAttributeString( ContextSchema.AttributeElement.DataType, attr.DataType );
				if( attr.Issuer != null && attr.Issuer.Length > 0 )
				{
					writer.WriteAttributeString( ContextSchema.AttributeElement.Issuer, attr.Issuer );
				}
				foreach( AttributeValueElementReadWrite attVal in attr.AttributeValues )
				{
					writer.WriteElementString( ContextSchema.AttributeElement.AttributeValue, attVal.Value );
				}
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		#endregion
	}
}