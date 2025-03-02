using UnityEngine;
public class DebugDirector : MonoBehaviour
{
    [SerializeField] LayerMask LayersToShow;
    [SerializeField] private bool HideDebugObjectsInEditor;
    private void Awake()
    {
#if !UNITY_EDITOR
    HideDebug();
    return;
#endif
        if (HideDebugObjectsInEditor)
            HideDebug();
    }
    private void HideDebug()
    {
        foreach (Camera c in FindObjectsByType<Camera>(FindObjectsSortMode.None))
            c.cullingMask = LayersToShow;
    }
}
