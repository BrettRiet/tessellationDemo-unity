using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class collisionManager : MonoBehaviour {

    public Ray2D clickRay;

    public bool notMoved = false;
    public bool singleFinger = false;

    private Vector2 touchPos;
    public static GameObject selected;

    public Transform uiController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_IOS
    touchController();
#elif UNITY_EDITOR
        mouseController();
#elif UNITY_ANDROID
    touchController();
#endif
    }

    void mouseController() //if the player is useing a computer it uses this function
    {
        if (EventSystem.current.IsPointerOverGameObject()){
            return;
        }
        RaycastHit2D hitInfo;
        hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hitInfo.collider != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (uiController.GetComponent<gamePlayUI>().getPanelsOpen() > 0)
                {
                    uiController.GetComponent<gamePlayUI>().closeDetails();
                    return;
                }
                if (selected != null)
                {
                    if (selected == hitInfo.collider.gameObject)
                    {
                        uiController.GetComponent<gamePlayUI>().openDetails();
                        return;
                    }
                    else
                    {
                        selected.GetComponent<tileManager>().deselect();
                    }
                }
                selected = hitInfo.collider.gameObject;

                selected.GetComponent<tileManager>().select();
              
            }
        }
    }
   

    void touchController()
    {
       
        if (Input.touchCount > 1)
            singleFinger = false;
        if (Input.touchCount == 0)
            singleFinger = true;
        if (Input.touchCount == 1 && singleFinger)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                notMoved = true;
                touchPos = Input.GetTouch(0).position;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if ((Vector2.Distance(touchPos, Input.GetTouch(0).position)) > 18)
                {
                    notMoved = false;
                }
            }

            RaycastHit2D hitInfo;
            hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);

            if (hitInfo.collider != null)
            {
                if (notMoved && Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    if (uiController.GetComponent<gamePlayUI>().getPanelsOpen() > 0)
                    {
                        uiController.GetComponent<gamePlayUI>().closeDetails();
                        return;
                    }
                    if (selected != null)
                    {
                        if (selected == hitInfo.collider.gameObject)
                        {
                            uiController.GetComponent<gamePlayUI>().openDetails();
                            notMoved = false;
                            return;
                        }
                        else
                        {
                            selected.GetComponent<tileManager>().deselect();
                        }
                    }

                    selected = hitInfo.collider.gameObject;

                    selected.GetComponent<tileManager>().select();

                    notMoved = false;
                }
            }

        }
    }

       
}
