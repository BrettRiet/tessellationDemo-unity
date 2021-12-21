using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initChunk : MonoBehaviour {

    //the basichexagon prefab
    public Transform hexPrefab;

 //   public GameObject Manger;

    //how many hexagons are int the world
    public int worldWidth = 20;
    public int worldHeight = 20;


    //the size of the hexagons for placement
    public float hexWidth = 3.1f;
    public float hexHeight = 2.7f;
    public float distance = 10.0f;

    //where it will start placing them procedurally
    public Vector3 startPos;

    //an array top keep track of all hexagons i have
    public Transform[,] allHex;

    public Vector2 CHUNK;

	// Use this for initialization
	void Start () {
        
            
	}

    public void init()
    {
        //find where to start then create the grid with empty hex's
        calculateStartPos();
        createEmptyGrid();
        this.GetComponent<proceduralManager>().init();
    }

    void calculateStartPos()
    {
        startPos = transform.position;
    }

    void createEmptyGrid() //will instantate all the grid positions
    {
        allHex = new Transform[worldWidth,worldHeight];
        for (int y = 0; y < worldHeight; y++) //this will be the height
        {
            for (int x = 0; x < worldWidth; x++) //this will be the width
            {
                allHex[x,y]  = Instantiate(hexPrefab,this.transform); //instantiate it and add it to my array for future use
                allHex[x, y].GetComponent<tileManager>().tilePos = new Vector2(x, y);
                allHex[x,y].position = calcWorldPos(x,y);   //set the positions via the calcworldpos function which takes the ints of the hex position(x,y)
            }
        }
    }

    Vector3 calcWorldPos(int x, int y) //get the grid location and then multiplies it buy the dimenstions of the hexigon to create a grid
    {
        //add the startpos here
        Vector3 position = new Vector3((x * hexWidth), (y * hexHeight), distance) + startPos;

        if (y % 2 == 1)
        {       //or here
                position = new Vector3((x * hexWidth) + (hexWidth/2), (y * hexHeight), distance) + startPos;
        }
       //return it to the initialization area
        return position;
    }


}
