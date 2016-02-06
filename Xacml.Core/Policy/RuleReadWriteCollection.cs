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
using System.Xml;
using cor = Xacml.Core;

namespace Xacml.Core.Policy
{
	/// <summary>
	/// Defines a typed collection of read/write Rules.
	/// </summary>
	public class RuleReadWriteCollection : CollectionBase 
	{
		#region CollectionBase members

		/// <summary>
		/// Adds an object to the end of the CollectionBase.
		/// </summary>
		/// <param name="value">The Object to be added to the end of the CollectionBase. </param>
		/// <returns>The CollectionBase index at which the value has been added.</returns>
		public virtual int Add( RuleElementReadWrite value )  
		{
			return( List.Add( value ) );
		}

		/// <summary>
		/// Clears the collection
		/// </summary>
		public virtual new void Clear()
		{
			base.Clear();
		}
		/// <summary>
		/// Removes the specified element
		/// </summary>
		/// <param name="index">Position of the element</param>
		public virtual new void RemoveAt ( int index )
		{
			base.RemoveAt(index);
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Gets the index of the given RuleElementReadWrite in the collection
		/// </summary>
		/// <param name="rule"></param>
		/// <returns></returns>
		public int GetIndex( RuleElementReadWrite rule )
		{
			for( int i=0; i<this.Count; i++ )
			{
				if( this.List[i] == rule )
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>
		/// Writes the XML of the current element
		/// </summary>
		/// <param name="writer">The XmlWriter in which the element will be written</param>
		public void WriteDocument(XmlWriter writer)
		{
            if (writer == null) throw new ArgumentNullException("writer");
			foreach(RuleElementReadWrite oRule in this)
			{
				writer.WriteStartElement(PolicySchema1.RuleElement.Rule);
				writer.WriteAttributeString(PolicySchema1.RuleElement.RuleId, oRule.Id);
				writer.WriteAttributeString(PolicySchema1.RuleElement.Effect, oRule.Effect.ToString());
				if( oRule.Description.Length != 0 )
				{
					writer.WriteElementString(PolicySchema1.RuleElement.Description, oRule.Description);
				}
				if(oRule.Target != null)
				{
					oRule.Target.WriteDocument(writer);
				}
				if(oRule.Condition != null)
				{
					oRule.Condition.WriteDocument(writer);
				}
				writer.WriteEndElement();
			}
		}

		#endregion
	}
}