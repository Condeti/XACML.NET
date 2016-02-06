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
using pol = Xacml.Core.Policy;
using cor = Xacml.Core;

namespace Xacml.Core.Policy
{
	/// <summary>
	/// This abstract base class is used to abstract the loading of the "target item list" found in the policy 
	/// document. Since the Resources, Actions and Subjects read-only nodes have a similar node structure they can be loaded 
	/// with the same code where the node names are changed by the derived class.
	/// </summary>
	public abstract class TargetItemsBase : TargetItemsBaseReadWrite
	{

		#region Constructor

		/// <summary>
		/// Private default constructor to avoid default instantiation.
		/// </summary>
		protected TargetItemsBase( XacmlVersion schemaVersion )
			: base( schemaVersion )
		{
		}

		/// <summary>
		/// Creates a new TargetItem collection with the specified aguments.
		/// </summary>
		/// <param name="anyItem">Whether the target item is defined for any item</param>
		/// <param name="items">The taregt items.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		protected TargetItemsBase( bool anyItem, TargetItemCollection items, XacmlVersion schemaVersion )
			: base( anyItem, items, schemaVersion )
		{
		}

		/// <summary>
		/// Creates a new TargetItem instance using the specified XmlReader instance provided, and the node names of 
		/// the "target item list" nodes which are provided by the derived class during construction. 
		/// </summary>
		/// <param name="reader">The XmlReader instance positioned at the "target item list" node.</param>
		/// <param name="itemsNodeName">The name of the "target item list" node.</param>
		/// <param name="anyItemNodeName">The name of the AnyXxxx node for this "target item list" node.</param>
		/// <param name="itemNodeName">The name of the "target item" node that can be defined within this "target 
		/// item list" node.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		protected TargetItemsBase( XmlReader reader, string itemsNodeName, string anyItemNodeName, string itemNodeName, XacmlVersion schemaVersion )
			: base( schemaVersion )
		{
            if (reader == null) throw new ArgumentNullException("reader");
			if( reader.LocalName == itemsNodeName && ValidateSchema( reader, schemaVersion ) )
			{
				while( reader.Read() )
				{
					if( reader.LocalName == anyItemNodeName && ValidateSchema( reader, schemaVersion ) )
					{
						base.IsAny = true;
					}
					else if( reader.LocalName == itemNodeName && ValidateSchema( reader, schemaVersion ) )
					{
						base.ItemsList.Add( (TargetItemBase)CreateTargetItem( reader ) );
					}
					else if( reader.LocalName == itemsNodeName && 
						reader.NodeType == XmlNodeType.EndElement )
					{
						break;
					}
				}
			}
			else
			{
				throw new Exception( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_node_name, reader.LocalName ] );
			}
		}


		#endregion

		#region Public properties
/*
		/// <summary>
		/// The list of "target item"s defined in the "target item list" node.
		/// </summary>
		public override ReadWriteTargetItemCollection ItemsList
		{
			get{ return new TargetItemCollection(base.ItemsList) ;}
		}
*/
		#endregion
	}
}
