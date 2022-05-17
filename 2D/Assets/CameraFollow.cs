using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public GameObject FollowObject;

    [SerializeField] public Vector2 FollowOffset;
    private Vector2 Threshold;
    [SerializeField] public float Speed;
    public Vector3 OffsetPosition;

    private Rigidbody2D FollowObjectRigidbody;

    void Start(){
        
        FollowObjectRigidbody = FollowObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        // Mover la camar con respecto al jugador
        Threshold = CalculateThreshold();
        Vector2 follow = FollowObject.transform.position - OffsetPosition;

        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newPosition = transform.position;

        if(xDifference > Threshold.x){
            newPosition.x = follow.x;
        }
        if(yDifference > Threshold.y){
            newPosition.y = follow.y;
        }

        float moveSpeed = FollowObjectRigidbody.velocity.magnitude > Speed ? FollowObjectRigidbody.velocity.magnitude : Speed;
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed*Time.fixedDeltaTime);
    }

    Vector3 CalculateThreshold(){
        Rect aspect = Camera.main.pixelRect;
        //Debug.Log("Ortographic Size: " + Camera.main.orthographicSize);
        //Debug.Log("aspect.width: " + aspect.width);
        //Debug.Log("aspect.height: " + aspect.height);
        Vector2 threshold = new Vector2(Camera.main.orthographicSize * aspect.width/aspect.height, Camera.main.orthographicSize);
        threshold.x -= FollowOffset.x;
        threshold.y -= FollowOffset.y;
        return threshold;
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Vector2 border = CalculateThreshold();
        Gizmos.DrawWireCube(transform.position + OffsetPosition, new Vector3(border.x*2, border.y*2 ,1));
    }
}
