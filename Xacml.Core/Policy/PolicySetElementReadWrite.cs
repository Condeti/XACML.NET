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
using ctx = Xacml.Core.Context;
using cor = Xacml.Core;
using inf = Xacml.Core.Interfaces;

namespace Xacml.Core.Policy
{
	/// <summary>
	/// Represents a read/write PolicySet defined within a policy document.
	/// </summary>
	public class PolicySetElementReadWrite : XacmlElement, inf.IHasTarget
	{
		#region Private members

		/// <summary>
		/// The policy combining algorithm name.
		/// </summary>
		private string _policyCombiningAlgorithm;

		/// <summary>
		/// The list of obigations defined.
		/// </summary>
		private ObligationReadWriteCollection _obligations = new ObligationReadWriteCollection();

		/// <summary>
		/// The target defining whether this policy set applies to a specific context request.
		/// </summary>
		private TargetElementReadWrite _target;

		/// <summary>
		/// The id.
		/// </summary>
		private string _id = String.Empty;

		/// <summary>
		/// The description.
		/// </summary>
		private string _description = String.Empty;
		
		/// <summary>
		/// All the policies defined in this policy set.
		/// </summary>
		private ArrayList _policies = new ArrayList();

		/// <summary>
		/// All the combiner parmeters in the policy set.
		/// </summary>
		private ArrayList _combinerParameters = new ArrayList();

		/// <summary>
		/// All the combiner parmeters in the policy set.
		/// </summary>
		private ArrayList _policyCombinerParameters = new ArrayList();

		/// <summary>
		/// All the combiner parmeters in the policy set.
		/// </summary>
		private ArrayList _policySetCombinerParameters = new ArrayList();

		/// <summary>
		/// The XPath version supported.
		/// </summary>
		private string _xpathVersion = String.Empty;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new policySet using the arguments provided.
		/// </summary>
		/// <param name="id">The policy set id.</param>
		/// <param name="description">The description of the policy set.</param>
		/// <param name="target">The target for this policy set.</param>
		/// <param name="policies">All the policies inside this policy set.</param>
		/// <param name="policyCombiningAlgorithm">The policy combining algorithm for this policy set.</param>
		/// <param name="obligations">The obligations.</param>
		/// <param name="xpathVersion">The XPath version supported.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public PolicySetElementReadWrite( string id, string description, TargetElementReadWrite target, ArrayList policies, string policyCombiningAlgorithm, ObligationReadWriteCollection obligations, string xpathVersion, XacmlVersion schemaVersion )
			: base( XacmlSchema.Policy, schemaVersion )
		{
			_id = id;
			_description = description;
			_target = target;
			_policies = policies;
			_policyCombiningAlgorithm = policyCombiningAlgorithm;
			_obligations = obligations;
			_xpathVersion = xpathVersion;
		}

		/// <summary>
		/// Creates a new PolicySet using the XmlReader instance provided.
		/// </summary>
		/// <param name="reader">The XmlReder positioned at the PolicySet element.</param>
		/// <param name="schemaVersion">The version of the schema that will be used to validate.</param>
		public PolicySetElementReadWrite( XmlReader reader, XacmlVersion schemaVersion )
			: base( XacmlSchema.Policy, schemaVersion )
		{
			// Validates the current node name
			if( reader.LocalName == PolicySchema1.PolicySetElement.PolicySet && 
				ValidateSchema( reader, schemaVersion ) )
			{
				// Get the attributes
				_id = reader.GetAttribute( PolicySchema1.PolicySetElement.PolicySetId ); 
				_policyCombiningAlgorithm = reader.GetAttribute( PolicySchema1.PolicySetElement.PolicyCombiningAlgorithmId );

				// Read the inner nodes
				while( reader.Read() )
				{
					switch( reader.LocalName )
					{
						case PolicySchema1.PolicySetElement.Description:
							_description = reader.ReadElementString();
							break;
						case PolicySchema1.PolicySetElement.PolicySetDefaults:
							if( reader.Read() && reader.Read() )
							{
								if( reader.LocalName == PolicySetDefaultsElement.XPathVersion && 
									ValidateSchema( reader, schemaVersion ) )
								{
									_xpathVersion = reader.ReadElementString();
									if( _xpathVersion != null && _xpathVersion.Length != 0 && _xpathVersion != Namespaces.XPath10 )
									{
										throw new Exception( Resource.ResourceManager[ Resource.MessageKey.exc_unsupported_xpath_version, _xpathVersion ] );
									}
								}
								reader.Read();
							}
							break;
						case PolicySchema1.TargetElement.Target:
							_target = new TargetElementReadWrite( reader, schemaVersion );
							break;
						case PolicySchema1.PolicySetElement.PolicySet:
							if( !reader.IsEmptyElement && reader.NodeType != XmlNodeType.EndElement )
							{
								_policies.Add( new PolicySetElementReadWrite( reader, schemaVersion ) );
							}
							break;
						case PolicySchema1.PolicyElement.Policy:
							_policies.Add( new PolicyElementReadWrite( reader, schemaVersion ) );
							break;
						case PolicySchema1.PolicySetIdReferenceElement.PolicySetIdReference:
							_policies.Add( new PolicySetIdReferenceElementReadWrite( reader, schemaVersion ) );
							break;
						case PolicySchema1.PolicyIdReferenceElement.PolicyIdReference:
							_policies.Add( new PolicyIdReferenceElement( reader, schemaVersion ) );
							break;
						case PolicySchema1.PolicySetElement.Obligations:
							while( reader.Read() )
							{
								switch( reader.LocalName )
								{
									case PolicySchema1.ObligationElement.Obligation:
										_obligations.Add( new ObligationElementReadWrite( reader, schemaVersion ) );
										break;
								}
								if( reader.LocalName == ObligationsElement.Obligations && 
									reader.NodeType == XmlNodeType.EndElement )
								{
									reader.Read();
									break;
								}		
							}
							break;
						case PolicySchema2.PolicySetElement.CombinerParameters:
							// Read all the combiner parameters
							while( reader.Read() )
							{
								switch( reader.LocalName )
								{
									case PolicySchema2.CombinerParameterElement.CombinerParameter:
										_combinerParameters.Add( new CombinerParameterElement( reader, schemaVersion ) );
										break;
								}
								if( reader.LocalName == PolicySchema2.PolicySetElement.CombinerParameters && 
									reader.NodeType == XmlNodeType.EndElement )
								{
									reader.Read();
									break;
								}		
							}
							break;
						case PolicySchema2.PolicySetElement.PolicyCombinerParameters:
							// Read all the policy combiner parameters
							while( reader.Read() )
							{
								switch( reader.LocalName )
								{
									case PolicySchema2.PolicyCombinerParameterElement.PolicyCombinerParameter:
										_policyCombinerParameters.Add( new PolicyCombinerParameterElement( reader, schemaVersion ) );
										break;
								}
								if( reader.LocalName == PolicySchema2.PolicySetElement.PolicyCombinerParameters && 
									reader.NodeType == XmlNodeType.EndElement )
								{
									reader.Read();
									break;
								}		
							}
							break;
						case PolicySchema2.PolicySetElement.PolicySetCombinerParameters:
							// Read all the policy set combiner parameters
							while( reader.Read() )
							{
								switch( reader.LocalName )
								{
									case PolicySchema2.PolicySetCombinerParameterElement.PolicySetCombinerParameter:
										_policySetCombinerParameters.Add( new PolicySetCombinerParameterElement( reader, schemaVersion ) );
										break;
								}
								if( reader.LocalName == PolicySchema2.PolicySetElement.PolicySetCombinerParameters && 
									reader.NodeType == XmlNodeType.EndElement )
								{
									reader.Read();
									break;
								}		
							}
							break;
					}
					if( reader.LocalName == PolicySchema1.PolicySetElement.PolicySet && 
						reader.NodeType == XmlNodeType.EndElement )
					{
						reader.Read();
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
		/// The PolicySet Id.
		/// </summary>
		public virtual string Id
		{
			set{ _id = value; }
			get{ return _id; }
		}

		/// <summary>
		/// The policy set description.
		/// </summary>
		public virtual string Description
		{
			get{ return _description; }
			set{ _description = value; }
		}

		/// <summary>
		/// The policy combining algorithm Id.
		/// </summary>
		public virtual string PolicyCombiningAlgorithm
		{
			get{ return _policyCombiningAlgorithm; }
			set{ _policyCombiningAlgorithm = value; }
		}

		/// <summary>
		/// The list of obligations.
		/// </summary>
		public virtual ObligationReadWriteCollection Obligations
		{
			get{ return _obligations; }
			set{ _obligations = value; }
		}

		/// <summary>
		/// The Target for this PolicySet
		/// </summary>
		public virtual TargetElementReadWrite Target
		{
			get{ return _target; }
			set{ _target = value; }
		}

		/// <summary>
		/// All the policies defined in this PolicySet
		/// </summary>
		public virtual ArrayList Policies
		{
			get{ return _policies; }
			set{ _policies = value; }
		}

		/// <summary>
		/// The XPath version supported.
		/// </summary>
		public virtual string XPathVersion
		{
			get{ return _xpathVersion; }
			set{ _xpathVersion = value; }
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
		/// Writes the XML of the current element
		/// </summary>
		/// <param name="writer">The XmlWriter in which the element will be written</param>
		/// <param name="namespaces">The xml's namespaces</param>
		public void WriteDocument( XmlWriter writer, Hashtable namespaces )
		{
			writer.WriteStartElement( PolicySchema1.PolicySetElement.PolicySet );
			foreach( DictionaryEntry name in namespaces )
			{
                		writer.WriteAttributeString(Namespaces.XMLNS, name.Key.ToString(), null, name.Value.ToString());
			}
			writer.WriteAttributeString( PolicySchema1.PolicySetElement.PolicySetId, this._id );
			writer.WriteAttributeString( PolicySchema1.PolicySetElement.PolicyCombiningAlgorithmId, this._policyCombiningAlgorithm );
            if (this._description != null && this._description.Length != 0)
			{
				writer.WriteElementString( PolicySchema1.PolicySetElement.Description, this._description );
			}
            if (this._xpathVersion != null && this._xpathVersion.Length != 0)
			{
				writer.WriteStartElement( PolicySchema1.PolicySetElement.PolicySetDefaults );
				writer.WriteElementString( PolicySetDefaultsElement.XPathVersion, this._xpathVersion );
				writer.WriteEndElement();
			}
			
			if( this._target != null )
			{
				this._target.WriteDocument( writer );
			}
			foreach( object policy in this._policies )
			{
				if( policy is PolicyElementReadWrite )
				{
					((PolicyElementReadWrite)policy).WriteDocument( writer );
				}
				else if( policy is PolicySetElementReadWrite )
				{
					((PolicySetElementReadWrite)policy).WriteDocument( writer );
				}
				else if( policy is PolicyIdReferenceElementReadWrite )
				{
					((PolicyIdReferenceElementReadWrite)policy).WriteDocument( writer );
				}
				else if( policy is PolicySetIdReferenceElementReadWrite )
                {
					((PolicySetIdReferenceElementReadWrite)policy).WriteDocument( writer );
				}
			}
			if( this._obligations != null )
			{
				this._obligations.WriteDocument( writer );
			}
			writer.WriteEndElement();
		}

		/// <summary>
		/// Writes the XML of the current element
		/// </summary>
		/// <param name="writer">The XmlWriter in which the element will be written</param>
		public void WriteDocument( XmlWriter writer )
		{
			writer.WriteStartElement( PolicySchema1.PolicySetElement.PolicySet );
			writer.WriteAttributeString( PolicySchema1.PolicySetElement.PolicySetId, this._id );
			writer.WriteAttributeString( PolicySchema1.PolicySetElement.PolicyCombiningAlgorithmId, this._policyCombiningAlgorithm );
            if (this._description != null && this._description.Length != 0)
			{
				writer.WriteElementString( PolicySchema1.PolicySetElement.Description, this._description );
			}
            if (this._xpathVersion != null && this._xpathVersion.Length != 0)
			{
				writer.WriteStartElement( PolicySchema1.PolicySetElement.PolicySetDefaults );
				writer.WriteElementString( PolicySetDefaultsElement.XPathVersion, this._xpathVersion );
				writer.WriteEndElement();
			}
			
			this._target.WriteDocument( writer );
			foreach( object policy in this._policies )
			{
				if( policy is PolicyElementReadWrite )
				{
					((PolicyElementReadWrite)policy).WriteDocument( writer );
				}
				else if( policy is PolicySetElementReadWrite )
				{
					((PolicySetElementReadWrite)policy).WriteDocument( writer );
				}
				else if( policy is PolicyIdReferenceElementReadWrite )
				{
					((PolicyIdReferenceElementReadWrite)policy).WriteDocument( writer );
				}
				else if( policy is PolicySetIdReferenceElementReadWrite )
				{
					((PolicySetIdReferenceElementReadWrite)policy).WriteDocument( writer );
				}
			}
			this._obligations.WriteDocument(writer );
			writer.WriteEndElement();
		}
		#endregion
	}
}
