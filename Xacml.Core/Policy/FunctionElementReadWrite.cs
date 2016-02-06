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
using inf = Xacml.Core.Interfaces;

namespace Xacml.Core.Policy
{
	/// <summary>
	/// Represents a read/write Function element found in the Policy document that is used as an argument in the Apply 
	/// (or Condition) evaluation.
	/// </summary>
	public class FunctionElementReadWrite : XacmlElement, inf.IExpression
	{
		#region Private members

		/// <summary>
		/// The id of the function that will be used as argument.
		/// </summary>
		private string _functionId;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance of the Function class using the XmlReader specified.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the Function node.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public FunctionElementReadWrite( XmlReader reader, XacmlVersion schemaVersion )
			: base( XacmlSchema.Policy, schemaVersion )
		{
			if( reader.LocalName == PolicySchema1.FunctionElement.Function && 
				ValidateSchema( reader, schemaVersion ) )
			{
				_functionId = reader.GetAttribute( PolicySchema1.FunctionElement.FunctionId );
			}
			else
			{
				throw new Exception( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_node_name, reader.LocalName ] );
			}
		}

		/// <summary>
		/// Creates a new instance of the Function class using the provided values.
		/// </summary>
		/// <param name="functionId">The function id</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public FunctionElementReadWrite( string functionId, XacmlVersion schemaVersion ) : base( XacmlSchema.Policy, schemaVersion )
		{
			_functionId = functionId;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The id of the function used as an argument to the condition or apply evaluation.
		/// </summary>
		public virtual string FunctionId
		{
			get{ return _functionId; }
			set{ _functionId = value; }
		}
		/// <summary>
		/// Whether the instance is a read only version.
		/// </summary>
		public override bool IsReadOnly
		{
			get{ return false; }
		}
		#endregion

		#region Public methods

		/// <summary>
		/// Writes the XML of the current element
		/// </summary>
		/// <param name="writer">The XmlWriter in which the element will be written</param>
		public void WriteDocument(XmlWriter writer)
		{
			writer.WriteStartElement( PolicySchema1.FunctionElement.Function );
			if( this._functionId != null && this._functionId.Length > 0 )
			{
				writer.WriteAttributeString( PolicySchema1.FunctionElement.FunctionId, this._functionId );
			}
			writer.WriteEndElement();
		}

		#endregion
	}
}
