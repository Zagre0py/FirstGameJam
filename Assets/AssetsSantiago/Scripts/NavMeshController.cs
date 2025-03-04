using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    public Transform objetivo;
    private Animator animator;
    private NavMeshAgent agente;
    public float attackDistance = 5f;


    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, objetivo.position);

        if(distance<= attackDistance){

            if (agente != null){

                agente.isStopped = true;
            }
            animator.SetTrigger("Attack");
        }
        agente.destination = objetivo.position;
    }
}
