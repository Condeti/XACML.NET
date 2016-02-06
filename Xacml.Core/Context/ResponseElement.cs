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
using cor = Xacml.Core;

namespace Xacml.Core.Context
{
	/// <summary>
	/// Represents a Response document created during the evaluation.
	/// </summary>
	public class ResponseElement : XacmlElement
	{
		#region Private members

		/// <summary>
		/// The Result elements of this Response.
		/// </summary>
		private ResultCollection _results;

		#endregion

		#region Constructors

		/// <summary>
		/// Create a new Response using the Result list provided.
		/// </summary>
		/// <param name="results">The list of Results that will be contained in this Response.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public ResponseElement( ResultElement[] results, XacmlVersion schemaVersion )
			: base( XacmlSchema.Context, schemaVersion )
		{
			_results = new ResultCollection();
			if( results != null )
			{
				foreach( ResultElement result in results )
				{
					_results.Add( result );
				}
			}
		}

		/// <summary>
		/// Creates a response using the XmlReader instance provided.
		/// </summary>
		/// <remarks>This method is only provided for testing purposes, because it's easy to run the ConformanceTests
		/// comparing the expected response with the response created.</remarks>
		/// <param name="reader">The XmlReader positioned at the Response node.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public ResponseElement( XmlReader reader, XacmlVersion schemaVersion )
			: base( XacmlSchema.Context, schemaVersion )
		{
            if (reader == null) throw new ArgumentNullException("reader");
			_results = new ResultCollection();
			if( reader.LocalName == ContextSchema.ResponseElement.Response )
			{
				while( reader.Read() )
				{
					switch( reader.LocalName )
					{
						case ContextSchema.ResultElement.Result:
							_results.Add( new ResultElement( reader, schemaVersion ) );
							break;
					}
					if( reader.LocalName == ContextSchema.ResponseElement.Response && 
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
		/// Gets the list of results.
		/// </summary>
		public ResultCollection Results
		{
			get{ return _results; }
		}
		/// <summary>
		/// Whether the instance is a read only version.
		/// </summary>
		public override bool IsReadOnly
		{
			get{ return true; }
		}
		#endregion

		#region Public methods

		/// <summary>
		/// Writes the Xml of this Response document.
		/// </summary>
		/// <param name="writer">An XmlWriter where the xml will be sent.</param>
		public void WriteDocument( XmlWriter writer )
		{
            if (writer == null) throw new ArgumentNullException("writer");
			writer.WriteStartDocument();
			writer.WriteStartElement( ContextSchema.ResponseElement.Response, Namespaces.Context );
			foreach( ResultElement result in Results )
			{
				// Create the Result node
				writer.WriteStartElement( ContextSchema.ResultElement.Result, Namespaces.Context );

				// Create the Decission node
				writer.WriteElementString( ContextSchema.ResultElement.Decision, result.Decision.ToString() );

				// Create the Status node
				writer.WriteStartElement( ContextSchema.StatusElement.Status );
				writer.WriteStartElement( ContextSchema.StatusElement.StatusCode );
				writer.WriteAttributeString( ContextSchema.StatusElement.Value, result.Status.StatusCode.Value );
				writer.WriteEndElement();
				writer.WriteEndElement();

				// Create the obligations node
				if( result.Obligations != null && result.Obligations.Count != 0 )
				{
					writer.WriteStartElement( ObligationsElement.Obligations );
					foreach( pol.ObligationElement obligation in result.Obligations )
					{
						writer.WriteStartElement( ObligationElement.Obligation );
						writer.WriteAttributeString( ObligationElement.ObligationId, obligation.ObligationId );
						writer.WriteAttributeString( ObligationElement.FulfillOn, obligation.FulfillOn.ToString() );

						if( obligation.AttributeAssignment != null && obligation.AttributeAssignment.Count != 0 )
						{
							foreach( pol.AttributeAssignmentElement attrAssign in obligation.AttributeAssignment )
							{
								writer.WriteStartElement( ObligationElement.AttributeAssignment );
								writer.WriteAttributeString( AttributeAssignmentElement.AttributeId, attrAssign.AttributeId );
								writer.WriteAttributeString( PolicySchema1.AttributeValueElement.DataType, attrAssign.DataTypeValue );

								writer.WriteString( attrAssign.Value );

								writer.WriteEndElement();
							}
						}

						writer.WriteEndElement();
					}
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
			writer.WriteEndDocument();
		}

		#endregion
	}
}