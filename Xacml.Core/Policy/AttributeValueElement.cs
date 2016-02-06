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
	/// Defines a read-only AttributeValue found in the Policy document. This node is used to define constants which are 
	/// defined as string values and are converted to the correct data type during policy evaluation. 
	/// </summary>
	public class AttributeValueElement : AttributeValueElementReadWrite
	{
		#region Constructor

		/// <summary>
		/// Creates a new AttributeValue using the data type and the contents provided.
		/// </summary>
		/// <param name="dataType">The data type id.</param>
		/// <param name="contents">The contents of the attribute value.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public AttributeValueElement( string dataType, string contents, XacmlVersion schemaVersion )
			: base( dataType, contents, schemaVersion )
		{
		}

		/// <summary>
		/// Creates an instance of the AttributeValue class using the XmlReader and the name of the node that 
		/// defines the AttributeValue. 
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the node AttributeValue.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public AttributeValueElement( XmlReader reader, XacmlVersion schemaVersion )
			: base( reader, schemaVersion )
		{
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Gets the contents of the AttributeValue as a string.
		/// </summary>
		public override string Contents
		{
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// The data type of the value.
		/// </summary>
		public override string DataType
		{
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// Overrides the Value of the abstract class StringValue.
		/// </summary>
		public override string Value
		{
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// Gets the DataType of the AttributeValue as a string.
		/// </summary>
		public override string DataTypeValue
		{
			set{ throw new NotSupportedException(); }
		}

		#endregion
	}
}
