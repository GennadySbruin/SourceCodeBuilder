using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SourceCodeBuilder
{
    public class MyFieldWriter : ICodeWriter<MyField>
    {
        private static ICodeWriter<MyField>? s_instance;
        private readonly System.Threading.Lock _writerLock = new();
        /// <summary>
        /// Reusable static formatter
        /// </summary>
        public static ICodeWriter<MyField> Formatter
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new MyFieldWriter();
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
        public string WriteCode(MyField o)
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

        TextWriter? _writer = null;
        public void GenerateCode(MyField o, TextWriter writer, string tabs = "")
        {
            if (o == null)
            {
                throw new ArgumentNullException("MyField o");
            }
            lock (_writerLock)
            {
                Clear();
                _parentTabs = tabs;
                _writer = writer;
                SetAccessModifiers(o);
                SetType(o);
                SetName(o);
                StartDefaultSetter(o);
                EndSetter(o);
            }
        }


        public virtual void SetAccessModifiers(MyField o)
        {
            foreach(var a in o.AccessModifiersList)
            {
                Write($"{_}{AccessModifierToString(a)}");
            }
        }

        public virtual void SetType(MyField o)
        {
            Write($"{_}{o.FieldTypeName}");
        }

        public virtual void SetName(MyField o)
        {
            Write($"{_}{o.FieldName}");
        }

        public virtual void StartDefaultSetter(MyField o)
        {
            if(o.InitialExpression == null)
            {
                return;
            }
            Write($"{_}={_}{o.InitialExpression}");
        }

        public virtual void EndSetter(MyField o)
        {
            Write(";");
        }

        public virtual string? AccessModifierToString(MyField.AccessModifiers? accessModifier)
        {
            return accessModifier?.ToString()?.ToLower();
        }

        public virtual void Write(string value)
        {
            _writer?.Write(value.Replace(Environment.NewLine, Environment.NewLine + _parentTabs));
        }
    }
}
