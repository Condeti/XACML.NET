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

namespace Xacml.Core.Configuration
{
	/// <summary>
	/// Abstract base class used to load any extension which format is name="" type="".
	/// </summary>
	public abstract class NameTypeConfig
	{
		#region Private members

		/// <summary>
		/// The name of the extension.
		/// </summary>
		private string _name;

		/// <summary>
		/// The type name of the extension.
		/// </summary>
		private string _typeName;

		/// <summary>
		/// The instantiated .Net type of the extension.
		/// </summary>
		private Type _type;

		/// <summary>
		/// The XmlNode for the configuration of the extension.
		/// </summary>
		private XmlNode _node;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance in the NamedTypeConfig using the XmlNode provided.
		/// </summary>
		/// <param name="configNode">The XmlNode for the extension configuration.</param>
		protected NameTypeConfig( XmlNode configNode )
		{
            if (configNode == null) throw new ArgumentNullException(nameof(configNode));
			_node = configNode;
		    if (configNode.Attributes != null)
		    {
		        _name = configNode.Attributes[ "name" ].Value;
		        _typeName = configNode.Attributes[ "type" ].Value;
		    }
		    _type = Type.GetType( _typeName );
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The name of the extension
		/// </summary>
		public string Name => _name;

	    /// <summary>
		/// The type name of the extension.
		/// </summary>
		public string TypeName => _typeName;

	    /// <summary>
		/// The instantiated type for the extension.
		/// </summary>
		public Type Type => _type;

	    /// <summary>
		/// The XmlNode with the extension configuration.
		/// </summary>
		public XmlNode XmlNode => _node;

	    #endregion
	}
}
