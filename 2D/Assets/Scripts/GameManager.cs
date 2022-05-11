using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Text LivesText;
    [SerializeField] public Transform CratesContainer;
    public static GameManager instance = null;

    public int Lives {set; get;}

    void Awake(){
        if(instance == null){
            instance = this;
        }
        else if (instance != this){
            Destroy(gameObject);
        }
    }

    void Start(){
        Lives = 3;
        LivesText.text = "Lives: "+ Lives;
        AudioManager.instance.PlayMusic(Constants.GAME_MUSIC);
    }

    public void UpdateLives(int lives){
        LivesText.text = "Lives: "+ lives;
        Lives = lives;
    }

    void Update(){
        if(CratesContainer.childCount == 0){
            Debug.Log("Ganó :)");
        }
        if(Lives == 0){
            Debug.Log("Perdió :(");
        }
    }


}
