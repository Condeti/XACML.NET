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

namespace Xacml.Core.Context
{
	/// <summary>
	/// Represents a StatusCode in the Status node.
	/// </summary>
	public class StatusCodeElement : XacmlElement
	{
		#region Private members

		/// <summary>
		/// An inner status code.
		/// </summary>
		private StatusCodeElement _statusCode;

		/// <summary>
		/// The value of the status code.
		/// </summary>
		private string _value;

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a StatusCode using an XmlReader instance provided.
		/// </summary>
		/// <param name="reader">The XmlReader instance positioned at the StatusCode node.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public StatusCodeElement( XmlReader reader, XacmlVersion schemaVersion )
			: base( XacmlSchema.Context, schemaVersion )
		{
            if (reader == null) throw new ArgumentNullException("reader");
			if( reader.LocalName == ContextSchema.StatusElement.StatusCode )
			{
				_value = reader.GetAttribute( ContextSchema.StatusElement.Value );
				if( !reader.IsEmptyElement )
				{
					while( reader.Read() )
					{
						switch( reader.LocalName )
						{
							case ContextSchema.StatusElement.StatusCode:
								_statusCode = new StatusCodeElement( reader, schemaVersion );
								break;
						}
						if( reader.LocalName == ContextSchema.StatusElement.StatusCode && 
							reader.NodeType == XmlNodeType.EndElement )
						{
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

		/// <summary>
		/// Creates a status code using the values supplied.
		/// </summary>
		/// <param name="value">The value of the status code.</param>
		/// <param name="statusCode">Another inner status code.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public StatusCodeElement( string value, StatusCodeElement statusCode, XacmlVersion schemaVersion )
			: base( XacmlSchema.Context, schemaVersion )
		{
			_value = value;
			_statusCode = statusCode;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The inner status code.
		/// </summary>
		public StatusCodeElement InnerStatusCode
		{
			get{ return _statusCode; }
		}

		/// <summary>
		/// The value for this status code.
		/// </summary>
		public string Value
		{
			get{ return _value; }
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
