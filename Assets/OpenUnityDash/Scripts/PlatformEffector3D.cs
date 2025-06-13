using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlatformEffector3D : MonoBehaviour
{
    [Tooltip("The direction that the other object should be coming from for entry.")]
    [SerializeField] private Vector3 entryDirection = Vector3.up;
    [Tooltip("How large should the trigger be in comparison to the original collider?")]
    [SerializeField] private Vector3 triggerScale = Vector3.one * 1.25f;
    [Tooltip("The collision will activate only when the penetration depth of the intruder is smaller than this threshold.")]
    [SerializeField]
    private float penetrationDepthThreshold = 0.2f;

    [Tooltip("The original collider. Will always be present thanks to the RequireComponent attribute.")]
    private BoxCollider ObjectCollider = null;
    [Tooltip("The trigger that we add ourselves once the game starts up.")]
    private BoxCollider CollisionCheckTrigger = null;

    /// <summary> The entry direction, calculated accordingly based on whether it is a local direction or not. 
    /// This is pretty much what I've done in the video when copy-pasting the local direction check, but written as a property. </summary>
    public Vector3 PassthroughDirection => transform.TransformDirection(entryDirection.normalized);

    private void Awake()
    {
        ObjectCollider = GetComponent<BoxCollider>();

        // Adding the BoxCollider and making sure that its sizes match the ones
        // of the OG collider.
        CollisionCheckTrigger = gameObject.AddComponent<BoxCollider>();
        CollisionCheckTrigger.size = new Vector3(
            ObjectCollider.size.x * triggerScale.x,
            ObjectCollider.size.y * triggerScale.y,
            ObjectCollider.size.z * triggerScale.z
        );
        CollisionCheckTrigger.center = ObjectCollider.center;
        CollisionCheckTrigger.isTrigger = true;
    }

    private void OnValidate()
    {
        ObjectCollider = GetComponent<BoxCollider>();
        ObjectCollider.isTrigger = false;
    }

    private void OnTriggerStay(Collider other)
    {
        TryIgnoreCollision(other);
    }

    public void TryIgnoreCollision(Collider other)
    {
        // Simulate a collision between our trigger and the intruder to check
        // the direction that the latter is coming from. The method returns true
        // if any collision has been detected.
        if (Physics.ComputePenetration(
            CollisionCheckTrigger, CollisionCheckTrigger.bounds.center, transform.rotation,
            other, other.bounds.center, other.transform.rotation,
            out Vector3 collisionDirection, out float penetrationDepth))
        {
            float dot = Vector3.Dot(PassthroughDirection, collisionDirection);

            // Opposite direction; passing not allowed.
            if (dot < 0)
            {
                // Activate collison only once the intruder is close enough to the trigger border, to avoid teleportation
                if(penetrationDepth < penetrationDepthThreshold) 
                {
                    // Making sure that the two object are NOT ignoring each other.
                    Physics.IgnoreCollision(ObjectCollider, other, false);
                }
            }
            else
            {
                // Making the colliders ignore each other, and thus allow passage
                // from one way.
                Physics.IgnoreCollision(ObjectCollider, other, true);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Instead of directly using the transform.position, we're using the
        // collider center, converted into global position. The way I did it
        // in the video made it easier to write, but the on-screen drawing would
        // not take in consideration the actual offset of the collider.
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.TransformPoint(ObjectCollider.center), PassthroughDirection * transform.localScale.y);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.TransformPoint(ObjectCollider.center), -PassthroughDirection * transform.localScale.y);
    }
}