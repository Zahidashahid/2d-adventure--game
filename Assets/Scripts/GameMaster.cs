
using UnityEngine;
public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    public static Vector2 lastCheckPointPos;
    public static Vector2 playerPos;
   
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
