using UnityEngine;
public class TriggerDetect : MonoBehaviour
{

    private void Awake()
    {
        
    }
    private void OnTriggerEnter(Collider Other) => DoAction(Other);
    private void DoAction(Collider Other)
    {
        if (Other.gameObject.CompareTag("Player") && TryGetComponent(out ITrigger Trigger))
        {
            Trigger.TriggerAction();
        }
    }
}
