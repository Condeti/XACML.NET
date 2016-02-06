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
using pol = Xacml.Core.Policy;
using ctx = Xacml.Core.Context;
using typ = Xacml.Core.Runtime.DataTypes;
using rtm = Xacml.Core.Runtime;
using inf = Xacml.Core.Interfaces;
using cor = Xacml.Core;

namespace Xacml.Core.Runtime.Functions
{
	/// <summary>
	/// Generic function base used to implement common IFunction members.
	/// </summary>
	public abstract class FunctionBase : inf.IFunction
	{
		#region IFunctionParameter Members

		/// <summary>
		/// Gets the data type of the value.
		/// </summary>
		/// <param name="context">The evaluation context.</param>
		/// <returns>The data type descriptor.</returns>
		public inf.IDataType GetType( EvaluationContext context )
		{ 
			return DataTypeDescriptor.Function;
		}

		/// <summary>
		/// Gets the value as a generic object.
		/// </summary>
		/// <param name="dataType">The expected data type of the value.</param>
		/// <param name="parNo">The number of parameter used only for error notification.</param>
		/// <returns></returns>
		public virtual object GetTypedValue( inf.IDataType dataType, int parNo)
		{
			return this;
		}

		/// <summary>
		/// Whether the value is a bag.
		/// </summary>
		public bool IsBag
		{	
			get{ throw new EvaluationException( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_function_usage ] ); }
		}

		/// <summary>
		/// If the value is a bag the size will be returned otherwise an exception is thrown.
		/// </summary>
		public int BagSize
		{
			get{ throw new EvaluationException( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_function_usage ] ); }
		}

		/// <summary>
		/// The elements of the bag value.
		/// </summary>
		public ArrayList Elements
		{ 
			get{ throw new EvaluationException( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_function_usage ] ); }
		}

		/// <summary>
		/// The instantiated function.
		/// </summary>
		/// <param name="parNo">THe number of parameter used only for error notification.</param>
		/// <returns></returns>
		public inf.IFunction GetFunction( int parNo )
		{
			return this;
		}

		#endregion

		#region Abstract methods

		/// <summary>
		/// The function Id defined in the specification.
		/// </summary>
		public abstract string Id{ get; } 

		/// <summary>
		/// Evaluates the function.
		/// </summary>
		/// <param name="context">The Evaluation context information.</param>
		/// <param name="args">The function arguments.</param>
		/// <returns>The result value of the function evaluation.</returns>
		public abstract EvaluationValue Evaluate( EvaluationContext context, params inf.IFunctionParameter[] args );

		/// <summary>
		/// The data type of the return value.
		/// </summary>
		public abstract inf.IDataType Returns{ get; }

		/// <summary>
		/// Defines the data types for the function arguments.
		/// </summary>
		public abstract inf.IDataType[] Arguments{ get; }

		/// <summary>
		/// Whether the function defines variable arguments. The data type of the variable arguments will be the 
		/// data type of the last parameter.
		/// </summary>
		public virtual bool VarArgs{ get{ return false; } }

		#endregion

		#region Protected methods

		/// <summary>
		/// Returns the value of the argument in the index specified of the type int.
		/// </summary>
		/// <param name="args">The arguments list</param>
		/// <param name="index">The index</param>
		/// <returns>The integer value.</returns>
		protected static int GetIntegerArgument( inf.IFunctionParameter[] args, int index )
		{
            if (args == null) throw new ArgumentNullException("args");
			return (int)args[ index ].GetTypedValue( DataTypeDescriptor.Integer, index );
		}

		/// <summary>
		/// Returns the value of the argument in the index specified of the type bool.
		/// </summary>
		/// <param name="args">The arguments list</param>
		/// <param name="index">The index</param>
		/// <returns>The bool value.</returns>
		protected static bool GetBooleanArgument( inf.IFunctionParameter[] args, int index )
		{
            if (args == null) throw new ArgumentNullException("args");
			return (bool)args[ index ].GetTypedValue( DataTypeDescriptor.Boolean, index );
		}

		/// <summary>
		/// Returns the value of the argument in the index specified of the type AnyUri.
		/// </summary>
		/// <param name="args">The arguments list</param>
		/// <param name="index">The index</param>
		/// <returns>The Uri value.</returns>
		protected static Uri GetAnyUriArgument( inf.IFunctionParameter[] args, int index )
		{
            if (args == null) throw new ArgumentNullException("args");
			return (Uri)args[ index ].GetTypedValue( DataTypeDescriptor.AnyUri, index );
		}

		/// <summary>
		/// Returns the value of the argument in the index specified of the type String.
		/// </summary>
		/// <param name="args">The arguments list</param>
		/// <param name="index">The index</param>
		/// <returns>The String value.</returns>
		protected static string GetStringArgument( inf.IFunctionParameter[] args, int index )
		{
            if (args == null) throw new ArgumentNullException("args");
			return (string)args[ index ].GetTypedValue( DataTypeDescriptor.String, index );
		}

		/// <summary>
		/// Returns the value of the argument in the index specified of the type Base64Binary.
		/// </summary>
		/// <param name="args">The arguments list</param>
		/// <param name="index">The index</param>
		/// <returns>The Base64Binary value.</returns>
		protected static typ.Base64Binary GetBase64BinaryArgument( inf.IFunctionParameter[] args, int index )
		{
            if (args == null) throw new ArgumentNullException("args");
			return (typ.Base64Binary)args[ index ].GetTypedValue( DataTypeDescriptor.Base64Binary, index );
		}

		/// <summary>
		/// Returns the value of the argument in the index specified of the type Date.
		/// </summary>
		/// <param name="args">The arguments list</param>
		/// <param name="index">The index</param>
		/// <returns>The Date value.</returns>
		protected static DateTime GetDateArgument( inf.IFunctionParameter[] args, int index )
		{
            if (args == null) throw new ArgumentNullException("args");
			return (DateTime)args[ index ].GetTypedValue( DataTypeDescriptor.Date, index );
		}

		/// <summary>
		/// Returns the value of the argument in the index specified of the type Time.
		/// </summary>
		/// <param name="args">The arguments list</param>
		/// <param name="index">The index</param>
		/// <returns>The Time value.</returns>
		protected static DateTime GetTimeArgument( inf.IFunctionParameter[] args, int index )
		{
            if (args == null) throw new ArgumentNullException("args");
			return (DateTime)args[ index ].GetTypedValue( DataTypeDescriptor.Time, index );
		}

		/// <summary>
		/// Returns the value of the argument in the index specified of the type DateTime.
		/// </summary>
		/// <param name="args">The arguments list</param>
		/// <param name="index">The index</param>
		/// <returns>The DateTime value.</returns>
		protected static DateTime GetDateTimeArgument( inf.IFunctionParameter[] args, int index )
		{
            if (args == null) throw new ArgumentNullException("args");
			return (DateTime)args[ index ].GetTypedValue( DataTypeDescriptor.DateTime, index );
		}

		/// <summary>
		/// Returns the value of the argument in the index specified of the type DaytimeDuration.
		/// </summary>
		/// <param name="args">The arguments list</param>
		/// <param name="index">The index</param>
		/// <returns>The DaytimeDuration value.</returns>
		protected static typ.DaytimeDuration GetDaytimeDurationArgument( inf.IFunctionParameter[] args, int index )
		{
            if (args == null) throw new ArgumentNullException("args");
			return (typ.DaytimeDuration)args[ index ].GetTypedValue( DataTypeDescriptor.DaytimeDuration, index );
		}

		/// <summary>
		/// Returns the value of the argument in the index specified of the type Double.
		/// </summary>
		/// <param name="args">The arguments list</param>
		/// <param name="index">The index</param>
		/// <returns>The Double value.</returns>
		protected static double GetDoubleArgument( inf.IFunctionParameter[] args, int index )
		{
            if (args == null) throw new ArgumentNullException("args");
			return (double)args[ index ].GetTypedValue( DataTypeDescriptor.Double, index );
		}

		/// <summary>
		/// Returns the value of the argument in the index specified of the type HexBinary.
		/// </summary>
		/// <param name="args">The arguments list</param>
		/// <param name="index">The index</param>
		/// <returns>The HexBinary value.</returns>
		protected static typ.HexBinary GetHexBinaryArgument( inf.IFunctionParameter[] args, int index )
		{
            if (args == null) throw new ArgumentNullException("args");
			return (typ.HexBinary)args[ index ].GetTypedValue( DataTypeDescriptor.HexBinary, index );
		}

		/// <summary>
		/// Returns the value of the argument in the index specified of the type Rfc822Name.
		/// </summary>
		/// <param name="args">The arguments list</param>
		/// <param name="index">The index</param>
		/// <returns>The Rfc822Name value.</returns>
		protected static typ.Rfc822Name GetRfc822NameArgument( inf.IFunctionParameter[] args, int index )
		{
            if (args == null) throw new ArgumentNullException("args");
			return (typ.Rfc822Name)args[ index ].GetTypedValue( DataTypeDescriptor.Rfc822Name, index );
		}

		/// <summary>
		/// Returns the value of the argument in the index specified of the type X500Name.
		/// </summary>
		/// <param name="args">The arguments list</param>
		/// <param name="index">The index</param>
		/// <returns>The X500Name value.</returns>
		protected static typ.X500Name GetX500NameArgument( inf.IFunctionParameter[] args, int index )
		{
            if (args == null) throw new ArgumentNullException("args");
			return (typ.X500Name)args[ index ].GetTypedValue( DataTypeDescriptor.X500Name, index );
		}

		/// <summary>
		/// Returns the value of the argument in the index specified of the type YearMonthDuration.
		/// </summary>
		/// <param name="args">The arguments list</param>
		/// <param name="index">The index</param>
		/// <returns>The YearMonthDuration value.</returns>
		protected static typ.YearMonthDuration GetYearMonthDurationArgument( inf.IFunctionParameter[] args, int index )
		{
            if (args == null) throw new ArgumentNullException("args");
            if (args == null) throw new ArgumentNullException("args");
			return (typ.YearMonthDuration)args[ index ].GetTypedValue( DataTypeDescriptor.YearMonthDuration, index );
		}

		/// <summary>
		/// Returns the value of the argument in the index specified of the type DnsName.
		/// </summary>
		/// <param name="args">The arguments list</param>
		/// <param name="index">The index</param>
		/// <returns>The DnsName value.</returns>
		protected static typ.DnsNameDataType GetDnsNameArgument( inf.IFunctionParameter[] args, int index )
		{
            if (args == null) throw new ArgumentNullException("args");
			return (typ.DnsNameDataType)args[ index ].GetTypedValue( DataTypeDescriptor.DnsName, index );
		}

		/// <summary>
		/// Returns the value of the argument in the index specified of the type IPAddress.
		/// </summary>
		/// <param name="args">The arguments list</param>
		/// <param name="index">The index</param>
		/// <returns>The IPAddress value.</returns>
		protected static typ.IPAddress GetIPAddressArgument( inf.IFunctionParameter[] args, int index )
		{
            if (args == null) throw new ArgumentNullException("args");
			return (typ.IPAddress)args[ index ].GetTypedValue( DataTypeDescriptor.IPAddress, index );
		}

		#endregion
	}
}
