using UnityEngine;
using Pathfinding;
public class Enemies : MonoBehaviour
{
    /*-----Change the direction of enemy "Make enemy facing to player"----*/
    public AIPath aiPath;
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);//Change the sprite  
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}