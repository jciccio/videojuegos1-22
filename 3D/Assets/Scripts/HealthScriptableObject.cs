using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "HealthManagerScriptableObject", menuName = "ScriptableObjects/Health Manager")]
public class HealthScriptableObject : ScriptableObject
{
    public float Health;
    public float MaxHealth = 100f;

    [System.NonSerialized] public UnityEvent<float> healthChangeEvent;
    
    private void OnEnable()
    {
        if (healthChangeEvent == null)
        {
            healthChangeEvent = new UnityEvent<float>();
        }
    }

    public void SetHealth(float qty)
    {
        Health = Mathf.Clamp(Health + qty, 0, MaxHealth);
        healthChangeEvent?.Invoke(Health / MaxHealth);
    }

}
