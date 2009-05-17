using System;
using System.Collections.Generic;
using System.Text;

namespace Tad.Validation
{
    [Serializable]
    public class BrokenRule
    {
        internal BrokenRule(ValidationRules.RuleMethod oRule)
        {
            PropertyName = oRule.ToString();
            Description = oRule.ToString();
            RuleName = oRule.RuleName;
        }

        public string RuleName { get; private set; }
        public string PropertyName { get; private set; }
        public string Description { get; private set; }
    }
}
