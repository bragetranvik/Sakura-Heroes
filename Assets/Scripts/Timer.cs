using UnityEngine;
using Fungus;

[EventHandlerInfo("Timers",
    "Simple Timer",
    "Executes the block after an amount of time has elapsed")]

public class Timer : EventHandler
{
    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("TimerExpired", duration);
    }

    void TimerExpired()
    {
        ExecuteBlock();
    }
}
