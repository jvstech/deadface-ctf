using System;
using System.Security.Cryptography;

byte[] initBytes = new byte[16]
  {
    174, 225, 238, 82, 98, 117, 124, 246, 123, 97,
    159, 246, 62, 150, 114, 182
  };

byte[] buffer = new byte[48]
{
  67, 108, 129, 219, 96, 208, 229, 183, 223, 102,
  171, 73, 158, 175, 125, 163, 145, 51, 214, 9,
  99, 17, 82, 140, 243, 82, 58, 242, 5, 217,
  224, 96, 179, 169, 152, 30, 13, 217, 28, 30,
  158, 82, 197, 175, 15, 198, 219, 137
};

using Aes aes = Aes.Create();
aes.Key = initBytes;
aes.IV = initBytes;

ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);

using MemoryStream stream = new(buffer);
using CryptoStream cryptoStream = new(stream, transform, CryptoStreamMode.Read);
using StreamReader reader = new(cryptoStream);
Console.WriteLine(reader.ReadToEnd());

