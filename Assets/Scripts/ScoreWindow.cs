using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWindow : MonoBehaviour
{
    private static Text[] thoughtTxt;


    private void Awake() {
        thoughtTxt = new Text[18];
        thoughtTxt[0] = transform.Find("ThoughtText1").GetComponent<Text>();
        thoughtTxt[1] = transform.Find("ThoughtText2").GetComponent<Text>();
        thoughtTxt[2] = transform.Find("ThoughtText3").GetComponent<Text>();
        thoughtTxt[3] = transform.Find("ThoughtText4").GetComponent<Text>();
        thoughtTxt[4] = transform.Find("ThoughtText5").GetComponent<Text>();
        thoughtTxt[5] = transform.Find("ThoughtText6").GetComponent<Text>();
        thoughtTxt[8] = transform.Find("ThoughtText7").GetComponent<Text>();
        thoughtTxt[9] = transform.Find("ThoughtText8").GetComponent<Text>();
        thoughtTxt[10] = transform.Find("ThoughtText9").GetComponent<Text>();
        thoughtTxt[11] = transform.Find("ThoughtText10").GetComponent<Text>();
        thoughtTxt[12] = transform.Find("ThoughtText11").GetComponent<Text>();
        thoughtTxt[13] = transform.Find("ThoughtText12").GetComponent<Text>();
        thoughtTxt[14] = transform.Find("ThoughtText13").GetComponent<Text>();
        thoughtTxt[15] = transform.Find("ThoughtText14").GetComponent<Text>();
        thoughtTxt[16] = transform.Find("ThoughtText15").GetComponent<Text>();
        thoughtTxt[17] = transform.Find("ThoughtText16").GetComponent<Text>();
    }

    public static void showThougth(int thoughtNum) {
        if (thoughtNum > 0) {
            if (thoughtNum == 8) {
                thoughtTxt[5].enabled = false;
            }
            else {
                thoughtTxt[thoughtNum - 1].enabled = false;
            }
        }
        thoughtTxt[thoughtNum].enabled = true;
    }

    public static void hideThougth(int thoughtNum) {
        if (thoughtNum > 0) {
            if (thoughtNum == 8) {
                thoughtTxt[5].enabled = true;
            }
            else {
                thoughtTxt[thoughtNum - 1].enabled = true;
            }
        }
        thoughtTxt[thoughtNum].enabled = false;
    }
}
