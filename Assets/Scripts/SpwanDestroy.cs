using UnityEngine;

public class SpwanDestroy : MonoBehaviour
{
    #region Destroy game object after define life time
    public float lifeTime = 40f;
    void Start()
    {
        Invoke("DestroyEnemy", lifeTime);
    }
    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    #endregion
}
