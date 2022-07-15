using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptB : MonoBehaviour
{
    void Awake(){
        ScriptA.Instance.Test();
    }
}
