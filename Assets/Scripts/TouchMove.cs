using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMove : MonoBehaviour
{
    private float minX, maxX, minY, maxY;
    private float playerRadius;
    float touchTrack = -1;
    public bool player2;
    Rigidbody2D rb;
    public float dashSpeed;

    TouchAssign touchAssign;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CapsuleCollider2D playerCollider = GetComponent<CapsuleCollider2D>();
        //BoxCollider2D playerCollider = GetComponent<BoxCollider2D>();
        playerRadius = playerCollider.bounds.extents.x;


        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));

        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        minX = bottomCorner.x + playerRadius + 0.2f;
        maxX = topCorner.x - playerRadius - 0.2f;
        if (!player2)
        {//PLAYER 1
            minY = bottomCorner.y + playerRadius + 0.2f;
            maxY = 0;
        }
        else
        {//PLAYER2
            minY = 0;
            maxY = topCorner.y - playerRadius - 0.2f;
        }

        Input.multiTouchEnabled = true;

        touchAssign = GameObject.Find("GameController").GetComponent<TouchAssign>();
    }

    // Update is called once per frame
    void LateUpdate()
    {



        if (Input.touchCount > 0)
        {
            if (!player2)
            {
                touchTrack = touchAssign.fingerIdPlayer1;
            }
            else if (player2)
            {
                touchTrack = touchAssign.fingerIdPlayer2;
            }

            if (touchTrack > -1)
            {
                foreach (Touch mtouch in Input.touches)
                {
                    Vector3 touchedPos = Camera.main.ScreenToWorldPoint(mtouch.position) + new Vector3(0, 0, 10);
                    /*  if(touchedPos.y > maxY)
                         return;   */

                    if (mtouch.phase == TouchPhase.Began && mtouch.fingerId == touchTrack)
                    {
                        transform.position = touchedPos;
                    }

                    if (mtouch.phase == TouchPhase.Stationary && mtouch.fingerId == touchTrack || mtouch.phase == TouchPhase.Moved && mtouch.fingerId == touchTrack)
                    {
                        transform.position = touchedPos;
                    }
                    if (mtouch.phase == TouchPhase.Stationary && mtouch.fingerId == touchTrack)
                    {
                        dashSpeed = 1f;
                    }
                    if (mtouch.phase == TouchPhase.Moved && mtouch.fingerId == touchTrack)
                    {
                        dashSpeed = 2.5f;
                    }
                }
            }

            // Horizontal contraint
            if (transform.position.x < minX)
                transform.position = new Vector3(minX, transform.position.y);
            if (transform.position.x > maxX)
                transform.position = new Vector3(maxX, transform.position.y);

            // vertical contraint
            if (transform.position.y < minY)
                transform.position = new Vector3(transform.position.x, minY);
            if (transform.position.y > maxY)
                transform.position = new Vector3(transform.position.x, maxY);
        }


    }

    	/* void OnGUI()
	{
		int w = Screen.width, h = Screen.height;
 
		GUIStyle style = new GUIStyle();
        Rect rect;

        if(player2)
        {
		rect = new Rect(100, 50, w, h * 2 / 100);
        }
        else
        {
		rect = new Rect(100, 500, w, h * 2 / 100);
        }
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = new Color (1, 1, 1, 1.0f);

		GUI.Label(rect, touchTrack.ToString(), style);
	} */
}
