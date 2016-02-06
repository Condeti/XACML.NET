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

namespace Xacml.ContextSchema
{
	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class RequestElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Request = "Request";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Subject = "Subject";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Resource = "Resource";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Action = "Action";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Environment = "Environment";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class ResponseElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Response = "Response";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class AttributeElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Attribute = "Attribute";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AttributeValue = "AttributeValue";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string AttributeId = "AttributeId";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string DataType = "DataType";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Issuer = "Issuer";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string IssueInstant = "IssueInstant";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class ResourceElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string ResourceContent = "ResourceContent";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string ResourceId = "urn:oasis:names:tc:xacml:1.0:resource:resource-id";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class SubjectElement
	{	
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string SubjectCategory = "SubjectCategory";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class ResultElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Result = "Result";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Decision = "Decision";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class StatusElement
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Status = "Status";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string StatusCode = "StatusCode";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string StatusMessage = "StatusMessage";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string StatusDetail = "StatusDetail";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Value = "Value";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class EnvironmentAttributes
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string CurrentTime = "urn:oasis:names:tc:xacml:1.0:environment:current-time";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string CurrentDate = "urn:oasis:names:tc:xacml:1.0:environment:current-date";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string CurrentDateTime = "urn:oasis:names:tc:xacml:1.0:environment:current-dateTime";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class StatusCodes
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string OK = "urn:oasis:names:tc:xacml:1.0:status:ok";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string MissingAttribute = "urn:oasis:names:tc:xacml:1.0:status:missing-attribute";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string SyntaxError = "urn:oasis:names:tc:xacml:1.0:status:syntax-error";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string ProcessingError = "urn:oasis:names:tc:xacml:1.0:status:processing-error";
	}

	/// <summary>The name of the element/attribute in the XSD schema.</summary>
	public class ResourceScope
	{
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string ResourceScopeAttributeId = "urn:oasis:names:tc:xacml:1.0:resource:scope";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Immediate = "Immediate";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Children = "Children";
		/// <summary>The name of the element/attribute in the XSD schema.</summary>
		public const string Descendants = "Descendants";
	}
}
