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
	/// Represents a read/write Subject node defined in the policy document. This class extends TargetItem
	/// since Subject, Action and Resource share a similar document structure.
	/// </summary>
	public class SubjectElementReadWrite : TargetItemBaseReadWrite
	{
		#region Constructors

		/// <summary>
		/// Creates a new Subject with the specified aguments.
		/// </summary>
		/// <param name="match">The target item match collection.</param>
		/// <param name="version">The version of the schema that was used to validate.</param>
		public SubjectElementReadWrite( TargetMatchReadWriteCollection match, XacmlVersion version )
			: base( match, version )
		{
		}

		/// <summary>
		/// Creates a new Subject using the XmlReader instance provided.
		/// </summary>
		/// <remarks>This constructor is also calling the base class constructor specifying the
		/// XmlReader instance and the node names for the Subject element and the 
		/// ResourceMatch.</remarks>
		/// <param name="reader">An XmlReader positioned at the Subject node.</param>
		/// <param name="version">The version of the schema that was used to validate.</param>
		public SubjectElementReadWrite( XmlReader reader, XacmlVersion version ) : 
			base( reader, PolicySchema1.SubjectElement.Subject, PolicySchema1.SubjectElement.SubjectMatch, version )
		{
		}

		#endregion

		#region Protected methods

		/// <summary>
		/// This method is called by the TargetItem class when a SubjectMatch element is found. 
		/// This method creates an instance of the SubjectMatch class using the XmlReader 
		/// provided.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the begining of the SubjectMatch
		/// node.</param>
		/// <returns>A new instance of the SubjectMatch element.</returns>
		protected override TargetMatchBaseReadWrite CreateMatch(XmlReader reader)
		{
			return new SubjectMatchElementReadWrite( reader, SchemaVersion );
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
