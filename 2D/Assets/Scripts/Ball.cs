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
        if(Input.GetMouseButtonDown(0)){
            _playing = true;
            _ballRigidbody.velocity = new Vector2(xVelocity, yVelocity);
        }
    }

    void OnPlayerLost(){
        _playing = false;
    }

    void OnCollisionEnter2D(Collision2D collision){
        Debug.Log(collision.gameObject.name);
    }

    void OnTriggerEnter2D(Collider2D collider){
        Debug.Log(collider.tag);
        if(collider.tag == Constants.LOST){
            OnPlayerLost();
        }   
    }
}
