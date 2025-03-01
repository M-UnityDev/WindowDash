using UnityEngine;

public class TriggerDetect : MonoBehaviour
{
    //private bool isPlayerDetected;
    public bool isPlayerDetected {get; private set;}
    private void OnCollisionEnter(Collision collision)
    {
        isPlayerDetected = collision.gameObject.CompareTag("Player");
    }
}
