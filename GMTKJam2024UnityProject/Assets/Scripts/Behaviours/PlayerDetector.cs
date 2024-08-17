using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerDetector : MonoBehaviour
{
    // Event delegates triggered on click.
    [FormerlySerializedAs("onEnter")]
    [SerializeField]
    private UnityEvent m_onEnter = new UnityEvent();

    public UnityEvent onEnter
    {
        get { return m_onEnter; }
        set { m_onEnter = value; }
    }

    [FormerlySerializedAs("onLeave")]
    [SerializeField]
    private UnityEvent m_onLeave = new UnityEvent();

    public UnityEvent onLeave
    {
        get { return m_onLeave; }
        set { m_onLeave = value; }
    }

    GameObject savedEntityAfterEnter;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("++Player entered " + other.gameObject.name);
        if (other.gameObject.name == "PlayerBody")
        {
            Debug.Log("++Trigger");
            savedEntityAfterEnter = other.gameObject;
            onEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == savedEntityAfterEnter)
        {
            onLeave.Invoke();
            savedEntityAfterEnter = null;
        }
    }
}
