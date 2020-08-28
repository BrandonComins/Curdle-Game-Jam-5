using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DunegeonGenerator : MonoBehaviour{
    public DungeonGenerationData dungeonGenerationData;

    private List<Vector2Int> dungeonRooms;

    private void Start(){
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(
            dungeonGenerationData
        );
        spawnRooms(dungeonRooms);
    }

    private void spawnRooms(IEnumerable<Vector2Int> rooms){
        RoomController.instance.loadRoom("Start", 0 ,0);
        foreach(Vector2Int roomLocation in rooms){
            if(roomLocation == dungeonRooms[dungeonRooms.Count-1]
                && !(roomLocation == Vector2Int.zero)
            ){
                RoomController.instance.loadRoom(
                    "End",
                    roomLocation.x,
                    roomLocation.y    
                );
            }else{
                RoomController.instance.loadRoom(
                    "Empty", 
                    roomLocation.x, 
                    roomLocation.y
            );
            }    
        }
    }
}
    