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
	/// Represents a read-only Rule defined in the policy document.
	/// </summary>
	public class RuleElement : RuleElementReadWrite
	{
		#region Constructor

		/// <summary>
		/// Creates a new Rule using the provided argument values.
		/// </summary>
		/// <param name="id">The rule id.</param>
		/// <param name="description">The rule description.</param>
		/// <param name="target">The target instance for this rule.</param>
		/// <param name="condition">The condition for this rule.</param>
		/// <param name="effect">The effect of this rule.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public RuleElement( string id, string description, TargetElementReadWrite target, ConditionElementReadWrite condition, Effect effect, XacmlVersion schemaVersion )
			: base( id, description, target, condition, effect, schemaVersion )
		{
		}

		/// <summary>
		/// Creates a new Rule using the XmlReader instance provided.
		/// </summary>
		/// <param name="reader">THe XmlReader instance positioned at the Rule node.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public RuleElement( XmlReader reader, XacmlVersion schemaVersion )
			: base( reader, schemaVersion )
		{
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The Rule Id.
		/// </summary>
		public override string Id
		{
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// The description of this rule.
		/// </summary>
		public override string Description
		{
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// The condition of this rule.
		/// </summary>
		public override ConditionElementReadWrite Condition
		{
			get
			{
				if(base.Condition != null)
					return new ConditionElement( base.Condition.FunctionId, base.Condition.Arguments, this.SchemaVersion );
				else
					return null;
			}
			set{ throw new NotSupportedException(); }
		}
	
		/// <summary>
		/// The target of the rule.
		/// </summary>
		public override TargetElementReadWrite Target
		{
			get
			{
				if(base.Target != null)
					return new TargetElement(base.Target.Resources, base.Target.Subjects, base.Target.Actions,
						 base.Target.Environments, base.Target.SchemaVersion); 
				else
					return null;
			}
		}

		/// <summary>
		/// The effect of the rule.
		/// </summary>
		public override Effect Effect
		{
			set{ throw new NotSupportedException(); }
		}

		#endregion


	}
}
