using UnityEngine;

public class SwipeLogger : MonoBehaviour
{
    private void SwipeDetector_OnSwipe(SwipeData data)
    {
        Debug.Log("Swipe in Direction: " + data.Direction);
    }
}
