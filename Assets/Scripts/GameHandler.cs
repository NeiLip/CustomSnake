
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;
using System.IO;

public class GameHandler : MonoBehaviour {
     
    private static GameHandler instance;
    private static int score;
    [SerializeField] private Snake snake;

    private LevelGrid levelGrid;

    private static int curProgPart;
    private static GameObject progPart;
    private static GameObject progPartChild;


    public static int difficaltyIndex;
    private static int difficalty;
    public static int lengthIndex;
    private static int length;
    public static int startSize;


    private void Awake() {
        instance = this;
        InitializeStatic();

        GameHandler.ResumeGame();
    }

    private void Start() {
        Debug.Log("GameHandler.Start");
       
        levelGrid = new LevelGrid(20, 20);

        snake.Setup(levelGrid);
        levelGrid.Setup(snake);
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (IsGamePause()) {
                GameHandler.ResumeGame();
            }
            else {
                GameHandler.PauseGame();
            }
        }
    }

    private static void InitializeStatic() {
        score = 0;
        curProgPart = 1;
        numOfPrizesGot = 0;

        locationsY.Add(4);
        locationsY.Add(6);
        locationsY.Add(8);
        locationsY.Add(10);
        locationsY.Add(12);
        locationsY.Add(14);
        locationsY.Add(16);


        if (lengthIndex == 0) {
            length = 20;
        }
        else if (lengthIndex == 1) {
            length = 10;
        }
        else if (lengthIndex == 2) {
            length = 30;
        }


       
    }

    public static int GetScore() {
        return score;
    }

    // Add Score and add Progress Bar if needed
    public static void AddScore() {
        score += 10;
        if (score % length == 0) {
            if (curProgPart < 19) {
                string strProgPart = "progPart" + curProgPart.ToString();
                progPart = GameObject.Find(strProgPart);
                progPart.GetComponent<SpriteRenderer>().enabled = true;

                //// Show emoji symbols on progress bar - CANCELED
                //if (curProgPart == 1 || curProgPart == 3 || curProgPart == 5 || curProgPart == 11 || curProgPart == 14 || curProgPart == 18) { 
                //    string strProgPartChild = "emoji" + curProgPart.ToString();
                //    progPartChild = GameObject.Find(strProgPartChild);
                //    progPartChild.GetComponent<SpriteRenderer>().enabled = true;
                //}

                curProgPart++;               
            }
        }
    }

    // Add Score and reduce Progress Bar if needed
    public static void ReduceScore() {
        score -= 10;
        if (curProgPart > 1)  {
            curProgPart--;
            string strProgPart = "progPart" + curProgPart.ToString();
            progPart = GameObject.Find(strProgPart);
            progPart.GetComponent<SpriteRenderer>().enabled = false;

			//// Show emoji symbols on progress bar - CANCELED
			//if (curProgPart == 1 || curProgPart == 3 || curProgPart == 5 || curProgPart == 11 || curProgPart == 14 || curProgPart == 18) {
   //             string strProgPartChild = "emoji" + curProgPart.ToString();
   //             progPartChild = GameObject.Find(strProgPartChild);
   //             progPartChild.GetComponent<SpriteRenderer>().enabled = false;
   //         }
        }
        
    }

    private static List<Vector2Int> prizesLocations = new List<Vector2Int>();
    public static int numOfPrizesGot;
    private static GameObject curPriseGameObject;
    public static List<int> locationsY = new List<int>();


    // If snake ate prise, and there is place on shelf, GotProse is used
    // GotPrise gets prise number and place a sprite of it on the shelf
    public static void GotPrise(int whichPrise) {

        //// START <<<<<<---------------##############<<<<<<<<<<<<<<<<<<<<<

        //Vector2Int prisePosition = new Vector2Int(Random.Range(-2, -4), Random.Range(2, 17));
        //bool goodLoc = false;
        //bool soFarSoGood;

        //while (goodLoc == false) {

        //    prisePosition = new Vector2Int(Random.Range(-2, -4), Random.Range(2, 17));
        //    soFarSoGood = true;
        //    for (int i = 0; i < numOfPrizesGot; i++) {
        //        //Check if in the same row
        //        if (prizesLocations[i].y == prisePosition.y || prizesLocations[i].y == prisePosition.y + 1 ||
        //                                                       prizesLocations[i].y == prisePosition.y - 1) {
        //            soFarSoGood = false;
        //            break;
        //        }
        //    }
        //    if (soFarSoGood == true) {
        //        goodLoc = true;
        //    }
        //}
        //prizesLocations.Add(prisePosition);

        ////   THE CODE ABOVE (commented) DO THE SAME AS THE CODE BELOW (real code, uncommented) <<<<<<---------------##############<<<<<<<<<<<<<<<<<<<<<

        int rnd = Random.Range(0, locationsY.Count);

        Vector2Int prisePosition = new Vector2Int(-3, locationsY[rnd]);

        locationsY.RemoveAt(rnd);

        ///// END <<<<<<---------------##############<<<<<<<<<<<<<<<<<<<<<




        if (whichPrise == 1) {
            curPriseGameObject = new GameObject("Food", typeof(SpriteRenderer));
            curPriseGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.priseSprite1;
            curPriseGameObject.transform.position = new Vector3(prisePosition.x, prisePosition.y);
        }
        if (whichPrise == 2) {
            curPriseGameObject = new GameObject("Food", typeof(SpriteRenderer));
            curPriseGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.priseSprite2;
            curPriseGameObject.transform.position = new Vector3(prisePosition.x, prisePosition.y);
        }
        if (whichPrise == 3) {
            curPriseGameObject = new GameObject("Food", typeof(SpriteRenderer));
            curPriseGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.priseSprite3;
            curPriseGameObject.transform.position = new Vector3(prisePosition.x, prisePosition.y);
        }

        numOfPrizesGot++;
     
    }

  
    public static int GetCurProgPart() {
        return curProgPart;
    }
    public static void SnakeDied() {
        GameOverWindow.ShowStatic();
    }

    public static void ResumeGame() {
        PauseWindow.HideStatic();
        Time.timeScale = 1f;
    }
    public static void PauseGame() {
        PauseWindow.ShowStatic();
        Time.timeScale = 0f;
    }

    public static bool IsGamePause() {
        return Time.timeScale == 0f;
    }
}
