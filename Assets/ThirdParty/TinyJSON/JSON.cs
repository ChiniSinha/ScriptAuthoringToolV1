using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

#if ENABLE_IL2CPP
using UnityEngine.Scripting;
#endif

namespace TinyJSON
{
    /// <summary>
    ///     Mark members that should be included.
    ///     Public fields are included by default.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class Include : Attribute
    {
    }


    /// <summary>
    ///     Mark members that should be excluded.
    ///     Private fields and all properties are excluded by default.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class Exclude : Attribute
    {
    }


    /// <summary>
    ///     Mark methods to be called after an object is decoded.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AfterDecode : Attribute
    {
    }


    /// <summary>
    ///     Mark methods to be called before an object is encoded.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class BeforeEncode : Attribute
    {
    }


    /// <summary>
    ///     Mark members to force type hinting even when EncodeOptions.NoTypeHints is set.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class TypeHint : Attribute
    {
    }


    [Obsolete("Use the Exclude attribute instead.")]
    public sealed class Skip : Exclude
    {
    }


    [Obsolete("Use the AfterDecode attribute instead.")]
    public sealed class Load : AfterDecode
    {
    }


    public sealed class DecodeException : Exception
    {
        public DecodeException(string message)
            : base(message)
        {
        }


        public DecodeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }


#if ENABLE_IL2CPP
	[Preserve]
	#endif

    public static class JSON
    {
        private static readonly Type includeAttrType = typeof(Include);
        private static readonly Type excludeAttrType = typeof(Exclude);


        public static Variant Load(string json)
        {
            if (json == null)
            {
                throw new ArgumentNullException("json");
            }

            return Decoder.Decode(json);
        }


        public static string Dump(object data)
        {
            return Dump(data, EncodeOptions.None);
        }


        public static string Dump(object data, EncodeOptions options)
        {
            // Invoke methods tagged with [BeforeEncode] attribute.
            if (data != null)
            {
                Type type = data.GetType();
                if (!(type.IsEnum || type.IsPrimitive || type.IsArray))
                {
                    foreach (MethodInfo method in type.GetMethods(instanceBindingFlags))
                    {
                        if (method.GetCustomAttributes(false).AnyOfType(typeof(BeforeEncode)))
                        {
                            if (method.GetParameters().Length == 0)
                            {
                                method.Invoke(data, null);
                            }
                        }
                    }
                }
            }

            return Encoder.Encode(data, options);
        }


        public static void MakeInto<T>(Variant data, out T item)
        {
            item = DecodeType<T>(data);
        }


        private static readonly Dictionary<string, Type> typeCache = new Dictionary<string, Type>();

        private static Type FindType(string fullName)
        {
            if (fullName == null)
            {
                return null;
            }

            Type type;
            if (typeCache.TryGetValue(fullName, out type))
            {
                return type;
            }

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = assembly.GetType(fullName);
                if (type != null)
                {
                    typeCache.Add(fullName, type);
                    return type;
                }
            }

            return null;
        }


#if ENABLE_IL2CPP
		[Preserve]
		#endif

        private static T DecodeType<T>(Variant data)
        {
            if (data == null)
            {
                return default(T);
            }

            Type type = typeof(T);

            if (type.IsEnum)
            {
                return (T) Enum.Parse(type, data.ToString());
            }

            if (type.IsPrimitive || type == typeof(string) || type == typeof(decimal))
            {
                return (T) Convert.ChangeType(data, type);
            }

            if (type.IsArray)
            {
                if (type.GetArrayRank() == 1)
                {
                    MethodInfo makeFunc = decodeArrayMethod.MakeGenericMethod(type.GetElementType());
                    return (T) makeFunc.Invoke(null, new object[] {data});
                }
                ProxyArray arrayData = data as ProxyArray;
                int arrayRank = type.GetArrayRank();
                int[] rankLengths = new int[arrayRank];
                if (arrayData.CanBeMultiRankArray(rankLengths))
                {
                    Array array = Array.CreateInstance(type.GetElementType(), rankLengths);
                    MethodInfo makeFunc = decodeMultiRankArrayMethod.MakeGenericMethod(type.GetElementType());
                    try
                    {
                        makeFunc.Invoke(null, new object[] {arrayData, array, 1, rankLengths});
                    }
                    catch (Exception e)
                    {
                        throw new DecodeException(
                            "Error decoding multidimensional array. Did you try to decode into an array of incompatible rank or element type?",
                            e);
                    }
                    return (T) Convert.ChangeType(array, typeof(T));
                }
                throw new DecodeException(
                    "Error decoding multidimensional array; JSON data doesn't seem fit this structure.");
#pragma warning disable 0162
                return default(T);
#pragma warning restore 0162
            }

            if (typeof(IList).IsAssignableFrom(type))
            {
                MethodInfo makeFunc = decodeListMethod.MakeGenericMethod(type.GetGenericArguments());
                return (T) makeFunc.Invoke(null, new object[] {data});
            }

            if (typeof(IDictionary).IsAssignableFrom(type))
            {
                MethodInfo makeFunc = decodeDictionaryMethod.MakeGenericMethod(type.GetGenericArguments());
                return (T) makeFunc.Invoke(null, new object[] {data});
            }

            // At this point we should be dealing with a class or struct.
            T instance;
            ProxyObject proxyObject = data as ProxyObject;
            if (proxyObject == null)
            {
                throw new InvalidCastException("ProxyObject expected when decoding into '" + type.FullName + "'.");
            }

            // If there's a type hint, use it to create the instance.
            string typeHint = proxyObject.TypeHint;
            if (typeHint != null && typeHint != type.FullName)
            {
                Type makeType = FindType(typeHint);
                if (makeType == null)
                {
                    throw new TypeLoadException("Could not load type '" + typeHint + "'.");
                }
                if (type.IsAssignableFrom(makeType))
                {
                    instance = (T) Activator.CreateInstance(makeType);
                    type = makeType;
                }
                else
                {
                    throw new InvalidCastException("Cannot assign type '" + typeHint + "' to type '" + type.FullName +
                                                   "'.");
                }
            }
            else
            {
                // We don't have a type hint, so just instantiate the type we have.
                instance = Activator.CreateInstance<T>();
            }


            // Now decode fields and properties.
            foreach (KeyValuePair<string, Variant> pair in data as ProxyObject)
            {
                FieldInfo field = type.GetField(pair.Key, instanceBindingFlags);
                if (field != null)
                {
                    bool shouldDecode = field.IsPublic;
                    foreach (object attribute in field.GetCustomAttributes(true))
                    {
                        if (excludeAttrType.IsAssignableFrom(attribute.GetType()))
                        {
                            shouldDecode = false;
                        }

                        if (includeAttrType.IsAssignableFrom(attribute.GetType()))
                        {
                            shouldDecode = true;
                        }
                    }

                    if (shouldDecode)
                    {
                        MethodInfo makeFunc = decodeTypeMethod.MakeGenericMethod(field.FieldType);
                        if (type.IsValueType)
                        {
                            // Type is a struct.
                            object instanceRef = instance;
                            field.SetValue(instanceRef, makeFunc.Invoke(null, new object[] {pair.Value}));
                            instance = (T) instanceRef;
                        }
                        else
                        {
                            // Type is a class.
                            field.SetValue(instance, makeFunc.Invoke(null, new object[] {pair.Value}));
                        }
                    }
                }

                PropertyInfo property = type.GetProperty(pair.Key, instanceBindingFlags);
                if (property != null)
                {
                    if (property.CanWrite && property.GetCustomAttributes(false).AnyOfType(includeAttrType))
                    {
                        MethodInfo makeFunc = decodeTypeMethod.MakeGenericMethod(property.PropertyType);
                        if (type.IsValueType)
                        {
                            // Type is a struct.
                            object instanceRef = instance;
                            property.SetValue(instanceRef, makeFunc.Invoke(null, new object[] {pair.Value}), null);
                            instance = (T) instanceRef;
                        }
                        else
                        {
                            // Type is a class.
                            property.SetValue(instance, makeFunc.Invoke(null, new object[] {pair.Value}), null);
                        }
                    }
                }
            }

            // Invoke methods tagged with [AfterDecode] attribute.
            foreach (MethodInfo method in type.GetMethods(instanceBindingFlags))
            {
                if (method.GetCustomAttributes(false).AnyOfType(typeof(AfterDecode)))
                {
                    if (method.GetParameters().Length == 0)
                    {
                        method.Invoke(instance, null);
                    }
                    else
                    {
                        method.Invoke(instance, new object[] {data});
                    }
                }
            }

            return instance;
        }


#if ENABLE_IL2CPP
		[Preserve]
		#endif

        private static List<T> DecodeList<T>(Variant data)
        {
            List<T> list = new List<T>();

            foreach (Variant item in data as ProxyArray)
            {
                list.Add(DecodeType<T>(item));
            }

            return list;
        }


#if ENABLE_IL2CPP
		[Preserve]
		#endif

        private static Dictionary<K, V> DecodeDictionary<K, V>(Variant data)
        {
            Dictionary<K, V> dict = new Dictionary<K, V>();
            Type type = typeof(K);

            foreach (KeyValuePair<string, Variant> pair in data as ProxyObject)
            {
                K k = (K) (type.IsEnum ? Enum.Parse(type, pair.Key) : Convert.ChangeType(pair.Key, type));
                V v = DecodeType<V>(pair.Value);
                dict.Add(k, v);
            }

            return dict;
        }


#if ENABLE_IL2CPP
		[Preserve]
		#endif

        private static T[] DecodeArray<T>(Variant data)
        {
            ProxyArray arrayData = data as ProxyArray;
            int arraySize = arrayData.Count;
            T[] array = new T[arraySize];

            int i = 0;
            foreach (Variant item in data as ProxyArray)
            {
                array[i++] = DecodeType<T>(item);
            }

            return array;
        }


#if ENABLE_IL2CPP
		[Preserve]
		#endif

        private static void DecodeMultiRankArray<T>(ProxyArray arrayData, Array array, int arrayRank, int[] indices)
        {
            int count = arrayData.Count;
            for (int i = 0; i < count; i++)
            {
                indices[arrayRank - 1] = i;
                if (arrayRank < array.Rank)
                {
                    DecodeMultiRankArray<T>(arrayData[i] as ProxyArray, array, arrayRank + 1, indices);
                }
                else
                {
                    array.SetValue(DecodeType<T>(arrayData[i]), indices);
                }
            }
        }


        private static readonly BindingFlags instanceBindingFlags = BindingFlags.Public | BindingFlags.NonPublic |
                                                                    BindingFlags.Instance;

        private static readonly BindingFlags staticBindingFlags = BindingFlags.Public | BindingFlags.NonPublic |
                                                                  BindingFlags.Static;

        private static readonly MethodInfo decodeTypeMethod = typeof(JSON).GetMethod("DecodeType", staticBindingFlags);
        private static readonly MethodInfo decodeListMethod = typeof(JSON).GetMethod("DecodeList", staticBindingFlags);

        private static readonly MethodInfo decodeDictionaryMethod = typeof(JSON).GetMethod("DecodeDictionary",
            staticBindingFlags);

        private static readonly MethodInfo decodeArrayMethod = typeof(JSON).GetMethod("DecodeArray", staticBindingFlags);

        private static readonly MethodInfo decodeMultiRankArrayMethod = typeof(JSON).GetMethod("DecodeMultiRankArray",
            staticBindingFlags);


#if ENABLE_IL2CPP
		[Preserve]
		#endif

        public static void SupportTypeForAOT<T>()
        {
            DecodeType<T>(null);
            DecodeList<T>(null);
            DecodeArray<T>(null);
            DecodeDictionary<short, T>(null);
            DecodeDictionary<ushort, T>(null);
            DecodeDictionary<int, T>(null);
            DecodeDictionary<uint, T>(null);
            DecodeDictionary<long, T>(null);
            DecodeDictionary<ulong, T>(null);
            DecodeDictionary<float, T>(null);
            DecodeDictionary<double, T>(null);
            DecodeDictionary<decimal, T>(null);
            DecodeDictionary<bool, T>(null);
            DecodeDictionary<string, T>(null);
        }


#if ENABLE_IL2CPP
		[Preserve]
		#endif

        private static void SupportValueTypesForAOT()
        {
            SupportTypeForAOT<short>();
            SupportTypeForAOT<ushort>();
            SupportTypeForAOT<int>();
            SupportTypeForAOT<uint>();
            SupportTypeForAOT<long>();
            SupportTypeForAOT<ulong>();
            SupportTypeForAOT<float>();
            SupportTypeForAOT<double>();
            SupportTypeForAOT<decimal>();
            SupportTypeForAOT<bool>();
            SupportTypeForAOT<string>();
        }
    }
}