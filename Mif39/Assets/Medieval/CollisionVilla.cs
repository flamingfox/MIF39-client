using UnityEngine;
using System.Collections;

public class CollisionVilla : MonoBehaviour {

	// Use this for initialization
	void Start () {

		for(int i=0; i < transform.childCount ; i++)
		{
			transform.GetChild(i).gameObject.AddComponent<MeshCollider>();
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
