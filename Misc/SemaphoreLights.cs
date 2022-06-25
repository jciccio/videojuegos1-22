using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemaphoreLights : MonoBehaviour
{
    // Start is called before the first frame update

    List<Light> _Post1Green;
    List<Light> _Post1Yellow;
    List<Light> _Post1Red;

    
    List<Light> _Post2Green;
    List<Light> _Post2Yellow;
    List<Light> _Post2Red;

    bool _ChangeLights;

    float _TimeElapsed1;
    float _TimeElapsed2;

    private int _CurrentState1 = 0;
    private int _CurrentState2 = 2;
    public float [] _SemaphoreTimes = new float[3];

    void Start()
    {
        _Post1Green = new List<Light>();
        _Post2Green = new List<Light>();
        _Post1Yellow = new List<Light>();
        _Post2Yellow = new List<Light>();
        _Post1Red = new List<Light>();
        _Post2Red = new List<Light>();

        foreach (Transform post in this.transform) {
            if(post.name == "Post1"){
                _Post1Green.Add(post.Find("Green light").GetComponent<Light>());
                _Post1Yellow.Add(post.Find("Yellow light").GetComponent<Light>());
                _Post1Red.Add(post.Find("Red light").GetComponent<Light>());
            }
            else if (post.name == "Post2"){
                _Post2Green.Add(post.Find("Green light").GetComponent<Light>());
                _Post2Yellow.Add(post.Find("Yellow light").GetComponent<Light>());
                _Post2Red.Add(post.Find("Red light").GetComponent<Light>());
            }
        }
        Debug.Log(_Post1Green);
        SetInitialState();   
    }


    void ChangeState(){
       ChangePost1State();
       ChangePost2State();
    }

    void ChangePost1State(){
        float intensity = 12;
      
        for (int i = 0 ; i < _Post1Green.Count ; i++){
            //if (_isPost2Red)
            _Post1Green[i].intensity = _CurrentState1 % 3 == 0   ? intensity : 0;
            _Post1Yellow[i].intensity = _CurrentState1 % 3 == 1  ? intensity : 0;
            _Post1Red[i].intensity = _CurrentState1 % 3 == 2 ? intensity : 0;
        }
    }

    void ChangePost2State(){
        float intensity = 12;
        
        for (int i = 0 ; i < _Post2Green.Count ; i++){
           // if (_isPost1Red)
            _Post2Green[i].intensity = (_CurrentState2) % 3 == 0  ? intensity : 0;
            _Post2Yellow[i].intensity =  (_CurrentState2) % 3 == 1   ? intensity : 0;
            _Post2Red[i].intensity = (_CurrentState2) % 3 == 2 ? intensity : 0;

        }
    }

    void SetInitialState(){
        ChangeState();
    }

    bool _isPost1Red;
    bool _isPost2Red;
    // Update is called once per frame
    void Update()
    {
        _TimeElapsed1 += Time.deltaTime;
        _TimeElapsed2 += Time.deltaTime;
        
        if (_TimeElapsed1 > _SemaphoreTimes[_CurrentState1%3]){
            //Debug.Log (_TimeElapsed);
            //Debug.Log (_SemaphoreTimes[_CurrentState%3]);
            _TimeElapsed1 = 0f;
            _CurrentState1++;
            ChangePost1State();
        }   
        
        if (_TimeElapsed2 > _SemaphoreTimes[_CurrentState2%3]){
            //Debug.Log (_TimeElapsed);
            //Debug.Log (_SemaphoreTimes[_CurrentState%3]);
            _TimeElapsed2 = 0f;
            _CurrentState2++;
            ChangePost2State();
        }

    }
}
