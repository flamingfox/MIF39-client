using UnityEngine;
using System.Collections;

public class Initialisation : MonoBehaviour {

	public Vector3[] posVillageois;

	public GameObject kidgirl ; 
	public GameObject kidboy ; 
	public GameObject woman ; 
	public GameObject man; 
	//ScriptableObject
	
	// Use this for initialization
	void Start () {
		// a changer car sera rempli par le serveur
		posVillageois = new Vector3[100]; 
		int k = 0;
		for (int i = 0; i < 100; i=i+10) {
			for (int j = 0; j < 100; j= j+10) {
				posVillageois[k].x = i;
				posVillageois[k].y = 35;
				posVillageois[k].z = j;
				k++;
			}
		}





		// On crée N gameObject correspondant aux villageois
		for (int i = 0; i < 1; i++) {
			GameObject temp = new GameObject() ; 


			int comp =1;//Random.Range (0, 3);
			int sex = 0;//Random.Range (0, 2);
			if (comp == 0) 
			{// c'est un enfante
				if(sex==0){
					temp = (GameObject) Instantiate(kidgirl,posVillageois[i], Quaternion.identity) ; 
				}
				if(sex==1)
					temp = (GameObject) Instantiate(kidboy,posVillageois[i], Quaternion.identity) ;
			} 
			else if (comp == 1) 
			{// c'est un adulte;
				if(sex==0)
					temp = (GameObject) Instantiate(woman,posVillageois[i], Quaternion.identity) ; 
				if(sex==1)
					temp = (GameObject) Instantiate(man,posVillageois[i], Quaternion.identity);
			} 
			else 
			{// vieux
				if(sex==0)
					temp = (GameObject) Instantiate(kidgirl,posVillageois[i], Quaternion.identity) ; 
				if(sex==1)
					temp = (GameObject) Instantiate(kidboy,posVillageois[i], Quaternion.identity);
			}


			temp.AddComponent<ComportementVillageois>() ; 

			//Temporaire pour positionner le villageoi devant la caméra
			temp.transform.position = new Vector3(40, 32, 90);

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
