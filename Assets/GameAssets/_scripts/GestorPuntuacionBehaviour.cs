using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
    
using UnityEngine;

public class GestorPuntuacionBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScore()
    {
        Debug.Log("Loading score");
        if (File.Exists(Application.persistentDataPath + "/playerdata.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerdata.dat", FileMode.Open);
            GameData data = (GameData)bf.Deserialize(file);
            Debug.Log(data.r2d2);
            Debug.Log(data.uruk);
        }
    }
    public void SaveScore()
    {
        Debug.Log("Saving score");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath +
        "/playerdata.dat", FileMode.Create);
        GameData gameData = new GameData();
        bf.Serialize(file, gameData);
        file.Close();
    }
}

[Serializable]
public class GameData
{
    public int r2d2 = 0;
    public int uruk = 0;
}

