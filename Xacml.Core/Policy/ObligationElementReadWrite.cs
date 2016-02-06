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

namespace Xacml.Core.Policy
{
	/// <summary>
	/// Represents a read/write Obligation node found in the Policy document.
	/// </summary>
	public class ObligationElementReadWrite : XacmlElement
	{
		#region Private members

		/// <summary>
		/// The id of the obligation.
		/// </summary>
		private string _obligationId;

		/// <summary>
		/// The Effect that will trigger the obligation to be passed to the response.
		/// </summary>
		private Effect _fulfillOn;

		/// <summary>
		/// The AttributeAssignments nodes defined in the Obligation.
		/// </summary>
		private AttributeAssignmentReadWriteCollection _attributeAssignment = new AttributeAssignmentReadWriteCollection();
		
		#endregion

		#region Constructors
		/// <summary>
		/// Creates an ReadWriteObligationElement with the parameters given
		/// </summary>
		/// <param name="obligationId"></param>
		/// <param name="fulfillOn"></param>
		/// <param name="attributeAssignment"></param>
		public ObligationElementReadWrite( string obligationId, Effect fulfillOn, AttributeAssignmentReadWriteCollection attributeAssignment )
		{
			_obligationId = obligationId;
			_fulfillOn = fulfillOn;
			_attributeAssignment = attributeAssignment;
		}

		/// <summary>
		/// Creates a new instance of the Obligation class using the XmlReader instance provided.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the Obligation node.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public ObligationElementReadWrite( XmlReader reader, XacmlVersion schemaVersion )
			: base( XacmlSchema.Policy, schemaVersion )
		{
			if(reader != null)
			{
				if( reader.LocalName == PolicySchema1.ObligationElement.Obligation && 
					ValidateSchema( reader, schemaVersion ) )
				{
					_obligationId = reader.GetAttribute( PolicySchema1.ObligationElement.ObligationId );

					// Parses the Effect attribute value
					_fulfillOn = (Effect)Enum.Parse( 
						typeof(Effect), reader.GetAttribute( PolicySchema1.ObligationElement.FulfillOn ), false );

					// Read all the attribute assignments
					while( reader.Read() )
					{
						switch( reader.LocalName )
						{
							case PolicySchema1.ObligationElement.AttributeAssignment:
								_attributeAssignment.Add( new AttributeAssignmentElementReadWrite( reader, schemaVersion ) );
								break;
						}
						if( reader.LocalName == PolicySchema1.ObligationElement.Obligation && 
							reader.NodeType == XmlNodeType.EndElement )
						{
							break;
						}
					}
				}
			}
			else
				_obligationId = "[TODO]: Add Obligation ID";
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Gets all the attribute assignments for this obligation.
		/// </summary>
		public virtual AttributeAssignmentReadWriteCollection AttributeAssignment
		{
			set{ _attributeAssignment = value; }
			get{ return _attributeAssignment; }
		}

		/// <summary>
		/// The effect that will trigger the obligation to be sent to the response.
		/// </summary>
		public virtual Effect FulfillOn
		{
			set{ _fulfillOn = value; }
			get{ return _fulfillOn; }
		}

		/// <summary>
		/// The obligation id.
		/// </summary>
		public virtual string ObligationId
		{
			set{ _obligationId = value; }
			get{ return _obligationId; }
		}
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
