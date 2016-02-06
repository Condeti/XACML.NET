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
 * either of the GNU General Public License Version 2 or later  (the "GPL"),
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

namespace Xacml.Core.Policy
{
	/// <summary>
	/// This abstract base class is used to unify the processing of the "target item", Resource, Action and 
	/// Subject nodes found in the policy document with a read-only access. As all those nodes have a similar representation in the 
	/// policy document they can be loaded by the same code. 
	/// </summary>
	public abstract class TargetItemBase : TargetItemBaseReadWrite
	{
		#region Constructors

		/// <summary>
		/// Private default constructor to avoid default instantiation.
		/// </summary>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		protected TargetItemBase( XacmlVersion schemaVersion )
			: base( schemaVersion )
		{
		}

		/// <summary>
		/// Creates a new instance of TargetItem using the specified arguments.
		/// </summary>
		/// <param name="match">The collection of target item matchs.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		protected TargetItemBase( TargetMatchReadWriteCollection match, XacmlVersion schemaVersion )
			: base( match, schemaVersion )
		{
		}

		/// <summary>
		/// Creates an instance of the TargetItem using the XmlReader instance provided and the node names provided
		/// by the extended class which allows this code being independent of the node names.
		/// </summary>
		/// <param name="reader">The XmlReader instance positioned at the "target item" node (Resource, Action or 
		/// Subject).</param>
		/// <param name="targetItemNodeName">The name of the node that represents the "target item".</param>
		/// <param name="matchNodeName">The name of the node that represents the Match element for the "target item" 
		/// being loaded.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		protected TargetItemBase( XmlReader reader, string targetItemNodeName, string matchNodeName, XacmlVersion schemaVersion )
			: base( reader, targetItemNodeName, matchNodeName, schemaVersion )
		{
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The list of Match elements found in the target item node.
		/// </summary>
		public override TargetMatchReadWriteCollection Match
		{
			set{ throw new NotSupportedException(); }
			get{ return new TargetMatchCollection( base.Match ); }
		}

		#endregion
	}
}
