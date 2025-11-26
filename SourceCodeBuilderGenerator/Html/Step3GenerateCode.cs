using SourceCodeBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SourceCodeBuilderGenerator.Html
{
    /// <summary>
    /// 
    /// </summary>
    internal class Step3GenerateCode
    {
        internal static void GenerateCsCode(Step2ReflectionModel model,
            string generatedCodeFileName, string generatedCodeNameSpace)
        {
            MyNamespaceBuilder
                .FileName(generatedCodeFileName)
                .AddUsing("System.Text")
                .AddUsing("System.IO")
                .WithName(generatedCodeNameSpace)

                //Builder class
                .AddClass(MyClassBuilder.Public
                    .Name("HtmlBuilder")
                    .BaseType("Tag")
                    .AddComments(["/// <summary>", "/// Html builder", "/// </summary>"])
                    .AddConstructor(MyConstructorBuilder.Public.Base($"(\"\")"))
                    .AddProperties(model.Tags.Select(tag => MyPropertyBuilder
                        .Public.Static.Type($"{tag.CsName}Tag<HtmlBuilder>").Name(tag.CsName).GetOnly
                        //.GetterExpression(" get {" + $"return new {tag.Name}Tag();")))
                        .GetterExpression(MyCodeExpressionBuilder.Start(2)
                            ._.Add("get").CodeBlock._.Add($"return new (new ());")._.FinishCodeBlock)))
                    .AddMethods(model.Tags.Select(tag => MyMethodBuilder
                        .Public.Static.Type($"{tag.CsName}Tag<HtmlBuilder>").Name($"{tag.NameUpper1}")
                        .Parameter(string.Join(", ", new string[] { "string? innerText = null" }.Union(tag?.Attributes?.Select(o => $"string? _{o} = null").ToArray() ?? [])))
                        .AddLine($"var tag = new {tag.CsName}Tag<HtmlBuilder>(new HtmlBuilder());")
                        .AddLine("if (innerText != null) tag.AddInnerText(innerText);")
                        .AddLines(tag?.Attributes?.Select(attr => $"if (_{attr} != null) tag.AddAttribute(\"{attr}\", _{attr});") ?? [])
                        .AddLine("return tag;"))
                    )
                    .AddMethod(MyMethodBuilder
                        .Public.Static.Type($"tagTag<HtmlBuilder>").Name("Tag")
                        .Parameter("string tagName, string? innerText = null")
                        .AddLine($"var tag = new tagTag<HtmlBuilder>(tagName, new HtmlBuilder());")
                        .AddLine("if (innerText != null) tag.AddInnerText(innerText);")
                        .AddLine("return tag;")
                    )
                    .AddMethod(MyMethodBuilder
                        .Public.Type("override string").Name("ToString")
                        .AddLine("using MemoryStream memoryStream = new MemoryStream();")
                        .AddLine("using StreamWriter streamWriter = new StreamWriter(memoryStream);")
                        .AddLine("Generate(streamWriter);")
                        .AddLine("streamWriter.Flush();")
                        .AddLine("return Encoding.ASCII.GetString(memoryStream.ToArray());")

                    )
                    .AddMethod(MyMethodBuilder
                        .Public.Type("string").Name("ToString")
                        .Parameter("Encoding? encoding = null")
                        .AddLine("using MemoryStream memoryStream = new MemoryStream();")
                        .AddLine("using StreamWriter streamWriter = new StreamWriter(memoryStream, encoding ?? Encoding.Default);")
                        .AddLine("Generate(streamWriter);")
                        .AddLine("streamWriter.Flush();")
                        .AddLine("return (encoding ?? Encoding.Default).GetString(memoryStream.ToArray());")

                    )
                )
                //Base interface for Tag and Code
                .AddInterface(MyInterfaceBuilder.Public
                    .Name("IElement")
                    .AddComments([
                        "/// <summary>",
                        "/// IElement interface",
                        "/// </summary>"
                        ])
                    .AddMethod(MyMethodBuilder
                        .Public.Type("void").Name("Generate")
                        .AddComments([
                            "/// <summary>",
                            "/// Generate result text with TextWriter",
                            "/// </summary>"
                            ])
                        .Parameter("TextWriter", "writer")
                        .Parameter("string?", "_defaultTabs", " = null")
                        .Parameter("bool", "forComments", " = false")
                    )
                )

                //Tag class (base for all tag classes)
                .AddClass(MyClassBuilder.Public
                    .Name("Tag")
                    .BaseType("IElement")
                    .AddComments([
                        "/// <summary>",
                        "/// Tag class",
                        "/// </summary>"
                        ])
                    .AddFields([
                            MyField.PublicString("s_Tabs").Static.Init("\"  \"")
                                .AddComments(["/// <summary>","/// Tree tabs","/// </summary>"]),
                            MyField.String("Name").Init("string.Empty")
                                .AddComments(["/// <summary>","/// Tag name","/// </summary>"]),
                            MyField.String("WriteName").Init("string.Empty")
                                .AddComments(["/// <summary>","/// Tag write name","/// </summary>"]),
                            MyField.String("DeclaredInnerText").Init("string.Empty")
                                .AddComments(["/// <summary>","/// Declared tag inner text","/// </summary>"]),
                            MyFieldBuilder.Private.Type("IElement").List.Name("InnerElements").Init("[]")
                                .AddComments(["/// <summary>","/// List of inner elements","/// </summary>"]),
                            MyFieldBuilder.Private.Type("Tag").Name("ParentTag")
                                .AddComments(["/// <summary>","/// Parent tag","/// </summary>"]),
                            MyFieldBuilder.Private.Type("Dictionary<string, string>").Name("Attributes").Init("[]")
                                .AddComments(["/// <summary>","/// Tag attributes","/// </summary>"]),
                            MyFieldBuilder.Internal.Type("enum").Name("TagTypes { HtmlTag, RazorLine, RazorBlock, RazorCodeBlock, RazorPage}"),
                            MyFieldBuilder.Internal.Type("TagTypes").Name("TagType").Init("TagTypes.HtmlTag")
                                .AddComments(["/// <summary>", "/// TagType", "/// </summary>"]),
                        ])
                    .AddConstructor(MyConstructorBuilder
                        .Public
                        .Parameter("string name")
                        .AddLine("Name = name;"))
                    .AddConstructor(MyConstructorBuilder
                        .Public
                        .Parameter("string name")
                        .Parameter("Tag parent")
                        .AddLine("Name = name;")
                        .AddLine("ParentTag = parent;"))
                    .AddMethod(MyMethodBuilder
                        .Internal.Type("void").Name("AddAttribute")
                        .Parameter("string", "name")
                        .Parameter("string", "value")
                        .AddLine("Attributes.Add(name, value);")
                        )
                    .AddMethod(MyMethodBuilder
                        .Internal.Type("void").Name("AddInnerElement")
                        .Parameter("Tag", "tag")
                        .AddLines([
                            "if(tag is HtmlBuilder)",
                            "{",
                            "    InnerElements.AddRange(tag.InnerElements);",
                            "}",
                            "else",
                            "{",
                            "    InnerElements.Add(tag);",
                            "}"
                            ])
                        )
                    .AddMethod(MyMethodBuilder
                        .Internal.Type("void").Name("AddInnerText")
                        .Parameter("string", "innerText")
                        .AddLine("DeclaredInnerText = innerText;")
                        )
                    .AddMethod(MyMethodBuilder
                        .Public.Type("void").Name("Generate")
                        .AddComments([
                            "/// <summary>",
                            "/// Generate result text with TextWriter",
                            "/// </summary>"
                            ])
                        .Parameter("TextWriter", "writer")
                        .Parameter("string?", "_defaultTabs", " = null")
                        .Parameter("bool", "forComments", " = false")
                        .AddCode(MyCodeExpressionBuilder.Start(1).NewLine
                            .If($"TagType == TagTypes.{nameof(tag.TagTypes.HtmlTag)}")
                            .CodeBlock
                                .AddLine("GenerateTag(writer, _defaultTabs, forComments);")
                            .ElseIf($"TagType == TagTypes.{nameof(tag.TagTypes.RazorLine)}")
                            .CodeBlock
                                .AddLine("GenerateRazorLine(writer, _defaultTabs, forComments);")
                            .ElseIf($"TagType == TagTypes.{nameof(tag.TagTypes.RazorBlock)}")
                            .CodeBlock
                                .AddLine("GenerateRazorBlock(writer, _defaultTabs, forComments);")
                            .ElseIf($"TagType == TagTypes.{nameof(tag.TagTypes.RazorCodeBlock)}")
                            .CodeBlock
                                .AddLine("GenerateRazorCodeBlock(writer, _defaultTabs, forComments);")
                            .ElseIf($"TagType == TagTypes.{nameof(tag.TagTypes.RazorPage)}")
                            .CodeBlock
                                .AddLine("GenerateRazorPage(writer, _defaultTabs, forComments);")
                            .EndIf
                        )
                    )
                    .AddMethod(MyMethodBuilder
                        .Public.Type("void").Name("GenerateTag")
                        .AddComments([
                            "/// <summary>",
                            "/// Generate result text with TextWriter",
                            "/// </summary>"
                            ])
                        .Parameter("TextWriter", "writer")
                        .Parameter("string?", "_defaultTabs", " = null")
                        .Parameter("bool", "forComments", " = false")
                        .AddCode(MyCodeExpressionBuilder.Start(1).NewLine
                            .Tab.Add("string attributes = string.Join(\" \", Attributes.Select(o => $\"{o.Key}=\\\"{o.Value}\\\"\"));")
                            .NewLine
                            .If("attributes != string.Empty")
                            .CodeBlock
                                .AddLine("attributes = $\" {attributes}\";")
                            .EndIf
                            .NewLine
                            .If("!string.IsNullOrEmpty(DeclaredInnerText)")
                            .CodeBlock.AddCode(MyCodeExpressionBuilder.Start(2)
                                .NewLine
                                .If("forComments")
                                    .CodeBlock.AddLine("writer.Write($\"{_defaultTabs}<{Name}{attributes}>{DeclaredInnerText.Replace(Environment.NewLine, Environment.NewLine + _defaultTabs)}</{Name}>\".Replace(\"<\", \">\"));")
                                .Else
                                    .AddLine("writer.Write($\"{_defaultTabs}<{Name}{attributes}>{DeclaredInnerText.Replace(Environment.NewLine, Environment.NewLine + _defaultTabs)}</{Name}>\");")
                                .EndIf)
                            .Else
                                .AddLine("if (!string.IsNullOrEmpty(Name))")
                                .AddLine("{")
                                .AddLine("    writer.Write($\"{_defaultTabs}<{Name}{attributes}>{Environment.NewLine}\");")
                                .AddLine("}")
                                .AddCode(MyCodeExpressionBuilder.Start(2).NewLine
                                    .Foreach("var", "element", "InnerElements")
                                        .AddLine("element.Generate(writer, _defaultTabs == null ? \"\" : _defaultTabs + s_Tabs, forComments);")
                                        .AddLine("writer.Write($\"{Environment.NewLine}\");")
                                    .EndCycle)
                                .AddLine("if (!string.IsNullOrEmpty(Name))")
                                .AddLine("{")
                                .AddLine("    writer.Write($\"{_defaultTabs}</{Name}>\");")
                                .AddLine("}")
                            .EndIf
                        )
                    )
                    .AddMethod(MyMethodBuilder
                        .Public.Type("void").Name("GenerateRazorLine")
                        .AddComments([
                            "/// <summary>",
                            "/// Generate result text with TextWriter",
                            "/// </summary>"
                            ])
                        .Parameter("TextWriter", "writer")
                        .Parameter("string?", "_defaultTabs", " = null")
                        .Parameter("bool", "forComments", " = false")
                        .AddCode(MyCodeExpressionBuilder.Start(1).NewLine
                            .Tab.Add("string attributes = string.Join(\" \", Attributes.Select(o => $\"{o.Key}=\\\"{o.Value}\\\"\"));")
                            .NewLine
                            .If("attributes != string.Empty")
                            .CodeBlock
                                .AddLine("attributes = $\" {attributes}\";")
                            .EndIf
                            .NewLine
                            .If("!string.IsNullOrEmpty(DeclaredInnerText)")
                            .CodeBlock.AddCode(MyCodeExpressionBuilder.Start(2)
                                .NewLine
                                .If("forComments")
                                    .CodeBlock.AddLine("writer.Write($\"{_defaultTabs}{Name} {DeclaredInnerText.Replace(Environment.NewLine, Environment.NewLine + _defaultTabs)}\".Replace(\"<\", \">\"));")
                                .Else
                                    .AddLine("writer.Write($\"{_defaultTabs}{Name} {DeclaredInnerText.Replace(Environment.NewLine, Environment.NewLine + _defaultTabs)}\");")
                                .EndIf)
                            .EndIf
                        )
                    )
                    .AddMethod(MyMethodBuilder
                        .Public.Type("void").Name("GenerateRazorBlock")
                        .AddComments([
                            "/// <summary>",
                            "/// Generate result text with TextWriter",
                            "/// </summary>"
                            ])
                        .Parameter("TextWriter", "writer")
                        .Parameter("string?", "_defaultTabs", " = null")
                        .Parameter("bool", "forComments", " = false")
                        .AddCode(MyCodeExpressionBuilder.Start(1).NewLine
                            .Tab.Add("string attributes = string.Join(\" \", Attributes.Select(o => o.Value));")
                            .NewLine
                            .If("!string.IsNullOrEmpty(DeclaredInnerText)")
                            .CodeBlock.AddCode(MyCodeExpressionBuilder.Start(2)
                                .NewLine
                                .If("forComments")
                                    .CodeBlock
                                        .AddLine("writer.Write($\"{_defaultTabs}{Name}({attributes}){DeclaredInnerText.Replace(Environment.NewLine, Environment.NewLine + _defaultTabs)}\".Replace(\"<\", \">\"));")
                                        .AddLine("writer.Write($\"{_defaultTabs}{Name}({attributes}){Environment.NewLine}\");")
                                        .AddLine("writer.Write($\"{_defaultTabs}{{{Environment.NewLine}\");")
                                        .AddLine("writer.Write($\"{_defaultTabs}{DeclaredInnerText.Replace(Environment.NewLine, Environment.NewLine + _defaultTabs)}{Environment.NewLine}\".Replace(\"<\", \">\"));")
                                        .AddLine("writer.Write($\"{_defaultTabs}}}\");")
                                    .Else
                                        .AddLine("writer.Write($\"{_defaultTabs}{Name}({attributes}){Environment.NewLine}\");")
                                        .AddLine("writer.Write($\"{_defaultTabs}{{{Environment.NewLine}\");")
                                        .AddLine("writer.Write($\"{_defaultTabs}{DeclaredInnerText.Replace(Environment.NewLine, Environment.NewLine + _defaultTabs)}{Environment.NewLine}\");")
                                        .AddLine("writer.Write($\"{_defaultTabs}}}\");")
                                .EndIf)
                            .Else
                                .AddLine("if (!string.IsNullOrEmpty(Name))")
                                .AddLine("{")
                                .AddLine("    writer.Write($\"{_defaultTabs}{Name}({attributes}){Environment.NewLine}\");")
                                .AddLine("    writer.Write($\"{_defaultTabs}{{{Environment.NewLine}\");")
                                .AddLine("}")
                                .AddCode(MyCodeExpressionBuilder.Start(2).NewLine
                                    .Foreach("var", "element", "InnerElements")
                                        .AddLine("element.Generate(writer, _defaultTabs == null ? \"\" : _defaultTabs + s_Tabs, forComments);")
                                        .AddLine("writer.Write($\"{Environment.NewLine}\");")
                                    .EndCycle)
                                .AddLine("if (!string.IsNullOrEmpty(Name))")
                                .AddLine("{")
                                .AddLine("    writer.Write($\"{_defaultTabs}}}\");")
                                .AddLine("}")
                            .EndIf
                        )
                    )
                    .AddMethod(MyMethodBuilder
                        .Public.Type("void").Name("GenerateRazorCodeBlock")
                        .AddComments([
                            "/// <summary>",
                            "/// Generate result text with TextWriter",
                            "/// </summary>"
                            ])
                        .Parameter("TextWriter", "writer")
                        .Parameter("string?", "_defaultTabs", " = null")
                        .Parameter("bool", "forComments", " = false")
                        .AddCode(MyCodeExpressionBuilder.Start(1).NewLine
                            .NewLine
                            .If("!string.IsNullOrEmpty(DeclaredInnerText)")
                            .CodeBlock.AddCode(MyCodeExpressionBuilder.Start(2)
                                .NewLine
                                .If("forComments")
                                    .CodeBlock
                                        .AddLine("writer.Write($\"{_defaultTabs}{Name}{DeclaredInnerText.Replace(Environment.NewLine, Environment.NewLine + _defaultTabs)}\".Replace(\"<\", \">\"));")
                                        .AddLine("writer.Write($\"{_defaultTabs}{Name}{Environment.NewLine}\");")
                                        .AddLine("writer.Write($\"{_defaultTabs}{{{Environment.NewLine}\");")
                                        .AddLine("writer.Write($\"{_defaultTabs}{DeclaredInnerText.Replace(Environment.NewLine, Environment.NewLine + _defaultTabs)}{Environment.NewLine}\".Replace(\"<\", \">\"));")
                                        .AddLine("writer.Write($\"{_defaultTabs}}}\");")
                                    .Else
                                        .AddLine("writer.Write($\"{_defaultTabs}{Name}{Environment.NewLine}\");")
                                        .AddLine("writer.Write($\"{_defaultTabs}{{{Environment.NewLine}\");")
                                        .AddLine("writer.Write($\"{_defaultTabs}{DeclaredInnerText.Replace(Environment.NewLine, Environment.NewLine + _defaultTabs)}{Environment.NewLine}\");")
                                        .AddLine("writer.Write($\"{_defaultTabs}}}\");")
                                .EndIf)
                            .Else
                                .AddLine("if (!string.IsNullOrEmpty(Name))")
                                .AddLine("{")
                                .AddLine("    writer.Write($\"{_defaultTabs}{Name}{Environment.NewLine}\");")
                                .AddLine("    writer.Write($\"{_defaultTabs}{{{Environment.NewLine}\");")
                                .AddLine("}")
                                .AddCode(MyCodeExpressionBuilder.Start(2).NewLine
                                    .Foreach("var", "element", "InnerElements")
                                        .AddLine("element.Generate(writer, _defaultTabs == null ? \"\" : _defaultTabs + s_Tabs, forComments);")
                                        .AddLine("writer.Write($\"{Environment.NewLine}\");")
                                    .EndCycle)
                                .AddLine("if (!string.IsNullOrEmpty(Name))")
                                .AddLine("{")
                                .AddLine("    writer.Write($\"{_defaultTabs}}}\");")
                                .AddLine("}")
                            .EndIf
                        )
                    )
                    .AddMethod(MyMethodBuilder
                        .Public.Type("void").Name("GenerateRazorPage")
                        .AddComments([
                            "/// <summary>",
                            "/// Generate result text with TextWriter",
                            "/// </summary>"
                            ])
                        .Parameter("TextWriter", "writer")
                        .Parameter("string?", "_defaultTabs", " = null")
                        .Parameter("bool", "forComments", " = false")
                        .AddCode(MyCodeExpressionBuilder.Start(1).NewLine
                            .NewLine
                            .If("!string.IsNullOrEmpty(DeclaredInnerText)")
                            .CodeBlock.AddCode(MyCodeExpressionBuilder.Start(2)
                                .NewLine
                                .If("forComments")
                                    .CodeBlock
                                        .AddLine("writer.Write($\"{_defaultTabs}{DeclaredInnerText.Replace(Environment.NewLine, Environment.NewLine + _defaultTabs)}\".Replace(\"<\", \">\"));")
                                        .AddLine("writer.Write($\"{_defaultTabs}{Environment.NewLine}\");")
                                        .AddLine("writer.Write($\"{_defaultTabs}{{{Environment.NewLine}\");")
                                        .AddLine("writer.Write($\"{_defaultTabs}{DeclaredInnerText.Replace(Environment.NewLine, Environment.NewLine + _defaultTabs)}{Environment.NewLine}\".Replace(\"<\", \">\"));")
                                        .AddLine("writer.Write($\"{_defaultTabs}}}\");")
                                    .Else
                                        .AddLine("writer.Write($\"{_defaultTabs}{Environment.NewLine}\");")
                                        .AddLine("writer.Write($\"{_defaultTabs}{{{Environment.NewLine}\");")
                                        .AddLine("writer.Write($\"{_defaultTabs}{DeclaredInnerText.Replace(Environment.NewLine, Environment.NewLine + _defaultTabs)}{Environment.NewLine}\");")
                                        .AddLine("writer.Write($\"{_defaultTabs}}}\");")
                                .EndIf)
                            .Else
                                .AddCode(MyCodeExpressionBuilder.Start(2).NewLine
                                    .Foreach("var", "element", "InnerElements")
                                        .AddLine("element.Generate(writer, _defaultTabs == null ? \"\" : _defaultTabs, forComments);")
                                        .AddLine("writer.Write($\"{Environment.NewLine}\");")
                                    .EndCycle)
                            .EndIf
                        )
                    )
                )
                
                //All Tag classes
                .AddClasses(model.Tags.Select(tag => MyClassBuilder.Public
                    .Name($"{tag?.CsName ?? throw new ArgumentNullException("tag.Name")}Tag")
                    .Generic_T
                    .GenericWhere("T : Tag")
                    .BaseType("Tag")
                    .AddField(MyFieldBuilder.Private.Type("T").Name("_parent"))
                    .AddProperty(MyPropertyBuilder
                        .Public.Type($"T").Name($"{tag.CsName}_").GetOnly
                        .GetterExpression(MyCodeExpressionBuilder.Start(2)
                            ._.Add("get").CodeBlock._.Add($"return _parent;")._.FinishCodeBlock))
                    .AddProperties(tag.ChildTags.Select(childTag => MyPropertyBuilder
                        .Public.Type($"{childTag.CsName}Tag<{tag.CsName}Tag<T>>").Name($"{childTag.CsName}").GetOnly
                        .GetterExpression(MyCodeExpressionBuilder.Start(2)
                            ._.Add("get").CodeBlock._.Add($"return new (this);")._.FinishCodeBlock)))
                    .AddComments(["/// <summary>"
                                , "/// <para>Generated from HtmlSpecification</para>"
                                , "/// </summary>"])
                    .AddConstructor(MyConstructorBuilder
                        .Public
                        .Base($"(\"{tag.WriteName ?? tag.CsName}\")")
                        .AddLine($"TagType = TagTypes.{tag.TagType};")
                        )
                    .AddConstructor(MyConstructorBuilder
                        .Public
                        .Parameter("T parent")
                        .Base($"(\"{tag.WriteName ?? tag.CsName}\", parent)")
                        .AddLine("_parent = parent;")
                        .AddLine("_parent.AddInnerElement(this);")
                        .AddLine($"TagType = TagTypes.{tag.TagType};")
                        )
                    .AddMethod(MyMethodBuilder
                        .Public
                        .Type($"{tag.CsName}Tag<T>")
                        .Name("Attribute")
                        .Parameter("string name")
                        .Parameter("string value")
                        .AddLine("AddAttribute(name, value);")
                        .AddLine("return this;")
                        )
                    .AddMethod(MyMethodBuilder
                        .Public
                        .Type($"{tag.CsName}Tag<T>")
                        .Name("Attributes")
                        .Parameter("Dictionary<string, string> attributes")
                        .AddLine("foreach (var attribute in attributes)")
                        .AddLine("{")
                        .AddLine("    AddAttribute(attribute.Key, attribute.Value);")
                        .AddLine("}")
                        .AddLine("return this;")
                        )
                    .AddMethod(MyMethodBuilder
                        .Public
                        .Type($"{tag.CsName}Tag<T>")
                        .Name("InnerText")
                        .Parameter("string innerText")
                        .AddLine("AddInnerText(innerText);")
                        .AddLine("return this;")
                        )
                    .AddMethod(MyMethodBuilder
                        .Public
                        .Type("T")
                        .Name("InnerTag")
                        .Parameter("HtmlBuilder tag")
                        .AddLine("AddInnerElement(tag);")
                        .AddLine("return _parent;")
                        )
                    .AddMethod(MyMethodBuilder
                        .Public
                        .Type("T")
                        .Name("InnerTags")
                        .Parameter("IEnumerable<HtmlBuilder> tags")
                        .AddLine("foreach (var tag in tags)")
                        .AddLine("{")
                        .AddLine("    AddInnerElement(tag);")
                        .AddLine("}")
                        .AddLine("return _parent;")
                        )
                    .AddMethods(tag.ChildTags.Select(childTag => MyMethodBuilder
                        .Public.Type($"{childTag.CsName}Tag<{tag.CsName}Tag<T>>").Name($"{childTag.NameUpper1}")
                        .Parameter(string.Join(", ", new string[] { "string? innerText = null" }.Union(childTag?.Attributes?.Select(o=> $"string? _{o} = null").ToArray() ?? [])))
                        .AddLine($"{childTag.CsName}Tag<{tag.CsName}Tag<T>> result = new(this);")
                        .AddLine("if (innerText != null) result.AddInnerText(innerText);")
                        .AddLines(childTag?.Attributes?.Select(attr => $"if (_{attr} != null) result.AddAttribute(\"{attr}\", _{attr});") ?? [])
                        .AddLine("return result;")))
                    .AddMethod(MyMethodBuilder
                        .Public.Type($"tagTag<{tag.CsName}Tag<T>>").Name($"Tag")
                        .Parameter("string tagName, string? innerText = null")
                        .AddLine($"tagTag<{tag.CsName}Tag<T>> result = new(tagName, this);")
                        .AddLine("if (innerText != null) result.AddInnerText(innerText);")
                        .AddLine("return result;"))
                ))

                //Tag<T>
                .AddClass(MyClassBuilder.Public
                    .Name("tagTag")
                    .Generic_T
                    .GenericWhere("T : Tag")
                    .BaseType("Tag")
                    .AddField(MyFieldBuilder.Private.Type("T").Name("_parent"))
                    .AddProperty(MyPropertyBuilder
                        .Public.Type($"T").Name($"tag_").GetOnly
                        .GetterExpression(MyCodeExpressionBuilder.Start(2)
                            ._.Add("get").CodeBlock._.Add($"return _parent;")._.FinishCodeBlock))
                    .AddProperties(model.Tags.Find(o=>o.CsName == "html").ChildTags.Select(childTag => MyPropertyBuilder
                        .Public.Type($"{childTag.CsName}Tag<tagTag<T>>").Name($"{childTag.CsName}").GetOnly
                        .GetterExpression(MyCodeExpressionBuilder.Start(2)
                            ._.Add("get").CodeBlock._.Add($"return new (this);")._.FinishCodeBlock)))
                    .AddComments(["/// <summary>"
                                , "/// <para>Generated from HtmlSpecification</para>"
                                , "/// </summary>"])
                    .AddConstructor(MyConstructorBuilder
                        .Public
                        .Parameter("string tagName")
                        .Base($"(tagName)")
                        .AddLine($"TagType = TagTypes.{tag.TagTypes.HtmlTag};")
                        )
                    .AddConstructor(MyConstructorBuilder
                        .Public
                        .Parameter("string tagName")
                        .Parameter("T parent")
                        .Base($"(tagName, parent)")
                        .AddLine("_parent = parent;")
                        .AddLine("_parent.AddInnerElement(this);")
                        .AddLine($"TagType = TagTypes.{tag.TagTypes.HtmlTag};")
                        )
                    .AddMethod(MyMethodBuilder
                        .Public
                        .Type($"tagTag<T>")
                        .Name("Attribute")
                        .Parameter("string name")
                        .Parameter("string value")
                        .AddLine("AddAttribute(name, value);")
                        .AddLine("return this;")
                        )
                    .AddMethod(MyMethodBuilder
                        .Public
                        .Type($"tagTag<T>")
                        .Name("Attributes")
                        .Parameter("Dictionary<string, string> attributes")
                        .AddLine("foreach (var attribute in attributes)")
                        .AddLine("{")
                        .AddLine("    AddAttribute(attribute.Key, attribute.Value);")
                        .AddLine("}")
                        .AddLine("return this;")
                        )
                    .AddMethod(MyMethodBuilder
                        .Public
                        .Type($"tagTag<T>")
                        .Name("InnerText")
                        .Parameter("string innerText")
                        .AddLine("AddInnerText(innerText);")
                        .AddLine("return this;")
                        )
                    .AddMethod(MyMethodBuilder
                        .Public
                        .Type("T")
                        .Name("InnerTag")
                        .Parameter("HtmlBuilder tag")
                        .AddLine("AddInnerElement(tag);")
                        .AddLine("return _parent;")
                        )
                    .AddMethod(MyMethodBuilder
                        .Public
                        .Type("T")
                        .Name("InnerTags")
                        .Parameter("IEnumerable<HtmlBuilder> tags")
                        .AddLine("foreach (var tag in tags)")
                        .AddLine("{")
                        .AddLine("    AddInnerElement(tag);")
                        .AddLine("}")
                        .AddLine("return _parent;")
                        )
                    .AddMethods(model.Tags.Find(o => o.CsName == "html").ChildTags.Select(childTag => MyMethodBuilder
                        .Public.Type($"{childTag.CsName}Tag<tagTag<T>>").Name($"{childTag.NameUpper1}")
                        .Parameter(string.Join(", ", new string[] { "string? innerText = null" }.Union(childTag?.Attributes?.Select(o => $"string? _{o} = null").ToArray() ?? [])))
                        .AddLine($"{childTag.CsName}Tag<tagTag<T>> result = new(this);")
                        .AddLine("if (innerText != null) result.AddInnerText(innerText);")
                        .AddLines(childTag?.Attributes?.Select(attr => $"if (_{attr} != null) result.AddAttribute(\"{attr}\", _{attr});") ?? [])
                        .AddLine("return result;")))
                    .AddMethod(MyMethodBuilder
                        .Public.Type($"tagTag<tagTag<T>>").Name($"Tag")
                        .Parameter("string tagName, string? innerText = null")
                        .AddLine($"tagTag<tagTag<T>> result = new(tagName, this);")
                        .AddLine("if (innerText != null) result.AddInnerText(innerText);")
                        .AddLine("return result;")
                )

            ).Save();

        }
    }
}
