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
using System.Collections;
using System.IO;
using System.Security.Permissions;
using System.Xml;
using pol = Xacml.Core.Policy;
using ctx = Xacml.Core.Context;
using rtm = Xacml.Core.Runtime;
using inf = Xacml.Core.Interfaces;
using cor = Xacml.Core;

namespace Xacml.Core.Extensions
{
	/// <summary>
	/// The default policy repository implementation that uses the configuration file to establish all the policies 
	/// in the repository.
	/// </summary>
	public class DefaultPolicyRepository : inf.IPolicyRepository
	{
		#region Private members

		/// <summary>
		/// All the policies defined in the configuration file.
		/// </summary>
		private Hashtable _policies = new Hashtable();

		/// <summary>
		/// All the policy sets defined in the configuration file.
		/// </summary>
		private Hashtable _policySets = new Hashtable();
		
		#endregion

		#region IPolicyRepository Members

		/// <summary>
		/// Initialization method called by the EvaluationEngine to initialize the extension.
		/// </summary>
		/// <param name="configNode">The XmlNode that defines the extension in the configuration file.</param>
#if NET10
		[ReflectionPermission( SecurityAction.Demand, Flags = ReflectionPermissionFlag.TypeInformation )]
#endif
#if NET20
		[ReflectionPermission( SecurityAction.Demand, Flags = ReflectionPermissionFlag.MemberAccess )]
#endif
		public void Init( XmlNode configNode )
		{
            if (configNode == null) throw new ArgumentNullException("configNode");
			foreach( XmlNode node in configNode.ChildNodes )
			{
				if( node.Name == "PolicySet" )
				{
					string policyId = node.Attributes[ "PolicySetId" ].Value;
					string filePath = node.Attributes[ "FilePath" ].Value;
					string version = node.Attributes[ "xacmlVersion" ].Value;
					XacmlVersion schemaVersion = (XacmlVersion)Enum.Parse( typeof(XacmlVersion), version, false );
					_policySets.Add( policyId, new pol.PolicyDocument( new XmlTextReader( new StreamReader( filePath ) ), schemaVersion ) );
				}
				else if( node.Name == "Policy" )
				{
					string policyId = node.Attributes[ "PolicyId" ].Value;
					string filePath = node.Attributes[ "FilePath" ].Value;		
					string version = node.Attributes[ "xacmlVersion" ].Value;
					XacmlVersion schemaVersion = (XacmlVersion)Enum.Parse( typeof(XacmlVersion), version, false );
					_policies.Add( policyId, new pol.PolicyDocument( new XmlTextReader( new StreamReader( filePath ) ), schemaVersion ) );
				}
			}
		}

		/// <summary>
		/// Returns a policy document using the PolicyRefereneId specified.
		/// </summary>
		/// <param name="policyReference">The policy reference with the Id of the policy searched.</param>
		/// <returns>The policy document.</returns>
		public pol.PolicyElement GetPolicy(pol.PolicyIdReferenceElement policyReference)
		{
            if (policyReference == null) throw new ArgumentNullException("policyReference");
			pol.PolicyDocument doc = _policies[ policyReference.PolicyId ] as pol.PolicyDocument;
			if( doc != null )
			{
				return (pol.PolicyElement)doc.Policy; //TODO: check if we have to return a read write or a read only policy here.
			}
			return null;
		}

		/// <summary>
		/// Returns a policy set document using the PolicySetRefereneId specified.
		/// </summary>
		/// <param name="policySetReference">The policy set reference with the Id of the policy set searched.</param>
		/// <returns>The policy set document.</returns>
		public pol.PolicySetElement GetPolicySet(pol.PolicySetIdReferenceElement policySetReference)
		{
            if (policySetReference == null) throw new ArgumentNullException("policySetReference");
			pol.PolicyDocument doc = _policySets[ policySetReference.PolicySetId ] as pol.PolicyDocument;
			if( doc != null )
			{
				return (pol.PolicySetElement)doc.PolicySet; //TODO: check if we have to return a read write or a read only policy here.
			}
			return null;
		}

		/// <summary>
		/// Method called by the EvaluationEngine when the evaluation is executed without a policy document, this 
		/// method search in the policy repository and return the first policy that matches its target with the
		/// context document specified.
		/// </summary>
		/// <param name="context">The evaluation context instance.</param>
		/// <returns>The policy document ready to be used by the evaluation engine.</returns>
		public pol.PolicyDocument Match( rtm.EvaluationContext context )
		{
            if (context == null) throw new ArgumentNullException("context");
			pol.PolicyDocument polEv = null;
 
			//Search if there is a policySet which target matches the context document
			foreach( pol.PolicyDocument policy in _policySets.Values )
			{
				rtm.PolicySet tempPolicy = new rtm.PolicySet( context.Engine, (pol.PolicySetElement)policy.PolicySet );

				rtm.EvaluationContext tempContext = new rtm.EvaluationContext( context.Engine, policy, context.ContextDocument );

				// Match the policy set target with the context document
				if( tempPolicy.Match( tempContext ) == rtm.TargetEvaluationValue.Match )
				{
					if( polEv == null )
					{
						polEv = policy;
					}
					else
					{
						throw new EvaluationException( Resource.ResourceManager[ Resource.MessageKey.exc_duplicated_policy_in_repository ] );
					}
				}
			}

			//Search if there is a policy which target matches the context document
			foreach( pol.PolicyDocument policy in _policies.Values )
			{
				rtm.Policy tempPolicy = new rtm.Policy( (pol.PolicyElement)policy.Policy );

				rtm.EvaluationContext tempContext = new rtm.EvaluationContext( context.Engine, policy, context.ContextDocument );

				// Match the policy target with the context document
				if( tempPolicy.Match( tempContext ) == rtm.TargetEvaluationValue.Match )
				{
					if( polEv == null )
					{
						polEv = policy;
					}
					else
					{
						throw new EvaluationException( Resource.ResourceManager[ Resource.MessageKey.exc_duplicated_policy_in_repository ] );
					}
				}
			}
			return polEv;
		}

		#endregion
	}
}
