using System;
using System.Collections.Generic;
using System.Text;

namespace Tad.Validation
{
    public class ValidationRules
    {
        [NonSerialized]
        private object m_oTarget;

        [NonSerialized]
        private Dictionary<string, List<RuleMethod>> m_oRulesList;

        private BrokenRuleCollection m_oBrokenRules;

        public BrokenRuleCollection BrokenRules
        {
            get
            {
                if (m_oBrokenRules == null)
                    m_oBrokenRules = new BrokenRuleCollection();

                return m_oBrokenRules;
            }
        }

        internal ValidationRules(object oBusinessObject)
        {
            m_oTarget = oBusinessObject;
        }

        internal void SetTarget(object oBusinessObject)
        {
            m_oTarget = oBusinessObject;
        }

        public void AddRules(RuleHandler oHandler, string strPropertyName)
        {
            List<RuleMethod> oList = GetRulesForProperty(strPropertyName);
            oList.Add(new RuleMethod(m_oTarget, oHandler, strPropertyName));
        }

        private Dictionary<string, List<RuleMethod>> RulesList
        {
            get
            {
                if (m_oRulesList == null)
                {
                    m_oRulesList = new Dictionary<string, List<RuleMethod>>();
                }
                return m_oRulesList;
            }
        }

        private List<RuleMethod> GetRulesForProperty(string strPropertyName)
        {
            if (RulesList.ContainsKey(strPropertyName))
            {
                m_oRulesList.Add(strPropertyName, new List<RuleMethod>());
            }

            return m_oRulesList[strPropertyName];
        }

        public void CheckRules(string strPropertyName)
        {
            if (RulesList.ContainsKey(strPropertyName))
            {
                CheckRules(RulesList[strPropertyName]);
            }
        }

        public void CheckRules()
        {
            foreach (KeyValuePair<string, List<RuleMethod>> oPair in RulesList)
            {
                CheckRules(oPair.Value);
            }
        }

        private void CheckRules(List<RuleMethod> oRules)
        {
            foreach (RuleMethod oRule in oRules)
            {
                if (oRule.Invoke())
                    BrokenRules.Remove(oRule);
                else
                    BrokenRules.Add(oRule);
            }
        }

        internal class RuleMethod
        {
            private object m_oTarget;
            private RuleHandler m_oHandler;
            private string m_strRuleName = string.Empty;
            private RuleArgs m_oArgs;

            public RuleMethod(object oTarget, RuleHandler oHandler, string strPropertyName)
            {
                m_oTarget = oTarget;
                m_oHandler = oHandler;
                m_oArgs = new RuleArgs(strPropertyName);

                m_strRuleName = oHandler.Method.Name + "!" + m_oArgs.ToString();
            }

            public string RuleName
            {
                get { return m_strRuleName; }
            }

            public bool Invoke()
            {
                return m_oHandler.Invoke(m_oTarget, m_oArgs);
            }
        }

        public bool IsValid
        {
            get { return BrokenRules.Count == 0; }
        }
    }
}
