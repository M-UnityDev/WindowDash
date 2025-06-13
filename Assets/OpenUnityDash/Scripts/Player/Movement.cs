using UnityEngine;
using Game.Input;
public class Movement : MonoBehaviour
{
    [SerializeField] private int Speed;
    public int SpeedF { get => Speed; set => Speed = value; }
    [SerializeField] private float JumpForce;
    [SerializeField] private float JumpRotationSpeed;
    [SerializeField] private bool IsGround = true;
    [SerializeField] private LayerMask MaskToLayer;
    private Vector3 DeadTriggerScale;
    private Collider[] Colliders;
    private Rigidbody RigidBody;
    private Vector3 Velocity;
    [SerializeField] private int Direction = 1;
    public int DirectionF { get => Direction; set => Direction = value; }
    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody>();
        DeadTriggerScale = transform.localScale / 2;
    }
    private void OnCollisionStay(Collision collision)
    {
        RigidBody.angularVelocity = new Vector3(0,0,0);
        IsGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        IsGround = false;
    }
    private void CheckCollison()
    {
        Colliders = Physics.OverlapBox(transform.position, DeadTriggerScale / 2, new Quaternion(0,0,0,0), MaskToLayer);
        foreach (Collider collider in Colliders)
        {
            if (collider.isTrigger)
            {
                Destroy(gameObject);
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, DeadTriggerScale);
    }
    private void FixedUpdate()
    {
        Move();
        CheckCollison();
        if (InputHandler.Jump.IsPressed() && IsGround) Jump();
    }
    private void Move()
    {
        Velocity.Set(Speed*Direction, RigidBody.linearVelocity.y, RigidBody.linearVelocity.z);
        RigidBody.linearVelocity = Velocity;
    }
    public void Jump()
    {
        IsGround = false;
        RigidBody.angularVelocity = new Vector3(0, 0, 0);
        RigidBody.linearVelocity = new Vector3(RigidBody.linearVelocity.x,JumpForce,RigidBody.linearVelocity.z);
        RigidBody.angularVelocity = new Vector3(0, 0, -JumpRotationSpeed*Direction);
    }
}
