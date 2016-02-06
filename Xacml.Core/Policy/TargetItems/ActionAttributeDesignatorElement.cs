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

namespace Xacml.Core.Policy
{
	/// <summary>
	/// Represents an ActionAttributeDesignator in the Policy document.
	/// </summary>
	public class ActionAttributeDesignatorElement : AttributeDesignatorBase
	{
		#region Constructor

		/// <summary>
		/// Creates an instance of the ActionAttributeDesignator using the specified arguments.
		/// </summary>
		/// <param name="dataType">The id of data type of the referenced attribute in the context docuemnt.</param>
		/// <param name="mustBePresent">Whether the attribute must be present or not.</param>
		/// <param name="attributeId">The id of the attribute in the context document.</param>
		/// <param name="issuer">The issuer of the attribute.</param>
		/// <param name="version">The version of the schema that was used to validate.</param>
		public ActionAttributeDesignatorElement( string dataType, bool mustBePresent, string attributeId, string issuer, XacmlVersion version )
			: base( dataType, mustBePresent, attributeId, issuer, version )
		{
		}

		/// <summary>
		/// Creates an instance of the ActionAttributeDesignator which is a derived class from the 
		/// AttributeDesignator abstract class. This constructor is also calling the base class constructor 
		/// specifying the XmlReader instance.
		/// </summary>
		/// <param name="reader">An XmlReader positioned at the start of the ActionAttributeDesignator element.</param>
		/// <param name="version">The version of the schema that was used to validate.</param>
		public ActionAttributeDesignatorElement( XmlReader reader, XacmlVersion version ) : 
			base( reader, version )
		{
		}

		#endregion

		#region Public methods
		/// <summary>
		/// Writes the XML of the current element
		/// </summary>
		/// <param name="writer">The XmlWriter in which the element will be written</param>
		public override void WriteDocument(XmlWriter writer)
		{
            if (writer == null) throw new ArgumentNullException("writer");
			writer.WriteStartElement(PolicySchema1.ActionAttributeDesignatorElement.ActionAttributeDesignator);
			writer.WriteAttributeString(AttributeDesignatorElement.AttributeId,this.AttributeId);
			writer.WriteAttributeString(AttributeDesignatorElement.DataType,this.DataType);
			if( this.Issuer.Length != 0 )
			{
				writer.WriteAttributeString(AttributeDesignatorElement.Issuer,this.Issuer);
			}
			if(this.MustBePresent)
			{
				writer.WriteAttributeString(AttributeDesignatorElement.MustBePresent,"true");
			}
			else
			{
				writer.WriteAttributeString(AttributeDesignatorElement.MustBePresent,"false");
			}
			writer.WriteEndElement();
		}

		#endregion

		#region Public properties
		/// <summary>
		/// Whether the instance is a read only version.
		/// </summary>
		public override bool IsReadOnly
		{
			get{ return false; }
		}
		#endregion
	}
}
