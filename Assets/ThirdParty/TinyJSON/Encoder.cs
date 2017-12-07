using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace TinyJSON
{
    public sealed class Encoder
    {
        private static readonly Type includeAttrType = typeof(Include);
        private static readonly Type excludeAttrType = typeof(Exclude);
        private static readonly Type typeHintAttrType = typeof(TypeHint);

        private readonly StringBuilder builder;
        private int indent;
        private readonly EncodeOptions options;


        private Encoder(EncodeOptions options)
        {
            this.options = options;
            builder = new StringBuilder();
            indent = 0;
        }


        private bool PrettyPrintEnabled
        {
            get { return (options & EncodeOptions.PrettyPrint) == EncodeOptions.PrettyPrint; }
        }


        private bool TypeHintsEnabled
        {
            get { return (options & EncodeOptions.NoTypeHints) != EncodeOptions.NoTypeHints; }
        }


        public static string Encode(object obj)
        {
            return Encode(obj, EncodeOptions.None);
        }


        public static string Encode(object obj, EncodeOptions options)
        {
            Encoder instance = new Encoder(options);
            instance.EncodeValue(obj, false);
            return instance.builder.ToString();
        }


        private void EncodeValue(object value, bool forceTypeHint)
        {
            Array asArray;
            IList asList;
            IDictionary asDict;
            string asString;

            if (value == null)
            {
                builder.Append("null");
            }
            else if ((asString = value as string) != null)
            {
                EncodeString(asString);
            }
            else if (value is bool)
            {
                builder.Append(value.ToString().ToLower());
            }
            else if (value is Enum)
            {
                EncodeString(value.ToString());
            }
            else if ((asArray = value as Array) != null)
            {
                EncodeArray(asArray, forceTypeHint);
            }
            else if ((asList = value as IList) != null)
            {
                EncodeList(asList, forceTypeHint);
            }
            else if ((asDict = value as IDictionary) != null)
            {
                EncodeDictionary(asDict, forceTypeHint);
            }
            else if (value is char)
            {
                EncodeString(value.ToString());
            }
            else
            {
                EncodeOther(value, forceTypeHint);
            }
        }


        private void EncodeObject(object value, bool forceTypeHint)
        {
            Type type = value.GetType();

            AppendOpenBrace();

            forceTypeHint = forceTypeHint || TypeHintsEnabled;

            bool firstItem = !forceTypeHint;
            if (forceTypeHint)
            {
                if (PrettyPrintEnabled)
                {
                    AppendIndent();
                }
                EncodeString(ProxyObject.TypeHintName);
                AppendColon();
                EncodeString(type.FullName);
                firstItem = false;
            }

            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                bool shouldTypeHint = false;
                bool shouldEncode = field.IsPublic;
                foreach (object attribute in field.GetCustomAttributes(true))
                {
                    if (excludeAttrType.IsAssignableFrom(attribute.GetType()))
                    {
                        shouldEncode = false;
                    }

                    if (includeAttrType.IsAssignableFrom(attribute.GetType()))
                    {
                        shouldEncode = true;
                    }

                    if (typeHintAttrType.IsAssignableFrom(attribute.GetType()))
                    {
                        shouldTypeHint = true;
                    }
                }

                if (shouldEncode)
                {
                    AppendComma(firstItem);
                    EncodeString(field.Name);
                    AppendColon();
                    EncodeValue(field.GetValue(value), shouldTypeHint);
                    firstItem = false;
                }
            }

            PropertyInfo[] properties =
                type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                if (property.CanRead)
                {
                    bool shouldTypeHint = false;
                    bool shouldEncode = false;
                    foreach (object attribute in property.GetCustomAttributes(true))
                    {
                        if (includeAttrType.IsAssignableFrom(attribute.GetType()))
                        {
                            shouldEncode = true;
                        }

                        if (typeHintAttrType.IsAssignableFrom(attribute.GetType()))
                        {
                            shouldTypeHint = true;
                        }
                    }

                    if (shouldEncode)
                    {
                        AppendComma(firstItem);
                        EncodeString(property.Name);
                        AppendColon();
                        EncodeValue(property.GetValue(value, null), shouldTypeHint);
                        firstItem = false;
                    }
                }
            }

            AppendCloseBrace();
        }


        private void EncodeDictionary(IDictionary value, bool forceTypeHint)
        {
            if (value.Count == 0)
            {
                builder.Append("{}");
            }
            else
            {
                AppendOpenBrace();

                bool firstItem = true;
                foreach (object e in value.Keys)
                {
                    AppendComma(firstItem);
                    EncodeString(e.ToString());
                    AppendColon();
                    EncodeValue(value[e], forceTypeHint);
                    firstItem = false;
                }

                AppendCloseBrace();
            }
        }


        private void EncodeList(IList value, bool forceTypeHint)
        {
            if (value.Count == 0)
            {
                builder.Append("[]");
            }
            else
            {
                AppendOpenBracket();

                bool firstItem = true;
                foreach (object obj in value)
                {
                    AppendComma(firstItem);
                    EncodeValue(obj, forceTypeHint);
                    firstItem = false;
                }

                AppendCloseBracket();
            }
        }


        private void EncodeArray(Array value, bool forceTypeHint)
        {
            if (value.Rank == 1)
            {
                EncodeList(value, forceTypeHint);
            }
            else
            {
                int[] indices = new int[value.Rank];
                EncodeArrayRank(value, 0, indices, forceTypeHint);
            }
        }


        private void EncodeArrayRank(Array value, int rank, int[] indices, bool forceTypeHint)
        {
            AppendOpenBracket();

            int min = value.GetLowerBound(rank);
            int max = value.GetUpperBound(rank);

            if (rank == value.Rank - 1)
            {
                for (int i = min; i <= max; i++)
                {
                    indices[rank] = i;
                    AppendComma(i == min);
                    EncodeValue(value.GetValue(indices), forceTypeHint);
                }
            }
            else
            {
                for (int i = min; i <= max; i++)
                {
                    indices[rank] = i;
                    AppendComma(i == min);
                    EncodeArrayRank(value, rank + 1, indices, forceTypeHint);
                }
            }

            AppendCloseBracket();
        }


        private void EncodeString(string value)
        {
            builder.Append('\"');

            char[] charArray = value.ToCharArray();
            foreach (char c in charArray)
            {
                switch (c)
                {
                    case '"':
                        builder.Append("\\\"");
                        break;

                    case '\\':
                        builder.Append("\\\\");
                        break;

                    case '\b':
                        builder.Append("\\b");
                        break;

                    case '\f':
                        builder.Append("\\f");
                        break;

                    case '\n':
                        builder.Append("\\n");
                        break;

                    case '\r':
                        builder.Append("\\r");
                        break;

                    case '\t':
                        builder.Append("\\t");
                        break;

                    default:
                        int codepoint = Convert.ToInt32(c);
                        if ((codepoint >= 32) && (codepoint <= 126))
                        {
                            builder.Append(c);
                        }
                        else
                        {
                            builder.Append("\\u" + Convert.ToString(codepoint, 16).PadLeft(4, '0'));
                        }
                        break;
                }
            }

            builder.Append('\"');
        }


        private void EncodeOther(object value, bool forceTypeHint)
        {
            if (value is float ||
                value is double ||
                value is int ||
                value is uint ||
                value is long ||
                value is sbyte ||
                value is byte ||
                value is short ||
                value is ushort ||
                value is ulong ||
                value is decimal)
            {
                builder.Append(value);
            }
            else
            {
                EncodeObject(value, forceTypeHint);
            }
        }

        #region Helpers

        private void AppendIndent()
        {
            for (int i = 0; i < indent; i++)
            {
                builder.Append('\t');
            }
        }


        private void AppendOpenBrace()
        {
            builder.Append('{');

            if (PrettyPrintEnabled)
            {
                builder.Append('\n');
                indent++;
            }
        }


        private void AppendCloseBrace()
        {
            if (PrettyPrintEnabled)
            {
                builder.Append('\n');
                indent--;
                AppendIndent();
            }

            builder.Append('}');
        }


        private void AppendOpenBracket()
        {
            builder.Append('[');

            if (PrettyPrintEnabled)
            {
                builder.Append('\n');
                indent++;
            }
        }


        private void AppendCloseBracket()
        {
            if (PrettyPrintEnabled)
            {
                builder.Append('\n');
                indent--;
                AppendIndent();
            }

            builder.Append(']');
        }


        private void AppendComma(bool firstItem)
        {
            if (!firstItem)
            {
                builder.Append(',');

                if (PrettyPrintEnabled)
                {
                    builder.Append('\n');
                }
            }

            if (PrettyPrintEnabled)
            {
                AppendIndent();
            }
        }


        private void AppendColon()
        {
            builder.Append(':');

            if (PrettyPrintEnabled)
            {
                builder.Append(' ');
            }
        }

        #endregion
    }
}