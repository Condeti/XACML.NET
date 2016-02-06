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
using Xacml.Core.Runtime.Functions;
using Xacml.PolicySchema1;
using inf = Xacml.Core.Interfaces;

namespace Xacml.Core.Runtime.DataTypes
{
	/// <summary>
	/// A class defining the Base64Binary data type.
	/// </summary>
	public class Base64Binary : inf.IDataType
	{
		#region Private members

		/// <summary>
		/// The byte array.
		/// </summary>
		private byte[] _value;

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		internal Base64Binary()
		{
		}

		/// <summary>
		/// Creates a Base64Binary with the byte array.
		/// </summary>
		/// <param name="value">The byte array.</param>
		public Base64Binary( byte[] value )
		{
			_value = value;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// The HashCode method overloaded because of a compiler warning. The base class is called.
		/// </summary>
		/// <returns>The HashCode calculated at the base class.</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>
		/// Equals method overloaded to compare two values of the same data type.
		/// </summary>
		/// <param name="obj">The object to compare with.</param>
		/// <returns>true, if both values are equals, otherwise false.</returns>
		public override bool Equals(object obj)
		{
			Base64Binary compareTo = obj as Base64Binary;
			if( compareTo == null )
			{
				return base.Equals( obj );
			}

			if( _value.Length != compareTo._value.Length )
			{
				return false;
			}

			for( int idx = 0; idx < _value.Length; idx++ )
			{
				if( _value[ idx ] != compareTo._value[ idx ] )
				{
					return false;
				}
			}
			return true;
		}

		#endregion

		#region IDataType Members

		/// <summary>
		/// Return the function that compares two values of this data type.
		/// </summary>
		/// <value>An IFunction instance.</value>
		public inf.IFunction EqualFunction
		{
			get{ return new Base64BinaryEqual(); }
		}

		/// <summary>
		/// Return the function that verifies if a value is contained within a bag of values of this data type.
		/// </summary>
		/// <value>An IFunction instance.</value>
		public inf.IFunction IsInFunction
		{
			get{ return new Base64BinaryIsIn(); }
		}

		/// <summary>
		/// Return the function that verifies if all the values in a bag are contained within another bag of values of this data type.
		/// </summary>
		/// <value>An IFunction instance.</value>
		public inf.IFunction SubsetFunction
		{
			get{ return new Base64BinarySubset(); }
		}

		/// <summary>
		/// The string representation of the data type constant.
		/// </summary>
		/// <value>A string with the Uri for the data type.</value>
		public string DataTypeName
		{ 
			get{ return InternalDataTypes.XsdBase64Binary; }
		}

		/// <summary>
		/// Return an instance of an Base64Binary form the string specified.
		/// </summary>
		/// <param name="value">The string value to parse.</param>
		/// <returns>An instance of the type.</returns>
		/// <param name="parNo">The parameter number being parsed.</param>
		public object Parse( string value, int parNo )
		{
			return new Base64Binary( Convert.FromBase64String( value ) );
		}

		#endregion
	}
}
