using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController
{
    const float DELTA_TIME_MAX = 1.0f;
    float _time = 0.0f;
    float _inv_time_max = 1.0f;

    public void Set(float max_time)
    {
        Debug.Assert(0.0f < max_time);

        _time = max_time;
        _inv_time_max = 1.0f / max_time;
    }

    public bool Update(float delta_Time)
    {
        if (DELTA_TIME_MAX < delta_Time) delta_Time = DELTA_TIME_MAX;

        _time -= delta_Time;

        if (_time <= 0.0f) 
        {
            _time = 0.0f;
            return false;
        }

        return true;
    }

    public float GetNormalized()
    {
        return _time* _inv_time_max;
    }
}
