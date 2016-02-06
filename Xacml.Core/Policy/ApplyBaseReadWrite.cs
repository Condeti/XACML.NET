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
using cor = Xacml.Core;

namespace Xacml.Core.Policy 
{
	/// <summary>
	/// The ApplyBase class is used to define the common data used in the Apply and Condition read/write nodes.
	/// </summary>
	public abstract class ApplyBaseReadWrite : XacmlElement
	{
		#region Private members

		/// <summary>
		/// The id of the function that will be executed in this node.
		/// </summary>
		private string _functionId = string.Empty;

		/// <summary>
		/// All the arguments that will be pased to the function.
		/// </summary>
		private IExpressionReadWriteCollection _arguments = new IExpressionReadWriteCollection();

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a ConditionElement with the given parameters
		/// </summary>
		/// <param name="functionId"></param>
		/// <param name="arguments"></param>
		/// <param name="schemaVersion"></param>
		protected ApplyBaseReadWrite( string functionId, IExpressionReadWriteCollection arguments, XacmlVersion schemaVersion)
			: base( XacmlSchema.Policy, schemaVersion )
		{
			_functionId = functionId;
			_arguments = arguments;
		}

		/// <summary>
		/// Creates an instance of the ApplyBase using the XmlReader positioned in the node and the node name
		/// specifyed by the derived class in the constructor.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the "nodeName" node.</param>
		/// <param name="nodeName">The name of the node specifies by the derived class.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		protected ApplyBaseReadWrite( XmlReader reader, string nodeName, XacmlVersion schemaVersion )
			: base( XacmlSchema.Policy, schemaVersion )
		{
			if(reader != null)
			{
				if( reader.LocalName == nodeName && 
					ValidateSchema( reader, schemaVersion ) )
				{
					// Get the id of function. It will be resolved in evaluation time.
					_functionId = reader.GetAttribute( PolicySchema1.ConditionElement.FunctionId );

					while( reader.Read() )
					{
						switch( reader.LocalName )
						{
							case PolicySchema1.ApplyElement.Apply:
								// Must validate if the Apply node is not an EndElement because there is a child node
								// with the same name as the parent node.
								if( !reader.IsEmptyElement && reader.NodeType != XmlNodeType.EndElement )
								{
									_arguments.Add( new ApplyElement( reader, schemaVersion ) );
								}
								break;
							case PolicySchema1.FunctionElement.Function:
								_arguments.Add( new FunctionElementReadWrite( reader, schemaVersion ) );
								break;
							case PolicySchema1.AttributeValueElement.AttributeValue:
								_arguments.Add( new AttributeValueElementReadWrite( reader, schemaVersion ) );
								break;
							case PolicySchema1.SubjectAttributeDesignatorElement.SubjectAttributeDesignator:
								_arguments.Add( new SubjectAttributeDesignatorElement( reader, schemaVersion ) );
								break;
							case PolicySchema1.ResourceAttributeDesignatorElement.ResourceAttributeDesignator:
								_arguments.Add( new ResourceAttributeDesignatorElement( reader, schemaVersion ) );
								break;
							case PolicySchema1.ActionAttributeDesignatorElement.ActionAttributeDesignator:
								_arguments.Add( new ActionAttributeDesignatorElement( reader, schemaVersion ) );
								break;
							case PolicySchema1.EnvironmentAttributeDesignatorElement.EnvironmentAttributeDesignator:
								_arguments.Add( new EnvironmentAttributeDesignatorElement( reader, schemaVersion ) );
								break;
							case PolicySchema1.AttributeSelectorElement.AttributeSelector:
								_arguments.Add( new AttributeSelectorElement( reader, schemaVersion ) );
								break;
							case PolicySchema2.VariableReferenceElement.VariableReference:
								_arguments.Add( new VariableReferenceElement( reader, schemaVersion ) );
								break;
						}
						if( reader.LocalName == nodeName && 
							reader.NodeType == XmlNodeType.EndElement )
						{
							reader.Read();
							break;
						}
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
		/// The id of the function used in this element.
		/// </summary>
		public virtual string FunctionId
		{
			set{ _functionId = value; }
			get{ return _functionId; }
		}

		/// <summary>
		/// The arguments of the condition (or apply)
		/// </summary>
		public virtual IExpressionReadWriteCollection Arguments
		{
			set{ _arguments = value; }
			get{ return _arguments; }
		}
		#endregion

		#region Abstract methods

		/// <summary>
		/// Writes the XML of the current element
		/// </summary>
		/// <param name="writer">The XmlWriter in which the element will be written</param>
		public abstract void WriteDocument(XmlWriter writer);

		#endregion
	}
}
