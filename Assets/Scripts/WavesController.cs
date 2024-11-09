using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WavesController : MonoBehaviour
{
    public int skeletonLVL1InitialNumber;
    public int skeletonLVL2InitialNumber;
    public int goblinLVL1InitialNumber;
    public int goblinLVL2InitialNumber;
    public int mushroomLVL1InitialNumber;

    public int numberOfWaves;
    public int timeBetweenWavesInSeconds;

    public bool enableSkeletonLVL1;
    public bool enableSkeletonLVL2;
    public bool enableGoblinLVL1;
    public bool enableGoblinLVL2;
    public bool enableMushroomLVL1;


    private int skeletonLVL1CurrentWaveNumber;
    private int skeletonLVL2CurrentWaveNumber;
    private int goblinLVL1CurrentWaveNumber;
    private int goblinLVL2CurrentWaveNumber;
    private int mushroomLVL1CurrentWaveNumber;

    private const float skeletonLVL1NumberFactor = 0.2f;
    private const float skeletonLVL2NumberFactor = 0.1f;
    private const float goblinLVL1NumberFactor = 0.15f;
    private const float goblinLVL2NumberFactor = 0.1f;
    private const float mushroomLVL1NumberFactor = 0.05f;

    private int enemyNumber;
    private int currentWave;

    void Start()
    {
        skeletonLVL1CurrentWaveNumber = skeletonLVL1InitialNumber;
        skeletonLVL2CurrentWaveNumber = skeletonLVL2InitialNumber;
        goblinLVL1CurrentWaveNumber = goblinLVL1InitialNumber;
        goblinLVL2CurrentWaveNumber = goblinLVL2InitialNumber;
        mushroomLVL1CurrentWaveNumber = mushroomLVL1InitialNumber;
        Debug.Log("Skeleton LVL_1: " + skeletonLVL1CurrentWaveNumber.ToString());
        Debug.Log("Skeleton LVL_2: " + skeletonLVL2CurrentWaveNumber.ToString());
        Debug.Log("Goblin LVL_1: " + goblinLVL1CurrentWaveNumber.ToString());
        Debug.Log("Goblin LVL_2: " + goblinLVL2CurrentWaveNumber.ToString());
        Debug.Log("Mushroom LVL_1: " + mushroomLVL1CurrentWaveNumber.ToString());
        NextWaveEnemiesNumber();
        NextWaveEnemiesNumber();
        NextWaveEnemiesNumber();
        NextWaveEnemiesNumber();
        NextWaveEnemiesNumber();
        NextWaveEnemiesNumber();
        NextWaveEnemiesNumber();
        NextWaveEnemiesNumber();
        NextWaveEnemiesNumber();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NextWaveEnemiesNumber()
    {
        if (enableSkeletonLVL1)
        {
            skeletonLVL1CurrentWaveNumber = (currentWave >= 1 && skeletonLVL1CurrentWaveNumber == 0) ? 
                5 : 
                skeletonLVL1CurrentWaveNumber + (int)Math.Ceiling(skeletonLVL1NumberFactor * skeletonLVL1CurrentWaveNumber);
        }
        if (enableSkeletonLVL2)
        {
            skeletonLVL2CurrentWaveNumber = (currentWave >= 1 && skeletonLVL2CurrentWaveNumber == 0) ? 
                5 : 
                skeletonLVL2CurrentWaveNumber + (int)Math.Ceiling(skeletonLVL2NumberFactor * skeletonLVL2CurrentWaveNumber);
        }
        if (enableGoblinLVL1)
        {
            goblinLVL1CurrentWaveNumber = (currentWave >= 1 && goblinLVL1CurrentWaveNumber == 0) ? 
                5 : 
                goblinLVL1CurrentWaveNumber + (int)Math.Ceiling(goblinLVL1NumberFactor * goblinLVL1CurrentWaveNumber);
        }
        if (enableGoblinLVL2)
        {
            goblinLVL2CurrentWaveNumber = (currentWave >= 1 && goblinLVL2CurrentWaveNumber == 0) ? 
                5 : 
                goblinLVL2CurrentWaveNumber + (int)Math.Ceiling(goblinLVL2NumberFactor * goblinLVL2CurrentWaveNumber);
        }
        if (enableMushroomLVL1)
        {
            mushroomLVL1CurrentWaveNumber = (currentWave >= 1 && mushroomLVL1CurrentWaveNumber == 0) ? 
                5 : 
                mushroomLVL1CurrentWaveNumber + (int)Math.Ceiling(mushroomLVL1NumberFactor * mushroomLVL1CurrentWaveNumber);
        }
        Debug.Log("Skeleton LVL_1: " + skeletonLVL1CurrentWaveNumber.ToString());
        Debug.Log("Skeleton LVL_2: " + skeletonLVL2CurrentWaveNumber.ToString());
        Debug.Log("Goblin LVL_1: " + goblinLVL1CurrentWaveNumber.ToString());
        Debug.Log("Goblin LVL_2: " + goblinLVL2CurrentWaveNumber.ToString());
        Debug.Log("Mushroom LVL_1: " + mushroomLVL1CurrentWaveNumber.ToString());
    }

}
