using UnityEngine;
public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 v;
    [SerializeField] private int spd;
    [SerializeField] private int jmpspd;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();    
    }
    private void Update()
    {
        v.Set(spd, rb.linearVelocity.y, rb.linearVelocity.z);
        rb.linearVelocity = v;
    }
}
