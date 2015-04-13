using UnityEngine;
using System.Collections;
/*
 * var newVertices : Vector3[];
 * var newUV : Vector2[];
 * var newTriangles : int[];*/


public class forme1 : MonoBehaviour {

	public float width = 50f;
	public float height = 50f;
	
	// Use this for initialization
	void Start () {
		Debug.Log ("coucou"); 	// affichage sur la sortie

		// création d'un mesh
		 
	/*	var mesh : Mesh = new Mesh ();
		GetComponent.<MeshFilter>().mesh = mesh;*/
		MeshFilter mf = GetComponent<MeshFilter>();
		Mesh mesh = new Mesh ();

		mf.mesh = mesh;

		//vertices
		Vector3[] vertices = new Vector3[4]
		{
			new Vector3 (0, 0, 0),new Vector3 (width, 0, 0),new Vector3 (0, 0, height),new Vector3 (width, 0, height)

		};

		/* 2    3
		 *  
		 * 0    1 faces 0 2 1   et   2 3 1 
		 */

	

		//faces
		int[] tri = new int[6];
	
		tri[0] = 0;
		tri[1] = 2;
		tri[2] = 1;

		tri[3] = 2;
		tri[4] = 3;
		tri[5] = 1;
			
		
		//mesh.triangles = newTriangles;

		// normales (si on veut)

		Vector3[] normals = new Vector3[4];
		// La meme normale pour les quatre points : 0 0 -1. forward est un raccourci pour le vetcteur 0 0 1
		normals[0] = -Vector3.forward;
		normals[1] = -Vector3.forward;
		normals[2] = -Vector3.forward;
		normals[3] = -Vector3.forward;

		// textures		
		Vector2[] uv = new Vector2[4];

		uv[0]= new Vector2(0,0);
		uv[1]= new Vector2(1,0);
		uv[2]= new Vector2(0,1);
		uv[3]= new Vector2(1,1);

		//mesh.uv = newUV;



		/* Assign */

		mesh.vertices = vertices;
		mesh.triangles = tri;
		mesh.normals = normals;
		mesh.uv = uv;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
