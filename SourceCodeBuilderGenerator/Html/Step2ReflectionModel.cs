using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilderGenerator.Html
{
    internal class Step2ReflectionModel
    {
        internal List<tag> Tags { get; set; } = [];

        public Step2ReflectionModel(Type specificationClass)
        {
            ReflectTags(specificationClass);
            PrepareChilds();
        }

        private void ReflectTags(Type specificationClass)
        {
            Assembly assembly = GetType().Assembly;
            var instance = assembly.CreateInstance(specificationClass.FullName);
            foreach (FieldInfo field in specificationClass.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                tag? value = (tag?)field.GetValue(instance);
                if (value == null)
                {
                    value = new tag(field.Name);
                    field.SetValue(instance, value);
                }
                else
                {
                    value.CsName ??= field.Name;
                }
                if (value.UseGlobalAttributes)
                {
                    if (value.Attributes == null)
                    {
                        value.Attributes = [];
                    }
                    value.Attributes.AddRange(tag.GlobalAtributes);
                }
                if (value.UseEventsAttributes)
                {
                    if (value.Attributes == null)
                    {
                        value.Attributes = [];
                    }
                    value.Attributes.AddRange(tag.EventAtributes);
                }
                value.WriteName ??= value.CsName;
                Tags.Add(value ?? throw new ArgumentNullException("null tag"));
            }
        }

        private void PrepareChilds()
        {
            foreach (tag t in Tags) 
            {
                t.ChildTags = [];
                if (t.WithUserChildTags)
                {
                    foreach (string usertag in t.GetUserChildTags)
                    {
                        t.ChildTags.AddRange(Tags.Where(o => o.CsName == usertag));
                    }
                }
                else
                {
                    foreach(var tag in Tags)
                    {
                        if(tag.UserParentTags == null || tag.UserParentTags?.Count == 0)
                        {
                            t.ChildTags.Add(tag);
                        }
                        else if (tag?.UserParentTags?.Where(o => o == t.CsName).Any() ?? false)
                        {
                            t.ChildTags.Add(tag);
                        }
                    }
                }
            }
        }
    }
}
