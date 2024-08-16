using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3Tweening : Tweening<Vector3>
{
    private Vector3 beginningValue;

    private Vector3 endingValue;

    private Vector3 valuesRange;


    public Vector3 beginningFrameValue;
    public Vector3 rangeFrameValue;
    public Vector3 computedValue;

    public Vector3Tweening() : base()
    {
        beginningValue = Vector3.zero;
        endingValue = Vector3.zero;
        valuesRange = Vector3.zero;
        computedValue = Vector3.zero;

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
        {

            computedValue.x = Evaluate(Type, Behaviour, tmpTime, beginningFrameValue.x, rangeFrameValue.x, aimedTime,
                addValue1, addValue2);

            computedValue.y = Evaluate(Type, Behaviour, tmpTime, beginningFrameValue.y, rangeFrameValue.y, aimedTime,
               addValue1, addValue2);

            computedValue.z = Evaluate(Type, Behaviour, tmpTime, beginningFrameValue.z, rangeFrameValue.z, aimedTime,
               addValue1, addValue2);
        }
    }

    public Vector3 ReturnComputedValue()
    {
        return computedValue;
    }


    public Vector3Tweening InitTwoValue(float time, Vector3 beginningValue, Vector3 endValue)
    {
        return InitRangeValue(time, beginningValue, endValue - beginningValue);
    }


    public Vector3Tweening InitRangeValue(float time, Vector3 beginningValue, Vector3 range)
    {
        this.beginningValue = beginningValue;
        valuesRange = range;

        endingValue = range + beginningValue;

        computedValue = beginningValue;

        StartTimeAfterInitValues(time);

        return this;
    }
}
