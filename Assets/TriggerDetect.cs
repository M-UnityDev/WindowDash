using UnityEngine;
public class TriggerDetect : MonoBehaviour
{
    [SerializeField] private bool MultiTrigger;
    private bool IsComplete;
    private void Awake()
    {

    }
    private void OnTriggerEnter(Collider Other) => DoAction(Other);
    private void DoAction(Collider Other)
    {
        if (!MultiTrigger && IsComplete) return;
        if (Other.gameObject.CompareTag("Player") && TryGetComponent(out ITrigger Trigger))
        {
            Trigger.TriggerAction();
            IsComplete = true;
        }
    }
}
