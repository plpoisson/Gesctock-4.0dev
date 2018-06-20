using System;
using System.Text;
using System.Security.Cryptography;

namespace Nop.Plugin.Payments.Monetico
{
    public class CMCIC_Hmac
    {
        private byte[] _sUsableKey;

        public CMCIC_Hmac(string cle)
        {

            _sUsableKey = _getUsableKey(cle);

        }


        private byte[] _getUsableKey(string cle)
        {

            string hexStrKey = cle.Substring(0, 38);
            string hexFinal = cle.Substring(38, 2) + "00";

            int cca0 = (int)hexFinal[0];

            if (cca0 > 70 && cca0 < 97)
            {
                hexStrKey += (char)(cca0 - 23) + hexFinal.Substring(1, 1);
            }
            else
            {
                if (hexFinal.Substring(1, 1) == "M")
                    hexStrKey += hexFinal.Substring(0, 1) + "0";
                else
                    hexStrKey += hexFinal.Substring(0, 2);
            }


            return (byte[])hexStringToByteArray(hexStrKey);

        }

        public string computeHmac(string sData)
        {

            HMACSHA1 TpeHmac = new HMACSHA1(_sUsableKey);

            TpeHmac.Initialize();
            byte[] bytes = Encoding.ASCII.GetBytes(sData);
            byte[] ba = TpeHmac.ComputeHash(bytes);

            return this.byteArrayToHexString(ba);
        }

        public bool isValidHmac(string sChaineMAC, string sMAC)
        {
            if (string.IsNullOrEmpty(sChaineMAC)) return false;
            if (string.IsNullOrEmpty(sMAC)) return false;
            return (this.computeHmac(sChaineMAC) == sMAC.ToLower());
        }

        private object hexStringToByteArray(string hs)
        {

            string str = "";
            if (hs.StartsWith("0x"))
            {
                str = str + hs.Substring(2, hs.Length - 2);
            }
            else
            {
                str = str + hs;
            }

            int num = (int)Math.Round((double)(((double)str.Length) / 2.0));

            if (str.Length != (2 * num))
            {
                str = "0" + str;
            }

            byte[] buffer = new byte[((int)Math.Round((double)((((double)str.Length) / 2.0) - 1.0))) + 1];

            int num3 = buffer.Length - 1;
            for (int i = 0; i <= num3; i++)
            {

                buffer[i] = Convert.ToByte(str.Substring(2 * i, 2), 0x10);
            }

            return (byte[])buffer.Clone();
        }

        private string byteArrayToHexString(byte[] ba)
        {

            string str = "";
            int num2 = ba.Length - 1;

            for (int i = 0; i <= num2; i++)
            {
                str = str + ba[i].ToString("x2");
            }

            return str;
        }
    }
}
