using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCrawler : MonoBehaviour{
    
    public Vector2Int position {get; set;}
    public DungeonCrawler(Vector2Int startPosition){
        position = startPosition;
    }
    public Vector2Int move(Dictionary<direction,Vector2Int> directionMovementMap){
        direction toMove = (direction)Random.Range(0,directionMovementMap.Count);
        position += directionMovementMap[toMove];
        return position;
    }
}
   