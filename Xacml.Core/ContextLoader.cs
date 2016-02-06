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
using System.IO;
using System.Xml;
using Xacml.ContextSchema;
using Xacml.PolicySchema1;
using ctx = Xacml.Core.Context;

namespace Xacml.Core
{
	/// <summary>
	/// Helper class used to load a context document which can be a Request or a Response.
	/// </summary>
	/// <remarks>Reading a Response context document is not really needed by the implementation but it's used to
	/// compare the Response emited by the evaluation with the Response provided in the Conformance tests.</remarks>
	public sealed class ContextLoader
	{
		#region Constructor

		/// <summary>
		/// Default constructor.
		/// </summary>
		private ContextLoader()
		{
		}

		#endregion

		#region Static methods

		/// <summary>
		/// Creates an instace of the ContextDocument using the provided Xml document string.
		/// </summary>
		/// <param name="xmlDocument">The Xml document fragment.</param>
		/// <returns>An instance of a ContextDocument/</returns>
		public static ctx.ContextDocumentReadWrite LoadContextDocument( string xmlDocument )
		{
			// Validate the parameters
			if( xmlDocument == null )
			{
				throw new ArgumentNullException( "xmlDocument" );
			}

			// Read the document to determine the version of the schema used.
			XacmlVersion version = GetXacmlVersion( new StreamReader( xmlDocument ) );

			return LoadContextDocument( new XmlTextReader( new StringReader( xmlDocument ) ), version );
		}

		/// <summary>
		/// Creates an instace of the ContextDocument using the stream provided with an Xml document.
		/// </summary>
		/// <param name="xmlDocument">The stream containing an Xml document.</param>
		/// <returns>An instance of a ContextDocument.</returns>
		public static ctx.ContextDocumentReadWrite LoadContextDocument( Stream xmlDocument )
		{
			// Validate the parameters
			if( xmlDocument == null )
			{
				throw new ArgumentNullException( "xmlDocument" );
			}

			// Read the document to determine the version of the schema used.
			XacmlVersion version = GetXacmlVersion( new StreamReader( xmlDocument ) );
			
			xmlDocument.Position = 0;

			return LoadContextDocument( new XmlTextReader( new StreamReader( xmlDocument ) ), version );
		}

		/// <summary>
		/// Creates an instace of the ContextDocument using the provided Xml document string.
		/// </summary>
		/// <param name="xmlDocument">The Xml document fragment.</param>
		/// <param name="schemaVersion">The version of the schema used to validate the document.</param>
		/// <returns>An instance of a ContextDocument/</returns>
		public static ctx.ContextDocumentReadWrite LoadContextDocument( string xmlDocument, XacmlVersion schemaVersion )
		{
			// Validate the parameters
			if( xmlDocument == null )
			{
				throw new ArgumentNullException( "xmlDocument" );
			}

			return LoadContextDocument( new XmlTextReader( new StringReader( xmlDocument ) ), schemaVersion );
		}

		/// <summary>
		/// Creates an instace of the ContextDocument using the stream provided with an Xml document.
		/// </summary>
		/// <param name="xmlDocument">The stream containing an Xml document.</param>
		/// <param name="schemaVersion">The version of the schema used to validate the document.</param>
		/// <returns>An instance of a ContextDocument.</returns>
		public static ctx.ContextDocumentReadWrite LoadContextDocument( Stream xmlDocument, XacmlVersion schemaVersion )
		{
			// Validate the parameters
			if( xmlDocument == null )
			{
				throw new ArgumentNullException( "xmlDocument" );
			}

			return LoadContextDocument( new XmlTextReader( new StreamReader( xmlDocument ) ), schemaVersion );
		}

		/// <summary>
		/// Creates an instance of the ContextDocument using the XmlReader instance provided.
		/// </summary>
		/// <param name="reader">The XmlReader used to read the Xml document.</param>
		/// <param name="schemaVersion">The versoin of the schema used to validate the document.</param>
		/// <returns>An instance of a ContextDocument.</returns>
		public static ctx.ContextDocumentReadWrite LoadContextDocument( XmlReader reader, XacmlVersion schemaVersion )
		{
			// Validate the parameters
			if( reader == null )
			{
				throw new ArgumentNullException( "reader" );
			}

			return new ctx.ContextDocument( reader, schemaVersion );
		}
		/// <summary>
		/// Creates an instace of the ContextDocument using the provided Xml document string.
		/// </summary>
		/// <param name="xmlDocument">The Xml document fragment.</param>
		/// <param name="access">The access to the document (read-write/read-only)</param>
		/// <returns>An instance of a ContextDocument/</returns>
		public static ctx.ContextDocumentReadWrite LoadContextDocument( string xmlDocument, DocumentAccess access )
		{
			// Validate the parameters
			if( xmlDocument == null )
			{
				throw new ArgumentNullException( "xmlDocument" );
			}

			// Read the document to determine the version of the schema used.
			XacmlVersion version = GetXacmlVersion( new StreamReader( xmlDocument ) );

			return LoadContextDocument( new XmlTextReader( new StringReader( xmlDocument ) ), version, access );
		}

		/// <summary>
		/// Creates an instace of the ContextDocument using the stream provided with an Xml document.
		/// </summary>
		/// <param name="xmlDocument">The stream containing an Xml document.</param>
		/// <param name="access">The access to the document (read-write/read-only)</param>
		/// <returns>An instance of a ContextDocument.</returns>
		public static ctx.ContextDocumentReadWrite LoadContextDocument( Stream xmlDocument, DocumentAccess access )
		{
			// Validate the parameters
			if( xmlDocument == null )
			{
				throw new ArgumentNullException( "xmlDocument" );
			}

			// Read the document to determine the version of the schema used.
			XacmlVersion version = GetXacmlVersion( new StreamReader( xmlDocument ) );
			
			xmlDocument.Position = 0;

			return LoadContextDocument( new XmlTextReader( new StreamReader( xmlDocument ) ), version, access );
		}

		/// <summary>
		/// Creates an instace of the ContextDocument using the provided Xml document string.
		/// </summary>
		/// <param name="xmlDocument">The Xml document fragment.</param>
		/// <param name="schemaVersion">The version of the schema used to validate the document.</param>
		/// <param name="access">The access to the document (read-write/read-only)</param>
		/// <returns>An instance of a ContextDocument/</returns>
		public static ctx.ContextDocumentReadWrite LoadContextDocument( string xmlDocument, XacmlVersion schemaVersion, DocumentAccess access )
		{
			// Validate the parameters
			if( xmlDocument == null )
			{
				throw new ArgumentNullException( "xmlDocument" );
			}

			return LoadContextDocument( new XmlTextReader( new StringReader( xmlDocument ) ), schemaVersion, access );
		}

		/// <summary>
		/// Creates an instace of the ContextDocument using the stream provided with an Xml document.
		/// </summary>
		/// <param name="xmlDocument">The stream containing an Xml document.</param>
		/// <param name="schemaVersion">The version of the schema used to validate the document.</param>
		/// <param name="access">The access to the document (read-write/read-only)</param>
		/// <returns>An instance of a ContextDocument.</returns>
		public static ctx.ContextDocumentReadWrite LoadContextDocument( Stream xmlDocument, XacmlVersion schemaVersion, DocumentAccess access )
		{
			// Validate the parameters
			if( xmlDocument == null )
			{
				throw new ArgumentNullException( "xmlDocument" );
			}

			return LoadContextDocument( new XmlTextReader( new StreamReader( xmlDocument ) ), schemaVersion, access );
		}

		/// <summary>
		/// Creates an instance of the ContextDocument using the XmlReader instance provided.
		/// </summary>
		/// <param name="reader">The XmlReader used to read the Xml document.</param>
		/// <param name="schemaVersion">The versoin of the schema used to validate the document.</param>
		/// <param name="access">The access to the document (read-write/read-only)</param>
		/// <returns>An instance of a ContextDocument.</returns>
		public static ctx.ContextDocumentReadWrite LoadContextDocument( XmlReader reader, XacmlVersion schemaVersion, DocumentAccess access )
		{
			// Validate the parameters
			if( reader == null )
			{
				throw new ArgumentNullException( "reader" );
			}

			if( access == DocumentAccess.ReadOnly )
			{
				return new ctx.ContextDocument( reader, schemaVersion );
			}
			else if ( access == DocumentAccess.ReadWrite )
			{
				return new ctx.ContextDocumentReadWrite( reader, schemaVersion );
			}
			return null;
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Reads the document to the first Request or Response element and compares the Namespace of that
		/// element with the namespaces for the different versions of the spec and determines the version of the
		/// schema that will be used.
		/// </summary>
		/// <param name="textReader">A reader positioned and ready to process.</param>
		/// <returns>The vesion of the schema required in the policy document.</returns>
		private static XacmlVersion GetXacmlVersion( TextReader textReader )
		{
			XmlTextReader reader = new XmlTextReader( textReader );
			while( reader.Read() )
			{
				if( reader.LocalName == RequestElement.Request || 
					reader.LocalName == ResponseElement.Response )
				{
					if( reader.NamespaceURI == Namespaces.Context )
					{
						return XacmlVersion.Version11;
					}
					else if( reader.NamespaceURI == PolicySchema2.Namespaces.Context )
					{
						return XacmlVersion.Version20;
					}
				}
			}
			throw new EvaluationException( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_document_format_no_policyorpolicyset ] );
		}

		#endregion
	}
}
