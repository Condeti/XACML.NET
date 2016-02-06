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

using System.Xml;

namespace Xacml.Core.Policy
{
	/// <summary>
	/// Represents a read/write Environment element in the Policy document. This class is a specialization of TargetItem class
	/// which contains the information needed for all the items that can be part of the target.
	/// </summary>
	public class EnvironmentElementReadWrite : TargetItemBaseReadWrite
	{
		#region Constructor

		/// <summary>
		/// Creates a new instance of Environment using the specified arguments.
		/// </summary>
		/// <param name="match">The target item match collection.</param>
		/// <param name="version">The version of the schema that was used to validate.</param>
		public EnvironmentElementReadWrite( TargetMatchReadWriteCollection match, XacmlVersion version ) : 
			base( match, version )
		{
		}

		/// <summary>
		/// Creates an instance of the Environment item and calls the base constructor specifying the names of the nodes
		/// that defines this target item.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the Environment node.</param>
		/// <param name="version">The version of the schema that was used to validate.</param>
		public EnvironmentElementReadWrite( XmlReader reader, XacmlVersion version ) : 
			base( reader, PolicySchema2.EnvironmentElement.Environment, PolicySchema2.EnvironmentElement.EnvironmentMatch, version )
		{
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Overrided method that is called when the xxxMatch element is found in the target item definition.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the start of the Match element found.</param>
		/// <returns>The instance of the ActionMatch which is a class extending the abstract Match class</returns>
		protected override TargetMatchBaseReadWrite CreateMatch(XmlReader reader)
		{
			return new EnvironmentMatchElementReadWrite( reader, SchemaVersion );
		}

		#endregion

		#region Public properties
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
