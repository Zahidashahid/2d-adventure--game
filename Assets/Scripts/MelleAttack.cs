using System.Collections;
using UnityEngine;
public class MelleAttack : MonoBehaviour
{
    /*--------- Script for enemy attack -------------*/
    #region Veribales
    public Animator animator;
    public Transform attackPoint1;
    public Transform attackPoint2;
    public LayerMask playerLayers;
    //float attackRange = 4f;
     float damage;
     float attackDistance = 5;  
    public float nextAttackTime; // after every 2 sec
    private float distance; // stores distance btw player and skeleton
    public static bool inRange = false; // check player in range
    private bool cooling;
    public GameObject target;
    public SkeletonEnemyMovement skeletonEnemyMovement;
    //public AudioSource meleeAttackSound;
    #endregion
    #region Start
    void Start()
    {
        nextAttackTime = -1;
        skeletonEnemyMovement = GameObject.FindGameObjectWithTag("Skeleton").GetComponent<SkeletonEnemyMovement>();
        target = GameObject.FindGameObjectWithTag("Player");  
    }
    #endregion
    #region Update
    void Update()
    {
        if (inRange)
        {
            skeletonEnemyMovement.Flip();
            if (nextAttackTime <= -1)
            {
                // MelleAttackLogic();
                StartCoroutine(Attack());
                nextAttackTime = 1;
                //Debug.Log("nextAttackTime" + nextAttackTime);
            }
            else
            {
                nextAttackTime -= Time.deltaTime;
            }
        }
        else
        {
            nextAttackTime = -1;
        }
    }
    #endregion
    #region Attack logic
    IEnumerator Attack()
    {
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.2f); 
        animator.SetBool("Attack", false);
       
        /*---deteck enemies in range of attack---*/
       // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(attackPoint1.position, attackPoint2.position, playerLayers);
         /*---demage them---*/
        foreach (Collider2D player in hitEnemies)
        {
            if (player.tag == "Player")
            {
                string difficultyLevel = PlayerPrefs.GetString("difficultyLevel");
                Debug.Log("We hit player");
                if (difficultyLevel == "Easy")
                {
                    player.GetComponent<PlayerMovement>().TakeDamage(30); 
                }
                else if (difficultyLevel == "Medium")
                {
                    player.GetComponent<PlayerMovement>().TakeDamage(40);
                }
                else if (difficultyLevel == "Hard")
                {
                    player.GetComponent<PlayerMovement>().TakeDamage(60);
                }
                break;
            }
        }
    }
    #endregion
    #region On trigger enter and exit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            inRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            inRange = false;
    }
    #endregion
}
