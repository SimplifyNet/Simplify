using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

using XmlSer = System.Xml.Serialization;

namespace Simplify.Xml;

/// <summary>
/// Provides XML serialization/deserialization extensions
/// </summary>
public static class XmlSerializer
{
	/// <summary>
	/// Serializes the specified items list to the XML string.
	/// </summary>
	/// <typeparam name="T">The type of the item</typeparam>
	/// <param name="items">The items list to serialize.</param>
	public static string Serialize<T>(IList<T> items)
	{
		using var memoryStream = new MemoryStream();
		var serializer = new XmlSer.XmlSerializer(typeof(List<T>));

		serializer.Serialize(memoryStream, items);

		memoryStream.Position = 0;

		return new StreamReader(memoryStream).ReadToEnd();
	}

	/// <summary>
	/// Serialize the object to XElement.
	/// </summary>
	/// <typeparam name="T">The type of the item</typeparam>
	/// <param name="obj">The object to serialize.</param>
	public static XElement ToXElement<T>(T obj)
	{
		using var memoryStream = new MemoryStream();
		using TextWriter streamWriter = new StreamWriter(memoryStream);
		var xmlSerializer = new XmlSer.XmlSerializer(typeof(T));

		xmlSerializer.Serialize(streamWriter, obj);

		return XElement.Parse(Encoding.UTF8.GetString(memoryStream.ToArray()));
	}

	/// <summary>
	/// Deserialize the XElement to object.
	/// </summary>
	/// <typeparam name="T">The type of the item</typeparam>
	/// <param name="xElement">The XElement to deserialize.</param>
	public static T FromXElement<T>(XElement xElement)
	{
		using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xElement.ToString()));
		var xmlSerializer = new XmlSer.XmlSerializer(typeof(T));

		return (T)xmlSerializer.Deserialize(memoryStream)!;
	}
}