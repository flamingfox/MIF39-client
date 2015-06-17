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
		System.Console.WriteLine("sid :" + sid);
		
		System.Guid id = new System.Guid (sid);
		System.Console.WriteLine("id :" + id);
		
		BinaryLoader.loadAsciiFromBinary (br);
		BinaryLoader.loadAsciiFromBinary (br);
		
		//name = new string( br.ReadChars(2) );
		//System.Console.WriteLine("name : " + name);
		
		System.Console.WriteLine("dataSize : " + new string(br.ReadChars(2)) );
	}*/
	
	public virtual void loadFromTcp (TcpSocket ts)
	{


		System.Console.WriteLine("***** Header ******");

		System.Guid id = ts.receiveMessageGuid();
		System.Console.WriteLine("id : " + id);
		System.Console.WriteLine("id : " + id.ToString());
		
		long nameLenght = ts.receiveMessageInt64 ();
		System.Console.WriteLine("nameLength : " + nameLenght);
		
		name = new string( ts.receiveMessageChars((int)nameLenght) );
		System.Console.WriteLine("name : " + name);
	}
}
