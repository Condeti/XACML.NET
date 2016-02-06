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
using rtm = Xacml.Core.Runtime;
using typ = Xacml.Core.Runtime.DataTypes;
using inf = Xacml.Core.Interfaces;
using cor = Xacml.Core;

namespace Xacml.Core.Policy
{
	/// <summary>
	/// An abstract base class used in the PolicyDocument and the ContextDocument to define a 
	/// constant value defined in the documents that are represented as strings and requires a 
	/// conversion in order to perform evaluations with this value.
	/// </summary>
	public abstract class StringValueBase : 
		XacmlElement, inf.IFunctionParameter
	{
		#region Constructors

		/// <summary>
		/// A protected default constructor.
		/// </summary>
		/// <param name="schemaVersion">The schema used to validate the document.</param>
		/// <param name="schema">The schema that defines this element.</param>
		protected StringValueBase( XacmlSchema schema, XacmlVersion schemaVersion )
			: base( schema, schemaVersion )
		{
		}

		#endregion

		#region Abstract methods

		/// <summary>
		/// The typed value of this string value.
		/// </summary>
		public abstract string Value{ get; set; }

		/// <summary>
		/// The data type for this string value.
		/// </summary>
		public abstract string DataTypeValue{ get; set; }

		#endregion

		#region IFunctionParameter Members

		/// <summary>
		/// Gets the data type of the value.
		/// </summary>
		/// <param name="context">The evaluation context.</param>
		/// <returns>The data type descriptor.</returns>
		public inf.IDataType GetType( rtm.EvaluationContext context )
		{ 
			return rtm.EvaluationEngine.GetDataType( DataTypeValue );
		}

		/// <summary>
		/// Returns the typed value for this string value.
		/// </summary>
		/// <param name="dataType">The expected data type of the value.</param>
		/// <param name="parNo">The parameter number used only for error reporing.</param>
		/// <returns>The typed value as an object.</returns>
		public object GetTypedValue( inf.IDataType dataType, int parNo )
		{
            if (dataType == null) throw new ArgumentNullException("dataType");
			if( IsBag )
			{
				return this;
			}
			else
			{
				if( DataTypeValue != dataType.DataTypeName )
				{
					throw new EvaluationException( "invalid data type" ); //TODO: make the error more clear.
				}

				if( DataTypeValue != null )
				{
					if( DataTypeValue != dataType.DataTypeName )
					{
						throw new EvaluationException( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_datatype_in_stringvalue, parNo, DataTypeValue ] );
					}
				}

				return dataType.Parse( Value, parNo );

				//TODO: this exception is possible?
				//throw new EvaluationException( cor.Resource.ResourceManager[ cor.Resource.MessageKey.exc_invalid_datatype_in_stringvalue, parNo, DataTypeValue ] );
			}
		}

		/// <summary>
		/// A string value can't be a Bag
		/// </summary>
		public bool IsBag
		{
			get{ return false; }
		}

		/// <summary>
		/// A string value can't be a Bag
		/// </summary>
		public int BagSize
		{
			get{ return 0; }
		}

		/// <summary>
		/// A string value can't be a Bag
		/// </summary>
		public ArrayList Elements
		{ 
			get{ return null; } 
		}

		/// <summary>
		/// A string value can't be converted into a function.
		/// </summary>
		/// <param name="parNo">THe parameter number used only for error reporing.</param>
		/// <returns></returns>
		public inf.IFunction GetFunction( int parNo )
		{
			throw new EvaluationException( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_datatype_in_stringvalue, parNo, DataTypeValue ] );
		}

		#endregion
	}
}
