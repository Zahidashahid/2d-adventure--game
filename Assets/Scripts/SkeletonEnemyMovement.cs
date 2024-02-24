using System.Collections;
using UnityEngine;
public class SkeletonEnemyMovement : MonoBehaviour
{
    /*-----Melee Attack enemy Controller  . 
    * health , Movement,  position, and  attacks "Animation controller"------*/
    /* private BoxCollider2D boxCollider2d;*/
    #region Public Variables;
    /*
    public AudioSource arrowHitSound;
    public AudioSource DeathSound;*/
    public Rigidbody2D rb;
    public Animator animator;
    //public Animator playerAnimator;
    public Transform rayCast;
    /*-------The range at which the enemy seeks to damage the player.-----------*/
    public Transform leftLimit;
    public Transform rightLimit;
    public HealthBar healthBar; 
   // public float attackRange = 2f;
    public float rayCastLength;
    public float attackDistance; // min distance for attack
    public float damage;
    public float stopDistance; //Enemy stop moving when distance < stop distance
    //public float retreatDistance; //Enemy start moving back from player
    public int currentHealth;
    public int direction = 1;
    public int numberOfDamgeTake;
    public LootSystem lootSystem;
    #endregion
    #region Private Variables
    private EnemyShield shield;
    private int maxHealth = 100;
    private Transform target; // Here target will be the player 
    private float distance; // stores distance btw player i.e target and enemy
                            //private bool inRange; // check player in range
    #endregion
    #region Awake
    private void Awake()
    {
        SelectTarget();
        currentHealth = maxHealth;
    }
    #endregion
    #region Start
    private void Start()
    {
        numberOfDamgeTake = 0;
        shield = GetComponent<EnemyShield>();
        lootSystem = GetComponent<LootSystem>();
        animator.SetFloat("Speed", Mathf.Abs(40));
        //healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
        
        if ((PlayerPrefs.GetInt("AvatarSelected") == 1))
        {
            target = GameObject.Find("Player_Goblin").GetComponent<Transform>();
        }
        else
        {
            target = GameObject.Find("MushrromPlayer").GetComponent<Transform>();
        }
        stopDistance = 2;
        //retreatDistance = 3;
    }
    #endregion
    #region Update
    void Update()
    {
        if (!InsideOfLimit() && !MelleAttack.inRange)
        {
            SelectTarget();
        }
        /*
           ----------- enemy moving towards player----------
        */
        if (Vector2.Distance(transform.position, target.position) > stopDistance && currentHealth > 0)
        {
            if (direction == 1)
            {
                rb.velocity = new Vector2(3, rb.velocity.y);
                transform.localScale = new Vector2(1, 1);
            }
            else
            {
                rb.velocity = new Vector2(-3, rb.velocity.y);
                transform.localScale = new Vector2(-1, 1);
            }
            //Debug.Log("transform pos" + transform.position);
            //Flip();
        }
        /*
            -----------if enemy near enough but not much near stop  moving----------
         */
        else if (Vector2.Distance(transform.position, target.position) < stopDistance ) 
        {
            rb.velocity = new Vector2(0, 0);
            transform.position = this.transform.position;
            //Flip();
        }
        /*
           -----------enemy moving away from player if it is very near to player----------
        */
       /*else if (Vector2.Distance(transform.position, target.position) <  retreatDistance)
        {
            if(direction == 1)
            {
              
                rb.velocity = new Vector2(-3, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(3, rb.velocity.y);
            }
            Flip();
            //transform.localScale = new Vector2(1, 1);
        }*/

        else
        {
            if (direction == 1)
            {
                rb.velocity = new Vector2(3, rb.velocity.y);
                transform.localScale = new Vector2(1, 1);
            }
            else
            {
                rb.velocity = new Vector2(-3, rb.velocity.y);
                transform.localScale = new Vector2(-1, 1);
            }
            //Debug.Log("transform pos" + transform.position);
           // Flip();
        }
    }
    #endregion
    #region OnTriggerEnter2D
    void OnTriggerEnter2D(Collider2D collision)
    {/*
         Debug.Log("Collision with " + collision.tag);
         Debug.Log("Collision name " + collision.name);*/
        /*-----If another enemy is in his way or there is an obstacle, the enemy will change the direction of his movement.----*/
        if (collision.tag == "Obstacles" || collision.tag == "Skeleton")
        {

            if (direction == 1)
            {
                direction = 2;
            }
            else if(direction == 2)
                direction = 1;

        }
        if (collision.tag == "Player")
        {
            target = collision.transform;
            
           // Debug.Log("player collied with skelton");
           // Debug.Log("player entred in Seleton zone");
           // collision.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }

    }
    #endregion
    #region When a player damage to an enemy.
    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
           // Debug.Log("Damaging the enemy :- shield.ActiveShield " + shield.ActiveShield);
            if (numberOfDamgeTake > 3)
                StartCoroutine(SheildTimer());
            if (!shield.ActiveShield || (transform.position.x > target.position.x && direction == 1) || (transform.position.x < target.position.x && direction == 2))
            {
                currentHealth -= damage;  
                /***********enemy / object state saving ends **************/
                healthBar.SetHealth(currentHealth); 
                StartCoroutine(SkeletonHurtAnimation());
                if (currentHealth <= 0)
                {
                    rb.velocity = new Vector2(0, 0);
                    transform.position = this.transform.position;
                    // transform.localScale = new Vector2(0, 0);
                    /* Debug.Log("transform " + this.name);
                     Debug.Log("position " + this.transform.position);*/
                    lootSystem.Spawnner(transform);
                    StartCoroutine(Die());
                }
            }
            else
                numberOfDamgeTake += 1;
        }
    }
    public IEnumerator SkeletonHurtAnimation()
    {
        /*---------play hurt animation--------------*/
        
        animator.SetBool("Hurt", true); 
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("Hurt", false);

    }
    #endregion

    /*  IEnumerator SeletonAttackAnimation()
      {
          Debug.Log("In IEnumerator SeletonAttackAnimation()");
          animator.SetBool("Attack", true);
          yield return new WaitForSeconds(0.2f);
          animator.SetBool("Attack", false);

      }*/
    #region On enemy death
    IEnumerator Die()
    {
        animator.SetBool("Hurt", false);
        animator.SetBool("Death", true);// Die Animation
        Debug.Log("Skeleton died!");  
        /*********** object saving ends **************/ 
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);// Disable the player 
    }
    #endregion
    #region Shield Animation timer 
    IEnumerator SheildTimer()
    {
        shield.ActiveShield = false;
        Debug.Log("Enemy 1 Shield active");
        animator.SetBool("Sheild", false);
        yield return new WaitForSeconds(5f);
        shield.ActiveShield = true;
        animator.SetBool("Sheild", true);
        numberOfDamgeTake = 0;
    }
    #endregion
    void RaycastDebugger()
    {
        if (distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);
        }
        else if (distance < attackDistance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.green);
        }
    }
    #region Changing the enemy's direction when the player is close to the enemy's back
    public void Flip()
    {
        distance = Vector2.Distance(transform.position, target.transform.position); // Checking distance btw player and enemy
        
        Vector3 rotation = transform.eulerAngles;
         rotation.x *= -1;
       /* if (distance >= 5)
        {
            inRange = false;
        }
        else
            inRange = true;*/
        /*
            direction 2 means skeleton moving towards left vise versa
            transform.position.x i.e skeleton position > target.position.x  i.e player position means player is on right side of enemy
        */
        
       
        
        if ( transform.position.x > target.position.x && direction == 1) 
        {
            Debug.Log("distance " + distance); Debug.Log("Filipng");
            Debug.Log("direction " + direction);
            Debug.Log("transform.position.x " + transform.position.x);
            Debug.Log("target.position.x " + target.position.x);
            rotation.y = 180f;
            direction = 2;
            Debug.Log("Skeleton Flip to left" );

        }
        else if (  transform.position.x < target.position.x && direction == 2)
        {
            Debug.Log("distance " + distance); Debug.Log("Filipng");
            Debug.Log("direction " + direction);
            Debug.Log("transform.position.x " + transform.position.x);
            Debug.Log("target.position.x " + target.position.x);
            rotation.y = 0f;
            direction = 1;
            Debug.Log("Skeleton Flip to right");
        }
        else
        {
        //rotation.y = 0f;
        }
        //transform.eulerAngles = rotation;
        //Debug.Log("transform.eulerAngles " + transform.eulerAngles);

    }
    #endregion
    #region  Check whether the enemy is within the defined range or not, if it is outside his range, let  go within the range.
    void SelectTarget()
    {
        
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);
        Vector3 rotation = transform.eulerAngles;
        //Debug.Log( " Selecting target "  );
        if(distanceToLeft > distanceToRight)
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
    bool InsideOfLimit()
    {
        /*
        Debug.Log(transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x);*/
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }
    #endregion
}
