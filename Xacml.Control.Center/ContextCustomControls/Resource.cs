using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using Xacml.Control.Center.CustomControls;
using Xacml.Core;
using con = Xacml.Core.Context;

namespace Xacml.Control.Center.ContextCustomControls
{
	/// <summary>
	/// Summary description for Resource.
	/// </summary>
	public class Resource : BaseControl
	{
		private GroupBox grpResourceContent;
		private con.ResourceElementReadWrite _resource;
		private TextBox txtContent;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="resource"></param>
		public Resource( con.ResourceElementReadWrite resource )
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			_resource = resource;
						
			if( _resource.ResourceContent != null )
			{
				txtContent.Text = _resource.ResourceContent.XmlDocument.InnerXml;
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
			this.grpResourceContent = new System.Windows.Forms.GroupBox();
			this.txtContent = new System.Windows.Forms.TextBox();
			this.grpResourceContent.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpResourceContent
			// 
			this.grpResourceContent.Controls.Add(this.txtContent);
			this.grpResourceContent.Location = new System.Drawing.Point(8, 8);
			this.grpResourceContent.Name = "grpResourceContent";
			this.grpResourceContent.Size = new System.Drawing.Size(520, 400);
			this.grpResourceContent.TabIndex = 0;
			this.grpResourceContent.TabStop = false;
			this.grpResourceContent.Text = "Resource content";
			// 
			// txtContent
			// 
			this.txtContent.Location = new System.Drawing.Point(16, 24);
			this.txtContent.Multiline = true;
			this.txtContent.Name = "txtContent";
			this.txtContent.Size = new System.Drawing.Size(488, 360);
			this.txtContent.TabIndex = 0;
			this.txtContent.Text = "";
			// 
			// Resource
			// 
			this.Controls.Add(this.grpResourceContent);
			this.Name = "Resource";
			this.Size = new System.Drawing.Size(536, 416);
			this.grpResourceContent.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		/// <summary>
		/// 
		/// </summary>
		public con.ResourceElementReadWrite ResourceElement
		{
			get
			{
				XmlDocument content = new XmlDocument();
				if( txtContent.Text != string.Empty )
				{
					try
					{
						XmlDocument asd = new XmlDocument();
						asd.InnerXml = txtContent.Text;
					}
					catch
					{
						throw new XmlException("The Xml is not well-formed");
					}
					content.InnerXml = txtContent.Text;
				}
				_resource.ResourceContent = new con.ResourceContentElementReadWrite(content,XacmlVersion.Version11);

				return _resource;
			}
		}
	}
}
