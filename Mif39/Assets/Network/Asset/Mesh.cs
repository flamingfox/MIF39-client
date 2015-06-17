using UnityEngine;
using System;
using System.Collections.Generic;

public class Triangle
{
	bool hasNormals;
	bool hasTexCoords;
	int[] vertexIndices;
	int[] normalIndices;
	int[] textCoordIndices;
	
	Triangle(){
		vertexIndices = new int[3];
		normalIndices = new int[3];
		textCoordIndices = new int[3];
	}
	
	public static Triangle loadFromTcp (TcpSocket ts)
	{
		Triangle retour = new Triangle();

		retour.hasNormals = ts.receiveMessageBool();
		retour.hasTexCoords = ts.receiveMessageBool();
		
		for (int i=0; i<3; i++) {
			retour.vertexIndices[i] = ts.receiveMessageInt16();
		}
		
		if (retour.hasNormals) {
			for (int i=0; i<3; i++) {
				retour.normalIndices[i] = ts.receiveMessageInt16();
			}
		}
		
		if (retour.hasTexCoords) {
			for (int i=0; i<3; i++) {
				retour.textCoordIndices[i] = ts.receiveMessageInt16();
			}
		}

		return retour;
	}
}

public class MaterialGroup : Asset
{
	private Guid idMateriaux, idMesh;
	private int nbFaces;
	private Triangle[] faces;

	public MaterialGroup(){
	}

	public static MaterialGroup loadFromTcp (TcpSocket ts)
	{
		MaterialGroup retour = new MaterialGroup();

		//base.loadFromTcp (ts);
		retour.idMateriaux = ts.receiveMessageGuid();
		retour.idMesh = ts.receiveMessageGuid ();

		retour.nbFaces = ts.receiveMessageInt16();
		
		retour.faces = new Triangle[retour.nbFaces];
		for (int i=0; i<retour.nbFaces; i++) {
			retour.faces[i] = Triangle.loadFromTcp(ts);
		}

		return retour;
	}
}

public class Mesh : Asset
{
	private int nbVertice;
	private Vector3[] vertices;
	private int nbTexVertices;
	private Vector2[] texVertices;
	private int nbFaces;
	private Triangle[] faces;
	private int nbMaterialGroups;
	private MaterialGroup[] materialGroups;

	public Mesh ()
	{
	}

	public static Mesh loadFromTcp (TcpSocket ts)
	{
		Mesh retour = new Mesh ();

		retour.nbVertice = ts.receiveMessageInt16();
		
		retour.vertices = new Vector3[retour.nbVertice];
		for (int i=0; i<retour.nbVertice; i++) {
			retour.vertices[i] = new Vector3(ts.receiveMessageFloat(),
			                      ts.receiveMessageFloat(),
			                      ts.receiveMessageFloat());
		}

		retour.nbTexVertices = ts.receiveMessageInt16();
		
		retour.texVertices = new Vector2[retour.nbTexVertices];
		for (int i=0; i<retour.nbTexVertices; i++) {
			retour.texVertices[i] = new Vector2(ts.receiveMessageFloat(),
			                         ts.receiveMessageFloat());
		}

		retour.nbFaces = ts.receiveMessageInt16();

		retour.faces = new Triangle[retour.nbFaces];
		for (int i=0; i<retour.nbFaces; i++) {
			retour.faces[i] = Triangle.loadFromTcp(ts);
		}

		retour.nbMaterialGroups = ts.receiveMessageInt16();

		retour.materialGroups = new MaterialGroup[retour.nbMaterialGroups];
		for (int i=0; i<retour.nbMaterialGroups; i++) {
			retour.materialGroups[i] = MaterialGroup.loadFromTcp(ts);
		}

		return retour;
	}
}