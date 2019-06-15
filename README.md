# KeksEncryption

KeksEncryption is a simple C# solution for File, byte array and text Encryption/Decryption
A lot of Cipher are avaliable from AES to Custom encryption protocols.
.NEt Freamwork 3.5, compatible unity 5 and .NET Core

## Getting Started

Just include the .DLL to your project.

### Globale Usage

start by adding the following line : 
```
using KeksEncryptor;
```

The Main entry point for any usage is the static 'Encryption' class.
Before encrypt or decrypt anything, first choose a Cipher by typing
```
Encryption.SetEncryptor([EncryptorType], [optional : password]);
```
You can now Encrypt and Decrypt file, byte array or plaintext

### Avaliable Encryptors

 - ReverseByte (no pass needed)
 - OneToZeroBit (no pass needed)
 - AES
 - CaesarChipher
 - Rijndael
 - SimplePasswordedCipher
 - CustomSBC
 - XOR

### Encrypt / Decrypt Files
```
    Encryption.SetEncryptor(EncryptorType.AES, "password");
    Encryption.EncryptFile(path);
    Encryption.DecryptFile(path);
```

### Encrypt / Decrypt byte[]
```
    Encryption.SetEncryptor(EncryptorType.OneToZeroBit);
    byte[] encrypted = Encryption.EncryptFile(new byte[]{ 0,1,2,3,4,5,6,7,8,9 });
    byte[] decrypted = Encryption.DecryptFile(byte[]);
```

### Encrypt / Decrypt text (string)
```
    Encryption.SetEncryptor(EncryptorType.SimplePasswordedCipher, "password");
    string encryptedText = Encryption.Encrypt("test", EncodingType.UTF8);
    string decryptedText = Encryption.Encrypt(encryptedText, EncodingType.UTF8);
```

### interSoftware Key and IV Sharing
When using AES/Rijndael Ciphers, you usualy can Decrypt only in the software that encrypt.
KeksEncryption Allow you ro generate  encypted binary (.bin) files that you can use to keep the Key and IV from a software to another.
For exemple, AES/Rijndael encrypt a file in a C# console, and decrypt the same file in a Unity scipt.

Encryption's side :
```
    Encryption.SetEncryptor(EncryptorType.Rijndael, "password");
    // save the current Rijndael Key and IV
    Encryption.SaveCurrentKeyIV(path);
```

Decryption's side :
```
    Encryption.SetEncryptor(EncryptorType.Rijndael, "password"); // new Key and IV are now generated
    // Get Rijndael Key and IV
    Encryption.SetEncryptorKeyFromFile(path); // Key and IV are now the same that for the Encryption
```

Kévin Bouetard