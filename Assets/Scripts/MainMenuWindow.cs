﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class MainMenuWindow : MonoBehaviour {




    private enum Sub {
        Main,
        HowToPlay,
        About,
    }


    private void Awake() {


        
        //Placing Menu window and HowToPlay window at 0,0
        transform.Find("howToPlaySub").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        transform.Find("mainSub").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        transform.Find("aboutSub").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        //When Clicing Play on main menu
        transform.Find("mainSub").Find("playBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            
     
            SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
            Loader.Load(Loader.Scene.GameScene);

        };

        //When Clicing HowToPlay on main menu
        transform.Find("mainSub").Find("howToPlayBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
            ShowSub(Sub.HowToPlay);
        };
        //When Clicing About on main menu
        transform.Find("mainSub").Find("aboutBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
            ShowSub(Sub.About);
        };

        //When Clicing Quit on main menu
        transform.Find("mainSub").Find("quitBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
            Application.Quit();
        };

        //When Clicing BackToManu on HowToPlay menu
        transform.Find("howToPlaySub").Find("backBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
            ShowSub(Sub.Main);
        };
        //When Clicing BackToManu on AboutSub menu
        transform.Find("aboutSub").Find("backBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
            ShowSub(Sub.Main);
        };

        //When Clicing Interface on main menu
        transform.Find("mainSub").Find("interfaceBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
            Loader.Load(Loader.Scene.Interface);
          
        };


        ShowSub(Sub.Main);
    }


    private void ShowSub(Sub sub) {
        transform.Find("mainSub").gameObject.SetActive(false);
        transform.Find("howToPlaySub").gameObject.SetActive(false);
        transform.Find("aboutSub").gameObject.SetActive(false);

        switch (sub) {
            case Sub.Main:
                transform.Find("mainSub").gameObject.SetActive(true);
                break;
            case Sub.HowToPlay:
                transform.Find("howToPlaySub").gameObject.SetActive(true);
                break;
            case Sub.About:
                transform.Find("aboutSub").gameObject.SetActive(true);
                break;
        }


    }
}