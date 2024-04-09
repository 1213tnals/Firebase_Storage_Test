using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using Firebase;
using Firebase.Storage;
using UnityEditor.SceneManagement;
using UnityEngine;

public class FirebaseStorageManager : MonoBehaviour
{
    private FirebaseStorage _storage;
    private StorageReference _storageRef;

    private void Awake()
    {
        // Firebase Storage �ʱ�ȭ
        _storage = FirebaseStorage.DefaultInstance;
        _storageRef = _storage.RootReference;

        Debug.Log("���̾�̽� �ʱ�ȭ!");
    }

    public async void UploadFile(string filePath, string fileName)
    {
        // ���ε��� ���� ��� �� �̸� ����
        var file = File.ReadAllBytes(filePath);
        var fileRef = _storageRef.Child(fileName);

        // ���� ���ε�
        await fileRef.PutBytesAsync(file);

        // ���ε� ���� �� �޽��� ���
        Debug.Log("���� ���ε� ����!");
    }

    public void Upload()
    {
        string path = ".";
        string name = "test.txt";

        UploadFile(path, name);
    }


    // �ؽ�Ʈ ���� �ٿ�ε� �޼ҵ�
    public async void DownloadTextFile(string remoteFilePath, string localFilePath)
    {
        // ���� �ٿ�ε�
        StorageReference textRef = _storageRef.Child(remoteFilePath);
        byte[] textBytes = await textRef.GetBytesAsync(10000);

        // ���ÿ� ���� ����
        File.WriteAllBytes(localFilePath, textBytes);

        Debug.Log("���� �ٿ�ε� ����!");
    }

    // �ٿ�ε� ��ư Ŭ�� �̺�Ʈ�� ����� �޼ҵ�
    public void Download()
    {
        string remoteFilePath = "test.txt"; // Firebase Storage�� �ִ� ���� ���
        //string localFilePath = Application.persistentDataPath + "/test_down.txt"; // ���ÿ� ������ ���� ���
        string localFilePath = "./test_down.txt"; // ���ÿ� ������ ���� ���

        Debug.Log(localFilePath);
        DownloadTextFile(remoteFilePath, localFilePath);
    }
}