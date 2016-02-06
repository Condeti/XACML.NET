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
	/// Represents a read-only Match found in an Action element in the Policy document. This class extends the abstract 
	/// Match class which is used to mantain any Match element found in a target item.
	/// </summary>
	public class ActionMatchElement : TargetMatchBase
	{
		#region Constructors

		/// <summary>
		/// Creates an instance of the ActionMatch class using the specified arguments.
		/// </summary>
		/// <param name="matchId">The function id for this match.</param>
		/// <param name="attributeValue">The attribute value to use as the first parameter to the function.</param>
		/// <param name="attributeReference">The attribute reference in the context document.</param>
		/// <param name="version">The version of the schema that was used to validate.</param>
		public ActionMatchElement( string matchId, AttributeValueElementReadWrite attributeValue, AttributeReferenceBase attributeReference, XacmlVersion version ) : 
			base( matchId, attributeValue, attributeReference, version )
		{
		}

		/// <summary>
		/// Creates an instance of the ActionMatch class and also calls the base class constructor specifying the
		/// XmlReader, and the names of the node defined in this action item match.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the start of the Match element.</param>
		/// <param name="version">The version of the schema that was used to validate.</param>
		public ActionMatchElement( XmlReader reader, XacmlVersion version ) : 
			base( reader, PolicySchema1.ActionElement.ActionMatch, PolicySchema1.ActionElement.ActionAttributeDesignator, version )
		{
		}

		#endregion

		#region Protected methods

		/// <summary>
		/// Creates an instance of the ActionAttributeDesignator when the element is found during the Match node
		/// is being processed.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the start of the ActionAttributeDesignatot node.</param>
		/// <returns>An instance of the ActionAttributeDesignator class.</returns>
		protected override AttributeDesignatorBase CreateAttributeDesignator( XmlReader reader )
		{
			return new ActionAttributeDesignatorElement( reader, SchemaVersion );
		}

		#endregion

		#region Public properties
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
