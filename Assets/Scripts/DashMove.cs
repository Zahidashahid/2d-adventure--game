using UnityEngine;
public class DashMove : MonoBehaviour
{
    #region Variables
    private Rigidbody2D rb;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction = 0;
    #endregion
    #region Start 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }
    #endregion
    #region Update
    void Update()
    {
        if (direction == 0)
        {/*
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                direction = 1;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                direction = 2;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                direction = 3;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                direction = 4;
            }*/
        }
        else
        {
            Debug.Log("Dash moving  Player");
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;
                if (direction == 1)
                {
                    rb.velocity = Vector2.left * dashTime;
                }
                else if (direction == 2)
                {
                    rb.velocity = Vector2.right * dashTime;
                }
                else if (direction == 3)
                {
                    rb.velocity = Vector2.up * dashTime;
                }
                else if (direction == 4)
                {
                    rb.velocity = Vector2.down * dashTime;
                }
            }
        }
    }
    #endregion
}

