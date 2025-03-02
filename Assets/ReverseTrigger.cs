using UnityEngine;
public class ReverseTrigger : MonoBehaviour, ITrigger
{
    private int Speed;
    public void TriggerAction()
    {
        Speed = GameObject.Find("MainPlayer").GetComponent<Movement>().SpeedF;
        GameObject.Find("MainPlayer").GetComponent<Movement>().SpeedF = Speed * -1;
    }
}
