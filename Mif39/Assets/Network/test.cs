using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Collections;

public class test : MonoBehaviour {

	public void connexion (Text tx)
	{	
		string ipAdresse = tx.text;

		if(ipAdresse == ""){
			ipAdresse = "192.168.1.33";
		}

		TcpSocket ts = GameObject.Find("/Net").GetComponent<TcpSocket>();

		ts.ConnectTo (ipAdresse, 3000);
		
		ts.sendMessageString("0");
		ts.sendMessageString("2");

		Message m = new Message ();
		
		m.loadFromTcp (ts);


		if (m.H.getName () == Asset.ASSET_TYPE_MESH) {
			GameObject go = new GameObject();
			go.name = "mesh test";
			go.AddComponent<MeshFilter>().mesh = ((MeshAsset)m.A).getMesh();
			go.AddComponent<MeshRenderer>();
			//go.transform.position = new Vector3(-1,-1,-11);
			GameObject.Find("/Fond").SetActive(false);
			GameObject.Find("/GUI").SetActive(false);
		}

		
		/*Byte[] receive = ts.receiveMessageAll ();
		
		// String to store the response ASCII representation.
		String responseData = String.Empty;

		for (int i = 0; i < receive.Length; i++) {
			if(receive[i] != 0)
				responseData += System.Text.Encoding.ASCII.GetString (receive, i, 1);
			else
				responseData += "_";
		}
		print ("Received length: "+receive.Length);
		print ("Received: "+responseData);*/

		ts.razCompteur ();
	}
}