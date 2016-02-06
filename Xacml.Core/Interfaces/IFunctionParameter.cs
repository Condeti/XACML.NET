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

using System.Collections;
using rtm = Xacml.Core.Runtime;

namespace Xacml.Core.Interfaces
{
	/// <summary>
	/// Defines a value that can be used as a function parameter. Containing methods that allows getting a typed 
	/// value in order to evaluate the function with typed values.
	/// </summary>
	public interface IFunctionParameter
	{
		/// <summary>
		/// Gets the data type of the value.
		/// </summary>
		/// <param name="context">The evaluation context.</param>
		/// <returns>The data type descriptor.</returns>
		IDataType GetType( rtm.EvaluationContext context );

		/// <summary>
		/// Whether the function parameter is a bag of values.
		/// </summary>
		bool IsBag{ get; }

		/// <summary>
		/// The size of the bag.
		/// </summary>
		int BagSize{ get; }

		/// <summary>
		/// All the elements of the bag.
		/// </summary>
		ArrayList Elements{ get; }

		/// <summary>
		/// Returns a function value from the function parameter.
		/// </summary>
		/// <param name="parNo"></param>
		/// <returns></returns>
		IFunction GetFunction( int parNo );

		/// <summary>
		/// Returns an object value from the function parameter. This method is used in fuctions that uses data 
		/// types not defined in the specification.
		/// </summary>
		/// <param name="dataType">The expected data type of the value.</param>
		/// <param name="parNo">The parameter number that represents this value.</param>
		object GetTypedValue( IDataType dataType, int parNo );
	}
}
