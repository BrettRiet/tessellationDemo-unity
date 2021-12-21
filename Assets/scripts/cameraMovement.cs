using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraMovement : MonoBehaviour {

    //settings to use for the mouse control movement
    private Vector3 dragOrigin;
    private Vector3 difference;
    private bool drag = false;

    //the zoom settings, 
    public float zoom; //current zoom will be specified on init()
    public float minZoom = 4.5f;
    public float maxZoom = 14f;

    //touch zoom variables
    public bool zooming = false;
    public float distance = 0;
    public float startDistance;

    public bool startmultitouch = false;
    public bool starttouch = false;
    public bool notouch = false;
    public int mytouchCount = 0;
    public Vector2 touchminus;
    public Touch touch0;
    public Touch touch1;

    public GameObject[] touchCounters;



    private void Start()
    {
        init();

    }
    public void init()
    {
        zoom = Camera.main.orthographicSize;

    }

    void Update()
    {
#if UNITY_IOS
    touchController();
#elif UNITY_EDITOR
    mouseController();
#elif UNITY_ANDROID
    touchController();
#endif

    }

    void mouseController()
    {
        //clickAndDrag controlls
        if (Input.GetMouseButtonDown(2))
        {
            if (!drag)
            {
                dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                drag = true;
            }

        }
        if (!Input.GetMouseButton(2))
            drag = false;
        if (drag)
        {
            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.position = dragOrigin - difference;
        }

        //ZoomControls
        float wheel = (Input.GetAxis("Mouse ScrollWheel"));
        if (wheel > 0)
        {
            if (zoom > minZoom)
            {
               // Debug.Log("positive");

                zoom -= wheel*10;
                Camera.main.orthographicSize = zoom;
            }
        }
        if (wheel < 0)
        {
            if (zoom < maxZoom)
            {
                zoom -= wheel*10;
                Camera.main.orthographicSize = zoom;
            }
        }

    } //for PC or MAC
    /*
    void touchController()    //for phones/Tablets
    {
        if (Input.touchCount == 3)
        {
            drag = false;
            notouch = true;
        }
        if (!notouch)
        {
            if (Input.touchCount == 2)
            {

                if (startmultitouch == false)
                {
                    touch0 = Input.GetTouch(0);
                    touch1 = Input.GetTouch(1);
                    starttouch = true;
                    startmultitouch = true;
                }
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                if (starttouch == false)
                {
                    touchOne = Input.GetTouch(0);
                    touchZero = Input.GetTouch(1);

                }


                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                distance = prevTouchDeltaMag - touchDeltaMag;

                if (distance > 0)
                {
                    if (zoom < maxZoom)
                    {
                        zoom += distance * .01f;
                        zoom = Mathf.Min(zoom, maxZoom);
                        Camera.main.orthographicSize = zoom;
                    }
                }
                if (distance < 0)
                {
                    if (zoom > minZoom)
                    {
                        zoom += distance * .01f;
                        zoom = Mathf.Max(zoom, minZoom);
                        Camera.main.orthographicSize = zoom;
                    }
                }
                if (touchZero.phase == TouchPhase.Ended)
                {
                    if (!starttouch)
                    {
                        starttouch = true;
                    }

                    drag = false;
                    return;
                }
                if (touchZero.phase == TouchPhase.Ended)
                {
                    if (starttouch)
                    {
                        starttouch = false;
                    }
                
                    drag = false;
                    return;
                }
            }

            if (Input.touchCount == 1)
            {

                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    if (!drag)
                    {
                        dragOrigin = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        drag = true;
                    }
                }
            }
        }
        if (Input.touchCount == 0)
        {
            notouch = false;
            drag = false;
            starttouch = false;
        }
        if (drag) {
            difference = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - transform.position;
            transform.position = dragOrigin - difference;
        }

        //ZoomControls
       

      

    }
    */

    void touchController()
    {
        if (Input.touchCount == 0)
        {
            notouch = false;
            drag = false;
            starttouch = false;
            mytouchCount = 0;
            touchminus = new Vector2(0,0);
        }
        if (Input.touchCount == 1)
        {
            if (notouch)
            {
                Debug.Log("Touch Zero: " + Input.GetTouch(0).position);
                if (!drag)
                {
                    dragOrigin = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position- touchminus);
                    drag = true;
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                mytouchCount += 1;
                dragOrigin = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (!drag)
                {
                    dragOrigin = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    drag = true;
                }
            }
        }

        if (Input.touchCount == 2)
        {
           
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                mytouchCount += 1;
            }
            if (Input.GetTouch(1).phase == TouchPhase.Began)
            {
                mytouchCount += 1;
            }
            if (Input.GetTouch(1).phase == TouchPhase.Ended)
            {
                notouch = false;
                drag = false;
                touchminus = new Vector2(0, 0);
                return;

            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                notouch = true;
                drag = false;
                touchminus = Input.GetTouch(1).position - Input.GetTouch(0).position; 
                Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
            
                
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            if (notouch)
            {
                touchZero = Input.GetTouch(1);
                touchOne = Input.GetTouch(0);
            }
        
            ////////I NEED TO SUBTRACT THE TOUCH POSITON AND STORE 


            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;


            Debug.Log("Touch Zero: " + touchZero.position + "Touch One" + touchOne.position);
            Debug.Log("Touch Zero previous: " + touchZeroPrevPos + "Touch One previous" + touchOnePrevPos);

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            distance = prevTouchDeltaMag - touchDeltaMag;

            if (distance > 0)
            {
                if (zoom < maxZoom)
                {
                    zoom += distance * .01f;
                    zoom = Mathf.Min(zoom, maxZoom);
                    Camera.main.orthographicSize = zoom;
                }
            }
            if (distance < 0)
            {
                if (zoom > minZoom)
                {
                    zoom += distance * .01f;
                    zoom = Mathf.Max(zoom, minZoom);
                    Camera.main.orthographicSize = zoom;
                }
            }

        }



        if (drag)
        {
            difference = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position- touchminus) - transform.position;
            transform.position = dragOrigin - difference;
        }

    }
}
