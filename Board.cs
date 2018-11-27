using System.Collections;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

public class Board : MonoBehaviour {

    public enum TileType
    {
        Wall, Floor,
    }

    public int columns = 100;
    public int rows = 100;

    public IntRange numRooms = new IntRange(15, 20);
    public IntRange roomWidth = new IntRange(3, 10);
    public IntRange roomHeight = new IntRange(3, 10);
    public IntRange corridorLength = new IntRange(6, 10);
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] outerWallTiles;
    public GameObject player;
    public GameObject NPC;
    public GameObject Spawner;
	// public GameObject winLoose;
  
    private TileType[][] tiles;
    private Room[] rooms1;
    private Room[] rooms2;
    private Room[] rooms3;
    private Room firstRoom;
    private Corridor northCor;
    private Corridor[] corridors1;
    private Corridor[] corridors2;
    private Corridor[] corridors3;
    private GameObject boardHolder;
    private int minRowSouth;
    private int maxRowSouth;
    private int minColSouth;
    private int maxColSouth;
    private int minRowEast;
    private int maxRowEast;
    private int minColEast;
    private int maxColEast;
    private int minRowWest;
    private int maxRowWest;
    private int minColWest;
    private int maxColWest;



    // Use this for initialization
    public void Start () {
        //create board holder
        if (GameObject.Find("BoardHolder") == null)
        {
            boardHolder = new GameObject("BoardHolder");
            SetupTilesArray();
            CreateRoomsAndCorridors(0);
            CreateRoomsAndCorridors(2);
            CreateRoomsAndCorridors(1);
            CreateRoomsAndCorridors(3);
            SetTilesValuesForRooms();
            SetTilesValuesForCorridors();
            InstantiateTiles();
            InstantiateOuterWalls();
        }

        player =  GameObject.FindGameObjectWithTag( "Player" );    
	}

    void SetupTilesArray(){
        //setup jagged array to correct width
        tiles = new TileType[columns][];
        //go through all the tiles arrays
        for (int i = 0; i < tiles.Length; i++){
            //set each tile array to the correct height
            tiles[i] = new TileType[rows];
        }
    }

    void CreateRoomsAndCorridors (int section)
    {


        if (section==0){//NORTH


            //there should be one less corridor than there is rooms


            //create the first room and corridor
            firstRoom = new Room();
            northCor = new Corridor();

            //setup first room
            firstRoom.SetupRoom(roomWidth, roomHeight, columns, rows);

            //setup first corridor based on first room
            northCor.SetupCorridor(firstRoom, corridorLength, roomWidth, roomHeight, 0, rows,0,columns, true, 0);
            Vector3 playerPos = new Vector3(firstRoom.xPos, firstRoom.yPos, 0);
            Instantiate(player, playerPos, Quaternion.identity);
            // Instantiate(NPC, playerPos, Quaternion.identity);
			// Instantiate(winLoose, playerPos, Quaternion.identity);
		}
        else if (section == 1)//EAST
        {

            //create the rooms array with a random size
            rooms1 = new Room[numRooms.Random];

            //there should be one less corridor than there is rooms
            corridors1 = new Corridor[rooms1.Length - 1];

            //create the first room and corridor
            rooms1[0] = firstRoom;
            corridors1[0] = new Corridor();

            //setup first room
            //rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);
            minRowEast = 0;
            maxRowEast = rows;
            minColEast = 0;
            maxColEast = columns;
            //setup first corridor based on first room
            corridors1[0].SetupCorridor(rooms1[0], corridorLength, roomWidth, roomHeight, minRowEast, maxRowEast, minColEast, maxColEast, true, 1);

            minRowEast = maxRowSouth+1;
            maxRowEast = rows-1;
            minColEast = corridors1[0].EndPositionX;
            maxColEast = columns-1;

            for (int i = 1; i < rooms1.Length; i++)
            {
                //create room
                rooms1[i] = new Room();

                //setup room based on previous corridor
                rooms1[i].SetupRoom(roomWidth, roomHeight, minRowEast, maxRowEast, minColEast, maxColEast, corridors1[i - 1]);

                //if we haven't reched the end of the corridors array
                if (i < corridors1.Length)
                {
                    //create a corridor
                    corridors1[i] = new Corridor();

                    //setup corridors cased on the room that was just created
                    corridors1[i].SetupCorridor(rooms1[i], corridorLength, roomWidth, roomHeight, minRowEast, maxRowEast, minColEast, maxColEast, false, 1);
                }
            }
            for (int i = 0; i < 15; i++)
            {
                int ran = Random.Range(2, rooms1.Length - 1);
                Room x = rooms1[ran];
                Vector3 spawnerpos = new Vector3(x.xPos, x.yPos, 0);
                Instantiate(Spawner, spawnerpos, Quaternion.identity);
            }
        }
        else if (section == 2)//SOUTH
        {
            //create the rooms array with a random size
            rooms2 = new Room[numRooms.Random];

            //there should be one less corridor than there is rooms
            corridors2 = new Corridor[rooms2.Length - 1];

            //create the first room and corridor
            rooms2[0] = firstRoom;
            corridors2[0] = new Corridor();

            //setup first room
            //rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);
            minRowSouth = 0;
            maxRowSouth = rows;
            minColSouth = 0;
            maxColSouth = columns;
            //setup first corridor based on first room
            corridors2[0].SetupCorridor(rooms2[0], corridorLength, roomWidth, roomHeight, minRowSouth, maxRowSouth, minColSouth, maxColSouth, true, 2);

            minRowSouth = 0;
            maxRowSouth = corridors2[0].EndPositionY;
            minColSouth = 0;
            maxColSouth = columns;

            for (int i = 1; i < rooms2.Length; i++)
            {
                //create room
                rooms2[i] = new Room();

                //setup room based on previous corridor
                rooms2[i].SetupRoom(roomWidth, roomHeight, minRowSouth, maxRowSouth, minColSouth, maxColSouth, corridors2[i - 1]);

                //if we haven't reched the end of the corridors array
                if (i < corridors2.Length)
                {
                    //create a corridor
                    corridors2[i] = new Corridor();

                    //setup corridors cased on the room that was just created
                    corridors2[i].SetupCorridor(rooms2[i], corridorLength, roomWidth, roomHeight, minRowSouth, maxRowSouth, minColSouth, maxColSouth, false, 2);
                }
            }
            for (int i = 0; i < 15; i++)
            {
                int ran = Random.Range(2, rooms2.Length - 1);
                Room x = rooms2[ran];
                Vector3 spawnerpos = new Vector3(x.xPos, x.yPos, 0);
                Instantiate(Spawner, spawnerpos, Quaternion.identity);
            }
        }
        else if (section == 3)//WEST
        {
            //create the rooms array with a random size
            rooms3 = new Room[numRooms.Random];

            //there should be one less corridor than there is rooms
            corridors3 = new Corridor[rooms3.Length - 1];

            //create the first room and corridor
            rooms3[0] = firstRoom;
            corridors3[0] = new Corridor();

            //setup first room
            //rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);
            minRowWest = 0;
            maxRowWest = rows;
            minColWest = 0;
            maxColWest = columns;
            //setup first corridor based on first room
            corridors3[0].SetupCorridor(rooms3[0], corridorLength, roomWidth, roomHeight, minRowWest, maxRowWest, minColWest, maxColWest, true, 3);

            minRowWest = maxRowSouth + 1;
            maxRowWest = rows;
            minColWest = 0;
            maxColWest = corridors3[0].EndPositionX;
            //public void SetupRoom(IntRange widthRange, IntRange heightRange, int minRow, int maxRow, int minCol, int maxCol, Corridor corridor)
            for (int i = 1; i < rooms3.Length; i++)
            {
                //create room
                rooms3[i] = new Room();

                //setup room based on previous corridor
                rooms3[i].SetupRoom(roomWidth, roomHeight, minRowWest, maxRowWest, minColWest, maxColWest, corridors3[i - 1]);

                //if we haven't reched the end of the corridors array
                if (i < corridors3.Length)
                {
                    //create a corridor
                    corridors3[i] = new Corridor();

                    //setup corridors cased on the room that was just created
                    // Debug.LogFormat("i: {0} length: {1}", i, corridors3.Length);
                    corridors3[i].SetupCorridor(rooms3[i], corridorLength, roomWidth, roomHeight, minRowWest, maxRowWest, minColWest, maxColWest, false, 3);
                }
            }
            for (int i = 0; i < 15; i++)
            {
                int ran = Random.Range(2, rooms3.Length - 1);
                Room x = rooms3[ran];
                Vector3 spawnerpos = new Vector3(x.xPos, x.yPos, 0);
                Instantiate(Spawner, spawnerpos, Quaternion.identity);
            }
        }
    }
	
    void SetTilesValuesForRooms(){
        //Go through all the rooms
        
        for (int i = 0; i < rooms1.Length; i++)
        {
            Room currentRoom = rooms1[i];

            // and for each room go through its width
            for (int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoord = currentRoom.xPos + j;

                //for each horizontal tile, go up vertically through height
                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPos + k;

                    //the coordinates in the jagged array are based on the rooms position and its width and height
                    //tiles[xCoord][yCoord] = TileType.Floor;
                    if (xCoord > columns - 1)
                        xCoord = columns - 1;
                    else if (xCoord < 0)
                        xCoord = 0;
                    if (yCoord > rows - 1)
                        yCoord = rows - 1;
                    else if (yCoord < 0)
                        yCoord = 0;
                    //tiles[(xCoord > columns-1 ? columns-1 : xCoord)][(yCoord > rows-1 ? rows - 1 : yCoord)] = TileType.Floor;
                    tiles[xCoord][yCoord] = TileType.Floor;
                }
            }
        }
        for (int i = 0; i < rooms2.Length; i++)
        {
            Room currentRoom = rooms2[i];

            // and for each room go through its width
            for (int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoord = currentRoom.xPos + j;

                //for each horizontal tile, go up vertically through height
                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPos + k;

                    //the coordinates in the jagged array are based on the rooms position and its width and height
                    if (xCoord > columns - 1)
                        xCoord = columns - 1;
                    else if (xCoord < 0)
                        xCoord = 0;
                    if (yCoord > rows - 1)
                        yCoord = rows - 1;
                    else if (yCoord < 0)
                        yCoord = 0;
                    tiles[xCoord][yCoord] = TileType.Floor;
                }
            }
        }
        for (int i = 0; i < rooms3.Length; i++)
        {
            Room currentRoom = rooms3[i];

            // and for each room go through its width
            for (int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoord = currentRoom.xPos + j;

                //for each horizontal tile, go up vertically through height
                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPos + k;

                    //the coordinates in the jagged array are based on the rooms position and its width and height
                    if (xCoord > columns - 1)
                        xCoord = columns - 1;
                    else if (xCoord < 0)
                        xCoord = 0;
                    if (yCoord > rows - 1)
                        yCoord = rows - 1;
                    else if (yCoord < 0)
                        yCoord = 0;
                    tiles[xCoord][yCoord] = TileType.Floor;
                }
            }
        }
    }

    void SetTilesValuesForCorridors(){
        //go through every corridor
        Corridor currentCorridor = northCor;

        //and go through length
        for (int j = 0; j < currentCorridor.corridorLength; j++)
        {
            int xCoord = currentCorridor.startXPos;
            int yCoord = currentCorridor.startYPos;

            //depending on the direction, add or sub from the apppropriate coordinate
            //based on how far the length the loop is

            if (currentCorridor.direction == Direction.North)
            {
                yCoord += j;
            }
            else if (currentCorridor.direction == Direction.East)
            {
                xCoord += j;
            }
            else if (currentCorridor.direction == Direction.South)
            {
                yCoord -= j;
            }
            else if (currentCorridor.direction == Direction.West)
            {
                xCoord -= j;
            }
            //set the tile at these coordinates to floor
            tiles[xCoord][yCoord] = TileType.Floor;
        }
            for (int i = 0; i < corridors1.Length; i++)
        {
            currentCorridor = corridors1[i];

            //and go through length
            for (int j = 0; j < currentCorridor.corridorLength; j++)
            {
                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;

                //depending on the direction, add or sub from the apppropriate coordinate
                //based on how far the length the loop is

                if (currentCorridor.direction == Direction.North)
                {
                    yCoord += j;
                }
                else if (currentCorridor.direction == Direction.East)
                {
                    xCoord += j;
                }
                else if (currentCorridor.direction == Direction.South)
                {
                    yCoord -= j;
                }
                else if (currentCorridor.direction==Direction.West)
                {
                    xCoord -= j;
                }
                //set the tile at these coordinates to floor
                if (xCoord > columns - 1)
                    xCoord = columns - 1;
                else if (xCoord < 0)
                    xCoord = 0;
                if (yCoord > rows - 1)
                    yCoord = rows - 1;
                else if (yCoord < 0)
                    yCoord = 0;
                tiles[xCoord][yCoord] = TileType.Floor;

            }
        }
        for (int i = 0; i < corridors2.Length; i++)
        {
            currentCorridor = corridors2[i];

            //and go through length
            for (int j = 0; j < currentCorridor.corridorLength; j++)
            {
                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;

                //depending on the direction, add or sub from the apppropriate coordinate
                //based on how far the length the loop is

                if (currentCorridor.direction == Direction.North)
                {
                    yCoord += j;
                }
                else if (currentCorridor.direction == Direction.East)
                {
                    xCoord += j;
                }
                else if (currentCorridor.direction == Direction.South)
                {
                    yCoord -= j;
                }
                else if (currentCorridor.direction == Direction.West)
                {
                    xCoord -= j;
                }
                //set the tile at these coordinates to floor
                if (xCoord > columns - 1)
                    xCoord = columns - 1;
                else if (xCoord < 0)
                    xCoord = 0;
                if (yCoord > rows - 1)
                    yCoord = rows - 1;
                else if (yCoord < 0)
                    yCoord = 0;
                tiles[xCoord][yCoord] = TileType.Floor;

            }
        }
        for (int i = 0; i < corridors3.Length; i++)
        {
            currentCorridor = corridors3[i];

            //and go through length
            for (int j = 0; j < currentCorridor.corridorLength; j++)
            {
                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;

                //depending on the direction, add or sub from the apppropriate coordinate
                //based on how far the length the loop is

                if (currentCorridor.direction == Direction.North)
                {
                    yCoord += j;
                }
                else if (currentCorridor.direction == Direction.East)
                {
                    xCoord += j;
                }
                else if (currentCorridor.direction == Direction.South)
                {
                    yCoord -= j;
                }
                else if (currentCorridor.direction == Direction.West)
                {
                    xCoord -= j;
                }
                //set the tile at these coordinates to floor
                if (xCoord > columns - 1)
                    xCoord = columns - 1;
                else if (xCoord < 0)
                    xCoord = 0;
                if (yCoord > rows - 1)
                    yCoord = rows - 1;
                else if (yCoord < 0)
                    yCoord = 0;
                tiles[xCoord][yCoord] = TileType.Floor;

            }
        }
    }

    void InstantiateTiles()
    {
        //go through all tiles
        for (int i = 0; i < tiles.Length; i++)
        {
            for (int j = 0; j < tiles[i].Length; j++){
            
                if(tiles[i][j] == TileType.Wall)
                {   
                    //instantiate a wall over top
                    InstantiateFromArray(wallTiles, i, j);
                }
                else{
                     
                    //Instantiate a floor tile
                    InstantiateFromArray(floorTiles, i, j);

                }            
            }
        }
    }

    void InstantiateOuterWalls()
    {
        //the outer wealls are one unit left, right, up, or down from board
        float leftEdgeX = -1f;
        float rightEdgeX = columns + 0f;
        float bottomEdgeY = -1f;
        float topEdgeY = rows + 0f;

        //Instantiate both vertical walls (one on each side)
        InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY);
        InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);

        //instantiate both horizontal walls, these are one on left and right from outer wall
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);
    }

    void InstantiateVerticalOuterWall(float xCoord, float startingY, float endingY){
        //start the loop at the starting value for Y
        float currentY = startingY;

        //while val for Y is less than the end value
        while(currentY <= endingY)
        {
            //instantiate an outer wall tile at the x coordinate and the current Y coordinate
            InstantiateFromArray(outerWallTiles, xCoord, currentY);

            currentY++;
        }
    }

    void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord)
    {
        //start the loop at the starting value for X
        float currentX = startingX;

        //while val for X is less than the end value
        while (currentX <= endingX)
        {
            //instantiate an outer wall tile at the y coordinate and the current x coordinate
            InstantiateFromArray(outerWallTiles, currentX, yCoord);

            currentX++;
        }
    }


    void InstantiateFromArray (GameObject[] prefabs, float xCoord, float yCoord)
    {
        //create a random index for the array
        int randomIndex = Random.Range(0, prefabs.Length);

        //the position to be instantiated at is based on the coordinates
        Vector3 position = new Vector3(xCoord, yCoord, 0f);

        //create an instance of the prefab from the random index of the array
        GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;
        //set the tile's parent to the board holder
        tileInstance.transform.parent = boardHolder.transform;
    }
}
