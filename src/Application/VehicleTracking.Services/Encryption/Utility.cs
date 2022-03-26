using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Service.Encryption
{
    public static class Utility
    {
		public static string EncryptString(string Source)
		{

			string strRet = null;
			string strSub = null;
			ArrayList arrOffsets = new ArrayList();
			int intCounter = 0;
			int intMod = 0;
			int intVal = 0;
			int intNewVal = 0;

			arrOffsets.Insert(0, 73);
			arrOffsets.Insert(1, 56);
			arrOffsets.Insert(2, 31);
			arrOffsets.Insert(3, 58);
			arrOffsets.Insert(4, 77);
			arrOffsets.Insert(5, 75);

			strRet = "";

			for (intCounter = 0; intCounter <= Source.Length - 1;
			intCounter++)
			{
				strSub = Source.Substring(intCounter, 1);
				intVal =
				(int)System.Text.Encoding.ASCII.GetBytes(strSub)[0];
				intMod = intCounter % arrOffsets.Count;
				intNewVal = intVal +
				Convert.ToInt32(arrOffsets[intMod]);
				intNewVal = intNewVal % 256;
				strRet = strRet + intNewVal.ToString("X2");
			}
			return strRet;

		}
		public static string DecryptString(string Source)
		{

			ArrayList arrOffsets = new ArrayList();
			int intCounter = 0;
			int intMod = 0;
			int intVal = 0;
			int intNewVal = 0;
			string strOut = null;
			string strSub = null;
			string strSub1 = null;
			string strDecimal = null;

			arrOffsets.Insert(0, 73);
			arrOffsets.Insert(1, 56);
			arrOffsets.Insert(2, 31);
			arrOffsets.Insert(3, 58);
			arrOffsets.Insert(4, 77);
			arrOffsets.Insert(5, 75);

			strOut = "";
			for (intCounter = 0; intCounter <= Source.Length - 1;
			intCounter += 2)
			{
				strSub = Source.Substring(intCounter, 1);
				strSub1 = Source.Substring((intCounter + 1), 1);
				intVal = int.Parse(strSub,
				System.Globalization.NumberStyles.HexNumber) * 16 + int.Parse(strSub1,
				System.Globalization.NumberStyles.HexNumber);
				intMod = (intCounter / 2) % arrOffsets.Count;
				intNewVal = intVal -
				Convert.ToInt32(arrOffsets[intMod]) + 256;
				intNewVal = intNewVal % 256;
				strDecimal = ((char)intNewVal).ToString();
				strOut = strOut + strDecimal;
			}
			return strOut;

		}
	}
}
