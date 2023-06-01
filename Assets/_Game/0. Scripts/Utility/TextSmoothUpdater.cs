using UnityEngine;
using TMPro;

/// <summary>
/// It Is Related To Currency And UnlockableItem Where I Am
/// Reducing Or Increasing Money. 
/// It Is Basically A Text Convention Which I Will Use To Animate My 
/// PlayerCurrency
/// </summary>
public static class TextSmoothUpdater
{
    public static System.Collections.IEnumerator UpdateMoneyTextSmoothly<T>(T moneyText, int startValue, int endValue, float duration = 1f) where T : TMP_Text
    {
        float startTime = Time.time;
        float endTime = startTime + duration;
        float scaleFactor = 1.2f; // Scale factor for the size up and down effect

        while (Time.time <= endTime)
        {
            float t = Mathf.InverseLerp(startTime, endTime, Time.time);
            int currentValue = (int)Mathf.Lerp(startValue, endValue, t);
            moneyText.text = "$" + currentValue.ToString();

            // Size up and down effect
            float scale = Mathf.PingPong(Time.time * scaleFactor, 1f) + 0.8f; // Adjust the scale range as needed
            moneyText.transform.localScale = new Vector3(scale, scale, scale);

            yield return null;
        }

        moneyText.text = "$" + endValue.ToString();
        moneyText.transform.localScale = Vector3.one; // Reset the scale to its original size
    }

}