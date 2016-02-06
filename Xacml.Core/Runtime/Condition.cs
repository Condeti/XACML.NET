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
using pol = Xacml.Core.Policy;
using ctx = Xacml.Core.Context;
using inf = Xacml.Core.Interfaces;

namespace Xacml.Core.Runtime
{
	/// <summary>
	/// Represents the Condition element of the policy document during evaluation.
	/// </summary>
	public class Condition : ApplyBase, IEvaluable
	{
		#region Constructors

		/// <summary>
		/// Creates a new Condition using the reference to the condition definition in the policy document.
		/// </summary>
		/// <param name="condition">The condition definition of the policy document.</param>
		public Condition( pol.ConditionElement condition ) : base( condition )
		{
		}

		#endregion

		#region Public methods

		/// <summary>
		/// This method overrides the ApplyBase method in order to provide extra validations 
		/// required in the condition evaluation, for example the final return value should be a
		/// boolean value.
		/// </summary>
		/// <param name="context">The evaluation context instance.</param>
		/// <returns>The EvaluationValue with the results of the condition evaluation.</returns>
		public override EvaluationValue Evaluate( EvaluationContext context )
		{
            if (context == null) throw new ArgumentNullException("context");
			EvaluationValue _evaluationValue = null;
			context.Trace( "Evaluating condition..." );
			context.AddIndent();
			try
			{
				// Get the function instance
				inf.IFunction function = EvaluationEngine.GetFunction( ApplyDefinition.FunctionId );
				if( function == null )
				{
					context.Trace( "ERR: function not found {0}", ApplyDefinition.FunctionId );
					context.ProcessingError = true;
					return EvaluationValue.Indeterminate;
				}

				// Validates the function return value
				if( function.Returns == null )
				{
					context.Trace( "The function '{0}' does not defines it's return value", ApplyDefinition.FunctionId );
					_evaluationValue = EvaluationValue.Indeterminate;
					context.ProcessingError = true;
				}
				else if( function.Returns != DataTypeDescriptor.Boolean )
				{
					context.Trace( "Function does not return Boolean a value" );
					_evaluationValue = EvaluationValue.Indeterminate;
					context.ProcessingError = true;
				}
				else
				{
					// Call the ApplyBase method to perform the evaluation.
					_evaluationValue = base.Evaluate( context );
				}

				// Validate the results of the evaluation
				if( _evaluationValue.IsIndeterminate )
				{
					context.Trace( "condition evaluated into {0}", _evaluationValue.ToString() );
					return _evaluationValue;
				} 
				if( !(_evaluationValue.Value is bool) )
				{
					context.Trace( "condition evaluated into {0}", _evaluationValue.ToString() );
					return EvaluationValue.Indeterminate;
				}
				if( _evaluationValue.BoolValue )
				{
					context.Trace( "condition evaluated into {0}", _evaluationValue.ToString() );
					return EvaluationValue.True;
				}
				else
				{
					// If the evaluation was false, validate if there was a missin attribute during 
					// evaluation and return an Indeterminate, otherwise return the False value.
					if( context.IsMissingAttribute )
					{
						context.Trace( "condition evaluated into {0}", _evaluationValue.ToString() );
						return EvaluationValue.Indeterminate;
					}
					else
					{
						context.Trace( "condition evaluated into {0}", _evaluationValue.ToString() );
						return EvaluationValue.False;
					}
				}
			}
			finally
			{
				context.TraceContextValues();

				context.RemoveIndent();
				context.Trace( "Condition: {0}", _evaluationValue.ToString() );
			}
		}

		#endregion
	}
}
