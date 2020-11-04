// Finding the shortest path using Dijkstra algorithm

// Algorithm finds the shortest path in a maze

// 3.11.2020
// Winter Semester, 2020/2021
// Lukasz Kwiecien Informatics

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PathFinderDijkstra.Grid
{
	/// <summary>
	/// Responsible for making deep copy of the object.
	/// </summary>
	public static class Extensions
	{
		public static T DeepClone<T>(this T obj)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, obj);
				stream.Position = 0;

				return (T)formatter.Deserialize(stream);
			}
		}
	}
}
