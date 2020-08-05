using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb.DataHandling
{
    public class Encryption
    {
        char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'X', 'Y', 'Z', 'Æ', 'Ø', 'Å', ' ' };
        char[] symbols = { '!', '@', '#', '£', '¤', '$', '%', '&', '/', '{', '}', '(', ')', '[', ']', '+', '?', '|', '~', ';', '*', '-', '_', '.', ';', '<', '>', '§', '½' };
        
        //This method is used to find the index position of a given character.
        public int[] GetIndexNumber(char[] charactersToConvert)
        {
            int[] indexNumber = new int[charactersToConvert.Length];
            for (int i = 0; i < charactersToConvert.Length; i++)
            {
                for (int j = 0; j < letters.Length; j++)
                {
                    if (charactersToConvert[i].ToString().ToUpper() == letters[j].ToString())
                    {
                        indexNumber[i] = j;
                    }
                }
            }
            return indexNumber;
        }
        //It will convert a text to random symbols and return a tuple with the encrypted string and a int array with a key to decrypt 
        public Tuple<string, int[]> EncryptString(string stringToEncrypt)
        {
            Random random = new Random();
            int[] encryptioncode;
            char[] input = stringToEncrypt.ToCharArray();
            int number = 0;
            encryptioncode = GetIndexNumber(stringToEncrypt.ToCharArray());
            for (int i = 0; i < stringToEncrypt.Length; i++)
            {
                number = random.Next(symbols.Length);
                input[i] = symbols[number];

            }
            stringToEncrypt = ConvertCharArrayToString(input);
            Tuple<string, int[]> tuple = new Tuple<string, int[]>(stringToEncrypt, encryptioncode);
            return tuple;
        }

        public string ConvertCharArrayToString(char[] chars)
        {
            string result = string.Empty;
            for (int i = 0; i < chars.Length; i++)
            {
                result += chars[i];
            }
            return result;
        }
        public string Decryption(Tuple<string, int[]> encryptedString)
        {
            string decryptedString = string.Empty;
            for (int i = 0; i < encryptedString.Item1.Length; i++)
            {
                if (i > 0)
                {
                    decryptedString += letters[encryptedString.Item2[i]].ToString().ToLower();
                }
                else
                {
                    decryptedString += letters[encryptedString.Item2[i]];
                }
            }
            return decryptedString;
        }
    }
}
