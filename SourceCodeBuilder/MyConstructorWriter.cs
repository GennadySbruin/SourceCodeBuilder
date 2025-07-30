using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;

namespace SourceCodeBuilder
{
    public class MyConstructorWriter : ICodeWriter<MyConstructor>
    {
        private static ICodeWriter<MyConstructor>? s_instance;
        private readonly System.Threading.Lock _writerLock = new();
        /// <summary>
        /// Reusable static formatter
        /// </summary>
        public static ICodeWriter<MyConstructor> Formatter
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new MyConstructorWriter();
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
        public string WriteCode(MyConstructor o)
        {
            if(o == null)
            {
                throw new ArgumentNullException("MyConstructor o");
            }
            using MemoryStream memoryStream = new MemoryStream();
            using StreamWriter streamWriter = new StreamWriter(memoryStream);
            GenerateCode(o, streamWriter);
            streamWriter.Flush();
            return Encoding.ASCII.GetString(memoryStream.ToArray());
        }

        TextWriter? _writer = null;
        public void GenerateCode(MyConstructor o, TextWriter writer, string tabs = "", bool forComments = false)
        {
            if (o == null)
            {
                throw new ArgumentNullException("MyConstructor o");
            }
            lock (_writerLock)
            {
                Clear();
                _parentTabs = tabs;
                _writer = writer;
                SetComments(o, o.ClassName, forComments);
                SetAttributes(o);
                SetAccessModifiers(o);
                SetName(o.ClassName);
                SetParameters(o);
                SetBase(o);
                SetLambdaExpression(o);
                SetBaseConstructor(o);
                SetBody(o);
            }
        }

        public virtual void SetComments(MyConstructor o, string className, bool forComments)
        {
            if (forComments)
            {
                return;
            }

            if (o.AutoGenerateComments)
            {
                GenerateComments(o, className);
            }
            else
            {
                if(o.Comments?.Count > 0)
                {
                    foreach (var c in o.Comments ?? [])
                    {
                        string comment = c.StartsWith("//") ? c : $"//{c}";
                        Write($"{Environment.NewLine}{comment}");
                    }
                    _writer?.Write(Environment.NewLine);
                }
            }
        }

        public virtual void SetAttributes(MyConstructor o)
        {
            if (o.Attributes?.Count > 0)
            {
                foreach (var attribute in o.Attributes ?? [])
                {
                    Write($"{Environment.NewLine}{attribute}");
                }
                _writer?.Write(Environment.NewLine);
            }
        }

        public virtual void SetAccessModifiers(MyConstructor o)
        {
            foreach(var a in o.AccessModifiersList)
            {
                Write($"{_}{AccessModifierToString(a)}");
            }
        }

        public virtual void SetLambdaExpression(MyConstructor o)
        {
            if (string.IsNullOrEmpty(o.LambdaExpression))
            {
                return;
            }
            Write($"{_}{o.LambdaExpression}");
        }

        public virtual void SetBaseConstructor(MyConstructor o)
        {
            if (string.IsNullOrEmpty(o.BaseConstructorExpression))
            {
                return;
            }
            Write($" :{_}{o.BaseConstructorExpression}");
        }

        public virtual void SetParameters(MyConstructor o)
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

        public virtual void SetBase(MyConstructor o)
        {
            if (!string.IsNullOrEmpty(o.Base))
            {
                Write($" : base{o.Base}");
            }
            
        }

        public virtual void SetBody(MyConstructor o)
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

        public virtual void SetName(string className)
        {
            Write($"{_}{className}");
        }

        public virtual string? AccessModifierToString(MyConstructor.AccessModifiers? accessModifier)
        {
            return accessModifier?.ToString()?.ToLower();
        }

        public virtual void Write(string value)
        {
            _writer?.Write(value.Replace(Environment.NewLine, Environment.NewLine +_parentTabs));
        }

        private void GenerateComments(MyConstructor o, string className)
        {
            _writer.Write(Environment.NewLine + _parentTabs + "/// <summary>");
            foreach (var c in o.Comments ?? [])
            {
                string comment = c.StartsWith("//") ? c : $"//{c}";
                _writer.Write(Environment.NewLine + _parentTabs + comment);
            }
            _writer.Write(Environment.NewLine + _parentTabs + "/// <example>");
            _writer.Write(Environment.NewLine + _parentTabs + "/// <code>");
            //Write($"{Environment.NewLine}");
            _writer?.Write(Environment.NewLine);
            MyConstructorWriter myConstructorWriter = new MyConstructorWriter();
            myConstructorWriter.GenerateCode(o, _writer, _parentTabs + "/// ", true);
            _writer.Write(Environment.NewLine + _parentTabs + "/// </code>");
            _writer.Write(Environment.NewLine + _parentTabs + "/// </example>");
            _writer.Write(Environment.NewLine + _parentTabs + "/// </summary>");
            _writer?.Write(Environment.NewLine);
        }
    }
}
