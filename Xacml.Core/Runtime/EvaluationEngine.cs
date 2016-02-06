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
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using Xacml.ContextSchema;
using Xacml.Core.Configuration;
using Xacml.Core.Runtime.Functions;
using Xacml.Core.Runtime.Functions.DateTimeDataType;
using Xacml.PolicySchema1;
using ctx = Xacml.Core.Context;
using pol = Xacml.Core.Policy;
using typ = Xacml.Core.Runtime.DataTypes;
using inf = Xacml.Core.Interfaces;
using cor = Xacml.Core;
using ResourceElement = Xacml.ContextSchema.ResourceElement;

namespace Xacml.Core.Runtime
{
	/// <summary>
	/// The EvaluationEngine is the PDP implementation which receives a policy document and a 
	/// context document and perform the evaluation of the policies. This instance is safe to be 
	/// reused but not safe for multithread operations. If multiple operations must be carried on
	/// a new instance must be created or the code must use a single instance per thread.
	/// </summary>
	public class EvaluationEngine
	{
		#region Private members

		/// <summary>
		/// The verbose information for this instance.
		/// </summary>
		private bool _verbose = true;

		/// <summary>
		/// All the internal functions.
		/// </summary>
		private static IDictionary _functions;

		/// <summary>
		/// All the internal functions.
		/// </summary>
		private static IDictionary _dataTypes;

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a new instance of the EvaluationEngine with the tracing disabled.
		/// </summary>
		public EvaluationEngine()
		{
			Prepare();
		}

		/// <summary>
		/// Creates a new instance of the EvaluationEngine with the tracing information.
		/// </summary>
		/// <param name="verbose">Whether the trace will be sent to the console or not.</param>
		public EvaluationEngine( bool verbose )
			: this()
		{
			_verbose = verbose;
		} 

		#endregion

		#region Public methods

		/// <summary>
		/// Static method used to get the internal data type description using the data type id.
		/// </summary>
		/// <param name="typeId">The data type Id.</param>
		/// <returns>The data type descriptor.</returns>
		public static inf.IDataType GetDataType( string typeId )
		{
			inf.IDataType dataType = _dataTypes[ typeId ] as inf.IDataType;
			if( dataType == null )
			{
				if( ConfigurationRoot.Config != null )
				{
					foreach( inf.IDataTypeRepository rep in ConfigurationRoot.Config.DataTypeRepositories )
					{
						dataType = rep.GetDataType( typeId );
						if( dataType != null )
						{
							return dataType;
						}
					}
				}
			}
			return dataType;
		}

		/// <summary>
		/// The factory creates a new instance of the PolicyCombiningAlgorithm.
		/// </summary>
		/// <param name="policyCombiningAlgorithmId">The name of the policy combining algorithm.</param>
		/// <returns>A new instance of the policy combining algorithm.</returns>
		public static inf.IPolicyCombiningAlgorithm CreatePolicyCombiningAlgorithm( string policyCombiningAlgorithmId )
		{
			switch( policyCombiningAlgorithmId )
			{
				case PolicyCombiningAlgorithms.DenyOverrides:
					return new PolicyCombiningAlgorithmDenyOverrides();
				case PolicyCombiningAlgorithms.PermitOverrides:
					return new PolicyCombiningAlgorithmPermitOverrides();
				case PolicyCombiningAlgorithms.FirstApplicable:
					return new PolicyCombiningAlgorithmFirstApplicable();
				case PolicyCombiningAlgorithms.OnlyOneApplicable:
					return new PolicyCombiningAlgorithmOnlyOneApplicable();
				case PolicyCombiningAlgorithms.OrderedDenyOverrides:
					return new PolicyCombiningAlgorithmOrderedDenyOverrides();
				case PolicyCombiningAlgorithms.OrderedPermitOverrides:
					return new PolicyCombiningAlgorithmOrderedPermitOverrides();
				default:
				{
					if( ConfigurationRoot.Config != null )
					{
						foreach( inf.IPolicyCombiningAlgorithmRepository rep in ConfigurationRoot.Config.PolicyCombiningAlgorithmRepositories )
						{
							inf.IPolicyCombiningAlgorithm pca = rep.GetPolicyCombiningAlgorithm( policyCombiningAlgorithmId );
							if( pca != null )
							{
								return pca;
							}
						}
					}
					return null;
				}
			}
		}

		/// <summary>
		/// The factory creates a new instance of the derived classes.
		/// </summary>
		/// <param name="ruleCombiningAlgorithmId">The name of the rule combining algorithm.</param>
		/// <returns>A new instance of the rule combinig algorithm.</returns>
		public static inf.IRuleCombiningAlgorithm CreateRuleCombiningAlgorithm( string ruleCombiningAlgorithmId )
		{
			switch( ruleCombiningAlgorithmId )
			{
				case RuleCombiningAlgorithms.DenyOverrides:
					return new RuleCombiningAlgorithmDenyOverrides();
				case RuleCombiningAlgorithms.PermitOverrides:
					return new RuleCombiningAlgorithmPermitOverrides();
				case RuleCombiningAlgorithms.FirstApplicable:
					return new RuleCombiningAlgorithmFirstApplicable();
				case RuleCombiningAlgorithms.OrderedDenyOverrides:
					return new RuleCombiningAlgorithmOrderedDenyOverrides();
				case RuleCombiningAlgorithms.OrderedPermitOverrides:
					return new RuleCombiningAlgorithmOrderedPermitOverrides();
				default:
				{
					if( ConfigurationRoot.Config != null )
					{
						foreach( inf.IRuleCombiningAlgorithmRepository rep in ConfigurationRoot.Config.RuleCombiningAlgorithmRepositories )
						{
							inf.IRuleCombiningAlgorithm rca = rep.GetRuleCombiningAlgorithm( ruleCombiningAlgorithmId );
							if( rca != null )
							{
								return rca;
							}
						}
					}
					return null;
				}
			}
		}

		/// <summary>
		/// Evaluate the context document using a specified policy
		/// </summary>
		/// <param name="contextDocument">The context document instance</param>
		/// <returns>The response document.</returns>
		public ctx.ResponseElement Evaluate( ctx.ContextDocument contextDocument )
		{
			EvaluationContext context = new EvaluationContext( this, null, contextDocument );

			try
			{
				// Validates the configuration file was found.
				if( ConfigurationRoot.Config != null )
				{
					// Search all the policies repositories to find a policy that matches the 
					// context document
					pol.PolicyDocument policyDocument = null;
					foreach( inf.IPolicyRepository policyRep in ConfigurationRoot.Config.PolicyRepositories )
					{
						if( policyDocument == null )
						{
							policyDocument = policyRep.Match( context );
						}
						else
						{
							throw new EvaluationException( Resource.ResourceManager[ Resource.MessageKey.exc_duplicated_policy_in_repository ] );
						}
					}

					// If the policy was found evaluate the context document, otherwise use the
					// Evaluate method to generate a Response context document.
					if( policyDocument != null )
					{
						return Evaluate( policyDocument, contextDocument );
					}
					else
					{
						return Evaluate( (pol.PolicyDocument)null, null );
					}
				}
				else
				{
					throw new EvaluationException( Resource.ResourceManager[ Resource.MessageKey.exc_configuration_file_not_found ] );
				}
			}
			catch( EvaluationException e )
			{
				context.Trace( Resource.TRACE_ERROR, e.Message ); 
			}
			return Evaluate( (pol.PolicyDocument)null, null );
		}

		/// <summary>
		/// Evaluate the context document using a specified policy.
		/// </summary>
		/// <param name="policyDocument">The policy instance.</param>
		/// <param name="contextDocument">The context document instance.</param>
		/// <param name="schemaVersion">The version of the schema used to validate the document.</param>
		/// <returns>The response document</returns>
		public ctx.ResponseElement Evaluate( string policyDocument, string contextDocument, XacmlVersion schemaVersion )
		{
			return Evaluate(
				(pol.PolicyDocument)PolicyLoader.LoadPolicyDocument( new FileStream( policyDocument, FileMode.Open ), schemaVersion, DocumentAccess.ReadOnly ), 
				(ctx.ContextDocument)ContextLoader.LoadContextDocument( new FileStream( contextDocument, FileMode.Open ), schemaVersion ) );
		}

		/// <summary>
		/// Evaluate the context document using a specified policy
		/// </summary>
		/// <param name="policyDocument">The policy instance</param>
		/// <param name="contextDocument">The context document instance</param>
		/// <returns>The response document</returns>
		public ctx.ResponseElement Evaluate( pol.PolicyDocument policyDocument, ctx.ContextDocument contextDocument )
		{
            if (policyDocument == null) throw new ArgumentNullException("policyDocument");
            if (contextDocument == null) throw new ArgumentNullException("contextDocument");
			EvaluationContext context = new EvaluationContext( this, policyDocument, (ctx.ContextDocument)contextDocument ) ;

			context.Trace( "Start evaluation" );
			context.AddIndent();

			if( policyDocument == null || contextDocument == null )
			{
				// If a validation error was found a response is created with the syntax error message
				ctx.ResponseElement response =
					new ctx.ResponseElement( 
						new ctx.ResultElement[] {
							new ctx.ResultElement( 
								null, 
								Decision.Indeterminate, 
								new ctx.StatusElement( 
									new ctx.StatusCodeElement( StatusCodes.ProcessingError, null, policyDocument.Version ), 
									null, 
									null, policyDocument.Version ), 
									null, policyDocument.Version ) }, 
					policyDocument.Version );
				return response;
			}

			// Check if both documents are valid
			if( !policyDocument.IsValidDocument || !contextDocument.IsValidDocument )
			{
				// If a validation error was found a response is created with the syntax error message
				ctx.ResponseElement response =
					new ctx.ResponseElement( 
						new ctx.ResultElement[] {
							new ctx.ResultElement( 
								null, 
								Decision.Indeterminate, 
								new ctx.StatusElement( 
									new ctx.StatusCodeElement( StatusCodes.SyntaxError, null, policyDocument.Version ), 
									null, 
									null, policyDocument.Version ), 
								null, policyDocument.Version ) }, 
					policyDocument.Version );
				return response;
			}

			// Create a new response
			contextDocument.Response = new ctx.ResponseElement( (ctx.ResultElement[])null, policyDocument.Version );

			try
			{
				// Create the evaluable policy intance
				IMatchEvaluable policy = null;
				if( policyDocument.PolicySet != null )
				{
					policy = new PolicySet( this, (pol.PolicySetElement)policyDocument.PolicySet );
				}
				else if( policyDocument.Policy != null )
				{
					policy = new Policy( (pol.PolicyElement)policyDocument.Policy );
				}

				// Evaluate the policy or policy set
				if( policy != null )
				{
					// Creates the evaluable policy set
					if( policy.AllResources.Count == 0 )
					{
						policy.AllResources.Add( "" );
					}

					string requestedResourceString = String.Empty;
					Uri requestedResource = null;

					foreach( ctx.ResourceElement resource in contextDocument.Request.Resources )
					{
						// Keep the requested resource
						if( resource.IsHierarchical )
						{
							foreach( ctx.AttributeElement attribute in resource.Attributes )
							{
								if( attribute.AttributeId == ResourceElement.ResourceId )
								{
									if( context.PolicyDocument.Version == XacmlVersion.Version10 || 
										context.PolicyDocument.Version == XacmlVersion.Version11 )
									{
										requestedResourceString = attribute.AttributeValues[0].Contents;
									}
									else
									{
										if( attribute.AttributeValues.Count > 1 )
										{
											throw new NotSupportedException( "resources contains a bag of values" );
										}
										requestedResourceString = attribute.AttributeValues[0].Contents;
									}
								}
							}
							if( !string.IsNullOrEmpty(requestedResourceString) )
							{
								requestedResource = new Uri( requestedResourceString );
							}
						}

						// Iterate through the policy resources evaluating each resource in the context document request 
					    foreach( string resourceName in policy.AllResources )
						{
						    bool mustEvaluate;
						    if( resource.IsHierarchical )
							{
								//Validate if the resource is hierarchically desdendant or children 
								//of the requested resource
								Uri policyResource = new Uri( resourceName );

								if( !(mustEvaluate = requestedResource.Equals( policyResource ) ) )
								{
									// Perform the hierarchical evaluation
									if( resource.ResourceScopeValue == ctx.ResourceScope.Children )
									{
										mustEvaluate = typ.AnyUri.IsChildrenOf( requestedResource, policyResource );
									}
									else if( resource.ResourceScopeValue == ctx.ResourceScope.Descendants )
									{
										mustEvaluate = typ.AnyUri.IsDescendantOf( requestedResource, policyResource );
									}
								}

								if( mustEvaluate )
								{
									foreach( ctx.AttributeElementReadWrite attribute in context.CurrentResource.Attributes )
									{
										if( attribute.AttributeId == ResourceElement.ResourceId )
										{
											attribute.AttributeValues[0].Contents = resourceName;
											break;
										}
									}
								}
							}
							else
							{
								context.CurrentResource = resource;
								mustEvaluate = true;
							}

							if( mustEvaluate )
							{
								// Evaluates the policy set
								Decision decision = policy.Evaluate( context );

								// Create a status code using the policy execution state
								ctx.StatusCodeElement scode;
								if( context.IsMissingAttribute )
								{
									scode = new ctx.StatusCodeElement( 
										StatusCodes.MissingAttribute, null, policyDocument.Version );
								}
								else if( context.ProcessingError )
								{
									scode = new ctx.StatusCodeElement( 
										StatusCodes.ProcessingError, null, policyDocument.Version );
								}
								else
								{
									scode = new ctx.StatusCodeElement( 
										StatusCodes.OK, null, policyDocument.Version );
								}

								//Stop the iteration if there is not a hierarchical request
								if( !resource.IsHierarchical )
								{
									// Ussually when a single resource is requested the ResourceId is not specified in the result
									IObligationsContainer oblig = policy as IObligationsContainer;
									contextDocument.Response.Results.Add( 
										new ctx.ResultElement( "", decision, 
											new ctx.StatusElement( scode, "", "", policyDocument.Version ), oblig.Obligations, policyDocument.Version ) );
									break;
								}
								else
								{
									// Adding a resource for each requested resource, using the resourceName as the resourceId of the result
									IObligationsContainer oblig = policy as IObligationsContainer;
									contextDocument.Response.Results.Add( 
										new ctx.ResultElement( resourceName, decision, 
											new ctx.StatusElement( scode, "", "", policyDocument.Version ), oblig.Obligations, policyDocument.Version ) );
								}
							} // if( mustEvaluate )
						} // foreach( string resourceName in policy.AllResources )
					}
				} //if( policy != null )
			}
			catch( EvaluationException e )
			{
				// If a validation error was found a response is created with the syntax error message
				contextDocument.Response =
					new ctx.ResponseElement( 
						new ctx.ResultElement[] {
							new ctx.ResultElement( 
								null, 
								Decision.Indeterminate, 
								new ctx.StatusElement( 
									new ctx.StatusCodeElement( StatusCodes.ProcessingError, null, policyDocument.Version ), 
									e.Message, 
									e.StackTrace, policyDocument.Version ), 
								null, policyDocument.Version ) }, 
					policyDocument.Version );
			}

			return contextDocument.Response;
		}


		/// <summary>
		/// Get the function for the id specified
		/// </summary>
		/// <param name="functionId">The function id</param>
		/// <returns>The function from the inner functions repository.</returns>
		public static inf.IFunction GetFunction( string functionId )
		{
			// Search in the internal function list.
			inf.IFunction fun = (inf.IFunction)_functions[ functionId ];
			if( fun != null )
			{
				return fun;
			}
			
			// Search the repositories.
			foreach( inf.IFunctionRepository rep in ConfigurationRoot.Config.FunctionRepositories )
			{
				fun = rep.GetFunction( functionId );
				if( fun != null )
				{
					return fun;
				}
			}

			// Function not found
			return null;
		}

		/// <summary>
		/// Resolves the AttributeSelector in the context document using the XPath sentence.
		/// </summary>
		/// <param name="context">The evaluation context instance.</param>
		/// <param name="attributeSelector">The attribute selector.</param>
		/// <returns>A bag of values with the contents of the node.</returns>
		public static BagValue Resolve( EvaluationContext context, pol.AttributeSelectorElement attributeSelector )
		{
			BagValue bagValue = new BagValue( GetDataType( attributeSelector.DataType ) );
			ctx.ResourceContentElement content = (ctx.ResourceContentElement)context.CurrentResource.ResourceContent;
			if( content != null )
			{
				XmlDocument doc = context.ContextDocument.XmlDocument;
				if( context.ContextDocument.XmlNamespaceManager == null )
				{
					context.ContextDocument.AddNamespaces( context.PolicyDocument.Namespaces );
				}
				try
				{
					string xpath = attributeSelector.RequestContextPath;
					XmlNodeList nodeList = doc.DocumentElement.SelectNodes( xpath, context.ContextDocument.XmlNamespaceManager );
					if( nodeList != null )
					{
						foreach( XmlNode node in nodeList )
						{
							pol.AttributeValueElement ave = 
								new pol.AttributeValueElement( 
								attributeSelector.DataType, 
								node.InnerText,
								attributeSelector.SchemaVersion );
							bagValue.Add( ave );
						}
					}
				}
				catch( XPathException e )
				{
					context.Trace( Resource.TRACE_ERROR, e.Message );
					bagValue = new BagValue( GetDataType( attributeSelector.DataType ) );
				}
			}
			return bagValue;
		}

		/// <summary>
		/// Resolves the PolicySetReferenceId using the policy repository
		/// </summary>
		/// <param name="policyReference">The policySet reference</param>
		/// <returns>The policySet found</returns>
		public static pol.PolicySetElement Resolve( pol.PolicySetIdReferenceElement policyReference )
		{
			if( ConfigurationRoot.Config != null )
			{
				// Search for attributes in the configured repositories
				foreach( inf.IPolicyRepository repository in ConfigurationRoot.Config.PolicyRepositories )
				{
					pol.PolicySetElement policySet = repository.GetPolicySet( policyReference );
					if( policySet != null )
					{
						return policySet;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Resolves the PolicyReferenceId using the policy repository
		/// </summary>
		/// <param name="policyReference">The policy reference</param>
		/// <returns>The policy found</returns>
		public static pol.PolicyElement Resolve( pol.PolicyIdReferenceElement policyReference )
		{
			if( ConfigurationRoot.Config != null )
			{
				// Search for attributes in the configured repositories
				foreach( inf.IPolicyRepository repository in ConfigurationRoot.Config.PolicyRepositories )
				{
					pol.PolicyElement policy = repository.GetPolicy( policyReference );
					if( policy != null )
					{
						return policy;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Solves the attribute designator in the context document using the attribute designator type
		/// </summary>
		/// <param name="context">The evaluation context instance.</param>
		/// <param name="attributeDesignator">The attribute designator to resolve</param>
		/// <returns>A bag value with the values found in the context document</returns>
		public static BagValue Resolve( EvaluationContext context, pol.AttributeDesignatorBase attributeDesignator )
		{
			if( attributeDesignator is pol.SubjectAttributeDesignatorElement )
			{
				if( context.ContextDocument.Request != null && context.ContextDocument.Request.Subjects != null )
				{
					BagValue bag = new BagValue( GetDataType( attributeDesignator.DataType ) );
					foreach( ctx.SubjectElement subject in context.ContextDocument.Request.Subjects )
					{
						if( ((pol.SubjectAttributeDesignatorElement)attributeDesignator).SubjectCategory == null || 
							( (pol.SubjectAttributeDesignatorElement)attributeDesignator).SubjectCategory == subject.SubjectCategory )
						{
							foreach( ctx.AttributeElement attrib in FindAttribute( context, attributeDesignator, subject ).Elements )
							{
								bag.Add( attrib );
							}
						}
					}
					return bag;
				}
			}
			else if( attributeDesignator is pol.ResourceAttributeDesignatorElement )
			{
				if( context.ContextDocument.Request != null && context.CurrentResource != null )
				{
					return FindAttribute( context, attributeDesignator, context.CurrentResource );
				}
				else
				{
					return BagValue.Empty;
				}
			}
			else if( attributeDesignator is pol.ActionAttributeDesignatorElement )
			{
				if( context.ContextDocument.Request != null && context.ContextDocument.Request.Action != null )
				{
					return FindAttribute( context, attributeDesignator, context.ContextDocument.Request.Action );
				}
				else
				{
					return BagValue.Empty;
				}
			}
			else if( attributeDesignator is pol.EnvironmentAttributeDesignatorElement )
			{
				if( context.ContextDocument.Request != null && context.ContextDocument.Request.Environment != null )
				{
					return FindAttribute( context, attributeDesignator, context.ContextDocument.Request.Environment );
				}
				else
				{
					return BagValue.Empty;
				}
			}
			throw new EvaluationException( Resource.ResourceManager[ Resource.MessageKey.exc_invalid_attribute_designator ] );
		}

		/// <summary>
		/// Search for the attribute in the context target item using the attribute designator specified.
		/// </summary>
		/// <param name="context">The evaluation context instance.</param>
		/// <param name="attributeDesignator">The attribute designator instance.</param>
		/// <param name="targetItem">The target item to search in.</param>
		/// <returns>A bag value with the values of the attributes found.</returns>
		public static BagValue FindAttribute( EvaluationContext context, pol.AttributeDesignatorBase attributeDesignator, ctx.TargetItemBase targetItem )
		{
			BagValue bag = new BagValue( GetDataType( attributeDesignator.DataType ) );
			foreach( ctx.AttributeElement attribute in targetItem.Attributes )
			{
				if( attribute.Match( attributeDesignator ) )
				{
					context.Trace( "Adding target item attribute designator: {0}", attribute.ToString() );
					bag.Add( attribute );
				}
			}
			return bag;
		}

		/// <summary>
		/// Resolves the attribute reference defined within the given match.
		/// </summary>
		/// <param name="context">The evaluation context instance.</param>
		/// <param name="match">The target item match.</param>
		/// <param name="contextTargetItem">The context target item.</param>
		/// <returns>The context attribute.</returns>
		public static ctx.AttributeElement Resolve( EvaluationContext context, pol.TargetMatchBaseReadWrite match, ctx.TargetItemBase contextTargetItem )
		{
			ctx.AttributeElementReadWrite attribute = null;
			if( match.AttributeReference is pol.AttributeDesignatorBase )
			{
				pol.AttributeDesignatorBase attrDesig = (pol.AttributeDesignatorBase)match.AttributeReference;
				context.Trace( "Looking for attribute: {0}", attrDesig.AttributeId );
				foreach( ctx.AttributeElementReadWrite tempAttribute in contextTargetItem.Attributes )
				{
					if( tempAttribute.Match( attrDesig ) )
					{
						attribute = tempAttribute;
						break;
					}
				}

				if( attribute == null )
				{
					context.Trace( "Attribute not found, loading searching an external repository" );
					attribute = GetAttribute( context, attrDesig );
				}
			}
			else if( match.AttributeReference is pol.AttributeSelectorElement )
			{
				pol.AttributeSelectorElement attrSelec = (pol.AttributeSelectorElement)match.AttributeReference;
				ctx.ResourceContentElement content = (ctx.ResourceContentElement)((ctx.ResourceElement)contextTargetItem).ResourceContent;
				if( content != null )
				{
					XmlDocument doc = context.ContextDocument.XmlDocument;
					
					if( context.ContextDocument.XmlNamespaceManager == null )
					{
						context.ContextDocument.AddNamespaces( context.PolicyDocument.Namespaces );
					}
					
					string xpath = attrSelec.RequestContextPath;
					try
					{
						XmlNode node = doc.DocumentElement.SelectSingleNode( xpath, context.ContextDocument.XmlNamespaceManager );
						if( node != null )
						{
							attribute = new ctx.AttributeElement( null, attrSelec.DataType, null, null, node.InnerText, attrSelec.SchemaVersion );
						}
					}
					catch( XPathException e )
					{
						context.Trace( Resource.TRACE_ERROR, e.Message ); 
						context.ProcessingError = true;
					}
				}
			}
			
			if( attribute == null && match.AttributeReference.MustBePresent )
			{
				context.IsMissingAttribute = true;
				context.AddMissingAttribute( match.AttributeReference );
			}

			if( attribute != null )
			{
				return new ctx.AttributeElement( attribute.AttributeId, attribute.DataType, attribute.Issuer, attribute.IssueInstant, 
					attribute.Value, attribute.SchemaVersion);
			}
			return null;
		}

		/// <summary>
		/// Resolve the attribute desingator that can't be found in the context document
		/// </summary>
		/// <param name="context">The evaluation context instance.</param>
		/// <param name="designator">The attribute designator instance</param>
		public static ctx.AttributeElement GetAttribute( EvaluationContext context, pol.AttributeDesignatorBase designator )
		{
			// Resolve internal attributes
			switch( designator.AttributeId )
			{
				case EnvironmentAttributes.CurrentDate:
					return new ctx.AttributeElement( 
						designator.AttributeId,
						InternalDataTypes.XsdDate,
						null,
						null,
						XmlConvert.ToString( DateTime.Now, "yyyy-MM-dd" ),
						designator.SchemaVersion );
				case EnvironmentAttributes.CurrentTime:
					return new ctx.AttributeElement( 
						designator.AttributeId,
						InternalDataTypes.XsdTime,
						null,
						null,
						XmlConvert.ToString( DateTime.Now, "HH:mm:sszzzzzz" ),
						designator.SchemaVersion );
				case EnvironmentAttributes.CurrentDateTime:
					return new ctx.AttributeElement( 
						designator.AttributeId,
						InternalDataTypes.XsdDateTime,
						null,
						null,
						XmlConvert.ToString( DateTime.Now, "yyyy-MM-ddTHH:mm:sszzzzzz" ),
						designator.SchemaVersion );
				default:
				{
					if( ConfigurationRoot.Config != null )
					{
						// Search for attributes in the configured repositories
						foreach( inf.IAttributeRepository repository in ConfigurationRoot.Config.AttributeRepositories )
						{
							ctx.AttributeElement attribute = repository.GetAttribute( context, designator );
							if( attribute != null )
							{
								return attribute;
							}
						}
					}
					return null;
				}
			}
		}

		/// <summary>
		/// Evaluates a function and also validates it's return value and parameter data types
		/// </summary>
		/// <param name="context">The evaluation context instance.</param>
		/// <param name="functionInstance">The function to call</param>
		/// <param name="arguments">The function arguments</param>
		/// <returns>The return value of the function</returns>
		public static EvaluationValue EvaluateFunction( EvaluationContext context, inf.IFunction functionInstance, params inf.IFunctionParameter[] arguments )
		{
            if (context == null) throw new ArgumentNullException("context");
			// If the caller is in a missing attribute state the function should not be called
			if( context.IsMissingAttribute )
			{
				context.Trace( "There's a missing attribute in the parameters" ); //TODO: resources
				return EvaluationValue.Indeterminate; 
			}
			else
			{
				// Validate function defined arguments
				int functionArgumentIdx;
				for( functionArgumentIdx = 0; functionArgumentIdx < functionInstance.Arguments.Length; functionArgumentIdx++ )
				{
					// Validate the value is not an Indeterminate value
					if( arguments[ functionArgumentIdx ] is EvaluationValue && 
						((EvaluationValue)arguments[ functionArgumentIdx ]).IsIndeterminate )
					{
						if( !context.IsMissingAttribute )
						{
							context.ProcessingError = true;
						}
						context.Trace( "There's a parameter with Indeterminate value" );
						return EvaluationValue.Indeterminate;
					}

					// Compare the function and the value data type
					if( ( ( functionInstance.Arguments[ functionArgumentIdx ] != arguments[ functionArgumentIdx ].GetType( context ) ) && 
						( ( functionInstance.Arguments[ functionArgumentIdx ] != DataTypeDescriptor.Bag ) && (arguments[ functionArgumentIdx ] is BagValue) ) ) )
					{
						context.ProcessingError = true;
						context.Trace( "There's a parameter with an invalid datatype" ); //TODO: resources
						return EvaluationValue.Indeterminate;
					}
				}

				//If the function supports variable arguments, the last datatype is used to validate the
				//rest of the parameters
				if( functionInstance.VarArgs )
				{
					functionArgumentIdx--;
					for( int argumentValueIdx = functionArgumentIdx; argumentValueIdx < arguments.Length; argumentValueIdx++ )
					{
						// Validate the value is not an Indeterminate value
						if( arguments[ argumentValueIdx ] is EvaluationValue && ((EvaluationValue)arguments[ argumentValueIdx ]).IsIndeterminate )
						{
							if( !context.IsMissingAttribute )
							{
								context.ProcessingError = true;
							}
							context.Trace( "There's a parameter with Indeterminate value" ); //TODO: resources
							return EvaluationValue.Indeterminate;
						}

						// Compare the function and the value data type
						if( ( functionInstance.Arguments[ functionArgumentIdx ] != arguments[ argumentValueIdx ].GetType( context ) ) && 
							( (arguments[ argumentValueIdx ] is BagValue) && ( functionInstance.Arguments[ functionArgumentIdx ] != DataTypeDescriptor.Bag ) ) )
						{
							context.ProcessingError = true;
							context.Trace( "There's a parameter with an invalid datatype" ); //TODO: resources
							return EvaluationValue.Indeterminate;
						}
					}
				}

				StringBuilder sb = new StringBuilder();

				// Call the function in a controlled evironment to capture any exception
				try
				{
					sb.Append( functionInstance.Id );
					sb.Append( "( " );
					bool isFirst = true;
					foreach( inf.IFunctionParameter param in arguments )
					{
						if( isFirst )
						{
							isFirst = false;
						}
						else
						{
							sb.Append( ", " );
						}
						sb.Append( param.ToString() );
					}
					sb.Append( " )" );
					sb.Append( " = " );

					EvaluationValue returnValue = functionInstance.Evaluate( context, arguments );
					
					sb.Append( returnValue.ToString() );
					context.Trace( sb.ToString() );

					return returnValue;
				}
				catch( EvaluationException e )
				{
					context.Trace( sb.ToString() );
					context.ProcessingError = true;
					context.Trace( "Error: {0}", e.Message ); //TODO: resources
					return EvaluationValue.Indeterminate;
				}
			}
		}

		#endregion

		#region Private members

		/// <summary>
		/// Prepare the evaluation engine with the function instances.
		/// </summary>
		private static void Prepare()
		{
			if( _functions != null && _dataTypes != null )
			{
				return;
			}
			else
			{
				_functions = new Hashtable();
				_dataTypes = new Hashtable();
			}

			_functions.Add( InternalFunctions.AnyUriEqual, new AnyUriEqual() );
			_functions.Add( InternalFunctions.AnyUriBagSize, new AnyUriBagSize() );
			_functions.Add( InternalFunctions.AnyUriOneAndOnly, new AnyUriOneAndOnly() );
			_functions.Add( InternalFunctions.AnyUriIsIn, new AnyUriIsIn() );
			_functions.Add( InternalFunctions.AnyUriBag, new AnyUriBag() );
			_functions.Add( InternalFunctions.AnyOf, new AnyOf() );
			_functions.Add( InternalFunctions.AnyOfAny, new AnyOfAny() );
			_functions.Add( InternalFunctions.AnyOfAll, new AnyOfAll() );
			_functions.Add( InternalFunctions.AnyUriAtLeastOneMemberOf, new AnyUriAtLeastOneMemberOf() );
			_functions.Add( InternalFunctions.AnyUriIntersection, new AnyUriIntersection() );
			_functions.Add( InternalFunctions.AnyUriSetEquals, new AnyUriSetEquals() );
			_functions.Add( InternalFunctions.AnyUriSubset, new AnyUriSubset() );
			_functions.Add( InternalFunctions.AnyUriUnion, new AnyUriUnion() );
			_functions.Add( InternalFunctions.AllOf, new AllOf() );
			_functions.Add( InternalFunctions.AllOfAny, new AllOfAny() );
			_functions.Add( InternalFunctions.AllOfAll, new AllOfAll() );
			_functions.Add( InternalFunctions.And, new AndFunction() );
			_functions.Add( InternalFunctions.Base64BinaryEqual, new Base64BinaryEqual() );
			_functions.Add( InternalFunctions.Base64BinaryBagSize, new Base64BinaryBagSize() );
			_functions.Add( InternalFunctions.Base64BinaryBag, new Base64BinaryBag() );
			_functions.Add( InternalFunctions.Base64BinaryIsIn, new Base64BinaryIsIn() );
			_functions.Add( InternalFunctions.Base64BinaryOneAndOnly, new Base64BinaryOneAndOnly() );
			_functions.Add( InternalFunctions.Base64BinaryAtLeastOneMemberOf, new Base64BinaryAtLeastOneMemberOf() );
			_functions.Add( InternalFunctions.Base64BinaryIntersection, new Base64BinaryIntersection() );
			_functions.Add( InternalFunctions.Base64BinarySetEquals, new Base64BinarySetEquals() );
			_functions.Add( InternalFunctions.Base64BinarySubset, new Base64BinarySubset() );
			_functions.Add( InternalFunctions.Base64BinaryUnion, new Base64BinaryUnion() );
			_functions.Add( InternalFunctions.BooleanEqual, new BooleanEqual() );
			_functions.Add( InternalFunctions.BooleanBagSize, new BooleanBagSize() );
			_functions.Add( InternalFunctions.BooleanOneAndOnly, new BooleanOneAndOnly() );
			_functions.Add( InternalFunctions.BooleanIsIn, new BooleanIsIn() );
			_functions.Add( InternalFunctions.BooleanBag, new BooleanBag() );
			_functions.Add( InternalFunctions.BooleanAtLeastOneMemberOf, new BooleanAtLeastOneMemberOf() );
			_functions.Add( InternalFunctions.BooleanIntersection, new BooleanIntersection() );
			_functions.Add( InternalFunctions.BooleanSetEquals, new BooleanSetEquals() );
			_functions.Add( InternalFunctions.BooleanSubset, new BooleanSubset() );
			_functions.Add( InternalFunctions.BooleanUnion, new BooleanUnion() );
			_functions.Add( InternalFunctions.DateEqual, new DateEqual() );
			_functions.Add( InternalFunctions.DateBagSize, new DateBagSize() );
			_functions.Add( InternalFunctions.DateBag, new DateBag() );
			_functions.Add( InternalFunctions.DateIsIn, new DateIsIn() );
			_functions.Add( InternalFunctions.DateOneAndOnly, new DateOneAndOnly() );
			_functions.Add( InternalFunctions.DateGreaterThanOrEqual, new DateGreaterThanOrEqual() );
			_functions.Add( InternalFunctions.DateGreaterThan, new DateGreaterThan() );
			_functions.Add( InternalFunctions.DateLessThanOrEqual, new DateLessThanOrEqual() );
			_functions.Add( InternalFunctions.DateLessThan, new DateLessThan() );
			_functions.Add( InternalFunctions.DateAddYearMonthDuration, new DateAddYearMonthDuration() );
			_functions.Add( InternalFunctions.DateSubtractYearMonthDuration, new DateSubtractYearMonthDuration() );
			_functions.Add( InternalFunctions.DateAtLeastOneMemberOf, new DateAtLeastOneMemberOf() );
			_functions.Add( InternalFunctions.DateIntersection, new DateIntersection() );
			_functions.Add( InternalFunctions.DateSetEquals, new DateSetEquals() );
			_functions.Add( InternalFunctions.DateSubset, new DateSubset() );
			_functions.Add( InternalFunctions.DateUnion, new DateUnion() );
			_functions.Add( InternalFunctions.DateTimeBagSize, new BagSize() );
			_functions.Add( InternalFunctions.DateTimeBag, new Bag() );
			_functions.Add( InternalFunctions.DateTimeEqual, new Equal() );
			_functions.Add( InternalFunctions.DateTimeOneAndOnly, new OneAndOnly() );
			_functions.Add( InternalFunctions.DateTimeIsIn, new IsIn() );
			_functions.Add( InternalFunctions.DateTimeGreaterThanOrEqual, new GreaterThanOrEqual() );
			_functions.Add( InternalFunctions.DateTimeGreaterThan, new GreaterThan() );
			_functions.Add( InternalFunctions.DateTimeLessThanOrEqual, new LessThanOrEqual() );
			_functions.Add( InternalFunctions.DateTimeLessThan, new LessThan() );
			_functions.Add( InternalFunctions.DateTimeAddDaytimeDuration, new AddDaytimeDuration() );
			_functions.Add( InternalFunctions.DateTimeAddYearMonthDuration, new AddYearMonthDuration() );
			_functions.Add( InternalFunctions.DateTimeSubtractDaytimeDuration, new SubtractDaytimeDuration() );
			_functions.Add( InternalFunctions.DateTimeSubtractYearMonthDuration, new SubtractYearMonthDuration() );
			_functions.Add( InternalFunctions.DateTimeAtLeastOneMemberOf, new AtLeastOneMemberOf() );
			_functions.Add( InternalFunctions.DateTimeIntersection, new Intersection() );
			_functions.Add( InternalFunctions.DateTimeSetEquals, new SetEquals() );
			_functions.Add( InternalFunctions.DateTimeSubset, new Subset() );
			_functions.Add( InternalFunctions.DateTimeUnion, new Union() );
			_functions.Add( InternalFunctions.DaytimeDurationEqual, new DaytimeDurationEqual() );
			_functions.Add( InternalFunctions.DaytimeDurationBag, new DaytimeDurationBag() );
			_functions.Add( InternalFunctions.DaytimeDurationBagSize, new DaytimeDurationBagSize() );
			_functions.Add( InternalFunctions.DaytimeDurationIsIn, new DaytimeDurationIsIn() );
			_functions.Add( InternalFunctions.DaytimeDurationOneAndOnly, new DaytimeDurationOneAndOnly() );
			_functions.Add( InternalFunctions.DaytimeDurationAtLeastOneMemberOf, new DaytimeDurationAtLeastOneMemberOf() );
			_functions.Add( InternalFunctions.DaytimeDurationIntersection, new DaytimeDurationIntersection() );
			_functions.Add( InternalFunctions.DaytimeDurationSetEquals, new DaytimeDurationSetEquals() );
			_functions.Add( InternalFunctions.DaytimeDurationSubset, new DaytimeDurationSubset() );
			_functions.Add( InternalFunctions.DaytimeDurationUnion, new DaytimeDurationUnion() );
			_functions.Add( InternalFunctions.DoubleGreaterThanOrEqual, new DoubleGreaterThanOrEqual() );
			_functions.Add( InternalFunctions.DoubleGreaterThan, new DoubleGreaterThan() );
			_functions.Add( InternalFunctions.DoubleLessThanOrEqual, new DoubleLessThanOrEqual() );
			_functions.Add( InternalFunctions.DoubleLessThan, new DoubleLessThan() );
			_functions.Add( InternalFunctions.DoubleOneAndOnly, new DoubleOneAndOnly() );
			_functions.Add( InternalFunctions.DoubleAdd, new DoubleAdd() );
			_functions.Add( InternalFunctions.DoubleMultiply, new DoubleMultiply() );
			_functions.Add( InternalFunctions.DoubleSubtract, new DoubleSubtract() );
			_functions.Add( InternalFunctions.DoubleDivide, new DoubleDivide() );
			_functions.Add( InternalFunctions.DoubleAbs, new DoubleAbs() );
			_functions.Add( InternalFunctions.DoubleToInteger, new DoubleToInteger() );
			_functions.Add( InternalFunctions.DoubleEqual, new DoubleEqual() );
			_functions.Add( InternalFunctions.DoubleBagSize, new DoubleBagSize() );
			_functions.Add( InternalFunctions.DoubleBag, new DoubleBag() );
			_functions.Add( InternalFunctions.DoubleIsIn, new DoubleIsIn() );
			_functions.Add( InternalFunctions.DoubleAtLeastOneMemberOf, new DoubleAtLeastOneMemberOf() );
			_functions.Add( InternalFunctions.DoubleIntersection, new DoubleIntersection() );
			_functions.Add( InternalFunctions.DoubleSetEquals, new DoubleSetEquals() );
			_functions.Add( InternalFunctions.DoubleSubset, new DoubleSubset() );
			_functions.Add( InternalFunctions.DoubleUnion, new DoubleUnion() );
			_functions.Add( InternalFunctions.Floor, new Floor() );
			_functions.Add( InternalFunctions.HexBinaryEqual, new HexBinaryEqual() );
			_functions.Add( InternalFunctions.HexBinaryBagSize, new HexBinaryBagSize() );
			_functions.Add( InternalFunctions.HexBinaryBag, new HexBinaryBag() );
			_functions.Add( InternalFunctions.HexBinaryIsIn, new HexBinaryIsIn() );
			_functions.Add( InternalFunctions.HexBinaryOneAndOnly, new HexBinaryOneAndOnly() );
			_functions.Add( InternalFunctions.HexBinaryAtLeastOneMemberOf, new HexBinaryAtLeastOneMemberOf() );
			_functions.Add( InternalFunctions.HexBinaryIntersection, new HexBinaryIntersection() );
			_functions.Add( InternalFunctions.HexBinarySetEquals, new HexBinarySetEquals() );
			_functions.Add( InternalFunctions.HexBinarySubset, new HexBinarySubset() );
			_functions.Add( InternalFunctions.HexBinaryUnion, new HexBinaryUnion() );
			_functions.Add( InternalFunctions.IntegerEqual, new IntegerEqual() );
			_functions.Add( InternalFunctions.IntegerBagSize, new IntegerBagSize() );
			_functions.Add( InternalFunctions.IntegerBag, new IntegerBag() );
			_functions.Add( InternalFunctions.IntegerIsIn, new IntegerIsIn() );
			_functions.Add( InternalFunctions.IntegerGreaterThanOrEqual, new IntegerGreaterThanOrEqual() );
			_functions.Add( InternalFunctions.IntegerGreaterThan, new IntegerGreaterThan() );
			_functions.Add( InternalFunctions.IntegerLessThanOrEqual, new IntegerLessThanOrEqual() );
			_functions.Add( InternalFunctions.IntegerLessThan, new IntegerLessThan() );
			_functions.Add( InternalFunctions.IntegerSubtract, new IntegerSubtract() );
			_functions.Add( InternalFunctions.IntegerAdd, new IntegerAdd() );
			_functions.Add( InternalFunctions.IntegerMultiply, new IntegerMultiply() );
			_functions.Add( InternalFunctions.IntegerDivide, new IntegerDivide() );
			_functions.Add( InternalFunctions.IntegerMod, new IntegerMod() );
			_functions.Add( InternalFunctions.IntegerAbs, new IntegerAbs() );
			_functions.Add( InternalFunctions.IntegerToDouble, new IntegerToDouble() );
			_functions.Add( InternalFunctions.IntegerOneAndOnly, new IntegerOneAndOnly() );
			_functions.Add( InternalFunctions.IntegerAtLeastOneMemberOf, new IntegerAtLeastOneMemberOf() );
			_functions.Add( InternalFunctions.IntegerIntersection, new IntegerIntersection() );
			_functions.Add( InternalFunctions.IntegerSetEquals, new IntegerSetEquals() );
			_functions.Add( InternalFunctions.IntegerSubset, new IntegerSubset() );
			_functions.Add( InternalFunctions.IntegerUnion, new IntegerUnion() );
			_functions.Add( InternalFunctions.Map, new MapFunction() );
			_functions.Add( InternalFunctions.Not, new NotFunction() );
			_functions.Add( InternalFunctions.Nof, new NofFunction() );
			_functions.Add( InternalFunctions.Or, new OrFunction() );
			_functions.Add( InternalFunctions.RegexpStringMatch, new StringRegexpMatch() );
			_functions.Add( InternalFunctions.Rfc822NameEqual, new Rfc822NameEqual() );
			_functions.Add( InternalFunctions.Rfc822NameBagSize, new Rfc822NameBagSize() );
			_functions.Add( InternalFunctions.Rfc822NameBag, new Rfc822NameBag() );
			_functions.Add( InternalFunctions.Rfc822NameIsIn, new Rfc822NameIsIn() );
			_functions.Add( InternalFunctions.Rfc822NameMatch, new Rfc822NameMatch() );
			_functions.Add( InternalFunctions.Rfc822NameOneAndOnly, new Rfc822NameOneAndOnly() );
			_functions.Add( InternalFunctions.Rfc822NameAtLeastOneMemberOf, new Rfc822NameAtLeastOneMemberOf() );
			_functions.Add( InternalFunctions.Rfc822NameIntersection, new Rfc822NameIntersection() );
			_functions.Add( InternalFunctions.Rfc822NameSetEquals, new Rfc822NameSetEquals() );
			_functions.Add( InternalFunctions.Rfc822NameSubset, new Rfc822NameSubset() );
			_functions.Add( InternalFunctions.Rfc822NameUnion, new Rfc822NameUnion() );
			_functions.Add( InternalFunctions.Round, new Round() );
			_functions.Add( InternalFunctions.StringEqual, new StringEqual() );
			_functions.Add( InternalFunctions.StringOneAndOnly, new StringOneAndOnly() );
			_functions.Add( InternalFunctions.StringIsIn, new StringIsIn() );
			_functions.Add( InternalFunctions.StringBagSize, new StringBagSize() );
			_functions.Add( InternalFunctions.StringBag, new StringBag() );
			_functions.Add( InternalFunctions.StringGreaterThanOrEqual, new StringGreaterThanOrEqual() );
			_functions.Add( InternalFunctions.StringGreaterThan, new StringGreaterThan() );
			_functions.Add( InternalFunctions.StringLessThanOrEqual, new StringLessThanOrEqual() );
			_functions.Add( InternalFunctions.StringLessThan, new StringLessThan() );
			_functions.Add( InternalFunctions.StringNormalizeSpace, new StringNormalizeSpace() );
			_functions.Add( InternalFunctions.StringNormalizeToLowercase, new StringNormalizeToLowercase() );
			_functions.Add( InternalFunctions.StringAtLeastOneMemberOf, new StringAtLeastOneMemberOf() );
			_functions.Add( InternalFunctions.StringIntersection, new StringIntersection() );
			_functions.Add( InternalFunctions.StringSetEquals, new StringSetEquals() );
			_functions.Add( InternalFunctions.StringSubset, new StringSubset() );
			_functions.Add( InternalFunctions.StringUnion, new StringUnion() );
			_functions.Add( InternalFunctions.TimeOneAndOnly, new TimeOneAndOnly() );
			_functions.Add( InternalFunctions.TimeBagSize, new TimeBagSize() );
			_functions.Add( InternalFunctions.TimeBag, new TimeBag() );
			_functions.Add( InternalFunctions.TimeIsIn, new TimeIsIn() );
			_functions.Add( InternalFunctions.TimeEqual, new TimeEqual() );
			_functions.Add( InternalFunctions.TimeGreaterThanOrEqual, new TimeGreaterThanOrEqual() );
			_functions.Add( InternalFunctions.TimeGreaterThan, new TimeGreaterThan() );
			_functions.Add( InternalFunctions.TimeLessThanOrEqual, new TimeLessThanOrEqual() );
			_functions.Add( InternalFunctions.TimeLessThan, new TimeLessThan() );
			_functions.Add( InternalFunctions.TimeAtLeastOneMemberOf, new TimeAtLeastOneMemberOf() );
			_functions.Add( InternalFunctions.TimeIntersection, new TimeIntersection() );
			_functions.Add( InternalFunctions.TimeSetEquals, new TimeSetEquals() );
			_functions.Add( InternalFunctions.TimeSubset, new TimeSubset() );
			_functions.Add( InternalFunctions.TimeUnion, new TimeUnion() );
			_functions.Add( InternalFunctions.X500NameEqual, new X500NameEqual() );
			_functions.Add( InternalFunctions.X500NameBagSize, new X500NameBagSize() );
			_functions.Add( InternalFunctions.X500NameBag, new X500NameBag() );
			_functions.Add( InternalFunctions.X500NameIsIn, new X500NameIsIn() );
			_functions.Add( InternalFunctions.X500NameMatch, new X500NameMatch() );
			_functions.Add( InternalFunctions.X500NameOneAndOnly, new X500NameOneAndOnly() );
			_functions.Add( InternalFunctions.X500NameAtLeastOneMemberOf, new X500NameAtLeastOneMemberOf() );
			_functions.Add( InternalFunctions.X500NameIntersection, new X500NameIntersection() );
			_functions.Add( InternalFunctions.X500NameSetEquals, new X500NameSetEquals() );
			_functions.Add( InternalFunctions.X500NameSubset, new X500NameSubset() );
			_functions.Add( InternalFunctions.X500NameUnion, new X500NameUnion() );
			_functions.Add( InternalFunctions.YearMonthDurationBag, new YearMonthDurationBag() );
			_functions.Add( InternalFunctions.YearMonthDurationBagSize, new YearMonthDurationBagSize() );
			_functions.Add( InternalFunctions.YearMonthDurationEqual, new YearMonthDurationEqual() );
			_functions.Add( InternalFunctions.YearMonthDurationIsIn, new YearMonthDurationIsIn() );
			_functions.Add( InternalFunctions.YearMonthDurationOneAndOnly, new YearMonthDurationOneAndOnly() );
			_functions.Add( InternalFunctions.YearMonthDurationAtLeastOneMemberOf, new YearMonthDurationAtLeastOneMemberOf() );
			_functions.Add( InternalFunctions.YearMonthDurationIntersection, new YearMonthDurationIntersection() );
			_functions.Add( InternalFunctions.YearMonthDurationSetEquals, new YearMonthDurationSetEquals() );
			_functions.Add( InternalFunctions.YearMonthDurationSubset, new YearMonthDurationSubset() );
			_functions.Add( InternalFunctions.YearMonthDurationUnion, new YearMonthDurationUnion() );
			_functions.Add( InternalFunctions.XPathNodeCount, new XPathNodeCount() );
			_functions.Add( InternalFunctions.XPathNodeEqual, new XPathNodeEqual() );
			_functions.Add( InternalFunctions.XPathNodeMatch, new XPathNodeMatch() );

			
			_functions.Add( PolicySchema2.InternalFunctions.DnsNameAtLeastOneMemberOf, new DnsNameAtLeastOneMemberOf() );
			_functions.Add( PolicySchema2.InternalFunctions.DnsNameBag, new DnsNameBag() );
			_functions.Add( PolicySchema2.InternalFunctions.DnsNameBagSize, new DnsNameBagSize() );
			_functions.Add( PolicySchema2.InternalFunctions.DnsNameIntersection, new DnsNameIntersection() );
			_functions.Add( PolicySchema2.InternalFunctions.DnsNameUnion, new DnsNameUnion() );
			_functions.Add( PolicySchema2.InternalFunctions.DnsNameSetEquals, new DnsNameSetEquals() );
			_functions.Add( PolicySchema2.InternalFunctions.DnsNameOneAndOnly, new DnsNameOneAndOnly() );
			_functions.Add( PolicySchema2.InternalFunctions.DnsNameEqual, new DnsNameEqual() );
			_functions.Add( PolicySchema2.InternalFunctions.DnsNameIsIn, new DnsNameIsIn() );
			_functions.Add( PolicySchema2.InternalFunctions.DnsNameSubset, new DnsNameSubset() );
			_functions.Add( PolicySchema2.InternalFunctions.StringConcatenate, new StringConcatenate() );
			_functions.Add( PolicySchema2.InternalFunctions.UrlStringConcatenate, new UrlStringConcatenate() );
			_functions.Add( PolicySchema2.InternalFunctions.IPAddressEqual, new IPAddressEqual() );
			_functions.Add( PolicySchema2.InternalFunctions.IPAddressIsIn, new IPAddressIsIn() );
			_functions.Add( PolicySchema2.InternalFunctions.IPAddressSubset, new IPAddressSubset() );
			_functions.Add( PolicySchema2.InternalFunctions.IPAddressIntersection, new IPAddressIntersection() );
			_functions.Add( PolicySchema2.InternalFunctions.IPAddressAtLeastOneMemberOf, new IPAddressAtLeastOneMemberOf() );
			_functions.Add( PolicySchema2.InternalFunctions.IPAddressUnion, new IPAddressUnion() );
			_functions.Add( PolicySchema2.InternalFunctions.IPAddressSetEquals, new IPAddressSetEquals() );
			_functions.Add( PolicySchema2.InternalFunctions.IPAddressBag, new IPAddressBag() );
			_functions.Add( PolicySchema2.InternalFunctions.IPAddressBagSize, new IPAddressBagSize() );
			_functions.Add( PolicySchema2.InternalFunctions.IPAddressOneAndOnly, new IPAddressOneAndOnly() );
			_functions.Add( PolicySchema2.InternalFunctions.StringRegexpMatch, new StringRegexpMatch() );
			_functions.Add( PolicySchema2.InternalFunctions.IPAddressRegexpMatch, new IPAddressRegexpMatch() );
			_functions.Add( PolicySchema2.InternalFunctions.DnsNameRegexpMatch, new DnsNameRegexpMatch() );
			_functions.Add( PolicySchema2.InternalFunctions.AnyUriRegexpMatch, new AnyUriRegexpMatch() );
			_functions.Add( PolicySchema2.InternalFunctions.Rfc822NameRegexpMatch, new Rfc822NameRegexpMatch() );
			_functions.Add( PolicySchema2.InternalFunctions.X500NameRegexpMatch, new X500NameRegexpMatch() );
			
			// Add the supported data types
			_dataTypes.Add( InternalDataTypes.X500Name, DataTypeDescriptor.X500Name );
			_dataTypes.Add( InternalDataTypes.Rfc822Name, DataTypeDescriptor.Rfc822Name );
			_dataTypes.Add( InternalDataTypes.XsdString, DataTypeDescriptor.String );
			_dataTypes.Add( InternalDataTypes.XsdBoolean, DataTypeDescriptor.Boolean );
			_dataTypes.Add( InternalDataTypes.XsdInteger, DataTypeDescriptor.Integer );
			_dataTypes.Add( InternalDataTypes.XsdDouble, DataTypeDescriptor.Double );
			_dataTypes.Add( InternalDataTypes.XsdTime, DataTypeDescriptor.Time );
			_dataTypes.Add( InternalDataTypes.XsdDate, DataTypeDescriptor.Date );
			_dataTypes.Add( InternalDataTypes.XsdDateTime, DataTypeDescriptor.DateTime );
			_dataTypes.Add( InternalDataTypes.XsdAnyUri, DataTypeDescriptor.AnyUri );
			_dataTypes.Add( InternalDataTypes.XsdHexBinary, DataTypeDescriptor.HexBinary );
			_dataTypes.Add( InternalDataTypes.XsdBase64Binary, DataTypeDescriptor.Base64Binary );
			_dataTypes.Add( InternalDataTypes.XQueryDaytimeDuration, DataTypeDescriptor.DaytimeDuration );
			_dataTypes.Add( InternalDataTypes.XQueryYearMonthDuration, DataTypeDescriptor.YearMonthDuration );
			_dataTypes.Add( PolicySchema2.InternalDataTypes.DnsName, DataTypeDescriptor.DnsName );
			_dataTypes.Add( PolicySchema2.InternalDataTypes.IPAddress, DataTypeDescriptor.IPAddress );
		}
		#endregion
	}
}
