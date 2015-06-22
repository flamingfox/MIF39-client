using UnityEngine;
using System;

public class ImageAsset : Asset
{
	private uint width, height, depth, nChannels;
	private char[] data;

	public ImageAsset ()
	{
	}

	public uint getWidth ()
	{
		return width;
	}

	public uint getHeight ()
	{
		return height;
	}

	public uint getDepth ()
	{
		return depth;
	}

	public uint getNChannels ()
	{
		return nChannels;
	}

	public static ImageAsset loadFromTcp (TcpSocket ts)
	{

		ImageAsset retour = new ImageAsset ();

		retour.hc.loadFromTcp (ts);
		
		retour.width = ts.receiveMessageUInt ();
		Debug.Log ("width " + retour.width);
		retour.height = ts.receiveMessageUInt ();
		Debug.Log ("height " + retour.height);
		retour.depth = ts.receiveMessageUInt ();
		Debug.Log ("depth " + retour.depth);
		retour.nChannels = ts.receiveMessageUInt ();
		Debug.Log ("nChannels " + retour.nChannels);

		ulong imageSize = retour.width * retour.height * retour.depth / 8;
		Debug.Log ("imageSize " + imageSize);

		retour.data = new char[imageSize];
		retour.data = ts.receiveMessageChars((int)imageSize);
		/*for (ulong i=0; i<imageSize; i++) {
			retour.data [i] = ts.receiveMessageChar();
		}*/
		
		return retour;
	}
}

