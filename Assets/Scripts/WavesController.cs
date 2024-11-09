using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesController : MonoBehaviour
{
    public int skeletonLVL1InitialNumber;
    public int skeletonLVL2InitialNumber;
    public int goblinLVL1InitialNumber;
    public int goblinLVL2InitialNumber;
    public int mushroomLVL1InitialNumber;
    public int numberOfWaves;
    public int timeBetweenWavesInSeconds;
    public bool skeletonLVL1;
    public bool skeletonLVL2;
    public bool goblinLVL1;
    public bool goblinLVL2;
    public bool mushroomLVL1;

    private int skeletonLVL1CurrentWaveNumber;
    private int skeletonLVL2CurrentWaveNumber;
    private int goblinLVL1CurrentWaveNumber;
    private int goblinLVL2CurrentWaveNumber;
    private int mushroomLVL1CurrentWaveNumber;
    private int enemyNumber;
    private int currentWave;

    void Start()
    {
        skeletonLVL1CurrentWaveNumber = skeletonLVL1InitialNumber;
        skeletonLVL2CurrentWaveNumber = skeletonLVL2InitialNumber;
        goblinLVL1CurrentWaveNumber = goblinLVL1InitialNumber;
        goblinLVL2CurrentWaveNumber = goblinLVL2InitialNumber;
        mushroomLVL1CurrentWaveNumber = mushroomLVL1InitialNumber;
}

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NextWaveEnemiesNumber()
    {
        skeletonLVL1CurrentWaveNumber = skeletonLVL1CurrentWaveNumber + (int)(0.4 * skeletonLVL1CurrentWaveNumber);
        skeletonLVL1CurrentWaveNumber = skeletonLVL1CurrentWaveNumber + (int)(0.4 * skeletonLVL1CurrentWaveNumber);
        skeletonLVL1CurrentWaveNumber = skeletonLVL1CurrentWaveNumber + (int)(0.4 * skeletonLVL1CurrentWaveNumber);
        skeletonLVL1CurrentWaveNumber = skeletonLVL1CurrentWaveNumber + (int)(0.4 * skeletonLVL1CurrentWaveNumber);
        skeletonLVL1CurrentWaveNumber = skeletonLVL1CurrentWaveNumber + (int)(0.4 * skeletonLVL1CurrentWaveNumber);
    }

}
