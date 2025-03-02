using UnityEngine;
using DG.Tweening;
public class RotateTrigger : MonoBehaviour, ITrigger
{
    [SerializeField] private Transform Target;
    [SerializeField] private float RotateDuration;
    [SerializeField] private Vector3 RotationToRotate;
    [SerializeField] private Ease Ease;
    public void TriggerAction()
    {
        Target.DORotate(RotationToRotate, RotateDuration).SetEase(Ease);
    }
}
