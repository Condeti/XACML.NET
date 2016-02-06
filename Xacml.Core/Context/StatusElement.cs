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

namespace Xacml.Core.Context
{
	/// <summary>
	/// Represents the Status of the Result node.
	/// </summary>
	public class StatusElement : XacmlElement
	{
		#region Private members

		/// <summary>
		/// The status code.
		/// </summary>
		private StatusCodeElement _statusCode;

		/// <summary>
		/// The status message.
		/// </summary>
		private string _statusMessage;

		/// <summary>
		/// The status detail.
		/// </summary>
		private string _statusDetail;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a Status using the supplied values.
		/// </summary>
		/// <param name="statusCode">The status code.</param>
		/// <param name="statusMessage">The status message.</param>
		/// <param name="statusDetail">The status detail.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public StatusElement( StatusCodeElement statusCode, string statusMessage, string statusDetail, XacmlVersion schemaVersion )
			: base( XacmlSchema.Context, schemaVersion )
		{
			_statusCode = statusCode;
			_statusMessage = statusMessage;
			_statusDetail = statusDetail;
		}

		/// <summary>
		/// Creates a new Status class using the XmlReade instance provided.
		/// </summary>
		/// <param name="reader">The XmlReader instance positioned at the Status node.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public StatusElement( XmlReader reader, XacmlVersion schemaVersion )
			: base( XacmlSchema.Context, schemaVersion )
		{
            if (reader == null) throw new ArgumentNullException("reader");
			if( reader.LocalName == ContextSchema.StatusElement.Status )
			{
				while( reader.Read() )
				{
					switch( reader.LocalName )
					{
						case ContextSchema.StatusElement.StatusCode:
							_statusCode = new StatusCodeElement( reader, schemaVersion );
							break;
						case ContextSchema.StatusElement.StatusMessage:
							_statusMessage = reader.ReadElementString();
							break;
						case ContextSchema.StatusElement.StatusDetail:
							_statusDetail = reader.ReadElementString();
							break;
					}
					if( reader.LocalName == ContextSchema.StatusElement.Status && 
						reader.NodeType == XmlNodeType.EndElement )
					{
						break;
					}
				}
			}
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The status message.
		/// </summary>
		public string StatusMessage
		{
			get{ return _statusMessage; }
		}

		/// <summary>
		/// The status detail.
		/// </summary>
		public string StatusDetail
		{
			get{ return _statusDetail; }
		}

		/// <summary>
		/// The status code.
		/// </summary>
		public StatusCodeElement StatusCode
		{
			get{ return _statusCode; }
			set{ _statusCode = value; }
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
