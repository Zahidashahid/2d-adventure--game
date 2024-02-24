using UnityEngine;
public class SkeletonBow : MonoBehaviour
{
    /*---------The bow contoller of range attack by skeleton------------ */
    #region Variables
    public float offset;
    public float timeBtwShots;
    public float startTimeBtwShots;
    public float damage;
    public float attackDistance; // arrow will be possible when   distance is 7.5 b/w arrow and player
    public float nextAttackTime; // after every 2 sec
    public Transform shotPoint;
   /* Vector3 playerPosition;
    Vector3 BowPosition;*/
    Vector3 targetDirection;
    //GameObject playerObject;
    public GameObject projectile;
    public GameObject bowObj;
    public GameObject target;
    public GameObject arrow;
    public bool inRange; // check player in range
    private float distance; // stores distance btw player and arrrow
    #endregion
    #region Start
    private void Start()
    {
        nextAttackTime = -1;
        /*if (PlayerPrefs.GetInt("AvatarSelected") == 2)
        {
            playerObject = GameObject.Find("MushrromPlayer");
        }
        else if (PlayerPrefs.GetInt("AvatarSelected")== 1)
        {
            playerObject = GameObject.Find("Player_Goblin");
        }*/
        //playerObject = GameObject.FindGameObjectWithTag("Player");
       // playerPosition = playerObject.transform.position;
       // BowPosition = bowObj.transform.position;
       
    }
    #endregion
    #region Update
    void Update()
    {
        if (inRange)
        {
            Debug.Log("inRange" + inRange);
            if (nextAttackTime <= -1)
            {
                ShootArrow();
                //ArrowLogic();
                // Debug.Log("nextAttackTime" + nextAttackTime);
            }
            else
            {
                nextAttackTime -= Time.deltaTime;
            }
            targetDirection = target.transform.position - transform.position;
            float rotZ = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
           // Debug.Log("Direction" + targetDirection);
           // Debug.Log("rot" + rotZ);
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
    }
    #endregion
    #region on trigger enter and exit
    private void OnTriggerEnter2D(Collider2D collision)
    {
       //  StartCoroutine(Attack());
        if (collision.tag == "Player")
        {
            target = collision.gameObject;
           // Debug.Log("player entred in arrow danger zone");
            //collision.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            inRange = true;
            //Debug.Log(" player Inrage =" + inRange);
            //nextAttackTime = -1;
            //ArrowLogic();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
     {
        if (collision.tag == "Player")
        {
            Debug.Log("player exiting in arrow danger zone");
            inRange = false;
            Debug.Log("Inrage =" + inRange);
            nextAttackTime = -1;
        }
     }
    #endregion
    void ArrowLogic() // Arrow attack and instantiate
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        //Debug.Log("Value of distance is " + distance);
        attackDistance = 13;
        if (attackDistance >= distance)
        {
            inRange = true;
           // Debug.Log("Inrage =" + inRange);
            
        }
        else
        {
            inRange = false;
            //Debug.Log("Inrage =" + inRange);
        }
    }
    #region throw a projectile
    void ShootArrow()
    {
        /*Debug.Log("In shoot function" + transform.rotation);
        Debug.Log("nextAttackTime :-" +  nextAttackTime);*/
   
        nextAttackTime = 2;
        //Debug.Log("nextAttackTime :-" + nextAttackTime);
        if (transform.position.x >= target.transform.position.x)
        {
            /*
             *  when player is at left side of ranged attack emeny;
             *  taeget direction will be change to enemy(self/skeleton) subtract target(Player) if player is at right side of enemy vise versa.
             */
            targetDirection =   transform.position - target.transform.position;
            float rotZ = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            //Debug.Log(rotZ + "Rotation");
            transform.rotation = Quaternion.Euler(0f, 180f, -rotZ + offset);
             /*
              * "-" is used to rotation towards player so that collider also change with arrow left moving
              * 180f to change arrow towards left 
              */
/**/
            //Debug.Log(transform.rotation + " new");
           // Instantiate(projectile, shotPoint.position,  transform.rotation);
        }
        Instantiate(projectile, shotPoint.position, transform.rotation);
        //Debug.Log("Arrow instantiated");
    }
    #endregion
}
