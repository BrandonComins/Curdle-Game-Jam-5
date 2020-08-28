using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController instance;

    public Room currentRoom;

    void Awake(){
        instance = this;
    }

    void start(){

    }

    void Update(){
        updatePosition();
    }

    private void updatePosition(){
        if(currentRoom == null){
            return;
        }
        Vector3 targetPosition = getCameraTargetPosition();
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            Time.deltaTime * Constants.moveSpeedWhenChangeRoom
        );
    }

    private Vector3 getCameraTargetPosition(){
        if(currentRoom == null){
            return Vector3.zero;
        }
        Vector3 targetPosition = currentRoom.getRoomCenter();
        targetPosition.z = transform.position.z;
        return targetPosition;
    }

    public bool IsSwitchScene(){
        return transform.position.Equals(getCameraTargetPosition()) == false;
    }

}
