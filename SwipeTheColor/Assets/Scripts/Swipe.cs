using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private bool tap, swipeLeftUp, swipeRightUp, swipeRightDown, swipeLeftDown;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;

    public GameObject slideEffect;

    private void Update()
    {
       
        tap = swipeLeftUp = swipeRightUp = swipeRightDown = swipeLeftDown = false;
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            slideEffect.active = true;
            
            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            slideEffect.active = false;
            isDraging = false;
            Reset();
        }

        #endregion
        #region Mobile Inputs
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                slideEffect.active = true;
               
                tap = true;
                isDraging = true;
                startTouch = Input.touches[0].position;

            }
            else if(Input.touches[0].phase== TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                slideEffect.active = false;
                isDraging = false;
                Reset();
            }
        }
        #endregion
        //Calculate the distance
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            
           
            if (Input.touches.Length > 0)
            {   
                slideEffect.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[Input.touchCount - 1].position.x, Input.touches[Input.touchCount - 1].position.y, 1));
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                slideEffect.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,1));
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }
        //Did we cross the deadzone? 
        if (swipeDelta.magnitude > 100)
        {
            //Which direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if(x>0 && y > 0)
            {
                swipeRightUp = true;
            }
            if(x>0 && y < 0)
            {
                swipeRightDown = true;
            }
            if(x<0 && y > 0)
            {
                swipeLeftUp = true;
            }
            if(x<0 && y < 0)
            {
                swipeLeftDown = true;
            }
            
            
            Reset();
        }
    }

    private void Reset()
    {
        startTouch =swipeDelta= Vector2.zero;
        isDraging = false;
    }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
	public bool SwipeLeftUp { get { return swipeLeftUp; } }
    public bool SwipeRigthUp{ get { return swipeRightUp; } }
    public bool SwipeRightDown{ get { return swipeRightDown; } }
    public bool SwipeLeftDown { get { return swipeLeftDown; } }
}
