using UnityEngine;
using System.Collections;

public class ComportementVillageois : MonoBehaviour {

	// EN PARAMETRE : VITESSE , POSITION( x , y ,z ) serveur (vecteur de vector3 ?)
	// On suppose que les positions d'initialisation des villageois sont prévues hors collision


	public float vitesse ; 
	public float x , y , z ; 
	public GameObject roiArthur;
	public bool connaissance;


	Renderer rend;

	// Pour l'instant, on suppose que le villageois (personnage) est présent dans la map, on va y attacher ce script de comportement
	// Use this for initialization
	void Start () 
	{
		transform.gameObject.AddComponent<BoxCollider> ();
		transform.gameObject.AddComponent<Rigidbody>() ;
		rend = GetComponent<Renderer>();

		transform.gameObject.GetComponent<Rigidbody>().mass = 1000f  ;

		Vector3 pos = new Vector3 (x, y, z);
		transform.position = pos;

		roiArthur = GameObject.Find ("Arthur");

		connaissance = true;
	}
	
	// Update is called once per frame
	void Update () {
		//print (GetComponent<MeshCollider>.); 

		RaycastHit hit, hitr, hitl;
		float distance, distancer, distancel;
		if (Physics.Raycast (transform.position, transform.forward, out hit)) {
			distance = hit.distance; 

			if(distance<7)
			{
				Physics.Raycast (transform.position, -transform.right, out hitl);
				distancel = hitl.distance;
				Physics.Raycast (transform.position, transform.right, out hitr); 
				distancer = hitr.distance;

				if(distancel < distancer) // on veut aller à droite
				{
					transform.Rotate(transform.up);
					transform.Rotate(transform.up);
					transform.Rotate(transform.up);

				}
				else
				{
					transform.Rotate(-transform.up);
					transform.Rotate(-transform.up);
					transform.Rotate(-transform.up);
				}


			}

		}

		if (Physics.Raycast (transform.position, transform.right, out hit)) {
			distance = hit.distance ; 
			if(distance < 2)
				transform.Rotate(-transform.up);

		}
		if (Physics.Raycast (transform.position, -transform.right, out hit)) {
			distance = hit.distance ; 
			if(distance < 2)
				transform.Rotate(transform.up);
			
		}




		if (connaissance && (Vector3.Distance (transform.position, roiArthur.transform.position) <= 10)) {

			rend.material.color = Color.blue;
			transform.LookAt(roiArthur.transform.position);
			print ("Hey Arthur !!!");
			//connaissance = false; // a modifier
		
		} else if (connaissance) {
			rend.material.color = Color.magenta;
			transform.position += transform.forward*vitesse;
		} 
		else 
		{
			rend.material.color= Color.red;
			transform.position += transform.forward*vitesse;

		}

	
	}



}
