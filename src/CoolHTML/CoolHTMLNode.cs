using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CoolHTML
{
    public class CoolHTMLNode
    {
        public CoolHTMLNode(CoolHTMLNode parent, List<CoolHTMLNode> children, TagType type, string name, List<CoolHTMLAttribute> attributes, CoolHTMLTextContent textContent)
        {
            Parent = parent;
            Children = children;
            Type = type;
            Name = name;
            Attributes = attributes;
            TextContent = textContent;
        }

        public CoolHTMLNode Parent { get; }
        public List<CoolHTMLNode> Children { get; }
        public TagType Type { get; }
        public string Name { get; }
        public List<CoolHTMLAttribute> Attributes { get; set; }
        public CoolHTMLTextContent TextContent { get; set; }
        public string InnerHtml { get; set; }
        public string OuterHtml { 
            get
            {
                if (InnerHtml == null || Attributes.Count == 0) return null;

                var innerHtml = InnerHtml;
                innerHtml = (Attributes.Count == 0 ? $"<{Name}>" : $"<{Name} {AttributeString}>") + innerHtml + $"</{Name}>";

                return innerHtml;
            }
        }
        public string InnerText
        {
            get
            {
                return Regex.Replace(Regex.Replace(InnerHtml, @"<(.*?)>", " "), @" +", " ");
            }
        }
        public string AttributeString { 
            get
            {
                var attributeString = "";

                if(Attributes != null)
                {
                    foreach (var attribute in Attributes)
                    {
                        attributeString += $"{attribute.Key}=\"{attribute.Value}\"";
                    }
                }

                return attributeString;
            }
        }
        public void AppendChild(CoolHTMLNode childNode)
        {
            Children.Add(childNode);
        }
    }
}
