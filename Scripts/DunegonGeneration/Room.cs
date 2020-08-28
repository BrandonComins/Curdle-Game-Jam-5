using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour{

    public int x;
    public int y;

    void Start(){
        if (RoomController.instance == null){
            Debug.Log("you pressed play in the long scene");
            return;
        }
        RoomController.instance.registerRoom(this);
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(
            transform.position,
            new Vector3(Constants.roomWidth, Constants.roomHeight,0)
        );
    }
    public Vector3 getRoomCenter(){
        return new Vector3(x * Constants.roomWidth, y * Constants.roomHeight);
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "Player"){
            RoomController.instance.onPlayerEnterRoom(this);
        }
    }
}


