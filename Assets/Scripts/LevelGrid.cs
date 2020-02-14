
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using System.IO.IsolatedStorage;

public class LevelGrid {

    private Vector2Int[] foodGridPositionArr;
    private GameObject[] foodGameObjectArr;

    private int foodTimer1;

    private Vector2Int[] badFoodGridPositionArr;
    private GameObject[] badFoodGameObjectArr;

    private Vector2Int priseGridPosition;
    private GameObject priseGameObject;
    private int ifSpawnPrise;

    private static int lastPrise;
    private int width;
    private int height;
    private Snake snake;

    public LevelGrid(int width, int height) {
        this.width = width;
        this.height = height;
    }

    public void Setup(Snake snake) {
        foodGridPositionArr = new Vector2Int[3];
        foodGameObjectArr = new GameObject[3];
        badFoodGridPositionArr = new Vector2Int[2];
        badFoodGameObjectArr = new GameObject[2];

        ifSpawnPrise = 15;
        foodTimer1 = 1;
        this.snake = snake;
        SpawnFood();
       // SpawnFood2();
        //SpawnFood3();
        SpawnBad1();
        SpawnBad2();

    }

    private void SpawnFood() {
        do {
            foodGridPositionArr[0] = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPositionArr[0]) != -1);

        foodGameObjectArr[0] = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObjectArr[0].GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
        foodGameObjectArr[0].transform.position = new Vector3(foodGridPositionArr[0].x, foodGridPositionArr[0].y);
    }

    private void SpawnFood2() {
        do {
            foodGridPositionArr[1] = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPositionArr[1]) != -1);

        foodGameObjectArr[1] = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObjectArr[1].GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
        foodGameObjectArr[1].transform.position = new Vector3(foodGridPositionArr[1].x, foodGridPositionArr[1].y);

    }


    private void SpawnBad1() {
        do {
            badFoodGridPositionArr[0] = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(badFoodGridPositionArr[0]) != -1);

        badFoodGameObjectArr[0] = new GameObject("Food", typeof(SpriteRenderer));
        badFoodGameObjectArr[0].GetComponent<SpriteRenderer>().sprite = GameAssets.i.badSprite;
        badFoodGameObjectArr[0].transform.position = new Vector3(badFoodGridPositionArr[0].x, badFoodGridPositionArr[0].y);

    }
    private void SpawnBad2() {
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
    private void SpawnPrise() {
        do {
            priseGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(priseGridPosition) != -1);

        priseGameObject = new GameObject("Food", typeof(SpriteRenderer));

        int curRand = Random.Range(1, 4);
        lastPrise = curRand;
        if (curRand == 1) {
            priseGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.priseSprite1;
        }
        else if (curRand == 2) {
            priseGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.priseSprite2;
        }
        else if (curRand == 3) {
            priseGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.priseSprite3;
        }
        priseGameObject.transform.position = new Vector3(priseGridPosition.x, priseGridPosition.y);

    }

    public bool TrySnakeEatFood(Vector2Int snakeGridPosition) {
        // If snake took food0
        if (snakeGridPosition == foodGridPositionArr[0]) {
            GameSound.PlayGoodFoodSound();
            Object.Destroy(foodGameObjectArr[0]);
            SpawnFood();
            GameHandler.AddScore(10);
            GameHandler.AddProgressBar();
            return true;
        }
        // If snake took food1
        if (snakeGridPosition == foodGridPositionArr[1]) {
            GameSound.PlayGoodFoodSound();
            Object.Destroy(foodGameObjectArr[1]);
            SpawnFood2();
            GameHandler.AddScore(10);
            GameHandler.AddProgressBar();
            return true;
        }

        // If snake took Bad food0
        if (snakeGridPosition == badFoodGridPositionArr[0]) {
            GameSound.PlayBadFoodSound();
            Object.Destroy(badFoodGameObjectArr[0]);
            SpawnBad1();
            GameHandler.ReduceScore(10);
            GameHandler.ReduceProgressBar();
            return true;
        }
        // If snake took Bad food1
        if (snakeGridPosition == badFoodGridPositionArr[1]) {
            GameSound.PlayBadFoodSound();
            Object.Destroy(badFoodGameObjectArr[1]);
            SpawnBad2();
            GameHandler.ReduceScore(10);
            GameHandler.ReduceProgressBar();
            return true;
        }
        ////////////////////////////////////////////
        //     Prize handler   -    should be summoned after victory //

        //Destroy and Spawn prize
        ifSpawnPrise--;
        if (ifSpawnPrise <= 0) {
            Object.Destroy(priseGameObject);
            SpawnPrise();
            ifSpawnPrise = 60; //Spawn Time. Maybe In the future change it to a varaible
        }

        //If took Prise
        if (snakeGridPosition == priseGridPosition && priseGameObject != null) {
            Object.Destroy(priseGameObject);

            GameHandler.PutPrizeOnShelf(lastPrise);


            return true;
        }

        return false;//Ate nothing

    }

    public int GetFoodTimer1() {
        return foodTimer1;
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
