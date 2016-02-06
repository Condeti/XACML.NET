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

using Xacml.Control.Center.TreeNodes;
using con = Xacml.Core.Context;

namespace Xacml.Control.Center.ContextTreeNodes
{
	/// <summary>
	/// 
	/// </summary>
	public class Request : NoBoldNode
	{
		/// <summary>
		/// 
		/// </summary>
		private con.RequestElementReadWrite _request;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		public Request( con.RequestElementReadWrite request )
		{
			_request = request;

			this.Text = "Request";
			this.SelectedImageIndex = 1;
			this.ImageIndex = 1;

			if( _request.Action != null )
			{
				this.Nodes.Add( new Action( _request.Action ) );
			}
			if( _request.Environment != null )
			{
				this.Nodes.Add( new Environment( _request.Environment ) ) ;
			}
			if( _request.Resources != null )
			{
				foreach( con.ResourceElementReadWrite resource in _request.Resources )
				{
					this.Nodes.Add( new Resource( resource ) );
				}
			}
			if( _request.Subjects != null )
			{
				foreach( con.SubjectElementReadWrite subject in _request.Subjects )
				{
					this.Nodes.Add( new Subject( subject ) );
				}
			}
			this.ExpandAll();
		}

		/// <summary>
		/// 
		/// </summary>
		public con.RequestElementReadWrite RequestDefinition
		{
			get{ return _request; }
		}
	}
}