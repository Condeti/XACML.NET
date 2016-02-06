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
using ctx = Xacml.Core.Context;
using pol = Xacml.Core.Policy;
using cor = Xacml.Core;

namespace Xacml.Core.Policy
{
	/// <summary>
	/// Represents a read/write Rule defined in the policy document.
	/// </summary>
	public class RuleElementReadWrite : XacmlElement
	{
		#region Private members

		/// <summary>
		/// The Rule Id.
		/// </summary>
		private string _id = String.Empty;

		/// <summary>
		/// The rule description.
		/// </summary>
		private string _description = String.Empty;

		/// <summary>
		/// The condition that defines the rule.
		/// </summary>
		private ConditionElementReadWrite _condition;

		/// <summary>
		/// The effect of the Rule
		/// </summary>
		private Effect _effect;

		/// <summary>
		/// The target that must be satisfied in order to apply the rule.
		/// </summary>
		private TargetElementReadWrite _target;

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a new Rule using the provided argument values.
		/// </summary>
		/// <param name="id">The rule id.</param>
		/// <param name="description">The rule description.</param>
		/// <param name="target">The target instance for this rule.</param>
		/// <param name="condition">The condition for this rule.</param>
		/// <param name="effect">The effect of this rule.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public RuleElementReadWrite( string id, string description, TargetElementReadWrite target, ConditionElementReadWrite condition, Effect effect, XacmlVersion schemaVersion )
			: base( XacmlSchema.Policy, schemaVersion )
		{
			_id = id;
			_description = description;
			_target = target;
			_condition = condition;
			_effect = effect;
		}

		/// <summary>
		/// Creates a new Rule using the XmlReader instance provided.
		/// </summary>
		/// <param name="reader">THe XmlReader instance positioned at the Rule node.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public RuleElementReadWrite( XmlReader reader, XacmlVersion schemaVersion )
			: base( XacmlSchema.Policy, schemaVersion )
		{
            if (reader == null) throw new ArgumentNullException("reader");
			if( reader.LocalName == PolicySchema1.RuleElement.Rule && 
				ValidateSchema( reader, schemaVersion ) )
			{
				// Read the attributes
				_id = reader.GetAttribute( PolicySchema1.RuleElement.RuleId );

				// The parsing should not fail because the document have been validated by an
				// Xsd schema.
				string temp = reader.GetAttribute( PolicySchema1.RuleElement.Effect );
				_effect = (Effect)Enum.Parse( 
					typeof(Effect), 
					temp,
					false );

				// Read the rule contents.
				while( reader.Read() )
				{
					switch( reader.LocalName )
					{
						case PolicySchema1.RuleElement.Description:
							_description = reader.ReadElementString();
							break;
						case PolicySchema1.RuleElement.Target:
							_target = new TargetElementReadWrite( reader, schemaVersion );
							break;
						case PolicySchema1.RuleElement.Condition:
							_condition = new ConditionElementReadWrite( reader, schemaVersion );
							break;
					}
					if( reader.LocalName == PolicySchema1.RuleElement.Rule && 
						reader.NodeType == XmlNodeType.EndElement )
					{
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
		/// The Rule Id.
		/// </summary>
		public virtual string Id
		{
			get{ return _id; }
			set{ _id = value; }
		}

		/// <summary>
		/// The description of this rule.
		/// </summary>
		public virtual string Description
		{
			get{ return _description; }
			set{ _description = value; }
		}

		/// <summary>
		/// The condition of this rule.
		/// </summary>
		public virtual ConditionElementReadWrite Condition
		{
			get{ return _condition; }
			set{ _condition = value; }
		}

		/// <summary>
		/// Whether the rule defines a condition.
		/// </summary>
		public bool HasCondition
		{
			get{ return _condition != null; }
		}

		/// <summary>
		/// Whether the rule defines a target.
		/// </summary>
		public bool HasTarget
		{
			get{ return _target != null; }
		}
		
		/// <summary>
		/// The target of the rule.
		/// </summary>
		public virtual TargetElementReadWrite Target
		{
			get{ return _target; }
			set{ _target = value; }
		}

		/// <summary>
		/// The effect of the rule.
		/// </summary>
		public virtual Effect Effect
		{
			get{ return _effect; }
			set{ _effect = value; }
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
