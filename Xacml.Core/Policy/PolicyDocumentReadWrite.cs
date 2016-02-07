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
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace Xacml.Core.Policy
{
    /// <summary>
    /// Represents a read/write PolicyDocument which may contain a Policy or a PolicySet.
    /// </summary>
    public class PolicyDocumentReadWrite
    {
        #region Private members

        /// <summary>
        /// The PolicySet if the document defines a PolicySet.
        /// </summary>
        private PolicySetElementReadWrite _policySet;

        /// <summary>
        /// The Policy if the document defines a single Policy.
        /// </summary>
        private PolicyElementReadWrite _policy;

        /// <summary>
        /// Whether the Xsd validation for the have succeeded or not.
        /// </summary>
        private bool _isValidDocument = true;

        /// <summary>
        /// All the namespaces and the prefix defined in the document. This is used in the XPath
        /// queries because the XPath uses the preffixes and we must provide them in the 
        /// XmlNamespaceManager.
        /// </summary>
        private Hashtable _namespaces = new Hashtable();

        /// <summary>
        /// The name of the embedded resource for the 1.0 schema.
        /// </summary>
        public const string Xacml10PolicySchemaResourceName = "Xacml.Core.Schemas.cs-xacml-schema-policy-01.xsd";

        /// <summary>
        /// The name of the embedded resource for the 2.0 schema.
        /// </summary>
        public const string Xacml20PolicySchemaResourceName = "Xacml.Core.Schemas.access_control-xacml-2.0-policy-schema-os.xsd";
        //public const string XACML_2_0_POLICY_SCHEMA_RESOURCE_NAME = "Xacml.Core.Schemas.access_control-xacml-2.0-policy-schema-cd-01.xsd";

        /// <summary>
        /// The version of the instance used to validate this document.
        /// </summary>
        private XacmlVersion _schemaVersion;

#if NET20
        /// <summary>
        /// The compiled schema for the policy document is kept in memory for performance reasons.
        /// </summary>
        private static XmlSchemaSet _compiledSchemas11;

        /// <summary>
        /// The compiled schema for the policy document is kept in memory for performance reasons.
        /// </summary>
        private static XmlSchemaSet _compiledSchemas20;
#endif
        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new blank PolicyDocument
        /// </summary>
        /// <param name="schemaVersion">The version of the schema that will be used to validate.</param>
        public PolicyDocumentReadWrite(XacmlVersion schemaVersion)
        {
            _schemaVersion = schemaVersion;
        }

        /// <summary>
        /// Creates a new PolicyDocument using the XmlReader instance provided with the possibility of writing.
        /// </summary>
        /// <param name="reader">The XmlReader positioned at the begining of the document.</param>
        /// <param name="schemaVersion">The version of the schema that will be used to validate.</param>
        public PolicyDocumentReadWrite(XmlReader reader, XacmlVersion schemaVersion)
        {
            // Validate the parameters
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            _schemaVersion = schemaVersion;

            // Prepare Xsd validation
#if NET10
			XmlValidatingReader vreader = new XmlValidatingReader( reader );
#endif
#if NET20
            ValidationEventHandler validationHandler = vreader_ValidationEventHandler;
            XmlReaderSettings settings = new XmlReaderSettings {ValidationType = ValidationType.Schema};
            settings.ValidationEventHandler += validationHandler;
            XmlReader vreader = null;
#endif
            try
            {
                switch (schemaVersion)
                {
                    case XacmlVersion.Version10:
                    case XacmlVersion.Version11:
                        {
                            Stream schemaStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Xacml10PolicySchemaResourceName);
#if NET10
                            XmlTextReader schemaReader = new XmlTextReader(schemaStream);
						    vreader.Schemas.Add( PolicySchema1.Namespaces.Policy, schemaReader );
#endif
#if NET20
                            if (_compiledSchemas11 == null)
                            {
                                _compiledSchemas11 = new XmlSchemaSet();
                                _compiledSchemas11.Add(XmlSchema.Read(schemaStream, validationHandler));
                                _compiledSchemas11.Compile();
                            }
                            settings.Schemas.Add(_compiledSchemas11);
#endif
                            break;
                        }
                    case XacmlVersion.Version20:
                        {
                            Stream schemaStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Xacml20PolicySchemaResourceName);
#if NET10
                            XmlTextReader schemaReader = new XmlTextReader(schemaStream);
						    vreader.Schemas.Add( PolicySchema2.Namespaces.Policy, schemaReader );
#endif
#if NET20
                            if (_compiledSchemas20 == null)
                            {
                                _compiledSchemas20 = new XmlSchemaSet();
                                _compiledSchemas20.Add(XmlSchema.Read(schemaStream, validationHandler));
                                _compiledSchemas20.Compile();
                            }
                            settings.Schemas.Add(_compiledSchemas20);
#endif
                            break;
                        }
                    default:
                        throw new ArgumentException(Resource.ResourceManager[Resource.MessageKey.exc_invalid_version_parameter_value], "version");
                }
#if NET10
				vreader.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler( vreader_ValidationEventHandler );
#endif
#if NET20
                vreader = XmlReader.Create(reader, settings);
#endif
                // Read and validate the document.
                while (vreader.Read())
                {
                    //Read all the namespaces and keep them in the _namespaces hashtable.
                    if (vreader.HasAttributes)
                    {
                        while (vreader.MoveToNextAttribute())
                        {
                            if (vreader.LocalName == PolicySchema1.Namespaces.XMLNS)
                            {
                                _namespaces.Add(vreader.Prefix, vreader.Value);
                            }
                            else if (vreader.Prefix == PolicySchema1.Namespaces.XMLNS)
                            {
                                _namespaces.Add(vreader.LocalName, vreader.Value);
                            }
                        }
                        vreader.MoveToElement();
                    }

                    // Check the first element of the document and proceeds to read the contents 
                    // depending on the first node name.
                    switch (vreader.LocalName)
                    {
                        case PolicySchema1.PolicySetElement.PolicySet:
                            {
                                _policySet = new PolicySetElementReadWrite(vreader, schemaVersion);
                                return;
                            }
                        case PolicySchema1.PolicyElement.Policy:
                            {
                                _policy = new PolicyElementReadWrite(vreader, schemaVersion);
                                return;
                            }
                    }
                }
            }
            finally
            {
                if (vreader != null)
                {
                    vreader.Close();
                }
            }
        }

        #endregion

        #region Public properties

        /// <summary>
        /// The version of the schema used to validate this instance.
        /// </summary>
        public virtual XacmlVersion Version
        {
            get { return _schemaVersion; }
            set { _schemaVersion = value; }
        }

        /// <summary>
        /// Whether the document have passed the Xsd validation.
        /// </summary>
        public virtual bool IsValidDocument
        {
            get { return _isValidDocument; }
            set { _isValidDocument = value; }
        }

        /// <summary>
        /// The PolicySet contained in the document.
        /// </summary>
        public virtual PolicySetElementReadWrite PolicySet
        {
            get { return _policySet; }
            set { _policySet = value; }
        }

        /// <summary>
        /// The Policy contained in the document.
        /// </summary>
        public virtual PolicyElementReadWrite Policy
        {
            get { return _policy; }
            set { _policy = value; }
        }

        /// <summary>
        /// All the namespaced defined in the document.
        /// </summary>
        public virtual IDictionary Namespaces
        {
            get { return _namespaces; }
            set { _namespaces = (Hashtable)value; }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// The method called for each Xsd error detected during validation.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The validation error detail.</param>
        private void vreader_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine();
            _isValidDocument = false;
        }

        #endregion

        #region Public methods
        /// <summary>
        /// Writes the XML of the current element
        /// </summary>
        /// <param name="writer">The XmlWriter in which the element will be written</param>
        public void WriteDocument(XmlWriter writer)
        {
            if (writer == null) throw new ArgumentNullException("writer");
            writer.WriteStartDocument();

            //If there's a PolicySet element
            if (_policySet != null)
            {
                this._policySet.WriteDocument(writer, _namespaces);
            }
            //If there's a Policy element
            else if (_policy != null)
            {
                this._policy.WriteDocument(writer, _namespaces);
            }
            writer.WriteEndDocument();
        }

        #endregion
    }
}
