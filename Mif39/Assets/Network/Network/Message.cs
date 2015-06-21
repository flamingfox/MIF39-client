using UnityEngine;
using System;

public class Message
{
	protected HeaderContainer h;
	protected Asset a;

	protected string typeOfData;

	public Message (){
		h = new HeaderContainer();
		a = null;

		typeOfData = "";
	}

	public HeaderContainer H {
		get {
			return h;
		}
	}

	public Asset A {
		get {
			return a;
		}
	}

	public void loadFromTcp (TcpSocket ts)
	{
		h.loadFromTcp (ts);

		a = Asset.loadFromTcp (ts, h);

	}
}
