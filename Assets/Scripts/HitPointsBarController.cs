using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPointsBarController : MonoBehaviour
{
    public Slider slider;

    public void UpdateHitPointsBar(float currentHitPoints, float maxHitPoints)
    {
        slider.value = currentHitPoints / maxHitPoints;
    }
    void Update()
    {
        
    }
}
