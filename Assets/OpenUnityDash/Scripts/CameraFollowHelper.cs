using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
public class CameraFollowHelper : MonoBehaviour
{
    [SerializeField] private Transform Player;
    private void Awake()
    {
        StartCoroutine("StartFollow");
    }
    private IEnumerator StartFollow()
    {
        yield return new WaitUntil(() => Player.position.x >= transform.position.x);
        GetComponent<CinemachineCamera>().Target.TrackingTarget = Player;
    }
}
