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
using cor = Xacml.Core;

namespace Xacml.Core.Context
{
	/// <summary>
	/// Defines a typed collection of Attribute.
	/// </summary>
	public class AttributeCollection : AttributeReadWriteCollection 
	{
		#region Constructor
		/// <summary>
		/// Creates a AttributeCollection, with the items contained in a AttributeReadWriteCollection
		/// </summary>
		/// <param name="items"></param>
		public AttributeCollection(AttributeReadWriteCollection items)
		{
            if (items == null) throw new ArgumentNullException("items");
			foreach( AttributeElementReadWrite item in items )
			{
				this.List.Add( new AttributeElement( item.AttributeId, item.DataType, item.Issuer, item.IssueInstant,
					item.Value, item.SchemaVersion) );
			}
		}
		/// <summary>
		/// Creates a new blank AttributeCollection
		/// </summary>
		public AttributeCollection()
		{
		}
		#endregion

		#region CollectionBase members

		/// <summary>
		/// Gets or sets the element at the specified index.
		/// </summary>
		/// <value>The element at the specified index.</value>
		public override AttributeElementReadWrite this[ int index ]  
		{
			get  
			{
				return( (AttributeElement) List[index] );
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>
		/// Adds an object to the end of the CollectionBase.
		/// </summary>
		/// <param name="value">The Object to be added to the end of the CollectionBase. </param>
		/// <returns>The CollectionBase index at which the value has been added.</returns>
		public override int Add( AttributeElementReadWrite value )  
		{
            if (value == null) throw new ArgumentNullException("value");
			return( List.Add( new AttributeElement( value.AttributeId, value.DataType, value.Issuer, value.IssueInstant, value.Value, 
				value.SchemaVersion) ) );
		}
		/// <summary>
		/// Clears the collection
		/// </summary>
		public override void Clear()
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Removes the specified element
		/// </summary>
		/// <param name="index">Position of the element</param>
		public override void RemoveAt ( int index )
		{
			throw new NotSupportedException();
		}

		#endregion
	}
}
