namespace CoolHTML.Syntax
{
    internal partial class Parser
    {
        enum HtmlTagType
        {
            // Tags (taken from https://developer.mozilla.org/en-US/docs/Web/HTML/Element)

            // Main root
            HtmlTag,

            // Document metadata
            HeadTag,

            // Sectioning root
            BodyTag,

            // Content sectioning

            // Text content
            PTag,
            StrongTag,

            // Inline text semantics

            // Image and multimedia

            // Embedded content

            // Svg and MathML

            // Scripting

            // Demarcating edits

            // Table content

            // Forms

            // Interactive elements

            // Web componenta

            // Obsolete and deprecated elements
        }
    }
}
