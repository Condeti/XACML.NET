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
using Xacml.PolicySchema1;
using cor = Xacml.Core;

namespace Xacml.Core.Policy
{
	/// <summary>
	/// Represents a generic read/write match found in the target items of the Policy document. 
	/// </summary>
	public abstract class TargetMatchBaseReadWrite : XacmlElement
	{
		#region Private members

		/// <summary>
		/// The id of the match which is the id of the function that will be used to evaluate the match.
		/// </summary>
		private string _id;

		/// <summary>
		/// The attribute value used as the first argument to the function.
		/// </summary>
		private AttributeValueElementReadWrite _attributeValue;

		/// <summary>
		/// The AttributeReference used as the second parameter to the function. This reference can be an 
		/// AttributeSelector or an AttributeDesignator.
		/// </summary>
		private AttributeReferenceBase _attributeReference;

		#endregion

		#region Constructor

		/// <summary>
		/// Creates an instance of a TargetMatchBase with the values specified.
		/// </summary>
		/// <param name="matchId">The match id</param>
		/// <param name="attributeValue">The attribute value instance.</param>
		/// <param name="attributeReference">An attribute reference instance.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		protected TargetMatchBaseReadWrite( string matchId, AttributeValueElementReadWrite attributeValue, AttributeReferenceBase attributeReference, XacmlVersion schemaVersion )
			: base( XacmlSchema.Policy, schemaVersion )
		{
			_id = matchId;
			_attributeValue = attributeValue;
			_attributeReference = attributeReference;
		}

		/// <summary>
		/// Creates an instance of the TargetMatchBase class using the XmlReader specified, the name of the node that defines
		/// the match (the name of the node changes depending on the target item that defines it) and the attribute
		/// designator node name which also changes depending on the target item that defines the match.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the "matchNodeName" node.</param>
		/// <param name="matchNodeName">The name of the match node for this target item.</param>
		/// <param name="attributeDesignatorNode">The name of the attribute designator node for this target item.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		protected TargetMatchBaseReadWrite( XmlReader reader, string matchNodeName, string attributeDesignatorNode, XacmlVersion schemaVersion )
			: base( XacmlSchema.Policy, schemaVersion )
		{
            if (reader == null) throw new ArgumentNullException("reader");
			if( reader.LocalName == matchNodeName && ValidateSchema( reader, schemaVersion ) )
			{
				_id = reader.GetAttribute( MatchElement.MatchId );
				while( reader.Read() )
				{
					if( reader.LocalName == PolicySchema1.AttributeValueElement.AttributeValue && 
						ValidateSchema( reader, schemaVersion ) && 
						reader.NodeType != XmlNodeType.EndElement )
					{
						_attributeValue = new AttributeValueElementReadWrite( reader, schemaVersion );
					}
					else if( reader.LocalName == attributeDesignatorNode && 
						ValidateSchema( reader, schemaVersion ) && 
						reader.NodeType != XmlNodeType.EndElement )
					{
						_attributeReference = CreateAttributeDesignator( reader );
					}
					else if( reader.LocalName == PolicySchema1.AttributeSelectorElement.AttributeSelector && 
						ValidateSchema( reader, schemaVersion ) && 
						reader.NodeType != XmlNodeType.EndElement )
					{
						_attributeReference = new AttributeSelectorElement( reader, schemaVersion );
					}
					else if( reader.LocalName == matchNodeName && 
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

		#region Abstract methods

		/// <summary>
		/// Method called to create the attribute designator that mathces the type of the target item found.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the attribute designator node.</param>
		/// <returns>An instance of an attribute designator or a derived class.</returns>
		protected abstract AttributeDesignatorBase CreateAttributeDesignator( XmlReader reader );

		#endregion

		#region Public properties

		/// <summary>
		/// The id of the mathc which is the id of the function used to evaluate the match.
		/// </summary>
		public virtual string MatchId
		{
			get{ return _id; }
			set{ _id = value; }
		}

		/// <summary>
		/// The attribute value used as the first argument of the function.
		/// </summary>
		public virtual AttributeValueElementReadWrite AttributeValue
		{
			get{ return _attributeValue; }
			set{ _attributeValue = value; }
		}

		/// <summary>
		/// The attribute reference used as a second argument of the function. This reference is resolved by the 
		/// EvaluationEngine before passing the value to the function.
		/// </summary>
		public virtual AttributeReferenceBase AttributeReference
		{
			get{ return _attributeReference; }
			set{ _attributeReference = value; }
		}

		#endregion
	}
}
