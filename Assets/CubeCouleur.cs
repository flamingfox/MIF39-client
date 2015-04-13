using UnityEngine;
using System.Collections;

public class CubeCouleur : MonoBehaviour {
	public GameObject reference;
	int compteur=0;
	int nbTour = 0;
	Renderer rend;
	// Use this for initialization
	void Start () {
	
		rend = GetComponent<Renderer>();
		rend.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (compteur == 60) {
			// changement de couleur
			if(nbTour==0)
			{
				rend.material.color = Color.red;
				nbTour=1;
			}
			else
			{
				rend.material.color = Color.blue;
				nbTour=0;
			}
			compteur=0;
		} 

		compteur++;
	
	}
}
