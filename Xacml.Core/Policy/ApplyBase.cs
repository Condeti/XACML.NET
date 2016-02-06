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
using cor = Xacml.Core;

namespace Xacml.Core.Policy 
{
	/// <summary>
	/// The ApplyBase class is used to define the common data used in the Apply and Condition read-only nodes.
	/// </summary>
	public abstract class ApplyBase : ApplyBaseReadWrite
	{
		#region Constructor

		/// <summary>
		/// Creates an instance of the ApplyBase using the XmlReader positioned in the node and the node name
		/// specifyed by the derived class in the constructor.
		/// </summary>
		/// <param name="reader">The XmlReader positioned at the "nodeName" node.</param>
		/// <param name="nodeName">The name of the node specifies by the derived class.</param>
		/// <param name="schemaVersion">The version of the schema that was used to validate.</param>
		protected ApplyBase( XmlReader reader, string nodeName, XacmlVersion schemaVersion )
			: base( reader, nodeName, schemaVersion )
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
		}

		/// <summary>
		/// The arguments of the condition (or apply)
		/// </summary>
		public override IExpressionReadWriteCollection Arguments
		{
			get{ return new IExpressionCollection( base.Arguments ); }
			set{ throw new NotSupportedException(); }
		}

		#endregion

	}
}
