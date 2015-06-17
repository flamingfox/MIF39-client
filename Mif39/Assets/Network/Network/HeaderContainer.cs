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
		System.Console.WriteLine ("sid :" + sid);

		System.Guid id = new System.Guid (sid);
		System.Console.WriteLine ("id :" + id);

		BinaryLoader.loadAsciiFromBinary (br);
		BinaryLoader.loadAsciiFromBinary (br);

		//name = new string( br.ReadChars(2) );
		//System.Console.WriteLine("name : " + name);

		dataSize = br.ReadInt32 ();
		System.Console.WriteLine ("dataSize : " + dataSize);
		
		System.Console.WriteLine ("dataSize : " + new string (br.ReadChars (2)));
	}*/

	public override void loadFromTcp (TcpSocket ts)
	{

		System.Console.WriteLine ();
		System.Console.WriteLine ("***** HeaderContainer ******");
		System.Console.WriteLine ();

		base.loadFromTcp (ts);
		
		dataSize = ts.receiveMessageInt32 ();
		System.Console.WriteLine ("dataSize : " + dataSize);
		System.Console.WriteLine ();
	}
}
