using UnityEngine;
using TMPro;
public static class TextSmoothUpdater
{
    public static System.Collections.IEnumerator UpdateMoneyTextSmoothly<T>(T moneyText,int startValue, int endValue, float duration = 1f) where T: TMP_Text
    {
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time <= endTime)
        {
            float t = Mathf.InverseLerp(startTime, endTime, Time.time);
            int currentValue = (int)Mathf.Lerp(startValue, endValue, t);
            moneyText.text = "$" + currentValue.ToString();
            yield return null;
        }

        moneyText.text = "$" + endValue.ToString();
    }
}