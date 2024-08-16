using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatTweening : Tweening<float>
{
    private float beginningValue;

    private float endingValue;

    private float valuesRange;


    public float beginningFrameValue;
    public float rangeFrameValue;
    public float computedValue;

    public FloatTweening() : base()
    {
        beginningValue = 0.0f;
        endingValue = 0.0f;
        valuesRange = 0.0f;
        computedValue = 0.0f;

        setupValueForFrame = SetupValueForFrameDelegate;
        revertDataForUpdate = ReverseDataForFrame;
        computeValue = ComputeValueDelegate;
        returnValue = ReturnComputedValue;

    }

    public void SetupValueForFrameDelegate()
    {
        beginningFrameValue = beginningValue;
        rangeFrameValue = valuesRange;
    }

    public void ReverseDataForFrame()
    {
        beginningFrameValue = endingValue;
        rangeFrameValue = -valuesRange;
    }

    public void ComputeValueDelegate(float tmpTime, float aimedTime, float? addValue1, float? addValue2)
    {

        if (aimedTime == 0)
            computedValue = endingValue;
        else
            computedValue = Evaluate(Type, Behaviour, tmpTime, beginningFrameValue, rangeFrameValue, aimedTime,
                addValue1, addValue2);
    }

    public float ReturnComputedValue()
    {
        return computedValue;
    }


    public FloatTweening InitTwoValue(float time, float beginningValue, float endValue)
    {
        return InitRangeValue(time, beginningValue, endValue - beginningValue);
    }


    public FloatTweening InitRangeValue(float time, float beginningValue, float range)
    {
        this.beginningValue = beginningValue;
        valuesRange = range;

        endingValue = range + beginningValue;

        computedValue = beginningValue;

        StartTimeAfterInitValues(time);

        return this;
    }
}
