using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOAN_BMCSDL
{
    public class SymmetricEncryption : Form
    {

        public static char[] character = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        public static string EncryptAddition(string plaintext, int key)
        {
            char[] charArray = plaintext.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                for (int j = 0; j < character.Length; j++)
                {
                    if (char.ToUpper(charArray[i]) == character[j])
                    {
                        int remainder = (j + key) % character.Length;
                        charArray[i] = character[remainder];
                        break;
                    }
                }
            }
            return new string(charArray);
        }
        public static string DecryptAddition(string ciphertext, int key)
        {
            char[] charArray = ciphertext.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                for (int j = 0; j < character.Length; j++)
                {
                    if (char.ToUpper(charArray[i]) == character[j])
                    {
                        int remainder = (j - key) % character.Length;
                        if (remainder < 0)
                            remainder = character.Length + remainder;
                        charArray[i] = character[remainder];
                        break;
                    }
                }
            }
            return new string(charArray);
        }

        public static string EncryptMultiplication(string plaintext, int key)
        {
            char[] charArray = plaintext.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                for (int j = 0; j < character.Length; j++)
                {
                    if (char.ToUpper(charArray[i]) == character[j])
                    {
                        int remainder = (j * key) % character.Length;
                        charArray[i] = character[remainder];
                        break;
                    }
                }
            }
            return new string(charArray);
        }

        public static int keyInverse(int modulo, int key)
        {
            int r1 = modulo, r2 = key;
            int t, q;
            int t1 = 0, t2 = 1;

            // Áp dụng thuật toán Euclid mở rộng
            while (r2 > 0)
            {
                q = r1 / r2;
                int r = r1 - r2 * q;
                r1 = r2;
                r2 = r;

                t = t1 - (t2 * q);
                t1 = t2;
                t2 = t;
            }

            if(t1 >= 0)
                return t1;
            else 
                return modulo + t1;
        }

        public static string DecryptMultiplication(string ciphertext, int key)
        {
            char[] charArray = ciphertext.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                for (int j = 0; j < character.Length; j++)
                {
                    if (char.ToUpper(charArray[i]) == character[j])
                    {
                        int remainder =((j * keyInverse(character.Length, key)) % character.Length);
                        charArray[i] = character[remainder];
                        break;
                    }
                }
            }
            return new string(charArray);
        }
    }

}

