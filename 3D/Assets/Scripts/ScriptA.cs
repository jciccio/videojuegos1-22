using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptA : MonoBehaviour
{
    public static ScriptA Instance {get; private set;}
    public void Awake(){
        Instance = this;
    }

    public void Test(){
        Debug.Log("Script A Test");
    }



}
