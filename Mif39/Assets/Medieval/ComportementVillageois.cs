using UnityEngine;
using System.Collections;

public class ComportementVillageois : MonoBehaviour {

	// Pour l'instant, on suppose que le villageois (personnage) est présent dans la map, on va y attacher ce script de comportement
	// Use this for initialization
	void Start () 
	{
		transform.gameObject.AddComponent<MeshCollider>();
		//transform.position = Vector3(44, 32.507, 92);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
