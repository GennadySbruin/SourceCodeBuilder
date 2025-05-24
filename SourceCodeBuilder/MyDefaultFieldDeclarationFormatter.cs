using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SourceCodeBuilder
{
    public class MyDefaultFieldDeclarationFormatter : IFormatter<MyField>
    {
        private static IFormatter<MyField>? s_instance;

        /// <summary>
        /// Reusable static formatter
        /// </summary>
        public static IFormatter<MyField> Formatter
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new MyDefaultFieldDeclarationFormatter();
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
        public string ToString(MyField o)
        {
            if(o == null)
            {
                throw new ArgumentNullException("MyField o");
            }

            StringBuilder stringBuilder = new ();
            SetAccessModifiers(o, stringBuilder);
            SetType(o, stringBuilder);
            SetName(o, stringBuilder);
            StartDefaultSetter(o, stringBuilder);
            EndSetter(o, stringBuilder);
            return stringBuilder.ToString();
        }

        public virtual void SetAccessModifiers(MyField o, StringBuilder stringBuilder)
        {
            foreach(var a in o.AccessModifiersList)
            {
                stringBuilder.Append($"{_}{AccessModifierToString(a)}");
            }
        }

        public virtual void SetType(MyField o, StringBuilder stringBuilder)
        {
            stringBuilder.Append($"{_}{o.FieldTypeName}");
        }

        public virtual void SetName(MyField o, StringBuilder stringBuilder)
        {
            stringBuilder.Append($"{_}{o.FieldName}");
        }

        public virtual void StartDefaultSetter(MyField o, StringBuilder stringBuilder)
        {
            if(o.InitialExpression == null)
            {
                return;
            }
            string defaultStartGetterSetterStatement = "=";
            stringBuilder.Append($"{_}{defaultStartGetterSetterStatement}{_}{o.InitialExpression}");
        }

        public virtual void EndSetter(MyField o, StringBuilder stringBuilder)
        {
            string defaultStartGetterSetterStatement = ";";
            stringBuilder.Append(defaultStartGetterSetterStatement);
        }

        public virtual string? AccessModifierToString(MyField.AccessModifiers? accessModifier)
        {
            return accessModifier?.ToString()?.ToLower();
        }
    }
}
