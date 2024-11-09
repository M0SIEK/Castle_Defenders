using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

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

    public Direction direction;
    public Transform startPoint;
    public Transform nextTarget;
    public GameObject skeletonLVL1Prefab;
    public GameObject skeletonLVL2Prefab;
    public GameObject goblinLVL1Prefab;
    public GameObject goblinLVL2Prefab;
    public GameObject mushroomLVL1Prefab;
    public GameObject enemiesParentObject; //Pusty GameObject do grupowania przeciwnikow na scenie


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
    private int timeToNextWave;
    private Transform startPointTranslation;

    void Start()
    {
        skeletonLVL1CurrentWaveNumber = skeletonLVL1InitialNumber;
        skeletonLVL2CurrentWaveNumber = skeletonLVL2InitialNumber;
        goblinLVL1CurrentWaveNumber = goblinLVL1InitialNumber;
        goblinLVL2CurrentWaveNumber = goblinLVL2InitialNumber;
        mushroomLVL1CurrentWaveNumber = mushroomLVL1InitialNumber;

        SummonNextWave(skeletonLVL1CurrentWaveNumber, skeletonLVL2CurrentWaveNumber, goblinLVL1CurrentWaveNumber, goblinLVL2CurrentWaveNumber, mushroomLVL1CurrentWaveNumber);
    }

    void FixedUpdate()
    {
        if(timeToNextWave <= 0)
        {
            SummonNextWave(skeletonLVL1CurrentWaveNumber, skeletonLVL2CurrentWaveNumber, goblinLVL1CurrentWaveNumber, goblinLVL2CurrentWaveNumber, mushroomLVL1CurrentWaveNumber);
        }
    }

    private void SummonNextWave(int skeletonLVL1Number, int skeletonLVL2Number, int goblinLVL1Number, int goblinLVL2Number, int mushroomLVL1Number)
    {
        startPointTranslation = startPoint;
        //while (mushroomLVL1Number > 0)
        //{
        //    SummonEnemy(mushroomLVL1Prefab);
        //    mushroomLVL1Number--;
        //}
        while (skeletonLVL1Number > 0)
        {
            SummonEnemy(skeletonLVL1Prefab);
            skeletonLVL1Number--;
        }
        //while (skeletonLVL2Number > 0)
        //{
        //    SummonEnemy(skeletonLVL2Prefab);
        //    skeletonLVL2Number--;
        //}
        //while (goblinLVL1Number > 0)
        //{
        //    SummonEnemy(goblinLVL1Prefab);
        //    skeletonLVL2Number--;
        //}
        //while (goblinLVL2Number > 0)
        //{
        //    SummonEnemy(goblinLVL2Prefab);
        //    skeletonLVL2Number--;
        //}
        timeToNextWave = timeBetweenWavesInSeconds;
        enemyNumber = NextWaveEnemiesNumber();
    }

    private void SummonEnemy(GameObject enemyPrefab)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, startPoint.position, Quaternion.identity, enemiesParentObject.transform);
        EnemyController scriptEnemyController = newEnemy.GetComponent<EnemyController>();
        scriptEnemyController.startPoint = this.startPoint;
        scriptEnemyController.nextTarget = this.nextTarget;
        newEnemy.GetComponent<Transform>().position = startPointTranslation.position;
        startPointTranslation.position = startPointTranslation.position + GetRandomTranslation(direction, 1.0f);
    }

    private Vector3 GetRandomTranslation(Direction dir, float maxValue)
    {
        switch (dir)
        {
            case Direction.right: return new Vector3(-UnityEngine.Random.Range(0, maxValue), 0, 0);
            case Direction.left: return new Vector3(UnityEngine.Random.Range(0, maxValue), 0, 0);
            case Direction.top: return new Vector3(0, UnityEngine.Random.Range(0, maxValue), 0);
            case Direction.bottom: return new Vector3(0, -UnityEngine.Random.Range(0, maxValue), 0);
            default: return new Vector3(0, 0, 0);
        }
    }

    private int NextWaveEnemiesNumber()
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

        return skeletonLVL1CurrentWaveNumber + skeletonLVL2CurrentWaveNumber + goblinLVL1CurrentWaveNumber + goblinLVL2CurrentWaveNumber + mushroomLVL1CurrentWaveNumber;
    }
}
