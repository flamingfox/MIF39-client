//using UnityEngine;
using System;
using System.IO;

public class BinaryLoader
{
	static public BinaryReader loadFromBinaryFile (string file)
	{
		BinaryReader br = new BinaryReader (File.Open (file, FileMode.Open));
		return br;
	}

	static public Char loadAsciiFromBinary(BinaryReader br){
		Char retour;
		string tmp = "";


		for (int i = 0; i<7; i++) {
			tmp += br.ReadBoolean();
		}
		
		System.Console.WriteLine ("tmp = " + tmp);

		//retour = System.Text.Encoding.ASCII.GetChars (tmp);

		byte b = Convert.ToByte(tmp, 2);

		retour = Convert.ToChar(b);

		System.Console.WriteLine ("char = " + retour);

		return retour;
	}
}

