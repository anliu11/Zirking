using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    
    


    private void Start()
    {
       
    }
    private void Update()
    {
       
    }

    //Set health bar to maximun health value
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    //Set health bar to health value
    public void SetHealth(int health)
    {
        slider.value = health;
     
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
