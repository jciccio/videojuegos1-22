using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sound{
    [Header("General")]
    public string Name;
    public AudioClip Clip;
    public AudioSource Source;

    [Header("Properties")]
    [Range(0f, 1f)] public float Volume = 0.5f;

    [Range(0f, 2f)] public float Pitch = 1f;
    public bool Loop = false;

    public void SetSource(AudioSource _source){
        Source = _source;
        Source.clip = Clip;
    }

    public void Play(){
        Source.pitch = Pitch;
        Source.volume = Volume;
        Source.loop = Loop;
        Source.Play();
    }

}

public class AudioManager : MonoBehaviour
{
   public static AudioManager instance;

   [SerializeField] private Sound [] Sfx;
   [SerializeField] private Sound [] Music;

   void Awake(){
        if(instance == null){
            instance = this;
            GameObject.DontDestroyOnLoad(this);
        }
        else if(instance != this){
            Destroy(gameObject);
        }

        InitAudioArrays(Sfx, "SFX");
        InitAudioArrays(Music, "Music");

   }

   void InitAudioArrays(Sound [] array, string prefix){
        for(int i = 0; i < array.Length ; i++){
            GameObject _audio = new GameObject(prefix +"_" + i + "_" + array[i].Name);
            _audio.transform.parent = transform;
            array[i].SetSource(_audio.AddComponent<AudioSource>());
        }
   }

    public void PlaySfx(string name){
        Sound audio = SearchSound(name, Sfx);
        if(audio != null){
            audio.Play();
            
        }
    }

   public void PlayMusic(string name){
        Sound audio = SearchSound(name, Music);
        if(audio != null){
            audio.Play();
        }
   }

   private Sound SearchSound(string name, Sound[] audios){
        foreach(Sound audio in audios){
            if(audio.Name == name){
                return audio;
            }
        }
        Debug.LogError("AudioManager: audio "+ name + " was not found");
        return null;
   }


}
