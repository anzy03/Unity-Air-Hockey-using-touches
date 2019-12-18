using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAssign : MonoBehaviour
{
    public float fingerIdPlayer1 = -1, fingerIdPlayer2 = -1;
    Vector3 touchedPos;



    // Update is called once per frame
    void Update()
    {


        if (Input.touchCount > 0)
        {

            foreach (Touch touch in Input.touches)
            {

                touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0)) + new Vector3(0, 0, 10);
                //Player 2 Assigining
                if (touchedPos.y > 0 && touch.phase == TouchPhase.Began)
                {
                    fingerIdPlayer2 = touch.fingerId;
                }
                else if (touch.phase == TouchPhase.Ended && touch.fingerId == fingerIdPlayer2)
                {
                    fingerIdPlayer2 = -1;
                }
                //Player1 Assining
                if (touchedPos.y < 0 && touch.phase == TouchPhase.Began)
                {
                    fingerIdPlayer1 = touch.fingerId;
                }
                else if (touch.phase == TouchPhase.Ended && touch.fingerId == fingerIdPlayer1)
                {
                    fingerIdPlayer1 = -1;
                }
            }
        }

    }
}
