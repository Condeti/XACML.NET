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
using System.Security.Permissions;
using System.Xml;
using pol = Xacml.Core.Policy;
using ctx = Xacml.Core.Context;
using rtm = Xacml.Core.Runtime;
using inf = Xacml.Core.Interfaces;

namespace Xacml.Core.Extensions
{
	/// <summary>
	/// The default implementation of a repository attribute that uses the configuration file to define the 
	/// attribute.
	/// </summary>
	public class DefaultAttributeRepository : inf.IAttributeRepository
	{
		/// <summary>
		/// The attributes defined in the configuration file.
		/// </summary>
		private ctx.AttributeCollection _attributes = new ctx.AttributeCollection();
		
		/// <summary>
		/// Default public constructor.
		/// </summary>
		public DefaultAttributeRepository()
		{
		}

		/// <summary>
		/// Initialization method called by the EvaluationEngine with the XmlNode with the configuration element 
		/// that defines the repository.
		/// </summary>
		/// <param name="configNode">The XmlNode with the configuration.</param>
#if NET10
		[ReflectionPermission( SecurityAction.Demand, Flags = ReflectionPermissionFlag.TypeInformation )]
#endif
#if NET20
		[ReflectionPermission( SecurityAction.Demand, Flags = ReflectionPermissionFlag.MemberAccess )]
#endif
		public void Init( XmlNode configNode )
		{
            if (configNode == null) throw new ArgumentNullException("configNode");
			// Iterate through the child nodes and finds any node named "Attribute"
			foreach( XmlNode node in configNode.ChildNodes )
			{
				if( node.Name == "Attribute" )
				{
					// Search for the value of the attribute
					XmlNode value = node.SelectSingleNode( "./AttributeValue" );

					// Get the attribute Id
					string attributeId = node.Attributes[ "AttributeId" ].Value;

					// Get the issuer
					string issuer = value.Attributes[ "Issuer" ] != null ? value.Attributes[ "Issuer" ].Value : "";

					string dataType = value.Attributes[ "DataType" ] != null ? value.Attributes[ "DataType" ].Value : "";

					// Add a new instance of the Attribute class using the configuration information
					_attributes.Add( 
						new ctx.AttributeElement( 
							attributeId, 
							dataType, 
							issuer, 
							null, value.InnerText, XacmlVersion.Version11 ) ); //TODO: remove the version hardcoded
				}
			}
		}

		/// <summary>
		/// Method called by the EvaluationEngine when an attribute is not found.
		/// </summary>
		/// <param name="context">The evaluation context instance.</param>
		/// <param name="designator">The attribute designator.</param>
		/// <returns>An instance of an Attribute with it's value.</returns>
		public ctx.AttributeElement GetAttribute( rtm.EvaluationContext context, pol.AttributeDesignatorBase designator )
		{
            if (context == null) throw new ArgumentNullException("context");
            if (designator == null) throw new ArgumentNullException("designator");
			foreach( ctx.AttributeElement attrib in _attributes )
			{
				if( attrib.AttributeId == designator.AttributeId )
				{
					if( designator.Issuer != null && designator.Issuer.Length != 0 )
					{
						if( designator.Issuer == attrib.Issuer )
						{
							return attrib;
						}
					}
					else
					{
						return attrib;
					}
				}
			}
			return null;
		}
	}
}
