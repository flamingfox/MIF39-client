using UnityEngine;
using System;
using System.IO;

public class HeaderContainer : Header
{
	private long dataSize;

	public HeaderContainer ()
	{
		dataSize = 0;
	}

	public long getDataSize ()
	{
		return dataSize;
	}

	public void	setDataSize (long newDataSize)
	{
		dataSize = newDataSize;
	}

	/*public void loadFromBinary (BinaryReader br)
	{

		string sid = new string (br.ReadChars (36 + 2));
		Debug.Log ("sid :" + sid);

		System.Guid id = new System.Guid (sid);
		Debug.Log ("id :" + id);

		BinaryLoader.loadAsciiFromBinary (br);
		BinaryLoader.loadAsciiFromBinary (br);

		//name = new string( br.ReadChars(2) );
		//Debug.Log("name : " + name);

		dataSize = br.ReadInt32 ();
		Debug.Log ("dataSize : " + dataSize);
		
		Debug.Log ("dataSize : " + new string (br.ReadChars (2)));
	}*/

	public override void loadFromTcp (TcpSocket ts)
	{
		Debug.Log ("***** HeaderContainer ******");

		base.loadFromTcp (ts);
		
		dataSize = ts.receiveMessageInt ();

		Debug.Log ("HeaderContainer : "+getId()+"; "+getName()+"; "+getDataSize());
	}
}
