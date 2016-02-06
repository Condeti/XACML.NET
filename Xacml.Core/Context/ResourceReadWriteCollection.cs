/* ***** BEGIN LICENSE BLOCK *****
 * Version: MPL 1.1/GPL 2.0/LGPL 2.1
 *
 * The contents of this file are Resource to the Mozilla Public License Version
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
using System.Collections;
using System.Xml;
using cor = Xacml.Core;

namespace Xacml.Core.Context
{
	/// <summary>
	/// Defines a typed collection of Resources.
	/// </summary>
	public class ResourceReadWriteCollection : CollectionBase 
	{
		#region CollectionBase members

		/// <summary>
		/// Gets or sets the element at the specified index.
		/// </summary>
		/// <value>The element at the specified index.</value>
		public virtual ResourceElementReadWrite this[ int index ]  
		{
			get  
			{
				return( (ResourceElementReadWrite) List[index] );
			}
			set
			{
				List[index]	= value;
			}
		}

		/// <summary>
		/// Adds an object to the end of the CollectionBase.
		/// </summary>
		/// <param name="value">The Object to be added to the end of the CollectionBase. </param>
		/// <returns>The CollectionBase index at which the value has been added.</returns>
		public virtual int Add( ResourceElementReadWrite value )  
		{
			return( List.Add( value ) );
		}

		/// <summary>
		/// Clears the collection
		/// </summary>
		public virtual new void Clear()
		{
			base.Clear();
		}
		/// <summary>
		/// Removes the specified element
		/// </summary>
		/// <param name="index">Position of the element</param>
		public virtual new void RemoveAt ( int index )
		{
			base.RemoveAt(index);
		}

		/// <summary>
		/// Gets the index of the given ResourceElementReadWrite in the collection
		/// </summary>
		/// <param name="resource"></param>
		/// <returns></returns>
		public int GetIndex( ResourceElementReadWrite resource )
		{
			for( int i=0; i<this.Count; i++ )
			{
				if( this.List[i] == resource )
				{
					return i;
				}
			}
			return -1;
		}
		#endregion

		#region Public methods

		/// <summary>
		/// Writes the element in the provided writer
		/// </summary>
		/// <param name="writer">The XmlWriter in which the element will be written</param>
		public void WriteDocument( XmlWriter writer )
		{
            if (writer == null) throw new ArgumentNullException("writer");
			foreach( ResourceElementReadWrite resource in this.List )
			{
				writer.WriteStartElement( ContextSchema.RequestElement.Resource );

				if( resource.ResourceContent != null )
				{
					writer.WriteStartElement( ContextSchema.ResourceElement.ResourceContent );
					writer.WriteRaw( resource.ResourceContent.XmlDocument.InnerXml );
					writer.WriteEndElement();
				}
				foreach( AttributeElementReadWrite attr in resource.Attributes )
				{
					writer.WriteStartElement( ContextSchema.AttributeElement.Attribute );
					writer.WriteAttributeString( ContextSchema.AttributeElement.AttributeId, attr.AttributeId );
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
		}

		#endregion
	}
}