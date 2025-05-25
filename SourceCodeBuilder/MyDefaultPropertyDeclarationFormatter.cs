using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SourceCodeBuilder
{
    public class MyDefaultPropertyDeclarationFormatter : IFormatter<MyProperty>
    {
        private static IFormatter<MyProperty>? s_instance;
        private readonly System.Threading.Lock _writerLock = new();
        /// <summary>
        /// Reusable static formatter
        /// </summary>
        public static IFormatter<MyProperty> Formatter
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new MyDefaultPropertyDeclarationFormatter();
                }
                s_instance.Clear();
                return s_instance;
            }
        }

        private bool _hasValue;
        private string _defaultTabs { get; set; }

        private const string Space = " ";
        private const string defaultStartCodeBlockStatement = "{";
        private const string defaultEndCodeBlockStatement = "}";

        private StringBuilder _stringBuilder;
        private TextWriter _textWriter;
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
        public string ToString(MyProperty o)
        {
            if(o == null)
            {
                throw new ArgumentNullException("MyProperty o");
            }

            using MemoryStream memoryStream = new MemoryStream();
            using StreamWriter streamWriter = new StreamWriter(memoryStream);
            GenerateCode(o, streamWriter);
            streamWriter.Flush();
            return Encoding.ASCII.GetString(memoryStream.ToArray());
        }

        TextWriter _writer;
        public void GenerateCode(MyProperty o, TextWriter writer, string tabs = "")
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
        private void WriteCode(MyProperty o)
        {
            SetAccessModifiers(o);
            SetType(o);
            SetName(o);
            StartGetterSetterBlock(o);
            SetGetter(o);
            SetSetter(o);
            FinishGetterSetterBlock(o);
            StartDefaultSetter(o);
        }
        public virtual void SetAccessModifiers(MyProperty o)
        {
            foreach(var a in o.AccessModifiersList)
            {
                Write($"{_}{AccessModifierToString(a)}");
            }
        }

        public virtual void SetType(MyProperty o)
        {
            Write($"{_}{o.PropertyTypeName}");
        }

        public virtual void SetName(MyProperty o)
        {
            Write($"{_}{o.PropertyName}");
        }

        public virtual void StartGetterSetterBlock(MyProperty o)
        {
            if (o.ExpandCodeBlock)
            {
                Write(Environment.NewLine);
                Write($"{_defaultTabs}{defaultStartCodeBlockStatement}");
            }
            else
            {
                Write($"{_}{defaultStartCodeBlockStatement}");
            }
            
        }

        public virtual void SetGetter(MyProperty o)
        {
            if (o.WithGetter)
            {
                if (string.IsNullOrEmpty(o.GetterExpression))
                {
                    string defaultGetStatement = "get;";
                    Write($"{_}{defaultGetStatement}");
                }
                else
                {
                    if (o.ExpandCodeBlock)
                    {
                        Write(Environment.NewLine);
                        Write($"{_defaultTabs}    get");
                        Write(Environment.NewLine);
                        Write($"{_defaultTabs}    {defaultStartCodeBlockStatement}");
                        Write(Environment.NewLine);
                        Write($"{_defaultTabs}        {o.GetterExpression}");
                        if (!o.WithSetter)
                        {
                            Write(Environment.NewLine);
                            Write($"{_defaultTabs}    {defaultEndCodeBlockStatement}");
                        }
                    }
                    else
                    {
                        Write(o.GetterExpression);
                    }
                    
                }
            }
            
        }
        
        public virtual void SetSetter(MyProperty o)
        {
            if (o.WithSetter)
            {
                if (string.IsNullOrEmpty(o.SetterExpression))
                {
                    string defaultGetStatement = "set;";
                    Write($"{_}{defaultGetStatement}");
                }
                else
                {
                    if (o.ExpandCodeBlock)
                    {
                        if (o.WithGetter)
                        {
                            Write(Environment.NewLine);
                            Write($"{_defaultTabs}    {defaultEndCodeBlockStatement}");
                        }
                        Write(Environment.NewLine);
                        Write($"{_defaultTabs}    set");
                        Write(Environment.NewLine);
                        Write($"{_defaultTabs}    {defaultStartCodeBlockStatement}");
                        Write(Environment.NewLine);
                        Write($"{_defaultTabs}        {o.SetterExpression}");
                        Write(Environment.NewLine);
                        Write($"{_defaultTabs}    {defaultEndCodeBlockStatement}");
                    }
                    else
                    {
                        Write(o.SetterExpression);
                    }
                        
                }
            }
        }

        public virtual void FinishGetterSetterBlock(MyProperty o)
        {
            if (o.ExpandCodeBlock)
            {
                Write(Environment.NewLine);
                Write($"{_defaultTabs}{defaultEndCodeBlockStatement}");
                Write(Environment.NewLine);
            }
            else
            {
                Write($"{_}{defaultEndCodeBlockStatement}");
            }
            
        }

        public virtual void StartDefaultSetter(MyProperty o)
        {
            if (o.InitialExpression == null)
            {
                return;
            }
            Write($"{_}={_}{o.InitialExpression};");
        }
        public virtual string? AccessModifierToString(MyProperty.AccessModifiers? accessModifier)
        {
            return accessModifier?.ToString()?.ToLower();
        }

        private void Write(string value)
        {
            _writer.Write(value.Replace(Environment.NewLine, Environment.NewLine + _defaultTabs));
        }
    }
}
