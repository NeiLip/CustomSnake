using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.UI;

public class PauseWindow : MonoBehaviour
{
    
    private static PauseWindow instance;

    public static bool isPaused;

    private void Awake() {
        instance = this;

        transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        transform.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

        isPaused = false;
        Hide();
    }

    private void Show() {
        isPaused = true;
        gameObject.SetActive(true);
    }
    private void Hide() {
        isPaused = false;
        gameObject.SetActive(false);
    }

    public static void ShowStatic() {
        instance.Show();
    }

    public static void HideStatic() {
        instance.Hide();
    }
    //public static bool IsPaused() {
    //    return isPaused;
    //}
}
