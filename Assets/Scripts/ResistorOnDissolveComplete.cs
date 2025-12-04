using UnityEngine;

public class ResistorOnDissolveComplete : MonoBehaviour
{
    public void OnDissolveComplete()
    {
        gameObject.SetActive(false);
    }
}
