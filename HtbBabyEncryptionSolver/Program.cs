using System;
using System.IO;
using System.Text;
using System.Globalization;

class Program
{
    static void Main()
    {
        Console.WriteLine("Let's solve https://app.hackthebox.com/challenges/BabyEncryption!");

        try
        {
            // Read the hex string from the file
            string hexString = File.ReadAllText("msg.enc");

            // Convert the hex string to a byte array
            byte[] encryptedMessage = HexStringToByteArray(hexString);

            var decryptedMessage = new StringBuilder(encryptedMessage.Length);
            foreach (byte encryptedChar in encryptedMessage)
            {
                var decryptedChar = BruteForceDecrypt(encryptedChar);
                if (decryptedChar.HasValue)
                    decryptedMessage.Append(decryptedChar);
            }

            Console.WriteLine(decryptedMessage.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static char? BruteForceDecrypt(byte encryptedValue)
    {
        for (int brute = 33; brute < 126; brute++)
        {
            if ((123 * brute + 18) % 256 == encryptedValue)
            {
                return (char)brute;
            }
        }
        return null; // Return null if no match is found
    }

    static byte[] HexStringToByteArray(string hex)
    {
        if (hex.Length % 2 != 0)
            throw new ArgumentException("Hex string must have an even length");

        int length = hex.Length / 2;
        byte[] bytes = new byte[length];
        for (int i = 0; i < length; i++)
        {
            bytes[i] = byte.Parse(hex.AsSpan(i * 2, 2), NumberStyles.HexNumber);
        }
        return bytes;
    }
}
