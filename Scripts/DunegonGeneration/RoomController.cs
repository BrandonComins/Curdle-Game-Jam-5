using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RoomInfo{

    public string name;
    public int x;
    public int y;

}

public class RoomController : MonoBehaviour {

    public static RoomController instance;
    string currentWorldName = "Garden";
    RoomInfo currentLoadRoomData;

    Room currentRoom;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;

    void Awake(){
        instance = this;
    }

    //testing
    void Start(){
        // loadRoom("Start", 0, 0);
        // loadRoom("Empty", 0, 1);
        // loadRoom("Empty", 0, -1);
    }

    void Update(){
        updateRoomQueue();
    }

    void updateRoomQueue(){
        if(isLoadingRoom){
            return;
        }
        if(loadRoomQueue.Count == 0){
            return;
        }else{
            currentLoadRoomData = loadRoomQueue.Dequeue();
            isLoadingRoom = true;
            StartCoroutine(loadRoomRoutine(currentLoadRoomData));
        }
    }

    public void loadRoom(string name, int x, int y){
        if(doesRoomExist(x,y)){
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.x = x;
        newRoomData.y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator loadRoomRoutine(RoomInfo info){
        string roomName = currentWorldName + info.name;
        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(
            roomName, 
            LoadSceneMode.Additive
        );

        while(loadRoom.isDone == false){
            yield return null;
        }    
    }

    public void registerRoom(Room room){
        if(!doesRoomExist(currentLoadRoomData.x, currentLoadRoomData.y)){
            room.transform.position = new Vector3(
                currentLoadRoomData.x * Constants.roomWidth,
                currentLoadRoomData.y * Constants.roomHeight,
                0
            );
            room.x = currentLoadRoomData.x;
            room.y = currentLoadRoomData.y;
            room.name = currentWorldName 
                + "-" 
                + currentLoadRoomData.name
                + " " 
                + room.x 
                + ", " 
                + room.y; 
            room.transform.parent = transform;

            isLoadingRoom = false;
            
            if(loadedRooms.Count == 0){
                CameraController.instance.currentRoom = room;

            }
        loadedRooms.Add(room);
        room.removeUnconnectedDoors();
        }else{
            Destroy(room.gameObject); 
            isLoadingRoom = false;
        }
    }
    public bool doesRoomExist(int x, int y){
        return loadedRooms.Find(
            item => item.x == x
            && item.y == y
        )   != null;
    }

    public Room findRoom(int x, int y){
        return loadedRooms.Find(
            item => item.x == x
            && item.y == y
        );
    }

    public void onPlayerEnterRoom(Room room){
        CameraController.instance.currentRoom = room;
        currentRoom = room;
    }
}
