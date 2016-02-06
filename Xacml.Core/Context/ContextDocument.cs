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
using pol = Xacml.Core.Policy;

namespace Xacml.Core.Context
{
	/// <summary>
	/// Mantains a context document which can be a Request or a response document.
	/// </summary>
	public class ContextDocument : ContextDocumentReadWrite
	{
		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public ContextDocument( )
		{
		}

		/// <summary>
		/// Creates a new ContextDocument using the XmlReader instance provided.
		/// </summary>
		/// <param name="reader">The XmlReader instance positioned at the begining of the document.</param>
		/// <param name="schemaVersion">The schema used to validate this context document.</param>
		public ContextDocument( XmlReader reader, XacmlVersion schemaVersion ) : base( reader, schemaVersion )
		{
		}

		#endregion

		#region Public properties
		/// <summary>
		/// The Request in the context document.
		/// </summary>
		public override RequestElementReadWrite Request
		{
			set{ throw new NotSupportedException(); }
			get
			{
				SubjectCollection subjects = null;
				ActionElement action = null;
				EnvironmentElement environment = null;
				ResourceCollection resources = null;
				if( base.Request.Subjects != null )
				{
					subjects = new SubjectCollection( base.Request.Subjects );
				}
				if( base.Request.Action != null )
				{
					action = new ActionElement( new AttributeCollection( base.Request.Action.Attributes ), base.Request.Action.SchemaVersion );
				}
				if( base.Request.Environment != null )
				{
					environment = new EnvironmentElement( new AttributeCollection( base.Request.Environment.Attributes ), base.Request.Environment.SchemaVersion );
				}
				if( base.Request.Resources != null )
				{
					resources = new ResourceCollection( base.Request.Resources );
				}
				return new RequestElement( subjects, resources, action, environment, base.Request.SchemaVersion );
			}
		}

		#endregion
	}
}
