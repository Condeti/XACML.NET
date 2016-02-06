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
using Xacml.PolicySchema2;
using cor = Xacml.Core;

namespace Xacml.Core.Policy
{
	/// <summary>
	/// Represents a read/write PolicyIdReference defined within a PolicySet.
	/// </summary>
	public class PolicyIdReferenceElementReadWrite : XacmlElement
	{
		#region Private members

		/// <summary>
		/// The referenced PolicyId.
		/// </summary>
		private string _policyIdReference;
		
		/// <summary>
		/// The referenced Policy version.
		/// </summary>
		private string _version;

		/// <summary>
		/// The referenced Policy earliest version.
		/// </summary>
		private string _earliestVersion;
		
		/// <summary>
		/// The referenced Policy latest version.
		/// </summary>
		private string _latestVersion;
		
		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new PolicyIdReference using the XmlReader instance provided.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the PolicyIdReference element.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public PolicyIdReferenceElementReadWrite( XmlReader reader, XacmlVersion schemaVersion )
			: base( XacmlSchema.Policy, schemaVersion )
		{
            if (reader == null) throw new ArgumentNullException("reader");
			if( reader.LocalName == PolicySchema1.PolicyIdReferenceElement.PolicyIdReference && 
				ValidateSchema( reader, schemaVersion ) )
			{
				if( reader.HasAttributes )
				{
					// Load all the attributes
					while( reader.MoveToNextAttribute() )
					{
						if( reader.LocalName == PolicyReferenceElement.Version )
						{
							_version = reader.GetAttribute( PolicyReferenceElement.Version );
						}
						else if( reader.LocalName == PolicyReferenceElement.EarliestVersion )
						{
							_earliestVersion = reader.GetAttribute( PolicyReferenceElement.EarliestVersion );
						}
						else if( reader.LocalName == PolicyReferenceElement.LatestVersion )
						{
							_latestVersion = reader.GetAttribute( PolicyReferenceElement.LatestVersion );
						}					
					}
					reader.MoveToElement();
				}
				_policyIdReference = reader.ReadElementString();
			}
			else
			{
				throw new Exception( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_node_name, reader.LocalName ] );
			}
		}

		/// <summary>
		/// Creates an instance of the element, with the provided values
		/// </summary>
		/// <param name="policyIdReference"></param>
		/// <param name="version"></param>
		/// <param name="earliestVersion"></param>
		/// <param name="latestVersion"></param>
		/// <param name="schemaVersion"></param>
		public PolicyIdReferenceElementReadWrite( string policyIdReference, string version, string earliestVersion, string latestVersion, 
			XacmlVersion schemaVersion ) : base( XacmlSchema.Policy, schemaVersion )
		{
			_policyIdReference = policyIdReference;
			_version = version;
			_earliestVersion = earliestVersion;
			_latestVersion = latestVersion;
		}
		#endregion
		
		#region Public properties

		/// <summary>
		/// The referenced PolicyId.
		/// </summary>
		public virtual string PolicyId
		{
			set{ _policyIdReference = value; }
			get{ return _policyIdReference; }
		}

		/// <summary>
		/// The referenced Policy version.
		/// </summary>
		public virtual string Version
		{
			set{ _version = value; }
			get{ return _version; }
		}


		/// <summary>
		/// The referenced Policy earliest version.
		/// </summary>
		public virtual string EarliestVersion
		{
			set{ _earliestVersion = value; }
			get{ return _earliestVersion; }
		}

		/// <summary>
		/// The referenced Policy latest version.
		/// </summary>
		public virtual string LatestVersion
		{
			set{ _latestVersion = value; }
			get{ return _latestVersion; }
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
		/// Writes the current element in the provided XmlWriter
		/// </summary>
		/// <param name="writer">The XmlWriter in which the element will be written</param>
		public void WriteDocument( XmlWriter writer )
		{
            if (writer == null) throw new ArgumentNullException("writer");
			writer.WriteElementString( PolicySchema1.PolicyIdReferenceElement.PolicyIdReference, this._policyIdReference );
		}

		#endregion
	}
}
