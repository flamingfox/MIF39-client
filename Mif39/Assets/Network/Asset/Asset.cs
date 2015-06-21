using UnityEngine;
using System;
using System.Net.Sockets;

public abstract class Asset
{
	static public string ASSET_TYPE_MESH = "Mesh";
	static public string ASSET_TYPE_IMAGE = "Image";
	//static public string ASSET_TYPE_MESH = "Mesh";

	protected Header hc;

	public Asset ()
	{
		hc = new Header();
	}

	public Header getHc() {
		return hc;
	}

	public void setHc(Header h) {
		this.hc = h;
	}

	static public Asset loadFromTcp (TcpSocket ts, Header messageHeader)
	{
		Asset a;

		if (messageHeader.getName ().Equals (ASSET_TYPE_MESH)) {
			Debug.Log("***** Mesh loader ******");
			a = MeshAsset.loadFromTcp (ts);

			return a;
		} else if (messageHeader.getName ().Equals (ASSET_TYPE_IMAGE)) {
			Debug.Log("***** Image loader ******");
			a = ImageAsset.loadFromTcp (ts);

			Debug.Log ("image : " + ((ImageAsset)a).getHeight () + " " + ((ImageAsset)a).getWidth () + " " + ((ImageAsset)a).getNChannels () + " " + ((ImageAsset)a).getDepth ());
			return a;
		}

		return null;
	}
}

