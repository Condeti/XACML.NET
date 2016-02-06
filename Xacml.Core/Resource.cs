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
 * The Original Code is com code.
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
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Xacml.Core
{
	/// <summary>
	/// Helper class used to manage application Resources
	/// </summary>
	class Resource
	{
        public const string TRACE_ERROR = "ERR: {0}";

		#region "MessageKey enum"

		/// <summary>
		/// Enumeration of all messages for strong typing
		/// </summary>
		public enum MessageKey 
		{
			/// <summary/>
			exc_invalid_collection_type,
			/// <summary/>
			exc_invalid_node_name,
			/// <summary/>
			exc_duplicated_policy_in_repository,
			/// <summary/>
			exc_invalid_datatype_in_stringvalue,
			/// <summary/>
			exc_invalid_uri_schema,
			/// <summary/>
			exc_bug,
			/// <summary/>
			exc_invalid_daytime_duration_value,
			/// <summary/>
			exc_invalid_yearmonth_duration_value,
			/// <summary/>
			exc_unsupported_xpath_version,
			/// <summary/>
			exc_configuration_file_not_found,
			/// <summary/>
			exc_invalid_attribute_designator,
			/// <summary/>
			exc_invalid_function_usage,
			/// <summary/>
			exc_type_mismatch,
			/// <summary/>
			exc_policy_reference_not_resolved,
			/// <summary/>
			exc_policyset_reference_not_resolved,
			/// <summary/>
			exc_invalid_version_parameter_value,
			/// <summary/>
			exc_invalid_stream_parameter_canseek,
			/// <summary/>
			exc_invalid_document_format_no_policyorpolicyset
		}

		#endregion

		#region "Static part"

		/// <summary>
		/// Resource singleton instance
		/// </summary>
		static Resource InternalResource = new Resource();

		/// <summary>
		/// Gets a resource manager for the assembly resource file
		/// </summary>
		public static Resource ResourceManager
		{
			get{ return InternalResource; }
		}

		#endregion
		
		#region "Private members"

		/// <summary>
		/// String for name of resx file used to store message strings.
		/// </summary>
		private const string ResourceFileName = ".Messages";

		/// <summary>
		/// The resource manager instance
		/// </summary>
		private ResourceManager _rm;

		#endregion

		#region "Constructors"

		/// <summary>
		/// Default constructor
		/// </summary>
		private Resource()
		{
			_rm = new ResourceManager(this.GetType().Namespace + ResourceFileName, Assembly.GetExecutingAssembly());
		}

		#endregion

		#region "Public properties"

		/// <summary>
		/// Gets the message with the specified MessageKey enum key from the assembly resource file
		/// </summary>
		public string this [ MessageKey key ]
		{
			get
			{
#if NET10
				string keyValue = key.ToString(CultureInfo.InvariantCulture);
#endif
#if NET20
				string keyValue = key.ToString();
#endif
				return _rm.GetString( keyValue, CultureInfo.CurrentUICulture );
			}
		}

		/// <summary>
		/// Gets the message with the specified MessageKey enum key from the assembly resource file
		/// </summary>
		public string this [ MessageKey key, params object[] parameters ]
		{
			get
			{
				return this.FormatMessage( key, parameters );
			}
		}

		/// <summary>
		/// Formats a message stored in the assembly resource file
		/// </summary>
		/// <param name="key"><see cref="MessageKey"/> enumeration key</param>
		/// <param name="format">format arguments</param>
		/// <returns>a formated string</returns>
		public string FormatMessage( MessageKey key, params object[] format )
		{
			return String.Format( CultureInfo.CurrentUICulture, this[key], format );  
		}

		#endregion
	}

}