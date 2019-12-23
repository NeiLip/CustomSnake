using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class OnMouseButton : MonoBehaviour
{

    public GameObject button;

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown() {
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        switch (button.name) {
            case "mainMenuBtn": Loader.Load(Loader.Scene.MainMenu);
                break;
            case "retryBtn":
                Loader.Load(Loader.Scene.GameScene);
                break;
            case "resumeBtn":
                GameHandler.ResumeGame();
                break;
            default:
                break;
        }
    }

    private void OnMouseEnter() {
     //   Debug.Log("Mouse Over!!!sfddsf");
        button.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnMouseExit() {
        button.GetComponent<SpriteRenderer>().enabled = false;
    }
}


