/*using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;*/
using UnityEngine;

public class Bow : MonoBehaviour
{
    /*--------Weapon(Gun/Bow) of the Player movement----------*/
    #region Variables 
    PlayerController controls;
    Vector3 rotateBow;
    public Transform shootPoint;
    public GameObject projectile;
    public Animator animator;
    public float timeBtwShots;
    public float nextAttackTime;
   public float rotZ;
    bool canAttack;
    //int arrowLeft;
    public ArrowStore arrowStore;
    public PlayerMovement playerMovement;
    PauseGame pauseGameScript;
    Vector2 BowRotateValue;
    #endregion
    #region Awake
    private void Awake()
    {
        controls = new PlayerController();
        controls.Gameplay.ArowHit.performed += ctx => ArrowShoot();
        controls.Gameplay.ArowHit.canceled += ctx => ArrowShootStop();
        controls.Gameplay.RangeAttackGP.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Gameplay.RangeAttackGP.canceled += ctx => rotateBow = Vector2.zero;
        controls.Gameplay.MouseDirection.performed += ctx => BowRotate(ctx.ReadValue<Vector2>());
        controls.Gameplay.MouseDirection.performed += ctx => BowRotateValue = ctx.ReadValue<Vector2>();
        controls.Gameplay.MouseDirection.canceled += ctx => rotateBow = Vector2.zero;
    }
    #endregion
    #region Start
    private void Start()
    {
        nextAttackTime = -1;
        canAttack = true;
        playerMovement = GetComponentInParent<PlayerMovement>();
        pauseGameScript = GameObject.FindGameObjectWithTag("PauseCanvas").GetComponent<PauseGame>();
        animator = GetComponentInParent<Animator>();
        Debug.Log("arrow player has in script" + ArrowStore.arrowPlayerHas);
    }
    #endregion
    #region Update
    void Update()
    {
        if (nextAttackTime <= -1 && !playerMovement.isHurt)
        {
            canAttack = true;
        }
        else
        {
            nextAttackTime -= Time.deltaTime;
            canAttack = false;
        }
    }
    #endregion
    #region The weapon must be pointed at the player
    private void Move(Vector2 vector)
    {
        transform.Rotate(vector.x * Vector3.forward + vector.y * Vector3.forward);
    }
    void BowRotate(Vector2 vector)
    {
        /*-----------Weapon i.e spreat object rotation with mouse position --------*/
        Vector2 mousePoint = Camera.main.ScreenToWorldPoint(vector) - transform.position;

        if (pauseGameScript.isGamePaused == false )
        {
            //Debug.Log("Gun is rotating");
            rotZ = Mathf.Atan2(mousePoint.y, mousePoint.x) * Mathf.Rad2Deg;
            /*if (mousePoint.x < 0 && playerMovement.direction == 2)
            {
                if (playerMovement.isWalking == false)
                {
                    transform.Rotate(180f, 180f, 180f);
                    playerMovement.PlayerChangeDirection();
                }
            }
            else if (mousePoint.x > 0 && playerMovement.direction == 1)
            {
                if (playerMovement.isWalking == false)
                {
                    transform.Rotate(0f, 180f, 0f);
                    playerMovement.PlayerChangeDirection();
                }
            }
            Debug.Log("rotation "+ rotZ);
            Debug.Log("Player direction" + playerMovement.direction );*/
            if (playerMovement.direction == 2)
            {
                if ( rotZ > -90 && rotZ < 90)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
                }
                else
                    playerMovement.PlayerChangeDirection();
            }
            else
            {
                if ((rotZ > 90 && rotZ < 180) || (rotZ > -180 && rotZ < -90))
                {
                    transform.rotation = Quaternion.Euler(0f, 180f, 180 - rotZ);
                }
                else
                    playerMovement.PlayerChangeDirection();
            }
        }
    }
    #endregion
    #region Range attack  
    void ArrowShoot()
    {
        if (pauseGameScript.isGamePaused == false)
        {
            Debug.Log("Shooting");
            if (ArrowStore.arrowPlayerHas > 0 && canAttack)
            {
                arrowStore.ArrowUsed();
                animator.SetBool("Attack1", true);
                Instantiate(projectile, shootPoint.position, transform.rotation);
                nextAttackTime = 0.01f;

            }
        }
    }
    void ArrowShootStop()
    {
        animator.SetBool("Attack1", false);
    }
    #endregion
    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
