using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject element;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E)){
            GameObject crate = GameObject.Instantiate(element);
            crate.transform.position = new Vector3(0,0.5f,0);
        }
    }
}
