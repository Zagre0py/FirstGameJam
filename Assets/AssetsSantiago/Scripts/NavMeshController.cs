using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    public Transform objetivo;
    private Animator animator;
    private NavMeshAgent agente;
    private bool estaAtacando = false;
    public float attackDistance;


    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, objetivo.position);

        if(distance<= attackDistance){

            if (agente != null){

                agente.isStopped = true;
            }
            estaAtacando = true;
            animator.SetBool("Ataque", estaAtacando);
        }
        else{
            agente.isStopped = false;
            estaAtacando = false;
            animator.SetBool("Ataque", estaAtacando);
        }
        agente.destination = objetivo.position;
    }
}
