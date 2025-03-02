using UnityEngine;
using DG.Tweening;
public class MoveTrigger : MonoBehaviour, ITrigger
{
    [SerializeField] private Transform Target;
    [SerializeField] private float MoveDuration;
    [SerializeField] private Vector3 PositionToMove;
    [SerializeField] private Ease Ease;
    public void TriggerAction()
    {
        Target.DOLocalMove(PositionToMove, MoveDuration).SetEase(Ease);
    }
}
