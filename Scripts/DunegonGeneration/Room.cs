using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour{

    public int x;
    public int y;

    private bool updatedDoors = false;

    public Room(int x, int y){
        x = x;
        y = y;
    }

    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;
    public List<Door> doors = new List<Door>();

    void Start(){
        if (RoomController.instance == null){
            Debug.Log("you pressed play in the long scene");
            return;
        }
        Door[] ds = GetComponentsInChildren<Door>();
        foreach(Door d in ds){
            doors.Add(d);
            switch(d.doorType){
                case Door.DoorType.right:
                    rightDoor = d;
                break;
                
                case Door.DoorType.left:
                    leftDoor = d;
                break;
                
                case Door.DoorType.top:
                    topDoor = d;
                break;
                
                case Door.DoorType.bottom:
                    bottomDoor = d;
                break;
            }
        }

        RoomController.instance.registerRoom(this);
    }

    void Update(){
        if(name.Contains("End") && !updatedDoors){
            removeUnconnectedDoors();
            updatedDoors = true;
        }
    }

    public void removeUnconnectedDoors(){
        foreach(Door door in doors){
            switch(door.doorType){
                case Door.DoorType.right:
                    if(getRight() == null){
                        door.gameObject.SetActive(false);
                    }
                break;
                
                case Door.DoorType.left:
                    if(getLeft() == null){
                        door.gameObject.SetActive(false);
                    }
                break;
                
                case Door.DoorType.top:
                    if(getTop() == null){
                        door.gameObject.SetActive(false);
                    }
                break;
                
                case Door.DoorType.bottom:
                    if(getBottom() == null){
                        door.gameObject.SetActive(false);
                    }
                break;
            }
        }
    }

    public Room getRight(){
        if(RoomController.instance.doesRoomExist(x - 1, y)){
            return RoomController.instance.findRoom(x - 1, y);
        }else{
            return null;
        }     
    }

    public Room getLeft(){
        if(RoomController.instance.doesRoomExist(x + 1, y)){
            return RoomController.instance.findRoom(x + 1, y);
        }else{
            return null;
        } 
    }

    public Room getTop(){
        if(RoomController.instance.doesRoomExist(x, y + 1)){
            return RoomController.instance.findRoom(x, y + 1);
        }else{
            return null;
        }         
    }

    public Room getBottom(){
        if(RoomController.instance.doesRoomExist(x, y - 1)){
            return RoomController.instance.findRoom(x, y - 1);
        }else{
            return null;
        } 
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


