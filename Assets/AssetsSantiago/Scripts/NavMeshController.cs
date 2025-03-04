using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    public Transform objetivo; // El objetivo al que la mosca persigue
    private Animator animator;
    private NavMeshAgent agente;
    private bool estaAtacando = false;
    public float attackDistance;
    public float alturaRandomMin = 5f; // Altura mínima aleatoria
    public float alturaRandomMax = 10f; // Altura máxima aleatoria

    void Start()
    {
        // Configura el NavMeshAgent
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Asigna una altura aleatoria al inicio
        float alturaAleatoria = Random.Range(alturaRandomMin, alturaRandomMax);
        Vector3 posicionConAltura = new Vector3(transform.position.x, alturaAleatoria, transform.position.z);
        transform.position = posicionConAltura;

        // Asegúrate de que el NavMeshAgent puede moverse en 3D (sin restricciones)
        agente.avoidancePriority = 50;  // Ajusta esto si lo necesitas para evitar colisiones con otros agentes

        // Desactiva el agente para que no se quede "pegado" al suelo
        agente.baseOffset = 0f;  // Esto elimina cualquier corrección automática del agente respecto al suelo
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, objetivo.position);

        if(distance <= attackDistance)
        {
            if (agente != null)
            {
                agente.isStopped = true; // Detiene el movimiento cuando está en el rango de ataque
            }
            estaAtacando = true;
            animator.SetBool("Ataque", estaAtacando);
        }
        else
        {
            agente.isStopped = false; // Vuelve a mover al agente si no está atacando
            estaAtacando = false;
            animator.SetBool("Ataque", estaAtacando);
        }

        // Mantén el agente persiguiendo al objetivo en 3D
        agente.destination = objetivo.position;

        // Asegura que la mosca se mantenga a una altura aleatoria mientras persigue al jugador
        if (agente.hasPath)
        {
            // Solo modifica la altura, mantén la posición X y Z
            Vector3 destinoConAltura = new Vector3(agente.destination.x, transform.position.y, agente.destination.z);
            agente.destination = destinoConAltura;
        }
    }
}
