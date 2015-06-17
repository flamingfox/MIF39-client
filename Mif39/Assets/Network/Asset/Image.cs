using System;

public class Image
{
	private uint width, height, depth, nChannels;
	private ushort[] data;

	public Image ()
	{
	}

	public static Image loadFromTcp (TcpSocket ts)
	{
		Image retour = new Image ();
		
		retour.width = ts.receiveMessageUInt16();
		retour.height = ts.receiveMessageUInt16();
		retour.depth = ts.receiveMessageUInt16();
		retour.nChannels = ts.receiveMessageUInt16();

		ulong imageSize = retour.width * retour.height * retour.depth / 8;
		
		retour.data = new ushort[imageSize];
		for (ulong i=0; i<imageSize; i++) {
			retour.data[i] = ts.receiveMessageUInt16();
		}
		
		return retour;
	}
}

