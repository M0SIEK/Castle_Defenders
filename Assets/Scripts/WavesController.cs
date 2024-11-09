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

    private int enemyNumberInWave;
    private int currentEnemyNumber;
    private int currentWave;
    private float timeToNextWave;
    private Transform startPointTranslation;
    private Vector3 startPointCoordinates;

    void Start()
    {
        skeletonLVL1CurrentWaveNumber = skeletonLVL1InitialNumber;
        skeletonLVL2CurrentWaveNumber = skeletonLVL2InitialNumber;
        goblinLVL1CurrentWaveNumber = goblinLVL1InitialNumber;
        goblinLVL2CurrentWaveNumber = goblinLVL2InitialNumber;
        mushroomLVL1CurrentWaveNumber = mushroomLVL1InitialNumber;

        startPointCoordinates = startPoint.position;
    }

    void FixedUpdate()
    {
        if(timeToNextWave <= 0f)
        {
            if (currentWave < numberOfWaves)
            {
                SummonNextWave(skeletonLVL1CurrentWaveNumber, skeletonLVL2CurrentWaveNumber, goblinLVL1CurrentWaveNumber, goblinLVL2CurrentWaveNumber, mushroomLVL1CurrentWaveNumber);
            }
        }
        timeToNextWave -= Time.deltaTime;
    }

    private void SummonNextWave(int skeletonLVL1Number, int skeletonLVL2Number, int goblinLVL1Number, int goblinLVL2Number, int mushroomLVL1Number)
    {
        currentWave++;
        startPointTranslation = startPoint;
        while (mushroomLVL1Number > 0)
        {
            SummonEnemy(mushroomLVL1Prefab);
            mushroomLVL1Number--;
        }

        startPointTranslation.position = startPointTranslation.position + GetRandomTranslation(direction, 2.0f, 3.0f);
        while (skeletonLVL1Number > 0)
        {
            SummonEnemy(skeletonLVL1Prefab);
            skeletonLVL1Number--;
        }

        startPointTranslation.position = startPointTranslation.position + GetRandomTranslation(direction, 2.0f, 3.0f);
        while (skeletonLVL2Number > 0)
        {
            SummonEnemy(skeletonLVL2Prefab);
            skeletonLVL2Number--;
        }

        startPointTranslation.position = startPointTranslation.position + GetRandomTranslation(direction, 2.0f, 3.0f);
        while (goblinLVL1Number > 0)
        {
            SummonEnemy(goblinLVL1Prefab);
            goblinLVL1Number--;
        }

        startPointTranslation.position = startPointTranslation.position + GetRandomTranslation(direction, 2.0f, 3.0f);
        while (goblinLVL2Number > 0)
        {
            SummonEnemy(goblinLVL2Prefab);
            goblinLVL2Number--;
        }
        timeToNextWave = timeBetweenWavesInSeconds;
        enemyNumberInWave = NextWaveEnemiesNumber();
        startPoint.position = startPointCoordinates;
    }

    private void SummonEnemy(GameObject enemyPrefab)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, startPoint.position, Quaternion.identity, enemiesParentObject.transform);
        EnemyController scriptEnemyController = newEnemy.GetComponent<EnemyController>();
        scriptEnemyController.startPoint = this.startPoint;
        scriptEnemyController.nextTarget = this.nextTarget;
        newEnemy.GetComponent<Transform>().position = startPointTranslation.position;
        startPointTranslation.position = startPointTranslation.position + GetRandomTranslation(direction);
    }

    private Vector3 GetRandomTranslation(Direction dir, float minValue = 0f, float maxValue=1f)
    {
        switch (dir)
        {
            case Direction.right: return new Vector3(-UnityEngine.Random.Range(minValue, maxValue), 0, 0);
            case Direction.left: return new Vector3(UnityEngine.Random.Range(minValue, maxValue), 0, 0);
            case Direction.top: return new Vector3(0, UnityEngine.Random.Range(minValue, maxValue), 0);
            case Direction.bottom: return new Vector3(0, -UnityEngine.Random.Range(minValue, maxValue), 0);
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
            skeletonLVL2CurrentWaveNumber = (currentWave >= 3 && skeletonLVL2CurrentWaveNumber == 0) ? 
                4 : 
                skeletonLVL2CurrentWaveNumber + (int)Math.Ceiling(skeletonLVL2NumberFactor * skeletonLVL2CurrentWaveNumber);
        }
        if (enableGoblinLVL1)
        {
            goblinLVL1CurrentWaveNumber = (currentWave >= 5 && goblinLVL1CurrentWaveNumber == 0) ? 
                5 : 
                goblinLVL1CurrentWaveNumber + (int)Math.Ceiling(goblinLVL1NumberFactor * goblinLVL1CurrentWaveNumber);
        }
        if (enableGoblinLVL2)
        {
            goblinLVL2CurrentWaveNumber = (currentWave >= 7 && goblinLVL2CurrentWaveNumber == 0) ? 
                4 : 
                goblinLVL2CurrentWaveNumber + (int)Math.Ceiling(goblinLVL2NumberFactor * goblinLVL2CurrentWaveNumber);
        }
        if (enableMushroomLVL1)
        {
            mushroomLVL1CurrentWaveNumber = (currentWave >= 9 && mushroomLVL1CurrentWaveNumber == 0) ? 
                2 : 
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
