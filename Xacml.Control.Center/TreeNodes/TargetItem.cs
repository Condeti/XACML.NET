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

using pol = Xacml.Core.Policy;

namespace Xacml.Control.Center.TreeNodes
{
	/// <summary>
	/// 
	/// </summary>
	public class TargetItem : NoBoldNode
	{
		/// <summary>
		/// 
		/// </summary>
		private pol.TargetItemBaseReadWrite _targetItem;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="targetItem"></param>
		public TargetItem( pol.TargetItemBaseReadWrite targetItem )
		{
			_targetItem = targetItem;

			if( targetItem is pol.ActionElementReadWrite )
			{
				this.Text = "Action";
				this.SelectedImageIndex = 2;
				this.ImageIndex = 2;
			}
			else if( targetItem is pol.SubjectElementReadWrite )
			{
				this.Text = "Subject";
				this.SelectedImageIndex = 1;
				this.ImageIndex = 1;
			}
			else if( targetItem is pol.ResourceElementReadWrite )
			{
				this.Text = "Resource";
				this.SelectedImageIndex = 3;
				this.ImageIndex = 3;
			}

			this.Text += " (" + targetItem.Match.Count + " match/es)";
		}

		/// <summary>
		/// 
		/// </summary>
		public pol.TargetItemBaseReadWrite TargetItemDefinition
		{
			get{ return _targetItem; }
		}
	}
}
