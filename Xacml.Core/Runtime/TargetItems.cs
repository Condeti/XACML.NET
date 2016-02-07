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
using ctx = Xacml.Core.Context;
using pol = Xacml.Core.Policy;
using inf = Xacml.Core.Interfaces;

namespace Xacml.Core.Runtime
{
	/// <summary>
	/// The base class for any target item that will be used in runtime.
	/// </summary>
	public abstract class TargetItems
	{
		#region Private members

		/// <summary>
		/// The evaluation value that results the evaluation of the target item.
		/// </summary>
		private TargetEvaluationValue _evaluationValue;

		/// <summary>
		/// The target item reference to the policy document.
		/// </summary>
		private pol.TargetItemsBaseReadWrite _targetItems;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance of any target item.
		/// </summary>
		protected TargetItems( pol.TargetItemsBaseReadWrite targetItems )
		{
			_targetItems = targetItems;
		}

		#endregion
		
		#region Public methods

		/// <summary>
		/// Evaluates the target items and return wether the target applies to the context or not.
		/// </summary>
		/// <param name="context">The evaluation context instance.</param>
		/// <param name="targetItem">The target item in the context document.</param>
		/// <returns></returns>
		public TargetEvaluationValue Evaluate( EvaluationContext context, ctx.TargetItemBase targetItem )
		{
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (_targetItems.IsAny)
            {
                context.Trace("IsAny");
                return TargetEvaluationValue.Match;
            }

            _evaluationValue = TargetEvaluationValue.NoMatch;

			//Match TargetItem
			foreach( pol.TargetItemBase polItem in _targetItems.ItemsList )
			{
				foreach( pol.TargetMatchBase match in polItem.Match )
				{
					_evaluationValue = TargetEvaluationValue.NoMatch;

					context.Trace( "Using function: {0}", match.MatchId );

					inf.IFunction matchFunction = EvaluationEngine.GetFunction( match.MatchId );
					if( matchFunction == null )
					{
						context.Trace( "ERR: function not found {0}", match.MatchId );
						context.ProcessingError = true;
						return TargetEvaluationValue.Indeterminate;
					}
				    if( matchFunction.Returns == null )
				    {
				        // Validates the function return value
				        context.Trace( "ERR: The function '{0}' does not defines it's return value", match.MatchId );
				        context.ProcessingError = true;
				        return TargetEvaluationValue.Indeterminate;
				    }
				    if( matchFunction.Returns != DataTypeDescriptor.Boolean )
				    {
				        context.Trace( "ERR: Function does not return Boolean a value" );
				        context.ProcessingError = true;
				        return TargetEvaluationValue.Indeterminate;
				    }
				    ctx.AttributeElement attribute = EvaluationEngine.Resolve( context, match, targetItem );

				    if( attribute != null )
				    {
				        context.Trace( "Attribute found, evaluating match function" );
				        try
				        {
				            EvaluationValue returnValue = EvaluationEngine.EvaluateFunction( context, matchFunction, match.AttributeValue, attribute );
				            _evaluationValue = returnValue.BoolValue ? TargetEvaluationValue.Match : TargetEvaluationValue.NoMatch;
				        }
				        catch( EvaluationException e )
				        {
				            context.Trace( Resource.TRACE_ERROR, e.Message ); 
				            _evaluationValue = TargetEvaluationValue.Indeterminate;
				        }
				    }

				    // Validate MustBePresent
				    if( match.AttributeReference.MustBePresent )
				    {
				        if( context.IsMissingAttribute )
				        {
				            context.Trace( "Attribute not found and must be present" );
				            _evaluationValue = TargetEvaluationValue.Indeterminate;
				        }
				    }

				    // Do not iterate if the value was found
				    if( _evaluationValue != TargetEvaluationValue.Match )
				    {
				        break;
				    }
				}

				// Do not iterate if the value was found
				if( _evaluationValue == TargetEvaluationValue.Match )
				{
					return _evaluationValue;
				}
			}

			return _evaluationValue;
		}

		#endregion
	}
}
