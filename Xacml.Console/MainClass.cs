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
using System.Configuration;
using System.IO;
using System.Xml;
using Xacml.Core;
using Xacml.Core.Configuration;
using Xacml.Core.Context;
using Xacml.Core.Runtime;

namespace Xacml.Console
{
	/// <summary>
	/// Main class for the test tool.
	/// </summary>
	class MainClass
	{
		/// <summary>
		/// Main method for the tool it receives command line arguments and performs the evaluation.
		/// </summary>
		/// <param name="args">The parsed command line arguments.</param>
		static void Main(string[] args)
		{
		    var t = ConfigurationManager.OpenExeConfiguration(@"C:\Git\Xacml.Net\Xacml.Console\bin\Debug\Xacml.Console.exe.config");

            string policy = String.Empty, request = String.Empty;
			bool verbose = false;
            //foreach (string arg in args)
            //{
            //    if ((arg[0] == '/' || arg[0] == '-'))
            //    {
            //        if (arg[1] == 'p' || arg[1] == 'P')
            //        {
            //            policy = arg.Substring(3);
            //        }

            //        if (arg[1] == 'r' || arg[1] == 'R')
            //        {
            //            request = arg.Substring(3);
            //        }

            //        if (arg[1] == 'v' || arg[1] == 'V')
            //        {
            //            verbose = true;
            //        }
            //    }
            //}

            try
			{
                request = @"C:\Git\Xacml.Net\Samples\requests\IIA001Request.xml";
                //request = @"C:\Git\Xacml.Net\Samples\Request.xml";
                policy = @"C:\Git\Xacml.Net\Samples\Policy.xml";
                if ( request.Length != 0 && policy.Length != 0 )
				{
				    using (FileStream fs1 = new FileStream(request, FileMode.Open, FileAccess.Read))

				    {
				        // Load Request
				        ContextDocumentReadWrite requestDocument = ContextLoader.LoadContextDocument(fs1, XacmlVersion.Version20);
                        
                        var res = new EvaluationEngine(verbose).Evaluate((ContextDocument)requestDocument);
                        XmlTextWriter tw = new XmlTextWriter(System.Console.Out) { Formatting = Formatting.Indented };
                        res.WriteDocument(tw);
                    }

                    
				}
				else
				{
					throw new Exception( "Request or policy file not specified." );
				}
			}
			catch( Exception e )
			{
				System.Console.WriteLine( e.Message );
				System.Console.WriteLine();
				System.Console.WriteLine( "Usage:" );
				System.Console.WriteLine( "\t-p:[policyFilePath]  - The path to the policy file" );
				System.Console.WriteLine( "\t-r:[requestFilePath] - The path to the request file" );
				System.Console.WriteLine( "\t-v                   - Makes the execution verbose" );
			}

            System.Console.WriteLine("Press Enter to close...");
		    System.Console.ReadLine();
		}
	}
}
