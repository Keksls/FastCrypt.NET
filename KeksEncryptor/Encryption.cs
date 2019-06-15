using KeksEncryptor.Encryptors;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace KeksEncryptor
{
    /// <summary>
    /// KeksEncryptor entry point.
    /// </summary>
    public static class Encryption
    {
        public static EncryptorType encryptorType { get; private set; }
        public static Encryptor encryptor { get; private set; }

        #region Encryptor Initalisation
        /// <summary>
        /// Set Encryptor for next operations
        /// </summary>
        /// <param name="_encryptorType">The encryptor you want to use</param>
        /// <param name="key">The plainText key you want to use for the selected encryptor</param>
        public static void SetEncryptor(EncryptorType _encryptorType, string key)
        {
            SetEncryptor(_encryptorType);
            if (encryptor != null)
                encryptor.SetKey(key);
        }

        /// <summary>
        /// Set Encryptor for next operations
        /// </summary>
        /// <param name="_encryptorType">The encryptor you want to use</param>
        /// <param name="key">The byte[] Key</param>
        /// <param name="IV">The byte[] IV</param>
        public static void SetEncryptor(EncryptorType _encryptorType, byte[] key, byte[] IV)
        {
            SetEncryptor(_encryptorType);
            if (encryptor != null)
                encryptor.SetKey(key, IV);
        }

        /// <summary>
        /// Set Encryptor for next operations
        /// </summary>
        /// <param name="_encryptorType">The encryptor you want to use</param>
        /// <param name="keyIV">The KeyIV Instance you want to use (Key + IV)</param>
        public static void SetEncryptor(EncryptorType _encryptorType, KeyIV keyIV)
        {
            SetEncryptor(_encryptorType);
            if (encryptor != null)
                encryptor.SetKey(keyIV);
        }

        /// <summary>
        /// Set Encryptor for next operations
        /// </summary>
        /// <param name="_encryptorType">The encryptor you want to use</param>
        public static void SetEncryptor(EncryptorType _encryptorType)
        {
            encryptorType = _encryptorType;
            switch (encryptorType)
            {
                case EncryptorType.ReverseByte:
                    encryptor = new ReverseByte_Encryptor();
                    break;
                case EncryptorType.OneToZeroBit:
                    encryptor = new OneToZeroBit_Encryptor();
                    break;
                case EncryptorType.AES:
                    encryptor = new AES_Encryptor();
                    break;
                case EncryptorType.CaesarChipher:
                    encryptor = new CaesarChipher_Encryptor();
                    break;
                case EncryptorType.Rijndael:
                    encryptor = new Rijndael_Encryptor();
                    break;
                case EncryptorType.SimplePasswordedCipher:
                    encryptor = new SimplePasswordedCipher_Encryptor();
                    break;
                case EncryptorType.CustomSBC:
                    encryptor = new CustomSBC_Encryptor();
                    break;
                case EncryptorType.XOR:
                    encryptor = new XOR_Encryptor();
                    break;
                default:
                    encryptor = null;
                    break;
            }
        }
        #endregion

        #region Encryption
        /// <summary>
        /// Encrypt a File. (Necesite to call 'SetEncryptor' at least once before).
        /// </summary>
        /// <param name="path">The path to the file you want to encrypt.</param>
        public static void EncryptFile(string path)
        {
            if (File.Exists(path))
                File.WriteAllBytes(path, Encrypt(File.ReadAllBytes(path)));
            else
                throw new FileNotFoundException("The file '" + path + "' does not exist.");
        }

        /// <summary>
        /// Encrypt PlainText with the selected Encryptior and Encoding
        /// </summary>
        /// <param name="plainText">the plaintext you want to encrypt</param>
        /// <param name="encoding">the encoding wich the text is formated</param>
        /// <returns>Encrypted Text</returns>
        public static string Encrypt(string plainText, EncodingType encoding = EncodingType.UTF8)
        {
            switch (encoding)
            {
                case EncodingType.Default:
                    return Encoding.Default.GetString(Encrypt(Encoding.Default.GetBytes(plainText)));
                case EncodingType.ASCII:
                    return Encoding.ASCII.GetString(Encrypt(Encoding.ASCII.GetBytes(plainText)));
                case EncodingType.UTF8:
                    return Encoding.UTF8.GetString(Encrypt(Encoding.UTF8.GetBytes(plainText)));
                case EncodingType.UTF7:
                    return Encoding.UTF7.GetString(Encrypt(Encoding.UTF7.GetBytes(plainText)));
                case EncodingType.UTF32:
                    return Encoding.UTF32.GetString(Encrypt(Encoding.UTF32.GetBytes(plainText)));
                case EncodingType.Unicode:
                    return Encoding.Unicode.GetString(Encrypt(Encoding.Unicode.GetBytes(plainText)));
                case EncodingType.BigEndianUnicode:
                    return Encoding.BigEndianUnicode.GetString(Encrypt(Encoding.BigEndianUnicode.GetBytes(plainText)));
                default:
                    return Encoding.UTF8.GetString(Encrypt(Encoding.UTF8.GetBytes(plainText)));
            }
        }

        /// <summary>
        /// Encrypt a byte[] with the selected Encryptor
        /// </summary>
        /// <param name="data">the byte[] you want to Encrypt</param>
        /// <returns>Encrypted byte[]</returns>
        public static byte[] Encrypt(byte[] data)
        {
            return encryptor.Encrypt(data);
        }

        /// <summary>
        /// Decrypt a File. (Necesite to call 'SetEncryptor' at least once before).
        /// </summary>
        /// <param name="path">the path to the file you want to decrypt.</param>
        public static void DecryptFile(string path)
        {
            if (File.Exists(path))
                File.WriteAllBytes(path, Decrypt(File.ReadAllBytes(path)));
            else
                throw new FileNotFoundException("The file '" + path + "' does not exist.");
        }

        /// <summary>
        /// Decrypt PlainText with the selected Encryptor and Encoding
        /// </summary>
        /// <param name="plainText">the plaintext you want to decrypt</param>
        /// <param name="encoding">the encoding wich the text was formated before being encrypted</param>
        /// <returns>Decrypted plainText</returns>
        public static string Decrypt(string plainText, EncodingType encoding = EncodingType.UTF8)
        {
            switch (encoding)
            {
                case EncodingType.Default:
                    return Encoding.Default.GetString(Decrypt(Encoding.Default.GetBytes(plainText)));
                case EncodingType.ASCII:
                    return Encoding.ASCII.GetString(Decrypt(Encoding.ASCII.GetBytes(plainText)));
                case EncodingType.UTF8:
                    return Encoding.UTF8.GetString(Decrypt(Encoding.UTF8.GetBytes(plainText)));
                case EncodingType.UTF7:
                    return Encoding.UTF7.GetString(Decrypt(Encoding.UTF7.GetBytes(plainText)));
                case EncodingType.UTF32:
                    return Encoding.UTF32.GetString(Decrypt(Encoding.UTF32.GetBytes(plainText)));
                case EncodingType.Unicode:
                    return Encoding.Unicode.GetString(Decrypt(Encoding.Unicode.GetBytes(plainText)));
                case EncodingType.BigEndianUnicode:
                    return Encoding.BigEndianUnicode.GetString(Decrypt(Encoding.BigEndianUnicode.GetBytes(plainText)));
                default:
                    return Encoding.UTF8.GetString(Decrypt(Encoding.UTF8.GetBytes(plainText)));
            }
        }

        /// <summary>
        /// Decrypt a byte[] with the selected Encryptor
        /// </summary>
        /// <param name="data">the byte[] you want to Decrypt</param>
        /// <returns>Decrypted byte[]</returns>
        public static byte[] Decrypt(byte[] data)
        {
            return encryptor.Decrypt(data);
        }
        #endregion

        #region Key and IV
        /// <summary>
        /// Save a binary file that contain the KeyIV data currently used by the selected Encryptor
        /// </summary>
        /// <param name="path">the path the file you want to save</param>
        public static void SaveCurrentKeyIV(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
            using (MemoryStream stream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, encryptor.KeyIV);
                File.WriteAllBytes(path, new OneToZeroBit_Encryptor().Encrypt(stream.ToArray()));
                stream.Close();
            }
        }

        /// <summary>
        /// Get a saved KeyIV instance from a binaryFile (must be created with SaveCurrentKeyIV)
        /// </summary>
        /// <param name="path">the path of the binary file</param>
        /// <returns>The KeyIV INstance from the file</returns>
        public static KeyIV GetKeyFromFile(string path)
        {
            using (MemoryStream stream = new MemoryStream(new OneToZeroBit_Encryptor().Decrypt(File.ReadAllBytes(path))))
                return (KeyIV)new BinaryFormatter().Deserialize(stream);
        }

        /// <summary>
        /// Set KeyIV Instance from a binaryFile (must be created with SaveCurrentKeyIV) to the current Encryptor
        /// </summary>
        /// <param name="path">the path of the binary file</param>
        public static void SetEncryptorKeyFromFile(string path)
        {
            encryptor.SetKey(GetKeyFromFile(path));
        }
        #endregion
    }

    public enum EncryptorType
    {
        ReverseByte = 0,
        OneToZeroBit = 1,
        AES = 2,
        CaesarChipher = 3,
        Rijndael = 4,
        SimplePasswordedCipher = 5,
        CustomSBC = 6,
        XOR = 7
    }

    public enum EncodingType
    {
        Default = -1,
        ASCII = 0,
        UTF8 = 1,
        UTF7 = 2,
        UTF32 = 3,
        Unicode = 4,
        BigEndianUnicode = 5,
    }
}