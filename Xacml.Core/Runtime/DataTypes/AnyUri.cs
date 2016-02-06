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
using cor = Xacml.Core;

namespace Xacml.Core.Runtime.DataTypes
{
	/// <summary>
	/// A AnyUri data type definition. This class contains two static helper methods that help
	/// to hierarchically compare two Uri instances.
	/// </summary>
	public class AnyUri : inf.IDataType
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		internal AnyUri()
		{
		}

		#endregion

		#region IDataType members

		/// <summary>
		/// Return the function that compares two values of this data type.
		/// </summary>
		/// <value>An IFunction instance.</value>
		public inf.IFunction EqualFunction
		{
			get{ return new AnyUriEqual(); }
		}

		/// <summary>
		/// Return the function that verifies if a value is contained within a bag of values of this data type.
		/// </summary>
		/// <value>An IFunction instance.</value>
		public inf.IFunction IsInFunction
		{
			get{ return new AnyUriIsIn(); }
		}

		/// <summary>
		/// Return the function that verifies if all the values in a bag are contained within another bag of values of this data type.
		/// </summary>
		/// <value>An IFunction instance.</value>
		public inf.IFunction SubsetFunction
		{
			get{ return new AnyUriSubset(); }
		}

		/// <summary>
		/// The string representation of the data type constant.
		/// </summary>
		/// <value>A string with the Uri for the data type.</value>
		public string DataTypeName
		{ 
			get{ return InternalDataTypes.XsdAnyUri; }
		}

		/// <summary>
		/// Return an instance of an AnyUri form the string specified.
		/// </summary>
		/// <param name="value">The string value to parse.</param>
		/// <param name="parNo">The parameter number being parsed.</param>
		/// <returns>An instance of the type.</returns>
		public object Parse( string value, int parNo )
		{
			try
			{
				return new Uri( value );
			}
			catch( Exception e )
			{
				throw new EvaluationException( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_datatype_in_stringvalue, parNo, DataTypeName ], e );
			}
		}

		#endregion

		#region Static members

		/// <summary>
		/// Verifies whether the first Uri specified is an indirect parent of the second Uri specified.
		/// </summary>
		/// <param name="parent">The parent Uri specified.</param>
		/// <param name="descendant">The descendant Uri specified.</param>
		/// <returns>True if the sencond Uri is a descendant of the first Uri, otherwise is False.</returns>
		public static bool IsDescendantOf( Uri parent, Uri descendant )
		{
            if (parent == null) throw new ArgumentNullException("parent");
            if (descendant == null) throw new ArgumentNullException("descendant");
			if( parent.Scheme == "urn" && descendant.Scheme == "urn" )
			{
				string[] parentParts = parent.AbsolutePath.Split( ':' );
				string[] descendantParts = descendant.AbsolutePath.Split( ':' );
				if( parentParts.Length > descendantParts.Length )
				{
					//Parent path is larger than descendant path so there's not a descendant
					return false;
				}
				for( int i = 0; i < parentParts.Length; i++ )
				{
					if( descendantParts[i] != parentParts[i] )
					{
						//Some parent path is not equal to descendant path so there's no relationship between them
						return false;
					}
				}
				return true;
			}
			else
			{
				throw new EvaluationException( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_uri_schema ] );
			}
		}

		/// <summary>
		/// Verifies whether the first Uri specified is a direct parent of the second Uri specified.
		/// </summary>
		/// <param name="parent">The parent Uri specified.</param>
		/// <param name="children">The children Uri specified.</param>
		/// <returns>True if the sencond Uri is a chldrend of the first Uri, otherwise is False.</returns>
		public static bool IsChildrenOf( Uri parent, Uri children )
		{
            if (parent == null) throw new ArgumentNullException("parent");
            if (children == null) throw new ArgumentNullException("children");
			if( parent.Scheme == "urn" && children.Scheme == "urn" )
			{
				string[] parentParts = parent.AbsolutePath.Split( ':' );
				string[] childrenParts = children.AbsolutePath.Split( ':' );
				if( parentParts.Length > childrenParts.Length )
				{
					//Parent path is larger than children path so there's not a children
					return false;
				}
				if( parentParts.Length+1 != childrenParts.Length )
				{
					//Parent path length +1 is not equals path length, so there's not a children is a descendant
					return false;
				}
				for( int i = 0; i < parentParts.Length; i++ )
				{
					if( childrenParts[i] != parentParts[i] )
					{
						//Some parent path is not equal to children path so there's no relationship between them
						return false;
					}
				}
				return true;
			}
			else
			{
				throw new EvaluationException( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_uri_schema ] );
			}
		}

		#endregion
	}
}
