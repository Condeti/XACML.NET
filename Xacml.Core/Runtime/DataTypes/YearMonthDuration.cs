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

using System.Globalization;
using System.Text.RegularExpressions;
using Xacml.Core.Runtime.Functions;
using Xacml.PolicySchema1;
using inf = Xacml.Core.Interfaces;
using cor = Xacml.Core;

namespace Xacml.Core.Runtime.DataTypes
{
    /// <summary>
    /// A class defining the YearMonthDuration data type.
    /// </summary>
    public class YearMonthDuration : inf.IDataType
    {
        #region Private members

        /// <summary>
        /// The regular expression used to validate the year month duration as a string value.
        /// </summary>
        private const string PATTERN = @"[\-]?P[0-9]+(Y([0-9]+M)?|M)";

        /// <summary>
        /// The regular expression used to match the year month duration and extract some values.
        /// </summary>
        private const string PATTERN_MATCH = @"(?<n>[\-]?)P((?<y>\d+)Y)?((?<m>\d+)M)?";

        /// <summary>
        /// The years of this duration.
        /// </summary>
        private int _years;

        /// <summary>
        /// The months for this duration.
        /// </summary>
        private int _months;

        /// <summary>
        /// Whether this is a negative duration.
        /// </summary>
        private bool _negative;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        internal YearMonthDuration()
        {
        }

        /// <summary>
        /// Creates a new YearMonthDuration using the string value.
        /// </summary>
        /// <param name="value">The YearMonthDuration as a string.</param>
        public YearMonthDuration(string value)
        {
            Regex re = new Regex(PATTERN);
            Match m = re.Match(value);
            if (m.Success)
            {
                re = new Regex(PATTERN_MATCH);
                m = re.Match(value);
                if (m.Success)
                {
                    _negative = (m.Groups["n"].Value == "-");
                    _years = int.Parse(m.Groups["y"].Value, CultureInfo.InvariantCulture);
                    _months = int.Parse(m.Groups["m"].Value, CultureInfo.InvariantCulture);
                }
                else
                {
                    throw new EvaluationException(Resource.ResourceManager[Resource.MessageKey.exc_bug]);
                }
            }
            else
            {
                throw new EvaluationException(Resource.ResourceManager[Resource.MessageKey.exc_invalid_yearmonth_duration_value, value]);
            }
        }

        #endregion

        #region Public properties

        /// <summary>
        /// The years of this duration.
        /// </summary>
        public int Years
        {
            get { return _negative ? _years * -1 : _years; }
        }

        /// <summary>
        /// The months for this duration.
        /// </summary>
        public int Months
        {
            get { return _negative ? _months * -1 : _months; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// The HashCode method overloaded because of a compiler warning. The base class is called.
        /// </summary>
        /// <returns>The HashCode calculated at the base class.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Equals method overloaded to compare two values of the same data type.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>true, if both values are equals, otherwise false.</returns>
        public override bool Equals(object obj)
        {
            YearMonthDuration compareTo = obj as YearMonthDuration;
            if (compareTo == null)
            {
                return base.Equals(obj);
            }

            return _months == compareTo._months && _years == compareTo._years && _negative == compareTo._negative;
        }

        #endregion

        #region IDataType Members

        /// <summary>
        /// Return the function that compares two values of this data type.
        /// </summary>
        /// <value>An IFunction instance.</value>
        public inf.IFunction EqualFunction
        {
            get { return new YearMonthDurationEqual(); }
        }

        /// <summary>
        /// Return the function that verifies if a value is contained within a bag of values of this data type.
        /// </summary>
        /// <value>An IFunction instance.</value>
        public inf.IFunction IsInFunction
        {
            get { return new YearMonthDurationIsIn(); }
        }

        /// <summary>
        /// Return the function that verifies if all the values in a bag are contained within another bag of values of this data type.
        /// </summary>
        /// <value>An IFunction instance.</value>
        public inf.IFunction SubsetFunction
        {
            get { return new YearMonthDurationSubset(); }
        }

        /// <summary>
        /// The string representation of the data type constant.
        /// </summary>
        /// <value>A string with the Uri for the data type.</value>
        public string DataTypeName
        {
            get { return InternalDataTypes.XQueryYearMonthDuration; }
        }

        /// <summary>
        /// Return an instance of an YearMonthDuration form the string specified.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <param name="parNo">The parameter number being parsed.</param>
        /// <returns>An instance of the type.</returns>
        public object Parse(string value, int parNo)
        {
            return new YearMonthDuration(value);
        }

        #endregion
    }
}
