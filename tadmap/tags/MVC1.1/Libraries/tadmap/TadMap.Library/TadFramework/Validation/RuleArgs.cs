namespace Tad.Validation
{
    public class RuleArgs
    {
        public string PropertyName { get; private set; }

        public string ProperytDescription { get; set; }

        public RuleArgs(string strPropertyName)
        {
            PropertyName = strPropertyName;
        }

        public override string ToString()
        {
            return PropertyName;
        }

    }
}
