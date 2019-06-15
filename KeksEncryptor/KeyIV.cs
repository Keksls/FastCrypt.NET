using System;

namespace KeksEncryptor
{
    [Serializable]
    public class KeyIV
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
    }
}
