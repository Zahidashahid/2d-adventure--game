using System.Collections;
using UnityEngine;
public class SkeletonRangeAttackMovement : MonoBehaviour
{
    /*-----------Range attack movement ------------*/
    #region Public Variables;
    // public float nextAttackTime; // after every 2 sec
    // public float damage;
    public GameObject playerObject;
    public int maxHealth = 100;
    public int currentHealth;
    public int numberOfDamgeTake;
    public HealthBar healthBar;
    public Animator animator;
    public LootSystem lootSystem;
    EnemyShield shield;
    public Transform leftLimit;
    public Transform rightLimit;
    /*
    public AudioSource arrowHitSound;
    public AudioSource DeathSound;*/
    #endregion
    #region Private Variables
    private BoxCollider2D boxCollider2d;
    private Transform target;
    private float distance;
    int direction = 1;
    #endregion
    #region Awake
    private void Awake()
    {
        currentHealth = maxHealth;
    }
    #endregion
    #region Start
    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        numberOfDamgeTake = 0;
        healthBar.SetHealth(currentHealth);
        target = playerObject.transform;
        animator = GetComponent<Animator>();
        lootSystem = GetComponent<LootSystem>();
        shield = GetComponent<EnemyShield>();
    }
    #endregion
    #region Update
    void Update()
    {
        Flip();
        if (!InsideOfLimit())
        {
            SelectTarget();
        }
    }
    #endregion
    #region Chane the direction when player is behind the enemy
    void Flip()
    {
        distance = Vector2.Distance(transform.position, target.position);
        //Debug.Log(transform.position + "!! " + target.position);
        //Debug.Log("Flip called" + distance);
        Vector3 rotation = transform.eulerAngles;
        rotation.x *= -1;
      /*  if ( transform.position.x == target.position.x )
        {
            Debug.Log("Zero rotaion");
            rotation.y = 0f;
           
        }
        else*/ 
        if ( transform.position.x > target.position.x)//&& direction == 1
        {
           // Debug.Log("180 rotaion");
            rotation.y = 180f;
            direction = 2;

        }
        else if ( transform.position.x < target.position.x)//&& direction == 2
        {
            //Debug.Log("flip");
            rotation.y = 0;
            direction = 1;
        }
        else
        {
            Debug.Log("None rotaion");
            rotation.y = 0f;
        }
        transform.eulerAngles = rotation;
    }
    #endregion
    #region When enemy is damage by player
    public void TakeDamage(int damage)
    {
        if (currentHealth > 0) // Player can only damage enemy if health is greater than zero. if not on need to damage it
        {
            if (numberOfDamgeTake > 3)
                StartCoroutine(SheildTimer());
            if (!shield.ActiveShield)
            {
                currentHealth -= damage;
                healthBar.SetHealth(currentHealth);  
                /***********Testing for object saving ends **************/
                // play hurt animation
                StartCoroutine(RangeAttackSkeletonHurtAnimation());
                if (currentHealth <= 0)
                {
                    StartCoroutine(Die());
                    lootSystem.Spawnner(transform);
                }
            }
            else
                numberOfDamgeTake += 1;

        }
            
    }
    #endregion
    #region Shield animation 
    IEnumerator SheildTimer()
    {
        shield.ActiveShield = false;
        animator.SetBool("Sheild", false);
        yield return new WaitForSeconds(5f);
        shield.ActiveShield = true;
        animator.SetBool("Sheild", true);
        numberOfDamgeTake = 0;
    }
    #endregion
    #region Death
    IEnumerator Die()
     {
        animator.SetBool("Hurt", false);
        // Die Animation
        yield return new WaitForSeconds(1); 
        animator.SetBool("Death", true);
        Debug.Log("Skeleton died!");
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    
        // Disable the player 
        Destroy(gameObject);
     }
    #endregion
    #region Hurt animation 
    public IEnumerator RangeAttackSkeletonHurtAnimation()
    {
        // play hurt animation
        animator.SetBool("Hurt", true); 
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("Hurt", false);

    }
    #endregion
    #region Fliping the enemy base of the position of player. "Face towards the enemy"
    void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);
        Vector3 rotation = transform.eulerAngles;
        //Debug.Log(distanceToLeft + " " + distanceToRight);
        if (distanceToLeft > distanceToRight)
        {
            rotation.y = 180f;
            direction = 2;
        }
        else
        {
            rotation.y = 180f;
            direction = 1;
        }

    }
    #endregion
    #region Check either enemy is in the defined limits (range) or not
    bool InsideOfLimit()
    {
        /*
        Debug.Log(transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x);*/
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }
    #endregion
}
