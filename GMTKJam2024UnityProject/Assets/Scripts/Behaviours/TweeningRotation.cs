using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweeningRotation : MonoBehaviour
{
    public ETweeningBehaviour Behaviour;
    public ETweeningOption Option;
    public ETweeningType Type;

    public Vector3 Offset;
    public float TimeTook;

    public bool RandomStart = false;

    private Vector3Tweening tweening;

    private Quaternion initialPosition;

    void Start()
    {
        initialPosition = transform.rotation;
        tweening = new Vector3Tweening();

        OnValidate();
    }

    // Update is called once per frame
    void Update()
    {

        tweening.Update();

        transform.rotation = initialPosition;
        transform.Rotate(tweening.Value);
    }

    void OnValidate()
    {
        if (tweening == null) return;

        tweening.SetTweeningValues(Type, Behaviour)
            .SetTweeningOption(Option);

        tweening.InitRangeValue(TimeTook, initialPosition.eulerAngles, initialPosition.eulerAngles + Offset);

        if (RandomStart)
        {
            tweening.InitRandomTweening();
        }
    }
}
