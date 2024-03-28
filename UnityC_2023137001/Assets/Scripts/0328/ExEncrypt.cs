using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Text;
using System;
using System.Linq;

public class ExEncrypt : MonoBehaviour
{
    string filePath;
    string key = "ThisIsASecretKey";    //��ȣȭŰ

    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.persistentDataPath + "/EncryptPlayerData.json";
        Debug.Log(filePath);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerData playerData = new PlayerData();
            playerData.playerName = "�÷��̾� 1";
            playerData.playerLevel = 1;
            playerData.items.Add("��1");
            playerData.items.Add("����1");
            SaveData(playerData);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerData playerData = new PlayerData();

            playerData = LoadData();

            Debug.Log(playerData.playerName);
            Debug.Log(playerData.playerLevel);
            for (int i = 0; i < playerData.items.Count; i++)
            {
                Debug.Log(playerData.items[i]);
            }
        }
    }
    void SaveData(PlayerData data)
    {
        //JSON ����ȭ
        string jsonData = JsonConvert.SerializeObject(data);

        //�����͸� ����Ʈ �迭�� ��ȯ
        byte[] bytesToEncfrypt = Encoding.UTF8.GetBytes(jsonData);

        //��ȣȭ
        byte[] encryptedBytes = Encrypt(bytesToEncfrypt);
        //��ȣȭ�� �����͸� base64 ���ڿ��� ��ȯ
        string encrytedData = Convert.ToBase64String(encryptedBytes);
        //���� ����
        File.WriteAllText(filePath, jsonData);
    }
    PlayerData LoadData()
    {
        if (File.Exists(filePath))
        {
            //���Ͽ��� ������ �б�
            //string jsonData = File.ReadAllText(filePath);

            //���Ͽ��� ��ȣȭ�� ������ �б�
            string encryptedData = File.ReadAllText(filePath);

            //base64 ���ڿ��� ����Ʈ �迭�� ��ȯ
            byte[] encryptedBytes = Convert.FromBase64String(encryptedData);

            //��ȣȭ
            byte[] decryptedBytes = Decrypt(encryptedBytes);

            //����Ʈ �迭�� ���ڿ��� ��ȯ
            string jsonData = Encoding.UTF8.GetString(decryptedBytes);

            //JSON ������ȭ
            PlayerData data = JsonConvert.DeserializeObject<PlayerData>(jsonData);
            return data;


        }
        else
        {
            return null;
        }
    }
    byte[] Encrypt(byte[] plainBytes)
    {
        using (Aes aseAlg = Aes.Create())
        {
            aseAlg.Key = Encoding.UTF8.GetBytes(key);
            aseAlg.IV = new byte[16];

            //��ȣȭ ��ȯ�� ����
            ICryptoTransform encryptor = aseAlg.CreateEncryptor(aseAlg.Key, aseAlg.IV);

            //��Ʈ�� ����
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                //��Ʈ���� ��ȣȭ ��ȯ�⸦ �����Ͽ� ��ȣȭ ��Ʈ���� ����
                using (CryptoStream csEncrypt=new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    //��ȣȭ ��Ʈ���� ������ ����
                    csEncrypt.Write(plainBytes, 0, plainBytes.Length);
                    csEncrypt.FlushFinalBlock();

                    //��ȣȭ�� ������ ����Ʈ�� �迭�� ��ȯ
                    return msEncrypt.ToArray();
                }
            }
        }
    }

    byte[] Decrypt(byte[] encryptedBytes)

    {
        using (Aes aseAlg = Aes.Create())
        {
            aseAlg.Key = Encoding.UTF8.GetBytes(key);
            aseAlg.IV = new byte[16];

            //��ȣȭ ��ȯ�� ����
            ICryptoTransform decryptor = aseAlg.CreateDecryptor(aseAlg.Key, aseAlg.IV);

            //��Ʈ�� ����
            using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
            {
                //��Ʈ���� ��ȣȭ ��ȯ�⸦ �����Ͽ� ��ȣȭ ��Ʈ�� ����
                using(CryptoStream csDecrypt=new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    //��ȣȭ�� �����͸� ���� ����Ʈ �迭 ����
                    byte[] decryptedBtyes = new byte[encryptedBytes.Length];

                    //��ȣȭ ��Ʈ������ �����͸� �б�
                    int descryptedByteCount = csDecrypt.Read(decryptedBtyes, 0, decryptedBtyes.Length);

                    //������ ���� ũ�⸸ŭ�� ����Ʈ �迭�� ��ȯ
                    return decryptedBtyes.Take(descryptedByteCount).ToArray();
                }
            }
        }
    }

    }
