using UnityEngine;
public class LeftHandMovement : MonoBehaviour
{
    /*--------Player's arm movement Script----------*/
    #region Varaibles
    PlayerController controls;
    Vector3 rotation;
     PlayerMovement playerMovement;
    PauseGame pauseGameScript;
    #endregion
    #region Awake 
    private void Awake()
    {
        controls = new PlayerController();
        controls.Gameplay.RangeAttackGP.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Gameplay.RangeAttackGP.canceled += ctx => rotation = Vector2.zero;
        controls.Gameplay.MouseDirection.performed += ctx => PlayersArmRotate(ctx.ReadValue<Vector2>());
        controls.Gameplay.MouseDirection.canceled += ctx => rotation = Vector2.zero;
    }
    #endregion
    #region Start
    private void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        pauseGameScript = GameObject.FindGameObjectWithTag("PauseCanvas").GetComponent<PauseGame>();
    }
    #endregion
    #region Roation of the object i.e  arm
    private void Move(Vector2 vector)
    {
        Debug.Log("Moving");
        transform.Rotate(vector.x * Vector3.forward + vector.y * Vector3.forward);
    }
    void PlayersArmRotate(Vector2 vector)
    {
        /*-----------Left hand i.e spreat object rotation with mouse position --------*/
        Vector2 mousePoint = Camera.main.ScreenToWorldPoint(vector) - transform.position;

        if (!pauseGameScript.isGamePaused)
        {
            float rotZ = Mathf.Atan2(mousePoint.y, mousePoint.x) * Mathf.Rad2Deg;
            if (playerMovement.direction == 2)
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            else
                transform.rotation = Quaternion.Euler(0f, 180f, 180 - rotZ);

           
        }
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
