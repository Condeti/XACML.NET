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
	/// Represents a RuleCombinerParameter defined in the policy document.
	/// </summary>
	public class RuleCombinerParameterElement : CombinerParameterElement
	{
		#region Private members

		/// <summary>
		/// The parameter name.
		/// </summary>
		private Uri _ruleIdRef;

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a new RuleCombinerParameter using the provided argument values.
		/// </summary>
		/// <param name="parameterName">The parameter name.</param>
		/// <param name="attributeValue">The attribute value.</param>
		/// <param name="ruleIdRef">The rule Id reference.</param>
		/// <param name="version">The version of the schema that was used to validate.</param>
		public RuleCombinerParameterElement( string parameterName, AttributeValueElement attributeValue, Uri ruleIdRef, XacmlVersion version )
			: base( parameterName, attributeValue, version ) 
		{
			_ruleIdRef = ruleIdRef;
		}

		/// <summary>
		/// Creates a new RuleCombinerParameter using the XmlReader instance provided.
		/// </summary>
		/// <param name="reader">The XmlReader instance positioned at the CombinerParameterElement node.</param>
		/// <param name="version">The version of the schema that was used to validate.</param>
		public RuleCombinerParameterElement( XmlReader reader, XacmlVersion version )
			: base( reader, PolicySchema2.RuleCombinerParameterElement.RuleCombinerParameter, version )
		{
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The parameter name
		/// </summary>
		public Uri RuleIdRef
		{
			get{ return _ruleIdRef; }
		}

		#endregion

		#region Protected methods

		/// <summary>
		/// Method called by the base class when unknown attributes are found during parsing of this element. 
		/// </summary>
		/// <param name="reader">The reader positioned at the attribute</param>
		protected override void AttributeFound( XmlReader reader )
		{
			if( reader.LocalName == PolicySchema2.RuleCombinerParameterElement.RuleIdRef )
			{
				_ruleIdRef = new Uri( reader.GetAttribute( PolicySchema2.RuleCombinerParameterElement.RuleIdRef ) );
			}
		}

		#endregion
	}
}
