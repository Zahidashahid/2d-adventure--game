using UnityEngine;
public class WayPointFollwer : MonoBehaviour
{
    /*----------Moving tile-  WayPointFollower*----------*/
    #region Variables
    public GameObject[] wayPoints; // limits in which tile move  
    int CurrentWayPointIndex = 0; //index of limit
    public float speed = 2f;
    #endregion
    #region Update
    void Update()
    {
        if(Vector2.Distance(wayPoints[CurrentWayPointIndex].transform.position, transform.position) < .1f) //Check if tile is out side the define range
        {
            CurrentWayPointIndex++;
            if (CurrentWayPointIndex >= wayPoints.Length)
                CurrentWayPointIndex = 0;
                
        }
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[CurrentWayPointIndex].transform.position, Time.deltaTime * speed);
    }
    #endregion
}
