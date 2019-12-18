using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Goals : MonoBehaviour
{
    GameObject dice;
    public TextMeshProUGUI PointText;
    public TextMeshProUGUI PausePointText;
    int redPoints;
    int bluePoints;

    void Start()
    {
        PointText.SetText("0");
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Dice")
        {
            dice = col.gameObject;
            dice.SetActive(false);
            if(gameObject.name =="BlueGoal")
            {
                bluePoints++;
                Debug.Log("BluePoint = "+ bluePoints);
                PointText.SetText(bluePoints.ToString());
                PausePointText.SetText(bluePoints.ToString());
            }
            if(gameObject.name =="RedGoal")
            {
                redPoints++;
                Debug.Log("RedPoint = "+ redPoints);
                PointText.SetText(redPoints.ToString());
                PausePointText.SetText(redPoints.ToString());
            }
            dice.transform.position = new Vector2(0, 0);
            StartCoroutine(WaitTime(1));
            //dice.SetActive(true);
        }
    }

    private IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);

        dice.SetActive(true);

    }
}
