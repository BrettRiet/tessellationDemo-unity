using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileManager : MonoBehaviour {

    public Transform uiController;

    public worldGenerator World;

    public Vector2 tilePos;
    public int type;
    public int upgradeLvl = 0;
    public int maxLvl = 0;
    public int maxValue;
    public int currentValue;

    public int sellprice = 0;

    public int[] upgradeCost;
    public GameObject outline;

  

  

	// Use this for initialization
	void Start () {
       
	}

    public void init()
    {
        uiController = GameObject.FindGameObjectWithTag("UiController").transform;
        World = GameObject.FindGameObjectWithTag("WorldGenerator").GetComponent<worldGenerator>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void select()
    {
        uiController.GetComponent<gamePlayUI>().setDetailButtonActive(true);
        outline.SetActive(true);
    }
    public void deselect()
    {
        uiController.GetComponent<gamePlayUI>().setDetailButtonActive(false);
        outline.SetActive(false);
    }

    public void upgrade()
    {
        if (upgradeLvl < maxLvl)
        {
            if (World.GetComponent<MoneyManager>().spendHoney(upgradeCost[upgradeLvl+1]))
            {
                uiController.GetComponent<gamePlayUI>().UpdateHoneyCounter();
                upgradeLvl++;
                setSprite(type, upgradeLvl);
            }
        }

    }

    public void sellOff()
    {
        if (type > 1)
        {
            sellprice = (upgradeCost[upgradeLvl] / 2);
            Debug.Log(sellprice);
            World.GetComponent<MoneyManager>().depositHoney(sellprice);
            setSprite(1, 0);
        }
    }

    public void setSprite(int Type, int Index)
    {
        upgradeLvl = Index;
        type = Type;
        switch (type) //sets the sprite
        {
            case 0:
                GetComponent<SpriteRenderer>().sprite = World.empty;
                maxLvl = 0;
                break;
            case 1:
                if (upgradeLvl >= World.grass.Length)
                   upgradeLvl = 0;
                maxLvl = 0;
                GetComponent<SpriteRenderer>().sprite = World.grass[upgradeLvl];
                break;
            case 2:
                if (upgradeLvl >= World.water.Length)
                    upgradeLvl = 0;
                maxLvl = 3;
                GetComponent<SpriteRenderer>().sprite = World.water[upgradeLvl];
                break;
            case 3:
                maxLvl = 1;
                if (upgradeLvl >= World.flower.Length)
                    upgradeLvl = 0;
                GetComponent<SpriteRenderer>().sprite = World.flower[upgradeLvl];
                break;
            case 4:
                maxLvl = 0;
                if (upgradeLvl >= World.beehive.Length)
                    upgradeLvl = 0;
                GetComponent<SpriteRenderer>().sprite = World.beehive[upgradeLvl];
                break;
            default:
                break;

        }
        setValues(); //now it sets the max value after it has set the new sprite
    }

    public void setValues()
    {
        switch (upgradeLvl) //sets the sprite
        {
            case 0:
                maxValue = 20;
                currentValue = 20;
                break;
            case 1:

                maxValue = 80;
                currentValue = 80;
                break;
            case 2:
                maxValue = 200;
                currentValue = 200;
                break;
            case 3:
                maxValue = 1000;
                currentValue = 1000;
                break;
            default:
                maxValue = 0;
                currentValue = 0;
                break;

        }
    }
}
