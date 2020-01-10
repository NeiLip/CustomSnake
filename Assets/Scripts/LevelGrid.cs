
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using System.IO.IsolatedStorage;

public class LevelGrid {

    private Vector2Int foodGridPosition;
    private GameObject foodGameObject;
    private Vector2Int foodGridPosition2;
    private GameObject foodGameObject2;
    private Vector2Int foodGridPosition3;
    private GameObject foodGameObject3;
    private Vector2Int badGridPosition1;
    private GameObject badGameObject1;
    private Vector2Int badGridPosition2;
    private GameObject badGameObject2;
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
        ifSpawnPrise = 15;
        this.snake = snake;
        SpawnFood();
        SpawnFood2();
        //SpawnFood3();
        SpawnBad1();
        SpawnBad2();

    }

    private void SpawnFood() {
        do {
            foodGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition) != -1);

        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
    }

    private void SpawnFood2() {
        do {
            foodGridPosition2 = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition2) != -1);

        foodGameObject2 = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject2.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
        foodGameObject2.transform.position = new Vector3(foodGridPosition2.x, foodGridPosition2.y);

    }
    private void SpawnFood3() {
        do {
            foodGridPosition3 = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition3) != -1);

        foodGameObject3 = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject3.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
        foodGameObject3.transform.position = new Vector3(foodGridPosition3.x, foodGridPosition3.y);

    }

    private void SpawnBad1() {
        do {
            badGridPosition1 = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(badGridPosition1) != -1);

        badGameObject1 = new GameObject("Food", typeof(SpriteRenderer));
        badGameObject1.GetComponent<SpriteRenderer>().sprite = GameAssets.i.badSprite;
        badGameObject1.transform.position = new Vector3(badGridPosition1.x, badGridPosition1.y);

    }
    private void SpawnBad2() {
        do {
            badGridPosition2 = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(badGridPosition2) != -1);

        badGameObject2 = new GameObject("Food", typeof(SpriteRenderer));
        badGameObject2.GetComponent<SpriteRenderer>().sprite = GameAssets.i.badSprite;
        badGameObject2.transform.position = new Vector3(badGridPosition2.x, badGridPosition2.y);

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
        // If snake took food1
        if (snakeGridPosition == foodGridPosition) {
            GameSound.PlayGoodFoodSound();
            Object.Destroy(foodGameObject);
            SpawnFood();
            GameHandler.AddScore();
            return true;
        }
        // If snake took food2
        if (snakeGridPosition == foodGridPosition2) {
            GameSound.PlayGoodFoodSound();
            Object.Destroy(foodGameObject2);
            SpawnFood2();
            GameHandler.AddScore();
            return true;
        }
        // If snake took food3
        if (snakeGridPosition == foodGridPosition3) {
            GameSound.PlayGoodFoodSound();
            Object.Destroy(foodGameObject3);
            SpawnFood3();
            GameHandler.AddScore();
            return true;
        }
        // If snake took Bad food1
        if (snakeGridPosition == badGridPosition1) {
            GameSound.PlayBadFoodSound();
            Object.Destroy(badGameObject1);
            SpawnBad1();
            GameHandler.ReduceScore();
            return true;
        }
        // If snake took Bad food2
        if (snakeGridPosition == badGridPosition2) {
            GameSound.PlayBadFoodSound();
            Object.Destroy(badGameObject2);
            SpawnBad2();
            GameHandler.ReduceScore();
            return true;
        }

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
