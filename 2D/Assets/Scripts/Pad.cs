using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour
{

    private float horizontalXUnits = 17.77f; // 32bits
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 1. Obtener el punto en la pantalla.
//        Debug.Log("Posicion X: " + Input.mousePosition.x);
        // 2. Necesitamos saber la posicion relativa en la pantalla
        // Hay distintas resoluciones
//        Debug.Log("Posicion relativa: " + Input.mousePosition.x / Screen.width);
        // 3. 
        // Nuestro valor en X es: 17.77
        // Las paredes suman 1 Ud.
//        Debug.Log("Posicion relativa: " + Input.mousePosition.x / Screen.width * horizontalXUnits);
        float normalizedPosition =  Input.mousePosition.x / Screen.width * horizontalXUnits;
        transform.position = new Vector2(normalizedPosition,transform.position.y);

        
        
    }
}
