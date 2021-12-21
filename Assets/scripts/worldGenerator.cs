using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldGenerator : MonoBehaviour {

    public Transform chunkPrefab;
    

    public int seed = 0;

    //each ground option goes there, the amount is how comon they are, stickyness is to keep them in groups better
    public Sprite[] grounds;
    public int[] amount = { 0, 4, 1, 1 };
    public int[] stickyness;
    private int length;

    public const int num = 10;

    public Vector2 chunkDimensions;
    public Vector2 cameraCoordinates;

    public Sprite empty;
    public Sprite[] grass;
    public Sprite[] water;
    public Sprite[] flower;
    public Sprite[] beehive;

    public string[] waterTitle;
    public string[] flowerTitle;
    public string[] hiveTitle;


    public static Transform[,] chunks;


    void fixLengths()
    {
        length = grounds.Length; //the default length will be the amount of ground options

        //shorten a long amount array
        if (amount.Length > length) //if amount is lonmger it will shorten it to length

        {
            int[] temp = new int[length];
            for (int i = 0; i < length; i++)
            {
                temp[i] = amount[i];
            }
            amount = temp;
        }

        //shorten a long stickyness aray
        if (stickyness.Length > length) //if stickyness is too long it will shorten it to be the same lenght adss the grounds
        {
            int[] temp = new int[length];
            for (int i = 0; i < length; i++)
            {
                temp[i] = stickyness[i];
            }
            stickyness = temp;
        }

        //lengthen a short amount array
        if (amount.Length < length)
        {
            int[] temp = new int[length];
            for (int i = 0; i < length; i++)
            {
                temp[i] = 0;
            }
            for (int i = 0; i < amount.Length; i++)
            {
                temp[i] = amount[i];
            }
            amount = temp;
        }

        //lengthen a short stickyness array
        if (stickyness.Length < length)
        {
            int[] temp = new int[length];
            for (int i = 0; i < length; i++)
            {
                temp[i] = 0;
            }
            for (int i = 0; i < stickyness.Length; i++)
            {
                temp[i] = stickyness[i];
            }
            stickyness = temp;
        }

    }

    // Use this for initialization
    void Start () {
        init();
	}

    void init() {
        fixLengths();
        Random.InitState(seed); //set the seed rightaway for the map generation
        generateFirstChunk();
        generateAllChunks();
        



    }

    private void generateFirstChunk()
    {
        chunks = new Transform[2, 2];
        Vector3 tempStart = new Vector3(0, 0, 0);
        chunks[0, 0] = Instantiate(chunkPrefab, transform);
        chunks[0, 0].transform.position = tempStart;
        chunks[0, 0].GetComponent<initChunk>().CHUNK = new Vector2(0, 0);
        chunks[0, 0].GetComponent<initChunk>().init();
        chunkDimensions = new Vector2(chunks[0, 0].GetComponent<initChunk>().worldWidth * chunks[0, 0].GetComponent<initChunk>().hexWidth,
                                     chunks[0, 0].GetComponent<initChunk>().worldHeight * chunks[0, 0].GetComponent<initChunk>().hexHeight);
    }

    private void generateAllChunks()
    {
        for (int y = 0; y < chunks.GetLength(0); y++)
        {
            for (int x = 0; x < chunks.GetLength(1); x++)
            {
                if (chunks[x, y] == null)
                {
                    Vector3 tempStart = new Vector3(0, 0, 0);
                    tempStart.x = -x * chunkDimensions.x;
                    tempStart.y = -y * chunkDimensions.y;
                    chunks[x, y] = Instantiate(chunkPrefab, transform);
                    chunks[x, y].transform.position = tempStart;
                    chunks[x, y].GetComponent<initChunk>().CHUNK = new Vector2(x, y);
                    chunks[x, y].GetComponent<initChunk>().init();
                }
            }
        }
    }


    public static float getRandomValue()
    {
        float temp = Random.value;
        return temp;
    }

	// Update is called once per frame
	void Update () {
        cameraCoordinates = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
        

    }
}
