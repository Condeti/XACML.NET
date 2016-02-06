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
using cor = Xacml.Core;
using inf = Xacml.Core.Interfaces;

namespace Xacml.Core.Runtime
{
	/// <summary>
	/// The runtime class for the policy set.
	/// </summary>
	public class PolicySet : IMatchEvaluable, IObligationsContainer
	{
		#region Private members

		/// <summary>
		/// All the resources in the policy.
		/// </summary>
		private StringCollection _allResources = new StringCollection();

		/// <summary>
		/// All the policies that belongs to this policy set.
		/// </summary>
		private IMatchEvaluableCollection _policies = new IMatchEvaluableCollection();

		/// <summary>
		/// The final decission for this policy set.
		/// </summary>
		private Decision _evaluationValue;

		/// <summary>
		/// The policy set defined in the context document.
		/// </summary>
		private pol.PolicySetElement _policySet;

		/// <summary>
		/// The target during the evaluation process.
		/// </summary>
		private Target _target;

		/// <summary>
		/// The obligations set to this policy.
		/// </summary>
		private pol.ObligationCollection _obligations;

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a new runtime policy set evaluation.
		/// </summary>
		/// <param name="engine">The evaluation engine.</param>
		/// <param name="policySet">The policy set defined in the policy document.</param>
		public PolicySet( EvaluationEngine engine, pol.PolicySetElement policySet )
		{
            if (engine == null) throw new ArgumentNullException("engine");
            if (policySet == null) throw new ArgumentNullException("policySet");
			_policySet = policySet;

			// Create a runtime target of this policy set.
			if( policySet.Target != null )
			{
				_target = new Target( (pol.TargetElement)policySet.Target );

				foreach( pol.ResourceElement resource in policySet.Target.Resources.ItemsList )
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

			// Add all the policies (or policy set) inside this policy set.
			foreach( object child in policySet.Policies )
			{
                pol.PolicySetElement childPolicySet = child as pol.PolicySetElement;
                pol.PolicyElement childPolicyElement = child as pol.PolicyElement;
                pol.PolicySetIdReferenceElement childPolicySetIdReference = child as pol.PolicySetIdReferenceElement;
                pol.PolicyIdReferenceElement childPolicyIdReferenceElement = child as pol.PolicyIdReferenceElement;
                if (childPolicySet != null)
				{
                    PolicySet policySetEv = new PolicySet(engine, childPolicySet);
					foreach( string rName in policySetEv.AllResources )
					{
						if( !_allResources.Contains( rName ) )
						{
							_allResources.Add( rName );
						}
					}
					_policies.Add( policySetEv );
				}
                else if (childPolicyElement!=null)
				{
                    Policy policyEv = new Policy(childPolicyElement);
					foreach( string rName in policyEv.AllResources )
					{
						if( !_allResources.Contains( rName ) )
						{
							_allResources.Add( rName );
						}
					}
					_policies.Add( policyEv );
				}
                else if (childPolicySetIdReference!=null)
				{
                    pol.PolicySetElement policySetDefinition = EvaluationEngine.Resolve(childPolicySetIdReference);
					if( policySetDefinition != null )
					{
						PolicySet policySetEv = new PolicySet( engine, policySetDefinition );
						foreach( string rName in policySetEv.AllResources )
						{
							if( !_allResources.Contains( rName ) )
							{
								_allResources.Add( rName );
							}
						}
						_policies.Add( policySetEv );
					}
					else
					{
						throw new EvaluationException( Resource.ResourceManager[ Resource.MessageKey.exc_policyset_reference_not_resolved, ((pol.PolicySetIdReferenceElement)child).PolicySetId ] );
					}
				}
                else if (childPolicyIdReferenceElement!=null)
				{
                    pol.PolicyElement policyDefinition = EvaluationEngine.Resolve(childPolicyIdReferenceElement);
					if( policyDefinition != null )
					{
						Policy policyEv = new Policy( policyDefinition );
						foreach( string rName in policyEv.AllResources )
						{
							if( !_allResources.Contains( rName ) )
							{
								_allResources.Add( rName );
							}
						}
						_policies.Add( policyEv );
					}
					else
					{
						throw new EvaluationException( Resource.ResourceManager[ Resource.MessageKey.exc_policy_reference_not_resolved, ((pol.PolicyIdReferenceElement)child).PolicyId ] );
					}
				}
			}
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The obligations that have been fulfilled for this policy set.
		/// </summary>
		public pol.ObligationCollection Obligations
		{
			get{ return _obligations; }
		}

		#endregion
		
		#region IMatchEvaluable members

		/// <summary>
		/// All the resources for this policy.
		/// </summary>
		public StringCollection AllResources
		{ 
			get{ return _allResources; }
		}

		/// <summary>
		/// Match the target of this policy set.
		/// </summary>
		/// <param name="context">The evaluation context instance.</param>
		/// <returns>The retult evaluation of the policy set target.</returns>
		public TargetEvaluationValue Match( EvaluationContext context )
		{
            if (context == null) throw new ArgumentNullException("context");
			// Evaluate the policy target
			TargetEvaluationValue targetEvaluationValue = TargetEvaluationValue.Match;
			if( _target != null )
			{
				targetEvaluationValue = _target.Evaluate( context );
				context.TraceContextValues();
			}
			return targetEvaluationValue;
		}

		/// <summary>
		/// Evaluates the policy set.
		/// </summary>
		/// <param name="context">The evaluation context instance.</param>
		/// <returns>The decission result for this policy set.</returns>
		public Decision Evaluate( EvaluationContext context )
		{
            if (context == null) throw new ArgumentNullException("context");
			context.Trace( "Evaluating policySet: {0}", _policySet.Description );
			context.CurrentPolicySet = this;
			try
			{
				context.Trace( "Evaluating Target..." );
				context.AddIndent();

				// Evaluate the policy target
				TargetEvaluationValue targetEvaluationValue = Match( context );

				context.RemoveIndent();
				context.Trace( "Target: {0}", targetEvaluationValue );

                ProcessTargetEvaluationValue(context, targetEvaluationValue);

				context.Trace( "PolicySet: {0}", _evaluationValue );

				// If the policy evaluated to Deny or Permit add the obligations depending on its fulfill value.
                ProcessObligations(context);

				return _evaluationValue;
			}
			finally
			{
				context.CurrentPolicySet = null;
			}
		}

        /// <summary>
        /// Process the obligations for the policy.
        /// </summary>
        /// <param name="context">The evaluation context instance.</param>
        private void ProcessObligations(EvaluationContext context)
        {
            _obligations = new pol.ObligationCollection();
            if (_evaluationValue != Decision.Indeterminate &&
                _evaluationValue != Decision.NotApplicable &&
                _policySet.Obligations != null &&
                _policySet.Obligations.Count != 0)
            {
                foreach (pol.ObligationElement obl in _policySet.Obligations)
                {
                    if ((obl.FulfillOn == pol.Effect.Deny && _evaluationValue == Decision.Deny) ||
                        (obl.FulfillOn == pol.Effect.Permit && _evaluationValue == Decision.Permit))
                    {
                        context.Trace("Adding obligation: {0} ", obl.ObligationId);
                        _obligations.Add(obl);
                    }
                }

                // Get all obligations from child policies
                foreach (IMatchEvaluable child in _policies)
                {
                    IObligationsContainer oblig = child as IObligationsContainer;
                    if (oblig != null && oblig.Obligations != null)
                    {
                        foreach (pol.ObligationElement childObligation in oblig.Obligations)
                        {
                            if ((childObligation.FulfillOn == pol.Effect.Deny && _evaluationValue == Decision.Deny) ||
                                (childObligation.FulfillOn == pol.Effect.Permit && _evaluationValue == Decision.Permit))
                            {
                                _obligations.Add(childObligation);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Process the match result.
        /// </summary>
        /// <param name="context">The evaluation context instance.</param>
        /// <param name="targetEvaluationValue">The match evaluation result.</param>
        private void ProcessTargetEvaluationValue(EvaluationContext context, TargetEvaluationValue targetEvaluationValue)
        {
            if (targetEvaluationValue == TargetEvaluationValue.Match)
            {
                try
                {
                    context.Trace("Evaluating policies...");
                    context.AddIndent();

                    context.Trace("Policy combination algorithm: {0}", _policySet.PolicyCombiningAlgorithm);

                    // Evaluate all policies and apply rule combination
                    inf.IPolicyCombiningAlgorithm pca = EvaluationEngine.CreatePolicyCombiningAlgorithm(_policySet.PolicyCombiningAlgorithm);

                    if (pca == null)
                    {
                        throw new EvaluationException("the policy combining algorithm does not exists."); //TODO: resources
                    }

                    _evaluationValue = pca.Evaluate(context, _policies);

                    // Update the flags for general evaluation status.
                    context.TraceContextValues();

                    context.Trace("Policy combination algorithm: {0}", _evaluationValue.ToString());
                }
                finally
                {
                    context.RemoveIndent();
                }
            }
            else if (targetEvaluationValue == TargetEvaluationValue.NoMatch)
            {
                _evaluationValue = Decision.NotApplicable;
            }
            else if (targetEvaluationValue == TargetEvaluationValue.Indeterminate)
            {
                _evaluationValue = Decision.Indeterminate;
            }
        }

		#endregion
	}
}
