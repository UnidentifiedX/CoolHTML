using System.Collections.Generic;
using static CoolHTML.Syntax.Parser;

namespace CoolHTML
{
    public class CoolHTMLNode
    {
        public CoolHTMLNode(CoolHTMLNode parent, List<CoolHTMLNode> children, TagType type, List<CoolHTMLAttribute> attributes, CoolHTMLTextContent textContent)
        {
            Parent = parent;
            Children = children;
            Type = type;
            Attributes = attributes;
            TextContent = textContent;
        }

        public CoolHTMLNode Parent { get; }
        public List<CoolHTMLNode> Children { get; }
        public TagType Type { get; }
        public List<CoolHTMLAttribute> Attributes { get; set; }
        public CoolHTMLTextContent TextContent { get; set; }
    }
}
