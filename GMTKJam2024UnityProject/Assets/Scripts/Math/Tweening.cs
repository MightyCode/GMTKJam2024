using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ETweeningOption
{
    Direct,
    DirectMirrored,
    DirectReversed,
    Loop,
    LoopMirrored,
    LoopReversed
}

public enum ETweeningBehaviour
{
    In, 
    Out, 
    InOut
}

public enum ETweeningType
{
    Linear,
    Quadratic,
    Cubic,
    Quartic,
    Quintic,
    Sinusoidal,
    Exponential,
    Circular,
    Elastic,
    Back,
    Bounce
}

public class Tweening<T>
{
    public static float Evaluate(ETweeningType type, ETweeningBehaviour behaviour, float t, float b, float c, float d, float? addValue1, float? addValue2)
    {
        switch (type)
        {
            case ETweeningType.Linear:
                return Linear.Evaluate(behaviour, t, b, c, d);
            case ETweeningType.Quadratic:
                return Quadratic.Evaluate(behaviour, t, b, c, d);
            case ETweeningType.Cubic:
                return Cubic.Evaluate(behaviour, t, b, c, d);
            case ETweeningType.Quartic:
                return Quartic.Evaluate(behaviour, t, b, c, d);
            case ETweeningType.Quintic:
                return Quintic.Evaluate(behaviour, t, b, c, d);
            case ETweeningType.Sinusoidal:
                return Sinusoidal.Evaluate(behaviour, t, b, c, d);
            case ETweeningType.Exponential:
                return Exponential.Evaluate(behaviour, t, b, c, d);
            case ETweeningType.Circular:
                return Cicular.Evaluate(behaviour, t, b, c, d);
            case ETweeningType.Elastic:
                return Elastic.Evaluate(behaviour, t, b, c, d,
                        addValue1, addValue2);
            case ETweeningType.Back:
                return Back.Evaluate(behaviour, t, b, c, d,
                        addValue1);
            case ETweeningType.Bounce:
                return Bounce.Evaluate(behaviour, t, b, c, d);
            default:
                return 0.0f;
        }
    }

    public static float Evaluate(ETweeningType type, ETweeningBehaviour behaviour, float t, float b, float c, float d)
    {
        return Evaluate(type, behaviour, t, b, c, d, null, null);
    }

    public ETweeningType Type { get; private set; }

    public ETweeningBehaviour Behaviour { get; private set; }

    public ETweeningOption Option { get; private set; }

    public T Value { get; private set; }

    public bool Reversed { get; private set; }
    public bool Mirrored { get; private set; }


    Timer timer;

    private float? addValue1, addValue2;

    public delegate void SetupValueForFrame();
    public delegate void RevertDataForUpdate();
    public delegate void ComputeValue(float tmpTime, float aimedTime, float? addValue1, float? addValue2);
    public delegate T ReturnValue();

    protected SetupValueForFrame setupValueForFrame;
    protected RevertDataForUpdate revertDataForUpdate;
    protected ComputeValue computeValue;
    protected ReturnValue returnValue;

    public Tweening()
    {
        Type = ETweeningType.Linear;
        Behaviour = ETweeningBehaviour.In;
        Option = ETweeningOption.Direct;

        timer = new Timer();



        addValue1 = null;
        addValue2 = null;
    }

    public void Update()
    {
        timer.Update();

        if (timer.Finished)
        {
            if (Option == ETweeningOption.DirectMirrored)
            {
                if (!Mirrored)
                {
                    Mirrored = true;
                    timer.ResetStart();
                }
            }
            else if (Option == ETweeningOption.DirectReversed)
            {
                if (!Reversed)
                {
                    Reversed = true;
                    timer.ResetStart();
                }
            }
            else if (Option == ETweeningOption.Loop)
            {
                float plus = timer.ElapsedTime - timer.AimedTime;
                timer.ResetStart();
                timer.ForceTime(plus);
            }
            else if (Option == ETweeningOption.LoopReversed)
            {
                float plus = timer.ElapsedTime - timer.AimedTime;
                Reversed = !Reversed;
                timer.ResetStart();
                timer.ForceTime(plus);
            }
            else if (Option == ETweeningOption.LoopMirrored)
            {
                float plus = timer.ElapsedTime - timer.AimedTime;
                Mirrored = !Mirrored;
                timer.ResetStart();
                timer.ForceTime(plus);
            }
        }

        setupValueForFrame();

        float tmpTimeValue = timer.ElapsedTime;

        if (Reversed)
        {
            tmpTimeValue = timer.AimedTime - tmpTimeValue;
        }

        if (Mirrored)
        {
            revertDataForUpdate();
        }

        computeValue(tmpTimeValue, timer.AimedTime, addValue1, addValue2);
        Value = returnValue();
    }

    public Tweening<T> SetTweeningOption(ETweeningOption tweeningOption)
    {
        Option = tweeningOption;

        timer.ElapsedTimeCapped = ((
                tweeningOption == ETweeningOption.Loop
                        || tweeningOption == ETweeningOption.LoopMirrored
                        || tweeningOption == ETweeningOption.LoopReversed));

        return this;
    }

    public Tweening<T> SetTweeningValues(ETweeningType type, ETweeningBehaviour behaviour)
    {
        Type = type;
        Behaviour = behaviour;

        return this;
    }
    
    protected void StartTimeAfterInitValues(float time)
    {
        timer.Start(time);
    }


    public Tweening<T> SetAdditionnalArguments(float? arg1, float? arg2)
    {
        addValue1 = arg1;
        addValue2 = arg2;

        return this;
    }


    public Tweening<T> InitRandomTweening()
    {
        timer.InitRandomTime();

        if (Option == ETweeningOption.DirectMirrored
                || Option == ETweeningOption.LoopMirrored)
        {
            if (Random.Range(0f, 1f) >= 0.5f)
            {
                Mirrored = true;
            }
        }
        else if (this.Option == ETweeningOption.DirectReversed
              || this.Option == ETweeningOption.LoopReversed)
        {
            if (Random.Range(0f, 1f) >= 0.5f)
            {
                Reversed = true;
            }
        }

        return this;
    }


    // Not is in infinityLoop
    public bool Finished
    {
        get
        {
            if (Option == ETweeningOption.Direct)
            {
                return (timer.Finished || (!timer.Started && !timer.Stopped));
            }
            else if (Option == ETweeningOption.DirectReversed)
            {
                return (timer.Finished || (!timer.Started && !timer.Stopped)) && Reversed;
            }
            else if (Option == ETweeningOption.DirectMirrored)
            {
                return (timer.Finished || (!timer.Started && !timer.Stopped)) && Mirrored;
            }

            return false;
        }
    }


    public Tweening<T> Reset()
    {
        Reversed = false;
        Mirrored = false;

        timer.ResetStart();

        return this;
    }
}

public class Linear
{
    public static float Evaluate(ETweeningBehaviour behaviour,
                                 float t, float b, float c, float d)
    {
        switch (behaviour)
        {
            case ETweeningBehaviour.In:
                return In(t, b, c, d);
            case ETweeningBehaviour.Out:
                return Out(t, b, c, d);
            case ETweeningBehaviour.InOut:
                return InOut(t, b, c, d);
            default:
                return 0.0f;
        }
    }

    public static float In(float t, float b, float c, float d)
    {
        return c * t / d + b;
    }

    public static float Out(float t, float b, float c, float d)
    {
        return In(t, b, c, d);
    }

    public static float InOut(float t, float b, float c, float d)
    {
        return In(t, b, c, d);
    }
}


public class Quadratic
{

    public static float Evaluate(ETweeningBehaviour behaviour,
                                 float t, float b, float c, float d)
    {
        switch (behaviour)
        {
            case ETweeningBehaviour.In:
                return In(t, b, c, d);
            case ETweeningBehaviour.Out:
                return Out(t, b, c, d);
            case ETweeningBehaviour.InOut:
                return InOut(t, b, c, d);
            default:
                return 0.0f;
        }
    }

    public static float In(float t, float b, float c, float d)
    {
        return c * (t /= d) * t + b;
    }


    public static float Out(float t, float b, float c, float d)
    {
        return -c * (t /= d) * (t - 2) + b;
    }


    public static float InOut(float t, float b, float c, float d)
    {
        if ((t /= d / 2) < 1)
        {
            return c / 2 * t * t + b;
        }

        return -c / 2 * ((--t) * (t - 2) - 1) + b;
    }
}

public class Cubic
{
    public static float Evaluate(ETweeningBehaviour behaviour,
                                 float t, float b, float c, float d)
    {
        switch (behaviour)
        {
            case ETweeningBehaviour.In:
                return In(t, b, c, d);
            case ETweeningBehaviour.Out:
                return Out(t, b, c, d);
            case ETweeningBehaviour.InOut:
                return InOut(t, b, c, d);
            default:
                return 0.0f;
        }
    }

    public static float In(float t, float b, float c, float d)
    {
        return c * (t /= d) * t * t + b;
    }


    public static float Out(float t, float b, float c, float d)
    {
        return c * ((t = t / d - 1) * t * t + 1) + b;
    }


    public static float InOut(float t, float b, float c, float d)
    {
        if ((t /= d / 2) < 1)
        {
            return c / 2 * t * t * t + b;
        }

        return c / 2 * ((t -= 2) * t * t + 2) + b;
    }
}


public class Quartic
{
    public static float Evaluate(ETweeningBehaviour behaviour,
                                 float t, float b, float c, float d)
    {
        switch (behaviour)
        {
            case ETweeningBehaviour.In:
                return In(t, b, c, d);
            case ETweeningBehaviour.Out:
                return Out(t, b, c, d);
            case ETweeningBehaviour.InOut:
                return InOut(t, b, c, d);
            default:
                return 0.0f;
        }
    }

    public static float In(float t, float b, float c, float d)
    {
        return c * (t /= d) * t * t * t + b;
    }


    public static float Out(float t, float b, float c, float d)
    {
        return -c * ((t = t / d - 1) * t * t * t - 1) + b;
    }


    public static float InOut(float t, float b, float c, float d)
    {
        if ((t /= d / 2) < 1)
        {
            return c / 2 * t * t * t * t + b;
        }

        return -c / 2 * ((t -= 2) * t * t * t - 2) + b;
    }
}

public class Quintic
{
    public static float Evaluate(ETweeningBehaviour behaviour,
                                 float t, float b, float c, float d)
    {
        switch (behaviour)
        {
            case ETweeningBehaviour.In:
                return In(t, b, c, d);
            case ETweeningBehaviour.Out:
                return Out(t, b, c, d);
            case ETweeningBehaviour.InOut:
                return InOut(t, b, c, d);
            default:
                return 0.0f;
        }
    }

    public static float In(float t, float b, float c, float d)
    {
        return c * (t /= d) * t * t * t * t + b;
    }


    public static float Out(float t, float b, float c, float d)
    {
        return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
    }


    public static float InOut(float t, float b, float c, float d)
    {
        if ((t /= d / 2) < 1)
        {
            return c / 2 * t * t * t * t * t + b;
        }

        return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
    }
}

public class Sinusoidal
{

    public static float Evaluate(ETweeningBehaviour behaviour,
                                 float t, float b, float c, float d)
    {
        switch (behaviour)
        {
            case ETweeningBehaviour.In:
                return In(t, b, c, d);
            case ETweeningBehaviour.Out:
                return Out(t, b, c, d);
            case ETweeningBehaviour.InOut:
                return InOut(t, b, c, d);
            default:
                return 0.0f;
        }
    }

    public static float In(float t, float b, float c, float d)
    {
        return -c * (float)Mathf.Cos(t / d * (Mathf.PI / 2)) + c + b;
    }


    public static float Out(float t, float b, float c, float d)
    {
        return c * (float)Mathf.Sin(t / d * (Mathf.PI / 2)) + b;
    }


    public static float InOut(float t, float b, float c, float d)
    {
        return -c / 2 * (float)(Mathf.Cos(Mathf.PI * t / d) - 1) + b;
    }
}

public class Exponential
{

    public static float Evaluate(ETweeningBehaviour behaviour,
                                 float t, float b, float c, float d)
    {
        switch (behaviour)
        {
            case ETweeningBehaviour.In:
                return In(t, b, c, d);
            case ETweeningBehaviour.Out:
                return Out(t, b, c, d);
            case ETweeningBehaviour.InOut:
                return InOut(t, b, c, d);
            default:
                return 0.0f;
        }
    }

    public static float In(float t, float b, float c, float d)
    {
        return (t == 0) ? b : c * (float)Mathf.Pow(2, 10 * (t / d - 1)) + b;
    }


    public static float Out(float t, float b, float c, float d)
    {
        return (t == d) ? b + c : c * (float)(-Mathf.Pow(2, -10 * t / d) + 1) + b;
    }


    public static float InOut(float t, float b, float c, float d)
    {
        if (t == 0)
            return b;

        if (t == d)
            return b + c;

        if ((t /= d / 2) < 1)
            return c / 2 * (float)Mathf.Pow(2, 10 * (t - 1)) + b;

        return c / 2 * (float)(-Mathf.Pow(2, -10 * --t) + 2) + b;
    }
}

public class Cicular
{

    public static float Evaluate(ETweeningBehaviour behaviour,
                                 float t, float b, float c, float d)
    {
        switch (behaviour)
        {
            case ETweeningBehaviour.In:
                return In(t, b, c, d);
            case ETweeningBehaviour.Out:
                return Out(t, b, c, d);
            case ETweeningBehaviour.InOut:
                return InOut(t, b, c, d);
            default:
                return 0.0f;
        }
    }

    public static float In(float t, float b, float c, float d)
    {
        return -c * (float)(Mathf.Sqrt(1 - (t /= d) * t) - 1) + b;
    }


    public static float Out(float t, float b, float c, float d)
    {
        return c * (float)Mathf.Sqrt(1 - (t = t / d - 1) * t) + b;
    }


    public static float InOut(float t, float b, float c, float d)
    {
        if ((t /= d / 2) < 1)
        {
            return -c / 2 * (float)(Mathf.Sqrt(1 - t * t) - 1) + b;
        }

        return c / 2 * (float)(Mathf.Sqrt(1 - (t -= 2) * t) + 1) + b;
    }
}

public class Elastic
{

    public static float Evaluate(ETweeningBehaviour behaviour,
                                 float t, float b, float c, float d,
                                float? a, float? p)
    {
        switch (behaviour)
        {
            case ETweeningBehaviour.In:
                return Elastic.InFull(t, b, c, d, a, p);
            case ETweeningBehaviour.Out:
                return Elastic.OutFull(t, b, c, d, a, p);
            case ETweeningBehaviour.InOut:
                return Elastic.InOutFull(t, b, c, d, a, p);
            default:
                return 0.0f;
        }
    }


    public static float InFull(float t, float b, float c, float d,
                           float? a, float? p)
    {

        if (a == null || p == null)
            return In(t, b, c, d);


        float s;
        if (t == 0)
            return b;

        if ((t /= d) == 1)
            return b + c;

        if (a < Mathf.Abs(c))
        {
            a = c;
            s = p.Value / 4;
        }
        else
        {
            s = p.Value / (2 * Mathf.PI) * Mathf.Asin(c / a.Value);
        }

        return -
                (a.Value
                 * Mathf.Pow(2, 10 * (t -= 1))
                 * Mathf.Sin((t * d - s)
                 * (2 * Mathf.PI) / p.Value))
                 + b;
    }


    public static float In(float t, float b, float c, float d)
    {
        if (t == 0)
            return b;

        if ((t /= d) == 1)
            return b + c;

        float p = d * 0.3f;
        float a = c;
        float s = p / 4;

        return -(float)(
                a
                * Mathf.Pow(2, 10 * (t -= 1))
                * Mathf.Sin((t * d - s)
                * (2 * Mathf.PI) / p)) + b;
    }


    public static float OutFull(float t, float b, float c, float d,
                               float? a, float? p)
    {

        if (a == null || p == null)
            return Elastic.Out(t, b, c, d);


        float s;
        if (t == 0)
            return b;

        if ((t /= d) == 1)
            return b + c;

        if (a < Mathf.Abs(c))
        {
            a = c;
            s = p.Value / 4;
        }
        else
        {
            s = p.Value / (2 * Mathf.PI) * Mathf.Asin(c / a.Value);
        }

        return (a.Value
                * Mathf.Pow(2, -10 * t)
                * Mathf.Sin((t * d - s)
                * (2 * Mathf.PI) / p.Value) + c + b);
    }


    public static float Out(float t, float b, float c, float d)
    {
        if (t == 0)
            return b;

        if ((t /= d) == 1)
            return b + c;

        float p = d * 0.3f;
        float a = c;
        float s = p / 4;

        return (a
                * Mathf.Pow(2, -10 * t)
                * Mathf.Sin((t * d - s)
                * (2 * Mathf.PI) / p) + c + b);
    }


    public static float InOutFull(float t, float b, float c, float d,
                               float? a, float? p)
    {
        if (a == null || p == null)
            return Elastic.InOut(t, b, c, d);

        float s;
        if (t == 0)
            return b;

        if ((t /= d / 2) == 2)
            return b + c;

        if (a < Mathf.Abs(c))
        {
            a = c;
            s = p.Value / 4;
        }
        else
        {
            s = p.Value / (2 * Mathf.PI) * Mathf.Asin(c / a.Value);
        }

        if (t < 1)
            return (-0.5f * (a.Value
                    * Mathf.Pow(2, 10 * (t -= 1))
                    * Mathf.Sin((t * d - s)
                    * (2 * Mathf.PI) / p.Value)) + b);

        return (a.Value
                * Mathf.Pow(2, -10 * (t -= 1))
                * Mathf.Sin((t * d - s)
                * (2 * Mathf.PI) / p.Value)
                * 0.5f + c + b);

    }

    public static float InOut(float t, float b, float c, float d)
    {
        if (t == 0)
            return b;

        if ((t /= d / 2) == 2)
            return b + c;

        float p = d * (0.3f * 1.5f);
        float a = c;
        float s = p / 4;

        if (t < 1)
            return (-0.5f * (a
                    * Mathf.Pow(2, 10 * (t -= 1))
                    * Mathf.Sin((t * d - s)
                    * (2 * Mathf.PI) / p)) + b);

        return (a
                * Mathf.Pow(2, -10 * (t -= 1))
                * Mathf.Sin((t * d - s)
                * (2 * Mathf.PI) / p)
                * 0.5f + c + b);
    }
}


public class Back
{

    public const float SWING_BACK_DEFAULT = 1.70158f;
    public const float SWING_MULTIPLICATOR = 1.525f;


    public static float Evaluate(ETweeningBehaviour behaviour,
                                 float t, float b, float c, float d,
                                 float? s)
    {
        switch (behaviour)
        {
            case ETweeningBehaviour.In:
                return InFull(t, b, c, d, s);
            case ETweeningBehaviour.Out:
                return OutFull(t, b, c, d, s);
            case ETweeningBehaviour.InOut:
                return InOutFull(t, b, c, d, s);
            default:
                return 0.0f;
        }
    }


    public static float InFull(float t, float b, float c, float d,
                               float? s)
    {

        if (s == null)
            return In(t, b, c, d);

        return c * (t /= d) * t * ((s.Value + 1) * t - s.Value) + b;
    }


    public static float In(float t, float b, float c, float d)
    {
        float s = SWING_BACK_DEFAULT;

        return c * (t /= d) * t * ((s + 1) * t - s) + b;
    }


    public static float OutFull(float t, float b, float c, float d,
                                float? s)
    {
        return c * ((t = t / d - 1) * t * ((s.Value + 1) * t + s.Value) + 1) + b;
    }


    public static float Out(float t, float b, float c, float d)
    {
        float s = SWING_BACK_DEFAULT;

        return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b;
    }


    public static float InOutFull(float t, float b, float c, float d,
                                  float? s)
    {
        if (s == null)
            return InOut(t, b, c, d);

        float ss = s.Value;

        if ((t /= d / 2) < 1)
            return c / 2 * (t * t *
                    (((ss *= (SWING_MULTIPLICATOR)) + 1) * t - ss)) + b;

        return c / 2 * ((t -= 2) * t *
                (((ss *= (SWING_MULTIPLICATOR)) + 1) * t + ss) + 2) + b;

    }

    public static float InOut(float t, float b, float c, float d)
    {
        float s = SWING_BACK_DEFAULT;

        if ((t /= d / 2) < 1)
            return c / 2 * (t * t *
                    (((s *= (SWING_MULTIPLICATOR)) + 1) * t - s)) + b;

        return c / 2 * ((t -= 2) * t *
                (((s *= (SWING_MULTIPLICATOR)) + 1) * t + s) + 2) + b;
    }
}

public class Bounce
{
    public static float Evaluate(ETweeningBehaviour behaviour,
                                 float t, float b, float c, float d)
    {
        switch (behaviour)
        {
            case ETweeningBehaviour.In:
                return Bounce.In(t, b, c, d);
            case ETweeningBehaviour.Out:
                return Bounce.Out(t, b, c, d);
            case ETweeningBehaviour.InOut:
                return Bounce.InOut(t, b, c, d);
            default:
                return 0.0f;
        }
    }

    public static float In(float t, float b, float c, float d)
    {
        return c - Out(d - t, 0, c, d) + b;
    }


    public static float Out(float t, float b, float c, float d)
    {
        if ((t /= d) < (1 / 2.75f))
        {
            return c * (7.5625f * t * t) + b;
        }
        else if (t < (2 / 2.75f))
        {
            return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f) + b;
        }
        else if (t < (2.5 / 2.75))
        {
            return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f) + b;
        }
        else
        {
            return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f) + b;
        }
    }


    public static float InOut(float t, float b, float c, float d)
    {
        if (t < d / 2)
        {
            return In(t * 2, 0, c, d) / 2 + b;
        }
        else
        {
            return Out(t * 2 - d, 0, c, d) / 2 + c / 2 + b;
        }
    }
}