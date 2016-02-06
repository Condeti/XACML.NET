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

namespace Xacml.Core.Runtime
{
	/// <summary>
	/// Mantains the value resulted of the evaluation of the variable definition.
	/// </summary>
	public class VariableDefinition : IEvaluable
	{
		#region Private members

		/// <summary>
		/// References the variable definition element in the policy document.
		/// </summary>
		private pol.VariableDefinitionElement _variableDefinition;

		/// <summary>
		/// The value resulted from the evaluation.
		/// </summary>
		private EvaluationValue _value;

		/// <summary>
		/// Whether the variable have been evaluated.
		/// </summary>
		private bool _isEvaluated;

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="variableDefinition">The variable definition in the policy document.</param>
		public VariableDefinition( pol.VariableDefinitionElement variableDefinition )
		{
			_variableDefinition = variableDefinition;
		}

		#endregion

		#region IEvaluable Members

		/// <summary>
		/// Evaluates the variable into a value.
		/// </summary>
		/// <param name="context">The contex of the evaluation.</param>
		/// <returns>The value of the function.</returns>
		public EvaluationValue Evaluate( EvaluationContext context )
		{
            if (context == null) throw new ArgumentNullException("context");
			context.Trace( "Evaluating variable" );
			context.AddIndent();

			try
			{
				if( _variableDefinition.Expression is pol.ApplyElement )
				{
					context.Trace( "Apply within condition." );

					// There is a nested apply un this policy a new Apply will be created and also 
					// evaluated. It's return value will be used as the processed argument.
					Apply _childApply = new Apply( (pol.ApplyElement)_variableDefinition.Expression );

					// Evaluate the Apply
					_value = _childApply.Evaluate( context );

					context.TraceContextValues();
					return _value;
				}
				else if( _variableDefinition.Expression is pol.FunctionElement )
				{
					throw new NotImplementedException( "FunctionElement" ); //TODO:
				}
				else if( _variableDefinition.Expression is pol.VariableReferenceElement )
				{
					pol.VariableReferenceElement variableRef = _variableDefinition.Expression as pol.VariableReferenceElement;
					VariableDefinition variableDef = context.CurrentPolicy.VariableDefinition[ variableRef.VariableId ] as VariableDefinition;

					context.TraceContextValues();

					if( !variableDef.IsEvaluated )
					{
						return variableDef.Evaluate( context );
					}
					else
					{
						return variableDef.Value;
					}
				}
				else if( _variableDefinition.Expression is pol.AttributeValueElementReadWrite )
				{
					// The AttributeValue does not need to be processed
					context.Trace( "Attribute value {0}", _variableDefinition.Expression.ToString() );

					pol.AttributeValueElementReadWrite att = (pol.AttributeValueElementReadWrite)_variableDefinition.Expression;
					pol.AttributeValueElement attributeValue = new pol.AttributeValueElement( att.DataType, att.Contents, att.SchemaVersion );

					_value = new EvaluationValue( 
						attributeValue.GetTypedValue( attributeValue.GetType( context ), 0 ), 
						attributeValue.GetType( context ) );
					return _value;
				}
				else if( _variableDefinition.Expression is pol.AttributeDesignatorBase )
				{
					// Resolve the AttributeDesignator using the EvaluationEngine public methods.
					context.Trace( "Processing attribute designator: {0}", _variableDefinition.Expression.ToString() );

					pol.AttributeDesignatorBase attrDes = (pol.AttributeDesignatorBase)_variableDefinition.Expression;
					BagValue bag = EvaluationEngine.Resolve( context, attrDes );

					// If the attribute was not resolved by the EvaluationEngine search the 
					// attribute in the context document, also using the EvaluationEngine public 
					// methods.
					if( bag.BagSize == 0 )
					{
						if( _variableDefinition.Expression is pol.SubjectAttributeDesignatorElement )
						{
							ctx.AttributeElement attrib = EvaluationEngine.GetAttribute( context, attrDes );
							if( attrib != null )
							{
								context.Trace( "Adding subject attribute designator: {0}", attrib.ToString() );
								bag.Add( attrib );
							}
						}
						else if( _variableDefinition.Expression is pol.ResourceAttributeDesignatorElement )
						{
							ctx.AttributeElement attrib = EvaluationEngine.GetAttribute( context, attrDes );
							if( attrib != null )
							{
								context.Trace( "Adding resource attribute designator {0}", attrib.ToString() );
								bag.Add( attrib );
							}
						}
						else if( _variableDefinition.Expression is pol.ActionAttributeDesignatorElement )
						{
							ctx.AttributeElement attrib = EvaluationEngine.GetAttribute( context, attrDes );
							if( attrib != null )
							{
								context.Trace( "Adding action attribute designator {0}", attrib.ToString() );
								bag.Add( attrib );
							}
						}
						else if( _variableDefinition.Expression is pol.EnvironmentAttributeDesignatorElement )
						{
							ctx.AttributeElement attrib = EvaluationEngine.GetAttribute( context, attrDes );
							if( attrib != null )
							{
								context.Trace( "Adding environment attribute designator {0}", attrib.ToString() );
								bag.Add( attrib );
							}
						}
					}

					// If the argument was not found and the attribute must be present this is 
					// a MissingAttribute situation so set the flag. Otherwise add the attribute 
					// to the processed arguments.
					if( bag.BagSize == 0 && attrDes.MustBePresent )
					{
						context.Trace( "Attribute is missing" );
						context.IsMissingAttribute = true;
						context.AddMissingAttribute( attrDes );
						_value = EvaluationValue.Indeterminate;
					}
					else
					{
						_value = new EvaluationValue( bag, bag.GetType( context ) );
					}
					return _value;
				}
				else if( _variableDefinition.Expression is pol.AttributeSelectorElement )
				{
					// Resolve the XPath query using the EvaluationEngine public methods.
					context.Trace( "Attribute selector" );
					try
					{
						pol.AttributeSelectorElement attributeSelector = (pol.AttributeSelectorElement)_variableDefinition.Expression;
						BagValue bag = EvaluationEngine.Resolve( context, attributeSelector );
						if( bag.Elements.Count == 0 && attributeSelector.MustBePresent )
						{
							context.Trace( "Attribute is missing" );
							context.IsMissingAttribute = true;
							context.AddMissingAttribute( attributeSelector );
							_value = EvaluationValue.Indeterminate;
						}
						else
						{
							_value = new EvaluationValue( bag, bag.GetType( context ) );
						}
					}
					catch( EvaluationException e )
					{
						context.Trace( "ERR: {0}", e.Message );
						context.ProcessingError = true;
						_value = EvaluationValue.Indeterminate;
					}
					return _value;
				}			
				throw new NotSupportedException( "internal error" );
			}
			finally
			{
				_isEvaluated = true;
				context.RemoveIndent();
			}
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The value of the variable.
		/// </summary>
		public EvaluationValue Value
		{
			get
			{ 
				if( !_isEvaluated )
				{
					throw new EvaluationException( "Te variable must be evaluated." ); //TODO: resources
				}
				return _value; 
			}
		}

		/// <summary>
		/// Whether the variable have been evaluated.
		/// </summary>
		public bool IsEvaluated
		{
			get{ return _isEvaluated; }
		}

		#endregion
	}
}
