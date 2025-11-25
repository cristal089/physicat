using UnityEngine;

public class SpeechBubbleController : MonoBehaviour
{
    [SerializeField] Transform player;           // player world transform
    [SerializeField] RectTransform bubbleUI;     // bubble RectTransform
    [SerializeField] Vector3 offset;             // screen-space offset (pixels)

    void LateUpdate()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(player.position);
        bubbleUI.position = screenPos + offset;
    }
}
