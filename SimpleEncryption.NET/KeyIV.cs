using System;

namespace SimpleEncryption.NET
{
    [Serializable]
    public class KeyIV
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
    }
}
