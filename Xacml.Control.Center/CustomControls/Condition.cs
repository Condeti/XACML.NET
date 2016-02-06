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
using Xacml.Control.Center.TreeNodes;
using Xacml.Core;
using Xacml.PolicySchema1;
using pol = Xacml.Core.Policy;

namespace Xacml.Control.Center.CustomControls
{
	/// <summary>
	/// Summary description for Condition.
	/// </summary>
	public class Condition : BaseControl
	{
		private TreeView tvwCondition;
		private pol.ConditionElementReadWrite _condition;
		private GroupBox grpCondition;
		private Label label1;
		private ComboBox cmbDataType;
		private Label label2;
		private TextBox txtValue;
		private GroupBox grpElement;
		private ContextMenu contextMenu;
		private MenuItem mniAdd;
		private MenuItem mniDelete;
		private ComboBox cmbInternalFunctions;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		/// <summary>
		/// 
		/// </summary>
		public Condition( pol.ConditionElementReadWrite condition )
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			_condition = condition;

			tvwCondition.Nodes.Add( new FunctionExecution( condition ) );
			tvwCondition.ExpandAll();

			foreach( FieldInfo field in typeof(InternalDataTypes).GetFields() )
			{
				cmbDataType.Items.Add( field.GetValue( null ) );
			}
			foreach( FieldInfo field in typeof(InternalFunctions).GetFields() )
			{
				cmbInternalFunctions.Items.Add( field.GetValue( null ) );
			}
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
			this.tvwCondition = new System.Windows.Forms.TreeView();
			this.contextMenu = new System.Windows.Forms.ContextMenu();
			this.mniAdd = new System.Windows.Forms.MenuItem();
			this.mniDelete = new System.Windows.Forms.MenuItem();
			this.grpCondition = new System.Windows.Forms.GroupBox();
			this.grpElement = new System.Windows.Forms.GroupBox();
			this.cmbDataType = new System.Windows.Forms.ComboBox();
			this.txtValue = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbInternalFunctions = new System.Windows.Forms.ComboBox();
			this.grpCondition.SuspendLayout();
			this.grpElement.SuspendLayout();
			this.SuspendLayout();
			// 
			// tvwCondition
			// 
			this.tvwCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tvwCondition.ContextMenu = this.contextMenu;
			this.tvwCondition.ImageIndex = -1;
			this.tvwCondition.Location = new System.Drawing.Point(8, 24);
			this.tvwCondition.Name = "tvwCondition";
			this.tvwCondition.SelectedImageIndex = -1;
			this.tvwCondition.Size = new System.Drawing.Size(632, 248);
			this.tvwCondition.TabIndex = 0;
			this.tvwCondition.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvwCondition_MouseDown);
			this.tvwCondition.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwCondition_AfterSelect);
			this.tvwCondition.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvwCondition_BeforeSelect);
			// 
			// contextMenu
			// 
			this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.mniAdd,
																						this.mniDelete});
			this.contextMenu.Popup += new System.EventHandler(this.contextMenu_Popup);
			// 
			// mniAdd
			// 
			this.mniAdd.Index = 0;
			this.mniAdd.Text = "Add";
			// 
			// mniDelete
			// 
			this.mniDelete.Index = 1;
			this.mniDelete.Text = "Delete";
			this.mniDelete.Click += new System.EventHandler(this.mniDelete_Click);
			// 
			// grpCondition
			// 
			this.grpCondition.Controls.Add(this.grpElement);
			this.grpCondition.Controls.Add(this.tvwCondition);
			this.grpCondition.Location = new System.Drawing.Point(8, 8);
			this.grpCondition.Name = "grpCondition";
			this.grpCondition.Size = new System.Drawing.Size(656, 456);
			this.grpCondition.TabIndex = 1;
			this.grpCondition.TabStop = false;
			this.grpCondition.Text = "Condition";
			// 
			// grpElement
			// 
			this.grpElement.Controls.Add(this.cmbInternalFunctions);
			this.grpElement.Controls.Add(this.cmbDataType);
			this.grpElement.Controls.Add(this.txtValue);
			this.grpElement.Controls.Add(this.label1);
			this.grpElement.Controls.Add(this.label2);
			this.grpElement.Location = new System.Drawing.Point(8, 288);
			this.grpElement.Name = "grpElement";
			this.grpElement.Size = new System.Drawing.Size(632, 144);
			this.grpElement.TabIndex = 9;
			this.grpElement.TabStop = false;
			this.grpElement.Text = "Element";
			// 
			// cmbDataType
			// 
			this.cmbDataType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cmbDataType.Location = new System.Drawing.Point(72, 40);
			this.cmbDataType.Name = "cmbDataType";
			this.cmbDataType.Size = new System.Drawing.Size(536, 21);
			this.cmbDataType.TabIndex = 6;
			// 
			// txtValue
			// 
			this.txtValue.Location = new System.Drawing.Point(72, 88);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(536, 20);
			this.txtValue.TabIndex = 8;
			this.txtValue.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 24);
			this.label1.TabIndex = 1;
			this.label1.Text = "Data type:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 23);
			this.label2.TabIndex = 7;
			this.label2.Text = "Value:";
			// 
			// cmbInternalFunctions
			// 
			this.cmbInternalFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cmbInternalFunctions.Location = new System.Drawing.Point(72, 88);
			this.cmbInternalFunctions.Name = "cmbInternalFunctions";
			this.cmbInternalFunctions.Size = new System.Drawing.Size(536, 21);
			this.cmbInternalFunctions.TabIndex = 9;
			this.cmbInternalFunctions.Visible = false;
			// 
			// Condition
			// 
			this.Controls.Add(this.grpCondition);
			this.Name = "Condition";
			this.Size = new System.Drawing.Size(680, 488);
			this.grpCondition.ResumeLayout(false);
			this.grpElement.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void tvwCondition_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if( e.Node is FunctionExecution )
			{
				grpElement.Text = "Function execution";
				FunctionExecution node = (FunctionExecution)e.Node;
				txtValue.Visible = false;
				cmbInternalFunctions.Visible = true;
				cmbInternalFunctions.SelectedIndex = cmbInternalFunctions.FindStringExact( node.ApplyBaseDefinition.FunctionId );
				label2.Text = "FunctionId:";
				cmbDataType.Enabled = false;
			}
			else if( e.Node is FunctionParameter )
			{
				grpElement.Text = "Function parameter";
				FunctionParameter node = (FunctionParameter)e.Node;
				txtValue.Visible = false;
				cmbInternalFunctions.Visible = true;
				cmbInternalFunctions.SelectedIndex = cmbInternalFunctions.FindStringExact( node.FunctionDefinition.FunctionId );
				label2.Text = "FunctionId:";
				cmbDataType.Enabled = false;
			}
			else if( e.Node is AttributeValue )
			{
				grpElement.Text = "Attribute value";
				AttributeValue node = (AttributeValue)e.Node;
				txtValue.Visible = true;
				cmbInternalFunctions.Visible = false;
				txtValue.Text = node.AttributeValueDefinition.Contents;
				label2.Text = "Value:";
				cmbDataType.Enabled = true;
				cmbDataType.SelectedIndex = cmbDataType.FindStringExact( node.AttributeValueDefinition.DataType );
			}
			else if( e.Node is AttributeDesignator )
			{
				grpElement.Text = "Attribute designator";
				AttributeDesignator node = (AttributeDesignator)e.Node;
				txtValue.Visible = true;
				cmbInternalFunctions.Visible = false;
				txtValue.Text = node.AttributeDesignatorDefinition.AttributeId;
				label2.Text = "AttributeId:";
				cmbDataType.Enabled = true;
				cmbDataType.SelectedIndex = cmbDataType.FindStringExact( node.AttributeDesignatorDefinition.DataType );
			}
			else if( e.Node is AttributeSelector )
			{
				grpElement.Text = "Attribute selector";
				AttributeSelector node = (AttributeSelector)e.Node;
				txtValue.Visible = true;
				cmbInternalFunctions.Visible = false;
				txtValue.Text = node.AttributeSelectorDefinition.RequestContextPath;
				label2.Text = "XPath:";
				cmbDataType.Enabled = false;
			}
		}

		private void tvwCondition_MouseDown(object sender, MouseEventArgs e)
		{
			if( e.Button == MouseButtons.Right )
			{
				tvwCondition.SelectedNode = tvwCondition.GetNodeAt( e.X, e.Y );
			}
		}

		private void tvwCondition_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			NoBoldNode node = (NoBoldNode)tvwCondition.SelectedNode;

			if( node is FunctionExecution )
			{
				FunctionExecution funcNode = ((FunctionExecution)node);
				funcNode.ApplyBaseDefinition.FunctionId = cmbInternalFunctions.Text;
				tvwCondition.SelectedNode = funcNode;
				tvwCondition.SelectedNode.Text = "[" + "dataType" + "] " + funcNode.ApplyBaseDefinition.FunctionId;
			}
			else if( node is FunctionParameter )
			{
				FunctionParameter funcNode = ((FunctionParameter)node);
				funcNode.FunctionDefinition.FunctionId = cmbInternalFunctions.Text;
				tvwCondition.SelectedNode = funcNode;
				tvwCondition.SelectedNode.Text = "Function: " + funcNode.FunctionDefinition.FunctionId;;
			}
			else if( node is AttributeValue )
			{
				AttributeValue attNode = ((AttributeValue)node);
				attNode.AttributeValueDefinition.Value = txtValue.Text;
				attNode.AttributeValueDefinition.DataType = cmbDataType.Text;
				tvwCondition.SelectedNode = attNode;
				tvwCondition.SelectedNode.Text = "[" + attNode.AttributeValueDefinition.DataType + "] " + attNode.AttributeValueDefinition.Contents;
			}
			else if( node is AttributeDesignator )
			{
				AttributeDesignator attNode = ((AttributeDesignator)node);
				attNode.AttributeDesignatorDefinition.AttributeId = txtValue.Text;
				attNode.AttributeDesignatorDefinition.DataType = cmbDataType.Text;
				tvwCondition.SelectedNode = attNode;
				tvwCondition.SelectedNode.Text = "[" + attNode.AttributeDesignatorDefinition.DataType + "]:" + attNode.AttributeDesignatorDefinition.AttributeId;
			}
			else if( node is AttributeSelector )
			{
				AttributeSelector attNode = ((AttributeSelector)node);
				attNode.AttributeSelectorDefinition.RequestContextPath = txtValue.Text;
				tvwCondition.SelectedNode = attNode;
				tvwCondition.SelectedNode.Text = "XPath: " + attNode.AttributeSelectorDefinition.RequestContextPath;
			}
		}

		#region Context menu

		private void contextMenu_Popup(object sender, EventArgs e)
		{
			mniAdd.MenuItems.Clear();
			if( tvwCondition.SelectedNode == null)
			{
				if(tvwCondition.Nodes.Count == 0)
				{
					mniAdd.MenuItems.Add( "Function execution", new EventHandler( CreateFunctionExecution ) );
					mniAdd.MenuItems.Add( "Function parameter", new EventHandler( CreateFunctionParameter ) );
					mniAdd.MenuItems.Add( "Attribute value", new EventHandler( CreateAttributeValue ) );
					mniAdd.MenuItems.Add( "Action attribute designator", new EventHandler( CreateActionAttributeDesignator ) );
					mniAdd.MenuItems.Add( "Subject attribute designator", new EventHandler( CreateSubjectAttributeDesignator ) );
					mniAdd.MenuItems.Add( "Resource designator", new EventHandler( CreateResourceAttributeDesignator ) );
					mniAdd.MenuItems.Add( "Attribute selector", new EventHandler( CreateAttributeSelector ) );
				}
				else
				{
					mniDelete.Visible = false;
				}
			}
			else if( tvwCondition.SelectedNode is FunctionExecution)
			{
				mniAdd.MenuItems.Add( "Function execution", new EventHandler( CreateFunctionExecutionFromFunction ) );
				mniAdd.MenuItems.Add( "Function parameter", new EventHandler( CreateFunctionParameterFromFunction ) );
				mniAdd.MenuItems.Add( "Attribute value", new EventHandler( CreateAttributeValueFromFunction ) );
				mniAdd.MenuItems.Add( "Action attribute designator", new EventHandler( CreateActionAttributeDesignatorFromFunction ) );
				mniAdd.MenuItems.Add( "Subject attribute designator", new EventHandler( CreateSubjectAttributeDesignatorFromFunction ) );
				mniAdd.MenuItems.Add( "Resource designator", new EventHandler( CreateResourceAttributeDesignatorFromFunction ) );
				mniAdd.MenuItems.Add( "Attribute selector", new EventHandler( CreateAttributeSelectorFromFunction ) );
			}

			if( mniAdd.MenuItems.Count == 0 )
			{
				mniAdd.Visible = false;
			}
			else
			{
				mniAdd.Visible = true;
			}
		}

		private void CreateFunctionExecution( object sender, EventArgs args )
		{
			pol.ApplyElement apply = new pol.ApplyElement( "urn:new_function", new pol.IExpressionReadWriteCollection(), XacmlVersion.Version11 );
			FunctionExecution node = new FunctionExecution( apply );

			tvwCondition.Nodes.Add( node );
			_condition.Arguments.Add( apply );
		}

		private void CreateFunctionExecutionFromFunction( object sender, EventArgs args )
		{
			FunctionExecution func = (FunctionExecution)tvwCondition.SelectedNode;
			pol.ApplyBaseReadWrite parentApply = func.ApplyBaseDefinition;

			pol.ApplyElement apply = new pol.ApplyElement( "urn:new_function", new pol.IExpressionReadWriteCollection(), XacmlVersion.Version11 );
			FunctionExecution node = new FunctionExecution( apply );

			func.Nodes.Add( node );
			parentApply.Arguments.Add( apply );
		}

		private void CreateFunctionParameter( object sender, EventArgs args )
		{
			pol.FunctionElementReadWrite function = new pol.FunctionElementReadWrite( "urn:new_function_param", XacmlVersion.Version11 );
			FunctionParameter node = new FunctionParameter( function );

			tvwCondition.Nodes.Add( node );
			_condition.Arguments.Add( function );
		}

		private void CreateFunctionParameterFromFunction( object sender, EventArgs args )
		{
			FunctionExecution func = (FunctionExecution)tvwCondition.SelectedNode;
			pol.ApplyBaseReadWrite parentApply = func.ApplyBaseDefinition;

			pol.FunctionElementReadWrite function = new pol.FunctionElementReadWrite( "urn:new_function_param", XacmlVersion.Version11 );
			FunctionParameter node = new FunctionParameter( function );

			func.Nodes.Add( node );
			parentApply.Arguments.Add( function );
		}

		private void CreateAttributeValueFromFunction( object sender, EventArgs args )
		{
			FunctionExecution func = (FunctionExecution)tvwCondition.SelectedNode;
			pol.ApplyBaseReadWrite parentApply = func.ApplyBaseDefinition;

			pol.AttributeValueElementReadWrite attr = new pol.AttributeValueElementReadWrite( InternalDataTypes.XsdString, "TODO: Add content", XacmlVersion.Version11 );
			AttributeValue node = new AttributeValue( attr );

			func.Nodes.Add( node );
			parentApply.Arguments.Add( attr );
		}

		private void CreateAttributeValue( object sender, EventArgs args )
		{
			pol.AttributeValueElementReadWrite attr = new pol.AttributeValueElementReadWrite( InternalDataTypes.XsdString, "TODO: Add content", XacmlVersion.Version11 );
			AttributeValue node = new AttributeValue( attr );

			tvwCondition.Nodes.Add( node );
			_condition.Arguments.Add( attr );
		}

		private void CreateActionAttributeDesignator( object sender, EventArgs args )
		{
			pol.ActionAttributeDesignatorElement att = new pol.ActionAttributeDesignatorElement( string.Empty, false, "TODO: Add attribute id", string.Empty ,XacmlVersion.Version11 );
			AttributeDesignator node = new AttributeDesignator( att );

			tvwCondition.Nodes.Add( node );
			_condition.Arguments.Add( att );
		}

		private void CreateSubjectAttributeDesignator( object sender, EventArgs args )
		{
			pol.SubjectAttributeDesignatorElement att = new pol.SubjectAttributeDesignatorElement( string.Empty, false, "TODO: Add attribute id", string.Empty, string.Empty ,XacmlVersion.Version11 );
			AttributeDesignator node = new AttributeDesignator( att );

			tvwCondition.Nodes.Add( node );
			_condition.Arguments.Add( att );
		}

		private void CreateResourceAttributeDesignator( object sender, EventArgs args )
		{
			pol.ResourceAttributeDesignatorElement att = new pol.ResourceAttributeDesignatorElement( string.Empty, false, "TODO: Add attribute id", string.Empty, XacmlVersion.Version11 );
			AttributeDesignator node = new AttributeDesignator( att );

			tvwCondition.Nodes.Add( node );
			_condition.Arguments.Add( att );
		}

		private void CreateActionAttributeDesignatorFromFunction( object sender, EventArgs args )
		{
			FunctionExecution func = (FunctionExecution)tvwCondition.SelectedNode;
			pol.ApplyBaseReadWrite parentApply = func.ApplyBaseDefinition;

			pol.ActionAttributeDesignatorElement att = new pol.ActionAttributeDesignatorElement( string.Empty, false, "TODO: Add attribute id", string.Empty ,XacmlVersion.Version11 );
			AttributeDesignator node = new AttributeDesignator( att );

			func.Nodes.Add( node );
			parentApply.Arguments.Add( att );
		}

		private void CreateSubjectAttributeDesignatorFromFunction( object sender, EventArgs args )
		{
			FunctionExecution func = (FunctionExecution)tvwCondition.SelectedNode;
			pol.ApplyBaseReadWrite parentApply = func.ApplyBaseDefinition;

			pol.SubjectAttributeDesignatorElement att = new pol.SubjectAttributeDesignatorElement( string.Empty, false, "TODO: Add attribute id", string.Empty, string.Empty ,XacmlVersion.Version11 );
			AttributeDesignator node = new AttributeDesignator( att );

			func.Nodes.Add( node );
			parentApply.Arguments.Add( att );
		}

		private void CreateResourceAttributeDesignatorFromFunction( object sender, EventArgs args )
		{
			FunctionExecution func = (FunctionExecution)tvwCondition.SelectedNode;
			pol.ApplyBaseReadWrite parentApply = func.ApplyBaseDefinition;

			pol.ResourceAttributeDesignatorElement att = new pol.ResourceAttributeDesignatorElement( string.Empty, false, "TODO: Add attribute id", string.Empty, XacmlVersion.Version11 );
			AttributeDesignator node = new AttributeDesignator( att );

			func.Nodes.Add( node );
			parentApply.Arguments.Add( att );
		}

		private void CreateAttributeSelector( object sender, EventArgs args )
		{
			pol.AttributeSelectorElement attr = new pol.AttributeSelectorElement( string.Empty, false, "TODO: Add XPath", XacmlVersion.Version11 );
			AttributeSelector node = new AttributeSelector( attr );

			tvwCondition.Nodes.Add( node );
			_condition.Arguments.Add( attr );
		}

		private void CreateAttributeSelectorFromFunction( object sender, EventArgs args )
		{
			FunctionExecution func = (FunctionExecution)tvwCondition.SelectedNode;
			pol.ApplyBaseReadWrite parentApply = func.ApplyBaseDefinition;

			pol.AttributeSelectorElement attr = new pol.AttributeSelectorElement( string.Empty, false, "TODO: Add XPath", XacmlVersion.Version11 );
			AttributeSelector node = new AttributeSelector( attr );

			func.Nodes.Add( node );
			parentApply.Arguments.Add( attr );
		}

		private void mniDelete_Click(object sender, EventArgs e)
		{
			FunctionExecution functionNode =  (FunctionExecution)tvwCondition.SelectedNode.Parent;

			NoBoldNode node = (NoBoldNode)tvwCondition.SelectedNode;

			if( node is FunctionExecution )
			{
				FunctionExecution funcNode = ((FunctionExecution)node);
				int index = functionNode.ApplyBaseDefinition.Arguments.GetIndex( (pol.ApplyElement)funcNode.ApplyBaseDefinition );
				functionNode.ApplyBaseDefinition.Arguments.RemoveAt( index );
				functionNode.Nodes.Remove( funcNode );
			}
			else if( node is FunctionParameter )
			{
				FunctionParameter funcNode = ((FunctionParameter)node);
				int index = functionNode.ApplyBaseDefinition.Arguments.GetIndex( funcNode.FunctionDefinition );
				functionNode.ApplyBaseDefinition.Arguments.RemoveAt( index );
				functionNode.Nodes.Remove( funcNode );
			}
			else if( node is AttributeValue )
			{
				AttributeValue attNode = ((AttributeValue)node);
				int index = functionNode.ApplyBaseDefinition.Arguments.GetIndex( attNode.AttributeValueDefinition );
				functionNode.ApplyBaseDefinition.Arguments.RemoveAt( index );
				functionNode.Nodes.Remove( attNode );
			}
			else if( node is AttributeDesignator )
			{
				AttributeDesignator attNode = ((AttributeDesignator)node);
				int index = functionNode.ApplyBaseDefinition.Arguments.GetIndex( attNode.AttributeDesignatorDefinition );
				functionNode.ApplyBaseDefinition.Arguments.RemoveAt( index );
				functionNode.Nodes.Remove( attNode );
			}
			else if( node is AttributeSelector )
			{
				AttributeSelector attNode = ((AttributeSelector)node);
				int index = functionNode.ApplyBaseDefinition.Arguments.GetIndex( attNode.AttributeSelectorDefinition );
				functionNode.ApplyBaseDefinition.Arguments.RemoveAt( index );
				functionNode.Nodes.Remove( attNode );
			}
		}
		
		#endregion
	}
}
