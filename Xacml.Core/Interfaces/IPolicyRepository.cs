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
using pol = Xacml.Core.Policy;
using ctx = Xacml.Core.Context;
using rtm = Xacml.Core.Runtime;

namespace Xacml.Core.Interfaces
{
	/// <summary>
	/// Defines a generic policy repository used to load policy documents.
	/// </summary>
	public interface IPolicyRepository
	{
		/// <summary>
		/// Initialization method called once during the EvaluationEngine startup.
		/// </summary>
		/// <param name="configNode">The XmlNode that defines the repository in the configuration file.</param>
		void Init( XmlNode configNode );

		/// <summary>
		/// Returns a loaded policy document using the PolicyIdReference instance.
		/// </summary>
		/// <param name="policyReference">The policy id that must be loaded.</param>
		/// <returns>The loaded policy document if it's found, otherwise returns null.</returns>
		pol.PolicyElement GetPolicy( pol.PolicyIdReferenceElement policyReference );

		/// <summary>
		/// Returns a loaded policy set document using the PolicySetIdReference instance.
		/// </summary>
		/// <param name="policySetReference">The policy id that must be loaded.</param>
		/// <returns>The loaded policy document if it's found, otherwise returns null.</returns>
		pol.PolicySetElement GetPolicySet( pol.PolicySetIdReferenceElement policySetReference );

		/// <summary>
		/// If the policy document is not provided to the EvaluationEngine in order to evaluate a context request,
		/// the EvaluationEngine will call this method to find a policydocument which target matches the context 
		/// document information.
		/// </summary>
		/// <param name="context">The evaluation context instance.</param>
		/// <returns></returns>
		pol.PolicyDocument Match( rtm.EvaluationContext context );
	}
}
