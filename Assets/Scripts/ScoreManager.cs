using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    /*------ Score updates in game view----------*/
    #region Variables
    public static ScoreManager obj;
    public TMP_Text gemText;
    public TMP_Text cherryText;
    /*int gemCollected = 0;
    int cherryCollected =0;*/
    #endregion
    #region UpdateGemText
    public void UpdateGemText(int count)
    {
        gemText.text = "X" + count;
        //Debug.Log("Gem = " + count);
    }
    #endregion
    #region UpdateCherryText
    public void UpdateCherryText(int count)
    {
        cherryText.text = "X" + count;
        //Debug.Log("Cherry = " + count);
    }
    #endregion
}
