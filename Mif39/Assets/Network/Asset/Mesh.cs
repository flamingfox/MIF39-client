using UnityEngine;
using System;
using System.Collections.Generic;

public class TriangleAsset
{
	bool hasNormals;
	bool hasTexCoords;
	int[] vertexIndices;
	int[] normalIndices;
	int[] textCoordIndices;
	
	TriangleAsset(){
		vertexIndices = new int[3];
		normalIndices = new int[3];
		textCoordIndices = new int[3];
	}

	public int[] VertexIndices {
		get {
			return vertexIndices;
		}
	}

	public static TriangleAsset loadFromTcp (TcpSocket ts)
	{
		TriangleAsset retour = new TriangleAsset();

		retour.hasNormals = ts.receiveMessageBool();
		Debug.Log ("hasNormals :" + retour.hasNormals);

		retour.hasTexCoords = ts.receiveMessageBool();
		Debug.Log ("hasTexCoords :" + retour.hasTexCoords);

		for (int i=0; i<3; i++) {
			retour.vertexIndices[i] = ts.receiveMessageInt()+1;
			Debug.Log ("vertexIndices["+i+"] :" + retour.vertexIndices[i]);
		}

		
		//if (retour.hasNormals) {
			for (int i=0; i<3; i++) {
				retour.normalIndices[i] = ts.receiveMessageInt()+1;
				Debug.Log ("normalIndices["+i+"] :" + retour.normalIndices[i]);
			}
		//}
		
		//if (retour.hasTexCoords) {
			for (int i=0; i<3; i++) {
				retour.textCoordIndices[i] = ts.receiveMessageInt()+1;
				Debug.Log ("textCoordIndices["+i+"] :" + retour.textCoordIndices[i]);
			}
		//}

		return retour;
	}
}

public class MaterialGroupAsset : Asset
{
	private Guid idMateriaux, idMesh;
	private int nbFaces;
	private TriangleAsset[] faces;

	public MaterialGroupAsset(){
	}

	public static MaterialGroupAsset loadFromTcp (TcpSocket ts)
	{
		MaterialGroupAsset retour = new MaterialGroupAsset();

		//base.loadFromTcp (ts);
		retour.idMateriaux = ts.receiveMessageGuid();
		retour.idMesh = ts.receiveMessageGuid ();

		retour.nbFaces = ts.receiveMessageShort();
		
		retour.faces = new TriangleAsset[retour.nbFaces];
		for (int i=0; i<retour.nbFaces; i++) {
			retour.faces[i] = TriangleAsset.loadFromTcp(ts);
		}

		return retour;
	}
}

public class MeshAsset : Asset
{
	private uint nbVertice;
	private Vector3[] vertices;
	private uint nbTexVertices;
	private Vector2[] texVertices;
	private uint nbNormal;
	private Vector3[] normal;
	private uint nbFaces;
	private TriangleAsset[] faces;
	private uint nbMaterialGroups;
	private MaterialGroupAsset[] materialGroups;

	public MeshAsset ()
	{
	}

	public static MeshAsset loadFromTcp (TcpSocket ts)
	{
		MeshAsset retour = new MeshAsset ();

		//MeshTopology.Points;

		retour.hc.loadFromTcp (ts);

		retour.nbVertice = ts.receiveMessageUInt();

		//Debug.Log ("nbVertice " + retour.nbVertice);

		retour.vertices = new Vector3[retour.nbVertice];
		for (int i=0; i<retour.nbVertice; i++) {

			Vector3 t = new Vector3(ts.receiveMessageFloat(),
			                        ts.receiveMessageFloat(),
			                        ts.receiveMessageFloat());

			//Debug.Log("Vertice ["+i+"] = "+t);

			retour.vertices[i] = t;
		}

		retour.nbTexVertices = ts.receiveMessageUInt();

		retour.texVertices = new Vector2[retour.nbTexVertices];
		//Debug.Log ("nbTexVertices " + retour.nbTexVertices);
		for (int i=0; i<retour.nbTexVertices; i++) {
			retour.texVertices[i] = new Vector2(ts.receiveMessageFloat(),
			                         ts.receiveMessageFloat());
		}

		retour.nbNormal = ts.receiveMessageUInt();		
		//Debug.Log ("nbNormal " + retour.nbNormal);

		retour.normal = new Vector3[retour.nbNormal];
		for (int i=0; i<retour.nbNormal; i++) {
			/*retour.vertices[i] = new Vector3(ts.receiveMessageFloat(),
			                      ts.receiveMessageFloat(),
			                      ts.receiveMessageFloat());*/
			
			Vector3 t = new Vector3(ts.receiveMessageFloat(),
			                        ts.receiveMessageFloat(),
			                        ts.receiveMessageFloat());
			
			//Debug.Log("Normal ["+i+"] = "+t);
			
			retour.normal[i] = t;
		}

		retour.nbFaces = ts.receiveMessageUInt();

		//Debug.Log ("nbFaces " + retour.nbFaces);

		retour.faces = new TriangleAsset[retour.nbFaces];
		for (int i=0; i<retour.nbFaces; i++) {
		//for (int i=0; i<2; i++) {
			Debug.Log("/******** Triangle ["+i+"] *********/");
			retour.faces[i] = TriangleAsset.loadFromTcp(ts);
		}

		retour.nbMaterialGroups = ts.receiveMessageUInt();

		//Debug.Log ("nbMaterialGroups " + retour.nbMaterialGroups);

		retour.materialGroups = new MaterialGroupAsset[retour.nbMaterialGroups];
		for (int i=0; i<retour.nbMaterialGroups; i++) {
			retour.materialGroups[i] = MaterialGroupAsset.loadFromTcp(ts);
		}

		return retour;
	}

	public Mesh getMesh(){
		Mesh retour = new Mesh();

		retour.vertices = new Vector3[nbVertice];
		retour.vertices = vertices;

		retour.uv = new Vector2[nbTexVertices];
		retour.uv = texVertices;

		retour.RecalculateNormals();

		retour.triangles = new int[nbFaces*3];
		for (int i=0; i<nbFaces; i++){
			retour.triangles[i*3] = faces[i].VertexIndices[0];
			retour.triangles[i*3+1] = faces[i].VertexIndices[1];
			retour.triangles[i*3+2] = faces[i].VertexIndices[2];
		}
		retour.RecalculateBounds();
		return retour;
	}
}