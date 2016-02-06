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
using System.Collections.Specialized;
using pol = Xacml.Core.Policy;
using ctx = Xacml.Core.Context;

namespace Xacml.Core.Runtime
{
	/// <summary>
	/// The runtime rule reference that keeps the runtime information for the rule.
	/// </summary>
	public class Rule : IMatchEvaluable
	{
		#region Private members

		/// <summary>
		/// All the resources defined in this rule.
		/// </summary>
		private StringCollection _allResources = new StringCollection();

		/// <summary>
		/// The result decission of the evaluation.
		/// </summary>
		private Decision _evaluationValue;

		/// <summary>
		/// The rule defined in the policy document.
		/// </summary>
		private pol.RuleElement _rule;

		/// <summary>
		/// The runtime condition defined for this rule.
		/// </summary>
		private Condition _condition;

		/// <summary>
		/// The runtime target defined for this rule.
		/// </summary>
		private Target _target;

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a new instance of the Rule using the rule defined in the policy document.
		/// </summary>
		/// <param name="rule">The rule defined in the policy document.</param>
		public Rule( pol.RuleElement rule )
		{
            if (rule == null) throw new ArgumentNullException("rule");
			_rule = rule;
			if( _rule.SchemaVersion == XacmlVersion.Version10 || _rule.SchemaVersion == XacmlVersion.Version11 )
			{
				_condition = new Condition( (pol.ConditionElement)_rule.Condition );
			}
			else if( _rule.SchemaVersion == XacmlVersion.Version20 )
			{
				_condition = new Condition2( (pol.ConditionElement)_rule.Condition );
			}
		
			if( rule.Target != null )
			{
				_target = new Target( (pol.TargetElement)rule.Target );

				// Load all the resources for the elements within this rule.
				foreach( pol.ResourceElement resource in rule.Target.Resources.ItemsList )
				{
					foreach( pol.ResourceMatchElement rmatch in resource.Match )
					{
						if( !_allResources.Contains( rmatch.AttributeValue.Contents ) )
						{
							_allResources.Add( rmatch.AttributeValue.Contents );
						}
					}
				}
			}
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The rule definition in the policy document.
		/// </summary>
		public pol.RuleElement RuleDefinition
		{
			get{ return _rule; }
		}

		#endregion
		
		#region IEvaluable members

		/// <summary>
		/// Return all Resources in this rule
		/// </summary>
		public StringCollection AllResources
		{ 
			get{ return _allResources; }
		}

		/// <summary>
		/// Verifies if the rule target matches
		/// </summary>
		/// <param name="context">The evaluation context instance.</param>
		/// <returns>The result of the Match process.</returns>
		public TargetEvaluationValue Match( EvaluationContext context )
		{
            if (context == null) throw new ArgumentNullException("context");
			TargetEvaluationValue targetEvaluationValue = TargetEvaluationValue.Indeterminate;
			context.Trace( "Evaluating rule target" );
			context.AddIndent();
			try
			{
				// Evaluate the policy target
				targetEvaluationValue = TargetEvaluationValue.Match;
				if( _target != null )
				{
					targetEvaluationValue = _target.Evaluate( context );

					context.TraceContextValues();
				}
				return targetEvaluationValue;
			}
			finally
			{
				context.RemoveIndent();
				context.Trace( "Target: {0}", targetEvaluationValue );
			}
		}

		/// <summary>
		/// Evaluates the rule contents.
		/// </summary>
		/// <param name="context">The evaluation context instance.</param>
		/// <returns>A decission for this evalauation.</returns>
		public Decision Evaluate( EvaluationContext context )
		{
            if (context == null) throw new ArgumentNullException("context");
			context.Trace( "Evaluating rule: {0}", _rule.Description );
			context.AddIndent();
			context.CurrentRule = this;
			try
			{
				// Validate the Target element
				TargetEvaluationValue targetEvaluation = Match( context );

				// If the target matches the conditions ar evaluated
				EvaluationValue conditionEvaluation = EvaluationValue.True;
				if( _rule.HasCondition )
				{
					// Evaluate the condition
					conditionEvaluation = _condition.Evaluate( context );
				}
				else
				{
					context.Trace( "Rule does not have a condition" );
				}

				// Decite the final rule evaluation value
				if( targetEvaluation == TargetEvaluationValue.Indeterminate || conditionEvaluation.IsIndeterminate )
				{
					_evaluationValue = Decision.Indeterminate;
				}
				else if( targetEvaluation == TargetEvaluationValue.Match && conditionEvaluation.BoolValue )
				{
					_evaluationValue = ((_rule.Effect == pol.Effect.Permit) ? Decision.Permit : Decision.Deny);
				}
				else if( ( targetEvaluation == TargetEvaluationValue.NoMatch ) ||
					( targetEvaluation == TargetEvaluationValue.Match && !conditionEvaluation.BoolValue ) )
				{
					_evaluationValue = Decision.NotApplicable;
				}

				// Return the value
				context.Trace( "Rule: {0}", _evaluationValue );
				return _evaluationValue;
			}
			finally
			{
				context.RemoveIndent();
				context.CurrentRule = null;
			}
		}

		#endregion
	}
}
