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

namespace Xacml.Core.Policy
{
	/// <summary>
	/// Represents a read-only Environments node defined in the policy document. This class extends the 
	/// abstract base class TargetItems which defines the elements of the Resources, Actions,
	/// Subjets and Environments nodes.
	/// </summary>
	public class EnvironmentsElement : EnvironmentsElementReadWrite
	{
		#region Constructors

		/// <summary>
		/// Creates a new Environments with the specified aguments.
		/// </summary>
		/// <param name="anyItem">Whether the target item is defined for any item.</param>
		/// <param name="items">The taregt items.</param>
		/// <param name="version">The version of the schema that was used to validate.</param>
		public EnvironmentsElement( bool anyItem, TargetItemReadWriteCollection items, XacmlVersion version )
			: base( anyItem, items, version )
		{
		}

		/// <summary>
		/// Creates an instance of the Environments class using the XmlReader instance provided.
		/// </summary>
		/// <remarks>
		/// This constructor is also calling the base class constructor specifying the XmlReader
		/// and the node names that defines this TargetItmes extended class.
		/// </remarks>
		/// <param name="reader">The XmlReader positioned at the Environments node.</param>
		/// <param name="version">The version of the schema that was used to validate.</param>
		public EnvironmentsElement( XmlReader reader, XacmlVersion version ) 
			: base( reader, version )
		{
		}

		#endregion

		#region Protected methods

		/// <summary>
		/// Creates an instance of the containing element of the Environment class. This method is 
		/// called by the TargetItems base class when an element that identifies a Environment is 
		/// found.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the Environment node.</param>
		/// <returns>A new instance of the Environment class.</returns>
		protected override TargetItemBaseReadWrite CreateTargetItem( XmlReader reader )
		{
			return new EnvironmentElement( reader, SchemaVersion );
		}

		#endregion

		#region Public properties
		/// <summary>
		/// Gets the items
		/// </summary>
		public override TargetItemReadWriteCollection ItemsList
		{
			get{ return new TargetItemCollection(base.ItemsList) ;}
			set{ throw new NotSupportedException(); }
		}
		#endregion
	}
}
