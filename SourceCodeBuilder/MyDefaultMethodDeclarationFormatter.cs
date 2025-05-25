using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SourceCodeBuilder
{
    public class MyDefaultMethodDeclarationFormatter : IFormatter<MyMethod>
    {
        private static IFormatter<MyMethod>? s_instance;
        private readonly System.Threading.Lock _writerLock = new();
        /// <summary>
        /// Reusable static formatter
        /// </summary>
        public static IFormatter<MyMethod> Formatter
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new MyDefaultMethodDeclarationFormatter();
                }
                s_instance.Clear();
                return s_instance;
            }
        }

        private bool _hasValue;
        public virtual string _defaultTabs { get; set; }

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
                    return _defaultTabs;
                }
            }
        }
        public void Clear()
        {
            _hasValue = false;
        }
        public string ToString(MyMethod o)
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

        TextWriter _writer;
        public void GenerateCode(MyMethod o, TextWriter writer, string tabs = "")
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

        private void WriteCode(MyMethod o)
        {
            SetAccessModifiers(o);
            SetType(o);
            SetName(o);
            SetParameters(o);
            SetLambdaExpression(o);
            SetBody(o);
        }

        public virtual void SetAccessModifiers(MyMethod o)
        {
            foreach(var a in o.AccessModifiersList)
            {
                Write($"{_}{AccessModifierToString(a)}");
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
                Write(_defaultTabs + defaultStartCodeBlockStatement);
                //Write(_defaultTabs + o.ToString());
                o.MyCode.BuildCode(_writer, _defaultTabs);
                Write(Environment.NewLine);
                Write(_defaultTabs + defaultEndCodeBlockStatement);
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

        private void Write(string value)
        {
            if(value == Environment.NewLine)
            {
                _writer.Write(value);
            }
            else
            {
                _writer.Write(value.Replace(Environment.NewLine, Environment.NewLine + _defaultTabs));
            }
            
        }
    }
}
