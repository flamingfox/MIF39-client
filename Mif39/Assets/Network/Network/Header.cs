using UnityEngine;
using System;
using System.IO;

public class Header
{
	private System.Guid id;
	private string name;
	
	public Header(){
		name = "";
	}
	
	public System.Guid getId() {
		return id;
	}
	
	public void	setId( System.Guid newId) {
		id = newId;
	}
	
	public string getName() {
		return name;
	}
	
	public void	setName( string newName) {
		name = newName;
	}
	
	/*public void loadFromBinary(BinaryReader br){
		
		string sid = new string( br.ReadChars(36+2) );
		Debug.Log("sid :" + sid);
		
		System.Guid id = new System.Guid (sid);
		Debug.Log("id :" + id);
		
		BinaryLoader.loadAsciiFromBinary (br);
		BinaryLoader.loadAsciiFromBinary (br);
		
		//name = new string( br.ReadChars(2) );
		//Debug.Log("name : " + name);
		
		Debug.Log("dataSize : " + new string(br.ReadChars(2)) );
	}*/
	
	public virtual void loadFromTcp (TcpSocket ts)
	{

		//Debug.Log("***** Header ******");

		id = ts.receiveMessageGuid();
		//Debug.Log("id : " + id);
		
		long nameLenght = ts.receiveMessageLong ();
		//Debug.Log("nameLength : " + nameLenght);
		
		name = new string( ts.receiveMessageChars((int)nameLenght) );
		Debug.Log ("header : "+getId()+"; "+getName());
	}
}
