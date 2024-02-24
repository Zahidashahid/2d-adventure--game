using UnityEngine;
public class ProjectileShooting : MonoBehaviour
{
    /*------------ Projectile shoot by player  ----------  */
    #region Variables
    public float speed; /***Speed of projectile i.e bullet or arrow***/
    public float lifeTime; /***Time after that projectile call self-distruction***/
    public float distance; /***disctance btw projectile and enemy***/
    public LayerMask enemy; /***defines the enemy layers***/
    public Transform arrowTransform;
    PlayerMovement playerMovement; /***Refer to script***/
    public SpriteRenderer spriteRenderer;
    public GameObject impactEffect;
    /*public AudioSource arrowHitSound;*/
    Vector2 velocity;
    #endregion
    #region Start
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        speed = 1f;
        Invoke("DestroyProjectile", lifeTime);
        velocity = (Vector3.right * speed);     
    }
    #endregion
    #region FixedUpdate
    void FixedUpdate()
    {
        transform.Translate(velocity);
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, enemy);/***objects that lie along the path of a ray & observing which objects are hit by it***/
        if (hitInfo.collider != null)
        {
            Debug.Log("Arrow hit Skeleton in scriptprojectile");
            Debug.Log(hitInfo.collider.name);
            string difficultyLevel = PlayerPrefs.GetString("difficultyLevel");
            if (hitInfo.collider.name == "Skeleton" || hitInfo.collider.tag == "Skeleton")
            {
                GameObject impactGameObject = Instantiate(impactEffect, hitInfo.point, Quaternion.identity);
                if (difficultyLevel == "Easy")
                {
                    hitInfo.collider.GetComponent<SkeletonEnemyMovement>().TakeDamage(40);
                }
                else if (difficultyLevel == "Medium")
                {
                    hitInfo.collider.GetComponent<SkeletonEnemyMovement>().TakeDamage(30);
                }
                else if (difficultyLevel == "Hard")
                {
                    hitInfo.collider.GetComponent<SkeletonEnemyMovement>().TakeDamage(10);
                }

            }
            if (hitInfo.collider.name == "Range Attack Skeleton" || hitInfo.collider.tag == "RangedAttackSkeleton")
            {
                GameObject impactGameObject = Instantiate(impactEffect, hitInfo.point, Quaternion.identity);
                if (difficultyLevel == "Easy")
                {
                    hitInfo.collider.GetComponent<SkeletonRangeAttackMovement>().TakeDamage(40);
                }
                else if (difficultyLevel == "Medium")
                {
                    hitInfo.collider.GetComponent<SkeletonRangeAttackMovement>().TakeDamage(30);
                }
                else if (difficultyLevel == "Hard")
                {
                    hitInfo.collider.GetComponent<SkeletonRangeAttackMovement>().TakeDamage(10);
                }
            }
            
            if (hitInfo.collider.tag == "Enemy")
            {
                Debug.Log("Enemy hit!!!!!");
                GameObject hit = hitInfo.collider.gameObject;
                Destroy(hit);
            }
                DestroyProjectile();
        }
    }
    #endregion
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}