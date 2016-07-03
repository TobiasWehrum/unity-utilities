using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace UnityUtilities
{
    /// <summary>
    /// The XmlHelper serializes and deserializes to/from XML and allows convenient
    /// access to optional element content and attributes when reading general XMLs.
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        /// A dictionary of XmlSerializer, accessible by type. Used by <see cref="GetSerializer{T}"/>.
        /// </summary>
        static Dictionary<Type, XmlSerializer> serializersByType = new Dictionary<Type, XmlSerializer>();

        /// <summary>
        /// A thread lock object to make access to <see cref="serializersByType"/> threadsafe.
        /// </summary>
#pragma warning disable 414
        static Object serializersByTypeLock = new object();
#pragma warning restore 414

        /// <summary>
        /// Gets a cached XmlSerializer for the specified type.
        /// </summary>
        /// <typeparam name="T">The type to get the XmlSerializer for.</typeparam>
        /// <returns>An XmlSerializer for the specified type.</returns>
        public static XmlSerializer GetSerializer<T>()
        {
            lock (serializersByType)
            {
                var t = typeof (T);
                XmlSerializer serializer;
                if (!serializersByType.TryGetValue(t, out serializer))
                {
                    serializer = new XmlSerializer(t);
                    serializersByType[t] = serializer;
                }
                return serializer;
            }
        }

        /// <summary>
        /// Uses the XmlSerializer to serialize data into a string that can later
        /// be deserialized again via <see cref="DeserializeFromXmlString{T}"/>.
        /// 
        /// With a few exceptions (e.g. arrays of ArrayList and arrays of List&lt;T&gt;),
        /// all public attributes and fields of any public class should be serialized
        /// without any further need to tag the elements. The only thing needed is
        /// a public default constructor.
        /// 
        /// For finer control, see the MSDN document on XmlSerializer:
        /// https://msdn.microsoft.com/en-us/library/system.xml.serialization.xmlserializer(v=vs.110).aspx
        /// </summary>
        /// <typeparam name="T">The data type to be serialized.</typeparam>
        /// <param name="data">The data to be serialized.</param>
        /// <returns>The serialized data string.</returns>
        public static string SerializeToXmlString<T>(this T data)
        {
            using (var stringWriter = new StringWriter())
            {
                GetSerializer<T>().Serialize(stringWriter, data);
                return stringWriter.ToString();
            }
        }

        /// <summary>
        /// Deserializes an object serialized with <see cref="SerializeToXmlString{T}"/>.
        /// </summary>
        /// <typeparam name="T">Thedata type that was serialized.</typeparam>
        /// <param name="str">The serialized data string.</param>
        /// <returns>The deserialized data.</returns>
        /// <exception cref="InvalidOperationException">
        ///     An error occurred during deserialization. The original exception is available
        ///     using the InnerException property.
        /// </exception>
        public static T DeserializeFromXmlString<T>(this string str)
        {
            using (var stringReader = new StringReader(str))
            {
                return (T) GetSerializer<T>().Deserialize(stringReader);
            }
        }

        /// <summary>
        /// Gets the content of first child element with the specified name. If no child
        /// with that name exists, the defaultValue is returned.
        /// </summary>
        /// <param name="xmlNode">The XMlNode to get the child from.</param>
        /// <param name="name">The name of the child.</param>
        /// <param name="defaultValue">The default value if no child with that name exists.</param>
        /// <returns>The content of the child if it exists; else the default value.</returns>
        public static string GetElementString(this XmlNode xmlNode, string name, string defaultValue = "")
        {
            var element = xmlNode[name];
            if (element == null)
                return defaultValue;

            return element.InnerText;
        }

        /// <summary>
        /// Gets the content of first child element with the specified name. If no child
        /// with that name exists, the defaultValue is returned.
        /// </summary>
        /// <param name="xmlNode">The XMlNode to get the child from.</param>
        /// <param name="name">The name of the child.</param>
        /// <param name="defaultValue">The default value if no child with that name exists.</param>
        /// <returns>The content of the child if it exists; else the default value.</returns>
        /// <exception cref="FormatException">The content is is not in the correct format.</exception>
        /// <exception cref="OverflowException">The content represents a number less than MinValue or greater than MaxValue.</exception>
        public static int GetElementInt(this XmlNode xmlNode, string name, int defaultValue = 0)
        {
            var element = xmlNode[name];
            if (element == null)
                return defaultValue;

            return int.Parse(element.InnerText);
        }

        /// <summary>
        /// Gets the content of first child element with the specified name. If no child
        /// with that name exists, the defaultValue is returned.
        /// </summary>
        /// <param name="xmlNode">The XMlNode to get the child from.</param>
        /// <param name="name">The name of the child.</param>
        /// <param name="defaultValue">The default value if no child with that name exists.</param>
        /// <returns>The content of the child if it exists; else the default value.</returns>
        /// <exception cref="FormatException">The content is is not in the correct format.</exception>
        /// <exception cref="OverflowException">The content represents a number less than MinValue or greater than MaxValue.</exception>
        public static float GetElementFloat(this XmlNode xmlNode, string name, float defaultValue = 0)
        {
            var element = xmlNode[name];
            if (element == null)
                return defaultValue;

            return float.Parse(element.InnerText);
        }

        /// <summary>
        /// Gets the content of first child element with the specified name. If no child
        /// with that name exists, the defaultValue is returned.
        /// </summary>
        /// <param name="xmlNode">The XMlNode to get the child from.</param>
        /// <param name="name">The name of the child.</param>
        /// <param name="defaultValue">The default value if no child with that name exists.</param>
        /// <returns>The content of the child if it exists; else the default value.</returns>
        /// <exception cref="FormatException">The content is is not in the correct format.</exception>
        /// <exception cref="OverflowException">The content represents a number less than MinValue or greater than MaxValue.</exception>
        public static bool GetElementBool(this XmlNode xmlNode, string name, bool defaultValue = false)
        {
            var element = xmlNode[name];
            if (element == null)
                return defaultValue;

            return bool.Parse(element.InnerText);
        }

        /// <summary>
        /// Gets the content of first attribute with the specified name. If no attribute
        /// with that name exists, the defaultValue is returned.
        /// </summary>
        /// <param name="xmlNode">The XMlNode to get the attribute from.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="defaultValue">The default value if no attribute with that name exists.</param>
        /// <returns>The content of the attribute if it exists; else the default value.</returns>
        public static string GetAttributeString(this XmlNode xmlNode, string name, string defaultValue = "")
        {
            var attribute = xmlNode.Attributes[name];
            if (attribute == null)
                return defaultValue;

            return attribute.Value;
        }

        /// <summary>
        /// Gets the content of first attribute with the specified name. If no attribute
        /// with that name exists, the defaultValue is returned.
        /// </summary>
        /// <param name="xmlNode">The XMlNode to get the attribute from.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="defaultValue">The default value if no attribute with that name exists.</param>
        /// <returns>The content of the attribute if it exists; else the default value.</returns>
        /// <exception cref="FormatException">The content is is not in the correct format.</exception>
        /// <exception cref="OverflowException">The content represents a number less than MinValue or greater than MaxValue.</exception>
        public static int GetAttributeInt(this XmlNode xmlNode, string name, int defaultValue = 0)
        {
            var attribute = xmlNode.Attributes[name];
            if (attribute == null)
                return defaultValue;

            return int.Parse(attribute.Value);
        }

        /// <summary>
        /// Gets the content of first attribute with the specified name. If no attribute
        /// with that name exists, the defaultValue is returned.
        /// </summary>
        /// <param name="xmlNode">The XMlNode to get the attribute from.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="defaultValue">The default value if no attribute with that name exists.</param>
        /// <returns>The content of the attribute if it exists; else the default value.</returns>
        /// <exception cref="FormatException">The content is is not in the correct format.</exception>
        /// <exception cref="OverflowException">The content represents a number less than MinValue or greater than MaxValue.</exception>
        public static int? GetAttributeIntNullable(this XmlNode xmlNode, string name, int? defaultValue = null)
        {
            var attribute = xmlNode.Attributes[name];
            if (attribute == null)
                return defaultValue;

            return int.Parse(attribute.Value);
        }

        /// <summary>
        /// Gets the content of first attribute with the specified name. If no attribute
        /// with that name exists, the defaultValue is returned.
        /// </summary>
        /// <param name="xmlNode">The XMlNode to get the attribute from.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="defaultValue">The default value if no attribute with that name exists.</param>
        /// <returns>The content of the attribute if it exists; else the default value.</returns>
        /// <exception cref="FormatException">The content is is not in the correct format.</exception>
        /// <exception cref="OverflowException">The content represents a number less than MinValue or greater than MaxValue.</exception>
        public static float GetAttributeFloat(this XmlNode xmlNode, string name, float defaultValue = 0)
        {
            var attribute = xmlNode.Attributes[name];
            if (attribute == null)
                return defaultValue;

            return float.Parse(attribute.Value);
        }

        /// <summary>
        /// Gets the content of first attribute with the specified name. If no attribute
        /// with that name exists, the defaultValue is returned.
        /// </summary>
        /// <param name="xmlNode">The XMlNode to get the attribute from.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="defaultValue">The default value if no attribute with that name exists.</param>
        /// <returns>The content of the attribute if it exists; else the default value.</returns>
        /// <exception cref="FormatException">The content is is not in the correct format.</exception>
        /// <exception cref="OverflowException">The content represents a number less than MinValue or greater than MaxValue.</exception>
        public static float? GetAttributeFloatNullable(this XmlNode xmlNode, string name, float? defaultValue = null)
        {
            var attribute = xmlNode.Attributes[name];
            if (attribute == null)
                return defaultValue;

            return float.Parse(attribute.Value);
        }

        /// <summary>
        /// Gets the content of first attribute with the specified name. If no attribute
        /// with that name exists, the defaultValue is returned.
        /// </summary>
        /// <param name="xmlNode">The XMlNode to get the attribute from.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="defaultValue">The default value if no attribute with that name exists.</param>
        /// <returns>The content of the attribute if it exists; else the default value.</returns>
        /// <exception cref="FormatException">The content is is not in the correct format.</exception>
        public static bool GetAttributeBool(this XmlNode xmlNode, string name, bool defaultValue = false)
        {
            var attribute = xmlNode.Attributes[name];
            if (attribute == null)
                return defaultValue;

            return bool.Parse(attribute.Value);
        }
    }
}