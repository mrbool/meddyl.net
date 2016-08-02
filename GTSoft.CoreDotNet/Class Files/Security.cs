using System;
using System.Security.Cryptography;
using System.Text;

namespace GTSoft.CoreDotNet
{
    /// <summary>
    /// Handles encryption and application security.
    /// </summary>
    public class Security
    {
        public Security()
        {
        }

        /// <summary>
        /// ASCII encodes a string, encrypts it using MD5
        /// encryption, then converts it to a Base64 string.
        /// </summary>
        /// <param name="input">String to encode.</param>
        /// <returns>Encrypted String.</returns>
        public string EncryptString(string input)
        {
            // get new md5 provider object
            MD5 md5 = new MD5CryptoServiceProvider();
            // get new ASCIIEncoding object
            ASCIIEncoding AE = new ASCIIEncoding();
            // get byte array from input string
            byte[] byteArray = AE.GetBytes(input);
            // get a hash from md5 object
            byte[] newHash = md5.ComputeHash(byteArray);
            // return a Base64 string from the hash
            return Convert.ToBase64String(newHash);
        }

        /// <summary>
        /// Compares a clear text string to an ecnrypted string that
        /// was encrypted using Security.EncryptString().
        /// </summary>
        /// <param name="input">Clear text string to compare.</param>
        /// <param name="compare">Encrypted string to compare against.</param>
        /// <returns>Boolean true if values match, false if they don't.</returns>
        public bool Compare(string input, string compare)
        {
            // Encrypt clear text string
            string newVal = EncryptString(input);
            // Compare values
            int result = compare.CompareTo(newVal);

            if (result == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Compares a clear text string to an ecnrypted string that
        /// was encrypted using Security.DESEncryptString().
        /// </summary>
        /// <param name="input">Clear text string to compare.</param>
        /// <param name="compare">comma-delimited string of bytes representing the encrypted string.</param>
        /// <param name="desKey">Key that the DES Encryptor uses</param>
        /// <param name="desIV">Initialization Vector that the DES Encryptor uses</param>
        /// <returns>Boolean true if values match, false if they don't.</returns>
        protected bool DESCompare(string input, string compare, byte[] desKey, byte[] desIV)
        {
            int retval = compare.CompareTo(DESDecryptString(input, desKey, desIV));
            if (retval == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// takes an input string and encrypts it using DES encryption,
        /// and returns a comma-delimited string of bytes (for database storage).
        /// </summary>
        /// <param name="input">string to encrypt</param>
        /// <param name="desKey">Key that the DES Encryptor uses</param>
        /// <param name="desIV">Initialization Vector that the DES Encryptor uses</param>
        /// <returns>comma delimited string of bytes representing the encrypted value</returns>
        public string DESEncryptString(string input, byte[] desKey, byte[] desIV)
        {
            //Get a new instance of the DESCryptoServiceProvider
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputBuffer = new ASCIIEncoding().GetBytes(input);

            //create DES Encryptor from this instance
            ICryptoTransform desEncrypt = des.CreateEncryptor(desKey, desIV);
            byte[] encrypted = desEncrypt.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);

            //Save the encrypted string to the database as a comma-delimited byte string
            return ConvertByteArrayToDelimitedString(encrypted);
        }

        /// <summary>
        /// Takes a comma-delimited string of bytes and decrypts it using DES encryption.
        /// </summary>
        /// <param name="input">comma-delimited string of bytes</param>
        /// <param name="desKey">Key that the DES Encryptor uses</param>
        /// <param name="desIV">Initialization Vector that the DES Encryptor uses</param>
        /// <returns>string representation of the decrypted byte string</returns>
        /// <remarks>
        /// Only works for strings encrypted with the EncryptString method of this class
        /// using the same public key and Initialization Vector.
        ///</remarks>
        public string DESDecryptString(string input, byte[] desKey, byte[] desIV)
        {
            //Get a new instance of the DESCryptoServiceProvider
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] encrypted = ConvertDelimitedStringToByteArray(input);

            //create DES Decryptor from our des instance
            ICryptoTransform desDecrypt = des.CreateDecryptor(desKey, desIV);
            byte[] decrypted = desDecrypt.TransformFinalBlock(encrypted, 0, encrypted.Length);
            return ConvertByteArrayToCharString(decrypted);

        }

        /// <summary>
        /// Takes a byte array decrypts it using DES encryption.
        /// </summary>
        /// <param name="encrypted">byte array of encrypted string</param>
        /// <param name="desKey">Key that the DES Encryptor uses</param>
        /// <param name="desIV">Initialization Vector that the DES Encryptor uses</param>
        /// <returns>string representation of the decrypted byte string</returns>
        /// <remarks>
        /// Only works for strings encrypted with the EncryptString method of this class
        /// using the same public key and Initialization Vector.
        ///</remarks>
        public string DESDecryptString(byte[] encrypted, byte[] desKey, byte[] desIV)
        {
            //Get a new instance of the DESCryptoServiceProvider
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            //create DES Decryptor from our des instance
            ICryptoTransform desDecrypt = des.CreateDecryptor(desKey, desIV);
            byte[] decrypted = desDecrypt.TransformFinalBlock(encrypted, 0, encrypted.Length);
            return ConvertByteArrayToCharString(decrypted);
        }

        /// <summary>
        /// Takes a comma-delimited string of bytes and converts it to a byte array.
        /// </summary>
        /// <param name="input">comma-delimited string of bytes</param>
        /// <returns>Byte Array of the comma-delimited array of strings sent to it.</returns>
        /// <remarks>
        /// Has a public scope so the application can convert the public Key and
        /// Initialization Vector to a byte array for the DESCryptoServiceProvider to use.
        /// </remarks>
        public byte[] ConvertDelimitedStringToByteArray(string input)
        {
            //Create a Char Array to hold the Split delimiter
            char[] delimiter = { Convert.ToChar(",") };
            string[] temp = input.Split(delimiter, 100);

            //Take each element of the returned String array and copy it to the Byte Array
            int index = 0;
            byte[] retval = new byte[temp.Length];
            foreach (string s in temp)
            {
                retval.SetValue(Convert.ToByte(s), index);
                index += 1;
            }
            return retval;
        }

        /// <summary>
        /// Takes a byte array and converts it to a character string of the array elements
        /// </summary>
        /// <param name="input">array of bytes to be converted to string</param>
        /// <returns>string representation of the input byte array.</returns>
        /// <remarks> Couldn't use the Convert.ToBase64String method. </remarks>
        private string ConvertByteArrayToCharString(byte[] input)
        {
            string retval = "";
            foreach (byte b in input)
            { retval += Convert.ToChar(b); }
            return retval;
        }

        /// <summary>
        /// Takes a byte array and converts it to a comma-delimited string of bytes.
        /// </summary>
        /// <param name="input">array of bytes to be converted to a comma-delimited string</param>
        /// <returns>comma-delimited string of bytes for database storage</returns>
        private string ConvertByteArrayToDelimitedString(byte[] input)
        {
            string temp = "";
            foreach (byte b in input)
            {
                temp += Convert.ToString(b) + ",";
            }
            return temp.Substring(0, temp.Length - 1);
        }

    }
}
