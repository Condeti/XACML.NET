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

using V1 = Xacml.PolicySchema1;

namespace Xacml.PolicySchema2
{
	
	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class Namespaces
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Policy = "urn:oasis:names:tc:xacml:2.0:policy";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Context = "urn:oasis:names:tc:xacml:2.0:context";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class CombinerParameterElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string ParameterName = "ParameterName";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AttributeValue = "AttributeValue";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string CombinerParameter = "CombinerParameter";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class PolicyCombinerParameterElement : CombinerParameterElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string PolicyCombinerParameter = "PolicyCombinerParameterElement";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string PolicyIdRef = "PolicyIdRef";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class PolicySetCombinerParameterElement : CombinerParameterElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string PolicySetCombinerParameter = "PolicySetCombinerParameterElement";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string PolicySetIdRef = "PolicySetIdRef";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class RuleCombinerParameterElement : CombinerParameterElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string RuleCombinerParameter = "RuleCombinerParameterElement";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string RuleIdRef = "RuleIdRef";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class TargetElement : V1.TargetElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Environments = "Environments";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class PolicyElement : V1.PolicyElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string CombinerParameters = "CombinerParameters";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string RuleCombinerParameters = "RuleCombinerParameters";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string VariableDefinition = "VariableDefinition";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Rule = "Rule";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Version = "Version";
	}
    /// <summary>
    /// 
    /// </summary>
    public class RuleElement : V1.RuleElement
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class ObligationElement : V1.ObligationElement
    {

    }


	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class PolicySetElement : V1.PolicySetElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string CombinerParameters = "CombinerParameters";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string PolicyCombinerParameters = "PolicyCombinerParameters";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string PolicySetCombinerParameters = "PolicySetCombinerParameters";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class ResourceElement : V1.ResourceElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string ResourceTargetNamespace = "urn:oasis:names:tc:xacml:2.0:resource:target-namespace";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class InternalFunctions : V1.InternalFunctions
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string StringConcatenate = "urn:oasis:names:tc:xacml:2.0:function:string-concatenate";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string UrlStringConcatenate = "urn:oasis:names:tc:xacml:2.0:function:url-string-concatenate";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DnsNameAtLeastOneMemberOf = "urn:oasis:names:tc:xacml:2.0:function:dnsName-at-least-one-member-of";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DnsNameBag = "urn:oasis:names:tc:xacml:2.0:function:dnsName-bag";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DnsNameBagSize = "urn:oasis:names:tc:xacml:2.0:function:dnsName-bag-size";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DnsNameIntersection = "urn:oasis:names:tc:xacml:2.0:function:dnsName-intersection";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DnsNameEqual = "urn:oasis:names:tc:xacml:2.0:function:dnsName-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DnsNameIsIn = "urn:oasis:names:tc:xacml:2.0:function:dnsName-is-in";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DnsNameSetEquals = "urn:oasis:names:tc:xacml:2.0:function:dnsName-set-equals";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DnsNameSubset = "urn:oasis:names:tc:xacml:2.0:function:dnsName-subset";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DnsNameUnion = "urn:oasis:names:tc:xacml:2.0:function:dnsName-union";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DnsNameOneAndOnly = "urn:oasis:names:tc:xacml:2.0:function:dnsName-one-and-only";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DnsNameRegexpMatch = "urn:oasis:names:tc:xacml:2.0:function:dnsName-regexp-match";

		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IPAddressEqual = "urn:oasis:names:tc:xacml:2.0:function:ipAddress-equal";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IPAddressIsIn = "urn:oasis:names:tc:xacml:2.0:function:ipAddress-is-in";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IPAddressSubset = "urn:oasis:names:tc:xacml:2.0:function:ipAddress-subset";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IPAddressIntersection = "urn:oasis:names:tc:xacml:2.0:function:ipAddress-intersection";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IPAddressAtLeastOneMemberOf = "urn:oasis:names:tc:xacml:2.0:function:ipAddress-at-least-one-member-of";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IPAddressUnion = "urn:oasis:names:tc:xacml:2.0:function:ipAddress-union";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IPAddressSetEquals = "urn:oasis:names:tc:xacml:2.0:function:ipAddress-set-equals";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IPAddressBag = "urn:oasis:names:tc:xacml:2.0:function:ipAddress-bag";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IPAddressBagSize = "urn:oasis:names:tc:xacml:2.0:function:ipAddress-bag-size";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IPAddressOneAndOnly = "urn:oasis:names:tc:xacml:2.0:function:ipAddress-one-and-only";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IPAddressRegexpMatch = "urn:oasis:names:tc:xacml:2.0:function:ipAddress-regexp-match";

		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string StringRegexpMatch = "urn:oasis:names:tc:xacml:2.0:function:string-regexp-match";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AnyUriRegexpMatch = "urn:oasis:names:tc:xacml:2.0:function:anyUri-regexp-match";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Rfc822NameRegexpMatch = "urn:oasis:names:tc:xacml:2.0:function:rfc822Name-regexp-match";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string X500NameRegexpMatch = "urn:oasis:names:tc:xacml:2.0:function:x500name-regexp-match";

		
	}


	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class InternalDataTypes : V1.InternalDataTypes
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IPAddress = "urn:oasis:names:tc:xacml:2.0:data-type:ipAddress";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DnsName = "urn:oasis:names:tc:xacml:2.0:data-type:dnsName";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class EnvironmentElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AnyEnvironment = "AnyEnvironment";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string EnvironmentMatch = "EnvironmentMatch";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Environment = "Environment";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string CurrentTime = "urn:oasis:names:tc:xacml:1.0:environment:current-time";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string CurrentDate = "urn:oasis:names:tc:xacml:1.0:environment:current-date";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string CurrentDateTime = "urn:oasis:names:tc:xacml:1.0:environment:current-dateTime";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class VariableDefinitionElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string VariableDefinition = "VariableDefinition";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string VariableId = "VariableId";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class PolicyReferenceElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Version = "Version";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string EarliestVersion = "EarliestVersion";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string LatestVersion = "LatestVersion";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class VariableReferenceElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string VariableReference = "VariableReference";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string VariableId = "VariableId";
	}
	
}

