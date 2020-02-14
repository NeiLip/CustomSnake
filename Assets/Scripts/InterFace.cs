using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.UI;

public class InterFace : MonoBehaviour
{

    
    public Dropdown lenDrop;
    public Dropdown startSizeDrop;


    
    public static int chosenLen;
    public static int chosenSize;




    private void Awake() {
        lenDrop.value = chosenLen;
        startSizeDrop.value = chosenSize;

        //When Clicing Play on main menu
        transform.Find("toMenuBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {

            StartCoroutine(MyCoroutine(Loader.Scene.MainMenu));

        };

      

        //When Clicing Quit on main menu
        transform.Find("quitBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            Application.Quit();
        };

    }


    private IEnumerator MyCoroutine(Loader.Scene scene) {

        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        // yield return new WaitForSeconds(.15f);
        yield return null;
        Loader.Load(scene);
    }

    private void Update() {
        chosenLen = lenDrop.value;
        chosenSize = startSizeDrop.value;

      
    }


}
