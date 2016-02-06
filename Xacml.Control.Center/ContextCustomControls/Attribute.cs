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
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using Xacml.Control.Center.CustomControls;
using Xacml.Core;
using Xacml.PolicySchema1;
using con = Xacml.Core.Context;
using rtm = Xacml.Core.Runtime;

namespace Xacml.Control.Center.ContextCustomControls
{
	/// <summary>
	/// Summary description for Attribute.
	/// </summary>
	public class Attribute : BaseControl
	{
		private Button btnAdd;
		private Button btnRemove;
		private con.AttributeElementReadWrite _attribute;
		private Label label1;
		private Label label2;
		private TextBox txtId;
		private GroupBox grpAttribute;
		private ComboBox cmbDataType;
		private TextBox txtIssuer;
		private Label label3;
		private GroupBox grpAttributeValue;
		private ListBox lstAttributeValue;
		private TextBox txtValue;
		private Label label5;
		private Container components = null;
		private int index = -1;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="attribute"></param>
		public Attribute( con.AttributeElementReadWrite attribute )
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			LoadingData = true;

			_attribute = attribute;

			foreach( FieldInfo field in typeof(InternalDataTypes).GetFields() )
			{
				cmbDataType.Items.Add( field.GetValue( null ) );
			}

			this.lstAttributeValue.DisplayMember = "Value";
			
			foreach( con.AttributeValueElementReadWrite attr in _attribute.AttributeValues )
			{
				lstAttributeValue.Items.Add( attr );
			}

			if( _attribute.AttributeValues.Count != 0 )
			{
				lstAttributeValue.SelectedIndex = 0;
			}

			txtId.Text = _attribute.AttributeId;
			txtId.DataBindings.Add( "Text", _attribute, "AttributeId" );
			txtIssuer.Text = _attribute.Issuer;
			//txtIssuer.DataBindings.Add( "Text", _attribute, "Issuer" );
			cmbDataType.SelectedIndex = cmbDataType.FindStringExact( _attribute.DataType);
			cmbDataType.DataBindings.Add( "SelectedItem", _attribute, "DataType" );
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
			this.grpAttributeValue = new System.Windows.Forms.GroupBox();
			this.lstAttributeValue = new System.Windows.Forms.ListBox();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.txtValue = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.grpAttribute = new System.Windows.Forms.GroupBox();
			this.txtIssuer = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cmbDataType = new System.Windows.Forms.ComboBox();
			this.txtId = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.grpAttributeValue.SuspendLayout();
			this.grpAttribute.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpAttributeValue
			// 
			this.grpAttributeValue.Controls.Add(this.lstAttributeValue);
			this.grpAttributeValue.Controls.Add(this.btnRemove);
			this.grpAttributeValue.Controls.Add(this.btnAdd);
			this.grpAttributeValue.Controls.Add(this.txtValue);
			this.grpAttributeValue.Controls.Add(this.label5);
			this.grpAttributeValue.Location = new System.Drawing.Point(8, 160);
			this.grpAttributeValue.Name = "grpAttributeValue";
			this.grpAttributeValue.Size = new System.Drawing.Size(576, 152);
			this.grpAttributeValue.TabIndex = 2;
			this.grpAttributeValue.TabStop = false;
			this.grpAttributeValue.Text = "Attribute value";
			// 
			// lstAttributeValue
			// 
			this.lstAttributeValue.Location = new System.Drawing.Point(8, 16);
			this.lstAttributeValue.Name = "lstAttributeValue";
			this.lstAttributeValue.Size = new System.Drawing.Size(472, 82);
			this.lstAttributeValue.TabIndex = 5;
			this.lstAttributeValue.SelectedIndexChanged += new System.EventHandler(this.lstAttributeValue_SelectedIndexChanged);
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(488, 56);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.TabIndex = 2;
			this.btnRemove.Text = "Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(488, 24);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.TabIndex = 1;
			this.btnAdd.Text = "Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// txtValue
			// 
			this.txtValue.Location = new System.Drawing.Point(80, 120);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(488, 20);
			this.txtValue.TabIndex = 9;
			this.txtValue.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 120);
			this.label5.Name = "label5";
			this.label5.TabIndex = 8;
			this.label5.Text = "Value:";
			// 
			// grpAttribute
			// 
			this.grpAttribute.Controls.Add(this.txtIssuer);
			this.grpAttribute.Controls.Add(this.label3);
			this.grpAttribute.Controls.Add(this.cmbDataType);
			this.grpAttribute.Controls.Add(this.txtId);
			this.grpAttribute.Controls.Add(this.label2);
			this.grpAttribute.Controls.Add(this.label1);
			this.grpAttribute.Location = new System.Drawing.Point(8, 8);
			this.grpAttribute.Name = "grpAttribute";
			this.grpAttribute.Size = new System.Drawing.Size(576, 128);
			this.grpAttribute.TabIndex = 3;
			this.grpAttribute.TabStop = false;
			this.grpAttribute.Text = "Attribute";
			// 
			// txtIssuer
			// 
			this.txtIssuer.Location = new System.Drawing.Point(80, 80);
			this.txtIssuer.Name = "txtIssuer";
			this.txtIssuer.Size = new System.Drawing.Size(488, 20);
			this.txtIssuer.TabIndex = 5;
			this.txtIssuer.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 80);
			this.label3.Name = "label3";
			this.label3.TabIndex = 4;
			this.label3.Text = "Issuer:";
			// 
			// cmbDataType
			// 
			this.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbDataType.Location = new System.Drawing.Point(80, 48);
			this.cmbDataType.Name = "cmbDataType";
			this.cmbDataType.Size = new System.Drawing.Size(488, 21);
			this.cmbDataType.TabIndex = 3;
			this.cmbDataType.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
			// 
			// txtId
			// 
			this.txtId.Location = new System.Drawing.Point(80, 16);
			this.txtId.Name = "txtId";
			this.txtId.Size = new System.Drawing.Size(488, 20);
			this.txtId.TabIndex = 2;
			this.txtId.Text = "";
			this.txtId.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.TabIndex = 1;
			this.label2.Text = "Data type:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "Attribute id:";
			// 
			// Attribute
			// 
			this.Controls.Add(this.grpAttribute);
			this.Controls.Add(this.grpAttributeValue);
			this.Name = "Attribute";
			this.Size = new System.Drawing.Size(592, 328);
			this.grpAttributeValue.ResumeLayout(false);
			this.grpAttribute.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAdd_Click(object sender, EventArgs e)
		{
			LoadingData = true;
			con.AttributeValueElementReadWrite att = new con.AttributeValueElementReadWrite( "TODO: Add value", XacmlVersion.Version11 );
			lstAttributeValue.Items.Add(att);
			_attribute.AttributeValues.Add( att );
			LoadingData = false;
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			con.AttributeValueElementReadWrite attr = lstAttributeValue.SelectedItem as con.AttributeValueElementReadWrite;

			try
			{
				LoadingData = true;
				
				txtValue.Text = string.Empty;
				_attribute.AttributeValues.RemoveAt(lstAttributeValue.SelectedIndex);
				lstAttributeValue.Items.RemoveAt(lstAttributeValue.SelectedIndex);
			}
			finally
			{
				LoadingData = false;
			}
		}

		private void lstAttributeValue_SelectedIndexChanged(object sender, EventArgs e)
		{
			if( index != lstAttributeValue.SelectedIndex )
			{
				if( index != -1 )
				{
					con.AttributeValueElementReadWrite attrAux = lstAttributeValue.Items[index] as con.AttributeValueElementReadWrite;
					attrAux.Value = txtValue.Text;
					lstAttributeValue.Items.RemoveAt( index );
					lstAttributeValue.Items.Insert( index, attrAux );
				}

				if(!LoadingData)
				{
					try
					{
						LoadingData = true;
						index = lstAttributeValue.SelectedIndex;
						con.AttributeValueElementReadWrite attribute = lstAttributeValue.SelectedItem as con.AttributeValueElementReadWrite;

						if( attribute != null )
						{
							txtValue.Text = attribute.Value;
						}
					}
					finally
					{
						LoadingData = false;
					}
				}
				else
				{
					index = -1;
				}
			}
		}

		/// <summary>
		/// Gets the element
		/// </summary>
		public con.AttributeElementReadWrite AttributeElement
		{
			get
			{
				if( index != -1 )
				{
					con.AttributeValueElementReadWrite attAux = lstAttributeValue.Items[index] as con.AttributeValueElementReadWrite;
					attAux.Value = txtValue.Text;
				}
				_attribute.AttributeId = txtId.Text;
				_attribute.DataType = cmbDataType.Text;
				_attribute.Issuer = txtIssuer.Text;
				return _attribute;
			}
		}
	}
}