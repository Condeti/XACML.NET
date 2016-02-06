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
using System.Xml;

namespace Xacml.Core.Policy
{
	/// <summary>
	/// Represents a read-only PolicySet defined within a policy document.
	/// </summary>
	public class PolicySetElement : PolicySetElementReadWrite
	{
		#region Constructors

		/// <summary>
		/// Creates a new policySet using the arguments provided.
		/// </summary>
		/// <param name="id">The policy set id.</param>
		/// <param name="description">The description of the policy set.</param>
		/// <param name="target">The target for this policy set.</param>
		/// <param name="policies">All the policies inside this policy set.</param>
		/// <param name="policyCombiningAlgorithm">The policy combining algorithm for this policy set.</param>
		/// <param name="obligations">The obligations.</param>
		/// <param name="xpathVersion">The XPath version supported.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public PolicySetElement( string id, string description, TargetElementReadWrite target, ArrayList policies, string policyCombiningAlgorithm, ObligationReadWriteCollection obligations, string xpathVersion, XacmlVersion schemaVersion )
			: base( id, description, target, policies, policyCombiningAlgorithm, obligations, xpathVersion, schemaVersion )
		{
		}

		/// <summary>
		/// Creates a new PolicySet using the XmlReader instance provided.
		/// </summary>
		/// <param name="reader">The XmlReder positioned at the PolicySet element.</param>
		/// <param name="schemaVersion">The version of the schema that will be used to validate.</param>
		public PolicySetElement( XmlReader reader, XacmlVersion schemaVersion )
			: base( reader, schemaVersion )
		{
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The PolicySet Id.
		/// </summary>
		public override string Id
		{
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// The Target for this PolicySet
		/// </summary>
		public override TargetElementReadWrite Target
		{
			get
			{
				return new TargetElement(base.Target.Resources, base.Target.Subjects, base.Target.Actions,
					 base.Target.Environments, base.Target.SchemaVersion); 
			}
		}

		/// <summary>
		/// The policy set description.
		/// </summary>
		public override string Description
		{
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// The policy combining algorithm Id.
		/// </summary>
		public override string PolicyCombiningAlgorithm
		{
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// The list of obligations.
		/// </summary>
		public override ObligationReadWriteCollection Obligations
		{
			get{ return new ObligationCollection( base.Obligations ); }
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// All the policies defined in this PolicySet
		/// </summary>
		public override ArrayList Policies
		{
			set{ throw new NotSupportedException(); }
			get
			{
				ArrayList policies = new ArrayList();

				foreach( XacmlElement element in base.Policies)
				{
					if( element is PolicySetElementReadWrite )
					{
						PolicySetElementReadWrite tempElement = (PolicySetElementReadWrite)element;
						policies.Add( new PolicySetElement( tempElement.Id,tempElement.Description,
							tempElement.Target,tempElement.Policies,
							tempElement.PolicyCombiningAlgorithm,tempElement.Obligations,
							tempElement.XPathVersion,tempElement.SchemaVersion ));
					}
					else if( element is PolicyElementReadWrite )
					{
						PolicyElementReadWrite tempElement = (PolicyElementReadWrite)element;
						policies.Add( new PolicyElement( tempElement.Id,tempElement.Description,
							tempElement.Target,tempElement.Rules,
							tempElement.RuleCombiningAlgorithm,tempElement.Obligations,
							tempElement.XPathVersion,tempElement.CombinerParameters,
							tempElement.RuleCombinerParameters,tempElement.VariableDefinitions,
							tempElement.SchemaVersion) );
					}
					else if( element is /*ReadWrite*/PolicyIdReferenceElement )
					{
						PolicyIdReferenceElementReadWrite tempElement = (PolicyIdReferenceElementReadWrite)element;
						policies.Add( new PolicyIdReferenceElement( tempElement.PolicyId, tempElement.Version, tempElement.EarliestVersion,
							tempElement.LatestVersion, tempElement.SchemaVersion ) );
					}
					else if( element is PolicySetIdReferenceElementReadWrite )
					{
						PolicySetIdReferenceElementReadWrite tempElement = (PolicySetIdReferenceElementReadWrite)element;
						policies.Add( new PolicySetIdReferenceElement( tempElement.PolicySetId, tempElement.Version, tempElement.EarliestVersion,
							tempElement.LatestVersion, tempElement.SchemaVersion ) );
					}
				}
				return ArrayList.ReadOnly(policies);
			}
		}

		/// <summary>
		/// The XPath version supported.
		/// </summary>
		public override string XPathVersion
		{
			set{ throw new NotSupportedException(); }
		}

		#endregion
	}
}
