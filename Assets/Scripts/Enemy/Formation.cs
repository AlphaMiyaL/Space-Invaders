using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public int formationSize=50;
    public int rowsLength = 10;
    public float rowSpacing = 0.6f;
    public float columnSpacing = 0.6f;
    public int[,] enemyLifeArray;
    public GameObject[,] enemyArray;
    public float movementDelay = 0.05f;
    public float moveDistance = 0.2f;
    public float minShootDelay = 1f;
    public float maxShootDelay = 6f;

    private int rows;
    private float killed = 0;
    private string moveState = "Right";
    private string prevMoveState;
    private float lastMovement;
    private float timeLastShot;

    void Start()
    {
        rows = formationSize / rowsLength;
        enemyLifeArray = new int[rowsLength, rows];
        enemyArray = new GameObject[rowsLength, rows];
        for (int i=0; i< rowsLength; i++){
            for (int j=0; j< rows; j++){
                enemyLifeArray[i, j] = 1;
                if (j < rows / 4) {
                    enemyArray[i, j] = Object.Instantiate(enemy1, this.transform, this.transform);
                }
                else if (j< rows/2) {
                    enemyArray[i, j] = Object.Instantiate(enemy2, this.transform, this.transform);
                }
                else if (j < rows*3/4) {
                    enemyArray[i, j] = Object.Instantiate(enemy3, this.transform, this.transform);
                }
                else {
                    enemyArray[i, j] = Object.Instantiate(enemy4, this.transform, this.transform);
                }
                enemyArray[i, j].transform.position = this.transform.position + new Vector3(i*columnSpacing, j*rowSpacing);
            }
        }
        lastMovement = Time.time;
        timeLastShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        frontRowShoot();
        if (lastMovement+movementDelay/(1+(killed/10)) < Time.time){
            Move();
            lastMovement = Time.time;
        }
    }
    
    //return points
    public int CheckHit() {
        for (int i = 0; i<rowsLength; i++) {
            for (int j = 0; j<rows; j++) {
                if (enemyLifeArray[i,j] == 1) {
                    if (enemyArray[i, j].GetComponent<EnemyHit>().getHit() == true) {
                        Destroy(enemyArray[i, j]);
                        enemyLifeArray[i, j] = 0;
                        killed++;
                        if (j < rows / 4) {
                            return 10;
                        }
                        else if (j < rows / 2) {
                            return 20;
                        }
                        else if (j < rows * 3 / 4) {
                            return 30;
                        }
                        else {
                            return 40;
                        }
                    }
                }
            }
        }
        return 0;
    }

    public bool CheckEmpty() {
        if (killed == formationSize) {
            return true;
        }
        return false;
    }

    void frontRowShoot() {
        if (timeLastShot+minShootDelay<Time.time) {
            timeLastShot = Time.time+Random.Range(0, maxShootDelay-minShootDelay-killed/10);
            int col = Random.Range(0, 9);
            while (true) {
                for (int j = 0; j<rows; j++) {
                    if (enemyLifeArray[col, j] == 1) {
                        enemyArray[col, j].GetComponent<EnemyShooting>().Shoot();
                        return;
                    }
                }
                //Failed to find a enemy in that row
                if (col!=9) {
                    col++;
                }
                else {
                    col = 0;
                }
            }

        }
    }

    void Move(){
        switch (moveState)
        {
            case "Left":
                for (int i=0; i< rowsLength; i++) {
                    for (int j=0; j< rows; j++) {
                        if (enemyLifeArray[i,j]==1) {
                            enemyArray[i, j].transform.position = new Vector3(
                            enemyArray[i, j].transform.position.x - moveDistance,
                            enemyArray[i, j].transform.position.y);
                        }
                    }
                    if (getMostLeft() < -4.25f) {
                        prevMoveState = moveState;
                        moveState = "Down";
                    }
                }
                break;
            case "Right":
                for (int i = 0; i < rowsLength; i++) {
                    for (int j = 0; j < rows; j++) {
                        if (enemyLifeArray[i, j] == 1) {
                            enemyArray[i, j].transform.position = new Vector3(
                            enemyArray[i, j].transform.position.x + moveDistance,
                            enemyArray[i, j].transform.position.y);
                        }
                    }
                }
                if (getMostRight()>4.25f) {
                    prevMoveState = moveState;
                    moveState = "Down";
                }
                break;
            case "Down":
                for (int i = 0; i < rowsLength; i++) {
                    for (int j = 0; j < rows; j++) {
                        if (enemyLifeArray[i, j] == 1) {
                           enemyArray[i, j].transform.position = new Vector3(
                           enemyArray[i, j].transform.position.x,
                           enemyArray[i, j].transform.position.y - moveDistance);
                        }
                    }
                }
                if (prevMoveState == "Right") {
                    moveState = "Left";
                }
                else{
                    moveState = "Right";
                }
                break;
        }
    }

    private float getMostRight()
    {
        for (int i= rowsLength - 1; i>0; i--){
            for (int j= rows - 1; j>0; j--){
                if (enemyLifeArray[i,j] == 1){
                    return enemyArray[i, j].transform.position.x;
                }
            }
        }
        return 0f;
    }

    private float getMostLeft()
    {
        for (int i = 0; i < rowsLength; i++){
            for (int j = rows - 1; j > 0; j--){
                if (enemyLifeArray[i, j] == 1){
                    return enemyArray[i, j].transform.position.x;
                }
            }
        }
        return 0f;
    }
}
