using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable IDE0044
#pragma warning disable IDE0051
#pragma warning disable CS8618
#pragma warning disable CS8981
#pragma warning disable IDE1006
#pragma warning disable IDE0052 
#pragma warning disable IDE0090
#pragma warning disable CA1861 
namespace SourceCodeBuilderGenerator.Html
{

    internal class Step1Specification
    {
        tag a = new tag(attributes: ["download", "href", "hreflang", "media", "ping", "referrerpolicy", "rel", "target", "type"]);
        tag abbr;
        tag address;
        tag area = new tag(userParentTags: ["map"], attributes: ["alt", "coords", "download", "href", "hreflang", "media", "referrerpolicy", "rel", "shape", "target", "type"]);
        tag article;
        tag aside;
        tag audio = new tag(userChildTags: ["source"], attributes: ["autoplay", "controls", "loop", "muted", "preload", "src"]);
        tag b;
        tag base_ = new(name: "base", userParentTags: ["head"], attributes: ["href", "target"]);
        tag bdi;
        tag bdo = new tag(attributes: ["dir"]);
        tag blockquote = new tag(attributes: ["cite"]);
        tag body = new tag(userParentTags: ["html"]);
        tag br;
        tag button = new tag(attributes: ["autofocus", "disabled", "form", "formaction", "formenctype", "formmethod", "formnovalidate", "formtarget", "popovertarget", "popovertargetaction", "name", "type", "value"]);
        tag canvas = new tag(attributes: ["height", "width"]);
        tag caption = new tag(userChildTags:[], userParentTags: ["table"]);
        tag cite;
        tag code;
        tag col = new tag(userParentTags: ["colgroup"]);
        tag colgroup = new tag(userParentTags: ["table"], userChildTags: ["col"]);
        tag data = new tag(attributes: ["value"]);
        tag datalist = new tag(userParentTags: ["input"], userChildTags: ["option"]);
        tag dd = new tag(userParentTags: ["dl"]);
        tag del = new tag(attributes: ["cite", "datetime"]);
        tag details = new tag(attributes: ["open"]);
        tag dfn;
        tag dialog = new tag(attributes: ["open"]);
        tag div;
        //tag dl;
        //tag dt;
        //tag em;
        //tag embed;
        //tag fieldset;
        //tag figcaption;
        //tag figure;
        //tag footer;
        //tag form;
        //tag h1;
        //tag h2;
        //tag h3;
        //tag h4;
        //tag h5;
        //tag h6;
        //tag head;
        //tag header;
        //tag hgroup;
        //tag hr;
        tag html;
        //tag i;
        //tag iframe;
        //tag img;
        //tag input;
        //tag ins;
        //tag kbd;
        //tag label;
        //tag legend;
        //tag li;
        //tag link;
        //tag main;
        //tag map;
        //tag mark;
        //tag math;
        //tag menu;
        //tag meta;
        //tag meter;
        //tag nav;
        //tag noscript;
        //tag object_ = new tag("object");
        //tag ol;
        //tag optgroup;
        //tag option;
        //tag output;
        //tag p;
        //tag picture;
        //tag pre;
        //tag progress;
        //tag q;
        //tag rp;
        //tag rt;
        //tag ruby;
        //tag s;
        //tag samp;
        //tag script;
        //tag search;
        //tag section;
        //tag select;
        //tag slot;
        //tag small;
        //tag source;
        //tag span;
        //tag strong;
        //tag style;
        //tag sub;
        //tag summary;
        //tag sup;
        //tag svg;
        tag table = new tag(userChildTags: ["tr", "th", "td", "caption", "colgroup", "thead", "tfoot", "tbody"]);
        tag tbody = new tag(userChildTags: ["tr"], userParentTags: ["table"]);
        tag td = new tag(userChildTags: [], userParentTags: ["tr"], attributes: ["colspan", "headers", "rowspan"]);
        tag template;
        tag textarea;
        tag tfoot = new tag(userChildTags: ["tr"], userParentTags: ["table"]);
        tag th = new tag(userChildTags: [], userParentTags: ["tr"], attributes: ["abbr", "colspan", "headers", "rowspan", "scope"]);
        tag thead = new tag(userChildTags: ["tr"], userParentTags: ["table"]);
        tag time;
        tag title;
        tag tr = new tag(userChildTags: ["td", "th"], userParentTags: ["table", "thead", "tbody", "tfoot"]);
        //tag track;
        //tag u;
        //tag ul;
        //tag var;
        //tag video;
        //tag wbr;
    }

    internal class tag
    {
        internal List<string> UserChildTags = [];
        internal List<string> UserParentTags = [];
        internal string? CsName;
        internal string? WriteName;
        internal string? NameUpper1 => CsName.Substring(0,1).ToUpper() + CsName.Substring(1);
        internal List<tag>? ChildTags = [];
        internal List<tag>? ParentTags = [];
        internal List<string>? Attributes;
        internal bool UseGlobalAttributes = true;
        internal bool UseEventsAttributes = true;
        internal bool WithUserChildTags => UserChildTags.Count > 0;
        internal List<string> GetUserChildTags => UserChildTags;
        internal tag() { }
        internal tag(string name)
        {
            CsName = name;
        }
        //internal tag(string[] userChildTags = null, string[] userParentTags = null, List<string>? attributes = null,
        //    bool useGlobalAttributes = true, bool useEventsAttributes = true)
        //{
        //    Attributes = attributes;
        //    if (userChildTags != null)
        //    {
        //        UserChildTags.AddRange(userChildTags);
        //    }
        //    if (userParentTags != null)
        //    {
        //        UserParentTags.AddRange(userParentTags);
        //    }
        //    UseGlobalAttributes = useGlobalAttributes;
        //    UseEventsAttributes = useEventsAttributes;
        //}
        internal tag(string name = null, List<string>? attributes = null, string[]? userChildTags = null, string[] userParentTags = null,
            bool useGlobalAttributes = true, bool useEventsAttributes = true)
        {
            if(name != null)
            {
                WriteName = name;
                CsName = name;
            }
            Attributes = attributes;
            if (userChildTags != null)
            {
                UserChildTags.AddRange(userChildTags);
            }
            if (userParentTags != null)
            {
                UserParentTags.AddRange(userParentTags);
            }
            UseGlobalAttributes = useGlobalAttributes;
            UseEventsAttributes = useEventsAttributes;
        }
        internal static List<string> GlobalAtributes => 
            [
            "accesskey",
            "class",
            "contenteditable",
            //"data-*",
            "dir",
            "draggable",
            "enterkeyhint",
            "hidden",
            "id",
            "inert",
            "inputmode",
            "lang",
            "popover",
            "spellcheck",
            "style",
            "tabindex",
            "title",
            "translate",
            ];

        internal static List<string> EventAtributes =>
            [
            "onafterprint",
            ];
    }
}
