using UnityEngine;

public class SkeletonProjectile : MonoBehaviour
{
    /*-------------- "Projectile Script" Range Attack by Enemy----------- */
    #region Variables
    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask enemy;
    public Transform arrowTransform;
    PlayerMovement playerMovement; // Refer to script
    public SpriteRenderer spriteRenderer;
     GameObject playerObject;
     //GameObject skeletonObject;
     /*
    public AudioSource arrowHitSound;*/
    Vector2 velocity;
    Vector3 newDirection;
    Vector3 targetDirection;
    public float offset;
    #endregion
    #region Start
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        speed = 20f;
        Invoke("DestroyProjectile", lifeTime);
        //Debug.Log(""+playerObject.transform.position.x +"< "+skeletonObject.transform.position.x);
        distance = Vector2.Distance(transform.position, playerObject.transform.position);
        /* targetDirection = playerObject.transform.position - transform.position;
         newDirection = Vector3.RotateTowards(transform.forward, targetDirection, speed * Time.deltaTime, 0.0f);
        */
       /* if (playerObject.transform.position.x < skeletonObject.transform.position.x )
        {
            Debug.Log("Inflip left");
*//*
             transform.rotation = Quaternion.Euler(0f,180f,0f);
            transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, playerObject.transform.position.z);*//*
            //spriteRenderer.flipX = true;
            velocity = (Vector3.right * speed * Time.deltaTime );
        }
        else
        {
            Debug.Log("Inflip right");
            spriteRenderer.flipX = false;
            velocity = (Vector3.right * speed * Time.deltaTime);
        }*/
        velocity = (Vector3.right * Time.deltaTime * speed );
    }
    #endregion
    #region Update
    void Update()
    {
       // transform.rotation = Quaternion.LookRotation(newDirection);
        transform.Translate(velocity);
       // RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, enemy);
    }
    #endregion
    #region When projectile trigger any collider , it will check is it player if yes will damage it
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        { 
            collision.GetComponent<PlayerMovement>().TakeDamage(60);
            
            DestroyProjectile();
        }
        
    }
    #endregion
    #region Destroy the projectile 
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    #endregion
}
