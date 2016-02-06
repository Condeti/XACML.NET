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

namespace Xacml.Core.Policy
{
	/// <summary>
	/// Represents a read-only PolicyDocument which may contain a Policy or a PolicySet
	/// </summary>
	public class PolicyDocument : PolicyDocumentReadWrite
	{
		#region Constructors
		/// <summary>
		/// Constructor of the class
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="schemaVersion"></param>
		public PolicyDocument( XmlReader reader, XacmlVersion schemaVersion ) : base(reader,schemaVersion)
		{
		}
		/// <summary>
		/// Creates a new black PolicyDocument
		/// </summary>
		/// <param name="schemaVersion"></param>
		public PolicyDocument(XacmlVersion schemaVersion) : base(schemaVersion)
		{
		}

		#endregion
		#region Public properties

		/// <summary>
		/// The version of the schema used to validate this instance.
		/// </summary>
		public override XacmlVersion Version
		{
			set{ throw new NotSupportedException(); }
		}
		
		/// <summary>
		/// Whether the document have passed the Xsd validation.
		/// </summary>
		public override bool IsValidDocument
		{
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// The PolicySet contained in the document.
		/// </summary>
		public override PolicySetElementReadWrite PolicySet
		{
			set{ throw new NotSupportedException(); }
			get
			{
				if( base.PolicySet != null )
					return new PolicySetElement(base.PolicySet.Id, base.PolicySet.Description,base.PolicySet.Target,base.PolicySet.Policies,
						base.PolicySet.PolicyCombiningAlgorithm, base.PolicySet.Obligations,base.PolicySet.XPathVersion,base.PolicySet.SchemaVersion); 
				else
					return null;
			}
		}

		/// <summary>
		/// The Policy contained in the document.
		/// </summary>
		public override PolicyElementReadWrite Policy
		{
			set{ throw new NotSupportedException(); }
			get
			{ 
				if( base.Policy != null )
                    return new PolicyElement( base.Policy.Id,base.Policy.Description,base.Policy.Target,base.Policy.Rules,
						base.Policy.RuleCombiningAlgorithm,base.Policy.Obligations,base.Policy.XPathVersion,base.Policy.CombinerParameters,
						base.Policy.RuleCombinerParameters,base.Policy.VariableDefinitions,base.Policy.SchemaVersion); 
				else
					return null;
			}
		}

		/// <summary>
		/// All the namespaced defined in the document.
		/// </summary>
		public override IDictionary Namespaces
		{
			set{ throw new NotSupportedException(); }
		}

		#endregion
	}
}
