using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public static class TransformExtensions
{
    public static void MoveTowardsUI(this Transform transform, Transform uiWorldPos, float duration)
    {
        transform.DOMove(uiWorldPos.position, duration);
    }

    private static Vector3 GetWorldPosition(RectTransform uiElement)
    {
        // Get the UI element's screen position
        Vector3 screenPosition = uiElement.position;

        // Convert the screen position to world space
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        // Adjust the y-coordinate to account for the difference between UI and world space
        float yOffset = Mathf.Abs(Camera.main.transform.position.z - uiElement.position.z);
        worldPosition.y += yOffset;

        return worldPosition;
    }
    public static Vector3 ConvertToWorldSpace(this RectTransform rectTransform, Transform transform)
    {
        // Get the position of the UI element in screen space
        Vector3 screenPosition = rectTransform.position;

        // Convert the screen position to world space
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        float yOffset = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        worldPosition.y = yOffset;

        return worldPosition;
    }
}