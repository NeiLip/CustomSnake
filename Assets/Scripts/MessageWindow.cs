using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class MessageWindow : MonoBehaviour
{

    private static MessageWindow instance;


 
    private static Text messageGameObject1;
    private static Text messageGameObject2;


    private void Awake() {
        instance = this;

        transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        transform.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

        messageGameObject1 = transform.Find("MessageTxt1").GetComponent<Text>();
        messageGameObject2 = transform.Find("MessageTxt2").GetComponent<Text>();

        Hide();
    }


    public static void SetMessageText(int textNum) {
        string curText;
        
        switch (textNum) {
            case 0:
                curText = "אל םעפ ףא ינא";
                messageGameObject1.text = curText;

                curText = "םולכב חילצמ";
                messageGameObject2.text = curText;
                break;
            case 1:
                curText = "אל הז חילצא םא";
                messageGameObject1.text = curText;

                curText = "בוט ינאש רמוא";
                messageGameObject2.text = curText;
                break;
            case 2:
                curText = "בוט אל ינא";
                messageGameObject1.text = curText;

                curText = "";
                messageGameObject2.text = curText;
                break;
            case 3:
                curText = "ילע ודיגי המ";
                messageGameObject1.text = curText;

                curText = "חילצא אל םא";
                messageGameObject2.text = curText;
                break;
            case 4:
                curText = "בוט אל השוע ינא";
                messageGameObject1.text = curText;

                curText = "";
                messageGameObject2.text = curText;
                break;
            case 5:
                curText = "הזמ רתוי חילצא אל";
                messageGameObject1.text = curText;

                curText = "";
                messageGameObject2.text = curText;
                break;
            case 6:
                curText = "ירשפא ךא לק אל הז";
                messageGameObject1.text = curText;

                curText = "";
                messageGameObject2.text = curText;
                break;
            case 7:
                curText = "ףוסב חילצמ דחא לכ";
                messageGameObject1.text = curText;

                curText = "";
                messageGameObject2.text = curText;
                break;
            case 8:
                curText = "לוכי ינאש יתעדי";
                messageGameObject1.text = curText;

                curText = "חילצהל";
                messageGameObject2.text = curText;
                break;
            case 9:
                curText = "!בוט רבכ ינא";
                messageGameObject1.text = curText;

                curText = "";
                messageGameObject2.text = curText;
                break;
            case 10:
                curText = "!לוכי ינאש חוטב ינא";
                messageGameObject1.text = curText;

                curText = "";
                messageGameObject2.text = curText;
                break;
            case 11:
                curText = "חילצא אלש בצמ ןיא";
                messageGameObject1.text = curText;

                curText = "";
                messageGameObject2.text = curText;
                break;
            case 12:
                curText = "ץמאתמש ימ";
                messageGameObject1.text = curText;

                curText = "חילצמ ףוסב";
                messageGameObject2.text = curText;
                break;
            case 13:
                curText = "חצנמ ינא חוטב";
                messageGameObject1.text = curText;

                curText = "";
                messageGameObject2.text = curText;
                break;
            case 14:
                curText = "האמחמ לבקל ףיכ";
                messageGameObject1.text = curText;

                curText = "םיחצנמשכ";
                messageGameObject2.text = curText;
                break;
            case 15:
                curText = "!חצנאש יתעדי";
                messageGameObject1.text = curText;

                curText = "";
                messageGameObject2.text = curText;
                break;
            default:
                curText = "";
                messageGameObject1.text = curText;

                curText = "";
                messageGameObject2.text = curText;
                break;
        }
    }


    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }

    public static void ShowStatic() {
        instance.Show();
    }

    public static void HideStatic() {
        instance.Hide();
    }
}
