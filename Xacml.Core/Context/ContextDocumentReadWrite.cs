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
using pol = Xacml.Core.Policy;

namespace Xacml.Core.Context
{
    /// <summary>
    /// Mantains a context document which can be a Request or a response document.
    /// </summary>
    public class ContextDocumentReadWrite
    {
        #region Private members

        /// <summary>
        /// The request defined in the context document.
        /// </summary>
        private RequestElementReadWrite _request;

        /// <summary>
        /// The response defined in the context document.
        /// </summary>
        private ResponseElement _response;

        /// <summary>
        /// Whether the document have passed the validation.
        /// </summary>
        private bool _isValidDocument = true;

        /// <summary>
        /// The name of the embedded resource for the 1.0 schema.
        /// </summary>
        private const string Xacml10ContextSchemaResourceName = "Xacml.Core.Schemas.cs-xacml-schema-context-01.xsd";

        /// <summary>
        /// The name of the embedded resource for the 2.0 schema.
        /// </summary>
        private const string Xacml20ContextSchemaResourceName = "Xacml.Core.Schemas.access_control-xacml-2.0-context-schema-os.xsd";

        /// <summary>
        /// All the namespaces and the prefix defined in the document. This is used in the XPath
        /// queries because the XPath uses the preffixes and we must provide them in the 
        /// XmlNamespaceManager.
        /// </summary>
        private readonly Hashtable _namespaces = new Hashtable();

        /// <summary>
        /// The string of the context document, dirty trick to use the XmlReader and also create an XmlDocument 
        /// instance.
        /// </summary>
        private readonly string _xmlString;

        /// <summary>
        /// The XmlDocument instance will be created the first time its requested.
        /// </summary>
        private XmlDocument _xmlDocument;

        /// <summary>
        /// The XmlNamespaceManager used to execute XPath queries over the document with the namespaces defined 
        /// in the policy document.
        /// </summary>
        private XmlNamespaceManager _xmlNamespaceManager;

#if NET20
        /// <summary>
        /// The compiled schemas are kept in memory for performance reasons.
        /// </summary>
        private static XmlSchemaSet _compiledSchemas11;

        /// <summary>
        /// The compiled schemas are kept in memory for performance reasons.
        /// </summary>
        private static XmlSchemaSet _compiledSchemas20;
#endif
        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new blank ContextDocumentReadWrite
        /// </summary>
        public ContextDocumentReadWrite()
        {
        }

        /// <summary>
        /// Creates a new ContextDocumentReadWrite using the XmlReader instance provided.
        /// </summary>
        /// <param name="reader">The XmlReader instance positioned at the begining of the document.</param>
        /// <param name="schemaVersion">The schema used to validate this context document.</param>
        public ContextDocumentReadWrite(XmlReader reader, XacmlVersion schemaVersion)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            // Search for the first element.
            while (reader.Read() && reader.NodeType != XmlNodeType.Element)
            { }

            // Keep the contents of the context document.
            // HACK: Due to XPath validation the document must be readed twice, the first time to load the instance
            // and the second one to keep a document to execute XPath sentences.
            _xmlString = reader.ReadOuterXml();

            // Read the contents in a new reader.
#if NET20
            StringReader sreader = new StringReader(_xmlString);
#endif

            // Prepare the validation.

#if NET20
            ValidationEventHandler validationHandler = vreader_ValidationEventHandler;
            XmlReaderSettings settings = new XmlReaderSettings {ValidationType = ValidationType.Schema};
            settings.ValidationEventHandler += validationHandler;
            XmlReader vreader = null;
#endif
            switch (schemaVersion)
            {
                case XacmlVersion.Version10:
                case XacmlVersion.Version11:
                    {
                        Stream policySchemaStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(pol.PolicyDocument.Xacml10PolicySchemaResourceName);
                        Stream contextSchemaStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Xacml10ContextSchemaResourceName);
#if NET10
					    vreader.Schemas.Add( PolicySchema1.Namespaces.Policy, new XmlTextReader( policySchemaStream ) );
					    vreader.Schemas.Add( PolicySchema1.Namespaces.Context, new XmlTextReader( contexSchemaStream ) );
#endif
#if NET20
                        if (_compiledSchemas11 == null)
                        {
                            _compiledSchemas11 = new XmlSchemaSet();
                            _compiledSchemas11.Add(XmlSchema.Read(policySchemaStream, validationHandler));
                            _compiledSchemas11.Add(XmlSchema.Read(contextSchemaStream, validationHandler));
                            _compiledSchemas11.Compile();
                        }
                        settings.Schemas.Add(_compiledSchemas11);
#endif
                        break;
                    }
                case XacmlVersion.Version20:
                    {
                        Stream policySchemaStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(pol.PolicyDocumentReadWrite.Xacml20PolicySchemaResourceName);
                        Stream contextSchemaStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Xacml20ContextSchemaResourceName);

                        if (_compiledSchemas20 == null)
                        {
                            _compiledSchemas20 = new XmlSchemaSet();
                            if (policySchemaStream != null)
                                _compiledSchemas20.Add(XmlSchema.Read(policySchemaStream, validationHandler));
                            if (contextSchemaStream != null)
                                _compiledSchemas20.Add(XmlSchema.Read(contextSchemaStream, validationHandler));
                            _compiledSchemas20.Compile();
                        }
                        settings.Schemas.Add(_compiledSchemas20);

                        break;
                    }
                default:
                    throw new ArgumentException(Resource.ResourceManager[Resource.MessageKey.exc_invalid_version_parameter_value], nameof(reader));
            }

#if NET20
            vreader = XmlReader.Create(sreader, settings);
#endif
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
                switch (vreader.LocalName)
                {
                    case ContextSchema.RequestElement.Request:
                        _request = new RequestElementReadWrite(vreader, schemaVersion);
                        break;
                    case ContextSchema.ResponseElement.Response:
                        _response = new ResponseElement(vreader, schemaVersion);
                        break;
                }
            }
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Whether the document loaded have passed the Xsd validation.
        /// </summary>
        public bool IsValidDocument
        {
            get { return _isValidDocument; }
        }

        /// <summary>
        /// The Request in the context document.
        /// </summary>
        public virtual RequestElementReadWrite Request
        {
            get { return _request; }
            set { _request = value; }
        }

        /// <summary>
        /// The Response in the context document.
        /// </summary>
        public ResponseElement Response
        {
            get { return _response; }
            set { _response = value; }
        }

        /// <summary>
        /// Gets the XmlDocument for the context document in order to execute XPath queries. 
        /// </summary>
        public XmlDocument XmlDocument
        {
            get
            {
                if (_xmlDocument == null)
                {
                    _xmlDocument = new XmlDocument();
                    _xmlDocument.LoadXml(_xmlString);
                }
                return _xmlDocument;
            }
        }

        /// <summary>
        /// The XmlNamespaceManager with the namespaces defined in the context document.
        /// </summary>
        public XmlNamespaceManager XmlNamespaceManager
        {
            get
            {
                return _xmlNamespaceManager;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Hashtable Namespaces
        {
            get
            {
                return _namespaces;
            }
        }

        /// <summary>
        /// Allows adding extra namespaces to the namespace manager.
        /// </summary>
        public void AddNamespaces(IDictionary namespaces)
        {
            if (namespaces == null) throw new ArgumentNullException("namespaces");
            _xmlNamespaceManager = new XmlNamespaceManager(_xmlDocument.NameTable);
            foreach (string key in namespaces.Keys)
            {
                _xmlNamespaceManager.AddNamespace(key, (string)namespaces[key]);
            }
        }
        #endregion

        #region Private members

        /// <summary>
        /// Method called by the Xsd validator when an error is found during Xsd validation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vreader_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine();
            _isValidDocument = false;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Writes the context element in the provided writer
        /// </summary>
        /// <param name="writer">The XmlWriter in which the element will be written</param>
        public void WriteRequestDocument(XmlWriter writer)
        {
            if (this._request != null)
            {
                this._request.WriteDocument(writer, _namespaces);
            }
        }

        #endregion
    }
}
