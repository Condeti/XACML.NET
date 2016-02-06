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
using cor = Xacml.Core;

namespace Xacml.Core.Policy
{
	/// <summary>
	/// Represents a read/write AttributeAssignment node found in the Policy document. This element is not usefull in the 
	/// specification because they are only copied to the context if the Obligation is satified.
	/// </summary>
	public class AttributeAssignmentElementReadWrite : StringValueBase
	{
		#region Private members

		/// <summary>
		/// The id of the attribute.
		/// </summary>
		private string _attributeId;

		/// <summary>
		/// The data type of the value.
		/// </summary>
		private string _dataType;

		/// <summary>
		/// The contents of the AttributeValue element which represents the value of the constant.
		/// </summary>
		private string _contents;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an instance of the ReadWriteAttributeAssignment using the provided XmlReader.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the AttributeAssignament node.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public AttributeAssignmentElementReadWrite( XmlReader reader, XacmlVersion schemaVersion )
			: base( XacmlSchema.Policy, schemaVersion )
		{
            if (reader == null) throw new ArgumentNullException("reader");
			if( reader.LocalName == PolicySchema1.ObligationElement.AttributeAssignment && 
				ValidateSchema( reader, schemaVersion ) )
			{
				if( reader.HasAttributes )
				{
					// Load all the attributes
					while( reader.MoveToNextAttribute() )
					{
						if( reader.LocalName == PolicySchema1.AttributeValueElement.DataType )
						{
							_dataType = reader.GetAttribute( PolicySchema1.AttributeValueElement.DataType );
						}
						else if( reader.LocalName == PolicySchema1.AttributeAssignmentElement.AttributeId )
						{
							_attributeId = reader.GetAttribute( PolicySchema1.AttributeAssignmentElement.AttributeId );
						}
					}
					reader.MoveToElement();
				}

				// Load the node contents
				_contents = reader.ReadInnerXml();
			}
			else
			{
				throw new Exception( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_node_name, reader.LocalName ] );
			}
		}
		/// <summary>
		/// Creates an instance of the ReadWriteAttributeAssignment using the provided values
		/// </summary>
		/// <param name="attributeId"></param>
		/// <param name="dataType"></param>
		/// <param name="contents"></param>
		/// <param name="version"></param>
		public AttributeAssignmentElementReadWrite( string attributeId, string dataType, string contents, XacmlVersion version ) : 
			base( XacmlSchema.Policy, version )
		{
			_attributeId = attributeId;
			_dataType = dataType;
			_contents = contents;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Overrided method because the hash used is the hash of the contents string.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return _contents.GetHashCode();
		}

		/// <summary>
		/// Returns a string representation of the attribute assignment specifying the data type and the value.
		/// </summary>
		/// <returns>The string representation of the atrtibute assignment.</returns>
		public override string ToString()
		{
			return "[" + this._dataType + "]:" + _contents;
		}

		#endregion

		#region Public properties

		
		/// <summary>
		/// Gets the value of the assignent.
		/// </summary>
		public override string Value
		{
			set{ _contents = value; }
			get{ return _contents; }
		}

		/// <summary>
		/// Gets the data type of the attribute assignment.
		/// </summary>
		public override string DataTypeValue
		{
			set{ _dataType = value; }
			get{ return _dataType; }
		}

		/// <summary>
		/// The id of the attribute.
		/// </summary>
		public virtual string AttributeId
		{
			set{ _attributeId = value; }
			get{ return _attributeId; }
		}
		/// <summary>
		/// Whether the instance is a read only version.
		/// </summary>
		public override bool IsReadOnly
		{
			get{ return false; }
		}
		#endregion
	}
}
