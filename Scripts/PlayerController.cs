using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


Vector2 movement;
public Rigidbody2D rigidbody;
public GameObject thornPrefab;

private float lastFire;
private float shootVertical;
private float shootHorizontal;



    void Start() {

    
    }

    
    private void Update() {
        getAxis();
        shoot();
    }

    private void FixedUpdate() {
        move();
        
    }

    private void getAxis(){
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        shootHorizontal = Input.GetAxis("ShootHorizontal");
        shootVertical = Input.GetAxis("ShootVertical");

    }


    private void move(){
        rigidbody.MovePosition(
            rigidbody.position 
            + movement 
            * Constants.playerMoveSpeed 
            * Time.fixedDeltaTime
        );
    }

 

    private void createThorn(float x, float y){
        GameObject thorn = Instantiate(
            thornPrefab, 
            transform.position,
            transform.rotation
        ) as GameObject;
        
        thorn.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (
                (x < 0)
                ? Mathf.Floor(x) * Constants.thornForce 
                : Mathf.Ceil(x) * Constants.thornForce
            ), (    
                (y < 0) 
                ? Mathf.Floor(y) * Constants.thornForce 
                : Mathf.Ceil(y) * Constants.thornForce
            ),
                0
        );

    }

    private void shoot(){
        if(
            ((shootHorizontal !=0) 
            || (shootVertical != 0))
            && (Time.time > lastFire + Constants.fireDelay)
        ){
            createThorn(shootHorizontal, shootVertical);
            lastFire = Time.time;

        }
    }
}


