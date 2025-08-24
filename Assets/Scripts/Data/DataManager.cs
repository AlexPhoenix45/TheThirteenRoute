using System;
using UnityEngine;
using System.Text;
using System.Security.Cryptography;

public static class DataManager
{
    // Change this to your own key (must be 32 bytes for AES-256)
    private static readonly string encryptionKey = "1g98I1TBssLaAMimPvCh9cMYdAlpd0kq"; 
    
    public static GameData gameData = new GameData();
    public static PlayerData playerData = new PlayerData();

    // ===================== PUBLIC METHODS =====================

    public static void SaveData(DataType type)
    {
        switch (type)
        {
            case DataType.GAMEDATA:
            {
                _SaveData("GameData", gameData);
                break;
            }
            case DataType.PLAYERDATA:
            {
                _SaveData("PlayerData", playerData);
                break;
            }
            default:
                break;
        }
    }
    
    private static void _SaveData<T>(string key, T data)
    {
        // 1. Convert object -> JSON
        string json = JsonUtility.ToJson(data);

        // 2. Encrypt JSON
        string encrypted = Encrypt(json, encryptionKey);

        // 3. Save to PlayerPrefs
        PlayerPrefs.SetString(key, encrypted);
        PlayerPrefs.Save();
    }

    public static void LoadData(DataType type)
    {
        switch (type)
        {
            case DataType.GAMEDATA:
            {
                gameData = _LoadData<GameData>("GameData");
                break;
            }
            case DataType.PLAYERDATA:
            {
                playerData = _LoadData<PlayerData>("PlayerData");
                break;
            }
            default:
            {
                break;
            }
        }
    }
    
    private static T _LoadData<T>(string key) where T : new()
    {
        if (!PlayerPrefs.HasKey(key))
        {
            Debug.LogWarning("No data found for key: " + key + " → creating new one.");

            // Create new object instead of returning null/default
            T newData = new T();

            // Save it immediately so it's stored
            _SaveData(key, newData);

            return newData;
        }

        try
        {
            string encrypted = PlayerPrefs.GetString(key);
            string json = Decrypt(encrypted, encryptionKey);
            return JsonUtility.FromJson<T>(json);
        }
        catch
        {
            Debug.LogError("Failed to load data for key: " + key + " → creating new one.");
            return new T();
        }
    }


    // ===================== ENCRYPTION HELPERS =====================

    private static string Encrypt(string plainText, string key)
    {
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.GenerateIV(); // unique IV per encryption
            byte[] iv = aes.IV;

            using (var encryptor = aes.CreateEncryptor(aes.Key, iv))
            {
                byte[] cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                // Store IV + Cipher text together (base64)
                byte[] combined = new byte[iv.Length + cipherBytes.Length];
                Buffer.BlockCopy(iv, 0, combined, 0, iv.Length);
                Buffer.BlockCopy(cipherBytes, 0, combined, iv.Length, cipherBytes.Length);

                return System.Convert.ToBase64String(combined);
            }
        }
    }

    private static string Decrypt(string cipherText, string key)
    {
        byte[] combined = System.Convert.FromBase64String(cipherText);
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;

            // Extract IV (first 16 bytes)
            byte[] iv = new byte[aes.BlockSize / 8];
            byte[] cipherBytes = new byte[combined.Length - iv.Length];
            Buffer.BlockCopy(combined, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(combined, iv.Length, cipherBytes, 0, cipherBytes.Length);

            using (var decryptor = aes.CreateDecryptor(aes.Key, iv))
            {
                byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                return Encoding.UTF8.GetString(plainBytes);
            }
        }
    }
}

public enum DataType
{
    GAMEDATA,
    PLAYERDATA,
}