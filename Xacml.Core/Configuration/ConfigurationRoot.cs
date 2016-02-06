using System;
using System.Collections;
using System.Configuration;
using System.Xml;
using Xacml.Core.Interfaces;

namespace Xacml.Core.Configuration
{
	/// <summary>
	/// Main configuration class used to mantain all the configuration placed in the configuration section.
	/// </summary>
	public class ConfigurationRoot
	{
		#region Static members

		/// <summary>
		/// Static property used to reload the configuration.
		/// </summary>
		public static ConfigurationRoot Config
		{
			get
			{ 
#if NET10
				return (ConfigurationRoot)ConfigurationSettings.GetConfig( "Xacml.net" );
#endif
#if NET20
				return (ConfigurationRoot)ConfigurationManager.GetSection( "Xacml.net" );
#endif
			}
		}

		#endregion

		#region Private members

		/// <summary>
		/// The configured attribute repositories.
		/// </summary>
		private ArrayList _attributeRepositories = new ArrayList();

		/// <summary>
		/// The configured policy repositories.
		/// </summary>
		private ArrayList _policyRepositories = new ArrayList();

		/// <summary>
		/// The configured function repositories.
		/// </summary>
		private ArrayList _functionRepositories = new ArrayList();

		/// <summary>
		/// The configured data type repositories.
		/// </summary>
		private ArrayList _dataTypeRepositories = new ArrayList();

		/// <summary>
		/// The configured rule combinig algorothm repositories.
		/// </summary>
		private ArrayList _ruleCombiningRepositories = new ArrayList();

		/// <summary>
		/// The configured policy combinig algorothm repositories.
		/// </summary>
		private ArrayList _policyCombiningRepositories = new ArrayList();

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance of the configuration class using the XmlNode specified.
		/// </summary>
		/// <param name="configNode">The XmlNode for the configuration section.</param>
		public ConfigurationRoot( XmlNode configNode )
		{
            if (configNode == null) throw new ArgumentNullException("configNode");

			// Load all attribute repositories
			XmlNodeList nodeList = configNode.SelectNodes( "./attributeRepositories/repository" );
			foreach( XmlNode node in nodeList )
			{
				AttributeRepositoryConfig repConfig = new AttributeRepositoryConfig( node );
				
				IAttributeRepository rep = (IAttributeRepository)Activator.CreateInstance( repConfig.Type );
				rep.Init( repConfig.XmlNode );

				_attributeRepositories.Add( rep );
			}

			// Load all policy repositories
			nodeList = configNode.SelectNodes( "./policyRepositories/repository" );
			foreach( XmlNode node in nodeList )
			{
				PolicyRepositoryConfig policyConfig = new PolicyRepositoryConfig( node );

				IPolicyRepository rep = (IPolicyRepository)Activator.CreateInstance( policyConfig.Type );
				rep.Init( policyConfig.XmlNode );

				_policyRepositories.Add( rep );
			}

			// Load all function repositories
			nodeList = configNode.SelectNodes( "./functionRepositories/repository" );
			foreach( XmlNode node in nodeList )
			{
				FunctionRepositoryConfig functionConfig = new FunctionRepositoryConfig( node );

				IFunctionRepository rep = (IFunctionRepository)Activator.CreateInstance( functionConfig.Type );
				rep.Init( functionConfig.XmlNode );

				_functionRepositories.Add( rep );
			}
			
			// Load all dataType repositories
			nodeList = configNode.SelectNodes( "./dataTypeRepositories/repository" );
			foreach( XmlNode node in nodeList )
			{
				DataTypeRepositoryConfig dataTypeConfig = new DataTypeRepositoryConfig( node );

				IDataTypeRepository rep = (IDataTypeRepository)Activator.CreateInstance( dataTypeConfig.Type );
				rep.Init( dataTypeConfig.XmlNode );

				_dataTypeRepositories.Add( rep );
			}

			// Load all rule combinig algorothm repositories
			nodeList = configNode.SelectNodes( "./ruleCombiningAlgorithmRepositories/repository" );
			foreach( XmlNode node in nodeList )
			{
				RuleCombiningAlgorithmRepository ruleCAConfig = new RuleCombiningAlgorithmRepository( node );

				IRuleCombiningAlgorithmRepository rep = (IRuleCombiningAlgorithmRepository)Activator.CreateInstance( ruleCAConfig.Type );
				rep.Init( ruleCAConfig.XmlNode );

				_ruleCombiningRepositories.Add( rep );
			}

			// Load all policy combinig algorothm repositories
			nodeList = configNode.SelectNodes( "./policyCombiningAlgorithmRepositories/repository" );
			foreach( XmlNode node in nodeList )
			{
				PolicyCombiningAlgorithmRepository policyCAConfig = new PolicyCombiningAlgorithmRepository( node );

				IPolicyCombiningAlgorithmRepository rep = (IPolicyCombiningAlgorithmRepository)Activator.CreateInstance( policyCAConfig.Type );
				rep.Init( policyCAConfig.XmlNode );

				_policyCombiningRepositories.Add( rep );
			}

			// Load all rule combinig algorothm repositories
			nodeList = configNode.SelectNodes( "./ruleCombiningAlgorithmRepositories/repository" );
			foreach( XmlNode node in nodeList )
			{
				_ruleCombiningRepositories.Add( new RuleCombiningAlgorithmRepository( node ) );
			}

		}

		#endregion

		#region Public properties

		/// <summary>
		/// The attribute repositories loaded.
		/// </summary>
		public ArrayList AttributeRepositories
		{
			get{ return _attributeRepositories; }
		}

		/// <summary>
		/// The policy repositories loaded.
		/// </summary>
		public ArrayList PolicyRepositories
		{
			get{ return _policyRepositories; }
		}

		/// <summary>
		/// The function repositories loaded.
		/// </summary>
		public ArrayList FunctionRepositories
		{
			get{ return _functionRepositories; }
		}

		/// <summary>
		/// The datatype repositories loaded.
		/// </summary>
		public ArrayList DataTypeRepositories
		{
			get{ return _dataTypeRepositories; }
		}

		/// <summary>
		/// The pca repositories loaded.
		/// </summary>
		public ArrayList PolicyCombiningAlgorithmRepositories
		{
			get{ return _policyCombiningRepositories; }
		}

		/// <summary>
		/// The rca repositories loaded.
		/// </summary>
		public ArrayList RuleCombiningAlgorithmRepositories
		{
			get{ return _ruleCombiningRepositories; }
		}
		
		#endregion
	}
}
