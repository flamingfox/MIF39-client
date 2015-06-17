using System;

public class Message
{
	protected Header h;
	protected HeaderContainer hc;
	protected Asset a;

	protected string typeOfData;

	public Message (){
		h = null;
		hc = null;
		a = null;

		typeOfData = "";
	}

	public void loadFromTcp (TcpSocket ts)
	{
		h.loadFromTcp (ts);
		hc.loadFromTcp (ts);
		loadAssetFromTcp (ts);
	}

	private void loadAssetFromTcp (TcpSocket ts){
		if (hc.getName().Equals ("Mesh")) {
			a = Mesh.loadFromTcp(ts);
		}
	}
}
