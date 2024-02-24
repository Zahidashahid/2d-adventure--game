using UnityEngine;
public class EnemyShield : MonoBehaviour
{
    /*------Enemy shield script--------*/
    #region Variables
    public Animator animator;
    private Transform player;
    private float distance;
    public  bool activeShield;
    MelleAttack melleAttack;
    #endregion
    #region Start
    void Start()
    {
        //melleAttack =  GetComponent<MelleAttack>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        activeShield = false;
        animator.SetBool("Sheild", false);
        distance = 7;
    }
    #endregion
    /* void Update()
     {
         player.position = player.transform.position;
         if (Vector2.Distance(transform.position, player.position) < distance && animator.GetBool("Attack") == false) 
         {
             //Debug.Log("sheild Active !! "  );
             animator.SetBool("Sheild", true);
             activeShield = true;

         }
         else
         {
             animator.SetBool("Sheild", false);
             activeShield = false;
         }
     }
     private void OnTriggerEnter2D(Collider2D collision)
     {
         if(collision.tag == "Player")
         {
             Debug.Log("Collision "+collision);
             if (!activeShield)
             {
                 //melleAttack.inRange = true;
                 animator.SetBool("Sheild", true);
                 activeShield = true;
             }

         }
     }
    */
    #region Check either shield is active or not
    public bool ActiveShield
    {
        get
        {
            return activeShield;
        }
        set
        {
            activeShield = value;
        }
    }
    #endregion
}