using UnityEngine;
using DG.Tweening;
public class TaskBarDirector : MonoBehaviour
{
    private RectTransform Panel;
    void Start()
    {
        Panel = GetComponent<RectTransform>();
        //Panel.DOAnchorMax()
    }

    void Update()
    {
    }
}
