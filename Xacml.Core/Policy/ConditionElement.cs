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

namespace Xacml.Core.Policy
{
	/// <summary>
	/// Represents a read-only Condition element in the Policy document. This class extends the abstract class ApplyBase.
	/// </summary>
	public class ConditionElement : ConditionElementReadWrite
	{
		#region Constructors

		/// <summary>
		/// Createa an instance of the Condition class using the specified XmlReader. The base class constructor is
		/// also called with the XmlReader and the node name of Condition node.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the Condition node.</param>
		/// <param name="version">The version of the schema that was used to validate.</param>
		public ConditionElement( XmlReader reader, XacmlVersion version ) 
			: base( reader, version )
		{
		}

		/// <summary>
		/// Creates a ConditionElement with the given parameters
		/// </summary>
		/// <param name="functionId"></param>
		/// <param name="arguments"></param>
		/// <param name="schemaVersion"></param>
		public ConditionElement( string functionId, IExpressionReadWriteCollection arguments, XacmlVersion schemaVersion)
			: base( functionId, arguments, schemaVersion )
		{
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The id of the function used in this element.
		/// </summary>
		public override string FunctionId
		{
			set{ throw new NotSupportedException(); }
			get{ return base.FunctionId; }
		}

		/// <summary>
		/// The arguments of the condition (or apply)
		/// </summary>
		public override IExpressionReadWriteCollection Arguments
		{
			set{ throw new NotSupportedException(); }
			get{ return new IExpressionCollection( base.Arguments ); }
		}

		#endregion
	}
}
