using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private GameObject _ball;
    private Transform _ballTransform;
    // Start is called before the first frame update
    [SerializeField] Pad Paddle;

    private Rigidbody2D _ballRigidbody;

    private bool _playing = false;
    private Vector2 _paddleToBallVector;

    [SerializeField] private float xVelocity;
    [SerializeField] private float yVelocity;

    [SerializeField] private Vector2 direction;

    [SerializeField] private float xMultiplier;

    float collisionFloat = 0.47f;

    void Start()
    {
        _ball = this.gameObject;
        _ballTransform = this.gameObject.transform;
        _paddleToBallVector = transform.position - Paddle.transform.position;
        _ballRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LockToPaddle();
        LaunchOnClick();
    }

    void LockToPaddle(){
        if(!_playing){
            Vector2 paddleRef = Paddle.transform.position;
            Vector2 paddlePos = new Vector2(paddleRef.x, paddleRef.y);
            transform.position = paddlePos + _paddleToBallVector;
        }
    }

    void LaunchOnClick(){
        if(!_playing && Input.GetMouseButtonDown(0)){
            _playing = true;
            _ballRigidbody.velocity = new Vector2(xVelocity, yVelocity);
        }
    }

    void OnPlayerLost(){
        //GameManager.instance.Lives = GameManager.instance.Lives - 1; 
        GameManager.instance.UpdateLives(GameManager.instance.Lives - 1);
        _playing = false;
    }

    void OnCollisionEnter2D(Collision2D other){
        string collisionTag = other.gameObject.tag;
        
        //Debug.Log(other.gameObject.name + " xCollisionPoint " + xCollisionPoint);
        if(collisionTag == Constants.HORIZONTAL_WALL){
            OnHorizontalCollision();            
        }
        if(collisionTag == Constants.VERTICAL_WALL){
            OnVerticalCollision();
        }
        if(collisionTag == Constants.PADDLE){
            OnPaddleCollision(other);
        }
        if(collisionTag == Constants.BLOCK){
            OnBlockCollision(other);
        }
    }

    void OnBlockCollision(Collision2D block){
        Vector2 collision = block.contacts[0].point;
        float xColPoint = collision.x - block.transform.position.x;
        float yColPoint = collision.y - block.transform.position.y;
        // Debug.Log("Block collision X: "+ xColPoint + "Block collision Y: " + yColPoint);
        if(Mathf.Abs(yColPoint) > collisionFloat){
            yVelocity *= -1;
            _ballRigidbody.velocity = new Vector2(xVelocity, yVelocity);
        }
        else if (Mathf.Abs(xColPoint) > collisionFloat){
            xVelocity *= -1;
            _ballRigidbody.velocity = new Vector2(xVelocity, yVelocity);
        }
        AudioManager.instance.PlaySfx(Constants.BOX_BREAK_SFX);
    }

    void OnHorizontalCollision(){
        yVelocity *= -1;
        _ballRigidbody.velocity = new Vector2(xVelocity, yVelocity);
        direction = _ballRigidbody.velocity ;
    } 

    void OnVerticalCollision(){
        xVelocity *= -1;
        _ballRigidbody.velocity = new Vector2(xVelocity, yVelocity);
        direction = _ballRigidbody.velocity ;
    }

    void OnPaddleCollision(Collision2D other){
        float xCollisionPoint = other.contacts[0].point.x - other.transform.position.x;
        yVelocity *= -1;
        xVelocity = xCollisionPoint * xMultiplier;
        _ballRigidbody.velocity = new Vector2(xVelocity, yVelocity);
        direction = _ballRigidbody.velocity ;
    }

    void OnTriggerEnter2D(Collider2D collider){
        Debug.Log(collider.tag);
        if(collider.tag == Constants.LOST){
            OnPlayerLost();
        }   
    }
}
