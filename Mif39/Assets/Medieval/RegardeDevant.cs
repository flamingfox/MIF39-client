using UnityEngine;
using System.Collections;

public class RegardeDevant : MonoBehaviour {
	public bool yarienDevant;
	// Use this for initialization
	void Start () {
		yarienDevant = true;
	}
	
	void OnCollisionEnter(Collision collision)
	{
		
		Debug.Log(" je rentre dans un truc" );
		yarienDevant = false;
	}


	void OnCollisionExit (Collision collision) {
		yarienDevant = true;
	}

}
