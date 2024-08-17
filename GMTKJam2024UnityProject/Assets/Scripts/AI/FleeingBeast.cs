using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeingBeast : MonoBehaviour
{

    [SerializeField] private Transform fleeingTarget;
    private NavMeshAgent agent;

    public float fleeingRange = 4f;
    // Start is called before the first frame update
    void Start()
    {
        fleeingTarget = PlayerManager.Instance.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float distance = Vector3.Distance(transform.position,fleeingTarget.position);

        if(distance < fleeingRange)
        {
            Vector3 playerDirection = fleeingTarget.position - transform.position;

            Vector3 newPos = transform.position - playerDirection;

            agent.SetDestination(newPos);
        }
    }
}
