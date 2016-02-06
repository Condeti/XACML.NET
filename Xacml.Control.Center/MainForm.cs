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
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Xacml.ContextSchema;
using Xacml.Control.Center.ContextCustomControls;
using Xacml.Control.Center.ContextTreeNodes;
using Xacml.Control.Center.CustomControls;
using Xacml.Control.Center.TreeNodes;
using Xacml.Core;
using Xacml.Core.Runtime;
using Xacml.PolicySchema1;
using Action = System.Action;
using Attribute = Xacml.Control.Center.ContextTreeNodes.Attribute;
using pol = Xacml.Core.Policy;
using con = Xacml.Core.Context;
using Condition = Xacml.Control.Center.TreeNodes.Condition;
using Obligations = Xacml.Control.Center.TreeNodes.Obligations;
using Policy = Xacml.Control.Center.TreeNodes.Policy;
using PolicySet = Xacml.Control.Center.TreeNodes.PolicySet;
using Resource = Xacml.Control.Center.ContextTreeNodes.Resource;
using ResourceElement = Xacml.PolicySchema1.ResourceElement;
using Rule = Xacml.Control.Center.TreeNodes.Rule;
using SubjectElement = Xacml.PolicySchema1.SubjectElement;
using Target = Xacml.Control.Center.TreeNodes.Target;
using TargetItem = Xacml.Control.Center.TreeNodes.TargetItem;

namespace Xacml.Control.Center
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : Form
	{
		/// <summary>
		/// 
		/// </summary>
		public static readonly Font DEFAULT_FONT = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((Byte)(0)));
		private MenuItem menuItem1;
		private TreeView mainTree;
		private ImageList mainImageList;
		private MenuItem menuItem2;
		private Panel panel1;
		private Splitter splitter1;
		private ContextMenu contextMenu;
		private Panel mainPanel;
		private MainMenu mainMenu;
		private MenuItem menuItem10;
		private MenuItem menuItem11;
		private MenuItem mniCreateNew;
		private MenuItem mniDelete;
		private OpenFileDialog openFileDialog;
		private MenuItem menuItem3;
		private SaveFileDialog saveFileDialog;
		private IContainer components;
		private MenuItem menuItem4;
		private MenuItem menuItem5;
		private string _path = string.Empty;
		private MenuItem menuItem6;
		private MenuItem menuItem7;
		private MenuItem menuItem8;
		private MenuItem menuItem9;
		private DocumentType docType;

		/// <summary>
		/// 
		/// </summary>
		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.mainTree = new System.Windows.Forms.TreeView();
			this.contextMenu = new System.Windows.Forms.ContextMenu();
			this.mniCreateNew = new System.Windows.Forms.MenuItem();
			this.mniDelete = new System.Windows.Forms.MenuItem();
			this.mainImageList = new System.Windows.Forms.ImageList(this.components);
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.panel1 = new System.Windows.Forms.Panel();
			this.mainPanel = new System.Windows.Forms.Panel();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainTree
			// 
			this.mainTree.AllowDrop = true;
			this.mainTree.ContextMenu = this.contextMenu;
			this.mainTree.Dock = System.Windows.Forms.DockStyle.Left;
			this.mainTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.mainTree.FullRowSelect = true;
			this.mainTree.HotTracking = true;
			this.mainTree.ImageList = this.mainImageList;
			this.mainTree.Location = new System.Drawing.Point(0, 0);
			this.mainTree.Name = "mainTree";
			this.mainTree.Size = new System.Drawing.Size(232, 625);
			this.mainTree.TabIndex = 0;
			this.mainTree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainTree_MouseDown);
			this.mainTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.mainTree_AfterSelect);
			this.mainTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.mainTree_BeforeSelect);
			// 
			// contextMenu
			// 
			this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.mniCreateNew,
																						this.mniDelete});
			this.contextMenu.Popup += new System.EventHandler(this.contextMenu_Popup);
			// 
			// mniCreateNew
			// 
			this.mniCreateNew.Index = 0;
			this.mniCreateNew.Text = "Create new";
			// 
			// mniDelete
			// 
			this.mniDelete.Index = 1;
			this.mniDelete.Text = "Delete";
			this.mniDelete.Click += new System.EventHandler(this.mniDelete_Click);
			// 
			// mainImageList
			// 
			this.mainImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.mainImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("mainImageList.ImageStream")));
			this.mainImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuItem1,
																					 this.menuItem6,
																					 this.menuItem10});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem5,
																					  this.menuItem9,
																					  this.menuItem3,
																					  this.menuItem4});
			this.menuItem1.Text = "File";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "Open Policy...";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 1;
			this.menuItem5.Text = "Open Request...";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Enabled = false;
			this.menuItem9.Index = 2;
			this.menuItem9.Text = "Save";
			this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Enabled = false;
			this.menuItem3.Index = 3;
			this.menuItem3.Text = "Save as...";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 4;
			this.menuItem4.Text = "Close";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 1;
			this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem7,
																					  this.menuItem8});
			this.menuItem6.Text = "Context";
			// 
			// menuItem7
			// 
			this.menuItem7.Enabled = false;
			this.menuItem7.Index = 0;
			this.menuItem7.Text = "Run with policy...";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Enabled = false;
			this.menuItem8.Index = 1;
			this.menuItem8.Text = "Run with request...";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 2;
			this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.menuItem11});
			this.menuItem10.Text = "Help";
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 0;
			this.menuItem11.Text = "About";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.mainPanel);
			this.panel1.Controls.Add(this.splitter1);
			this.panel1.Controls.Add(this.mainTree);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(864, 625);
			this.panel1.TabIndex = 3;
			// 
			// mainPanel
			// 
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.Location = new System.Drawing.Point(235, 0);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(629, 625);
			this.mainPanel.TabIndex = 4;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(232, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 625);
			this.splitter1.TabIndex = 3;
			this.splitter1.TabStop = false;
			// 
			// openFileDialog
			// 
			this.openFileDialog.DefaultExt = "xml";
			this.openFileDialog.Filter = "Policy Files|*.xml|All Files|*.*";
			this.openFileDialog.InitialDirectory = ".";
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.CreatePrompt = true;
			this.saveFileDialog.DefaultExt = "xml";
			this.saveFileDialog.Filter = "Policy Files|*.xml|All Files|*.*";
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(864, 625);
			this.Controls.Add(this.panel1);
			this.Menu = this.mainMenu;
			this.Name = "MainForm";
			this.Text = "XACML Control Center";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem2_Click(object sender, EventArgs e)
		{
			openFileDialog.Filter = "Policy Files|*.xml|All Files|*.*";
			if( openFileDialog.ShowDialog() == DialogResult.OK )
			{
				Stream stream = openFileDialog.OpenFile();
				pol.PolicyDocumentReadWrite doc = PolicyLoader.LoadPolicyDocument( stream, XacmlVersion.Version20, DocumentAccess.ReadWrite );
				_path = openFileDialog.FileName;
				mainTree.Nodes.Add( new PolicyDocument( doc ) );
				docType = DocumentType.Policy;
				menuItem3.Enabled = true;
				menuItem9.Enabled = true;
				menuItem2.Enabled = false;
				menuItem5.Enabled = false;
				menuItem8.Enabled = true;
				menuItem7.Enabled = false;
				stream.Close();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mainTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			// Clear the main panel
			mainPanel.Controls.Clear();

			// If the control have been instantiated before use it, otherwise create the control.
			if( e.Node.Tag != null )
			{
				mainPanel.Controls.Add( (System.Windows.Forms.Control)e.Node.Tag );
			}
			else
			{
				// Create the control depending on the node type.
				if( e.Node is PolicySet )
				{
					mainPanel.Controls.Add( new CustomControls.PolicySet( ((PolicySet)e.Node).PolicySetDefinition ) );
				}
				else if( e.Node is Policy )
				{
					mainPanel.Controls.Add( new CustomControls.Policy( ((Policy)e.Node).PolicyDefinition ) );
				}
				else if( e.Node is PolicyIdReference )
				{
				}
				else if( e.Node is PolicySetIdReference )
				{
				}
				else if( e.Node is Target )
				{
				}
				else if( e.Node is Obligations )
				{
					mainPanel.Controls.Add( new CustomControls.Obligations( ((Obligations)e.Node).ObligationDefinition ) );
				}
				else if( e.Node is TargetItem )
				{
					mainPanel.Controls.Add( new CustomControls.TargetItem( ((TargetItem)e.Node).TargetItemDefinition ) );
				}
				else if( e.Node is Rule )
				{
					mainPanel.Controls.Add( new CustomControls.Rule( ((Rule)e.Node).RuleDefinition ) );
				}
				else if( e.Node is Condition )
				{
					mainPanel.Controls.Add( new CustomControls.Condition( ((Condition)e.Node).ConditionDefinition ) );
				}
				else if( e.Node is Attribute )
				{
					mainPanel.Controls.Add( new ContextCustomControls.Attribute( ((Attribute)e.Node).AttributeDefinition ) );
				}
				else if( e.Node is Resource )
				{
					mainPanel.Controls.Add( new ContextCustomControls.Resource( ((Resource)e.Node).ResourceDefinition ) );
				}

				// If the control was created and added successfully, Dock it and keep the 
				// instance in the tree node.
				if( mainPanel.Controls.Count != 0 )
				{
					mainPanel.Controls[0].Dock = DockStyle.Fill;
					e.Node.Tag = mainPanel.Controls[0];
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void contextMenu_Popup(object sender, EventArgs e)
		{
			mniCreateNew.MenuItems.Clear();
			if( mainTree.SelectedNode == null)
			{
				if(mainTree.Nodes.Count == 0)
				{
					mniCreateNew.MenuItems.Add( "PolicyDocument", new EventHandler( CreatePolicyDocument ) );
					mniCreateNew.MenuItems.Add( "ContextDocument", new EventHandler( CreateContextDocument ) );
				}
			}
			else if( mainTree.SelectedNode is PolicyDocument)
			{
				if( ((PolicyDocument)mainTree.SelectedNode).PolicyDocumentDefinition.Policy == null &&
					((PolicyDocument)mainTree.SelectedNode).PolicyDocumentDefinition.PolicySet == null)
				{
					mniCreateNew.MenuItems.Add( "Policy", new EventHandler( CreatePolicyFromDocument ) );
					mniCreateNew.MenuItems.Add( "PolicySet", new EventHandler( CreatePolicySetFromDocument ) );
				}
			}
			else if( mainTree.SelectedNode is PolicySet )
			{
				mniCreateNew.MenuItems.Add( "Policy", new EventHandler( CreatePolicy ) );
				mniCreateNew.MenuItems.Add( "PolicySet", new EventHandler( CreatePolicySet ) );
				if( ((PolicySet)mainTree.SelectedNode).PolicySetDefinition.Target == null )
				{
					mniCreateNew.MenuItems.Add( "Target", new EventHandler( CreateTarget ) );
				}
				if( ((PolicySet)mainTree.SelectedNode).PolicySetDefinition.Obligations == null )
				{
					mniCreateNew.MenuItems.Add( "Obligations", new EventHandler( CreateObligationsFromPolicySet ) );
				}
			}
			else if( mainTree.SelectedNode is Policy )
			{
				mniCreateNew.MenuItems.Add( "Rule", new EventHandler( CreateRule ) );
				if( ((Policy)mainTree.SelectedNode).PolicyDefinition.Target == null )
				{
					mniCreateNew.MenuItems.Add( "Target", new EventHandler( CreateTarget ) );
				}
				if( ((Policy)mainTree.SelectedNode).PolicyDefinition.Obligations == null )
				{
					mniCreateNew.MenuItems.Add( "Obligations", new EventHandler( CreateObligationsFromPolicy ) );
				}
			}
			else if( mainTree.SelectedNode is Rule )
			{
				if( ((Rule)mainTree.SelectedNode).RuleDefinition.Condition == null )
				{
					mniCreateNew.MenuItems.Add( "Condition", new EventHandler( CreateCondition ) );
				}
				if( ((Rule)mainTree.SelectedNode).RuleDefinition.Target == null )
				{
					mniCreateNew.MenuItems.Add( "Target", new EventHandler( CreateTarget ) );
				}
			}
			else if( mainTree.SelectedNode is PolicyIdReference )
			{
			}
			else if( mainTree.SelectedNode is PolicySetIdReference )
			{
			}
			else if( mainTree.SelectedNode is Obligations )
			{
			}
			else if( mainTree.SelectedNode is Target )
			{
				
			}
			else if( mainTree.SelectedNode is TargetItem )
			{
			}
			else if( mainTree.SelectedNode is AnyTarget )
			{
				mniCreateNew.MenuItems.Add( "Target", new EventHandler( CreateTarget ) );
			}
			else if( mainTree.SelectedNode is AnySubject )
			{
				mniCreateNew.MenuItems.Add( "Subject", new EventHandler( CreateTargetItem ) );
			}
			else if( mainTree.SelectedNode is AnyAction )
			{
				mniCreateNew.MenuItems.Add( "Action", new EventHandler( CreateTargetItem ) );
			}
			else if( mainTree.SelectedNode is AnyResource )
			{
				mniCreateNew.MenuItems.Add( "Resource", new EventHandler( CreateTargetItem ) );
			}
			else if( mainTree.SelectedNode is Condition )
			{
			}
			else if( mainTree.SelectedNode is Request )
			{
				if( ((Request)mainTree.SelectedNode).RequestDefinition.Action == null )
				{
					mniCreateNew.MenuItems.Add( "Action", new EventHandler( CreateContextActionElement ) );
				}
				mniCreateNew.MenuItems.Add( "Resource", new EventHandler( CreateContextResourceElement ) );
				mniCreateNew.MenuItems.Add( "Subject", new EventHandler( CreateContextSubjectElement ) );
			}
			else if( mainTree.SelectedNode is ContextTreeNodes.Action || mainTree.SelectedNode is Resource ||
				mainTree.SelectedNode is Subject)
			{
				mniCreateNew.MenuItems.Add( "Attribute", new EventHandler( CreateContextAttributeElement ) );
			}
			else if( mainTree.SelectedNode is Context )
			{
				if( ((Context)mainTree.SelectedNode).ContextDefinition.Request == null )
				{
					mniCreateNew.MenuItems.Add( "Request", new EventHandler( CreateContextRequest ) );
				}
			}

			if( mniCreateNew.MenuItems.Count == 0 )
			{
				mniCreateNew.Visible = false;
			}
			else
			{
				mniCreateNew.Visible = true;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mainTree_MouseDown(object sender, MouseEventArgs e)
		{
			if( e.Button == MouseButtons.Right )
			{
				mainTree.SelectedNode = mainTree.GetNodeAt( e.X, e.Y );
			}
		}

		private void CreateContextSubjectElement( object sender, EventArgs args)
		{
			con.SubjectElementReadWrite newSubject = new con.SubjectElementReadWrite(string.Empty, new con.AttributeReadWriteCollection(), XacmlVersion.Version11);
			Request requestNode = (Request)mainTree.SelectedNode;
			Subject newNode = new Subject( newSubject );
			con.RequestElementReadWrite request = requestNode.RequestDefinition;

			if( request.Subjects == null )
			{
				request.Subjects = new con.SubjectReadWriteCollection();
			}
			request.Subjects.Add( newSubject );
			requestNode.Nodes.Add( newNode );
		}

		private void CreateContextAttributeElement( object sender, EventArgs args )
		{
			NoBoldNode node = (NoBoldNode)mainTree.SelectedNode;

			if( node is ContextTreeNodes.Action )
			{
				ContextTreeNodes.Action actionNode = (ContextTreeNodes.Action)mainTree.SelectedNode;
				con.ActionElementReadWrite action = actionNode.ActionDefinition;
				con.AttributeElementReadWrite attribute = new con.AttributeElementReadWrite( string.Empty, string.Empty, string.Empty,
					string.Empty, "TODO: Add value", XacmlVersion.Version11);
				Attribute attributeNode = new Attribute( attribute );

				action.Attributes.Add( attribute );
				actionNode.Nodes.Add( attributeNode );
			}
			else if( node is Resource )
			{
				Resource resourceNode = (Resource)mainTree.SelectedNode;
				con.ResourceElementReadWrite resource = resourceNode.ResourceDefinition;
				con.AttributeElementReadWrite attribute = new con.AttributeElementReadWrite( string.Empty, string.Empty, string.Empty,
					string.Empty, "TODO: Add value", XacmlVersion.Version11);
				Attribute attributeNode = new Attribute( attribute );

				resource.Attributes.Add( attribute );
				resourceNode.Nodes.Add( attributeNode );
			}
			else if( node is Subject )
			{
				Subject subjectNode = (Subject)mainTree.SelectedNode;
				con.SubjectElementReadWrite subject = subjectNode.SubjectDefinition;
				con.AttributeElementReadWrite attribute = new con.AttributeElementReadWrite( "urn:new_attribute", string.Empty, string.Empty,
					string.Empty, "TODO: Add value", XacmlVersion.Version11);
				Attribute attributeNode = new Attribute( attribute );

				subject.Attributes.Add( attribute );
				subjectNode.Nodes.Add( attributeNode );
			}
		}

		private void CreateContextActionElement( object sender, EventArgs args)
		{
			con.ActionElementReadWrite newAction = new con.ActionElementReadWrite(new con.AttributeReadWriteCollection(), XacmlVersion.Version11);
			Request requestNode = (Request)mainTree.SelectedNode;
            var newNode = new ContextTreeNodes.Action(newAction);
			con.RequestElementReadWrite request = requestNode.RequestDefinition;

			request.Action = newAction;
			requestNode.Nodes.Add( newNode );
		}
		private void CreateContextResourceElement( object sender, EventArgs args)
		{
			con.ResourceElementReadWrite newResource = new con.ResourceElementReadWrite(null, con.ResourceScope.Immediate, new con.AttributeReadWriteCollection(), XacmlVersion.Version11);
			Request requestNode = (Request)mainTree.SelectedNode;
			Resource newNode = new Resource( newResource );
			con.RequestElementReadWrite request = requestNode.RequestDefinition;

			if( request.Resources == null )
			{
				request.Resources = new con.ResourceReadWriteCollection();
			}
			request.Resources.Add( newResource );
			requestNode.Nodes.Add( newNode );
		}

		/// <summary>
		/// Creates a new context document
		/// </summary>
		/// <param name="sender">The mainTree control.</param>
		/// <param name="args">THe arguements for the event.</param>
		private void CreateContextDocument( object sender, EventArgs args )
		{
			// Create a new policydocument
			con.ContextDocumentReadWrite newContext = new con.ContextDocumentReadWrite( ); //TODO: check version

			newContext.Namespaces.Add(string.Empty, Namespaces.Context);
			newContext.Namespaces.Add("xsi", Namespaces.Xsi);
			Context newNode = new Context(newContext);
			mainTree.Nodes.Add(newNode);
			docType = DocumentType.Request;
			newNode.NodeFont = new Font( mainTree.Font, FontStyle.Bold );
			menuItem2.Enabled = false;
			menuItem5.Enabled = false;
			menuItem3.Enabled = true;
			menuItem9.Enabled = true;
			menuItem7.Enabled = true;
			menuItem8.Enabled = false;
		}

		private void CreateContextRequest( object sender, EventArgs args )
		{
			con.RequestElementReadWrite newRequest = new con.RequestElementReadWrite(null,null,null,null,XacmlVersion.Version11);
			Context contextNode = (Context)mainTree.SelectedNode;
			Request requestNode = new Request( newRequest );
			con.ContextDocumentReadWrite context = contextNode.ContextDefinition;

			context.Request = newRequest;
			contextNode.Nodes.Add( requestNode );
		}

		/// <summary>
		/// Creates a new policy document
		/// </summary>
		/// <param name="sender">The mainTree control.</param>
		/// <param name="args">The arguements for the event.</param>
		private void CreatePolicyDocument( object sender, EventArgs args )
		{
			// Create a new policydocument
			pol.PolicyDocumentReadWrite newPolicyDoc = new pol.PolicyDocumentReadWrite(XacmlVersion.Version11 ); //TODO: check version

			newPolicyDoc.Namespaces.Add(string.Empty, Namespaces.Policy);
			newPolicyDoc.Namespaces.Add("xsi", Namespaces.Xsi);
			PolicyDocument newNode = new PolicyDocument(newPolicyDoc);
			mainTree.Nodes.Add(newNode);
			docType = DocumentType.Policy;
			
			newNode.NodeFont = new Font( mainTree.Font, FontStyle.Bold );
			menuItem2.Enabled = false;
			menuItem5.Enabled = false;
			menuItem3.Enabled = true;
			menuItem9.Enabled = true;
			menuItem8.Enabled = true;
			menuItem7.Enabled = false;
		}

		/// <summary>
		/// Creates a new policy for the policy set selected.
		/// </summary>
		/// <param name="sender">The mainTree control.</param>
		/// <param name="args">The arguements for the event.</param>
		private void CreatePolicy( object sender, EventArgs args )
		{
			PolicySet policySetNode = (PolicySet)mainTree.SelectedNode;
			pol.PolicySetElementReadWrite policySet = policySetNode.PolicySetDefinition;

			// Create a new policy
			pol.PolicyElementReadWrite newPolicy = new pol.PolicyElementReadWrite( 
				"urn:newpolicy", "[TODO: add a description]", 
				null,
				new pol.RuleReadWriteCollection(), 
				RuleCombiningAlgorithms.FirstApplicable, 
				new pol.ObligationReadWriteCollection(), 
				string.Empty,
				null,
				null,
				null,
				XacmlVersion.Version11 ); //TODO: check version

			// Add the policy to the policySet.
			policySet.Policies.Add( newPolicy );

			// Create a new node
			Policy policyNode = new Policy( newPolicy );

			// Add the tree node.
			policySetNode.Nodes.Add( policyNode );

			// Set the font so the user knows the item was changed
			policyNode.NodeFont = new Font( mainTree.Font, FontStyle.Bold );
		}

		/// <summary>
		/// Creates a new policy for the policy document selected.
		/// </summary>
		/// <param name="sender">The mainTree control.</param>
		/// <param name="args">The arguements for the event.</param>
		private void CreatePolicyFromDocument( object sender, EventArgs args )
		{
			PolicyDocument policyDocumentNode = (PolicyDocument)mainTree.SelectedNode;
			pol.PolicyDocumentReadWrite policyDocument = policyDocumentNode.PolicyDocumentDefinition;

			// Create a new policy
			pol.PolicyElementReadWrite newPolicy = new pol.PolicyElementReadWrite( 
				"urn:newpolicy", "[TODO: add a description]", null,
				new pol.RuleReadWriteCollection(), 
				RuleCombiningAlgorithms.FirstApplicable, 
				new pol.ObligationReadWriteCollection(), 
				string.Empty,
				null,
				null,
				null,
				XacmlVersion.Version11 ); //TODO: check version

			
			policyDocument.Policy = newPolicy ;

			// Create a new node
			Policy policyNode = new Policy( newPolicy );

			// Add the tree node.
			policyDocumentNode.Nodes.Add( policyNode );

			// Set the font so the user knows the item was changed
			policyNode.NodeFont = new Font( mainTree.Font, FontStyle.Bold );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void CreatePolicySet( object sender, EventArgs args )
		{
			PolicySet policySetNode = (PolicySet)mainTree.SelectedNode;
			pol.PolicySetElementReadWrite policySet = policySetNode.PolicySetDefinition;

			// Create a new policy
			pol.PolicySetElementReadWrite newPolicySet = new pol.PolicySetElementReadWrite( 
				"urn:newpolicy", "[TODO: add a description]", 
				null, 
				new ArrayList(), 
				PolicyCombiningAlgorithms.FirstApplicable, 
				new pol.ObligationReadWriteCollection(), 
				null,
				XacmlVersion.Version11 ); //TODO: check version

			// Add the policy to the policySet.
			policySet.Policies.Add( newPolicySet );

			// Create a new node.
			PolicySet newPolicySetNode = new PolicySet( newPolicySet );

			// Add the tree node.
			policySetNode.Nodes.Add( newPolicySetNode );

			// Set the font so the user knows the item was changed
			newPolicySetNode.NodeFont = new Font( mainTree.Font, FontStyle.Bold );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void CreatePolicySetFromDocument( object sender, EventArgs args )
		{
			PolicyDocument policyDocumentNode = (PolicyDocument)mainTree.SelectedNode;
			pol.PolicyDocumentReadWrite policyDoc = policyDocumentNode.PolicyDocumentDefinition;

			// Create a new policy
			pol.PolicySetElementReadWrite newPolicySet = new pol.PolicySetElementReadWrite( 
				"urn:newpolicy", "[TODO: add a description]", 
				null, 
				new ArrayList(), 
				PolicyCombiningAlgorithms.FirstApplicable, 
				new pol.ObligationReadWriteCollection(), 
				null,
				XacmlVersion.Version11 ); //TODO: check version

			policyDoc.PolicySet = newPolicySet;

			// Create a new node.
			PolicySet newPolicySetNode = new PolicySet( newPolicySet );

			// Add the tree node.
			policyDocumentNode.Nodes.Add( newPolicySetNode );

			// Set the font so the user knows the item was changed
			newPolicySetNode.NodeFont = new Font( mainTree.Font, FontStyle.Bold );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void CreateTarget( object sender, EventArgs args )
		{
			Target newTargetNode = null;
			TreeNode parentNode = null;

			// Create the target
			pol.TargetElementReadWrite target = new pol.TargetElementReadWrite( 
				new pol.ResourcesElementReadWrite( true, new pol.TargetItemReadWriteCollection(), XacmlVersion.Version11 ), //TODO: check version
				new pol.SubjectsElementReadWrite( true, new pol.TargetItemReadWriteCollection(), XacmlVersion.Version11 ), //TODO: check version 
				new pol.ActionsElementReadWrite( true, new pol.TargetItemReadWriteCollection(), XacmlVersion.Version11 ), //TODO: check version
				new pol.EnvironmentsElementReadWrite( true, new pol.TargetItemReadWriteCollection(), XacmlVersion.Version11 ), //TODO: check version
				XacmlVersion.Version11 ); //TODO: check version

			// Create the node
			newTargetNode = new Target( target );

			if( mainTree.SelectedNode is PolicySet )
			{
				parentNode = mainTree.SelectedNode;
				pol.PolicySetElementReadWrite policySet = ((PolicySet)parentNode).PolicySetDefinition;

				// Set the target
				policySet.Target = target;
			}
			else if ( mainTree.SelectedNode is Policy )
			{
				parentNode = mainTree.SelectedNode;
				pol.PolicyElementReadWrite policy = ((Policy)parentNode).PolicyDefinition;

				// Set the target
				policy.Target = target;
			}
			else if ( mainTree.SelectedNode is Rule )
			{
				parentNode = mainTree.SelectedNode;
				pol.RuleElementReadWrite rule = ((Rule)parentNode).RuleDefinition;

				// Set the target
				rule.Target = target;
			}
			else if ( mainTree.SelectedNode is AnyTarget )
			{
				parentNode = mainTree.SelectedNode.Parent;

				// Set the target
				if( parentNode is PolicySet )
				{
					((PolicySet)parentNode).PolicySetDefinition.Target = target;
				}
				else if ( parentNode is Policy )
				{
					((Policy)parentNode).PolicyDefinition.Target = target;
				}
				else if ( parentNode is Rule )
				{
					((Rule)parentNode).RuleDefinition.Target = target;
				}
			}

			if( newTargetNode != null && parentNode != null )
			{
				int idx = -1;
				
				// Search the previous node
				foreach( TreeNode node in parentNode.Nodes )
				{
					if( node is AnyTarget )
					{
						idx = parentNode.Nodes.IndexOf( node );
						break;
					}
				}

				if( idx != -1 )
				{
					// Set the font to the node
					newTargetNode.NodeFont = new Font( mainTree.Font, FontStyle.Bold );

					// Remove the previous target node
					parentNode.Nodes.RemoveAt( idx );

					// Add the node to the node.
					parentNode.Nodes.Insert( idx, newTargetNode );
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void CreateRule( object sender, EventArgs args )
		{
			Policy policyNode = (Policy)mainTree.SelectedNode;
			pol.PolicyElementReadWrite policy = policyNode.PolicyDefinition;

			pol.RuleElementReadWrite rule = new pol.RuleElementReadWrite(
				"urn:new_rule",
				"[TODO: add rule description]",
				null,
				null, 
				pol.Effect.Permit,
				XacmlVersion.Version11 );  //TODO: check version

			policy.Rules.Add( rule );

			Rule ruleNode = new Rule( rule );

			policyNode.Nodes.Add( ruleNode );

			ruleNode.NodeFont = new Font( mainTree.Font, FontStyle.Bold );
		}

		private void CreateObligationsFromPolicy(object sender, EventArgs args )
		{
			Policy policyNode = (Policy)mainTree.SelectedNode;
			pol.PolicyElementReadWrite policy = policyNode.PolicyDefinition;

			pol.ObligationReadWriteCollection obligations = new pol.ObligationReadWriteCollection();  //TODO: check version

			policy.Obligations = obligations;

			Obligations obligationsNode = new Obligations( obligations );

			policyNode.Nodes.Add( obligationsNode );

			obligationsNode.NodeFont = new Font( mainTree.Font, FontStyle.Bold );
		}

		private void CreateObligationsFromPolicySet(object sender, EventArgs args )
		{
			PolicySet policySetNode = (PolicySet)mainTree.SelectedNode;
			pol.PolicySetElementReadWrite policySet = policySetNode.PolicySetDefinition;

			pol.ObligationReadWriteCollection obligations = new pol.ObligationReadWriteCollection();  //TODO: check version

			policySet.Obligations = obligations;

			Obligations obligationsNode = new Obligations( obligations );

			policySetNode.Nodes.Add( obligationsNode );

			obligationsNode.NodeFont = new Font( mainTree.Font, FontStyle.Bold );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void CreateCondition( object sender, EventArgs args )
		{
			Condition newConditionNode = null;
			TreeNode parentNode = null;

			pol.ConditionElementReadWrite condition = new pol.ConditionElementReadWrite( "urn:new_function", new pol.IExpressionReadWriteCollection(), XacmlVersion.Version11 );

			newConditionNode = new Condition( condition );

			parentNode = mainTree.SelectedNode;

			pol.RuleElementReadWrite rule = ((Rule)parentNode).RuleDefinition;

			rule.Condition = condition;


			parentNode.Nodes.Add( newConditionNode );
			/*if( newConditionNode != null && parentNode != null )
			{
				int idx = -1;
				
				// Search the previous node
				foreach( TreeNode node in parentNode.Nodes )
				{
					if( node is TreeNodes.Condition )
					{
						idx = parentNode.Nodes.IndexOf( node );
						break;
					}
				}

				if( idx != -1 )
				{
					// Set the font to the node
					newConditionNode.NodeFont = new Font( mainTree.Font, FontStyle.Bold );

					// Remove the previous condition node
					parentNode.Nodes.RemoveAt( idx );

					// Add the node to the node.
					parentNode.Nodes.Insert( idx, newConditionNode);
				}
			}*/
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void CreateTargetItem( object sender, EventArgs args )
		{
			if( mainTree.SelectedNode is AnySubject )
			{
				AnySubject anyNode = (AnySubject)mainTree.SelectedNode;
				Target targetNode = (Target)anyNode.Parent;

				int idx = targetNode.Nodes.IndexOf( anyNode );
				targetNode.Nodes.RemoveAt( idx );

				pol.TargetMatchReadWriteCollection matchCollection = new pol.TargetMatchReadWriteCollection();
				matchCollection.Add( 
					new pol.SubjectMatchElementReadWrite( 
						InternalFunctions.StringEqual, 
						new pol.AttributeValueElementReadWrite( InternalDataTypes.XsdString, "Somebody", XacmlVersion.Version11 ),  //TODO: check version
						new pol.SubjectAttributeDesignatorElement( InternalDataTypes.XsdString, false, SubjectElement.ActionSubjectId, "", "", XacmlVersion.Version11 ), XacmlVersion.Version11 ) );  //TODO: check version
				pol.SubjectElementReadWrite targetItem = new pol.SubjectElementReadWrite( matchCollection, XacmlVersion.Version11 );  //TODO: check version

				TargetItem targetItemNode = new TargetItem( targetItem );

				targetNode.Nodes.Insert( idx, targetItemNode ); 
				targetNode.TargetDefinition.Subjects.IsAny = false;
				targetNode.TargetDefinition.Subjects.ItemsList.Add( targetItem );
				targetItemNode.NodeFont = new Font( mainTree.Font, FontStyle.Bold );

				mainTree.SelectedNode = targetItemNode;
			}
			else if( mainTree.SelectedNode is AnyAction )
			{
				AnyAction anyActionNode = (AnyAction)mainTree.SelectedNode;
				Target targetNode = (Target)anyActionNode.Parent;

				int idx = targetNode.Nodes.IndexOf( anyActionNode );
				targetNode.Nodes.RemoveAt( idx );

				pol.TargetMatchReadWriteCollection matchCollection = new pol.TargetMatchReadWriteCollection();
				matchCollection.Add( 
					new pol.ActionMatchElementReadWrite( 
						InternalFunctions.StringEqual, 
						new pol.AttributeValueElementReadWrite( InternalDataTypes.XsdString, "DoSomething", XacmlVersion.Version11 ),  //TODO: check version
						new pol.ActionAttributeDesignatorElement( InternalDataTypes.XsdString, false, ActionElement.ActionId, "", XacmlVersion.Version11 ), XacmlVersion.Version11 ) ); //TODO: check version
				pol.ActionElementReadWrite action = new pol.ActionElementReadWrite( matchCollection, XacmlVersion.Version11 ); //TODO: check version

				TargetItem actionNode = new TargetItem( action );

				targetNode.Nodes.Insert( idx, actionNode ); 
				targetNode.TargetDefinition.Actions.IsAny = false;
				targetNode.TargetDefinition.Actions.ItemsList.Add( action );
				actionNode.NodeFont = new Font( mainTree.Font, FontStyle.Bold );

				mainTree.SelectedNode = actionNode;
			}
			else if( mainTree.SelectedNode is AnyResource )
			{
				AnyResource anyNode = (AnyResource)mainTree.SelectedNode;
				Target targetNode = (Target)anyNode.Parent;

				int idx = targetNode.Nodes.IndexOf( anyNode );
				targetNode.Nodes.RemoveAt( idx );

				pol.TargetMatchReadWriteCollection matchCollection = new pol.TargetMatchReadWriteCollection();
				matchCollection.Add( 
					new pol.ResourceMatchElementReadWrite( 
						InternalFunctions.StringEqual, 
						new pol.AttributeValueElementReadWrite( InternalDataTypes.XsdString, "Something", XacmlVersion.Version11 ),  //TODO: check version
						new pol.ResourceAttributeDesignatorElement( InternalDataTypes.XsdString, false, ResourceElement.ResourceId, "", XacmlVersion.Version11 ), XacmlVersion.Version11 ) ); //TODO: check version
				pol.ResourceElementReadWrite targetItem = new pol.ResourceElementReadWrite( matchCollection, XacmlVersion.Version11 ); //TODO: check version

				TargetItem targetItemNode = new TargetItem( targetItem );

				targetNode.Nodes.Insert( idx, targetItemNode ); 
				targetNode.TargetDefinition.Resources.IsAny = false;
				targetNode.TargetDefinition.Resources.ItemsList.Add( targetItem );
				targetItemNode.NodeFont = new Font( mainTree.Font, FontStyle.Bold );

				mainTree.SelectedNode = targetItemNode;
			}			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mainTree_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			// Check if the control have been modified
			if( mainPanel.Controls.Count != 0 )
			{
				if( !(mainPanel.Controls[0] is XmlViewer) )
				{
					BaseControl baseControl = mainPanel.Controls[0] as BaseControl;

					mainTree.SelectedNode.NodeFont = new Font( mainTree.Font, FontStyle.Regular );
					NoBoldNode oNode = null;
					if( baseControl is CustomControls.PolicySet )
					{
						oNode = new PolicySet(((CustomControls.PolicySet)baseControl).PolicySetElement);
					}
					else if( baseControl is CustomControls.PolicySet )
					{
						oNode = new Policy(((CustomControls.Policy)baseControl).PolicyElement);
					}
					else if( baseControl is CustomControls.Rule )
					{
						oNode = new Rule(((CustomControls.Rule)baseControl).RuleElement);
					}
					else if( baseControl is CustomControls.TargetItem )
					{
						pol.TargetItemBaseReadWrite element = ((CustomControls.TargetItem)baseControl).TargetItemBaseElement;
						oNode = new TargetItem(element);
					}
					else if( baseControl is CustomControls.Obligations )
					{
						oNode = new Obligations(((CustomControls.Obligations)baseControl).ObligationsElement);
					}
					else if( baseControl is ContextCustomControls.Attribute )
					{
						oNode = new Attribute( ((ContextCustomControls.Attribute)baseControl).AttributeElement );
					}
					else if( baseControl is ContextCustomControls.Resource )
					{
						oNode = new Resource( ((ContextCustomControls.Resource)baseControl).ResourceElement );
					}
				
					if( oNode != null )
					{
						mainTree.SelectedNode = oNode;
						mainTree.SelectedNode.Text = oNode.Text;
					}
				}
			}
		}

		private void mniDelete_Click(object sender, EventArgs e)
		{
			if(mainTree.SelectedNode != null)
			{
				mainPanel.Controls.Clear();
				if(mainTree.SelectedNode.Parent != null)
				{
					NoBoldNode node = (NoBoldNode)mainTree.SelectedNode.Parent;
					if( node is PolicySet )
					{
						DeleteFromPolicySet( (NoBoldNode)mainTree.SelectedNode );
					}
					else if( node is Policy )
					{
						DeleteFromPolicy( (NoBoldNode)mainTree.SelectedNode );
					}
					else if( node is Target )
					{
						DeleteFromTarget( (TargetItem)mainTree.SelectedNode );
					}
					else if( node is Request )
					{
						DeleteFromRequest( (NoBoldNode)mainTree.SelectedNode );
					}
					if( mainTree.SelectedNode is Attribute )
					{
						DeleteContextAttribute( (NoBoldNode)mainTree.SelectedNode.Parent );
						mainTree.SelectedNode.Remove();
					}
					else
					{
						node.Nodes.Remove( mainTree.SelectedNode );
					}
				}
				else
				{
					menuItem5.Enabled = true;
					menuItem2.Enabled = true;
					menuItem3.Enabled = false;
					menuItem9.Enabled = false;
					mainTree.Nodes.Clear();
				}
			}
		}

		private void DeleteFromRequest( NoBoldNode childNode )
		{
			Request parentNode = (Request)mainTree.SelectedNode.Parent;
			if( childNode is ContextTreeNodes.Action )
			{
				parentNode.RequestDefinition.Action = null;
			}
			else if( childNode is Resource )
			{
				con.ResourceElementReadWrite resource = ((Resource)childNode).ResourceDefinition;
                				
				int index = parentNode.RequestDefinition.Resources.GetIndex( resource );
				parentNode.RequestDefinition.Resources.RemoveAt( index );
			}
			else if( childNode is Subject )
			{
				con.SubjectElementReadWrite subject = ((Subject)childNode).SubjectDefinition;
                				
				int index = parentNode.RequestDefinition.Subjects.GetIndex( subject );
				parentNode.RequestDefinition.Subjects.RemoveAt( index );
			}
		}

		private void DeleteContextAttribute( NoBoldNode parentNode )
		{
			Attribute attributeNode = (Attribute)mainTree.SelectedNode;
			con.AttributeElementReadWrite attribute = attributeNode.AttributeDefinition;

			if( parentNode is ContextTreeNodes.Action )
			{
				con.ActionElementReadWrite action = ((ContextTreeNodes.Action)parentNode).ActionDefinition;

				int index = action.Attributes.GetIndex( attribute );
				action.Attributes.RemoveAt( index );
			}
			else if( parentNode is Resource )
			{
				con.ResourceElementReadWrite resource = ((Resource)parentNode).ResourceDefinition;

				int index = resource.Attributes.GetIndex( attribute );
				resource.Attributes.RemoveAt( index );
			}
			else if( parentNode is Subject )
			{
				con.SubjectElementReadWrite subject = ((Subject)parentNode).SubjectDefinition;

				int index = subject.Attributes.GetIndex( attribute );
				subject.Attributes.RemoveAt( index );
			}
		}

		private void DeleteFromTarget( TargetItem childNode )
		{
			Target parentNode = (Target)mainTree.SelectedNode.Parent;

			pol.TargetItemBaseReadWrite element = childNode.TargetItemDefinition;

			if( element is pol.ActionElementReadWrite )
			{
				AnyAction anyAction = new AnyAction();
				parentNode.Nodes.Add( anyAction );
				parentNode.TargetDefinition.Actions.ItemsList = null;
				parentNode.TargetDefinition.Actions.IsAny = true;
			}
			else if( element is pol.ResourceElementReadWrite )
			{
				AnyResource anyResource = new AnyResource();
				parentNode.Nodes.Add( anyResource );
				parentNode.TargetDefinition.Resources.ItemsList = null;
				parentNode.TargetDefinition.Resources.IsAny = true;
			}
			else if( element is pol.SubjectElementReadWrite )
			{
				AnySubject anySubject = new AnySubject();
				parentNode.Nodes.Add( anySubject );
				parentNode.TargetDefinition.Subjects.ItemsList = null;
				parentNode.TargetDefinition.Subjects.IsAny = true;
			}
		}
		private void DeleteFromPolicySet( NoBoldNode childNode )
		{
			PolicySet parentNode = (PolicySet)mainTree.SelectedNode.Parent;
			if( childNode is Policy )
			{
				Policy policyNode = (Policy)childNode;
				parentNode.PolicySetDefinition.Policies.Remove( policyNode.PolicyDefinition );
			}
			else if( childNode is PolicySet )
			{
				PolicySet policySetNode = (PolicySet)childNode;
				parentNode.PolicySetDefinition.Policies.Remove( policySetNode.PolicySetDefinition );
			}
			else if( childNode is Obligations )
			{
				parentNode.PolicySetDefinition.Obligations = null;
			}
			else if( childNode is Target )
			{
				parentNode.PolicySetDefinition.Target = null;
			}
		}

		private void DeleteFromPolicy( NoBoldNode childNode )
		{
			Policy parentNode = (Policy)mainTree.SelectedNode.Parent;
			if( childNode is Rule )
			{
				pol.RuleElementReadWrite rule = ((Rule)childNode).RuleDefinition;
				int index = parentNode.PolicyDefinition.Rules.GetIndex( rule );
				parentNode.PolicyDefinition.Rules.RemoveAt( index );
			}
			else if( childNode is Obligations )
			{
				parentNode.PolicyDefinition.Obligations = null;
			}
			else if( childNode is Target )
			{
				parentNode.PolicyDefinition.Target = null;
			}
		}

		private void DeleteFromRule( NoBoldNode childNode )
		{
			Policy parentNode = (Policy)mainTree.SelectedNode.Parent;
			if( childNode is Rule )
			{
				parentNode.PolicyDefinition.Rules.RemoveAt( childNode.Index - 1 );
			}
			else if( childNode is Obligations )
			{
				parentNode.PolicyDefinition.Obligations = null;
			}
			else if( childNode is Target )
			{
				parentNode.PolicyDefinition.Target = null;
			}
		}

		private void menuItem3_Click(object sender, EventArgs e)
		{
			if( saveFileDialog.ShowDialog() == DialogResult.OK )
			{
				XmlTextWriter writer = new XmlTextWriter( saveFileDialog.FileName,Encoding.UTF8 );
				writer.Namespaces = true;
				writer.Formatting = Formatting.Indented;

				if( docType == DocumentType.Request )
				{
					con.ContextDocumentReadWrite oCon = ((Context)mainTree.TopNode).ContextDefinition;
					oCon.WriteRequestDocument(writer);
				}
				else if( docType == DocumentType.Policy )
				{
					pol.PolicyDocumentReadWrite oPol = ((PolicyDocument)mainTree.TopNode).PolicyDocumentDefinition;
					oPol.WriteDocument(writer);
				}

				writer.Close();
			}
		}

		private void menuItem4_Click(object sender, EventArgs e)
		{
			mainTree.Nodes.Clear();
			mainPanel.Controls.Clear();
			_path = string.Empty;
			menuItem2.Enabled = true;
			menuItem5.Enabled = true;
			menuItem3.Enabled = false;
			menuItem9.Enabled = false;
			menuItem8.Enabled = false;
			menuItem7.Enabled = false;
		}

		private void menuItem5_Click(object sender, EventArgs e)
		{
			openFileDialog.Filter = "Request Files|*.xml|All Files|*.*";
			if( openFileDialog.ShowDialog() == DialogResult.OK )
			{
				Stream stream = openFileDialog.OpenFile();
				_path = openFileDialog.FileName;
				docType = DocumentType.Request;
				con.ContextDocumentReadWrite doc = ContextLoader.LoadContextDocument( stream, XacmlVersion.Version11, DocumentAccess.ReadWrite );
				mainTree.Nodes.Add( new Context( doc ) );
				menuItem3.Enabled = true;
				menuItem9.Enabled = true;
				menuItem5.Enabled = false;
				menuItem2.Enabled = false;
				menuItem7.Enabled = true;
				menuItem8.Enabled = false;
				stream.Close();
			}
		}

		private void menuItem7_Click(object sender, EventArgs e)
		{
			if( MessageBox.Show( this,"The request will be saved. Do you want to proceed?", "Warning", MessageBoxButtons.YesNo ) == DialogResult.Yes )
			{
				//Loads the policy
				openFileDialog.Filter = "Policy Files|*.xml|All Files|*.*";
				if( openFileDialog.ShowDialog() == DialogResult.OK )
				{
					menuItem9_Click( sender, e );
					pol.PolicyDocumentReadWrite oPol = PolicyLoader.LoadPolicyDocument( openFileDialog.OpenFile(), XacmlVersion.Version11 );
					//Gets the context from the TreeView
					Stream stream = new FileStream( _path, FileMode.Open );
					con.ContextDocumentReadWrite oCon = ContextLoader.LoadContextDocument( stream , XacmlVersion.Version11 );
				
					stream.Close();

					//Evaluates the request
					EvaluationEngine engine = new EvaluationEngine();
					con.ResponseElement res = engine.Evaluate( (pol.PolicyDocument)oPol, (con.ContextDocument)oCon );
				
					mainPanel.Controls.Clear();
					//Creates the xml
					string path = Path.GetTempFileName();
					XmlWriter writer = new XmlTextWriter( path, Encoding.UTF8 );
					res.WriteDocument( writer );
					writer.Close();

					mainPanel.Controls.Add( new XmlViewer( path, ResponseElement.Response ) );
				}
			}
		}

		private void menuItem8_Click(object sender, EventArgs e)
		{
			if( MessageBox.Show( this,"The policy will be saved. Do you want to proceed?", "Warning", MessageBoxButtons.YesNo ) == DialogResult.Yes )
			{
				//Loads the request
				openFileDialog.Filter = "Request Files|*.xml|All Files|*.*";
				if( openFileDialog.ShowDialog() == DialogResult.OK )
				{
					menuItem9_Click( sender, e );
					con.ContextDocumentReadWrite oCon = ContextLoader.LoadContextDocument( openFileDialog.OpenFile(), XacmlVersion.Version11 );
					//Gets the policy from the TreeView
					Stream stream = new FileStream( _path, FileMode.Open );
					pol.PolicyDocumentReadWrite oPol = PolicyLoader.LoadPolicyDocument( stream , XacmlVersion.Version20 );
				
					stream.Close();

					//Evaluates the request
					EvaluationEngine engine = new EvaluationEngine();
					con.ResponseElement res = engine.Evaluate( (pol.PolicyDocument)oPol, (con.ContextDocument)oCon );

					//Creates the xml
					string path = Path.GetTempFileName();
					XmlWriter writer = new XmlTextWriter( path, Encoding.UTF8 );
					res.WriteDocument( writer );
					writer.Close();
				
					mainPanel.Controls.Clear();

					mainPanel.Controls.Add( new XmlViewer( path, ResponseElement.Response ) );
				}
			}
		}

		private void menuItem9_Click(object sender, EventArgs e)
		{
			XmlTextWriter writer = new XmlTextWriter( _path,Encoding.UTF8 );
			writer.Namespaces = true;
			writer.Formatting = Formatting.Indented;

			if( docType == DocumentType.Request )
			{
				con.ContextDocumentReadWrite oCon = ((Context)mainTree.TopNode).ContextDefinition;
				oCon.WriteRequestDocument(writer);
			}
			else if( docType == DocumentType.Policy )
			{
				pol.PolicyDocumentReadWrite oPol = ((PolicyDocument)mainTree.TopNode).PolicyDocumentDefinition;
				oPol.WriteDocument(writer);
			}

			writer.Close();		
		}
	}
}
