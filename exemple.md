`string path = Environment.CurrentDirectory + @"\test.txt";
            string KeyIVpath = Environment.CurrentDirectory + @"\KeyIV.bin";
            string plainText = "This is a plain test not encrypted.";
            string keyPass = "@Th1s_i5_M7_P45sW0R6!@";
            File.WriteAllText(path, plainText);
            Console.WriteLine(File.ReadAllText(path));

            // One To Zero Bit
            Console.WriteLine("\nOne To Zero Bit Encryption : ");
            Encryption.SetEncryptor(EncryptorType.OneToZeroBit);
            Encryption.EncryptFile(path);
            Console.WriteLine(File.ReadAllText(path));
            Encryption.DecryptFile(path);
            Console.WriteLine(File.ReadAllText(path));

            // Reverse Byte
            Console.WriteLine("\nReverse Byte Encryption : ");
            Encryption.SetEncryptor(EncryptorType.ReverseByte);
            Encryption.EncryptFile(path);
            Console.WriteLine(File.ReadAllText(path));
            Encryption.DecryptFile(path);
            Console.WriteLine(File.ReadAllText(path));

            // Caesar Chipher
            Console.WriteLine("\nCaesar Chipher Encryption : ");
            Encryption.SetEncryptor(EncryptorType.CaesarChipher, keyPass);
            Encryption.EncryptFile(path);
            Console.WriteLine(File.ReadAllText(path));
            Encryption.DecryptFile(path);
            Console.WriteLine(File.ReadAllText(path));

            // AES
            Console.WriteLine("\nAES Encryption : ");
            Encryption.SetEncryptor(EncryptorType.AES, keyPass);
            Encryption.EncryptFile(path);
            Console.WriteLine(File.ReadAllText(path));
            Encryption.DecryptFile(path);
            Console.WriteLine(File.ReadAllText(path));

            // Rijndael
            Console.WriteLine("\nRijndael Encryption : ");
            Encryption.SetEncryptor(EncryptorType.Rijndael, keyPass);
            Encryption.EncryptFile(path);
            Console.WriteLine(File.ReadAllText(path));
            Encryption.DecryptFile(path);
            Console.WriteLine(File.ReadAllText(path));
            // save the current Rijndael Key and IV
            Encryption.SaveCurrentKeyIV(KeyIVpath);

            // SimplePasswordedCipher
            Console.WriteLine("\nSimple Passworded Cipher : ");
            Encryption.SetEncryptor(EncryptorType.SimplePasswordedCipher, keyPass);
            Encryption.EncryptFile(path);
            Console.WriteLine(File.ReadAllText(path));
            Encryption.DecryptFile(path);
            Console.WriteLine(File.ReadAllText(path));

            // CustomSBC
            Console.WriteLine("\nCustom SBC : ");
            Encryption.SetEncryptor(EncryptorType.CustomSBC, keyPass);
            Encryption.EncryptFile(path);
            Console.WriteLine(File.ReadAllText(path));
            Encryption.DecryptFile(path);
            Console.WriteLine(File.ReadAllText(path));

            // XOR
            Console.WriteLine("\nXOR : ");
            Encryption.SetEncryptor(EncryptorType.XOR, keyPass);
            Encryption.EncryptFile(path);
            Console.WriteLine(File.ReadAllText(path));
            Encryption.DecryptFile(path);
            Console.WriteLine(File.ReadAllText(path));

            Encryption.Encrypt("test", EncodingType.UTF8);

            Console.ReadKey();`