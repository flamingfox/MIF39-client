using UnityEngine;
using System.Collections;
using System;


// ACCELERER RALENTIR EN FONCTION DU TEMPS 
// + mesh collider sur tout les perso 

public class ComportementVillageois : MonoBehaviour {

	// EN PARAMETRE : VITESSE , POSITION( x , y ,z ) serveur (vecteur de vector3 ?)
	// On suppose que les positions d'initialisation des villageois sont prévues hors collision
	float marcher = 1f ; 
	float trot = 2f ;
	float courir = 5f ;
	float arret = 0f ;
	float vitesse ; 
	//float x , y , z ; 
	GameObject roiArthur;
	bool connaissance;
	int vu ,type,v;
	Renderer rend;
	bool colli=false;
	int c=0;
	int compteurRot=0;
	int r=0;

	// Use this for initialization
	void Start () 
	{
		// On suppose que l'objet a dejà tout ce qu'il faut, i.e un rigidbody, un meshrenderer (ou skinnedMeshRendrer sur le fils), un box collider à la bonne taille.
		// Pour avoir le meme box collider pour tout objet de type Anna jeune : mettre anna dans la scene, ajouter ce qu'il faut dont le box collider, mettre le centre
		// de son maillage (fils Polygon dans cet exemple) en centre du box collider d'anna, et mettre deux fois les tailles des extents du maillage comme taille du box collider
		// puis créer un prefab à partir de ce modèle (create prefab dans le folder medieval, puis on fait glisser le perso de la map surle prefab
		// enfin, mettre le prefab dans le script initialisation

		roiArthur = GameObject.Find ("Arthur");

		if (UnityEngine.Random.Range (0, 2) == 0)
			connaissance = true;
		else 
			connaissance = false; 

		vu = 0; // sinon vu = 100 


		vitesse = arret; 
		v = 0;
	}
	
	bool floatEgal(Vector3 p1, Vector3 p2)
	{
		print ("x   "+((Math.Round (p1.x, 1) == Math.Round (p2.x, 1)) + "......." + (Math.Round (p1.x, 1) == Math.Round (p2.x, 1) + 0.1) + "......." + (Math.Round (p1.x, 1) == Math.Round (p2.x, 1) - 0.1)));
		print ("y   "+((Math.Round (p1.y, 1) == Math.Round (p2.y, 1)) + "......." + (Math.Round (p1.y, 1) == Math.Round (p2.y, 1) + 0.1) + "......." + (Math.Round (p1.y, 1) == Math.Round (p2.y, 1) - 0.1)));
		print ("z   "+((Math.Round (p1.z, 1) == Math.Round (p2.z, 1)) + "......." + (Math.Round (p1.z, 1) == Math.Round (p2.z, 1) + 0.1) + "......." + (Math.Round (p1.z, 1) == Math.Round (p2.z, 1) - 0.1)));


		return     (((Math.Round (p1.x, 1) == Math.Round (p2.x, 1)) || (Math.Round (p1.x, 1) == Math.Round (p2.x, 1)+0.1) || (Math.Round (p1.x, 1) == Math.Round (p2.x, 1)-0.1))
		        && ((Math.Round (p1.y, 1) == Math.Round (p2.y, 1)) || (Math.Round (p1.y, 1) == Math.Round (p2.y, 1) +0.1) || (Math.Round (p1.y, 1) == Math.Round (p2.y, 1)-0.1))
		        && ((Math.Round (p1.z, 1) == Math.Round (p2.z, 1)) || (Math.Round (p1.z, 1) == Math.Round (p2.z, 1)+0.1) || (Math.Round (p1.z, 1) == Math.Round (p2.z, 1)-0.1) ));
	}


	/* 
	 ************************************************* UPDATE 
	 */

	void Update () 
	{

		//Debug.Log ("fils : " + transform.FindChild ("devant")); 

		if ((! transform.GetComponentInChildren < RegardeDevant > ().yarienDevant) && compteurRot == 0) { 

			vitesse = marcher;
			//r = UnityEngine.Random.Range (0, 180); 
			r=180;
			r=r/120;
			transform.Rotate (r*transform.up);
			compteurRot++;

		} else if (compteurRot == 120) 
		{
			compteurRot=0;
		}
		else if(compteurRot!= 0)
		{
			transform.Rotate (r*transform.up);
			compteurRot++;
			transform.position += transform.forward * vitesse * Time.deltaTime;
		}
		else 
		{
			if (v == 500) {
				// Eventuellement, ajouter une gestion de manière à ce que un enfant qui ait courru 30km passe à l'arret (energie)
				if (type == 0) {// c'est un enfant, on gère le comportement de la vitesse
					enfant ();
				} else if (type == 1) {// c'est un adulte
					adulte ();
				} else {// vieux
					vieux ();
				}
			
				v = 0; 
			}
			v++;



			RaycastHit hit, hitr, hitl;
			float distance, distancer, distancel;
			if (Physics.Raycast (transform.position, transform.forward, out hit)) {
				distance = hit.distance; 
				if (distance < 7) {
					Physics.Raycast (transform.position, -transform.right, out hitl);
					distancel = hitl.distance;
					Physics.Raycast (transform.position, transform.right, out hitr); 
					distancer = hitr.distance;

					if (distancel < distancer) {
						transform.Rotate (transform.up);
					} else {
						transform.Rotate (-transform.up);
					}
				}
			}
			if (Physics.Raycast (transform.position, transform.right, out hit)) {
				distance = hit.distance; 
				if (distance < 2)
					transform.Rotate (-transform.up);
			}

			if (Physics.Raycast (transform.position, -transform.right, out hit)) {
				distance = hit.distance; 
				if (distance < 2)
					transform.Rotate (transform.up);
			}
			transform.position += transform.forward * vitesse * Time.deltaTime;
		}

		/*	if (connaissance && (Vector3.Distance (transform.position, roiArthur.transform.position) <= 10)&& vu<60  ) {

			if(vu==0){
				transform.LookAt(roiArthur.transform.position);
				print ( transform.gameObject.name + " dit: Hey Arthur !!!");
			}
			vu ++;
		} else if (connaissance) {
			transform.position += transform.forward*vitesse*Time.deltaTime ;
		} 
		else
		 {
			transform.position += transform.forward*vitesse*Time.deltaTime ;
		}	*/
			
	 
	/*	else 
		{
			c++;
			if(c==120)
			{
				c=0;
				colli=false;
			}
		}*/
	}

	void enfant() {
		int rand = UnityEngine.Random.Range(0,100); 

		if (rand < 15) 
			vitesse = arret;
		else if (rand < 65 && rand > 15)
			vitesse = trot;
		else 
			vitesse = courir;
	}

	void adulte() {
		int rand = UnityEngine.Random.Range(0, 100); 
		
		if (rand < 50) 
			vitesse = arret;
		else if (rand < 85 && rand > 50)
			vitesse = marcher;
		else 
			vitesse = trot;
	}

	void vieux() {
		int rand = UnityEngine.Random.Range(0, 100); 
		
		if (rand < 60) 
			vitesse = arret;
		else 
			vitesse = marcher;
	}
}
