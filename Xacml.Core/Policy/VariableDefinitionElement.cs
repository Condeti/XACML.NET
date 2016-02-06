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
using inf = Xacml.Core.Interfaces;

namespace Xacml.Core.Policy
{
	/// <summary>
	/// Represents a RuleCombinerParameter defined in the policy document.
	/// </summary>
	public class VariableDefinitionElement : XacmlElement
	{
		#region Private members

		/// <summary>
		/// The parameter name.
		/// </summary>
		private string _id;

		/// <summary>
		/// The expression for the variable definition.
		/// </summary>
		private inf.IExpression _expression;

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a new RuleCombinerParameter using the provided argument values.
		/// </summary>
		/// <param name="id">The variable id.</param>
		/// <param name="expression">The expression for the variable definition.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public VariableDefinitionElement( string id, inf.IExpression expression, XacmlVersion schemaVersion )
			: base( XacmlSchema.Policy, schemaVersion )
		{
			_id = id;
			_expression = expression;
		}

		/// <summary>
		/// Creates a new RuleCombinerParameter using the XmlReader instance provided.
		/// </summary>
		/// <param name="reader">The XmlReader instance positioned at the CombinerParameterElement node.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public VariableDefinitionElement( XmlReader reader, XacmlVersion schemaVersion )
			: base( XacmlSchema.Policy, schemaVersion )
		{
			if( reader.LocalName == PolicySchema2.VariableDefinitionElement.VariableDefinition && 
				ValidateSchema( reader, schemaVersion ) )
			{
				// Read the attributes
				if( reader.HasAttributes )
				{
					// Load all the attributes
					while( reader.MoveToNextAttribute() )
					{
						if( reader.LocalName == PolicySchema2.VariableDefinitionElement.VariableId )
						{
							_id = reader.GetAttribute( PolicySchema2.VariableDefinitionElement.VariableId );
						}
					}
					reader.MoveToElement();
				}

				// Read the rule contents.
				while( reader.Read() )
				{
					switch( reader.LocalName )
					{
						case PolicySchema1.AttributeSelectorElement.AttributeSelector:
							_expression = new AttributeSelectorElement( reader, schemaVersion );
							break;
						case PolicySchema1.SubjectAttributeDesignatorElement.SubjectAttributeDesignator:
							_expression = new SubjectAttributeDesignatorElement( reader, schemaVersion );
							break;
						case PolicySchema1.ActionAttributeDesignatorElement.ActionAttributeDesignator:
							_expression = new ActionAttributeDesignatorElement( reader, schemaVersion );
							break;
						case PolicySchema1.ResourceAttributeDesignatorElement.ResourceAttributeDesignator:
							_expression = new ResourceAttributeDesignatorElement( reader, schemaVersion );
							break;
						case PolicySchema1.EnvironmentAttributeDesignatorElement.EnvironmentAttributeDesignator:
							_expression = new EnvironmentAttributeDesignatorElement( reader, schemaVersion );
							break;
						case PolicySchema1.AttributeValueElement.AttributeValue:
							_expression = new AttributeValueElementReadWrite( reader, schemaVersion );
							break;
						case PolicySchema1.FunctionElement.Function:
							_expression = new FunctionElementReadWrite( reader, schemaVersion );
							break;
						case PolicySchema1.ApplyElement.Apply:
							_expression = new ApplyElement( reader, schemaVersion );
							break;
						case PolicySchema2.VariableReferenceElement.VariableReference:
							_expression = new VariableReferenceElement( reader, schemaVersion );
							break;
					}
					if( reader.LocalName == PolicySchema2.VariableDefinitionElement.VariableDefinition && 
						reader.NodeType == XmlNodeType.EndElement )
					{
						break;
					}
				}
			}
			else
			{
				throw new Exception( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_node_name, reader.LocalName ] );
			}

		}

		#endregion

		#region Public properties

		/// <summary>
		/// The variable id
		/// </summary>
		public string Id
		{
			get{ return _id; }
		}

		/// <summary>
		/// Returns the expression definition.
		/// </summary>
		public inf.IExpression Expression
		{
			get{ return _expression; }
		}

		/// <summary>
		/// Whether the instance is a read only version.
		/// </summary>
		public override bool IsReadOnly
		{
			get{ return true; }
		}
		#endregion

		#region Public methods

		/// <summary>
		/// Writes the XML of the current element
		/// </summary>
		/// <param name="writer">The XmlWriter in which the element will be written</param>
		public void WriteDocument( XmlWriter writer )
		{
			writer.WriteStartElement( PolicySchema2.VariableDefinitionElement.VariableDefinition);
			writer.WriteAttributeString( PolicySchema2.VariableDefinitionElement.VariableId, this._id );
			
			this._expression.WriteDocument( writer );

			writer.WriteEndElement();
		}

		#endregion

	}
}
