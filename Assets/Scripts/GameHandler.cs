
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;
using System.IO;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class GameHandler : MonoBehaviour {
     
    private static GameHandler instance;
    private static int score;
    [SerializeField] private Snake snake;

    private LevelGrid levelGrid;

    public static int curProgPart;
    private static GameObject progPart;
    private static GameObject progPartChild;

    public static GameObject particleSys;


    public static int difficaltyIndex;
    public static int lengthIndex;
    private static int length;
    public static int startSize;

    public static int prizesLeftToWin;
    public static bool isPrizesSpwaned;




    private static bool lostGame;

  


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
            if (!lostGame) {
                if (IsGamePause()) {
                    GameHandler.ResumeGame();
                }
                else {
                    GameHandler.PauseGame();
                }
            }
        }

    }

    private static void InitializeStatic() {


        
        lengthIndex = InterFace.chosenLen;
        startSize = InterFace.chosenSize;

        lostGame = false;
        isPrizesSpwaned = false;

        score = 0;
        curProgPart = 1;
        numOfPrizesGot = 0;
        prizesLeftToWin = 2;

        particleSys = GameObject.Find("MaxPartSys");

        locationsY.Add(16);
        locationsY.Add(14);
        locationsY.Add(12);
        locationsY.Add(10);
        locationsY.Add(8);
        locationsY.Add(6);
        locationsY.Add(4);
        
        if (lengthIndex == 0) {
            length = 25;
        }
        else if (lengthIndex == 1) {
            length = 35;
        }
        else if (lengthIndex == 2) {
            length = 45;
        } 
    }


 


    public static int GetScore() {
        return score;
    }

    public static void ReduceScore(int sc) {
        score -= sc;
    }



    public static void AddScore(int sc) {
        score += sc;
    }


    // Add Score and add Progress Bar if needed
    public static void AddProgressBar() {

        Debug.Log("Score: " + score);

  
        if (score > length) {
            if (curProgPart < 17) {
                

                progPart = GameObject.Find(curProgPart.ToString());
                progPart.GetComponent<SpriteRenderer>().enabled = true;

                GameSound.PlayMessage(curProgPart - 1); //Playing audio message
                MessageWindow.SetMessageText(curProgPart - 1); //Setting text message

             //   ScoreWindow.showThougth(curProgPart - 1); //Showing text message
                        
                PauseGameForMessage();

                
                curProgPart++;
            }
            score = 0;
        }
    }


    // Add Score and reduce Progress Bar if needed
    public static void ReduceProgressBar() {
        if (curProgPart > 1)  {
            curProgPart--;
            progPart = GameObject.Find(curProgPart.ToString());
            progPart.GetComponent<SpriteRenderer>().enabled = false;

         
            //ScoreWindow.hideThougth(curProgPart - 1); //Text on progress bar. I dont use it
         
        }
    }
        
    

    private static List<Vector2Int> prizesLocations = new List<Vector2Int>();
    public static int numOfPrizesGot;
    private static GameObject curPrizeGameObject;
    public static List<int> locationsY = new List<int>();


    // If snake ate prise, and there is place on shelf, GotProse is used
    // GotPrise gets prise number and place a sprite of it on the shelf
    public static void PutPrizeOnShelf(int curPrize) {

        if (numOfPrizesGot < 7) {
            int rnd = Random.Range(0, locationsY.Count);

            Vector2Int prisePosition = new Vector2Int(-3, locationsY[numOfPrizesGot]);

            // locationsY.RemoveAt(0);

            if (curPrize == 1) {
                curPrizeGameObject = new GameObject("Food", typeof(SpriteRenderer));
                curPrizeGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.priseSprite1;
                curPrizeGameObject.transform.position = new Vector3(prisePosition.x, prisePosition.y);
            }
            if (curPrize == 2) {
                curPrizeGameObject = new GameObject("Food", typeof(SpriteRenderer));
                curPrizeGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.priseSprite2;
                curPrizeGameObject.transform.position = new Vector3(prisePosition.x, prisePosition.y);
            }
            if (curPrize == 3) {
                curPrizeGameObject = new GameObject("Food", typeof(SpriteRenderer));
                curPrizeGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.priseSprite3;
                curPrizeGameObject.transform.position = new Vector3(prisePosition.x, prisePosition.y);
            }

            numOfPrizesGot++;
        }
    }

  
    public static int GetCurProgPart() {
        return curProgPart;
    }
    public static void SnakeDied() {
        lostGame = true;
        GameOverWindow.ShowStatic();
    }

    public static void OnWin() { // I'm not using it yet
        lostGame = true;
        WinningWindow.ShowStatic();
    }

    public static void ResumeGame() {
        MessageWindow.HideStatic();
        PauseWindow.HideStatic();
        Time.timeScale = 1f;
    }
    public static void PauseGame() {
        PauseWindow.ShowStatic();
        Time.timeScale = 0f;
    }


    public static void PauseGameForMessage() {
        MessageWindow.ShowStatic();
        Time.timeScale = 0f;
    }

    public static void ResumeGameForMessage() {
        MessageWindow.HideStatic();
        Time.timeScale = 1f;
    }

    public static bool IsGamePause() {
        return Time.timeScale == 0f;
    }

    
   
}
