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
using System.Security.Permissions;
using System.Xml;
using pol = Xacml.Core.Policy;
using ctx = Xacml.Core.Context;
using rtm = Xacml.Core.Runtime;
using inf = Xacml.Core.Interfaces;

namespace Xacml.Core.Extensions
{
	/// <summary>
	/// Default data type repository which uses the configuration file to define the external 
	/// data types.
	/// </summary>
	public class DefaultPolicyCombiningAlgorithmRepository : inf.IPolicyCombiningAlgorithmRepository
	{
		#region Private members

		/// <summary>
		/// All the defined functions using the function id as the key.
		/// </summary>
		private Hashtable _algorithms = new Hashtable();

		#endregion

		#region "Constructors"

		/// <summary>
		/// Default constructor
		/// </summary>
		public DefaultPolicyCombiningAlgorithmRepository()
		{
		}

		#endregion

		#region IDataTypeRepository Members

		/// <summary>
		/// Initializes the repository provider using XmlNode that defines the provider in the configuration file.
		/// </summary>
		/// <param name="configNode">The XmlNode that defines the provider in the configuration file.</param>
#if NET10
		[ReflectionPermission( SecurityAction.Demand, Flags = ReflectionPermissionFlag.TypeInformation )]
#endif
#if NET20
		[ReflectionPermission( SecurityAction.Demand, Flags = ReflectionPermissionFlag.MemberAccess )]
#endif
		public void Init( XmlNode configNode )
		{
            if (configNode == null) throw new ArgumentNullException("configNode");
			XmlNodeList dataTypes = configNode.SelectNodes( "./policyCombiningAlgorithm" );
			foreach( XmlNode node in dataTypes )
			{
				inf.IPolicyCombiningAlgorithm pca = (inf.IPolicyCombiningAlgorithm)Activator.CreateInstance( Type.GetType( node.Attributes[ "type" ].Value ) );
				_algorithms.Add( node.Attributes[ "id" ].Value, pca );
			}
		}

		/// <summary>
		/// Returns an instance of the policyCombiningAlgorithm descriptor using the data type id specified.
		/// </summary>
		/// <param name="policyCombiningAlgorithmId">The policyCombiningAlgorithm id referenced in the policy document.</param>
		/// <returns>The policyCombiningAlgorithm instance or null if the policyCombiningAlgorithm was not found.</returns>
		public inf.IPolicyCombiningAlgorithm GetPolicyCombiningAlgorithm( string policyCombiningAlgorithmId )
		{
			return _algorithms[ policyCombiningAlgorithmId ] as inf.IPolicyCombiningAlgorithm;
		}

		#endregion
	}
}
