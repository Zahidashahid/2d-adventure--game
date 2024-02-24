using UnityEngine;
public class BirdEnemySpwaner : MonoBehaviour
{
    /*---------Bird Enemy Spawner ---------*/
    #region Varaibles
    public GameObject birdSpwan;
    float randX;
    Vector2 whereToSpwan;
    public float spwanRate = 50f;
    float nextSpwan = 0.0f;
    #endregion
    #region Spawn bird after defined time intervel 
    void Update()
    {
        if (Time.time > nextSpwan)
        {
            nextSpwan = Time.time + spwanRate;
            randX = Random.Range(158, 184);
            whereToSpwan = new Vector2(randX, transform.position.y);
            Instantiate(birdSpwan, whereToSpwan, Quaternion.identity);
        }
    }
    #endregion
}
