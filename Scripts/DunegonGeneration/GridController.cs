using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour {

    public Room room;

    [System.Serializable]
    public struct Grid{
        public float colums, rows;
        public float verticalOffset, horizontalOffset;
    }

    public Grid grid;
    public GameObject gridTile;
    public List<Vector2> availablePoints = new List<Vector2>();


    void Awake(){
        room = GetComponentInParent<Room>();
        grid.colums = Constants.roomWidth - Constants.offset;
        grid.rows = Constants.roomHeight - Constants.offset;
        generateGrid();
    }

    public void generateGrid(){
        grid.verticalOffset += room.transform.localPosition.y;
        grid.horizontalOffset += room.transform.localPosition.x;

        for(int y = 0; y < grid.rows; y++){
            for(int x = 0; x < grid.colums; x++){
                GameObject go = Instantiate(gridTile, transform);
                go.GetComponent<Transform>().position = new Vector2(
                    x - (grid.colums - grid.horizontalOffset),
                    y - (grid.rows - grid.verticalOffset)
                );
                go.name = ("X: " + x + ", Y: " + y);
                availablePoints.Add(go.transform.position);
            }
        }
    }

}