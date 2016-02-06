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
using pol = Xacml.Core.Policy;
using rtm = Xacml.Core.Runtime;
using cor = Xacml.Core;

namespace Xacml.Core.Context
{
	/// <summary>
	/// Represents a Result node in the context document.
	/// </summary>
	public class ResultElement : XacmlElement
	{
		#region Private members

		/// <summary>
		/// The resource id, used only if the request was a hierarchical request.
		/// </summary>
		private string _resourceId;

		/// <summary>
		/// The decission for this Result.
		/// </summary>
		private rtm.Decision _decision;
		
		/// <summary>
		/// The Status for this result.
		/// </summary>
		private StatusElement _status;

		/// <summary>
		/// All the obligations copied during evaluation.
		/// </summary>
		private pol.ObligationCollection _obligations = new pol.ObligationCollection();

		#endregion

		#region Constructor

		/// <summary>
		/// A default constructor.
		/// </summary>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public ResultElement( XacmlVersion schemaVersion ) 
			: base( XacmlSchema.Context, schemaVersion )
		{
		}

		/// <summary>
		/// Creates a new Result using the provided information.
		/// </summary>
		/// <param name="resourceId">The resource id for this result.</param>
		/// <param name="decision">The decission of the evaluation.</param>
		/// <param name="status">The status with information about the execution.</param>
		/// <param name="obligations">The list of obligations</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public ResultElement( string resourceId, rtm.Decision decision, StatusElement status, pol.ObligationCollection obligations, XacmlVersion schemaVersion )
			: base( XacmlSchema.Context, schemaVersion )
		{
			_resourceId = resourceId;
			_decision = decision;

			// If the status is null, create an empty status
			if( status == null )
			{
				_status = new StatusElement( null, null, null, schemaVersion );
			}
			else
			{
				_status = status;
			}

			// If the obligations are null, leave the empty ObligationCollection.
			if( obligations != null )
			{
				_obligations = obligations;
			}
		}

		/// <summary>
		/// Creates a Result using an XmlReader instance provided.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the Result node.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public ResultElement( XmlReader reader, XacmlVersion schemaVersion )
			: base( XacmlSchema.Context, schemaVersion )
		{
            if (reader == null) throw new ArgumentNullException("reader");
			if( reader.LocalName == ContextSchema.ResultElement.Result )
			{
				while( reader.Read() )
				{
					switch( reader.LocalName )
					{
						case ContextSchema.ResultElement.Decision:
							// The parsing should be safe because the document was validated using a Xsd Shcema
							_decision = (rtm.Decision)Enum.Parse( typeof(rtm.Decision), reader.ReadElementString(), false );
							break;
						case ContextSchema.StatusElement.Status:
							_status = new StatusElement( reader, schemaVersion );
							break;
						case ObligationsElement.Obligations:
							while( reader.Read() )
							{
								switch( reader.LocalName )
								{
									case ObligationElement.Obligation:
										_obligations.Add( new pol.ObligationElement( reader, schemaVersion ) );
										break;
								}
								// Trick to support multiple nodes of the same name.
								if( reader.LocalName == ObligationsElement.Obligations && 
									reader.NodeType == XmlNodeType.EndElement )
								{
									reader.Read();
									break;
								}		
							}
							
							break;
					}
					if( reader.LocalName == ContextSchema.ResultElement.Result && 
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
		/// The resource id, if the request was a hierarchical request.
		/// </summary>
		public string ResourceId
		{
			get{ return _resourceId; }
		}

		/// <summary>
		/// The evaluation decission.
		/// </summary>
		public rtm.Decision Decision
		{
			get{ return _decision; }
			set{ _decision = value; }
		}

		/// <summary>
		/// The status for this result.
		/// </summary>
		public StatusElement Status
		{
			get{ return _status; }
			set{ _status = value; }
		}

		/// <summary>
		/// The obligations copied during evaluation.
		/// </summary>
		public pol.ObligationCollection Obligations
		{
			get{ return _obligations; }
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
