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
using typ = Xacml.Core.Runtime.DataTypes;
using rtm = Xacml.Core.Runtime;
using inf = Xacml.Core.Interfaces;
using cor = Xacml.Core;

namespace Xacml.Core.Runtime
{
	/// <summary>
	/// This class represents the values that can be handled by the evaluation engine.
	/// </summary>
	public class EvaluationValue : inf.IFunctionParameter
	{
		#region Static members

		/// <summary>
		/// Default evaluation value True.
		/// </summary>
		private static EvaluationValue _true = new EvaluationValue( (object)true, DataTypeDescriptor.Boolean );

		/// <summary>
		/// Default evaluation value False.
		/// </summary>
		private static EvaluationValue _false = new EvaluationValue( (object)false, DataTypeDescriptor.Boolean );

		/// <summary>
		/// Default evaluation value True.
		/// </summary>
		public static EvaluationValue True
		{
			get{ return _true; }
		}

		/// <summary>
		/// Default evaluation value Fale.
		/// </summary>
		public static EvaluationValue False
		{
			get{ return _false; }
		}

		/// <summary>
		/// Default evaluation value Indeterminate.
		/// </summary>
		public static EvaluationValue Indeterminate
		{
			get{ return new EvaluationValue( true ); }
		}

		#endregion

		#region Private members

		/// <summary>
		/// Whether the value is indeterminate.
		/// </summary>
		private bool _isIndeterminate;

		/// <summary>
		/// The value contained.
		/// </summary>
		private object _value;

		/// <summary>
		/// The data type of the value.
		/// </summary>
		private inf.IDataType _dataType;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new empty EvaluationValue or Indeterminate.
		/// </summary>
		/// <param name="isIndeterminate">Whether the EvaluationValue will be Indeterminate or not.</param>
		private EvaluationValue( bool isIndeterminate )
		{
			_isIndeterminate = isIndeterminate;
		}

		/// <summary>
		/// Creates a new evaluation value specifying the data type.
		/// </summary>
		/// <param name="value">The value to hold in the evaluation value.</param>
		/// <param name="dataType">The data type for the value.</param>
		public EvaluationValue( object value, inf.IDataType dataType )
		{
            if (dataType == null) throw new ArgumentNullException("dataType");
			if( value is EvaluationValue )
			{
				_value = ((EvaluationValue)value)._value;
				_dataType = ((EvaluationValue)value)._dataType;
			}
			else if( value is BagValue )
			{
				_value = value;
				_dataType = dataType;
			}
			else if(value is inf.IFunctionParameter )
			{
				_value = ((inf.IFunctionParameter)value).GetTypedValue( dataType, 0 );
				_dataType = dataType;
			}
			else
			{
				_value = value;
				_dataType = dataType;
			}
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The value contained.
		/// </summary>
		public object Value
		{
			get{ return _value; }
		}

		/// <summary>
		/// Whether the value is Indeterminate.
		/// </summary>
		public bool IsIndeterminate
		{
			get{ return _isIndeterminate; }
		}

		/// <summary>
		/// The value as a boolean value.
		/// </summary>
		public bool BoolValue
		{
			get
			{ 
				if( !(_value is bool) || _isIndeterminate )
				{
					throw new EvaluationException( "invalid data type." );
				}
				return (bool)_value; 
			}
		}

		#endregion

		#region IFunctionParameter Members

		/// <summary>
		/// Gets the data type of the value.
		/// </summary>
		/// <param name="context">The evaluation context.</param>
		/// <returns>The data type descriptor.</returns>
		public inf.IDataType GetType( EvaluationContext context )
		{ 
			return _dataType;
		}

		/// <summary>
		/// Gets the value as a generic object.
		/// </summary>
		/// <param name="dataType">The expected data type of the value.</param>
		/// <param name="parNo">THe number of parameter used only for error notification.</param>
		/// <returns></returns>
		public object GetTypedValue( inf.IDataType dataType, int parNo )
		{
			if( dataType != _dataType )
			{
				throw new EvaluationException( Resource.ResourceManager[ Resource.MessageKey.exc_type_mismatch, dataType, _dataType ] );
			}
			return _value;
		}

		/// <summary>
		/// A function can't be within an EvaluationValue so an exception will be thrown always.
		/// </summary>
		/// <param name="parNo">THe number of parameter used only for error notification.</param>
		/// <returns>None.</returns>
		public inf.IFunction GetFunction( int parNo )
		{
			throw new EvaluationException( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_datatype_in_stringvalue, parNo, "" ] );
		}

		/// <summary>
		/// Whether the value is a bag.
		/// </summary>
		public bool IsBag
		{
			get{ return (_value is BagValue); } //TODO: an exception if the element is not a bag
		}

		/// <summary>
		/// If the value is a bag the size will be returned otherwise an exception is thrown.
		/// </summary>
		public int BagSize
		{
			get{ return ((BagValue)_value).BagSize; } //TODO: an exception if the element is not a bag
		}

		/// <summary>
		/// The elements of the bag value.
		/// </summary>
		public ArrayList Elements
		{
			get{ return ((BagValue)_value).Elements; } //TODO: an exception if the element is not a bag
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Returns a string representation of the value.
		/// </summary>
		/// <returns>A string representation of the value.</returns>
		public override string ToString()
		{
			if( IsIndeterminate )
			{
				return Decision.Indeterminate.ToString();
			}
			else
			{
				if( _value != null )
				{
					return "[" + _dataType + ":" + _value.ToString() + "]";
				}
				else
				{
					return "null";
				}
			}
		}

		#endregion
	}
}
