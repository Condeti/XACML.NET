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
using System.Drawing;
using System.Windows.Forms;
using Xacml.PolicySchema1;
using pol = Xacml.Core.Policy;

namespace Xacml.Control.Center.CustomControls
{
	/// <summary>
	/// Summary description for PolicySet.
	/// </summary>
	public class PolicySet : BaseControl
	{
		private Label label1;
		private TextBox txtDescription;
		private GroupBox grpDefaults;
		private Label label2;
		private TextBox txtPolicySetId;
		private Label label3;
		private ComboBox cmbPolicyCombiningAlgorithm;
		private Label label4;
		private TextBox txtXPathVersion;
		private pol.PolicySetElementReadWrite _policySet;
		private Button btnApply;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		/// <summary>
		/// Creates a new control that points to the policy set element specified.
		/// </summary>
		/// <param name="policySet"></param>
		public PolicySet( pol.PolicySetElementReadWrite policySet )
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			_policySet = policySet;

			LoadingData = true;

			cmbPolicyCombiningAlgorithm.Items.Add( PolicyCombiningAlgorithms.DenyOverrides );
			cmbPolicyCombiningAlgorithm.Items.Add( PolicyCombiningAlgorithms.PermitOverrides );
			cmbPolicyCombiningAlgorithm.Items.Add( PolicyCombiningAlgorithms.FirstApplicable );
			cmbPolicyCombiningAlgorithm.Items.Add( PolicyCombiningAlgorithms.OnlyOneApplicable );

			txtDescription.Text = _policySet.Description;
			txtPolicySetId.Text = _policySet.Id;
			txtXPathVersion.Text = _policySet.XPathVersion;
			cmbPolicyCombiningAlgorithm.SelectedText = _policySet.PolicyCombiningAlgorithm;

			txtDescription.DataBindings.Add( "Text", _policySet, "Description" );
			txtPolicySetId.DataBindings.Add( "Text", _policySet, "Id" );
			if(_policySet.XPathVersion != null)
				txtXPathVersion.DataBindings.Add( "Text", _policySet, "XPathVersion" );
			cmbPolicyCombiningAlgorithm.DataBindings.Add( "SelectedValue", policySet, "PolicyCombiningAlgorithm" );

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
			this.label1 = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.grpDefaults = new System.Windows.Forms.GroupBox();
			this.txtXPathVersion = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtPolicySetId = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cmbPolicyCombiningAlgorithm = new System.Windows.Forms.ComboBox();
			this.btnApply = new System.Windows.Forms.Button();
			this.grpDefaults.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "Description:";
			// 
			// txtDescription
			// 
			this.txtDescription.Location = new System.Drawing.Point(80, 8);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtDescription.Size = new System.Drawing.Size(504, 64);
			this.txtDescription.TabIndex = 1;
			this.txtDescription.Text = "";
			this.txtDescription.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
			// 
			// grpDefaults
			// 
			this.grpDefaults.Controls.Add(this.txtXPathVersion);
			this.grpDefaults.Controls.Add(this.label4);
			this.grpDefaults.Location = new System.Drawing.Point(8, 144);
			this.grpDefaults.Name = "grpDefaults";
			this.grpDefaults.Size = new System.Drawing.Size(576, 56);
			this.grpDefaults.TabIndex = 2;
			this.grpDefaults.TabStop = false;
			this.grpDefaults.Text = "PolicySet Defaults";
			// 
			// txtXPathVersion
			// 
			this.txtXPathVersion.Location = new System.Drawing.Point(80, 24);
			this.txtXPathVersion.Name = "txtXPathVersion";
			this.txtXPathVersion.Size = new System.Drawing.Size(488, 20);
			this.txtXPathVersion.TabIndex = 1;
			this.txtXPathVersion.Text = "";
			this.txtXPathVersion.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(176, 23);
			this.label4.TabIndex = 0;
			this.label4.Text = "XPath version:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 80);
			this.label2.Name = "label2";
			this.label2.TabIndex = 0;
			this.label2.Text = "PolicySet Id:";
			// 
			// txtPolicySetId
			// 
			this.txtPolicySetId.Location = new System.Drawing.Point(80, 80);
			this.txtPolicySetId.Name = "txtPolicySetId";
			this.txtPolicySetId.Size = new System.Drawing.Size(504, 20);
			this.txtPolicySetId.TabIndex = 1;
			this.txtPolicySetId.Text = "";
			this.txtPolicySetId.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 112);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(176, 23);
			this.label3.TabIndex = 0;
			this.label3.Text = "Policy Combining Algorithm:";
			// 
			// cmbPolicyCombiningAlgorithm
			// 
			this.cmbPolicyCombiningAlgorithm.Location = new System.Drawing.Point(152, 112);
			this.cmbPolicyCombiningAlgorithm.Name = "cmbPolicyCombiningAlgorithm";
			this.cmbPolicyCombiningAlgorithm.Size = new System.Drawing.Size(432, 21);
			this.cmbPolicyCombiningAlgorithm.TabIndex = 3;
			this.cmbPolicyCombiningAlgorithm.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
			// 
			// button1
			// 
			this.btnApply.Location = new System.Drawing.Point(248, 208);
			this.btnApply.Name = "btnApply";
			this.btnApply.TabIndex = 4;
			this.btnApply.Text = "Apply";
			this.btnApply.Click += new System.EventHandler(this.button1_Click);
			// 
			// PolicySet
			// 
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.cmbPolicyCombiningAlgorithm);
			this.Controls.Add(this.grpDefaults);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtPolicySetId);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Name = "PolicySet";
			this.Size = new System.Drawing.Size(592, 232);
			this.grpDefaults.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, EventArgs e)
		{
			_policySet.Description = txtDescription.Text;
			_policySet.Id = txtPolicySetId.Text;
			_policySet.XPathVersion = txtXPathVersion.Text;
			_policySet.PolicyCombiningAlgorithm = cmbPolicyCombiningAlgorithm.SelectedText;

			ModifiedValue = false;

			txtDescription.BackColor = Color.White;
			txtPolicySetId.BackColor = Color.White;
			txtXPathVersion.BackColor = Color.White;
			cmbPolicyCombiningAlgorithm.BackColor = Color.White;
		}
		/// <summary>
		/// 
		/// </summary>
		public pol.PolicySetElementReadWrite PolicySetElement
		{
			get{ return _policySet; }
		}

	}
}
