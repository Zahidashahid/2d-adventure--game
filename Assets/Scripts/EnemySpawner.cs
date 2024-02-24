using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    /*--------Enemy Spawner script-------*/
    public enum SpwanState { SPWANING, WAITING, COUNTING  };
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public int rate;
    }

    #region Variables
    public Wave[] waves;
    public List<int> TakeList = new List<int>();
    private int nextWave = 0;
    int randaomNum;
    public Transform[] spwanPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountDown;
    
    private float searchCountDown = 1f;
    public SpwanState state = SpwanState.COUNTING;
    #endregion
    #region Start
    void Start()
    {  
        StartCoroutine(SpwanWave(waves[nextWave]));
        if (spwanPoints.Length == 0)
        {
            Debug.LogError("No Spwan points referenced");
        }
        waveCountDown = timeBetweenWaves;
    }
    #endregion
    #region Update
    void Update()
    {
        if (state == SpwanState.WAITING)
        {
            WaveCompleted();
           //check enemies still alive
            /*if(!EnemyIsAlive())
            {
                // Spwan Enemies now
                WaveCompleted();
            }
            else
            {
                return;
            }*/

        }
        if (waveCountDown <= 0)
        {
            if (state != SpwanState.SPWANING)
            {
                // start spwaning
                StartCoroutine(SpwanWave (waves[nextWave]));
                //To empty the listwhen a single wave of spawn s completed
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }
    #endregion
    #region on Single wave complete 
    void WaveCompleted()
    {
        //Debug.Log("Completed");
        state = SpwanState.COUNTING;
        waveCountDown = timeBetweenWaves;
        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            //Debug.Log("All waves completed");
        }
        else
            nextWave++;
    }
    #endregion
    #region Check any spawned enemy still exists or not and return  bool value 
    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if(searchCountDown <= 0f)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }
    #endregion
    #region Spwaning the enemy
    IEnumerator SpwanWave(Wave _wave)
    {
        /*-----Spawn wave --------*/
        Debug.Log("[_wave.count]" + _wave.count);
        TakeList = new List<int>(new int[_wave.count]);
        state = SpwanState.SPWANING;
        //spawn
        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            TakeList[i] = randaomNum;
             yield return new WaitForSeconds(1f/ _wave.rate);
        }
        state = SpwanState.WAITING;
        yield break;
    }
    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spwaning Enemy :" + _enemy.name);
         randaomNum = Random.Range(0, spwanPoints.Length);
        while(TakeList.Contains(randaomNum))
        {
            Debug.Log("Spawn repeted !!!!!!!!!!" );
            randaomNum = Random.Range(0, spwanPoints.Length);
        }
        
        Transform _sp = spwanPoints[randaomNum];
        Instantiate(_enemy, _sp.position, transform.rotation);
        Debug.Log("-sp-----------------  " + _sp);
    }
    #endregion
}