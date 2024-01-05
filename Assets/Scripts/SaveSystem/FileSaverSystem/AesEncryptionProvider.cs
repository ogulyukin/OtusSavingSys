using System;
using System.Security.Cryptography;
using System.Text;

namespace SaveSystem.FileSaverSystem
{
    public class AesEncryptionProvider
    {
        private const string Key = "A60B1812FE5E7AA200BA9CFC94E4E8B0"; //set any string of 32 chars
        private const string Iv = "1234967887654111"; //set any string of 16 chars
        private readonly AesCryptoServiceProvider aesCryptoProvider;

        public AesEncryptionProvider()
        {
            aesCryptoProvider = new AesCryptoServiceProvider();
            aesCryptoProvider.BlockSize = 128;
            aesCryptoProvider.KeySize = 256;
            aesCryptoProvider.Key = Encoding.ASCII.GetBytes(Key);
            aesCryptoProvider.IV = Encoding.ASCII.GetBytes(Iv);
            aesCryptoProvider.Mode = CipherMode.CBC;
            aesCryptoProvider.Padding = PaddingMode.PKCS7;
        }
        
        public string AesEncryption(string inputData)
        {
            var txtByteData = Encoding.ASCII.GetBytes(inputData);
            var cryptoTransform = aesCryptoProvider.CreateEncryptor(aesCryptoProvider.Key, aesCryptoProvider.IV);
 
            var result = cryptoTransform.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
            return Convert.ToBase64String(result);
        }
        
        public string AesDecryption(string inputData)
        {
            var txtByteData = Convert.FromBase64String(inputData);
            var cryptoTransform = aesCryptoProvider.CreateDecryptor();
 
            var result = cryptoTransform.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
            return Encoding.ASCII.GetString(result);
        }
        
    }
}
