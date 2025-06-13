using UnityEngine;
public class ReverseTrigger : MonoBehaviour, ITrigger
{
    private int Direction;
    public void TriggerAction()
    {
        Direction = GameObject.Find("MainPlayer").GetComponent<Movement>().DirectionF;
        GameObject.Find("MainPlayer").GetComponent<Movement>().DirectionF = Direction * -1;
    }
}
