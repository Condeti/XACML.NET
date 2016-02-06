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
	/// Represents a CombinerParameter defined in the policy document.
	/// </summary>
	public class CombinerParameterElement : XacmlElement
	{
		#region Private members

		/// <summary>
		/// The parameter name.
		/// </summary>
		private string _parameterName;

		/// <summary>
		/// The attribute value.
		/// </summary>
		private AttributeValueElement _attributeValue;

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a new CombinerParameter using the provided argument values.
		/// </summary>
		/// <param name="parameterName">The parameter name.</param>
		/// <param name="attributeValue">The attribute value.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public CombinerParameterElement( string parameterName, AttributeValueElement attributeValue, XacmlVersion schemaVersion )
			: base( XacmlSchema.Policy, schemaVersion )
		{
			_parameterName = parameterName;
			_attributeValue = attributeValue;
		}

		/// <summary>
		/// Creates a new CombinerParameterElement using the XmlReader instance provided.
		/// </summary>
		/// <param name="reader">The XmlReader instance positioned at the CombinerParameterElement node.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public CombinerParameterElement( XmlReader reader, XacmlVersion schemaVersion ) : 
			this( reader, PolicySchema2.CombinerParameterElement.CombinerParameter, schemaVersion )
		{
		}

		/// <summary>
		/// Creates a new CombinerParameterElement using the XmlReader instance provided.
		/// </summary>
		/// <param name="reader">The XmlReader instance positioned at the CombinerParameterElement node.</param>
		/// <param name="nodeName">The name of the node for this combiner parameter item.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		protected CombinerParameterElement( XmlReader reader, string nodeName, XacmlVersion schemaVersion )
			: base( XacmlSchema.Policy, schemaVersion )
		{
            if (reader == null) throw new ArgumentNullException("reader");
			if( reader.LocalName == nodeName && 
				ValidateSchema( reader, schemaVersion ) )
			{
				// Read the attributes
				if( reader.HasAttributes )
				{
					// Load all the attributes
					while( reader.MoveToNextAttribute() )
					{
						if( reader.LocalName == PolicySchema2.CombinerParameterElement.ParameterName )
						{
							_parameterName = reader.GetAttribute( PolicySchema2.CombinerParameterElement.ParameterName );
						}
						else
						{
							AttributeFound( reader );
						}
					}
					reader.MoveToElement();
				}

				// Read the rule contents.
				while( reader.Read() )
				{
					switch( reader.LocalName )
					{
						case PolicySchema2.CombinerParameterElement.AttributeValue:
							_attributeValue = new AttributeValueElement( reader, schemaVersion );
							break;
					}
					if( reader.LocalName == PolicySchema2.CombinerParameterElement.CombinerParameter && 
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

		#region Protected methods

		/// <summary>
		/// Method called when unknown attributes are found during parsing of this element. Derived classes will use this
		/// method to being notified about attributes.
		/// </summary>
		/// <param name="reader">The reader positioned at the attribute.</param>
		protected virtual void AttributeFound( XmlReader reader )
		{
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The parameter name
		/// </summary>
		public string ParameterName
		{
			get{ return _parameterName; }
		}

		/// <summary>
		/// The effect of the rule.
		/// </summary>
		public AttributeValueElement AttributeValue
		{
			get{ return _attributeValue; }
		}
		/// <summary>
		/// Whether the instance is a read only version.
		/// </summary>
		public override bool IsReadOnly
		{
			get{ return true; }
		}
		#endregion
	}
}
