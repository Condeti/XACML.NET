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
using ctx = Xacml.Core.Context;

namespace Xacml.Core.Policy
{
	/// <summary>
	/// Represents a generic attribute designator found in the Policy document. The AttributeDesignator is a node
	/// that must be replaced with the value of the designated node in the context document. The algorithm that 
	/// resolves the designation is in the EvaluationEngine class.
	/// </summary>
	/// <remarks>The algorith used to match the attrbute designator with the Context document is defined in the 
	/// section 7.9 of the XACML specification.</remarks>
	public abstract class AttributeDesignatorBase : AttributeReferenceBase
	{
		#region Private members

		/// <summary>
		/// The Id of the designated attribute.
		/// </summary>
		private string _attributeId = String.Empty;

		/// <summary>
		/// The issuer of the designated attribute.
		/// </summary>
		private string _issuer = String.Empty;

		#endregion

		#region Constructor

		/// <summary>
		/// Creates an instance of the AttributeDesignator using the arguments specified.
		/// </summary>
		/// <param name="dataType">The data type id.</param>
		/// <param name="mustBePresent">Whether the attribute must be present.</param>
		/// <param name="attributeId">The attribute id.</param>
		/// <param name="issuer">The issuer id.</param>
		/// <param name="version">The version of the schema that was used to validate.</param>
		protected AttributeDesignatorBase( string dataType, bool mustBePresent, string attributeId, string issuer, XacmlVersion version )
			: base( dataType, mustBePresent, version )
		{
			_attributeId = attributeId;
			_issuer = issuer;
		}

		/// <summary>
		/// Creates an instance of the AttributeDesignator class with the XmlReader instance. The base class 
		/// constructor is also called using the XmlReader.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the begining of the AttributeDesignator node.</param>
		/// <param name="version">The version of the schema that was used to validate.</param>
		protected AttributeDesignatorBase( XmlReader reader, XacmlVersion version ) : 
			base( reader, version )
		{
            if (reader == null) throw new ArgumentNullException("reader");
			_attributeId = reader.GetAttribute( AttributeDesignatorElement.AttributeId );
			_issuer = reader.GetAttribute( AttributeDesignatorElement.Issuer );
			if( _issuer == null )
			{
				_issuer = String.Empty;
			}
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// The Id of the designated attribute.
		/// </summary>
		public string AttributeId
		{
			get{ return _attributeId; }
			set{ _attributeId = value; }
		}

		/// <summary>
		/// The issuer of the designated attribute.
		/// </summary>
		public string Issuer
		{
			get{ return _issuer; }
			set{ _issuer = value; }
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Returns a string representation of the attribute designator placing the attribute id and the data type.
		/// </summary>
		/// <returns>The string representation of the instance.</returns>
		public override string ToString()
		{
			return "[" + this._attributeId + "[" + DataType + "]" + "]";
		}

		#endregion
	}
}
