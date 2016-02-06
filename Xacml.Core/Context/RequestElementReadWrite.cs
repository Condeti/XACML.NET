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
using System.Collections;
using System.Xml;
using Xacml.PolicySchema1;
using cor = Xacml.Core;

namespace Xacml.Core.Context
{
	/// <summary>
	/// Represents a Request node in the context document.
	/// </summary>
	public class RequestElementReadWrite : XacmlElement
	{
		#region Private members

		/// <summary>
		/// The list of subjects that were placed in the context document.
		/// </summary>
		private SubjectReadWriteCollection _subjects = new SubjectReadWriteCollection();

		/// <summary>
		/// The resource node defined in the context document.
		/// </summary>
		private ResourceReadWriteCollection _resources = new ResourceReadWriteCollection();

		/// <summary>
		/// The action node defined in the context document.
		/// </summary>
		private ActionElementReadWrite _action;

		/// <summary>
		/// The environment node defined in the context document.
		/// </summary>
		private EnvironmentElementReadWrite _environment;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new Request using the parameters specified.
		/// </summary>
		/// <param name="subjects">The subjects list.</param>
		/// <param name="resources">The resource requested</param>
		/// <param name="action">The action requested.</param>
		/// <param name="environment">Any environment attribute part of the request.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public RequestElementReadWrite( SubjectReadWriteCollection subjects, ResourceReadWriteCollection resources, ActionElementReadWrite action, EnvironmentElementReadWrite environment, XacmlVersion schemaVersion )
			: base( XacmlSchema.Context, schemaVersion )
		{
			_subjects = subjects;
			_resources = resources;
			_action = action;
			_environment = environment;
		}

		/// <summary>
		/// Creates a new Request using the XmlReader instance provided.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the Request node.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public RequestElementReadWrite( XmlReader reader, XacmlVersion schemaVersion )
			: base( XacmlSchema.Context, schemaVersion )
		{
            if (reader == null) throw new ArgumentNullException("reader");
			if( reader.LocalName == ContextSchema.RequestElement.Request )
			{
				while( reader.Read() )
				{
					switch( reader.LocalName )
					{
						case ContextSchema.RequestElement.Subject:
							_subjects.Add( new SubjectElementReadWrite( reader, schemaVersion ) );
							break;
						case ContextSchema.RequestElement.Resource: 
							_resources.Add( new ResourceElementReadWrite( reader, schemaVersion ) );
							break;
						case ContextSchema.RequestElement.Action: 
							_action = new ActionElementReadWrite( reader, schemaVersion );
							break;
						case ContextSchema.RequestElement.Environment: 
							_environment = new EnvironmentElementReadWrite( reader, schemaVersion );
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
		/// The list of subjects that were placed in the context document.
		/// </summary>
		public virtual SubjectReadWriteCollection Subjects
		{
			set{ _subjects = value; }
			get{ return _subjects; }
		}
		
		/// <summary>
		/// The resources defined in the context document.
		/// </summary>
		public virtual ResourceReadWriteCollection Resources
		{
			set{ _resources = value; }
			get{ return _resources; }
		}
		
		/// <summary>
		/// The action node defined in the context document.
		/// </summary>
		public virtual ActionElementReadWrite Action
		{
			set{ _action = value; }
			get{ return _action; }
		}

		/// <summary>
		/// The environment node defined in the context document.
		/// </summary>
		public virtual EnvironmentElementReadWrite Environment
		{
			set{ _environment = value; }
			get{ return _environment; }
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
		/// Writes the element in the provided writer
		/// </summary>
		/// <param name="writer">The XmlWriter in which the element will be written</param>
		/// <param name="namespaces">The xml's namespaces</param>
		public void WriteDocument( XmlWriter writer, Hashtable namespaces )
		{
            if (writer == null) throw new ArgumentNullException("writer");
            if (namespaces == null) throw new ArgumentNullException("namespaces");
			writer.WriteStartDocument();
			writer.WriteStartElement( ContextSchema.RequestElement.Request, string.Empty );

			foreach(DictionaryEntry name in namespaces)
			{
                writer.WriteAttributeString(Namespaces.XMLNS, name.Key.ToString(), null, name.Value.ToString());
			}
			if( this._subjects != null )
			{
				this._subjects.WriteDocument( writer );
			}
			if( this._resources != null )
			{
				this._resources.WriteDocument( writer );
			}
			if( this._action != null )
			{
				this._action.WriteDocument( writer );
			}
			if( this._environment != null )
			{
				this._environment.WriteDocument( writer );
			}
			writer.WriteEndElement();
			writer.WriteEndDocument();
		}

		#endregion
	}
}
