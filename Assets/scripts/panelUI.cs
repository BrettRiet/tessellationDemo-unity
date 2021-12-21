using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panelUI : MonoBehaviour {

    public Button upgradeButton;  //this button is on the detail panel
    public Button sellButton;
    public Text title;
    public Text level;
    public Transform valueText;

    public Image flowerpurchaseimg;
    public Image waterpurchaseimg;
    public Image beehivepurchaseimg;

    public Transform ConfimrationSellPage;
    

    // Use this for initialization
    void Start () {
        init();
	}

    public void init()
    {
        upgradeButton.onClick.AddListener(upgradeTile);
        sellButton.onClick.AddListener(sellTile);
        ConfimrationSellPage.gameObject.SetActive(false);

        flowerpurchaseimg.transform.Find("buyFlowers").GetComponent<Button>().onClick.AddListener(buyFlower);
        waterpurchaseimg.transform.Find("buyWater").GetComponent<Button>().onClick.AddListener(buyWater);
        beehivepurchaseimg.transform.Find("buyHive").GetComponent<Button>().onClick.AddListener(buyHive);
    }

    public void buyHive()
    {
        if (GameObject.FindGameObjectWithTag("WorldGenerator").GetComponent<MoneyManager>().spendHoney(10))
        {
            GameObject.FindGameObjectWithTag("UiController").GetComponent<gamePlayUI>().UpdateHoneyCounter();
            collisionManager.selected.GetComponent<tileManager>().setSprite(4, 0);
        }
        setUITitle();
    }

    public void buyFlower() {
        if (GameObject.FindGameObjectWithTag("WorldGenerator").GetComponent<MoneyManager>().spendHoney(10))
        {
            GameObject.FindGameObjectWithTag("UiController").GetComponent<gamePlayUI>().UpdateHoneyCounter();
            collisionManager.selected.GetComponent<tileManager>().setSprite(3, 0);
        }
        setUITitle();

    }

    public void buyWater() {
        if (GameObject.FindGameObjectWithTag("WorldGenerator").GetComponent<MoneyManager>().spendHoney(10))
        {
            GameObject.FindGameObjectWithTag("UiController").GetComponent<gamePlayUI>().UpdateHoneyCounter();
            collisionManager.selected.GetComponent<tileManager>().setSprite(2, 0);
        }
        setUITitle();

    }

    public void upgradeTile() //will find the selected tile and then upgrade it
    {
        collisionManager.selected.GetComponent<tileManager>().upgrade();
        setUITitle();
        setUIsimpleValues();
    }
    public void sellTile()
    {
        tileManager temp = collisionManager.selected.GetComponent<tileManager>();
        if (temp.type > 1)
        {
            ConfimrationSellPage.gameObject.SetActive(true);
            ConfimrationSellPage.GetChild(2).GetComponent<Button>().onClick.AddListener(confirmSale);
            ConfimrationSellPage.GetChild(3).GetComponent<Button>().onClick.AddListener(cancelSale);
            ConfimrationSellPage.GetChild(4).GetComponent<Text>().text = "+" + (temp.upgradeCost[temp.upgradeLvl] / 2);

        }
    }
    public void confirmSale()
    {
        
        collisionManager.selected.GetComponent<tileManager>().sellOff();
        ConfimrationSellPage.gameObject.SetActive(false);
        setUITitle();
        
    }

    public void cancelSale()
    {
        ConfimrationSellPage.gameObject.SetActive(false);

    }

    public void setUITitle()
    {
        tileManager temp = collisionManager.selected.GetComponent<tileManager>();
        int type = temp.type;
        int lvl = temp.upgradeLvl;
        switch (type)
        {
            case 0:
                title.text = "Error, no tile";
                level.text = null;
                flowerpurchaseimg.gameObject.SetActive(false);
                waterpurchaseimg.gameObject.SetActive(false);
                beehivepurchaseimg.gameObject.SetActive(false);
                sellButton.interactable = false;
                upgradeButton.gameObject.SetActive(false);

                valueText.gameObject.SetActive(false);

                break;
            case 1:
                title.text = "Grass";
                level.text = null;
                flowerpurchaseimg.gameObject.SetActive(true);
                beehivepurchaseimg.gameObject.SetActive(true);
                waterpurchaseimg.gameObject.SetActive(true);
                sellButton.interactable = false;
                upgradeButton.gameObject.SetActive(false);
                valueText.gameObject.SetActive(false);
                break;
            case 2:
                title.text = collisionManager.selected.GetComponent<tileManager>().World.waterTitle[lvl];
                sellButton.interactable = true;
                flowerpurchaseimg.gameObject.SetActive(false);
                beehivepurchaseimg.gameObject.SetActive(false);
                waterpurchaseimg.gameObject.SetActive(false);
                valueText.gameObject.SetActive(true);
                upgradeButton.gameObject.SetActive(true);

                level.text = "Water " + (lvl + 1);
                valueText.GetComponent<Text>().text = "Water Level";

                break;
            case 3:
                title.text = collisionManager.selected.GetComponent<tileManager>().World.flowerTitle[lvl];
                sellButton.interactable = true;
                flowerpurchaseimg.gameObject.SetActive(false);
                beehivepurchaseimg.gameObject.SetActive(false);
                waterpurchaseimg.gameObject.SetActive(false);
                upgradeButton.gameObject.SetActive(true);
                valueText.gameObject.SetActive(true);
                level.text = "Flower " + (lvl + 1);
                valueText.GetComponent<Text>().text = "Flower's Life Span";
                break;
            case 4:
                title.text = collisionManager.selected.GetComponent<tileManager>().World.hiveTitle[lvl];
                sellButton.interactable = true;
                flowerpurchaseimg.gameObject.SetActive(false);
                beehivepurchaseimg.gameObject.SetActive(false);
                waterpurchaseimg.gameObject.SetActive(false);
                upgradeButton.gameObject.SetActive(true);
               
                valueText.gameObject.SetActive(true);
                level.text = "Beehive " + (lvl + 1);
                valueText.GetComponent<Text>().text = "Bees Alive";
                break;
            default:
                break;
        }
        setUIsimpleValues();


    }
    public void setUIsimpleValues()
    {
        tileManager temp = collisionManager.selected.GetComponent<tileManager>();
        int upCost = temp.upgradeCost[(temp.upgradeLvl)+1];
        valueText.Find("VALUE").GetComponent<Text>().text = temp.currentValue + "/" + temp.maxValue;
        upgradeButton.transform.Find("VALUE").GetComponent<Text>().text = upCost.ToString();
        if (upCost > PlayerPrefs.GetInt("CURRENTHONEY") && temp.upgradeLvl < temp.maxLvl)
        {
            upgradeButton.transform.Find("TITLE").GetComponent<Text>().color = new Vector4(.3f, .3f, .3f, .5f);
        }
        else
            upgradeButton.transform.Find("TITLE").GetComponent<Text>().color = new Vector4(255, 255, 255, 255);

    }

    public void setUITitle(int type, int lvl)
    {
        switch (type)
        {
            case 0:
                title.text = "Error, no tile";
                level.text = null;
                break;
            case 1:
                title.text = "Grass";
                level.text = null;
                break;
            case 2:
                title.text = collisionManager.selected.GetComponent<tileManager>().World.waterTitle[lvl];
                level.text = "Water " + (lvl+1);
                break;
            case 3:
                title.text = collisionManager.selected.GetComponent<tileManager>().World.flowerTitle[lvl];
                level.text = "Flower " + (lvl+1);
                break;
            default:
                break;
        }
        

    }


    // Update is called once per frame
    void Update () {
		
	}
}
