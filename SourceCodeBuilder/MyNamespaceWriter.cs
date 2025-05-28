using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SourceCodeBuilder
{
    public class MyNamespaceWriter : ICodeWriter<MyNamespace>
    {
        private static ICodeWriter<MyNamespace>? s_instance;
        private readonly System.Threading.Lock _writerLock = new();
        public MyInterfaceWriter InterfaceWriter { get; set; } = new MyInterfaceWriter();
        public MyClassWriter ClassWriter { get; set; } = new MyClassWriter();

        /// <summary>
        /// Reusable static formatter
        /// </summary>
        public static ICodeWriter<MyNamespace> Formatter
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new MyNamespaceWriter();
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
        public string WriteCode(MyNamespace o)
        {
            if(o == null)
            {
                throw new ArgumentNullException("MyNamespace o");
            }
            using MemoryStream memoryStream = new MemoryStream();
            using StreamWriter streamWriter = new StreamWriter(memoryStream);
            GenerateCode(o, streamWriter);
            streamWriter.Flush();
            return Encoding.ASCII.GetString(memoryStream.ToArray());
        }

        TextWriter? _writer = null;
        public void GenerateCode(MyNamespace o, TextWriter writer, string tabs = "")
        {
            if (o == null)
            {
                throw new ArgumentNullException("MyNamespace o");
            }
            lock (_writerLock)
            {
                Clear();
                _parentTabs = tabs;
                _writer = writer;
                SetUsing(o);
                SetName(o);
                SetStartMembers(o);
                SetMembers(o);
                SetEndMembers(o);
            }
        }

        public virtual void SetUsing(MyNamespace o)
        {
            foreach (var u in o.Usings) 
            {
                Write(Environment.NewLine);
                Write($"Using {u};");
            }
            
        }
        public virtual void SetStartMembers(MyNamespace o)
        {
            Write(Environment.NewLine);
            Write(_parentTabs + "{");
        }
        public virtual void SetName(MyNamespace o)
        {
            Write($"{_}{o.NamespaceName}");
        }
        public virtual void SetMembers(MyNamespace o)
        {
            foreach (MyInterface myInterface in o.InterfaceList ?? [])
            {
                //Write(Environment.NewLine);
                _writer?.Write(Environment.NewLine);
                InterfaceWriter.GenerateCode(myInterface, _writer, _parentTabs + _defaultTabs);
            }
            foreach (MyClass myClass in o.ClassList ?? [])
            {
                //Write(Environment.NewLine);
                _writer?.Write(Environment.NewLine);
                ClassWriter.GenerateCode(myClass, _writer, _parentTabs +  _defaultTabs);
            }
        }
        public virtual void SetEndMembers(MyNamespace o)
        {
            Write(Environment.NewLine);
            Write(_parentTabs + "}");
        }
        public virtual void Write(string value)
        {
            _writer?.Write(value.Replace(Environment.NewLine, Environment.NewLine + _parentTabs));
        }
    }
}
