using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsTest : MonoBehaviour
{
    void Awake(){
        PlayerPrefs.SetInt("Level", 2);
        Debug.Log(PlayerPrefs.GetInt("Level"));


        Debug.Log("Level: " + PlayerPrefs.HasKey("Level"));
        Debug.Log("level: " + PlayerPrefs.HasKey("level"));

        SaveSettingsPrefs settings = new SaveSettingsPrefs(){
            width = 1024,
            height = 768,
            volume = 10,
            difficulty = "hard",
            playerPosition = new Vector3(1,2,3)
        };
        
        PlayerPrefs.SetString("Settings" , JsonUtility.ToJson(settings));
        PlayerPrefs.Save();


        SaveSettingsPrefs settingsObj = JsonUtility.FromJson<SaveSettingsPrefs>(PlayerPrefs.GetString("Settings"));
        Debug.Log(settingsObj.difficulty);
    }

    private void SetBool(string key, bool value){
        PlayerPrefs.SetInt(key, value ? 1 : 0 );
    }

    private bool GetBoole(string key){
        return PlayerPrefs.GetInt(key) == 1;
    }

    [System.Serializable]
    public class SaveSettingsPrefs{
        public int width;
        public int height;
        public int volume;
        public string difficulty;
        public Vector3 playerPosition;
    }
}
