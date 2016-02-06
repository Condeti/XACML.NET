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
using System.ComponentModel;
using System.Windows.Forms;
using Xacml.Core;
using Xacml.PolicySchema1;
using pol = Xacml.Core.Policy;

namespace Xacml.Control.Center.CustomControls
{
	/// <summary>
	/// Summary description for PolicySet.
	/// </summary>
	public class TargetItem : BaseControl
	{
		private pol.TargetItemBaseReadWrite _targetItem;
		private ListBox lstMatch;
		private Button btnDown;
		private Button btnUp;
		private Button btnRemove;
		private Button btnAdd;
		private GroupBox grpTargetItems;
		private Panel mainPanel;
		private Hashtable _list = new Hashtable();
		private Button btnApply;
		private int index = -1;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="targetItem"></param>
		public TargetItem( pol.TargetItemBaseReadWrite targetItem )
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			LoadingData = true;

			_targetItem = targetItem;

			lstMatch.DisplayMember = "MatchId";
			foreach( pol.TargetMatchBaseReadWrite match in targetItem.Match )
			{
				lstMatch.Items.Add( match );
			}
			if( lstMatch.Items.Count != 0 )
			{
				lstMatch.SelectedIndex = 0;
			}

			LoadingData = false;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lstMatch = new System.Windows.Forms.ListBox();
			this.btnDown = new System.Windows.Forms.Button();
			this.btnUp = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.grpTargetItems = new System.Windows.Forms.GroupBox();
			this.mainPanel = new System.Windows.Forms.Panel();
			this.btnApply = new System.Windows.Forms.Button();
			this.grpTargetItems.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstMatch
			// 
			this.lstMatch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lstMatch.Location = new System.Drawing.Point(8, 16);
			this.lstMatch.Name = "lstMatch";
			this.lstMatch.Size = new System.Drawing.Size(472, 134);
			this.lstMatch.TabIndex = 5;
			this.lstMatch.SelectedIndexChanged += new System.EventHandler(this.lstMatch_SelectedIndexChanged);
			// 
			// btnDown
			// 
			this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDown.Location = new System.Drawing.Point(488, 112);
			this.btnDown.Name = "btnDown";
			this.btnDown.TabIndex = 4;
			this.btnDown.Text = "Down";
			// 
			// btnUp
			// 
			this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUp.Location = new System.Drawing.Point(488, 80);
			this.btnUp.Name = "btnUp";
			this.btnUp.TabIndex = 3;
			this.btnUp.Text = "Up";
			// 
			// btnRemove
			// 
			this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemove.Location = new System.Drawing.Point(488, 48);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.TabIndex = 2;
			this.btnRemove.Text = "Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.Location = new System.Drawing.Point(488, 16);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.TabIndex = 1;
			this.btnAdd.Text = "Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// grpTargetItems
			// 
			this.grpTargetItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpTargetItems.Controls.Add(this.lstMatch);
			this.grpTargetItems.Controls.Add(this.btnDown);
			this.grpTargetItems.Controls.Add(this.btnUp);
			this.grpTargetItems.Controls.Add(this.btnRemove);
			this.grpTargetItems.Controls.Add(this.btnAdd);
			this.grpTargetItems.Location = new System.Drawing.Point(8, 8);
			this.grpTargetItems.Name = "grpTargetItems";
			this.grpTargetItems.Size = new System.Drawing.Size(576, 160);
			this.grpTargetItems.TabIndex = 2;
			this.grpTargetItems.TabStop = false;
			this.grpTargetItems.Text = "Matches";
			// 
			// mainPanel
			// 
			this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.mainPanel.Location = new System.Drawing.Point(8, 176);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(576, 352);
			this.mainPanel.TabIndex = 3;
			// 
			// button1
			// 
			this.btnApply.Location = new System.Drawing.Point(264, 552);
			this.btnApply.Name = "btnApply";
			this.btnApply.TabIndex = 4;
			this.btnApply.Text = "Apply";
			this.btnApply.Click += new System.EventHandler(this.button1_Click);
			// 
			// TargetItem
			// 
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.mainPanel);
			this.Controls.Add(this.grpTargetItems);
			this.Name = "TargetItem";
			this.Size = new System.Drawing.Size(592, 584);
			this.grpTargetItems.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lstMatch_SelectedIndexChanged(object sender, EventArgs e)
		{
			if( lstMatch.SelectedItem != null )
			{
				if( index != -1 && index != lstMatch.SelectedIndex )
				{
					pol.TargetMatchBaseReadWrite indexMatch = lstMatch.Items[index] as pol.TargetMatchBaseReadWrite;
					lstMatch.Items.RemoveAt( index );
					lstMatch.Items.Insert( index, indexMatch );
				}
				pol.TargetMatchBaseReadWrite match = lstMatch.SelectedItem as pol.TargetMatchBaseReadWrite;
				index = lstMatch.SelectedIndex;
				try
				{
					LoadingData = true;
					Match matchControl = _list[	lstMatch.SelectedIndex ] as Match;
					if( matchControl == null )
					{
						matchControl = new Match( _targetItem, match,lstMatch.SelectedIndex );
						_list[ lstMatch.SelectedIndex ] = matchControl;
					}
					mainPanel.Controls.Clear();
					mainPanel.Controls.Add( matchControl );
				}
				finally
				{
					LoadingData = false;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAdd_Click(object sender, EventArgs e)
		{
			pol.TargetMatchBaseReadWrite targetMatch = null;

			if( _targetItem is pol.ActionElementReadWrite )
				targetMatch = new pol.ActionMatchElementReadWrite( 
					InternalFunctions.StringEqual, 
					new pol.AttributeValueElementReadWrite( InternalDataTypes.XsdString, "Somebody", XacmlVersion.Version11 ),  //TODO: check version
					new pol.ActionAttributeDesignatorElement( InternalDataTypes.XsdString, false, "", "", XacmlVersion.Version11 ),  //TODO: check version
					XacmlVersion.Version11 ) ;
			else if( _targetItem is pol.EnvironmentElementReadWrite )
				targetMatch = new pol.EnvironmentMatchElementReadWrite( 
					InternalFunctions.StringEqual, 
					new pol.AttributeValueElementReadWrite( InternalDataTypes.XsdString, "Somebody", XacmlVersion.Version11 ),  //TODO: check version
					new pol.EnvironmentAttributeDesignatorElement( InternalDataTypes.XsdString, false, "", "", XacmlVersion.Version11 ),  //TODO: check version
					XacmlVersion.Version11 ) ;
			else if( _targetItem is pol.ResourceElementReadWrite )
				targetMatch = new pol.ResourceMatchElementReadWrite( 
					InternalFunctions.StringEqual, 
					new pol.AttributeValueElementReadWrite( InternalDataTypes.XsdString, "Somebody", XacmlVersion.Version11 ),  //TODO: check version
					new pol.ResourceAttributeDesignatorElement( InternalDataTypes.XsdString, false, "", "", XacmlVersion.Version11 ),  //TODO: check version
					XacmlVersion.Version11 ) ;
			else if( _targetItem is pol.SubjectElementReadWrite )
				targetMatch = new pol.SubjectMatchElementReadWrite( 
					InternalFunctions.StringEqual, 
					new pol.AttributeValueElementReadWrite( InternalDataTypes.XsdString, "Somebody", XacmlVersion.Version11 ),  //TODO: check version
					new pol.SubjectAttributeDesignatorElement( InternalDataTypes.XsdString, false, "", "", "", XacmlVersion.Version11 ),  //TODO: check version
					XacmlVersion.Version11 ) ;

			_targetItem.Match.Add( targetMatch ); //TODO: check version

			try
			{
				LoadingData = true;
				Match matchControl = new Match( _targetItem, targetMatch,lstMatch.Items.Count );
				_list[ lstMatch.Items.Count ] = matchControl;
			}
			finally
			{
				LoadingData = false;
			}

			index = -1;
			lstMatch.Items.Clear();
			foreach( pol.TargetMatchBaseReadWrite match in _targetItem.Match )
			{
				lstMatch.Items.Add( match );
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			LoadingData = true;
			for( int index = 0; index<_list.Count; index++ )
			{
				_targetItem.Match[index] = ((Match)_list[index]).MatchElement;
			}
			
			LoadingData = false;
			ModifiedValue = false;

			lstMatch.Items.Clear();
			foreach( pol.TargetMatchBaseReadWrite match in _targetItem.Match )
			{
				lstMatch.Items.Add( match );
			}
			mainPanel.Controls.Clear();
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			LoadingData = true;
			_list.Remove( lstMatch.SelectedIndex );
			_targetItem.Match.RemoveAt( lstMatch.SelectedIndex );
			lstMatch.Items.RemoveAt( lstMatch.SelectedIndex );
			mainPanel.Controls.Clear();
			LoadingData = false;
			index = -1;
		}
		/// <summary>
		/// 
		/// </summary>
		public pol.TargetItemBaseReadWrite TargetItemBaseElement
		{
			get
			{
				if( index != -1 )
				{
					pol.TargetMatchBaseReadWrite indexMatch = lstMatch.Items[index] as pol.TargetMatchBaseReadWrite;
					lstMatch.Items.RemoveAt( index );
					lstMatch.Items.Insert( index, indexMatch );
				}
				return _targetItem;
			}
		}
	}
}
