using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

public class TcpSocket : MonoBehaviour
{
	public string ipAdresse;
	public int port = 3000;

	private TcpClient tc = new TcpClient ();

	NetworkStream ns;
	BinaryReader br;

	long tailleMessageRestant = 0;

	protected TcpSocket ()	{}

	~ TcpSocket(){
		br.Close ();
		ns.Close ();
		tc.Close ();
	}

	public void ConnectTo(string adress, int port){

		if (!tc.Connected) {
			ipAdresse = adress;
			this.port = port;

			tc.Connect (ipAdresse, port);

			ns = tc.GetStream ();

			br = new BinaryReader (ns);
		}
	}

	public void razCompteur(){
		tailleMessageRestant = 0;
	}

	public void sendMessageString(string message){

		if (!ns.CanWrite) {
			tc.Close();
			ConnectTo(ipAdresse, port);
		}

		Debug.Log ("envois de "+ message );

		// Translate the passed message into ASCII and store it as a Byte array.
		Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
		
		Byte[] size = BitConverter.GetBytes( data.Length );
		
		// Send the message to the connected TcpServer. 
		//ns.BeginWrite(data, 0, sizeof(int), 

		ns.Write(size, 0, sizeof(int));
		ns.Flush ();
		ns.Write(data, 0, data.Length);
		ns.Flush ();
		
		//Debug.Log ("fin envois de "+ message );
		
	}

	public void sendMessageBytes(Byte[] message){

		if (!ns.CanWrite) {
			tc.Close();
			ConnectTo(ipAdresse, port);
		}

		Debug.Log ("envois du message en Bytes");
		
		Byte[] size = BitConverter.GetBytes( message.Length );
		
		// Send the message to the connected TcpServer. 
		ns.Write(size, 0, sizeof(int));
		ns.Flush ();
		ns.Write(message, 0, message.Length);
		ns.Flush ();
		//Debug.Log ("fin envois du message en Bytes");
		
	}

	public Byte[] receiveMessageAll(){
		
		if (tailleMessageRestant <= 0) {
			Debug.Log("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log("tailleMessageRestant: "+tailleMessageRestant);
		}

		//Debug.Log ("tailleMessageRestant: "+ tailleMessageRestant);

		// Read the first batch of the TcpServer response bytes.
		Byte[] data = br.ReadBytes ((int)tailleMessageRestant);

		tailleMessageRestant -= data.Length;


		return data;
	}

	public Byte[] receiveMessageBytes(long nbByte){
		
		if (tailleMessageRestant <= 0) {
			Debug.Log("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log("tailleMessageRestant: "+tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		Byte[] data = br.ReadBytes ((int)nbByte);

		tailleMessageRestant -= data.Length;

		Debug.Log ("tailleMessageRestant: "+ tailleMessageRestant);

		return data;
	}

	public System.Guid receiveMessageGuid(){

		if (tailleMessageRestant <= 0) {
			Debug.Log("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log("tailleMessageRestant: "+tailleMessageRestant);
		}

		string sid = new string (receiveMessageChars (36+2));

		//Debug.Log("sid : " + sid);
		
		System.Guid data = new System.Guid (sid);

		Debug.Log ("tailleMessageRestant: "+ tailleMessageRestant);

		return data;
	}

	public bool receiveMessageBool ()
	{
		if (tailleMessageRestant <= 0) {
			Debug.Log("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log("tailleMessageRestant: "+tailleMessageRestant);
		}

		// Read the first batch of the TcpServer response bytes.
		bool data = br.ReadBoolean();
		
		tailleMessageRestant -= sizeof(bool);

		//Debug.Log ("tailleMessageRestant: "+ tailleMessageRestant+"; result : "+data);

		return data;
	}

	public short receiveMessageShort(){
		
		if (tailleMessageRestant <= 0) {
			Debug.Log("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log("tailleMessageRestant: "+tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		short data = br.ReadInt16();
		
		tailleMessageRestant -= sizeof(short);

		//Debug.Log ("tailleMessageRestant: "+ tailleMessageRestant+"; result : "+data);

		return data;
	}

	public ushort receiveMessageUShort ()
	{
		if (tailleMessageRestant <= 0) {
			Debug.Log("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log("tailleMessageRestant: "+tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		ushort data = br.ReadUInt16();
		
		tailleMessageRestant -= sizeof(ushort);

		//Debug.Log ("tailleMessageRestant: "+ tailleMessageRestant+"; result : "+data);

		return data;
	}

	public int receiveMessageInt(){
		
		if (tailleMessageRestant <= 0) {
			Debug.Log("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log("tailleMessageRestant: "+tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		int data = br.ReadInt32();
		
		tailleMessageRestant -= sizeof(int);

		//Debug.Log ("tailleMessageRestant: "+ tailleMessageRestant+"; result : "+data);

		return data;
	}

	public uint receiveMessageUInt ()
	{
		if (tailleMessageRestant <= 0) {
			Debug.Log("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log("tailleMessageRestant: "+tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		uint data = br.ReadUInt32();
		
		tailleMessageRestant -= sizeof(uint);

		//Debug.Log ("tailleMessageRestant: "+ tailleMessageRestant+"; result : "+data);

		return data;
	}

	public long receiveMessageLong(){
		
		if (tailleMessageRestant <= 0) {
			Debug.Log("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log("tailleMessageRestant: "+tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		long data = br.ReadInt64();
		
		tailleMessageRestant -= sizeof(long);

		//Debug.Log ("tailleMessageRestant: "+ tailleMessageRestant+"; result : "+data);

		return data;
	}

	public ulong receiveMessageULong(){
		
		if (tailleMessageRestant <= 0) {
			Debug.Log("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log("tailleMessageRestant: "+tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		ulong data = br.ReadUInt64();
		
		tailleMessageRestant -= sizeof(ulong);

		//Debug.Log ("tailleMessageRestant: "+ tailleMessageRestant+"; result : "+data);

		return data;
	}

	public char receiveMessageChar(){
		
		if (tailleMessageRestant <= 0) {
			Debug.Log("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log("tailleMessageRestant: "+tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		Char data = br.ReadChar();
		
		tailleMessageRestant -= 1;
		//tailleMessageRestant -= sizeof(char)*nb;
		
		//Debug.Log ("tailleMessageRestant: "+ tailleMessageRestant);
		
		return data;
	}

	public char[] receiveMessageChars(int nb){
		
		if (tailleMessageRestant <= 0) {
			Debug.Log("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log("tailleMessageRestant: "+tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		char[] data = br.ReadChars(nb);
		
		tailleMessageRestant -= 1*nb;
		//tailleMessageRestant -= sizeof(char)*nb;

		//Debug.Log ("tailleMessageRestant: "+ tailleMessageRestant);

		return data;
	}

	public double receiveMessageDouble(){
		
		if (tailleMessageRestant <= 0) {
			Debug.Log("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log("tailleMessageRestant: "+tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		double data = br.ReadDouble();
		
		tailleMessageRestant -= sizeof(double);

		//Debug.Log ("tailleMessageRestant: "+ tailleMessageRestant+"; result : "+data);

		return data;
	}

	public float receiveMessageFloat ()
	{
		if (tailleMessageRestant <= 0) {
			Debug.Log("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log("tailleMessageRestant: "+tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		float data = br.ReadSingle();
		
		tailleMessageRestant -= sizeof(float);

		//Debug.Log ("tailleMessageRestant: "+ tailleMessageRestant+"; result : "+data);

		return data;
	}

	/************************************/
	/*									*/
	/************************************/


	static public Byte[] guidToByte(System.Guid toConvert){
		
		Byte[] data;

		// Translate the passed message into ASCII and store it as a Byte array.
		data = System.Text.Encoding.ASCII.GetBytes(toConvert.ToString());
		
		return data;
	}
	
	/*public Byte[] boolToByte (bool toConvert)
	{
		if (tailleMessageRestant <= 0) {
			Debug.Log ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log ("tailleMessageRestant: {0}", tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		bool data = br.ReadBoolean();
		
		tailleMessageRestant -= 1;
		
		return data;
	}
	
	public Byte[] int16ToByte(Int16 toConvert){
		
		if (tailleMessageRestant <= 0) {
			Debug.Log ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log ("tailleMessageRestant: {0}", tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		Int16 data = br.ReadInt16();
		
		tailleMessageRestant -= 16;
		
		return data;
	}
	
	public Byte[] uInt16ToByte (UInt16 toConvert)
	{
		if (tailleMessageRestant <= 0) {
			Debug.Log ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log ("tailleMessageRestant: {0}", tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		UInt16 data = br.ReadUInt16();
		
		tailleMessageRestant -= 16;
		
		return data;
	}
	
	public Byte[] int32ToByte(Int32 toConvert){
		
		if (tailleMessageRestant <= 0) {
			Debug.Log ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log ("tailleMessageRestant: {0}", tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		Int32 data = br.ReadInt32();
		
		tailleMessageRestant -= 32;
		
		return data;
	}
	
	public Byte[] uInt32ToByte (UInt32 toConvert)
	{
		if (tailleMessageRestant <= 0) {
			Debug.Log ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log ("tailleMessageRestant: {0}", tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		UInt32 data = br.ReadUInt32();
		
		tailleMessageRestant -= 32;
		
		return data;
	}
	
	public Byte[] int64ToByte(Int64 toConvert){
		
		if (tailleMessageRestant <= 0) {
			Debug.Log ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log ("tailleMessageRestant: {0}", tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		Int64 data = br.ReadInt64();
		
		tailleMessageRestant -= 64;
		
		return data;
	}
	
	public Byte[] uInt64ToByte(UInt64 toConvert){
		
		if (tailleMessageRestant <= 0) {
			Debug.Log ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log ("tailleMessageRestant: {0}", tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		UInt64 data = br.ReadUInt64();
		
		tailleMessageRestant -= 64;
		
		return data;
	}
	
	public Byte[] charsToByte(char[] toConvert){
		
		if (tailleMessageRestant <= 0) {
			Debug.Log ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log ("tailleMessageRestant: {0}", tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		Char[] data = br.ReadChars(nb);
		
		tailleMessageRestant -= nb;
		
		return data;
	}
	
	public Byte[] doubleToByte(double toConvert){
		
		if (tailleMessageRestant <= 0) {
			Debug.Log ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log ("tailleMessageRestant: {0}", tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		Double data = br.ReadDouble();
		
		tailleMessageRestant -= 64;
		
		return data;
	}
	
	public Byte[] floatToByte (float toConvert)
	{
		if (tailleMessageRestant <= 0) {
			Debug.Log ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Debug.Log ("tailleMessageRestant: {0}", tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		float data = br.ReadSingle();
		
		tailleMessageRestant -= 32;
		
		return data;
	}*/
}