using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 public enum direction{
        up = 0,
        left = 1,
        down = 2,
        right = 3
    };

public class DungeonCrawlerController : MonoBehaviour{

    public static List<Vector2Int> positionVisited = new List<Vector2Int>();
    private static readonly Dictionary<
        direction, 
        Vector2Int>
        directionMovementMap = new Dictionary<direction, Vector2Int>{
            {direction.up, Vector2Int.up},
            {direction.left, Vector2Int.left},
            {direction.down, Vector2Int.down},
            {direction.right, Vector2Int.right}
        };

    public static List<Vector2Int> GenerateDungeon(
        DungeonGenerationData dungeonData
    ){
            List<DungeonCrawler> dungeonCrawlers = new List<DungeonCrawler>();

        for(int i = 0; i < dungeonData.numberOfCrawlers; i++){
            dungeonCrawlers.Add(new DungeonCrawler(Vector2Int.zero));

        }
        int iterations = Random.Range(
            dungeonData.iterationMin, 
            dungeonData.iterationMax
        );
        for(int i = 0; i < iterations; i++){
            foreach(DungeonCrawler dungeonCrawler in dungeonCrawlers){       
                Vector2Int newPosition = dungeonCrawler.move(directionMovementMap);
                positionVisited.Add(newPosition);
            }
        }
        return positionVisited;
    }
}
