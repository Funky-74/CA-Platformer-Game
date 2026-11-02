using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void UpdateHealthBar(float currentValue)
    {
        slider.value = currentValue;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}

