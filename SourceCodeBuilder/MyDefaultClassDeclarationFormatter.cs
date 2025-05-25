using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SourceCodeBuilder
{
    public class MyDefaultClassDeclarationFormatter : IFormatter<MyClass>
    {
        private static IFormatter<MyClass>? s_instance;
        private readonly System.Threading.Lock _writerLock = new();
        /// <summary>
        /// Reusable static formatter
        /// </summary>
        public static IFormatter<MyClass> Formatter
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new MyDefaultClassDeclarationFormatter();
                }
                s_instance.Clear();
                return s_instance;
            }
        }

        private bool _hasValue;
        private string _defaultTabs { get; set; }

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
                    return _defaultTabs;
                }
            }
        }

        public void Clear()
        {
            _hasValue = false;
        }
        public string ToString(MyClass o)
        {
            if(o == null)
            {
                throw new ArgumentNullException("MyField o");
            }
            using MemoryStream memoryStream = new MemoryStream();
            using StreamWriter streamWriter = new StreamWriter(memoryStream);
            GenerateCode(o, streamWriter);
            streamWriter.Flush();
            return Encoding.ASCII.GetString(memoryStream.ToArray());
        }

        TextWriter _writer;
        public void GenerateCode(MyClass o, TextWriter writer, string tabs = "")
        {
            if (o == null)
            {
                throw new ArgumentNullException("MyField o");
            }
            lock (_writerLock)
            {
                Clear();
                _defaultTabs = tabs;
                _writer = writer;
                WriteCode(o);
            }
        }

        private void WriteCode(MyClass o)
        {
            SetAccessModifiers(o);
            SetType(o);
            SetName(o);
            SetGeneric(o);
            SetBase(o);
            SetStartMembers(o);
            SetMembers(o);
            SetEndMembers(o);
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
            Write(_defaultTabs + "{");
        }

        public virtual void SetMembers(MyClass o)
        {
            foreach(MyField field in o.Fields ?? [])
            {
                Write(Environment.NewLine);
                Write("    " + _defaultTabs + field.ToString());
            }
            foreach (MyProperty property in o.Properties ?? [])
            {
                Write(Environment.NewLine);
                Write("    " + _defaultTabs + property.ToString());
            }
            foreach (MyMethod method in o.Methods ?? [])
            {
                Write(Environment.NewLine);
                //method.MyCode.BuildCode(_writer);
                new MyDefaultMethodDeclarationFormatter().GenerateCode(method, _writer, "    " + _defaultTabs);
            }
        }

        public virtual void SetEndMembers(MyClass o)
        {
            Write(Environment.NewLine);
            Write(_defaultTabs + "}");
        }

        public virtual string? AccessModifierToString(MyClass.AccessModifiers? accessModifier)
        {
            return accessModifier?.ToString()?.ToLower();
        }

        private void Write(string value)
        {
            _writer.Write(value.Replace(Environment.NewLine, Environment.NewLine + _defaultTabs));
        }
    }
}
