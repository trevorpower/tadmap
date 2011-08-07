using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Tad.Validation
{
    public class BrokenRuleCollection : Collection<BrokenRule>
    {
        internal BrokenRuleCollection()
        {
        }

        internal void Add(ValidationRules.RuleMethod oRule)
        {
            Remove(oRule);
            Add(new BrokenRule(oRule));
        }

        internal void Remove(ValidationRules.RuleMethod oRule)
        {
            for (int iIndex = 0; iIndex < Count; iIndex++)
            {
                if (this[iIndex].RuleName == oRule.RuleName)
                {
                    RemoveAt(iIndex);
                    break;
                }
            }
        }
    }
}
