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
	public class Condition2 : Condition, IEvaluable
	{
		#region Constructors

		/// <summary>
		/// Creates a new Condition using the reference to the condition definition in the policy document.
		/// </summary>
		/// <param name="condition">The condition definition of the policy document.</param>
		public Condition2( pol.ConditionElement condition )
			: base( condition )
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
			context.Trace( "Evaluating condition" );
			context.AddIndent();
			try
			{
				// Iterate through the arguments, the IExpressionType is a mark interface
				foreach( inf.IExpression arg in ApplyDefinition.Arguments )
				{
					if( arg is pol.ApplyElement )
					{
						context.Trace( "Apply within condition." );

						// There is a nested apply un this policy a new Apply will be created and also 
						// evaluated. It's return value will be used as the processed argument.
						Apply _childApply = new Apply( (pol.ApplyElement)arg );

						// Evaluate the Apply
						EvaluationValue retVal = _childApply.Evaluate( context );

						return retVal;
					}
					else if( arg is pol.FunctionElementReadWrite )
					{
						throw new NotImplementedException( "FunctionElement" ); //TODO:
					}
					else if( arg is pol.VariableReferenceElement )
					{
						pol.VariableReferenceElement variableRef = arg as pol.VariableReferenceElement;
						VariableDefinition variableDef = context.CurrentPolicy.VariableDefinition[ variableRef.VariableId ] as VariableDefinition;

						if( !variableDef.IsEvaluated )
						{
							return variableDef.Evaluate( context );
						}
						else
						{
							return variableDef.Value;
						}
					}
					else if( arg is pol.AttributeValueElementReadWrite )
					{
						// The AttributeValue does not need to be processed
						context.Trace( "Attribute value {0}", arg.ToString() );
					
						pol.AttributeValueElement attributeValue = new pol.AttributeValueElement( ((pol.AttributeValueElementReadWrite)arg).DataType, ((pol.AttributeValueElementReadWrite)arg).Contents, ((pol.AttributeValueElementReadWrite)arg).SchemaVersion );

						return new EvaluationValue( 
							attributeValue.GetTypedValue( attributeValue.GetType( context ), 0 ), 
							attributeValue.GetType( context ) );
					}
					else if( arg is pol.AttributeDesignatorBase )
					{
						// Returning an empty bag, since the condition is not supposed to work with a bag
						context.Trace( "Processing attribute designator: {0}", arg.ToString() );

						pol.AttributeDesignatorBase attrDes = (pol.AttributeDesignatorBase)arg;
						BagValue bag = new BagValue( EvaluationEngine.GetDataType( attrDes.DataType ) );
						return new EvaluationValue( bag, bag.GetType( context ) );
					}
					else if( arg is pol.AttributeSelectorElement )
					{
						// Returning an empty bag, since the condition is not supposed to work with a bag
						context.Trace( "Attribute selector" );

						pol.AttributeSelectorElement attrSel = (pol.AttributeSelectorElement)arg;
						BagValue bag = new BagValue( EvaluationEngine.GetDataType( attrSel.DataType ) );
						return new EvaluationValue( bag, bag.GetType( context ) );
					}
				}
				throw new NotSupportedException( "internal error" );
			}
			finally
			{
				context.TraceContextValues();
				context.RemoveIndent();
			}
		}

		#endregion
	}
}
