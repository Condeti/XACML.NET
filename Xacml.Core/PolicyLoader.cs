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
 * The Original Code is com code.
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
using Xacml.PolicySchema1;
using pol = Xacml.Core.Policy;

namespace Xacml.Core
{
	/// <summary>
	/// Loads a Policy document from the Xml data provided.
	/// </summary>
	public sealed class PolicyLoader
	{
		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		private PolicyLoader()
		{
		}
		
		#endregion

		#region Public methods

		/// <summary>
		/// Creates an instace of the PolicyDocument using the provided Xml document string.
		/// </summary>
		/// <param name="xmlDocument">The Xml document fragment.</param>
		/// <param name="access">The type of PolicyDocument</param>
		/// <returns>An instance of a PolicyDocument.</returns>
		public static pol.PolicyDocumentReadWrite LoadPolicyDocument( string xmlDocument, DocumentAccess access )
		{
			// Validate the parameters
			if( xmlDocument == null )
			{
				throw new ArgumentNullException( nameof(xmlDocument) );
			}

			// Read the document to determine the version of the schema used.
			XacmlVersion version = GetXacmlVersion( new StreamReader( xmlDocument ) );

			return LoadPolicyDocument( new StringReader( xmlDocument ), version, access );
		}

		/// <summary>
		/// Creates an instace of the PolicyDocument using the stream provided with an Xml document.
		/// </summary>
		/// <param name="xmlDocument">The stream containing an Xml document.</param>
		/// <param name="access">The type of PolicyDocument</param>
		/// <returns>An instance of a PolicyDocument.</returns>
		public static pol.PolicyDocumentReadWrite LoadPolicyDocument( Stream xmlDocument, DocumentAccess access )
		{			
			// Validate the parameters
			if( xmlDocument == null )
			{
				throw new ArgumentNullException( nameof(xmlDocument) );
			}

			// Validate the stream
			if( !xmlDocument.CanSeek )
			{
				throw new ArgumentException( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_stream_parameter_canseek ], nameof(xmlDocument) );
			}

			// Read the document to determine the version of the schema used.
			XacmlVersion version = GetXacmlVersion( new StreamReader( xmlDocument ) );

			xmlDocument.Position = 0;

			return LoadPolicyDocument( new StreamReader( xmlDocument ), version, access );
		}

		/// <summary>
		/// Creates an instace of the PolicyDocument using the stream provided with an Xml document.
		/// </summary>
		/// <param name="xmlDocument">The stream containing an Xml document.</param>
		/// <param name="version">The version of the schema that will be used to validate.</param>
		/// <param name="access">The type of PolicyDocument</param>
		/// <returns>An instance of a PolicyDocument.</returns>
		public static pol.PolicyDocumentReadWrite LoadPolicyDocument( Stream xmlDocument, XacmlVersion version, DocumentAccess access )
		{			
			// Validate the parameters
			if( xmlDocument == null )
			{
				throw new ArgumentNullException( nameof(xmlDocument) );
			}

			return LoadPolicyDocument( new StreamReader( xmlDocument ), version, access );
		}

		
		/// <summary>
		/// Creates an instace of the PolicyDocument using the provided Xml document string.
		/// </summary>
		/// <param name="xmlDocument">The Xml document fragment.</param>
		/// <param name="version">The version of the schema that will be used to validate.</param>
		/// <param name="access">The type of PolicyDocument</param>
		/// <returns>An instance of a PolicyDocument.</returns>
		public static pol.PolicyDocumentReadWrite LoadPolicyDocument( TextReader xmlDocument, XacmlVersion version, DocumentAccess access )
		{
			// Validate the parameters
			if( xmlDocument == null )
			{
				throw new ArgumentNullException( nameof(xmlDocument) );
			}

			return LoadPolicyDocument( new XmlTextReader( xmlDocument ), version, access );
		}
		/// <summary>
		/// Creates an instance of the PolicyDocument using the XmlReader instance provided.
		/// </summary>
		/// <param name="reader">The XmlReader used to read the Xml document.</param>
		/// <param name="version">The version of the schema that will be used to validate.</param>
		/// <param name="access">The type of PolicyDocument</param>
		/// <returns>An instance of a PolicyDocument.</returns>
		public static pol.PolicyDocumentReadWrite LoadPolicyDocument( XmlReader reader, XacmlVersion version, DocumentAccess access )
		{
			// Validate the parameters
			if( reader == null )
			{
				throw new ArgumentNullException( nameof(reader) );
			}

			if( access.Equals(DocumentAccess.ReadOnly) )
			{
				return new pol.PolicyDocument( reader, version );
			}
			else if( access.Equals(DocumentAccess.ReadWrite) )
			{
				return new pol.PolicyDocumentReadWrite( reader, version );
			}
			return null;
		}

		/// <summary>
		/// Creates a read-only instace of the PolicyDocument using the provided Xml document string.
		/// </summary>
		/// <param name="xmlDocument">The Xml document fragment.</param>
		/// <returns>An instance of a PolicyDocument.</returns>
		public static pol.PolicyDocumentReadWrite LoadPolicyDocument( string xmlDocument)
		{
			// Validate the parameters
			if( xmlDocument == null )
			{
				throw new ArgumentNullException( nameof(xmlDocument) );
			}

			// Read the document to determine the version of the schema used.
			XacmlVersion version = GetXacmlVersion( new StreamReader( xmlDocument ) );

			return LoadPolicyDocument( new StringReader( xmlDocument ), version);
		}

		/// <summary>
		/// Creates a read-only instace of the PolicyDocument using the stream provided with an Xml document.
		/// </summary>
		/// <param name="xmlDocument">The stream containing an Xml document.</param>
		/// <returns>An instance of a PolicyDocument.</returns>
		public static pol.PolicyDocumentReadWrite LoadPolicyDocument( Stream xmlDocument )
		{			
			// Validate the parameters
			if( xmlDocument == null )
			{
				throw new ArgumentNullException( nameof(xmlDocument) );
			}

			// Validate the stream
			if( !xmlDocument.CanSeek )
			{
				throw new ArgumentException( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_stream_parameter_canseek ], nameof(xmlDocument) );
			}

			// Read the document to determine the version of the schema used.
			XacmlVersion version = GetXacmlVersion( new StreamReader( xmlDocument ) );

			xmlDocument.Position = 0;

			return LoadPolicyDocument( new StreamReader( xmlDocument ), version );
		}

		/// <summary>
		/// Creates a read-only instace of the PolicyDocument using the stream provided with an Xml document.
		/// </summary>
		/// <param name="xmlDocument">The stream containing an Xml document.</param>
		/// <param name="version">The version of the schema that will be used to validate.</param>
		/// <returns>An instance of a PolicyDocument.</returns>
		public static pol.PolicyDocumentReadWrite LoadPolicyDocument( Stream xmlDocument, XacmlVersion version )
		{			
			// Validate the parameters
			if( xmlDocument == null )
			{
				throw new ArgumentNullException( nameof(xmlDocument) );
			}

			return LoadPolicyDocument( new StreamReader( xmlDocument ), version );
		}

		
		/// <summary>
		/// Creates a read-only instace of the PolicyDocument using the provided Xml document string.
		/// </summary>
		/// <param name="xmlDocument">The Xml document fragment.</param>
		/// <param name="version">The version of the schema that will be used to validate.</param>
		/// <returns>An instance of a PolicyDocument.</returns>
		public static pol.PolicyDocumentReadWrite LoadPolicyDocument( TextReader xmlDocument, XacmlVersion version )
		{
			// Validate the parameters
			if( xmlDocument == null )
			{
				throw new ArgumentNullException( nameof(xmlDocument) );
			}

			return LoadPolicyDocument( new XmlTextReader( xmlDocument ), version );
		}
		/// <summary>
		/// Creates a read-only instance of the PolicyDocument using the XmlReader instance provided.
		/// </summary>
		/// <param name="reader">The XmlReader used to read the Xml document.</param>
		/// <param name="version">The version of the schema that will be used to validate.</param>
		/// <returns>An instance of a PolicyDocument.</returns>
		public static pol.PolicyDocumentReadWrite LoadPolicyDocument( XmlReader reader, XacmlVersion version )
		{
			// Validate the parameters
			if( reader == null )
			{
				throw new ArgumentNullException( nameof(reader) );
			}

			return new pol.PolicyDocument( reader, version );
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Reads the document to the first Policy or PolicySet element and compares the Namespace of that
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
				if( reader.LocalName == PolicySetElement.PolicySet || 
					reader.LocalName == PolicyElement.Policy )
				{
					if( reader.NamespaceURI == Namespaces.Policy )
					{
						return XacmlVersion.Version11;
					}
					else if( reader.NamespaceURI == PolicySchema2.Namespaces.Policy )
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
