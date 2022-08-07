using System;
using System.Collections.Generic;
using System.Text;

namespace CoolHTML
{
    public enum TagType
    {
        // Tags (taken from https://developer.mozilla.org/en-US/docs/Web/HTML/Element)

        // Main root
        HtmlTag,

        // Document metadata
        HeadTag,
        TitleTag,

        // Sectioning root
        BodyTag,

        // Content sectioning

        // Text content
        TextTag,
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

        // Custom
        CustomTag,
    }
}
