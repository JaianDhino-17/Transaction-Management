using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows.Forms;
using System.IO;

namespace DOAN_BMCSDL
{
    public class AsymmestricEncryption:Form
    {

        public static BigInteger publicKey;
        public static BigInteger privateKey;
        public static BigInteger n;

        public static BigInteger Coprime (BigInteger N)
        {
            BigInteger e = 2;
            while (e < N)
            {
                if (BigInteger.GreatestCommonDivisor(e, N) == 1)
                    return e;
                e++;
            }
            return 0;
        }
        public static void pub_k_And_pri_k(int p, int q)
        {
            if (p == q) 
                return;

            n = p * q;
            if (n > SymmetricEncryption.character.Length)
            {
                MessageBox.Show("Cặp giá trị có tích lớn hơn chiều dài của chuỗi ký tự hiện có!");
                return;
            }
            BigInteger N = (p-1) * (q-1);

            BigInteger e = Coprime(N);
            if (e == 0)
            {
                MessageBox.Show("Không tìm thấy số nguyên tố cùng nhau với phi!");
                return;
            }

            BigInteger d = SymmetricEncryption.keyInverse((int)N, (int)e);

            publicKey = e;
            privateKey = d;

            // Lưu trữ khóa công khai vào file
            File.WriteAllText("publicKey.pem", publicKey.ToString());
            // Lưu trữ khóa bí mật vào file
            File.WriteAllText("privateKey.pem", privateKey.ToString());
            // Lưu trữ n vào file
            File.WriteAllText("nValue.pem", n.ToString());
        }
        public static string Encrypt_RSA(string plaintext)
        {
            char[] charArray = plaintext.ToCharArray();
            string stringArray = "";
            for (int i = 0; i < charArray.Length; i++)
            {
                for (int j = 0; j < SymmetricEncryption.character.Length; j++)
                {
                    if (char.ToUpper(charArray[i]) == SymmetricEncryption.character[j])
                    {
                        string C;
                        BigInteger value = new BigInteger(j);
                        BigInteger publicKey = BigInteger.Parse(File.ReadAllText("publicKey.pem"));
                        BigInteger n = BigInteger.Parse(File.ReadAllText("nValue.pem"));
                        BigInteger encrypted = BigInteger.ModPow(value, publicKey, n);
                        int encrypted2 = int.Parse(encrypted.ToString());
                        if (encrypted >= 0 && encrypted <= 9)
                            C = "0" + $"{encrypted}";
                        else
                            C = $"{encrypted}";
                        stringArray += C;
                        break;
                    }
                }
            }
            return stringArray;
        }

        public static string Decrypt_RSA(string ciphertext)
        {
            char[] charArray = ciphertext.ToCharArray();
            string stringArray = "";
            for (int i = 0; i < charArray.Length; i = i + 2)
            {
                string charPair = charArray[i].ToString() + charArray[i + 1].ToString();
                int intPair = int.Parse(charPair);
                BigInteger value = new BigInteger(intPair);
                BigInteger privateKey = BigInteger.Parse(File.ReadAllText("privateKey.pem"));
                BigInteger n = BigInteger.Parse(File.ReadAllText("nValue.pem"));
                BigInteger decrypted = BigInteger.ModPow(value, privateKey, n);
                stringArray += SymmetricEncryption.character[(int)decrypted];
            }
            return stringArray;
        }
        public static string Encrypt_RSAwithPublic_key(string plaintext, BigInteger pu_k)
        {
            char[] charArray = plaintext.ToCharArray();
            string stringArray = "";
            for (int i = 0; i < charArray.Length; i++)
            {
                for (int j = 0; j < SymmetricEncryption.character.Length; j++)
                {
                    if (char.ToUpper(charArray[i]) == SymmetricEncryption.character[j])
                    {
                        string C;
                        BigInteger value = new BigInteger(j);
                        BigInteger encrypted = BigInteger.ModPow(value, pu_k, n);
                        int encrypted2 = int.Parse(encrypted.ToString());
                        if (encrypted >= 0 && encrypted <= 9)
                            C = "0" + $"{encrypted}";
                        else
                            C = $"{encrypted}";
                        stringArray += C;
                        break;
                    }
                }
            }
            return stringArray;
        }
        public static string Decrypt_RSA_withPrivate_key(string ciphertext, BigInteger pr_k, BigInteger n)
        {
            char[] charArray = ciphertext.ToCharArray();
            string stringArray = "";
            for (int i = 0; i < charArray.Length; i = i + 2)
            {
                string charPair = charArray[i].ToString() + charArray[i + 1].ToString();
                int intPair = int.Parse(charPair);
                BigInteger value = new BigInteger(intPair);
                BigInteger decrypted = BigInteger.ModPow(value, pr_k, n);
                stringArray += SymmetricEncryption.character[(int)decrypted];
            }
            return stringArray;
        }
    }
}
