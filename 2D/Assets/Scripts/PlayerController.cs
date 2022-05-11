using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] float forceX = 1000f;

    private Rigidbody2D physics;
    public bool canJump = true;
    // Start is called before the first frame update
    void Start()
    {
        physics = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D)){
            physics.AddForce(new Vector2(forceX * Time.deltaTime, 0));
        }
        if(Input.GetKey(KeyCode.A)){
            physics.AddForce(new Vector2(-1* forceX * Time.deltaTime, 0));
        }
        
    }

    public void Jump(InputAction.CallbackContext context){
        Debug.Log("Jump: " + context);
        if(context.performed){
            physics.AddForce((Vector2.up * 5f), ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 2f);
    }
}
