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
using pol = Xacml.Core.Policy;
using ctx = Xacml.Core.Context;

namespace Xacml.Core.Runtime
{
	/// <summary>
	/// Represents a Target that will be evaluated un runtime.
	/// </summary>
	public class Target
	{
		#region Private members

		/// <summary>
		/// The target defined in the policy document.
		/// </summary>
		private pol.TargetElement _target;

		/// <summary>
		/// The result of the target evaluation.
		/// </summary>
		private TargetEvaluationValue _evaluationValue;

		/// <summary>
		/// All the resources defined in the target.
		/// </summary>
		private ResourceTargetItems _resources;

		/// <summary>
		/// All the subjects defined in the target.
		/// </summary>
		private SubjectTargetItems _subjects;

		/// <summary>
		/// All the actions defined in the target.
		/// </summary>
		private ActionTargetItems _actions;

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a runtime evaluable target.
		/// </summary>
		/// <param name="target"></param>
		public Target( pol.TargetElement target )
		{
			_target = target;
			_resources = new ResourceTargetItems( (pol.ResourcesElement)_target.Resources );
			_subjects = new SubjectTargetItems( (pol.SubjectsElement)_target.Subjects );
			_actions = new ActionTargetItems( (pol.ActionsElement)_target.Actions );
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Matches this target instance using the context document.
		/// </summary>
		/// <param name="context">The evaluation context instance.</param>
		/// <returns>The results of the evaluation of this target.</returns>
		public TargetEvaluationValue Evaluate( EvaluationContext context )
		{
            if (context == null) throw new ArgumentNullException("context");
			// Set the default value.
			_evaluationValue = TargetEvaluationValue.NoMatch;

			// Resource
			context.Trace( "Evaluating Resource..." );
			context.AddIndent();

			TargetEvaluationValue resourceEval = _resources.Evaluate( context, context.CurrentResource );

			context.TraceContextValues();
			context.Trace( "Target item result: {0}", resourceEval );
			context.RemoveIndent();			

			// Action
			context.Trace( "Evaluating Action..." );
			context.AddIndent();

			TargetEvaluationValue actionEval = _actions.Evaluate( context, context.ContextDocument.Request.Action );

			context.TraceContextValues();
			context.Trace( "Target item result: {0}", actionEval );
			context.RemoveIndent();

			context.Trace( "Evaluating Subjects..." );
			context.AddIndent();
			if( actionEval == TargetEvaluationValue.Match && resourceEval == TargetEvaluationValue.Match )
			{
				// Subjects
				foreach( ctx.SubjectElement ctxSubject in context.ContextDocument.Request.Subjects )
				{
					context.Trace( "Evaluating Subject: {0}", ctxSubject.SubjectCategory );

					// Subject
					TargetEvaluationValue subjectEval = _subjects.Evaluate( context, ctxSubject );

					context.TraceContextValues();
					if( subjectEval == TargetEvaluationValue.Indeterminate )
					{
						_evaluationValue = TargetEvaluationValue.Indeterminate;
					}
					else if( subjectEval == TargetEvaluationValue.Match )
					{
						_evaluationValue = TargetEvaluationValue.Match;
						context.RemoveIndent();
						context.Trace( "Target item result: {0}", _evaluationValue );
						return _evaluationValue;
					}
				}
				context.RemoveIndent();
				context.Trace( "Target item result: {0}", _evaluationValue );
				return _evaluationValue;
			}
			else
			{
				context.Trace( "Actions or Resources does not Match so Subjects will not be evaluated" );
				if( resourceEval == TargetEvaluationValue.Indeterminate || actionEval == TargetEvaluationValue.Indeterminate )
				{
					context.RemoveIndent();
					return TargetEvaluationValue.Indeterminate;
				}
				else
				{
					context.RemoveIndent();
					return TargetEvaluationValue.NoMatch;
				}
			}

		}

		#endregion
	}
}
