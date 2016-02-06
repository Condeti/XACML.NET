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
	/// Represents a read-only Target node defined in the policy document.
	/// </summary>
	public class TargetElement : TargetElementReadWrite
	{

		#region Constructors

		/// <summary>
		/// Creates a new Target with the specified agumetns.
		/// </summary>
		/// <param name="resources">The resources for this target.</param>
		/// <param name="subjects">The subjects for this target.</param>
		/// <param name="actions">The actions for this target.</param>
		/// <param name="environments">The environments for this target.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public TargetElement( ResourcesElementReadWrite resources, SubjectsElementReadWrite subjects, ActionsElementReadWrite actions, EnvironmentsElementReadWrite environments, XacmlVersion schemaVersion )
			: base( resources, subjects, actions, environments, schemaVersion )
		{
		}

		/// <summary>
		/// Creates a new Target using the XmlReader instance provided.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the Target node.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		public TargetElement( XmlReader reader, XacmlVersion schemaVersion )
			: base( reader, schemaVersion )
		{
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The Resources defined in this target.
		/// </summary>
		public override ResourcesElementReadWrite Resources
		{
			get
			{
				if(base.Resources != null)
					return new ResourcesElement(base.Resources.IsAny, base.Resources.ItemsList,base.Resources.SchemaVersion) ; 
				else
					return null;
			}
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// The Subjects defined in this target.
		/// </summary>
		public override SubjectsElementReadWrite Subjects
		{
			get
			{
				if(base.Subjects != null)
					return new SubjectsElement( base.Subjects.IsAny, base.Subjects.ItemsList,base.Subjects.SchemaVersion); 
				else
					return null;
			}
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// The Actions defined in this target.
		/// </summary>
		public override ActionsElementReadWrite Actions
		{
			get
			{
				if(base.Actions != null)
					return new ActionsElement(base.Actions.IsAny, base.Actions.ItemsList,base.Actions.SchemaVersion) ;
				else
					return null;
			}
			set{ throw new NotSupportedException(); }
		}

		/// <summary>
		/// The Environments defined in this target.
		/// </summary>
		public override EnvironmentsElementReadWrite Environments
		{
			get
			{
				if(base.Environments != null)
					return new EnvironmentsElement(base.Environments.IsAny, base.Environments.ItemsList,base.Environments.SchemaVersion) ; 
				else
					return null;
			}
			set{ throw new NotSupportedException(); }
		}

		#endregion

	}
}
