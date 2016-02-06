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

namespace Xacml.Core.Context
{
	/// <summary>
	/// Represents an Resource node found in the context document. This class extends the abstract base class 
	/// TargetItem which loads the "target item" definition.
	/// </summary>
	public class ResourceElementReadWrite : TargetItemBase
	{
		#region Private members

		/// <summary>
		/// The contents of the ResourceContent node.
		/// </summary>
		private ResourceContentElementReadWrite _resourceContent;

		/// <summary>
		/// The scope of the resource if the request IsHierarchical
		/// </summary>
		private ResourceScope _resourceScope;

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a Resource using the specified arguments.
		/// </summary>
		/// <param name="resourceScope">The resource scope for this target item.</param>
		/// <param name="resourceContent">The resource content in the context document.</param>
		/// <param name="attributes">The attribute list.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public ResourceElementReadWrite( ResourceContentElementReadWrite resourceContent, ResourceScope resourceScope, AttributeReadWriteCollection attributes, XacmlVersion schemaVersion ) 
			: base( attributes, schemaVersion )
		{
			_resourceContent = resourceContent;
			_resourceScope = resourceScope;
		}

		/// <summary>
		/// Creates an instance of the Resource class using the XmlReader instance provided.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the Subject node.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public ResourceElementReadWrite( XmlReader reader, XacmlVersion schemaVersion ) 
			: base( reader, ContextSchema.RequestElement.Resource, schemaVersion )
		{
			// Search for a hierarchical node mark
			foreach( AttributeElementReadWrite attribute in Attributes )
			{
				if( attribute.AttributeId == ContextSchema.ResourceScope.ResourceScopeAttributeId ) 
				{
					foreach( AttributeValueElementReadWrite element in attribute.AttributeValues )
					{
						if( element.Contents != ContextSchema.ResourceScope.Immediate )
						{
							_resourceScope = (ResourceScope)Enum.Parse( typeof(ResourceScope), element.Contents, false );
							return;
						}
					}
				}
			}
		}

		#endregion

		#region Protected members

		/// <summary>
		/// Overrided to process custom attributes an attribute is found in the base class constructor.
		/// </summary>
		/// <param name="namespaceName">The namespace for the attribute.</param>
		/// <param name="attributeName">The attribute name found.</param>
		/// <param name="attributeValue">The attribute value found.</param>
		protected override void AttributeFound( string namespaceName, string attributeName, string attributeValue)
		{
		}

		/// <summary>
		/// Overrided to process custom nodes a node is found in the base class constructor.
		/// </summary>
		/// <remarks>This is used to avoid extra coding for all the target nodes</remarks>
		/// <param name="reader">The XmlReader pointing to the element found</param>
		protected override void NodeFound(XmlReader reader)
		{
			if( reader.LocalName == ContextSchema.ResourceElement.ResourceContent )
			{
				_resourceContent = new ResourceContentElementReadWrite( reader, SchemaVersion );
			}
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The contents of the ResourceContent node.
		/// </summary>
		public virtual ResourceContentElementReadWrite ResourceContent
		{
			get{ return _resourceContent; }
			set{ _resourceContent = value; }
		}

		/// <summary>
		/// Whether the request is a hierarchical request
		/// </summary>
		public bool IsHierarchical
		{
			get{ return (_resourceScope != ResourceScope.Immediate); }
		}

		/// <summary>
		/// The scope of the resource if the request IsHierarchical
		/// </summary>
		public virtual ResourceScope ResourceScopeValue
		{
			get{ return _resourceScope; }
			set{ _resourceScope = value; }
		}
		#endregion
	}
}
