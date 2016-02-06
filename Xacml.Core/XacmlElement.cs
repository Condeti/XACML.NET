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

namespace Xacml.Core
{
	/// <summary>
	/// Base class for every element so common methods can be placed in this class.
	/// </summary>
	public abstract class XacmlElement
	{
		#region Private members

		/// <summary>
		/// The version of the schema that was used to validate.
		/// </summary>
		private XacmlVersion _schemaVersion;

		/// <summary>
		/// The schema that defines the element.
		/// </summary>
		private XacmlSchema _schema;

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="schema">The schema that defines the element.</param>
		/// <param name="schemaVersion">The version of the schema used to load the element.</param>
		protected XacmlElement( XacmlSchema schema, XacmlVersion schemaVersion )
		{
			_schema = schema;
			_schemaVersion = schemaVersion;
		}
		/// <summary>
		/// Blank constructor.
		/// </summary>
		protected XacmlElement(  )
		{
		}
		#endregion

		#region Public properties

		/// <summary>
		/// The version of the schema that was used to validate.
		/// </summary>
		public XacmlVersion SchemaVersion
		{
			get{ return _schemaVersion; }
		}

		/// <summary>
		/// The schema that defines the element.
		/// </summary>
		public XacmlSchema Schema
		{
			get{ return _schema; }
		}

		/// <summary>
		/// Whether the instance is a read only version.
		/// </summary>
		public abstract bool IsReadOnly
		{
			get;
		}

		/// <summary>
		/// Return the string for the namespace using the schema and the version of this element.
		/// </summary>
		internal protected string XmlDocumentSchema
		{
			get
			{ 
				if( _schema == XacmlSchema.Context )
				{
					if( this.SchemaVersion == XacmlVersion.Version10 || this.SchemaVersion == XacmlVersion.Version11 )
					{
						return Namespaces.Context;
					}
					else if( this.SchemaVersion == XacmlVersion.Version20  )
					{
						return PolicySchema2.Namespaces.Context;
					}
				}
				else if( _schema == XacmlSchema.Policy )
				{
					if( this.SchemaVersion == XacmlVersion.Version10 || this.SchemaVersion == XacmlVersion.Version11 )
					{
						return Namespaces.Policy;
					}
					else if( this.SchemaVersion == XacmlVersion.Version20  )
					{
						return PolicySchema2.Namespaces.Policy;
					}				
				}
				throw new EvaluationException( "invalid schema and version information." ); //TODO: resources
			}
		}

		#endregion

		#region Protected methods

		/// <summary>
		/// Validates the schema using the version parameter.
		/// </summary>
		/// <param name="reader">The reader positioned in an element with namespace.</param>
		/// <param name="version">The version used to validate the document.</param>
		/// <returns><c>true</c>, if the schema corresponds to the namespace defined in the element.</returns>
		protected bool ValidateSchema( XmlReader reader, XacmlVersion version )
		{
            if (reader == null) throw new ArgumentNullException("reader");
			if( _schema == XacmlSchema.Policy )
			{
			    switch (version)
			    {
			        case XacmlVersion.Version11:
			        case XacmlVersion.Version10:
			            return (reader.NamespaceURI == Namespaces.Policy );
			        case XacmlVersion.Version20:
			            return (reader.NamespaceURI.Contains(PolicySchema2.Namespaces.Policy) );
			    }
			}
			else
			{
			    switch (version)
			    {
			        case XacmlVersion.Version11:
			        case XacmlVersion.Version10:
			            return (reader.NamespaceURI == Namespaces.Context );
			        case XacmlVersion.Version20:
			            return (reader.NamespaceURI == PolicySchema2.Namespaces.Context );
			    }
			}
		    throw new EvaluationException( "Invalid version provided" ); //TODO: resources
		}

		#endregion
	}
}
