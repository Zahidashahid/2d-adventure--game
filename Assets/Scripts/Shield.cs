using UnityEngine;
public class Shield : MonoBehaviour
{
     /*------Active and disable player shield  (This script is needed only when shield animation is available and a different gameobject is used for shield)-------*/
    #region Variables
    PlayerController controls;
    public GameObject shieldGO;
    public  bool activeShield;
    public Animator animator;
    #endregion
    private void Awake()
    {
        controls = new PlayerController(); 
       // controls.Gameplay.Shield.performed += ctx => SetShield();
    }
    void Start()
    {
        animator = GetComponent<Animator>(); ;
        activeShield = false;
        /*shieldGO.SetActive(false);*/
    }
    #region Returns bool value that shield is active or not
    public bool ActiveShield
    {
        get{
            return activeShield;
        }
        set
        {
            activeShield = value;
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
