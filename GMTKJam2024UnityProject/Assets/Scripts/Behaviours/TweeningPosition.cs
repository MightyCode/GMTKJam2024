using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TweeningPosition : MonoBehaviour
{
    public ETweeningBehaviour Behaviour;
    public ETweeningOption Option;
    public ETweeningType Type;

    public Vector3 Offset;
    public float TimeTook;

    public bool RandomStart = false;

    private Vector3Tweening tweening;

    private Vector3 initialPosition;


    void Start()
    {
        initialPosition = transform.position;
        tweening = new Vector3Tweening();

        OnValidate();
    }

    void Update()
    {
        Vector3 finalPosition = initialPosition;

        tweening.Update();
        finalPosition = tweening.Value;

        transform.position = finalPosition;
    }

    void OnValidate()
    {
        if (tweening == null) return;

        tweening.SetTweeningValues(Type, Behaviour)
            .SetTweeningOption(Option);

        tweening.InitRangeValue(TimeTook, initialPosition, Offset);

        if (RandomStart)
        {
            tweening.InitRandomTweening();
        }
    }
}
