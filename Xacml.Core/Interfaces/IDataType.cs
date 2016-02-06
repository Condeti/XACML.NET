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

namespace Xacml.Core.Interfaces
{
	/// <summary>
	/// Defines a data type that can be used by the evaluation engine. All the data types must define a set of 
	/// functions that are used by the evaluation engine to support function creation and some internal 
	/// requirements.
	/// </summary>
	public interface IDataType
	{
		/// <summary>
		/// Returns the function that allows comparing two values of this data type which returns a boolean value.
		/// </summary>
		/// <value>The instance of the function.</value>
		IFunction EqualFunction{ get; }

		/// <summary>
		/// Returns the function that allows determining if the first argument is contained in the
		/// bag of the second argument. This method is used to support the generic function BaseSubset, which allows 
		/// creating custom functions deriving from that class.
		/// </summary>
		/// <value>The instance of the function.</value>
		IFunction IsInFunction{ get; }

		/// <summary>
		/// Returns the function that allows determining if all the elements of first argument are contained in the
		/// bag of the second argument. This method is used to support the generic function BaseSubset, which allows 
		/// creating custom functions deriving from that class.
		/// </summary>
		/// <value>The instance of the function.</value>
		IFunction SubsetFunction{ get; }

		/// <summary>
		/// The string name of the data type.
		/// </summary>
		string DataTypeName{ get; }

		/// <summary>
		/// Return an instance of the type using the specified string value.
		/// </summary>
		/// <param name="value">The value to parse.</param>
		/// <param name="parNo">The parameter number used only for error reporing.</param>
		object Parse( string value, int parNo );
	}
}
