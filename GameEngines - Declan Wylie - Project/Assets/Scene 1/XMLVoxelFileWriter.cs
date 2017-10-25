using UnityEngine;
using System.Collections;
using System.Xml;

public class XMLVoxelFileWriter{

	public static void SaveChunkToXMLFile(int[, ,] voxelArray,
	                                      string filename){

		Debug.Log(filename);

		XmlWriterSettings writerSettings = new XmlWriterSettings();
		writerSettings.Indent = true;

		XmlWriter xmlWriter =
			XmlWriter.Create (filename + ".xml", writerSettings);

		xmlWriter.WriteStartDocument ();

		xmlWriter.WriteStartElement("VoxelChunk");


		for (int x = 0; x < voxelArray.GetLength(0); x++) {
			for (int y = 0; y < voxelArray.GetLength(1); y++) {
				for (int z = 0; z < voxelArray.GetLength(1); z++) {
					if (voxelArray [x, y, z] != 0) {
						// Create a single voxel element
						xmlWriter.WriteStartElement ("Voxel");
						xmlWriter.WriteAttributeString ("x", x.ToString());
						xmlWriter.WriteAttributeString ("y", y.ToString());
						xmlWriter.WriteAttributeString ("z", z.ToString());
						xmlWriter.WriteString(voxelArray[x, y, z].ToString());
						xmlWriter.WriteEndElement();
					}
				}
			}
		}
		// End the root element
		xmlWriter.WriteEndElement();
		// Close the document to save
		xmlWriter.Close();
	}


	public static int[, ,] LoadChunkFromXMLFile(int size,
	                                            string filename){

		int[, ,] voxelArray = new int[size, size, size];

		XmlReader xmlReader = XmlReader.Create (filename + ".xml");

		while (xmlReader.Read ()) {

			if(xmlReader.IsStartElement("Voxel")){

				int x = int.Parse (xmlReader["x"]);
				int y = int.Parse (xmlReader["y"]);
				int z = int.Parse (xmlReader["z"]);

				xmlReader.Read ();

				int value = int.Parse(xmlReader.Value);

				voxelArray[x, y, z] = value;

			}
		}
		return voxelArray;
	}

	public static bool ReadStartAndEndPosition(
		out Vector3 start, out Vector3 end, string fileName){

		bool foundStart = false;
		bool foundEnd = false;

		start = new Vector3 (-1, -1, -1);
		end = new Vector3 (-1, -1, -1);

		XmlReader xmlReader = XmlReader.Create (fileName + ".xml");

		while (xmlReader.Read()) {

			if(xmlReader.IsStartElement("start")){

				int x = int.Parse(xmlReader["x"]);
				int y = int.Parse(xmlReader["y"]);
				int z = int.Parse(xmlReader["z"]);

				start = new Vector3(x, y, z);
				foundStart = true;

			}

			if(xmlReader.IsStartElement("end")){

				int x = int.Parse(xmlReader["x"]);
				int y = int.Parse(xmlReader["y"]);
				int z = int.Parse(xmlReader["z"]);


				end = new Vector3(x, y, z);
				foundEnd = true;
			}

		}

		return foundStart && foundEnd;

	}


//	public static Vector3 LoadPlayerTransformsFromXMLFile(string filename){
//
//		Vector3 playerTransform = new Vector3 ();
//		Vector3 playerTransformRotation = new Vector3 ();
//
//		XmlReader xmlReader = XmlReader.Create (filename + ".xml");
//			
//		if (xmlReader.IsStartElement ("Player Information")) {
//			if (xmlReader.IsStartElement ("Player Location")) {
//				
//				float x = float.Parse (xmlReader ["x"]);
//				float y = float.Parse (xmlReader ["y"]);
//				float z = float.Parse (xmlReader ["z"]);
//
//			}
//			if (xmlReader.IsStartElement ("Player Rotation")) {
//
//				float x = float.Parse (xmlReader ["x"]);
//				float y = float.Parse (xmlReader ["y"]);
//				float z = float.Parse (xmlReader ["z"]);
//
//			}
//		}
//
//		return playerTransform;
//		return playerTransformRotation;
//	}
}
