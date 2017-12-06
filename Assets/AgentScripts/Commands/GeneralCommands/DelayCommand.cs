using UnityEngine;

public class DelayCommand : BaseCommand
{
    protected float _startTime;

    public DelayCommand(float durationMs)
    {
        Duration = durationMs/1000f;
    }

    public float Duration { get; protected set; }

    public override void Execute()
    {
        _startTime = Time.time;
        InProgress = true;
    }

    public override void OnUpdate()
    {
        if (Time.time > _startTime + Duration)
        {
            InProgress = false;
        }
    }
}