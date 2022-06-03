using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{

    // Translate -> No usa físicas (no funciona con físicas de Unity).
    // AddForce
    // Velocity
    // MovePosition

   
    public Rigidbody rigidbody;


    [SerializeField] private float Speed = 5f;
    private int maxHealth = 100;
    private int currentHealth = 70;
    [SerializeField] private Slider healthUI;

    [SerializeField] private PlayerScriptableObject playerScriptableObject;
 
    void Awake(){
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount){
        playerScriptableObject.health -= amount;
        healthUI.value =((float)currentHealth /100);
    }

    void Start(){
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void FixedUpdate(){
        Move(Vector3.left * Speed * Time.fixedDeltaTime);
    }

    void Move(Vector3 direction){
      //  rigidbody.AddForce(direction);
      // rigidbody.velocity = direction;
      rigidbody.MovePosition(direction + transform.position);
    } 
}
