using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    private float timeReset;
    public float timerCurrent;
    public float timerCurrentSmol;
    private float timeResetSmol;
    public GameObject enemyT;
    public GameObject enemyB;
    public GameObject smolEnemy;
    private Vector3 instancePosB;
    private Vector3 instancePosT;
    private bool flipFlop = false;
    private int enemyCount = 0;
    public float timeBetweenSpawn = 2;
    public float timeBetweenSpawnSmol = 2;
    private int randInt;

    // Update is called once per frame
    void Update()
    {
        //Generates for random position of EnemySmol
        randInt = Mathf.RoundToInt(Random.Range(0, 1));
        //Generates for random position of Enemy
        instancePosT = new Vector3(Random.Range(-7, 7), -7);
        instancePosB = new Vector3(Random.Range(-7, 7), 7);
        //Equates the two timers for spawning Enemy and EnemySmol
        timerCurrentSmol = Time.time - timeResetSmol;
        timerCurrent = Time.time - timeReset;

        //Checks enemyCount so that there are no more than 2 Enemies on screen at a time
        if (enemyCount <= 1)
        {
            //Instantiates Top and Bottom Enemies at a constantly decreasing time interval
            //Alternates between Top and Bottom enemies
            if (timerCurrent > timeBetweenSpawn && !flipFlop)
            {
                enemyCount++;
                GameObject enemyTop = Instantiate(enemyT, instancePosT, Quaternion.identity);
                enemyTop.transform.SetParent(this.transform);
                flipFlop = true;
                ResetTimer();
            }
            else if (timerCurrent > timeBetweenSpawn && flipFlop)
            {
                enemyCount++;
                GameObject enemyBot = Instantiate(enemyB, instancePosB, Quaternion.identity);
                enemyBot.transform.SetParent(this.transform);
                flipFlop = false;
                ResetTimer();

                //Decreased interval time
                if (timeBetweenSpawn >= 0.5)
                {
                    timeBetweenSpawn -= 0.1f;
                }

            }

        }

        //Instantiates SmolEnemy Independant of the larger Enemy
        if (timerCurrentSmol > timeBetweenSpawnSmol && randInt == 0)
        {
            Instantiate(smolEnemy, instancePosT, Quaternion.identity);
            ResetTimerSmol();
        }
        else if (timerCurrentSmol > timeBetweenSpawnSmol && randInt == 1)
        {
            Instantiate(smolEnemy, instancePosB, Quaternion.identity);
            ResetTimerSmol();
        }

    }

    //To be called upon by Player
    //Subtracts from the enemy count
    void SubtractEnemyCount()
    {
        enemyCount--;
    }

    //Resets Enemy timer
    void ResetTimer()
    {
        timeReset = Time.time;
    }

    //Resets SmolEnemy timer
    void ResetTimerSmol()
    {
        timeResetSmol = Time.time;
    }

}
