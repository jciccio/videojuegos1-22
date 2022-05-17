using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] float forceX = 1000f;
    private Rigidbody2D physics;
    public bool canJump = true;
    public Vector3 direction;
    public Animator animator;

    [Header("Physics")]
    public float jumpSpeed = 6f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;
    public float linearDrag;

    [Header("Ground")]
    public bool onGround = false;
    public bool jumping = false;
    public LayerMask groundLayer;
    public float groundLength = 0.6f;

    [Header("Read Only")]
    public float directionForce;


    // Start is called before the first frame update
    void Start()
    {
        physics = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(directionForce > 0){
            animator.SetBool("PlayerRun", true);
            direction = Vector3.zero;
        }
        else if(directionForce  < 0){
            animator.SetBool("PlayerRun", true);
            direction = new Vector3(0,180,0);
        }
        else{
            animator.SetBool("PlayerRun", false);
        }
    }

    void ModifyPhysics(){
        if(!jumping){ // Si está en el piso
            physics.gravityScale = 0;
        }
        else{
            physics.gravityScale = gravity;
            physics.drag = linearDrag * 0.15f;
        }
    }

    void FixedUpdate(){
        Quaternion targetRotation = Quaternion.Euler(direction);
        this.transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 5f);
        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundLength, groundLayer);
        jumping = !Physics2D.Raycast(transform.position, Vector2.down, groundLength, groundLayer);
        physics.AddForce(new Vector2(forceX* directionForce * Time.fixedDeltaTime, 0));
        ModifyPhysics();
    }

    public void Jump(InputAction.CallbackContext context){
        Debug.Log("Jump: " + context);
        if(context.performed){
            physics.AddForce((Vector2.up * 5f), ForceMode2D.Impulse); // (0,1)
        }
    }

    public void RunControl(InputAction.CallbackContext context){
        Debug.Log("Run Control " + context.phase + " value: " + context.ReadValue<float>());
        if(context.performed){ 
            directionForce = context.ReadValue<float>();
        }
        else if(context.canceled){
            directionForce = 0;
        }
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundLength);
    }
}
