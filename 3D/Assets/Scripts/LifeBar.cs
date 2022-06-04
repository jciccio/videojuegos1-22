using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
  
    [SerializeField] Slider lifeSlider;

    [SerializeField] HealthScriptableObject healthSC;

    public void OnEnable(){
        healthSC.healthChangeEvent.AddListener(SetLifeBar);
    }

    public void OnDisable(){
        healthSC.healthChangeEvent.RemoveListener(SetLifeBar);
    }

    // Start is called before the first frame update
    void Start()
    {
        lifeSlider = GetComponent<Slider>();    
    }

    public void SetLifeBar(float points, float maxPoints){
        lifeSlider.value = points/maxPoints;
    }

    public void SetLifeBar(float pts){
        lifeSlider.value = pts;
    }



}
