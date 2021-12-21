using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proceduralManager : MonoBehaviour {

    //private int seed = 0;

    //each ground option goes there, the amount is how comon they are, stickyness is to keep them in groups better
   // public Sprite[] grounds = null;
    public int[] amount = null;
    public int[] stickyness = null;

//    public GameObject deChunk;
    public Transform[,] grid;

    public int length;

	
    public void init()
    {

        getValues();
        
        //this must happen before being normalized!
        //fixLengths(); //make sure all the parralel arrays are equal lengths 

        normalizeProbabilites();
       
        randomizeChunk();
    }

    void getValues()
    {
        length = this.GetComponentInParent<worldGenerator>().grounds.Length;
       
        amount = new int[length];
        for (int i = 0; i < length; i++)
        {
            amount[i] = GetComponentInParent<worldGenerator>().amount[i];
        }
        stickyness = new int[length];
        for (int i = 0; i < length; i++)
        {
            stickyness[i] = GetComponentInParent<worldGenerator>().stickyness[i];
        }
    }

    void randomizeChunk()
    {
        //Debug.Log(grid);
        grid = this.GetComponent<initChunk>().allHex; //GET THE GRID IM WORKING WITH
        //Debug.Log(grid);
    



        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                int rand = (int)(worldGenerator.getRandomValue() * amount[length - 1]) + 1;
                assignRanAmount(rand, x, y);

            }
        }
       

    }

    public void assignRanAmount(int r, int x, int y)
    {
        for (int i = 0; i < length; i++)
        {
            if (r <= amount[i])
            {
                //i used to set the sprites directly from here
                // grid[x, y].GetComponent<SpriteRenderer>().sprite = grounds[i];
                // grid[x, y].GetComponent<tileManager>().type = i;
                grid[x, y].GetComponent<tileManager>().init();
                grid[x, y].GetComponent<tileManager>().setSprite(i,0);
                break;
            }

        }
    }


    void normalizeProbabilites() //this will make it easier to assign the random value to the the arrays
    {
       // int total = 0;// need this to assign the probability of a sprite option

        for (int i = 1; i <length; i++) //find out the higest value the random number will ever be
        {
            amount[i] = (amount[i] + amount[i - 1]); // increase numbers deeper in array in order.
        }
        
    }

    //fixes the arrays being different lengths
   

    // Update is called once per frame
    void Update () {
		
	}
}
