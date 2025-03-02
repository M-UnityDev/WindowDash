using System.Diagnostics;
using UnityEngine;
public class RunExecutableTrigger : MonoBehaviour, ITrigger
{
    [SerializeField] private string PathToExecutable = "C:\\Windows\\System32\\cmd.exe";
    [SerializeField] private string ArgumentsForExecutable = "";
    public void TriggerAction()
    {
        Process p = new Process();
        p.StartInfo.UseShellExecute = true;
        p.StartInfo.FileName = PathToExecutable;
        p.StartInfo.Arguments = ArgumentsForExecutable;
        p.Start();
    }
}
