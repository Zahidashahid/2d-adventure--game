
using UnityEngine;
public class Spawn_Boss_Run : StateMachineBehaviour
{
    #region Variables
    public Transform player;
    public Rigidbody2D rb; 
     float speed = 2.5f;
    public float attackRange = 5f;
    public Transform leftLimit;
    public Transform rightLimit;
    #endregion
    /*-------- OnStateEnter is called when a transition starts and the state machine starts to evaluate this state-------------*/
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        leftLimit = GameObject.FindGameObjectWithTag("Left Limit Boss").transform;
        rightLimit = GameObject.FindGameObjectWithTag("Right Limit Boss").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    
    }
    /*----------OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks---------------------*/
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distanceToLeft = Vector2.Distance(rb.transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(rb.transform.position, rightLimit.position);
        if (rb.transform.position.x >= leftLimit.position.x && rb.transform.position.x <= rightLimit.position.x)
        {/*
            Debug.Log("Boss is in the limits");*/
          
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            if (Vector2.Distance(player.position, rb.position) <= attackRange)
            {
                animator.SetBool("Attack", true);

            }
        }
        else
        {
             /*Debug.Log("Boss is out of the limits ----------------");
             Debug.Log("distanceTo Left limits " + distanceToLeft);
             Debug.Log("distanceTo Right limits " + distanceToRight);*/


            /*----Enemy in limits movement ----*/
            
            if (distanceToLeft > distanceToRight)
            {
                Vector2 target = new Vector2(leftLimit.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
            }
            else
            {
                Debug.Log("new target is right limit "  );
                Vector2 target = new Vector2(rightLimit.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                Debug.Log("new target name " +target);
                Debug.Log(" newPos name " + newPos);
                rb.MovePosition(newPos);
            }
           
        }
    }
    /*----------OnStateExit is called when a transition ends and the state machine finishes evaluating this state--------------*/
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.SetBool("Attack", false);

    }
}