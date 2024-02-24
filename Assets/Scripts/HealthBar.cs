using UnityEngine;
using UnityEngine.UI; 
public class HealthBar : MonoBehaviour
{
    /*--------------Player Healthbar Script----------------*/
    #region Variables
    public Slider slider;
	public Gradient gradient;
	public Image fill;
    #endregion
    #region Set player health to maximum
    public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;
		fill.color = gradient.Evaluate(1f);
	}
    #endregion
    #region Update player health
    public void SetHealth(int health)
	{
		slider.value = health;
		fill.color = gradient.Evaluate(slider.normalizedValue);
		//Debug.Log("health  is " + health);
	}
    #endregion
}