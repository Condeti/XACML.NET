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
using ctx = Xacml.Core.Context;
using pol = Xacml.Core.Policy;
using inf = Xacml.Core.Interfaces;
using rtm = Xacml.Core.Runtime;

namespace Xacml.Core.Runtime.Functions
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class BaseSetEquals : FunctionBase, inf.ITypeSpecificFunction
	{
		#region IFunction Members

		/// <summary>
		/// Evaluates the function.
		/// </summary>
		/// <param name="context">The evaluation context instance.</param>
		/// <param name="args">The function arguments.</param>
		/// <returns>The result value of the function evaluation.</returns>
		public override EvaluationValue Evaluate( EvaluationContext context, params inf.IFunctionParameter[] args )
		{
            if (context == null) throw new ArgumentNullException("context");
            if (args == null) throw new ArgumentNullException("args");
			inf.IFunction function = DataType.SubsetFunction;
			if( ( function.Evaluate( 
				context,
				new EvaluationValue( args[0], args[0].GetType( context ) ), new EvaluationValue( args[1], args[1].GetType( context ) ) ) == EvaluationValue.True ) && 
				( function.Evaluate( 
				context,
				new EvaluationValue( args[1], args[1].GetType( context ) ), new EvaluationValue( args[0], args[0].GetType( context ) ) ) == EvaluationValue.True ) )
			{
				return EvaluationValue.True;
			}
			else
			{
				return EvaluationValue.False;
			}
		}

		/// <summary>
		/// The data type of the return value.
		/// </summary>
		public override inf.IDataType Returns
		{
			get{ return DataTypeDescriptor.Boolean; }
		}

		/// <summary>
		/// Defines the data types for the function arguments.
		/// </summary>
		public override inf.IDataType[] Arguments
		{
			get
			{
				return new inf.IDataType[]{ DataTypeDescriptor.Bag, DataTypeDescriptor.Bag };
			}
		}

		/// <summary>
		/// Defines the data type for which the function was defined for.
		/// </summary>
		public abstract inf.IDataType DataType { get; }

		#endregion
	}
}
