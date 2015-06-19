using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

class MainClass
{
	// Use this for initialization
	void Start () {

		Console.WriteLine("start");

		TcpSocket ts = new TcpSocket ();
		
		ts.ConnectTo ("192.168.1.33", 3000);
		
		ts.sendMessage ("0");
		ts.sendMessage ("0");
		
		Message m = new Message ();
		
		//m.loadFromTcp (ts);
		
		
		Byte[] receive = ts.receiveMessageAll ();
		
		// String to store the response ASCII representation.
		String responseData = String.Empty;
		
		responseData = System.Text.Encoding.ASCII.GetString (receive, 0, receive.Length);
		Console.WriteLine ("Received length: {0}", receive.Length);
		Console.WriteLine ("Received: {0}", responseData);
		
		
		
		Console.WriteLine ("Hello World!");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
