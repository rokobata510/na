using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ScrapToHealRenderer : MonoBehaviour
{
    public float costMultiplierPerStep;
    public float steps;
    public float Denominator => 100 / steps;

    public Slider slider;   
    public Button button;

    public void OnEnable()
    {
        slider = GameObject.Find("ScrapToHealSlider").GetComponent<Slider>();
        button = GameObject.Find("ScrapToHealButton").GetComponent<Button>();
        slider.maxValue = steps;
        slider.value = (Inventory.Instance.CurrentHealthPercentage / Denominator);
    }

    public void Heal()
    {

        float goldToGetToCurrentHealth = Function(Inventory.Instance.CurrentHealthPercentage);
        float goldToGetToTargetHealth = Function(slider.value * Denominator);
        int goldToSubtract = (int)(goldToGetToTargetHealth - goldToGetToCurrentHealth);
        int targetHealth = (int)(slider.value / steps * Inventory.Instance.MaxHealth);
        if (Inventory.Instance.Gold < goldToSubtract)
        {
            Debug.Log("Not enough gold. Gold needed: " + goldToSubtract + " Gold owned: " + Inventory.Instance.Gold);
            return;
        }
        if (targetHealth <= Inventory.Instance.Health)
        {
            Debug.Log("Target health is less than current health. Target health: " + targetHealth + " Current health: " + Inventory.Instance.Health);
            return;
        }
        Inventory.Instance.Gold -= goldToSubtract;
        Inventory.Instance.Health = targetHealth;


    }
    public float Function(float x)
    {
        return Mathf.Pow(costMultiplierPerStep, x / Denominator);
    }

}

