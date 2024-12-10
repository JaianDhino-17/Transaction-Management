using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows.Forms;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Oracle.ManagedDataAccess.Types;

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

        //                      MÃ HÓA MỨC ỨNG DỤNG (ĐÃ TEST)

        //public static string Encrypt_RSA(string plaintext)
        //{
        //    char[] charArray = plaintext.ToCharArray();
        //    string stringArray = "";
        //    for (int i = 0; i < charArray.Length; i++)
        //    {
        //        for (int j = 0; j < SymmetricEncryption.character.Length; j++)
        //        {
        //            if (char.ToUpper(charArray[i]) == SymmetricEncryption.character[j])
        //            {
        //                string C;
        //                BigInteger value = new BigInteger(j);
        //                BigInteger publicKey = BigInteger.Parse(File.ReadAllText("publicKey.pem"));
        //                BigInteger n = BigInteger.Parse(File.ReadAllText("nValue.pem"));
        //                BigInteger encrypted = BigInteger.ModPow(value, publicKey, n);
        //                int encrypted2 = int.Parse(encrypted.ToString());
        //                if (encrypted >= 0 && encrypted <= 9)
        //                    C = "0" + $"{encrypted}";
        //                else
        //                    C = $"{encrypted}";
        //                stringArray += C;
        //                break;
        //            }
        //        }
        //    }
        //    return stringArray;
        //}

        //public static string Decrypt_RSA(string ciphertext)
        //{
        //    char[] charArray = ciphertext.ToCharArray();
        //    string stringArray = "";
        //    for (int i = 0; i < charArray.Length; i = i + 2)
        //    {
        //        string charPair = charArray[i].ToString() + charArray[i + 1].ToString();
        //        int intPair = int.Parse(charPair);
        //        BigInteger value = new BigInteger(intPair);
        //        BigInteger privateKey = BigInteger.Parse(File.ReadAllText("privateKey.pem"));
        //        BigInteger n = BigInteger.Parse(File.ReadAllText("nValue.pem"));
        //        BigInteger decrypted = BigInteger.ModPow(value, privateKey, n);
        //        stringArray += SymmetricEncryption.character[(int)decrypted];
        //    }
        //    return stringArray;
        //}


        //                                  MÃ HÓA MỨC CƠ SỞ DỮ LIỆU
        public static string Encrypt_RSA(string plainText)
        {
            try
            {
                string Function = "CRYPTO.RSA_ENCRYPT";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = Database.conn;
                cmd.CommandText = Function;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter resultParam = new OracleParameter();
                resultParam.ParameterName = "Result";
                resultParam.OracleDbType = OracleDbType.Varchar2;
                resultParam.Size = 10000;
                resultParam.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(resultParam);

                OracleParameter pltext = new OracleParameter();
                pltext.ParameterName = "@PLAIN_TEXT";
                pltext.OracleDbType = OracleDbType.Varchar2;
                pltext.Value = plainText;
                pltext.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(pltext);

                OracleParameter key = new OracleParameter();
                key.ParameterName = "@PRIVATE_KEY";
                key.OracleDbType = OracleDbType.Varchar2;
                key.Value = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCGGhrI98Or/y0z8o+8DyWJ3VQF9+Vd+/2w1pf/ucHv9/sJMLOMMv/3WgsoLqUo2FcHUVLV8pW9DTzrwETJ6wCKRugkkzBOuPampEMRSYyAkZWEqrdgiWZT/LcrKAfjuiSvDXy6IqCLOImc3yKbG3Zn9+OkRsuFoPOfcb5qzuoPEQIDAQAB";
                key.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(key);

                cmd.ExecuteNonQuery();

                if (resultParam.Value != DBNull.Value)
                {
                    OracleString ret = (OracleString)resultParam.Value;
                    return ret.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().ToString());
            }
            return null;
        }


        public static string Decrypt_RSA(string plainText)
        {
            try
            {
                string Function = "CRYPTO.RSA_DECRYPT";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = Database.conn;
                cmd.CommandText = Function;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter resultParam = new OracleParameter();
                resultParam.ParameterName = "Result";
                resultParam.OracleDbType = OracleDbType.Varchar2;
                resultParam.Size = 10000;
                resultParam.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(resultParam);

                OracleParameter pltext = new OracleParameter();
                pltext.ParameterName = "@ENCRYPTED_TEXT";
                pltext.OracleDbType = OracleDbType.Varchar2;
                pltext.Value = plainText;
                pltext.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(pltext);

                OracleParameter key = new OracleParameter();
                key.ParameterName = "@PUBLIC_KEY";
                key.OracleDbType = OracleDbType.Varchar2;
                key.Value = "MIICdgIBADANBgkqhkiG9w0BAQEFAASCAmAwggJcAgEAAoGBAIYaGsj3w6v/LTPyj7wPJYndVAX35V37/bDWl/+5we/3+wkws4wy//daCygupSjYVwdRUtXylb0NPOvARMnrAIpG6CSTME649qakQxFJjICRlYSqt2CJZlP8tysoB+O6JK8NfLoioIs4iZzfIpsbdmf346RGy4Wg859xvmrO6g8RAgMBAAECgYAGDofHK+maixvvjLUROV3orCZvXpLte8QpiIe09R7dR8X+1ERHwMXu1hJK7lBnV94WZoXnQ92ffEmUHEr/E64if/QorHsCjko9imZgVUJS7+zkhmK3+mRfsdW/N6ndIjCTkI13caABR4TXmTnlKdyMfkoTlYCdybMAvRl2IhZPAQJBALwr0tU913PTYFR26i5XgTef4fWuY1EtQfUa5XxYUpEmocp/2lMZx/mVzHzavTPwdAVgT6cbXmUlxLL9zaKAPcUCQQC2cNlMKIRNt7M6s7M+9KJxf6EKNAcn7tuGcqrpK7FUFnZU40ZKCfiQEPMlQ9rP2dBqVYDw99NQfhN4rlheiYzdAkEAnD2HAagnjPSlt3xFVdUyZY1LgUMbE/wQGAQNKAHuDLeW/xzJmtZ9RK8s6z50evvcWdpuSMJgzntdp4E1jQgOQQJAb+ZVkZ3EUHrdBqNTzMh1nlHe74gr33Vk1lkctTmkYWQJnlVsNltZRtvulqvA2P3LFH1vQd1vkg5SWRHuh8WsTQJANauq4uWOV2zDIMDetoYpTJtUJHluW+QfTOahbPA4KaYYbuAmgq+Ja0J2TQ1rA8s4YuKYbk7/Q+32Kpd0OKI3ww==";
                key.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(key);

                cmd.ExecuteNonQuery();

                if (resultParam.Value != DBNull.Value)
                {
                    OracleString ret = (OracleString)resultParam.Value;
                    return ret.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().ToString());
            }
            return null;
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
