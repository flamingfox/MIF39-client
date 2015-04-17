using UnityEngine;
using System.Collections;

public class RegardeDevant : MonoBehaviour {
	public bool yarienDevant;
	// Use this for initialization
	void Start () {
		yarienDevant = true;
	}
	
	void OnCollisionEnter(UnityEngine.Collision collision)
	{
		yarienDevant = false;
	}
	void OnCollisionExit (UnityEngine.Collision collision) {
		yarienDevant = true;
	}
}
