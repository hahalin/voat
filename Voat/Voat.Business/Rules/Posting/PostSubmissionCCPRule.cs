﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voat.Configuration;
using Voat.RulesEngine;

namespace Voat.Rules.Posting
{

    [RuleDiscovery("Approved if user who has -50 CCP or less has submitted fewer than 1 submission in a 24 hour sliding window.", "approved = (user.CCP <= -50 and TotalSubmissionsInPast24Hours < 1 or user.CCP > -50)")]
    public class PostSubmissionCCPRule : MinimumCCPRule
    {
        private int countThreshold = Settings.DailyPostingQuotaForNegativeScore;

        public PostSubmissionCCPRule() : base("Submission CCP Throttle", "6.1", -50, RuleScope.PostSubmission)
        {
        }

        protected override RuleOutcome EvaluateRule(VoatRuleContext context)
        {
            //TODO:
            //Need implementation of this rule
            if (context.UserData.Information.CommentPoints.Sum <= MinimumCommentPoints && context.UserData.TotalVotesUsedIn24Hours >= countThreshold)
            {
                return CreateOutcome(RuleResult.Denied, "User with CCP value of {0} is limited to {1} posts(s) in 24 hours.", context.PropertyBag.CCP, countThreshold);
            }
            return base.EvaluateRule(context);
        }
    }
}
