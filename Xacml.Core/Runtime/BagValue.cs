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
using typ = Xacml.Core.Runtime.DataTypes;
using rtm = Xacml.Core.Runtime;
using inf = Xacml.Core.Interfaces;
using cor = Xacml.Core;

namespace Xacml.Core.Runtime
{
	/// <summary>
	/// Defines a Bag of values as defined in A.5
	/// </summary>
	public class BagValue : inf.IFunctionParameter
	{
		#region Static members

		/// <summary>
		/// Returns a default Empty bag of values.
		/// </summary>
		public static BagValue Empty
		{
			get{ return new BagValue( null ); }
		}

		#endregion

		#region Private members

		/// <summary>
		/// The elements of the bag.
		/// </summary>
		private ArrayList _elements = new ArrayList();

		/// <summary>
		/// The datatype of the elements of the bag
		/// </summary>
		private inf.IDataType _dataType;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new empty bag for the given data type.
		/// </summary>
		/// <param name="dataType">The data type of the bag.</param>
		public BagValue( inf.IDataType dataType )
		{
			_dataType = dataType;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Add an element to the bag.
		/// </summary>
		/// <param name="value">The element to add.</param>
		public void Add( object value )
		{
			_elements.Add( value );
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
			if( dataType != DataTypeDescriptor.Bag )
			{
				throw new EvaluationException( "invalid datatype." );
			}
			return this;
		}

		/// <summary>
		/// Can't get a value from the bag using this function.
		/// </summary>
		/// <param name="parNo">THe number of parameter used only for error notification.</param>
		/// <returns></returns>
		public inf.IFunction GetFunction( int parNo )
		{
			throw new EvaluationException( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_datatype_in_stringvalue, parNo, "BagValue" ] );
		}

		/// <summary>
		/// Gets whether the function parameter is a bag of values.
		/// </summary>
		public bool IsBag
		{
			get{ return true; }
		}

		/// <summary>
		/// Gets the size of the bag.
		/// </summary>
		public int BagSize
		{
			get{ return _elements.Count; }
		}

		/// <summary>
		/// Gets the elements of the bag so they can be iterated.
		/// </summary>
		public ArrayList Elements
		{ 
			get{ return _elements; } 
		}

		#endregion
	}
}
