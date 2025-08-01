﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        tag dl = new tag(userChildTags: ["dd", "dt"]);
        tag dt = new tag(userParentTags: ["dl"]);
        tag em;
        tag embed = new tag(attributes: ["height", "src", "type", "width"]);
        tag fieldset = new tag(userParentTags: ["form"], attributes: ["disabled", "form", "name"]);
        tag figcaption = new tag(userParentTags: ["figure"]);
        tag figure;
        tag footer;
        tag form = new tag(userChildTags:["input", "textarea", "button", "select", "option", "optgroup", "fieldset", "label", "output"], attributes: ["action", "autocomplete", "enctype", "method", "name", "novalidate", "rel", "target"]);
        tag h1;
        tag h2;
        tag h3;
        tag h4;
        tag h5;
        tag h6;
        tag head = new tag(userParentTags: ["html"], userChildTags: ["title", "style", "base", "link", "meta", "script", "noscript"], useEventsAttributes:false);
        tag header;
        tag hgroup;
        tag hr;
        tag html = new tag(attributes: ["xmlns"]);
        tag i;
        tag iframe = new tag(attributes: ["allow", "allowfullscreen", "allowpaymentrequest", "height", "loading", "name", "referrerpolicy", "sandbox", "src", "srcdoc", "width"]);
        tag img = new tag(attributes: ["alt", "crossorigin", "height", "ismap", "loading", "longdesc", "referrerpolicy", "sizes", "src", "srcset", "usemap", "width"]);
        tag input = new tag(attributes: ["accept", "alt", "autocomplete", "autofocus", "checked", "dirname", "disabled", "form", "formaction", "formenctype", "formmethod", "formnovalidate", "formtarget", "height", "list", "max", "maxlength", "min", "minlength", "multiple", "name", "pattern", "placeholder", "popovertarget", "popovertargetaction", "readonly", "required", "size", "src", "step", "type", "value", "width"]);
        tag ins = new tag(attributes: ["cite", "datetime"]);
        tag kbd;
        tag labelnew = new tag(attributes: ["for", "form"]);
        tag legend = new tag(userParentTags: ["fieldset"]);
        tag li = new tag(userParentTags: ["ol", "ul", "menu"], attributes: ["value"]);
        tag link = new tag(attributes: ["crossorigin", "href", "hreflang", "media", "referrerpolicy", "rel", "sizes", "title", "type"]);
        tag main;
        tag map = new tag(attributes:["name"], userChildTags: ["area"]);
        tag mark;
        tag menu = new tag(userChildTags: ["li"]);
        tag meta = new tag(userParentTags:["head"], attributes: ["charset", "content", "name"], useEventsAttributes: false);
        tag meter = new tag(attributes: ["form", "high", "low", "max", "min", "optimum", "value"]);
        tag nav;
        tag noscript = new tag(userChildTags: ["link","style","meta"], userParentTags: ["head", "body"]);
        tag object_ = new tag(name: "object", attributes: ["data", "form", "height", "name", "type", "typemustmatch", "usemap", "width"]);
        tag ol = new tag(userChildTags: ["li"], attributes: ["reversed", "start", "type"]);
        tag optgroup = new tag(userParentTags: ["select"], attributes: ["disabled", "label"]);
        tag option = new tag(userParentTags: ["select", "optgroup", "datalist"], attributes: ["disabled", "label", "selected", "value"]);
        tag output = new tag(attributes: ["for", "form", "name"]);
        tag p;
        tag picture = new tag(userChildTags: ["source", "img"]);
        tag pre;
        tag progress = new tag(attributes: ["max", "value"]);
        tag q = new tag(attributes: ["cite"]);
        tag rp = new tag(userParentTags: ["ruby"]);
        tag rt = new tag(userParentTags: ["ruby"]);
        tag ruby = new tag(userChildTags: ["rp", "rt"]);
        tag s;
        tag samp;
        tag script = new tag(attributes: ["async", "crossorigin", "defer", "integrity", "nomodule", "referrerpolicy", "src", "type"], useEventsAttributes:false);
        tag search;
        tag section;
        tag select = new tag(attributes: ["autofocus", "disabled", "form", "multiple", "name", "required", "size"]);
        tag small;
        tag source = new tag(attributes: ["media", "sizes", "src", "srcset", "type"]);
        tag span;
        tag strong;
        tag style = new tag(userParentTags: ["head"], attributes: ["media", "type"]);
        tag sub;
        tag summary = new tag(userParentTags: ["details"]);
        tag sup;
        tag svg;
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
        tag track = new tag(userParentTags: ["audio", "video"], attributes: ["default", "kind", "label", "src", "srclang"]);
        tag u;
        tag ul = new tag(userChildTags: ["li"]);
        tag var;
        tag video = new tag(userChildTags: ["source"], attributes: ["autoplay", "controls", "height", "loop", "muted", "poster", "preload", "src", "width"]);
        tag wbr;
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
