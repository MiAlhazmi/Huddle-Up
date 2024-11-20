using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// TODO: Enum for the timers
public class TimerControl : MonoBehaviour
{
    public CircularTimer[] circularTimers;
    public Text text;

    public void StartTimer()
    {
        text.text = "Start";
        foreach (CircularTimer timer in circularTimers)
        {
            timer.StartTimer();
        }
    }

    public void StartTimerNumber(int index)
    {
        circularTimers[index].gameObject.SetActive(true);
        circularTimers[index].StartTimer();
    }

    public void PauseTimer()
    {
        text.text = "Pause";
        foreach (CircularTimer timer in circularTimers)
        {
            timer.PauseTimer();
        }
    }

    public void StopTimer()
    {
        text.text = "Stop";
        foreach (CircularTimer timer in circularTimers)
        {
            timer.StopTimer();
        }
    }

    public void DidFinishedTimer()
    {
        text.text = "Finished";
        Debug.Log("Timer finished");
    }

    public void ResetTimer(int index)
    {
        // circularTimers[index].ResetTimer();
        circularTimers[index].StopTimer();
    }
    
    public void ResetAllTimers()
    {
        // foreach (var aTimer in circularTimers)
        // {
        //     aTimer.ResetTimer();
        // }

        for (int i = 0; i < circularTimers.Length; i++)
        {
            ResetTimer(i);
        }
    }

    public CircularTimer GetTimer(int index)
    {
        return circularTimers[index];
    }
}
