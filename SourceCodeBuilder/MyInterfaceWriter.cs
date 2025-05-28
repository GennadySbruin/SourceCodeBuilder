using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SourceCodeBuilder
{
    public class MyInterfaceWriter : ICodeWriter<MyInterface>
    {
        private static ICodeWriter<MyInterface>? s_instance;
        private readonly System.Threading.Lock _writerLock = new();
        public MyPropertyWriter PropertyWriter { get; set; } = new MyPropertyWriter();
        public MyFieldWriter FieldWriter { get; set; } = new MyFieldWriter();
        public MyMethodWriter MethodWriter { get; set; } = new MyMethodWriter();
        /// <summary>
        /// Reusable static formatter
        /// </summary>
        public static ICodeWriter<MyInterface> Formatter
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new MyInterfaceWriter();
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
        public string WriteCode(MyInterface o)
        {
            if(o == null)
            {
                throw new ArgumentNullException("MyInterface o");
            }
            using MemoryStream memoryStream = new MemoryStream();
            using StreamWriter streamWriter = new StreamWriter(memoryStream);
            GenerateCode(o, streamWriter);
            streamWriter.Flush();
            return Encoding.ASCII.GetString(memoryStream.ToArray());
        }

        TextWriter? _writer = null;
        public void GenerateCode(MyInterface o, TextWriter writer, string tabs = "")
        {
            if (o == null)
            {
                throw new ArgumentNullException("MyInterface o");
            }
            lock (_writerLock)
            {
                Clear();
                _parentTabs = tabs;
                _writer = writer;
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

        public virtual void SetAccessModifiers(MyInterface o)
        {
            foreach(var a in o.AccessModifiersList)
            {
                Write($"{_}{AccessModifierToString(a)}");
            }
        }

        public virtual void SetType(MyInterface o)
        {
            Write($"{_}interface");
        }

        public virtual void SetName(MyInterface o)
        {
            Write($"{_}{o.InterfaceName}");
        }

        public virtual void SetGeneric(MyInterface o)
        {
            if (o.GenericList == null || o.GenericList.Count == 0)
            {
                return;
            }
            Write($"<");

            string q = string.Empty;
            foreach (string baseInterface in o.GenericList)
            {
                Write($"{q}{baseInterface}");
                q = $",{_}";
            }
            Write($">");
        }

        public virtual void SetBase(MyInterface o)
        {
            if(o.BaseInterfaceList == null || o.BaseInterfaceList.Count == 0)
            {
                return;
            }
            Write($"{_}:");

            string q = string.Empty;
            foreach(string baseInterface in o.BaseInterfaceList)
            {
                Write($"{q}{_}{baseInterface}");
                q = ",";
            }
        }

        public virtual void SetStartMembers(MyInterface o)
        {
            Write(Environment.NewLine);
            Write("{");
        }

        public virtual void SetMembers(MyInterface o)
        {
            foreach (MyProperty property in o.Properties ?? [])
            {
                Write(Environment.NewLine);
                PropertyWriter.GenerateCodeForInterface(property, _writer, _parentTabs + _defaultTabs);
            }
            foreach (MyMethod method in o.Methods ?? [])
            {
                Write(Environment.NewLine);
                MethodWriter.GenerateCodeForInterface(method, _writer, _parentTabs + _defaultTabs);
            }
        }

        public virtual void SetEndMembers(MyInterface o)
        {
            Write(Environment.NewLine);
            Write("}");
        }

        public virtual string? AccessModifierToString(MyInterface.AccessModifiers? accessModifier)
        {
            return accessModifier?.ToString()?.ToLower();
        }

        public virtual void Write(string value)
        {
            _writer?.Write(value.Replace(Environment.NewLine, Environment.NewLine + _parentTabs ));
        }
    }
}
