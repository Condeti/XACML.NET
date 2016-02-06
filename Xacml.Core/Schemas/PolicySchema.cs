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

namespace Xacml.PolicySchema1
{
	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class Namespaces
	{
        /// <summary>The standard namespace preffix</summary>
        public const string XMLNS = "xmlns";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string XPath10 = "http://www.w3.org/TR/1999/Rec-xpath-19991116";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Policy = "urn:oasis:names:tc:xacml:1.0:policy";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Context = "urn:oasis:names:tc:xacml:1.0:context";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string XmlSignature = "http://www.w3.org/2000/09/xmldsig#";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Xsi = "http://www.w3.org/2001/XMLSchema-instance";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class TargetElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Target = "Target";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Subjects = "Subjects";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Resources = "Resources";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Actions = "Actions";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class ApplyElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Apply = "Apply";

		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string FunctionId = "FunctionId";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class ObligationsElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Obligations = "Obligations";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class PolicySetIdReferenceElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string PolicySetIdReference = "PolicySetIdReference";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class PolicyIdReferenceElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string PolicyIdReference = "PolicyIdReference";
	}
	
	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class AttributeAssignmentElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AttributeId = "AttributeId";

		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DataType = "DataType";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class VariableReferenceElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string VariableReference = "VariableReference";

		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string VariableId = "VariableId";
	}
		 
	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class ObligationElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Obligation = "Obligation";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string ObligationId = "ObligationId";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string FulfillOn = "FulfillOn";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AttributeAssignment = "AttributeAssignment";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class FunctionElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Function = "Function";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string FunctionId = "FunctionId";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class ConditionElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Condition = "Condition";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string FunctionId = "FunctionId";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class AttributeValueElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AttributeValue = "AttributeValue";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DataType = "DataType";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class AttributeSelectorElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AttributeSelector = "AttributeSelector";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string RequestContextPath = "RequestContextPath";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DataType = "DataType";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string MustBePresent = "MustBePresent";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class AttributeDesignatorElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AttributeId = "AttributeId";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DataType = "DataType";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Issuer = "Issuer";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string MustBePresent = "MustBePresent";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class SubjectAttributeDesignatorElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string SubjectAttributeDesignator = "SubjectAttributeDesignator";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string SubjectCategory = "SubjectCategory";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class ActionAttributeDesignatorElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string ActionAttributeDesignator = "ActionAttributeDesignator";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class ResourceAttributeDesignatorElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string ResourceAttributeDesignator = "ResourceAttributeDesignator";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class EnvironmentAttributeDesignatorElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string EnvironmentAttributeDesignator = "EnvironmentAttributeDesignator";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class PolicySetElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string PolicySet = "PolicySet";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string PolicyCombiningAlgorithmId = "PolicyCombiningAlgId";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Description = "Description";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string PolicySetId = "PolicySetId";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Obligations = "Obligations";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string PolicySetDefaults = "PolicySetDefaults";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class PolicySetDefaultsElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string XPathVersion = "XPathVersion";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class PolicyElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Policy = "Policy";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Description = "Description";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string PolicyDefaults = "PolicyDefaults";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Obligations = "Obligations";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string PolicyId = "PolicyId";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string RuleCombiningAlgorithmId = "RuleCombiningAlgId";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class PolicyDefaultsElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string XPathVersion = "XPathVersion";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class MatchElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string MatchId = "MatchId";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class SubjectElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Rule = "Rule";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string SubjectAttributeDesignator = "SubjectAttributeDesignator";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AnySubject = "AnySubject";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Subject = "Subject";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string SubjectMatch = "SubjectMatch";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string ActionSubjectId = "urn:oasis:names:tc:xacml:1.0:subject:subject-id";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string SubjectIdQualifier = "urn:oasis:names:tc:xacml:1.0:subject:subject-id-qualifier";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string KeyInfo = "urn:oasis:names:tc:xacml:1.0:subject:key-info";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AuthenticationTime = "urn:oasis:names:tc:xacml:1.0:subject:authentication-time";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AuthLocalityAuthenticationMethod = "urn:oasis:names:tc:xacml:1.0:subject:authn-locality:authentication-method";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string RequestTime = "urn:oasis:names:tc:xacml:1.0:subject:request-time";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string SessionStartTime = "urn:oasis:names:tc:xacml:1.0:subject:session-start-time";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AuthLocalityIPAddress = "urn:oasis:names:tc:xacml:1.0:subject:authn-locality:ip-address";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AuthLocalityDnsName = "urn:oasis:names:tc:xacml:1.0:subject:authn-locality:dns-name";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string LdapUserPassword = "http://www.ietf.org/rfc/rfc2256.txt#userPassword";

		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string CategoryAccessSubject = "urn:oasis:names:tc:xacml:1.0:subject-category:access-subject";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string CategoryRecipientSubject = "urn:oasis:names:tc:xacml:1.0:subject-category:recipient-subject";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string CategoryIntermediarySubject = "urn:oasis:names:tc:xacml:1.0:subject-category:intermediary-subject";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string CategoryCodeBase = "urn:oasis:names:tc:xacml:1.0:subject-category:codebase";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string CategoryRequestingMachine = "urn:oasis:names:tc:xacml:1.0:subject-category:requesting-machine";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class ResourceElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string ResourceAttributeDesignator = "ResourceAttributeDesignator";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AnyResource = "AnyResource";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Resource = "Resource";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string ResourceMatch = "ResourceMatch";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string ResourceId = "urn:oasis:names:tc:xacml:1.0:resource:resource-id";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string ResourceContent = "urn:oasis:names:tc.xacml:1.0:resource:resource-content";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string SimpleFileName = "urn:oasis:names:tc.xacml:1.0:resource:simple-file-name";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string XPath = "urn:oasis:names:tc.xacml:1.0:resource:xpath";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string UfsPath = "urn:oasis:names:tc.xacml:1.0:resource:ufs-path";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Scope = "urn:oasis:names:tc.xacml:1.0:resource:scope";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class ActionElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string ActionAttributeDesignator = "ActionAttributeDesignator";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AnyAction = "AnyAction";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string ActionMatch = "ActionMatch";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Action = "Action";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string ActionId = "urn:oasis:names:tc:xacml:1.0:action:action-id";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string ImpliedAction = "urn:oasis:names:tc:xacml:1.0:action:implied-action";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string ActionNamespace = "urn:oasis:names:tc:xacml:1.0:action:action-namespace";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class RuleElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Rule = "Rule";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string RuleId = "RuleId";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Effect = "Effect";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Description = "Description";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Target = "Target";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Condition = "Condition";
	}

	
	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class RuleCombiningAlgorithms
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DenyOverrides = "urn:oasis:names:tc:xacml:1.0:rule-combining-algorithm:deny-overrides";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string PermitOverrides = "urn:oasis:names:tc:xacml:1.0:rule-combining-algorithm:permit-overrides";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string FirstApplicable = "urn:oasis:names:tc:xacml:1.0:rule-combining-algorithm:first-applicable";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string OrderedDenyOverrides = "urn:oasis:names:tc:xacml:1.1:rule-combining-algorithm:ordered-deny-overrides";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string OrderedPermitOverrides = "urn:oasis:names:tc:xacml:1.1:rule-combining-algorithm:ordered-permit-overrides";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class PolicyCombiningAlgorithms
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DenyOverrides = "urn:oasis:names:tc:xacml:1.0:policy-combining-algorithm:deny-overrides";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string PermitOverrides = "urn:oasis:names:tc:xacml:1.0:policy-combining-algorithm:permit-overrides";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string FirstApplicable = "urn:oasis:names:tc:xacml:1.0:policy-combining-algorithm:first-applicable";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string OnlyOneApplicable = "urn:oasis:names:tc:xacml:1.0:policy-combining-algorithm:only-one-applicable";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string OrderedDenyOverrides = "urn:oasis:names:tc:xacml:1.1:policy-combining-algorithm:ordered-deny-overrides";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string OrderedPermitOverrides = "urn:oasis:names:tc:xacml:1.1:policy-combining-algorithm:ordered-permit-overrides";
	}


	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class InternalFunctions
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string And = "urn:oasis:names:tc:xacml:1.0:function:and";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AnyOf = "urn:oasis:names:tc:xacml:1.0:function:any-of";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AnyOfAny = "urn:oasis:names:tc:xacml:1.0:function:any-of-any";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AnyOfAll = "urn:oasis:names:tc:xacml:1.0:function:any-of-all";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AllOf = "urn:oasis:names:tc:xacml:1.0:function:all-of";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AllOfAny = "urn:oasis:names:tc:xacml:1.0:function:all-of-any";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AllOfAll = "urn:oasis:names:tc:xacml:1.0:function:all-of-all";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AnyUriEqual = "urn:oasis:names:tc:xacml:1.0:function:anyURI-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AnyUriBagSize = "urn:oasis:names:tc:xacml:1.0:function:anyURI-bag-size";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AnyUriBag = "urn:oasis:names:tc:xacml:1.0:function:anyURI-bag";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AnyUriIsIn = "urn:oasis:names:tc:xacml:1.0:function:anyURI-is-in";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AnyUriOneAndOnly = "urn:oasis:names:tc:xacml:1.0:function:anyURI-one-and-only";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AnyUriAtLeastOneMemberOf = "urn:oasis:names:tc:xacml:1.0:function:anyURI-at-least-one-member-of";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AnyUriIntersection = "urn:oasis:names:tc:xacml:1.0:function:anyURI-intersection";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AnyUriSetEquals = "urn:oasis:names:tc:xacml:1.0:function:anyURI-set-equals";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AnyUriSubset = "urn:oasis:names:tc:xacml:1.0:function:anyURI-subset";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AnyUriUnion = "urn:oasis:names:tc:xacml:1.0:function:anyURI-union";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Base64BinaryEqual = "urn:oasis:names:tc:xacml:1.0:function:base64Binary-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Base64BinaryOneAndOnly = "urn:oasis:names:tc:xacml:1.0:function:base64Binary-one-and-only";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Base64BinaryBagSize = "urn:oasis:names:tc:xacml:1.0:function:base64Binary-bag-size";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Base64BinaryBag = "urn:oasis:names:tc:xacml:1.0:function:base64Binary-bag";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Base64BinaryIsIn = "urn:oasis:names:tc:xacml:1.0:function:base64Binary-is-in";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Base64BinaryAtLeastOneMemberOf = "urn:oasis:names:tc:xacml:1.0:function:base64Binary-at-least-one-member-of";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Base64BinaryIntersection = "urn:oasis:names:tc:xacml:1.0:function:base64Binary-intersection";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Base64BinarySetEquals = "urn:oasis:names:tc:xacml:1.0:function:base64Binary-set-equals";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Base64BinarySubset = "urn:oasis:names:tc:xacml:1.0:function:base64Binary-subset";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Base64BinaryUnion = "urn:oasis:names:tc:xacml:1.0:function:base64Binary-union";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string BooleanEqual = "urn:oasis:names:tc:xacml:1.0:function:boolean-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string BooleanOneAndOnly = "urn:oasis:names:tc:xacml:1.0:function:boolean-one-and-only";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string BooleanBagSize = "urn:oasis:names:tc:xacml:1.0:function:boolean-bag-size";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string BooleanBag = "urn:oasis:names:tc:xacml:1.0:function:boolean-bag";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string BooleanIsIn = "urn:oasis:names:tc:xacml:1.0:function:boolean-is-in";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string BooleanAtLeastOneMemberOf = "urn:oasis:names:tc:xacml:1.0:function:boolean-at-least-one-member-of";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string BooleanIntersection = "urn:oasis:names:tc:xacml:1.0:function:boolean-intersection";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string BooleanSetEquals = "urn:oasis:names:tc:xacml:1.0:function:boolean-set-equals";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string BooleanSubset = "urn:oasis:names:tc:xacml:1.0:function:boolean-subset";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string BooleanUnion = "urn:oasis:names:tc:xacml:1.0:function:boolean-union";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateEqual = "urn:oasis:names:tc:xacml:1.0:function:date-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateIsIn = "urn:oasis:names:tc:xacml:1.0:function:date-is-in";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateGreaterThanOrEqual = "urn:oasis:names:tc:xacml:1.0:function:date-greater-than-or-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateGreaterThan = "urn:oasis:names:tc:xacml:1.0:function:date-greater-than";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateLessThanOrEqual = "urn:oasis:names:tc:xacml:1.0:function:date-less-than-or-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateLessThan = "urn:oasis:names:tc:xacml:1.0:function:date-less-than";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateOneAndOnly = "urn:oasis:names:tc:xacml:1.0:function:date-one-and-only";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateBagSize = "urn:oasis:names:tc:xacml:1.0:function:date-bag-size";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateBag = "urn:oasis:names:tc:xacml:1.0:function:date-bag";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateAddYearMonthDuration = "urn:oasis:names:tc:xacml:1.0:function:date-add-yearMonthDuration";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateSubtractYearMonthDuration = "urn:oasis:names:tc:xacml:1.0:function:date-subtract-yearMonthDuration";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateAtLeastOneMemberOf = "urn:oasis:names:tc:xacml:1.0:function:date-at-least-one-member-of";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateIntersection = "urn:oasis:names:tc:xacml:1.0:function:date-intersection";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateSetEquals = "urn:oasis:names:tc:xacml:1.0:function:date-set-equals";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateSubset = "urn:oasis:names:tc:xacml:1.0:function:date-subset";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateUnion = "urn:oasis:names:tc:xacml:1.0:function:date-union";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateTimeEqual = "urn:oasis:names:tc:xacml:1.0:function:dateTime-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateTimeIsIn = "urn:oasis:names:tc:xacml:1.0:function:dateTime-is-in";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateTimeGreaterThanOrEqual = "urn:oasis:names:tc:xacml:1.0:function:dateTime-greater-than-or-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateTimeGreaterThan = "urn:oasis:names:tc:xacml:1.0:function:dateTime-greater-than";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateTimeLessThanOrEqual = "urn:oasis:names:tc:xacml:1.0:function:dateTime-less-than-or-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateTimeLessThan = "urn:oasis:names:tc:xacml:1.0:function:dateTime-less-than";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateTimeOneAndOnly = "urn:oasis:names:tc:xacml:1.0:function:dateTime-one-and-only";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateTimeBagSize = "urn:oasis:names:tc:xacml:1.0:function:dateTime-bag-size";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateTimeBag = "urn:oasis:names:tc:xacml:1.0:function:dateTime-bag";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateTimeAddDaytimeDuration = "urn:oasis:names:tc:xacml:1.0:function:dateTime-add-dayTimeDuration";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateTimeAddYearMonthDuration = "urn:oasis:names:tc:xacml:1.0:function:dateTime-add-yearMonthDuration";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateTimeSubtractDaytimeDuration = "urn:oasis:names:tc:xacml:1.0:function:dateTime-subtract-dayTimeDuration";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateTimeSubtractYearMonthDuration = "urn:oasis:names:tc:xacml:1.0:function:dateTime-subtract-yearMonthDuration";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateTimeAtLeastOneMemberOf = "urn:oasis:names:tc:xacml:1.0:function:dateTime-at-least-one-member-of";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateTimeIntersection = "urn:oasis:names:tc:xacml:1.0:function:dateTime-intersection";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateTimeSetEquals = "urn:oasis:names:tc:xacml:1.0:function:dateTime-set-equals";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateTimeSubset = "urn:oasis:names:tc:xacml:1.0:function:dateTime-subset";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DateTimeUnion = "urn:oasis:names:tc:xacml:1.0:function:dateTime-union";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DaytimeDurationEqual = "urn:oasis:names:tc:xacml:1.0:function:dayTimeDuration-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DaytimeDurationBag = "urn:oasis:names:tc:xacml:1.0:function:dayTimeDuration-bag";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DaytimeDurationBagSize = "urn:oasis:names:tc:xacml:1.0:function:dayTimeDuration-bag-size";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DaytimeDurationIsIn = "urn:oasis:names:tc:xacml:1.0:function:dayTimeDuration-is-in";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DaytimeDurationOneAndOnly = "urn:oasis:names:tc:xacml:1.0:function:dayTimeDuration-one-and-only";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DaytimeDurationAtLeastOneMemberOf = "urn:oasis:names:tc:xacml:1.0:function:dayTimeDuration-at-least-one-member-of";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DaytimeDurationIntersection = "urn:oasis:names:tc:xacml:1.0:function:dayTimeDuration-intersection";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DaytimeDurationSetEquals = "urn:oasis:names:tc:xacml:1.0:function:dayTimeDuration-set-equals";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DaytimeDurationSubset = "urn:oasis:names:tc:xacml:1.0:function:dayTimeDuration-subset";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DaytimeDurationUnion = "urn:oasis:names:tc:xacml:1.0:function:dayTimeDuration-union";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DoubleEqual = "urn:oasis:names:tc:xacml:1.0:function:double-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DoubleBagSize = "urn:oasis:names:tc:xacml:1.0:function:double-bag-size";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DoubleBag = "urn:oasis:names:tc:xacml:1.0:function:double-bag";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DoubleIsIn = "urn:oasis:names:tc:xacml:1.0:function:double-is-in";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DoubleAdd = "urn:oasis:names:tc:xacml:1.0:function:double-add";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DoubleSubtract = "urn:oasis:names:tc:xacml:1.0:function:double-subtract";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DoubleMultiply = "urn:oasis:names:tc:xacml:1.0:function:double-multiply";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DoubleDivide = "urn:oasis:names:tc:xacml:1.0:function:double-divide";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DoubleAbs = "urn:oasis:names:tc:xacml:1.0:function:double-abs";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DoubleGreaterThanOrEqual = "urn:oasis:names:tc:xacml:1.0:function:double-greater-than-or-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DoubleGreaterThan = "urn:oasis:names:tc:xacml:1.0:function:double-greater-than";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DoubleLessThanOrEqual = "urn:oasis:names:tc:xacml:1.0:function:double-less-than-or-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DoubleLessThan = "urn:oasis:names:tc:xacml:1.0:function:double-less-than";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DoubleOneAndOnly = "urn:oasis:names:tc:xacml:1.0:function:double-one-and-only";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DoubleToInteger = "urn:oasis:names:tc:xacml:1.0:function:double-to-integer";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DoubleAtLeastOneMemberOf = "urn:oasis:names:tc:xacml:1.0:function:double-at-least-one-member-of";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DoubleIntersection = "urn:oasis:names:tc:xacml:1.0:function:double-intersection";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DoubleSetEquals = "urn:oasis:names:tc:xacml:1.0:function:double-set-equals";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DoubleSubset = "urn:oasis:names:tc:xacml:1.0:function:double-subset";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DoubleUnion = "urn:oasis:names:tc:xacml:1.0:function:double-union";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Floor = "urn:oasis:names:tc:xacml:1.0:function:floor";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string HexBinaryEqual = "urn:oasis:names:tc:xacml:1.0:function:hexBinary-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string HexBinaryBagSize = "urn:oasis:names:tc:xacml:1.0:function:hexBinary-bag-size";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string HexBinaryBag = "urn:oasis:names:tc:xacml:1.0:function:hexBinary-bag";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string HexBinaryIsIn = "urn:oasis:names:tc:xacml:1.0:function:hexBinary-is-in";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string HexBinaryOneAndOnly = "urn:oasis:names:tc:xacml:1.0:function:hexBinary-one-and-only";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string HexBinaryAtLeastOneMemberOf = "urn:oasis:names:tc:xacml:1.0:function:hexBinary-at-least-one-member-of";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string HexBinaryIntersection = "urn:oasis:names:tc:xacml:1.0:function:hexBinary-intersection";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string HexBinarySetEquals = "urn:oasis:names:tc:xacml:1.0:function:hexBinary-set-equals";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string HexBinarySubset = "urn:oasis:names:tc:xacml:1.0:function:hexBinary-subset";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string HexBinaryUnion = "urn:oasis:names:tc:xacml:1.0:function:hexBinary-union";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IntegerEqual = "urn:oasis:names:tc:xacml:1.0:function:integer-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IntegerBagSize = "urn:oasis:names:tc:xacml:1.0:function:integer-bag-size";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IntegerBag = "urn:oasis:names:tc:xacml:1.0:function:integer-bag";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IntegerIsIn = "urn:oasis:names:tc:xacml:1.0:function:integer-is-in";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IntegerGreaterThanOrEqual = "urn:oasis:names:tc:xacml:1.0:function:integer-greater-than-or-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IntegerGreaterThan = "urn:oasis:names:tc:xacml:1.0:function:integer-greater-than";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IntegerLessThanOrEqual = "urn:oasis:names:tc:xacml:1.0:function:integer-less-than-or-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IntegerLessThan = "urn:oasis:names:tc:xacml:1.0:function:integer-less-than";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IntegerOneAndOnly = "urn:oasis:names:tc:xacml:1.0:function:integer-one-and-only";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IntegerAdd = "urn:oasis:names:tc:xacml:1.0:function:integer-add";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IntegerSubtract = "urn:oasis:names:tc:xacml:1.0:function:integer-subtract";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IntegerMultiply = "urn:oasis:names:tc:xacml:1.0:function:integer-multiply";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IntegerDivide = "urn:oasis:names:tc:xacml:1.0:function:integer-divide";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IntegerMod = "urn:oasis:names:tc:xacml:1.0:function:integer-mod";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IntegerAbs = "urn:oasis:names:tc:xacml:1.0:function:integer-abs";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IntegerToDouble = "urn:oasis:names:tc:xacml:1.0:function:integer-to-double";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IntegerAtLeastOneMemberOf = "urn:oasis:names:tc:xacml:1.0:function:integer-at-least-one-member-of";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IntegerIntersection = "urn:oasis:names:tc:xacml:1.0:function:integer-intersection";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IntegerSetEquals = "urn:oasis:names:tc:xacml:1.0:function:integer-set-equals";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IntegerSubset = "urn:oasis:names:tc:xacml:1.0:function:integer-subset";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IntegerUnion = "urn:oasis:names:tc:xacml:1.0:function:integer-union";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Map = "urn:oasis:names:tc:xacml:1.0:function:map";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Not = "urn:oasis:names:tc:xacml:1.0:function:not";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Nof = "urn:oasis:names:tc:xacml:1.0:function:n-of";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Or = "urn:oasis:names:tc:xacml:1.0:function:or";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Round = "urn:oasis:names:tc:xacml:1.0:function:round";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string RegexpStringMatch = "urn:oasis:names:tc:xacml:1.0:function:regexp-string-match";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Rfc822NameEqual = "urn:oasis:names:tc:xacml:1.0:function:rfc822Name-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Rfc822NameBagSize = "urn:oasis:names:tc:xacml:1.0:function:rfc822Name-bag-size";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Rfc822NameBag = "urn:oasis:names:tc:xacml:1.0:function:rfc822Name-bag";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Rfc822NameIsIn = "urn:oasis:names:tc:xacml:1.0:function:rfc822Name-is-in";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Rfc822NameMatch = "urn:oasis:names:tc:xacml:1.0:function:rfc822Name-match";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Rfc822NameOneAndOnly = "urn:oasis:names:tc:xacml:1.0:function:rfc822Name-one-and-only";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Rfc822NameAtLeastOneMemberOf = "urn:oasis:names:tc:xacml:1.0:function:rfc822Name-at-least-one-member-of";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Rfc822NameIntersection = "urn:oasis:names:tc:xacml:1.0:function:rfc822Name-intersection";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Rfc822NameSetEquals = "urn:oasis:names:tc:xacml:1.0:function:rfc822Name-set-equals";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Rfc822NameSubset = "urn:oasis:names:tc:xacml:1.0:function:rfc822Name-subset";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Rfc822NameUnion = "urn:oasis:names:tc:xacml:1.0:function:rfc822Name-union";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string StringNormalizeSpace = "urn:oasis:names:tc:xacml:1.0:function:string-normalize-space";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string StringNormalizeToLowercase = "urn:oasis:names:tc:xacml:1.0:function:string-normalize-to-lower-case";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string StringIsIn = "urn:oasis:names:tc:xacml:1.0:function:string-is-in";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string StringBag = "urn:oasis:names:tc:xacml:1.0:function:string-bag";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string StringBagSize = "urn:oasis:names:tc:xacml:1.0:function:string-bag-size";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string StringEqual = "urn:oasis:names:tc:xacml:1.0:function:string-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string StringOneAndOnly = "urn:oasis:names:tc:xacml:1.0:function:string-one-and-only";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string StringGreaterThanOrEqual = "urn:oasis:names:tc:xacml:1.0:function:string-greater-than-or-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string StringGreaterThan = "urn:oasis:names:tc:xacml:1.0:function:string-greater-than";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string StringLessThanOrEqual = "urn:oasis:names:tc:xacml:1.0:function:string-less-than-or-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string StringLessThan = "urn:oasis:names:tc:xacml:1.0:function:string-less-than";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string StringAtLeastOneMemberOf = "urn:oasis:names:tc:xacml:1.0:function:string-at-least-one-member-of";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string StringIntersection = "urn:oasis:names:tc:xacml:1.0:function:string-intersection";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string StringSetEquals = "urn:oasis:names:tc:xacml:1.0:function:string-set-equals";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string StringSubset = "urn:oasis:names:tc:xacml:1.0:function:string-subset";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string StringUnion = "urn:oasis:names:tc:xacml:1.0:function:string-union";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string TimeEqual = "urn:oasis:names:tc:xacml:1.0:function:time-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string TimeOneAndOnly = "urn:oasis:names:tc:xacml:1.0:function:time-one-and-only";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string TimeGreaterThanOrEqual = "urn:oasis:names:tc:xacml:1.0:function:time-greater-than-or-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string TimeGreaterThan = "urn:oasis:names:tc:xacml:1.0:function:time-greater-than";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string TimeLessThanOrEqual = "urn:oasis:names:tc:xacml:1.0:function:time-less-than-or-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string TimeLessThan = "urn:oasis:names:tc:xacml:1.0:function:time-less-than";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string TimeBagSize = "urn:oasis:names:tc:xacml:1.0:function:time-bag-size";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string TimeBag = "urn:oasis:names:tc:xacml:1.0:function:time-bag";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string TimeIsIn = "urn:oasis:names:tc:xacml:1.0:function:time-is-in";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string TimeAtLeastOneMemberOf = "urn:oasis:names:tc:xacml:1.0:function:time-at-least-one-member-of";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string TimeIntersection = "urn:oasis:names:tc:xacml:1.0:function:time-intersection";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string TimeSetEquals = "urn:oasis:names:tc:xacml:1.0:function:time-set-equals";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string TimeSubset = "urn:oasis:names:tc:xacml:1.0:function:time-subset";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string TimeUnion = "urn:oasis:names:tc:xacml:1.0:function:time-union";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string X500NameEqual = "urn:oasis:names:tc:xacml:1.0:function:x500Name-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string X500NameBagSize = "urn:oasis:names:tc:xacml:1.0:function:x500Name-bag-size";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string X500NameBag = "urn:oasis:names:tc:xacml:1.0:function:x500Name-bag";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string X500NameIsIn = "urn:oasis:names:tc:xacml:1.0:function:x500Name-is-in";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string X500NameMatch = "urn:oasis:names:tc:xacml:1.0:function:x500Name-match";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string X500NameOneAndOnly = "urn:oasis:names:tc:xacml:1.0:function:x500Name-one-and-only";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string X500NameAtLeastOneMemberOf = "urn:oasis:names:tc:xacml:1.0:function:x500Name-at-least-one-member-of";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string X500NameIntersection = "urn:oasis:names:tc:xacml:1.0:function:x500Name-intersection";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string X500NameSetEquals = "urn:oasis:names:tc:xacml:1.0:function:x500Name-set-equals";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string X500NameSubset = "urn:oasis:names:tc:xacml:1.0:function:x500Name-subset";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string X500NameUnion = "urn:oasis:names:tc:xacml:1.0:function:x500Name-union";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string YearMonthDurationEqual = "urn:oasis:names:tc:xacml:1.0:function:yearMonthDuration-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string YearMonthDurationBag = "urn:oasis:names:tc:xacml:1.0:function:yearMonthDuration-bag";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string YearMonthDurationBagSize = "urn:oasis:names:tc:xacml:1.0:function:yearMonthDuration-bag-size";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string YearMonthDurationIsIn = "urn:oasis:names:tc:xacml:1.0:function:yearMonthDuration-is-in";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string YearMonthDurationOneAndOnly = "urn:oasis:names:tc:xacml:1.0:function:yearMonthDuration-one-and-only";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string YearMonthDurationAtLeastOneMemberOf = "urn:oasis:names:tc:xacml:1.0:function:yearMonthDuration-at-least-one-member-of";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string YearMonthDurationIntersection = "urn:oasis:names:tc:xacml:1.0:function:yearMonthDuration-intersection";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string YearMonthDurationSetEquals = "urn:oasis:names:tc:xacml:1.0:function:yearMonthDuration-set-equals";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string YearMonthDurationSubset = "urn:oasis:names:tc:xacml:1.0:function:yearMonthDuration-subset";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string YearMonthDurationUnion = "urn:oasis:names:tc:xacml:1.0:function:yearMonthDuration-union";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string XPathNodeCount = "urn:oasis:names:tc:xacml:1.0:function:xpath-node-count";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string XPathNodeEqual = "urn:oasis:names:tc:xacml:1.0:function:xpath-node-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string XPathNodeMatch = "urn:oasis:names:tc:xacml:1.0:function:xpath-node-match";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class InternalDataTypes
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string X500Name = "urn:oasis:names:tc:xacml:1.0:data-type:x500Name";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Rfc822Name = "urn:oasis:names:tc:xacml:1.0:data-type:rfc822Name";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string XsdString = "http://www.w3.org/2001/XMLSchema#string";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string XsdBoolean = "http://www.w3.org/2001/XMLSchema#boolean";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string XsdInteger = "http://www.w3.org/2001/XMLSchema#integer";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string XsdDouble = "http://www.w3.org/2001/XMLSchema#double";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string XsdTime = "http://www.w3.org/2001/XMLSchema#time";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string XsdDate = "http://www.w3.org/2001/XMLSchema#date";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string XsdDateTime = "http://www.w3.org/2001/XMLSchema#dateTime";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string XsdAnyUri = "http://www.w3.org/2001/XMLSchema#anyURI";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string XsdHexBinary = "http://www.w3.org/2001/XMLSchema#hexBinary";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string XsdBase64Binary = "http://www.w3.org/2001/XMLSchema#base64Binary";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string XQueryDaytimeDuration = "http://www.w3.org/TR/2002/WD-xquery-operators-20020816#dayTimeDuration";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string XQueryYearMonthDuration = "http://www.w3.org/TR/2002/WD-xquery-operators-20020816#yearMonthDuration";
	}
}

