using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAnimator : MonoBehaviour {

    public Transform uiController;
    

    public int panelStatus = 0; //0 = panel hidden; 1= pannel being opened;  2 = pannel being closed; 3 = panel is open;
    public float panelSpeed = 165;
    public int panelSize = 12;

    public Vector2 panelHideLocation;
    public Vector3 CurrentLocation;

    public bool switchText = false; //used to switch the text on only when it starts movine the panel

    // Use this for initialization
    void Start () {
        init();
	}

    public void init() {
        panelHideLocation = transform.GetComponent<RectTransform>().localPosition;
        uiController = GameObject.FindGameObjectWithTag("UiController").transform;
    }

    public void animate(int newStatus, float speed)
    {
        panelSpeed = speed;
        panelStatus = newStatus;
    }
    public void animate(int newStatus)
    {
        panelStatus = newStatus;
    }

    // Update is called once per frame
    void Update () {
        CurrentLocation = transform.GetComponent<RectTransform>().localPosition;
        switch (panelStatus)
        {
            case 0:
               // transform.GetComponent<RectTransform>().position = new Vector3(panelHideLocation.x, CurrentLocation.y, CurrentLocation.z);
                break;
            case 3:
               // transform.GetComponent<RectTransform>().position = new Vector3(panelHideLocation.x - panelSize, CurrentLocation.y, CurrentLocation.z);
                break;
            case 1:
                if (panelHideLocation.x - CurrentLocation.x < panelSize) // panel is now being opened from nothing
                {
                    if (!switchText)
                    { //switch on the text to the appropriate tile's information
                        this.GetComponent<panelUI>().setUITitle();
                        switchText = true;
                    }
                    float moveAmount =panelSpeed * 100 * Time.deltaTime;
                    transform.GetComponent<RectTransform>().localPosition = new Vector3(CurrentLocation.x - moveAmount, CurrentLocation.y, CurrentLocation.z);
                }
                else
                {
                    switchText = false;
                    transform.GetComponent<RectTransform>().localPosition = new Vector3(panelHideLocation.x - panelSize, CurrentLocation.y, CurrentLocation.z);
                    panelStatus = 3;
                }
                break;
            case 2:
                if (panelHideLocation.x - CurrentLocation.x > 0) //panel is being closed from being open
                {
                    float moveAmount = panelSpeed * 100 * Time.deltaTime;
                    transform.GetComponent<RectTransform>().localPosition = new Vector3(CurrentLocation.x + moveAmount, CurrentLocation.y, CurrentLocation.z);
                }
                else
                {
                    transform.GetComponent<RectTransform>().localPosition = new Vector3(panelHideLocation.x, CurrentLocation.y, CurrentLocation.z);
                    uiController.GetComponent<gamePlayUI>().setDetailButtonActive(true);
                    this.GetComponent<panelUI>().title.text = null;
                    this.GetComponent<panelUI>().level.text = null;
                    panelStatus = 0;
                }
                break;
        }
    }
}
