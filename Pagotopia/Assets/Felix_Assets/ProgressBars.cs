using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBars : MonoBehaviour
{
    [SerializeField] Slider prosperityBar;
    [SerializeField] Slider happinessBar;
    [SerializeField] Slider environementBar;

    public void SetMaxNeeds(float prosperityValue, float happinessValue, float environmentValue)
    {
        prosperityBar.maxValue = prosperityValue;
        happinessBar.maxValue = happinessValue;
        environementBar.maxValue = environmentValue;

        prosperityBar.value = prosperityValue;
        happinessBar.value = happinessValue;
        environementBar.value = environmentValue;
    }
    public void SetNeeds(float prosperityValue, float happinessValue, float environmentValue)
    {
        prosperityBar.value = prosperityValue;
        happinessBar.value = happinessValue;
        environementBar.value = environmentValue;
    }
    /*public void SetHappiness(float happinessValue)
    {
        happinessBar.value = happinessValue;
    }
    public void SetEnvironment(float environmentValue)
    {
        environementBar.value = environmentValue;
    }*/
}
