namespace CoolHTML
{
    public class CoolHTMLAttribute
    {
        public CoolHTMLAttribute(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }
        public string Value { get; set; }
    }
}
