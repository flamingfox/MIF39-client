using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

public class TcpSocket
{
	private TcpClient tc = new TcpClient ();

	NetworkStream ns;
	BinaryReader br;

	long tailleMessageRestant = 0;

	public TcpSocket ()	{}

	~ TcpSocket(){
		br.Close ();
		ns.Close ();
		tc.Close ();
	}

	public void ConnectTo(string adress, int port){
		tc.Connect(adress, port);

		ns = tc.GetStream ();
		
		br = new BinaryReader (ns);
	}

	public void sendMessage(string message){
		
		System.Console.WriteLine ("envois de "+ message );
		
		// Translate the passed message into ASCII and store it as a Byte array.
		Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
		
		Byte[] size = BitConverter.GetBytes( data.Length );
		
		// Send the message to the connected TcpServer. 
		ns.Write(size, 0, sizeof(int));
		ns.Write(data, 0, data.Length);
		
		System.Console.WriteLine ("fin envois de "+ message );
		System.Console.WriteLine ();
		
	}

	public Byte[] receiveMessageAll(){
		
		if (tailleMessageRestant <= 0) {
			System.Console.WriteLine ("debut reception");

			tailleMessageRestant = br.ReadInt32();
			Console.WriteLine ("tailleMessageRestant: {0}", tailleMessageRestant);
		}

		Console.WriteLine ("tailleMessageRestant: {0}", tailleMessageRestant);

		// Read the first batch of the TcpServer response bytes.
		Byte[] data = br.ReadBytes ((int)tailleMessageRestant);

		tailleMessageRestant -= data.Length;

		return data;
	}

	public Byte[] receiveMessageBytes(long nbByte){
		
		if (tailleMessageRestant <= 0) {
			System.Console.WriteLine ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Console.WriteLine ("tailleMessageRestant: {0}", tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		Byte[] data = br.ReadBytes ((int)nbByte);

		tailleMessageRestant -= data.Length;
		
		return data;
	}

	public System.Guid receiveMessageGuid(){

		if (tailleMessageRestant <= 0) {
			System.Console.WriteLine ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Console.WriteLine ("tailleMessageRestant: {0}", tailleMessageRestant);
		}

		string sid = new string (receiveMessageChars (36+2));

		System.Console.WriteLine("sid : " + sid);
		
		System.Guid data = new System.Guid (sid);

		return data;
	}

	public bool receiveMessageBool ()
	{
		if (tailleMessageRestant <= 0) {
			System.Console.WriteLine ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Console.WriteLine ("tailleMessageRestant: {0}", tailleMessageRestant);
		}

		// Read the first batch of the TcpServer response bytes.
		bool data = br.ReadBoolean();
		
		tailleMessageRestant -= 1;
		
		return data;
	}

	public Int16 receiveMessageInt16(){
		
		if (tailleMessageRestant <= 0) {
			System.Console.WriteLine ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Console.WriteLine ("tailleMessageRestant: {0}", tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		Int16 data = br.ReadInt16();
		
		tailleMessageRestant -= 16;
		
		return data;
	}

	public UInt16 receiveMessageUInt16 ()
	{
		if (tailleMessageRestant <= 0) {
			System.Console.WriteLine ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Console.WriteLine ("tailleMessageRestant: {0}", tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		UInt16 data = br.ReadUInt16();
		
		tailleMessageRestant -= 16;
		
		return data;
	}

	public Int32 receiveMessageInt32(){
		
		if (tailleMessageRestant <= 0) {
			System.Console.WriteLine ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Console.WriteLine ("tailleMessageRestant: {0}", tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		Int32 data = br.ReadInt32();
		
		tailleMessageRestant -= 32;
		
		return data;
	}

	public UInt32 receiveMessageUInt32 ()
	{
		if (tailleMessageRestant <= 0) {
			System.Console.WriteLine ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Console.WriteLine ("tailleMessageRestant: {0}", tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		UInt32 data = br.ReadUInt32();
		
		tailleMessageRestant -= 32;
		
		return data;
	}

	public Int64 receiveMessageInt64(){
		
		if (tailleMessageRestant <= 0) {
			System.Console.WriteLine ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Console.WriteLine ("tailleMessageRestant: {0}", tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		Int64 data = br.ReadInt64();
		
		tailleMessageRestant -= 64;
		
		return data;
	}

	public UInt64 receiveMessageUInt64(){
		
		if (tailleMessageRestant <= 0) {
			System.Console.WriteLine ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Console.WriteLine ("tailleMessageRestant: {0}", tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		UInt64 data = br.ReadUInt64();
		
		tailleMessageRestant -= 64;
		
		return data;
	}

	public Char[] receiveMessageChars(int nb){
		
		if (tailleMessageRestant <= 0) {
			System.Console.WriteLine ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Console.WriteLine ("tailleMessageRestant: {0}", tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		Char[] data = br.ReadChars(nb);
		
		tailleMessageRestant -= nb;
		
		return data;
	}

	public Double receiveMessageDouble(){
		
		if (tailleMessageRestant <= 0) {
			System.Console.WriteLine ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Console.WriteLine ("tailleMessageRestant: {0}", tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		Double data = br.ReadDouble();
		
		tailleMessageRestant -= 64;
		
		return data;
	}

	public float receiveMessageFloat ()
	{
		if (tailleMessageRestant <= 0) {
			System.Console.WriteLine ("debut reception");
			
			tailleMessageRestant = br.ReadInt32();
			Console.WriteLine ("tailleMessageRestant: {0}", tailleMessageRestant);
		}
		
		// Read the first batch of the TcpServer response bytes.
		float data = br.ReadSingle();
		
		tailleMessageRestant -= 32;
		
		return data;
	}
}