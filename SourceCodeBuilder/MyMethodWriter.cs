using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SourceCodeBuilder
{
    public class MyMethodWriter : ICodeWriter<MyMethod>
    {
        private static ICodeWriter<MyMethod>? s_instance;
        private readonly System.Threading.Lock _writerLock = new();
        /// <summary>
        /// Reusable static formatter
        /// </summary>
        public static ICodeWriter<MyMethod> Formatter
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new MyMethodWriter();
                }
                s_instance.Clear();
                return s_instance;
            }
        }

        private bool _hasValue;
        public string _defaultTabs = "    ";
        public string _parentTabs = string.Empty;

        private const string Space = " ";
        private const string defaultStartCodeBlockStatement = "{";
        private const string defaultEndCodeBlockStatement = "}";
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
        public string WriteCode(MyMethod o)
        {
            if(o == null)
            {
                throw new ArgumentNullException("MyMethod o");
            }
            using MemoryStream memoryStream = new MemoryStream();
            using StreamWriter streamWriter = new StreamWriter(memoryStream);
            GenerateCode(o, streamWriter);
            streamWriter.Flush();
            return Encoding.ASCII.GetString(memoryStream.ToArray());
        }

        TextWriter? _writer = null;
        public void GenerateCode(MyMethod o, TextWriter writer, string tabs = "")
        {
            if (o == null)
            {
                throw new ArgumentNullException("MyMethod o");
            }
            lock (_writerLock)
            {
                Clear();
                _parentTabs = tabs;
                _writer = writer;
                SetAccessModifiers(o);
                SetAsync(o);
                SetType(o);
                SetName(o);
                SetParameters(o);
                SetLambdaExpression(o);
                SetBody(o);
            }
        }

        public void GenerateCodeForInterface(MyMethod o, TextWriter writer, string tabs = "")
        {
            if (o == null)
            {
                throw new ArgumentNullException("MyMethod o");
            }
            lock (_writerLock)
            {
                Clear();
                _parentTabs = tabs;
                _writer = writer;
                SetType(o);
                SetName(o);
                SetParameters(o);
            }
        }

        public virtual void SetAccessModifiers(MyMethod o)
        {
            foreach(var a in o.AccessModifiersList)
            {
                Write($"{_}{AccessModifierToString(a)}");
            }
        }

        public virtual void SetAsync(MyMethod o)
        {
            if (o.Async)
            {
                Write($"{_}async");
            }
            
        }
        public virtual void SetType(MyMethod o)
        {
            Write($"{_}{o.MethodReturnTypeName}");
        }

        public virtual void SetLambdaExpression(MyMethod o)
        {
            if (string.IsNullOrEmpty(o.LambdaExpression))
            {
                return;
            }
            Write($"{_}{o.LambdaExpression}");
        }

        public virtual void SetParameters(MyMethod o)
        {
            Write($"(");
            string q = string.Empty;
            foreach(var p in o.Parametrs ?? [])
            {
                Write($"{q}{p}");
                q = ", ";
            }
            Write($")");
        }

        public virtual void SetBody(MyMethod o)
        {
            if (!string.IsNullOrEmpty(o.LambdaExpression))
            {
                return;
            }
            if (o.MyCode.HasCode())
            {
                Write(Environment.NewLine);
                Write(defaultStartCodeBlockStatement);
                o.MyCode.BuildCode(_writer, _parentTabs);
                Write(Environment.NewLine);
                Write(defaultEndCodeBlockStatement);
            }
            else
            {
                Write($"{_}{defaultStartCodeBlockStatement}");
                Write($"{_}{defaultEndCodeBlockStatement}");
            }
        }

        public virtual void SetName(MyMethod o)
        {
            Write($"{_}{o.MethodName}");
        }

        public virtual string? AccessModifierToString(MyMethod.AccessModifiers? accessModifier)
        {
            return accessModifier?.ToString()?.ToLower();
        }

        public virtual void Write(string value)
        {
            _writer?.Write(value.Replace(Environment.NewLine, Environment.NewLine +_parentTabs));
        }
    }
}
