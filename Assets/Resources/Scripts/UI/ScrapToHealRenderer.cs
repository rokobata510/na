using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrapToHealRenderer : MonoBehaviour
{
    public float costMultiplierPerStep;
    public float steps;
    public float Denominator => 100 / steps;

    public Slider slider;
    public Button button;

    private void OnSliderValueChanged(float value)
    {
        float currentNotch = (Inventory.Instance.Health / (float)Inventory.Instance.MaxHealth) * steps;
        float minAllowedNotch = Mathf.Ceil(currentNotch);

        if (value < minAllowedNotch)
        {
            slider.value = minAllowedNotch;
        }

        UpdateCostText();
    }

    public void OnEnable()
    {
        slider = GameObject.Find("ScrapToHealSlider").GetComponent<Slider>();
        button = GameObject.Find("ScrapToHealButton").GetComponent<Button>();

        slider.wholeNumbers = true;
        slider.maxValue = steps;

        float currentNotch = (Inventory.Instance.Health / (float)Inventory.Instance.MaxHealth) * steps;
        float minAllowedNotch = Mathf.Ceil(currentNotch);
        slider.value = minAllowedNotch;

        slider.onValueChanged.AddListener(OnSliderValueChanged);
        UpdateCostText();
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

        // Update slider after healing
        float newCurrentNotch = (Inventory.Instance.Health / (float)Inventory.Instance.MaxHealth) * steps;
        float newMinAllowedNotch = Mathf.Ceil(newCurrentNotch);
        slider.value = Mathf.Max(slider.value, newMinAllowedNotch);

        UpdateCostText();
    }

    public void UpdateCostText()
    {
        float goldToGetToCurrentHealth = Function(Inventory.Instance.CurrentHealthPercentage);
        float goldToGetToTargetHealth = Function(slider.value * Denominator);
        int goldToSubtract = (int)(goldToGetToTargetHealth - goldToGetToCurrentHealth);
        int targetHealth = (int)(slider.value / steps * Inventory.Instance.MaxHealth);
        int healthDifference = targetHealth - Inventory.Instance.Health;
        button.GetComponentInChildren<TMP_Text>().text = "Heal " + healthDifference + " for " + goldToSubtract + " gold";
    }

    public float Function(float x)
    {
        return Mathf.Pow(costMultiplierPerStep, x / Denominator);
    }
}