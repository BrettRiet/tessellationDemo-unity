using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this will manage all UI for now

public class gamePlayUI : MonoBehaviour {

    public Button detailBtn; //open detailos of current tile
    public Button closeDetailsBtn;  //close the details
    public Transform detailPanel;
    public Transform confirmationPage;

    public Text Honey_Count;

    public int panelsOpen = 0;
    
	// Use this for initialization
	void Start () {
        init();
	}
    public void init()
    {
        detailBtn.onClick.AddListener(openDetails);
        closeDetailsBtn.onClick.AddListener(closeDetails);
        detailBtn.gameObject.SetActive(false);
        
    }

   

    public void setPanelsOpen(int val)
    {
        panelsOpen += val;
    }
    public int getPanelsOpen()
    {
        return panelsOpen;
    }

   public void openDetails()
    {
        setDetailButtonActive(false);
        setPanelsOpen(1);
        detailPanel.GetComponent<PanelAnimator>().animate(1);
    }
   public void closeDetails()
    {
        setPanelsOpen(-1);
        detailPanel.GetComponent<PanelAnimator>().animate(2);
    }

    public void setDetailButtonActive(bool val)
    {
        detailBtn.gameObject.SetActive(val);
    }

    public void UpdateHoneyCounter()
    {
        Honey_Count.text = PlayerPrefs.GetInt("CURRENTHONEY").ToString();
    }

    // Update is called once per frame
    void Update ()
    {
      
    }
}
