using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterController : MonoBehaviour
{
    private Camera _camera;
    
    private NavMeshAgent _agent;
    private Animator _animator;
    private Vector3 _lastPos = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _camera = Camera.main;
        _lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = _camera.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _agent.SetDestination(hit.point);
            }
            
            
        }

        float distanceDifference = Vector3.Distance(_lastPos, transform.position);
        
        if (distanceDifference >= 0.01f)
        {
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
        }
        _animator.SetFloat("WalkingSpeed", distanceDifference * Time.deltaTime * 60f * 180f);
        _lastPos = transform.position;
    }
}
