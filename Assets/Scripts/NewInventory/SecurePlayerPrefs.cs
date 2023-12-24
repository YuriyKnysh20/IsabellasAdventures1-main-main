using System;
using System.Text;
using UnityEngine;

public class SecurePlayerPrefs
{
    private const string encryptionKey = "2023/dskfdklfgkldfklgn99799d7g7@&#&)7";

    public static void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, Encrypt(value));
    }

    public static string GetString(string key)
    {
        string encryptedValue = PlayerPrefs.GetString(key);
        return Decrypt(encryptedValue);
    }

    private static string Encrypt(string value)
    {
        byte[] valueBytes = Encoding.UTF8.GetBytes(value);
        byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);

        for (int i = 0; i < valueBytes.Length; i++)
        {
            valueBytes[i] = (byte)(valueBytes[i] ^ keyBytes[i % keyBytes.Length]);
        }

        return Convert.ToBase64String(valueBytes);
    }

    private static string Decrypt(string encryptedValue)
    {
        byte[] valueBytes = Convert.FromBase64String(encryptedValue);
        byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);

        for (int i = 0; i < valueBytes.Length; i++)
        {
            valueBytes[i] = (byte)(valueBytes[i] ^ keyBytes[i % keyBytes.Length]);
        }

        return Encoding.UTF8.GetString(valueBytes);
    }
}
