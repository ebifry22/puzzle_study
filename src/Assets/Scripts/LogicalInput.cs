using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicalInput
{
    [Flags]
    public enum Key
    {
        Right=1<<0,
        Left=1<<1,
        RotR=1<<2,
        RotL=1<<3,
        QuickDrop=1<<4,
        Down=1<<5,

        MAX=6,
    }

    const int KEY_REPEAT_START_TIME = 12;
    const int KEY_REPEAT_ITERATION_TIME = 1;

    Key inputRaw;
    Key inputTrg;
    Key inputRel;
    Key inputRep;
    int[] _trgWaitingTime = new int[(int)Key.MAX];

    public bool IsRaw(Key K)
    {
        return inputRaw.HasFlag(K);
    }
    public bool IsTrigger(Key K)
    {
        return inputTrg.HasFlag(K);
    }
    public bool IsRelease(Key K) 
    { 
        return inputRel.HasFlag(K);
    }
    public bool IsRepeat(Key K)
    {
        return inputRep.HasFlag(K);
    }

    public void Clear()
    {
        inputRaw = 0;
        inputTrg = 0;
        inputRel = 0;
        inputRep = 0;
        for(int i=0;i<(int)Key.MAX;i++)
        {
            _trgWaitingTime[i] = 0;
        }
    }

    // Update is called once per frame
    public void Update(Key inputDev)
    {
        //入力が入った/抜けた
        inputTrg = (inputDev ^ inputRaw) & inputDev;
        inputRel = (inputDev ^ inputRaw) & inputRaw;

        //生データの生成
        inputRaw = inputDev;

        //キーリピートの生成
        inputRep = 0;
        for(int i=0;i<(int)Key.MAX;i++)
        {
            if(inputTrg.HasFlag((Key)(1<<i)))
            {
                inputRep |= (Key)(1 << i);
                _trgWaitingTime[i] = KEY_REPEAT_START_TIME;
            }
            else
            if(inputRaw.HasFlag((Key)(1<<i)))
            {
                if (--_trgWaitingTime[i]<=0)
                {
                    inputRaw= (Key)(1<<i);
                    _trgWaitingTime[i] = KEY_REPEAT_ITERATION_TIME;
                }
            }
        }
    }
}
