using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SourceCodeBuilder
{
    public class MyClassWriter : ICodeWriter<MyClass>
    {
        private static ICodeWriter<MyClass>? s_instance;
        private readonly System.Threading.Lock _writerLock = new();

        public MyPropertyWriter PropertyWriter { get; set; } = new MyPropertyWriter();
        public MyFieldWriter FieldWriter { get; set; } = new MyFieldWriter();
        public MyMethodWriter MethodWriter { get; set; } = new MyMethodWriter();

        /// <summary>
        /// Reusable static formatter
        /// </summary>
        public static ICodeWriter<MyClass> Formatter
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new MyClassWriter();
                }
                s_instance.Clear();
                return s_instance;
            }
        }

        private bool _hasValue;
        public string _defaultTabs = "    ";
        public string _parentTabs = string.Empty;

        private const string Space = " ";
        private string _
        {
            get
            {
                if (_hasValue)
                {
                    return Space;
                }
                else
                {
                    _hasValue = true;
                    return _parentTabs;
                }
            }
        }

        public void Clear()
        {
            _hasValue = false;
        }
        public string WriteCode(MyClass o)
        {
            if(o == null)
            {
                throw new ArgumentNullException("MyClass o");
            }
            using MemoryStream memoryStream = new MemoryStream();
            using StreamWriter streamWriter = new StreamWriter(memoryStream);
            GenerateCode(o, streamWriter);
            streamWriter.Flush();
            return Encoding.ASCII.GetString(memoryStream.ToArray());
        }

        TextWriter? _writer;
        public void GenerateCode(MyClass o, TextWriter writer, string tabs = "")
        {
            if (o == null)
            {
                throw new ArgumentNullException("MyClass o");
            }
            lock (_writerLock)
            {
                Clear();
                _parentTabs = tabs;
                _writer = writer;
                SetComments(o);
                SetAccessModifiers(o);
                SetType(o);
                SetName(o);
                SetGeneric(o);
                SetBase(o);
                SetStartMembers(o);
                SetMembers(o);
                SetEndMembers(o);
            }
        }

        public virtual void SetComments(MyClass o)
        {
            foreach (var c in o.Comments ?? [])
            {
                string comment = c.StartsWith("//") ? c : $"//{c}";
                Write($"{Environment.NewLine}{comment}");
            }
        }
        public virtual void SetAccessModifiers(MyClass o)
        {
            foreach(var a in o.AccessModifiersList)
            {
                Write($"{_}{AccessModifierToString(a)}");
            }
        }

        public virtual void SetType(MyClass o)
        {
            Write($"{_}class");
        }

        public virtual void SetName(MyClass o)
        {
            Write($"{_}{o.ClassName}");
        }

        public virtual void SetGeneric(MyClass o)
        {
            if (o.GenericList == null || o.GenericList.Count == 0)
            {
                return;
            }
            Write($"<");

            string q = string.Empty;
            foreach (string baseClass in o.GenericList)
            {
                Write($"{q}{baseClass}");
                q = $",{_}";
            }
            Write($">");
        }

        public virtual void SetBase(MyClass o)
        {
            if(o.BaseClassList == null || o.BaseClassList.Count == 0)
            {
                return;
            }
            Write($"{_}:");

            string q = string.Empty;
            foreach(string baseClass in o.BaseClassList)
            {
                Write($"{q}{_}{baseClass}");
                q = ",";
            }
        }

        public virtual void SetStartMembers(MyClass o)
        {
            Write(Environment.NewLine);
            Write("{");
        }

        public virtual void SetMembers(MyClass o)
        {
            foreach(MyField field in o.Fields ?? [])
            {
                //Write(Environment.NewLine);
                _writer?.Write(Environment.NewLine);
                FieldWriter.GenerateCode(field, _writer, _parentTabs + _defaultTabs);
            }
            foreach (MyProperty property in o.Properties ?? [])
            {
                //Write(Environment.NewLine);
                _writer?.Write(Environment.NewLine);
                PropertyWriter.GenerateCode(property, _writer, _parentTabs + _defaultTabs);
            }
            foreach (MyMethod method in o.Methods ?? [])
            {
                //Write(Environment.NewLine);
                _writer?.Write(Environment.NewLine);
                MethodWriter.GenerateCode(method, _writer, _parentTabs + _defaultTabs);
            }
        }

        public virtual void SetEndMembers(MyClass o)
        {
            Write(Environment.NewLine);
            Write("}");
        }

        public virtual string? AccessModifierToString(MyClass.AccessModifiers? accessModifier)
        {
            return accessModifier?.ToString()?.ToLower();
        }

        public virtual void Write(string value)
        {
            _writer?.Write(value.Replace(Environment.NewLine, Environment.NewLine + _parentTabs));
        }
    }
}
