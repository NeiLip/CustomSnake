
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using System.IO.IsolatedStorage;

public class LevelGrid {

    private Vector2Int[] foodGridPositionArr;
    private GameObject[] foodGameObjectArr;
    private int[] foodTimerArr;
    

    private Vector2Int[] badFoodGridPositionArr;
    private GameObject[] badFoodGameObjectArr;
    private int[] badFoodTimerArr;

    private bool stopSpawningFoods;
    private Vector2Int[] prizeGridPositionArr;
    private GameObject[] prizeGameObjectArr;
  

    private static int lastPrize;
    private int width;
    private int height;
    private Snake snake;

    public LevelGrid(int width, int height) {
        this.width = width;
        this.height = height;
    }

    public void Setup(Snake snake) {
        foodGridPositionArr = new Vector2Int[2];
        foodGameObjectArr = new GameObject[2];

        badFoodGridPositionArr = new Vector2Int[2];
        badFoodGameObjectArr = new GameObject[2];

        prizeGridPositionArr = new Vector2Int[2];
        prizeGameObjectArr = new GameObject[2];

        foodTimerArr = new int[2];
        badFoodTimerArr = new int[2];

        stopSpawningFoods = false;


        this.snake = snake;
        SpawnFood0();
        SpawnFood1();
        foodTimerArr[0] = 45;
        foodTimerArr[1] = 15;

        SpawnBad0();
        SpawnBad1();

        badFoodTimerArr[0] = 30;
        badFoodTimerArr[1] = 20;

    }

    private void SpawnFood0() {
        do {
            foodGridPositionArr[0] = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPositionArr[0]) != -1);

        foodGameObjectArr[0] = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObjectArr[0].GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
        foodGameObjectArr[0].transform.position = new Vector3(foodGridPositionArr[0].x, foodGridPositionArr[0].y);
    }

    private void SpawnFood1() {
        do {
            foodGridPositionArr[1] = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPositionArr[1]) != -1);

        foodGameObjectArr[1] = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObjectArr[1].GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
        foodGameObjectArr[1].transform.position = new Vector3(foodGridPositionArr[1].x, foodGridPositionArr[1].y);

    }


    private void SpawnBad0() {
        do {
            badFoodGridPositionArr[0] = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(badFoodGridPositionArr[0]) != -1);

        badFoodGameObjectArr[0] = new GameObject("Food", typeof(SpriteRenderer));
        badFoodGameObjectArr[0].GetComponent<SpriteRenderer>().sprite = GameAssets.i.badSprite;
        badFoodGameObjectArr[0].transform.position = new Vector3(badFoodGridPositionArr[0].x, badFoodGridPositionArr[0].y);

    }
    private void SpawnBad1() {
        do {
            badFoodGridPositionArr[1] = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(badFoodGridPositionArr[1]) != -1);

        badFoodGameObjectArr[1] = new GameObject("Food", typeof(SpriteRenderer));
        badFoodGameObjectArr[1].GetComponent<SpriteRenderer>().sprite = GameAssets.i.badSprite;
        badFoodGameObjectArr[1].transform.position = new Vector3(badFoodGridPositionArr[1].x, badFoodGridPositionArr[1].y);

    }

    // RELATED TO ->*****<-
    // STIL NEED TO DO: Check that I put priseSprite
    //                  since I plan to make several prizes and choose randomly,
    //                  I suggest to put the N prizes sprites in GameAssets and than
    //                  choose here randomly.
    //                  Then I'll choose whice priseSprite to use
    private void SpawnPrise0() {
        do {
            prizeGridPositionArr[0] = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(prizeGridPositionArr[0]) != -1);

        prizeGameObjectArr[0] = new GameObject("Food", typeof(SpriteRenderer));
        prizeGameObjectArr[0].GetComponent<SpriteRenderer>().sprite = GameAssets.i.priseSprite1;
        prizeGameObjectArr[0].transform.position = new Vector3(prizeGridPositionArr[0].x, prizeGridPositionArr[0].y);

    }
    private void SpawnPrise1() {
        do {
            prizeGridPositionArr[1] = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(prizeGridPositionArr[1]) != -1);

        prizeGameObjectArr[1] = new GameObject("Food", typeof(SpriteRenderer));
        prizeGameObjectArr[1].GetComponent<SpriteRenderer>().sprite = GameAssets.i.priseSprite2;
        prizeGameObjectArr[1].transform.position = new Vector3(prizeGridPositionArr[1].x, prizeGridPositionArr[1].y);

    }


    public bool TrySnakeEatFood(Vector2Int snakeGridPosition) {
        // If snake took food0
        if (snakeGridPosition == foodGridPositionArr[0]) {
            GameSound.PlayGoodFoodSound();
            Object.Destroy(foodGameObjectArr[0]);
            SpawnFood0();
            GameHandler.AddScore(10);
            GameHandler.AddProgressBar();
            GameHandler.SetProgressImage("Good");
            return true;
        }
        foodTimerArr[0]--;
        if (foodTimerArr[0] <= 0 && (!stopSpawningFoods)) { //Relocate food0
            Object.Destroy(foodGameObjectArr[0]);
            SpawnFood0();
            foodTimerArr[0] = 31;
        }


        // If snake took food1
        if (snakeGridPosition == foodGridPositionArr[1]) {
            GameSound.PlayGoodFoodSound();
            Object.Destroy(foodGameObjectArr[1]);
            SpawnFood1();
            GameHandler.AddScore(10);
            GameHandler.AddProgressBar();
            GameHandler.SetProgressImage("Good");
            return true;
        }

        foodTimerArr[1]--;
        if (foodTimerArr[1] <= 0 && (!stopSpawningFoods)) {  //Relocate food1
            Object.Destroy(foodGameObjectArr[1]);
            SpawnFood1();
            foodTimerArr[1] = 22;
        }

        // If snake took Bad food0
        if (snakeGridPosition == badFoodGridPositionArr[0]) {
            GameSound.PlayBadFoodSound();
            Object.Destroy(badFoodGameObjectArr[0]);
            SpawnBad0();
            GameHandler.ReduceScore(10);
            GameHandler.ReduceProgressBar();
            GameHandler.SetProgressImage("Bad");
            return true;
        }
        badFoodTimerArr[0]--;
        if (badFoodTimerArr[0] <= 0 && (!stopSpawningFoods)) {//Relocate bad food0
            Object.Destroy(badFoodGameObjectArr[0]);
            SpawnBad0();
            badFoodTimerArr[0] = 28;
        }

        // If snake took Bad food1
        if (snakeGridPosition == badFoodGridPositionArr[1]) {
            GameSound.PlayBadFoodSound();
            Object.Destroy(badFoodGameObjectArr[1]);
            SpawnBad1();
            GameHandler.ReduceScore(10);
            GameHandler.ReduceProgressBar();
            GameHandler.SetProgressImage("Bad");
            return true;
        }

        badFoodTimerArr[1]--;
        if (badFoodTimerArr[1] <= 0 && (!stopSpawningFoods)) {//Relocate bad food1
            Object.Destroy(badFoodGameObjectArr[1]);
            SpawnBad1();
            badFoodTimerArr[1] = 16;
        }
        ////////////////////////////////////////////
        //     Prize handler  //
        //If took Prise0
        if (snakeGridPosition == prizeGridPositionArr[0]) {
            Object.Destroy(prizeGameObjectArr[0]);
            GameHandler.prizesLeftToWin--;
            return true;
        }
        //If took Prise1
        if (snakeGridPosition == prizeGridPositionArr[1]) {
            Object.Destroy(prizeGameObjectArr[1]);
            GameHandler.prizesLeftToWin--;
            return true;
        }

        return false;//Ate nothing

    }

    //destroys all foods and bad foods on field
    public void DestroyAll() {
        stopSpawningFoods = true;
        for (int i = 0; i < 2; i++) {
            Object.Destroy(foodGameObjectArr[i]);
            Object.Destroy(badFoodGameObjectArr[i]);
            foodGridPositionArr[i].Set(-1, -1);
            badFoodGridPositionArr[i].Set(-1, -1);
        }
    }
    //Spawns final prizes
    public void SpawnPrizes() {
        SpawnPrise0();
        SpawnPrise1();
    }

    public Vector2Int ValidateGridPosition(Vector2Int gridPosition) {
        if(gridPosition.x < 0) {
            gridPosition.x = width - 1;
        }
        if (gridPosition.x > width - 1) {
            gridPosition.x = 0;
        }
        if (gridPosition.y < 0) {
            gridPosition.y = height - 1;
        }
        if (gridPosition.y > height - 1) {
            gridPosition.y = 0;
        }
        return gridPosition;
    }
}
