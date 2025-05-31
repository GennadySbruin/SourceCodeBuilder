using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SourceCodeBuilder
{
    public class MyPropertyWriter : ICodeWriter<MyProperty>
    {
        private static ICodeWriter<MyProperty>? s_instance;
        private readonly System.Threading.Lock _writerLock = new();
        /// <summary>
        /// Reusable static formatter
        /// </summary>
        public static ICodeWriter<MyProperty> Formatter
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new MyPropertyWriter();
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
        public string WriteCode(MyProperty o)
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

        TextWriter? _writer = null;
        public virtual void GenerateCode(MyProperty o, TextWriter writer, string tabs = "")
        {
            if (o == null)
            {
                throw new ArgumentNullException("MyProperty o");
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
                StartGetterSetterBlock(o);
                SetLambdaGetter(o);
                SetGetter(o);
                SetSetter(o);
                FinishGetterSetterBlock(o);
                StartDefaultSetter(o);
            }
        }

        public virtual void GenerateCodeForInterface(MyProperty o, TextWriter writer, string tabs = "")
        {
            if (o == null)
            {
                throw new ArgumentNullException("MyProperty o");
            }
            lock (_writerLock)
            {
                Clear();
                _parentTabs = tabs;
                _writer = writer;
                SetComments(o);
                SetType(o);
                SetName(o);
                InterfaceGetterSetter(o);
            }
        }

        public virtual void SetComments(MyProperty o)
        { 
            foreach (var c in o.Comments ?? [])
            {
                string comment = c.StartsWith("//") ? c : $"//{c}";
                Write($"{Environment.NewLine}{comment}");
            }
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

            if (!string.IsNullOrEmpty(o.LambdaExpression))
            {
                return;
            }

            if (o.ExpandCodeBlock)
            {
                Write(Environment.NewLine);
                Write($"{_parentTabs}{defaultStartCodeBlockStatement}");
            }
            else
            {
                Write($"{_}{defaultStartCodeBlockStatement}");
            }
            
        }

        public virtual void SetLambdaGetter(MyProperty o)
        {
            if (!string.IsNullOrEmpty(o.LambdaExpression))
            {
                Write(o.LambdaExpression);
            }
        }

        public virtual void SetGetter(MyProperty o)
        {
            if (!string.IsNullOrEmpty(o.LambdaExpression))
            {
                return;
            }

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
                        Write($"{_parentTabs}    get");
                        Write(Environment.NewLine);
                        Write($"{_parentTabs}    {defaultStartCodeBlockStatement}");
                        Write(Environment.NewLine);
                        Write($"{_parentTabs}        {o.GetterExpression}");
                        if (!o.WithSetter)
                        {
                            Write(Environment.NewLine);
                            Write($"{_parentTabs}    {defaultEndCodeBlockStatement}");
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
            if (!string.IsNullOrEmpty(o.LambdaExpression))
            {
                return;
            }

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
                            Write($"{_parentTabs}    {defaultEndCodeBlockStatement}");
                        }
                        Write(Environment.NewLine);
                        Write($"{_parentTabs}    set");
                        Write(Environment.NewLine);
                        Write($"{_parentTabs}    {defaultStartCodeBlockStatement}");
                        Write(Environment.NewLine);
                        Write($"{_parentTabs}        {o.SetterExpression}");
                        Write(Environment.NewLine);
                        Write($"{_parentTabs}    {defaultEndCodeBlockStatement}");
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

            if (!string.IsNullOrEmpty(o.LambdaExpression))
            {
                return;
            }

            if (o.ExpandCodeBlock)
            {
                Write(Environment.NewLine);
                Write($"{_parentTabs}{defaultEndCodeBlockStatement}");
                Write(Environment.NewLine);
            }
            else
            {
                Write($"{_}{defaultEndCodeBlockStatement}");
            }
            
        }

        public virtual void StartDefaultSetter(MyProperty o)
        {
            if (!string.IsNullOrEmpty(o.LambdaExpression))
            {
                return;
            }

            if (o.InitialExpression == null)
            {
                return;
            }
            Write($"{_}={_}{o.InitialExpression};");
        }

        public virtual void InterfaceGetterSetter(MyProperty o)
        {
            if(o.WithGetter && o.WithSetter)
            {
                Write($"{_}" + "{ get; set; }");
                return;
            }
            if (o.WithGetter)
            {
                Write($"{_}" + "{ get; }");
                return;
            }
            if (o.WithSetter)
            {
                Write($"{_}" + "{ set; }");
                return;
            }
        }
        public virtual string? AccessModifierToString(MyProperty.AccessModifiers? accessModifier)
        {
            return accessModifier?.ToString()?.ToLower();
        }

        public virtual void Write(string value)
        {
            _writer?.Write(value.Replace(Environment.NewLine, Environment.NewLine + _parentTabs));
        }
    }
}
