using UnityEngine;
public class ScrollingBackground : MonoBehaviour
{
    /*-------Dynamic background, with moving object i.e clouds  ------*/
    #region Variables
    public Renderer background;
    public float backgroundSpeed;
    #endregion
    #region Start
    void Start()
    {
        background = GetComponent<Renderer>();
    }
    #endregion
    #region Update
    void Update()
    {
        background.material.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime, 0f);
    }
    #endregion
}
