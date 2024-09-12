using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radial_Timer : MonoBehaviour
{
    public Slider slider;
    public float timer;
    public float timerSpeed = 1.0f;
    private bool radialTimer,hasUpdatedCount;
    public Manager manager;
    public static Radial_Timer instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timer = slider.minValue;
        hasUpdatedCount = false;
    }

    void Update()
    {
        if (timer <= slider.maxValue)
        {
            timer += timerSpeed * Time.deltaTime;
            slider.value = timer;
        }

        if (slider.value >= 20 && !hasUpdatedCount)
        {
            
            radialTimer = true;
        }
        if (radialTimer && !hasUpdatedCount)
        {
            manager.count++;
            Debug.Log("Striker Changed");
            hasUpdatedCount = true;
            radialTimer = false;

        }
    }
    public void OnDisable()
    {
       timer = 0;
       slider.value = 0;
       hasUpdatedCount = false;
    }
    
}
