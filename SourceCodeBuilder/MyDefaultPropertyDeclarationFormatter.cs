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
        public virtual string DefaultTabs { get; set; } = "        ";

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
                    return DefaultTabs;
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

            StringBuilder stringBuilder = new ();
            SetAccessModifiers(o, stringBuilder);
            SetType(o, stringBuilder);
            SetName(o, stringBuilder);
            StartGetterSetterBlock(o, stringBuilder);
            SetGetter(o, stringBuilder);
            SetSetter(o, stringBuilder);
            FinishGetterSetterBlock(o, stringBuilder);

            return stringBuilder.ToString();
        }

        public virtual void SetAccessModifiers(MyProperty o, StringBuilder stringBuilder)
        {
            foreach(var a in o.AccessModifiersList)
            {
                stringBuilder.Append($"{_}{AccessModifierToString(a)}");
            }
        }

        public virtual void SetType(MyProperty o, StringBuilder stringBuilder)
        {
            stringBuilder.Append($"{_}{o.PropertyTypeName}");
        }

        public virtual void SetName(MyProperty o, StringBuilder stringBuilder)
        {
            stringBuilder.Append($"{_}{o.PropertyName}");
        }

        public virtual void StartGetterSetterBlock(MyProperty o, StringBuilder stringBuilder)
        {
            string defaultStartGetterSetterStatement = "{";
            stringBuilder.Append($"{_}{defaultStartGetterSetterStatement}");
        }

        public virtual void SetGetter(MyProperty o, StringBuilder stringBuilder)
        {
            string defaultGetStatement = "get;";
            stringBuilder.Append($"{_}{defaultGetStatement}");
        }
        
        public virtual void SetSetter(MyProperty o, StringBuilder stringBuilder)
        {
            string defaultGetStatement = "set;";
            stringBuilder.Append($"{_}{defaultGetStatement}");
        }

        public virtual void FinishGetterSetterBlock(MyProperty o, StringBuilder stringBuilder)
        {
            string defaultFinishGetterSetterStatement = "}";
            stringBuilder.Append($"{_}{defaultFinishGetterSetterStatement}");
        }

        public virtual string? AccessModifierToString(MyProperty.AccessModifiers? accessModifier)
        {
            return accessModifier?.ToString()?.ToLower();
        }
    }
}
