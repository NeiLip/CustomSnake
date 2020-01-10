
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;
using System.IO;
using UnityEngine.UI;

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

    private static int curMessage;

    private static bool lostGame;

  //  private static Text curMessageText;


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

        if (Input.GetKeyDown(KeyCode.Escape) && !lostGame) {
            if (IsGamePause()) {
                GameHandler.ResumeGame();
            }
            else {
                GameHandler.PauseGame();
            }
        }

    }

    private static void InitializeStatic() {
        difficaltyIndex = InterFace.chosenDif;
        lengthIndex = InterFace.chosenLen;
        startSize = InterFace.chosenSize;

        lostGame = false;

        score = 0;
        curProgPart = 1;
        numOfPrizesGot = 0;

        curMessage = 1;

        particleSys = GameObject.Find("MaxPartSys");

        locationsY.Add(16);
        locationsY.Add(14);
        locationsY.Add(12);
        locationsY.Add(10);
        locationsY.Add(8);
        locationsY.Add(6);
        locationsY.Add(4);
        
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
       
        //Progress bar is on max
        if (curProgPart >= 18) {
            PlayParticleOnMax();
        }

        if (score % length == 0) {
            if (curProgPart < 19) {
                string strProgPart = "progPart" + curProgPart.ToString();
                progPart = GameObject.Find(strProgPart);
                progPart.GetComponent<SpriteRenderer>().enabled = true;

                if (curProgPart != 7 ) {
                    if (curProgPart != 8) {

                        if (curProgPart > 8) {
                            GameSound.PlayMessage(curProgPart - 3);
                            MessageWindow.SetMessageText(curProgPart - 3);
                        }
                        else {
                            GameSound.PlayMessage(curProgPart - 1);
                            MessageWindow.SetMessageText(curProgPart - 1);
                        }
                       
                        ScoreWindow.showThougth(curProgPart - 1);
                      //  MessageWindow.SetMessageText(curMessage);
                        curMessage++;
                        PauseGameForMessage();
                    }
                }

                curProgPart++;

                
            }
        }
    }

    // Add Score and reduce Progress Bar if needed
    public static void ReduceScore() {
        score -= 10;
        if (curProgPart > 1)  {
            curProgPart--;
            curMessage--;
            string strProgPart = "progPart" + curProgPart.ToString();
            progPart = GameObject.Find(strProgPart);
            progPart.GetComponent<SpriteRenderer>().enabled = false;

            if (curProgPart != 7) {
                if (curProgPart != 8) {
                    ScoreWindow.hideThougth(curProgPart - 1);
                }
            }
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


    public static void PlayParticleOnMax() {
        ParticleSystem currPS = particleSys.GetComponent<ParticleSystem>();
        
        currPS.Play();
    }

  
    public static int GetCurProgPart() {
        return curProgPart;
    }
    public static void SnakeDied() {
        lostGame = true;
        GameOverWindow.ShowStatic();
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
