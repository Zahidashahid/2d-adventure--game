using UnityEngine;
using TMPro;
public class ArrowStore : MonoBehaviour
{
    #region Variables
    public static int arrowPlayerHas;
    public static int numOfArrowsTillCheckPoint;
    public int maxNumOfArrow = 100;
    public TMP_Text arrowStoreText;
    #endregion
    #region Start
    void Start()
    {
        /*SaveSystem.instance.playerData.arrow, maxNumOfArrow);
        arrowPlayerHas = SaveSystem.instance.playerData.arrow;*/
        //arrowStoreText.text = "X " + arrowPlayerHas;
    }
    #endregion
    #region Update
    private void Update()
    {
        /*Debug.Log("arrowPlayerHas " + arrowPlayerHas);*/
       
    }
    #endregion
    #region When bullets/ammunition or long shafted projectiles are used.
    public void ArrowUsed()
    {
        if (arrowPlayerHas > 0)
        {
            arrowPlayerHas -= 1;
            UpdateArrowText();
        }
        else
            Debug.Log("!!!You dont have arrows!!! ");
    }
    #endregion
    #region Update text in game view when an arrow or a bullet is usec
    public void UpdateArrowText()
    {
        arrowStoreText.text = "X " + arrowPlayerHas;
    }
    #endregion
}
