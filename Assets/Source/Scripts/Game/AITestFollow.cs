using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AITestFollow : MonoBehaviour
{

    public enum AITestFollowState
    {
        ScanForTarget = 0,
        FollowTarget = 1,
        ReturnHome = 2
    }

    public AITestFollowState followState = AITestFollowState.ScanForTarget;
    
    public GameObject targetToFollow;
    
    [Tooltip("How close to the target the agent will attempt to follow")]
    public float followDistanceMin = 2f;
    
    [Tooltip("The AI follow range")]
    public float followDistance = 10f;

    [Tooltip("How far the follow agent is allowed to be from its starting point before it navigates back")]
    public float maxTravelDistance = 30f;
    
    private NavMeshAgent _agent;
    private Vector3 _startPos;
    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _startPos = transform.position;
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToStartPos = Vector3.Distance(transform.position, _startPos);
        float distToTarget = Vector3.Distance(transform.position, targetToFollow.transform.position);
        switch (followState)
        {
            case AITestFollowState.ScanForTarget:
                if (targetToFollow)
                {
                    _agent.SetDestination(_startPos);
                    if (distToTarget <= followDistance)
                    {
                        followState = AITestFollowState.FollowTarget;
                        _animator.SetBool("IsWalking", true);
                    }
                    else
                    {
                        _animator.SetBool("IsWalking", (distanceToStartPos > 0.1f) ? true : false);
                    }
                }
                break;
            
            case AITestFollowState.ReturnHome:
                _agent.SetDestination(_startPos);
                if (distanceToStartPos <= 1)
                {
                    //Updating the state once we return home to scan for a target
                    followState = AITestFollowState.ScanForTarget;
                    _animator.SetBool("IsWalking", false);
                }
                else
                {
                    _animator.SetBool("IsWalking", true);
                }
                break;
            
            case AITestFollowState.FollowTarget:
                if (distToTarget >= followDistance)
                {
                    followState = AITestFollowState.ScanForTarget;
                    _animator.SetBool("IsWalking", false);
                }
                else
                {
                    if (distToTarget > followDistanceMin)
                    {
                        _agent.SetDestination(targetToFollow.transform.position);
                        _animator.SetBool("IsWalking", true);
                    }
                    else
                    {
                        _agent.SetDestination(transform.position);
                        _animator.SetBool("IsWalking", false);
                    }

                    if (distanceToStartPos >= maxTravelDistance)
                    {
                        followState = AITestFollowState.ReturnHome;
                    }
                }
                break;
        }
    }
}
