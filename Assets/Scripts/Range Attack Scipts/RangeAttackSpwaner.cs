using UnityEngine;
public class RangeAttackSpwaner : MonoBehaviour
{
    /*-------Range attack enemy spawner--------*/
    #region variables 
    public GameObject skeletonRangeAttackSpwan;
    float randX;
    Vector2 whereToSpwan;
    float spwanRate = 45f;
    float nextSpwan = 0.0f;
    #endregion
    #region Update
    void Update()
    {
        if (Time.time > nextSpwan)
        {
            nextSpwan = Time.time + spwanRate;
            /*randX = Random.Range(transform.position.x -10, transform.position.x);
            whereToSpwan = new Vector2(randX, transform.position.y);*/
            whereToSpwan = new Vector2(transform.position.x, transform.position.y);
            Instantiate(skeletonRangeAttackSpwan, whereToSpwan, Quaternion.identity);
        }
    }
    #endregion
}
