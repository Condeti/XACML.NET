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
using ctx = Xacml.Core.Context;
using pol = Xacml.Core.Policy;
using cor = Xacml.Core;
using inf = Xacml.Core.Interfaces;

namespace Xacml.Core.Policy
{
	/// <summary>
	/// Represents a read-only policy node defined in the Policy document.
	/// </summary>
	public class PolicyElement : PolicyElementReadWrite
	{

		#region Constructors

		/// <summary>
		/// Creates a new Policy with the specified arguments.
		/// </summary>
		/// <param name="id">The policy id.</param>
		/// <param name="description">THe policy description.</param>
		/// <param name="target">THe policy target.</param>
		/// <param name="rules">THe rules for this policy.</param>
		/// <param name="ruleCombiningAlgorithm">THe rule combining algorithm.</param>
		/// <param name="obligations">The Obligations for this policy.</param>
		/// <param name="xpathVersion">The XPath version supported.</param>
		/// <param name="combinerParameters">The combiner parameters in this policy.</param>
		/// <param name="ruleCombinerParameters">The rule parameters in this policy.</param>
		/// <param name="variableDefinitions">The variable definitions of this policy.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public PolicyElement( string id, string description, TargetElementReadWrite target, RuleReadWriteCollection rules, string ruleCombiningAlgorithm, ObligationReadWriteCollection obligations, string xpathVersion, 
			ArrayList combinerParameters, ArrayList ruleCombinerParameters, IDictionary variableDefinitions, XacmlVersion schemaVersion )
			: base( id, description, target, rules, ruleCombiningAlgorithm, obligations, xpathVersion, 
			combinerParameters, ruleCombinerParameters, variableDefinitions, schemaVersion )
		{
		}

		/// <summary>
		/// Creates a new Policy using the XmlReader instance specified.
		/// </summary>
		/// <param name="reader">The XmlReader instance positioned at the Policy node.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public PolicyElement( XmlReader reader, XacmlVersion schemaVersion )
			: base( reader, schemaVersion )
		{
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Gets the Id of the policy.
		/// </summary>
		public override string Id
		{
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// Gets the description of the policy.
		/// </summary>
		public override string Description
		{
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// Gets the rule combining algorithm name
		/// </summary>
		public override string RuleCombiningAlgorithm
		{
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// Gets the list of rules.
		/// </summary>
		public override RuleReadWriteCollection Rules
		{
			get{ return new RuleCollection( base.Rules ); }
			set{ throw new NotSupportedException(); }
		}
		/// <summary>
		/// Gets the target instance.
		/// </summary>
		public override TargetElementReadWrite Target
		{
			get{ return new TargetElement(base.Target.Resources, base.Target.Subjects, base.Target.Actions,
					 base.Target.Environments, base.Target.SchemaVersion); 
			}
		}
		/// <summary>
		/// Gets the list of obligations.
		/// </summary>
		public override ObligationReadWriteCollection Obligations
		{
			get{ return new ObligationCollection( base.Obligations ); }
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// The XPath version supported.
		/// </summary>
		public override string XPathVersion
		{
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// The variable definitions.
		/// </summary>
		public override IDictionary VariableDefinitions
		{
			set{ throw new NotSupportedException(); }
		}
		/// <summary>
		/// The combiner parameters
		/// </summary>
		public override ArrayList CombinerParameters
		{
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// The rule combiner parameters
		/// </summary>
		public override ArrayList RuleCombinerParameters
		{
			set{ throw new NotSupportedException(); }
		}
		#endregion
	}
}
