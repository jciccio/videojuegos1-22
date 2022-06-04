using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] HealthScriptableObject HealthSC;

    public void AddLife(float pts){
        HealthSC.SetHealth(pts);
    }

}
