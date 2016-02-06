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
using System.Globalization;
using System.Text.RegularExpressions;
using Xacml.Core.Runtime.Functions;
using Xacml.PolicySchema1;
using inf = Xacml.Core.Interfaces;

namespace Xacml.Core.Runtime.DataTypes
{
	/// <summary>
	/// A class defining the X500Name data type.
	/// </summary>
	public class X500Name : inf.IDataType
	{
		#region Private members

		/// <summary>
		/// The ordered list of keys found in the name.
		/// </summary>
		private ArrayList _keys = new ArrayList();
		
		/// <summary>
		/// The list of parts found in the name.
		/// </summary>
		private ArrayList _parts = new ArrayList();

		/// <summary>
		/// The full name.
		/// </summary>
		private string _name;

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		internal X500Name()
		{
		}

		/// <summary>
		/// Creates a new X500Name using the name as a string.
		/// </summary>
		/// <param name="name">The name as a string.</param>
		public X500Name( string name )
		{
            if (name == null || name.Length == 0) throw new ArgumentNullException("name");
			_name = name;
			string[] parts = name.Split( ',' );
			//iterate through the parts in reverse order, to allow terminal RDN match
			for (int i = parts.GetUpperBound(0); i >=0; i--)
			{
				X500RDN rdn = new X500RDN(parts[i]);
				_parts.Add(rdn);
				_keys.Add(rdn.Attribute);
			}
		}

		#endregion

		#region Public methods

		/// <summary>
		/// The HashCode method overloaded because of a compiler warning. The base class is called.
		/// </summary>
		/// <returns>The HashCode calculated at the base class.</returns>
		public override int GetHashCode()
		{
			return _name.GetHashCode();
		}

		/// <summary>
		/// Equals method overloaded to compare two values of the same data type.
		/// </summary>
		/// <param name="obj">The object to compare with.</param>
		/// <returns>true, if both values are equals, otherwise false.</returns>
		public override bool Equals(object obj)
		{
			if( !(obj is X500Name ) )
			{
				return base.Equals( obj );
			}

			X500Name compareTo = (X500Name)obj;

			//to be equal, they MUST have the same number of RDNs
			if (_parts.Count != compareTo._parts.Count)
			{
				return false;
			}

			//iterate through the individual parts, starting at the terminal RDN
			for (int i = 0; i < _parts.Count; i++)
			{
				if (!((X500RDN)_parts[i]).Equals(((X500RDN)compareTo._parts[i])))
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Matches this full name with the name provided as a parameter.
		/// </summary>
		/// <param name="compareTo">A name to compare with.</param>
		/// <returns>true, if the names match, otherwise false.</returns>
		public bool Matches(X500Name compareTo)
		{
            if (compareTo == null) throw new ArgumentNullException("compareTo");
			//iterate through the individual parts, starting at the terminal RDN
			for (int i = 0; i < _parts.Count; i++)
			{
				if (!((X500RDN)_parts[i]).Equals(((X500RDN)compareTo._parts[i])))
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
			get{ return new X500NameEqual(); }
		}

		/// <summary>
		/// Return the function that verifies if a value is contained within a bag of values of this data type.
		/// </summary>
		/// <value>An IFunction instance.</value>
		public inf.IFunction IsInFunction
		{
			get{ return new X500NameIsIn(); }
		}

		/// <summary>
		/// Return the function that verifies if all the values in a bag are contained within another bag of values of this data type.
		/// </summary>
		/// <value>An IFunction instance.</value>
		public inf.IFunction SubsetFunction
		{
			get{ return new X500NameSubset(); }
		}

		/// <summary>
		/// The string representation of the data type constant.
		/// </summary>
		/// <value>A string with the Uri for the data type.</value>
		public string DataTypeName
		{ 
			get{ return InternalDataTypes.X500Name; }
		}

		/// <summary>
		/// Return an instance of an x500Name form the string specified.
		/// </summary>
		/// <param name="value">The string value to parse.</param>
		/// <param name="parNo">The parameter number being parsed.</param>
		/// <returns>An instance of the type.</returns>
		public object Parse( string value, int parNo )
		{
			try
			{
				return new X500Name( value );
			}
			catch( Exception e )
			{
				throw new EvaluationException( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_datatype_in_stringvalue, parNo, DataTypeName ], e );
			}
		}

		#endregion

		#region Relative Distinguished Name subclass

		private class X500RDN
		{
			/// <summary>
			/// The name of the attribute found in the RDN.
			/// </summary>
			public string _attribute;

			/// <summary>
			/// The value of the attribute found in the RDN.
			/// </summary>
			private string _value;
            
			/// <summary>
			/// The full attributeTypeValue of the RDN.
			/// </summary>
			private string _relativeName;

			#region Constructors

			/// <summary>
			/// Default constructor.
			/// </summary>
			internal X500RDN()
			{
			}

			/// <summary>
			/// Creates a new X500RDN using the name as a string.
			/// </summary>
			/// <param name="relativeName">The name as a string.</param>
			public X500RDN(string relativeName)
			{
				_relativeName = relativeName;

				string[] pair;
				pair = relativeName.Split('=');
				_attribute = pair[0].Trim();

				//remove all extra whitespace as per RFC 3280
				Regex regEx = new Regex(@"\s+");
				_value = regEx.Replace(pair[1].Trim(), " ");
			}
			#endregion

			#region Public Methods

			/// <summary>
			/// Equals method overloaded to compare two values of the same data type.
			/// </summary>
			/// <param name="obj">The object to compare with.</param>
			/// <returns>true, if both values are equals, otherwise false.</returns>
			public override bool Equals(object obj)
			{
				if (!(obj is X500RDN))
				{
					return base.Equals(obj);
				}

				X500RDN compareTo = (X500RDN) obj;

				return ((string.Compare(this.Attribute, compareTo.Attribute, true, CultureInfo.InvariantCulture)==0)
					&& (string.Compare(this.Value, compareTo.Value, true, CultureInfo.InvariantCulture)==0));

			}
			/// <summary>
			/// The HashCode method overloaded because of a compiler warning. The base class is called.
			/// </summary>
			/// <returns>The HashCode calculated at the base class.</returns>
			public override int GetHashCode()
			{
				return _relativeName.GetHashCode();
			}

			#endregion

			#region Public Properties

			/// <summary>
			/// The name of the attribute found in the RDN.
			/// </summary>
			public string Attribute
			{
				get
				{
					return _attribute;
				}
				set
				{
					_attribute = value;
				}
			}

			/// <summary>
			/// The value of the attribute found in the RDN.
			/// </summary>
			public string Value
			{
				get
				{
					return _value;
				}
				set
				{
					_value = value;
				}
			}

			#endregion
		}

		#endregion

	}
}
