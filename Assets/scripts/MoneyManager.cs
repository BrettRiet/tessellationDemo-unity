using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour {

    public int startingMoney = 100;
    public int totalHoneyEarned;
    public int totalHoneySpent;
    public int currentHoney;



	// Use this for initialization
	void Start () {
        init();
	}

    public void init()
    {
        RESETMONEY();
    }

    public void RESETMONEY()
    {
        PlayerPrefs.SetInt("FIRSTGAME", 1);
        currentHoney = startingMoney;
        totalHoneySpent = 0;
        totalHoneyEarned = startingMoney;
        updateCount();
    }
    void updateCount()
    {
        PlayerPrefs.SetInt("CURRENTHONEY", currentHoney);
        PlayerPrefs.SetInt("TOTALHONEYEARNED", totalHoneyEarned);
        PlayerPrefs.SetInt("TOTALHONEYSPENT", totalHoneySpent);
        GameObject.FindGameObjectWithTag("UiController").GetComponent<gamePlayUI>().UpdateHoneyCounter();
    }

   public void depositHoney(int amount)
    {
        currentHoney += amount;
        totalHoneyEarned += amount;
        updateCount();
    }

   public bool spendHoney(int amount) //returns true if succsesful, false if the funds are too small
    {
        if (amount <= currentHoney)
        {
            currentHoney -= amount;
            totalHoneySpent += amount;
            updateCount();
            return true;
        }
        else
            return false;
    }
     
	// Update is called once per frame
	void Update () {
		
	}
}
