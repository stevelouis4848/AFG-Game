using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public int xPos; //X coordinate of the lower left tile of the room
    public int yPos;
    public int roomWidth;
    public int roomHeight;
    public Direction enteringCorridor;


    //This is used for the first room, doesnt have corridor parameter, might need to alter this.
    public void SetupRoom (IntRange widthRange, IntRange heightRange, int columns, int rows)
    {
        roomWidth = widthRange.Random;
        roomHeight = heightRange.Random;

        xPos = Mathf.RoundToInt(columns / 2f - roomWidth / 2f);
        yPos = Mathf.RoundToInt(rows / 2f - roomHeight / 2f);
    }

    public void SetupRoom (IntRange widthRange, IntRange heightRange, int minRow, int maxRow, int minCol, int maxCol, Corridor corridor)
    {
        //set the entering corridor direction
        enteringCorridor = corridor.direction;

        roomWidth = widthRange.Random;
        roomHeight = heightRange.Random;

        if (corridor.direction == Direction.North)
        {
            // ... the height of the room mustn't go beyond the board so it must be clamped based
            // on the height of the board (rows) and the end of corridor that leads to the room.
            //might need to use similar to restrict different sectors of the map, depending how we implement the map*********
            roomHeight = Mathf.Clamp(roomHeight, 1, maxRow - corridor.EndPositionY);

            yPos = corridor.EndPositionY;

            xPos = Random.Range(corridor.EndPositionX - roomWidth + 1, corridor.EndPositionX);

            xPos = Mathf.Clamp(xPos, minCol, maxCol - roomWidth);

            if (yPos + roomHeight >= maxRow)
            {
                if (maxRow - yPos - 1 <= 0)
                    roomHeight = 0;
                else
                    roomHeight = maxRow - yPos - 1;
            }
        }
        else if (corridor.direction == Direction.East)
        {
            roomWidth = Mathf.Clamp(roomWidth, 1, maxCol - corridor.EndPositionX);
           //  Debug.LogFormat("minCol:{0} maxCol:{1} roomWidth:{2}", minCol, maxCol, roomHeight);
            xPos = corridor.EndPositionX;

            yPos = Random.Range(corridor.EndPositionY - roomHeight + 1, corridor.EndPositionY);

            yPos = Mathf.Clamp(yPos, minRow, maxRow - roomHeight);


            if (xPos + roomWidth >= maxCol)
            {
                if (maxCol - xPos - 1 <= 0)
                    roomWidth = 0;
                else
                    roomWidth = maxCol - xPos - 1;
            }
        }

        else if (corridor.direction == Direction.South)
        {
            roomHeight = Mathf.Clamp(roomHeight, 1, corridor.EndPositionY);

            yPos = corridor.EndPositionY - roomHeight + 1;

            xPos = Random.Range(corridor.EndPositionX - roomWidth + 1, corridor.EndPositionX);

            xPos = Mathf.Clamp(xPos, minCol, maxCol - roomWidth);


            if (yPos - roomHeight <= minRow)
            {
                if (yPos - minRow + 1 <= 0)
                    roomHeight = 0;
                else
                    roomHeight = yPos - minRow + 1;
            }
        }
        else if (corridor.direction == Direction.West)
        {
            roomWidth = Mathf.Clamp(roomWidth, 1, corridor.EndPositionX);

            xPos = corridor.EndPositionX - roomWidth + 1;

            yPos = Random.Range(corridor.EndPositionY - roomHeight + 1, corridor.EndPositionY);

            yPos = Mathf.Clamp(yPos, minRow, maxRow - roomHeight);

            if (xPos - roomWidth <= minCol)
            {
                if (xPos-minCol + 1 <= 0)
                    roomWidth = 0;
                else
                    roomWidth = xPos - minCol + 1;
            }

        }
    }
}
