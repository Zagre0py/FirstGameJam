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
    public float attackDistance; // Distancia a la que el enemigo ataca
    public float minAlturaDeVuelo = 3f; // Altura mínima de vuelo
    public float maxAlturaDeVuelo = 7f; // Altura máxima de vuelo
    public float alturaDeAtaque = 2f; // Altura específica al acercarse al objetivo
    private float alturaDeVuelo; // Altura actual de vuelo

    void Start()
    {
        // Configura el NavMeshAgent
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agente.updatePosition = false; // Desactiva la actualización automática de la posición
        agente.updateRotation = false; // Desactiva la rotación automática (opcional)

        // Asigna una altura de vuelo aleatoria al inicio
        alturaDeVuelo = Random.Range(minAlturaDeVuelo, maxAlturaDeVuelo);
    }

    void Update()
    {
        // Calcula la distancia al objetivo
        float distance = Vector3.Distance(transform.position, objetivo.position);

        // Ajusta la altura de vuelo según la distancia al objetivo
        if (distance <= attackDistance)
        {
            // Si está dentro del rango de ataque, ajusta la altura hacia la altura de ataque
            alturaDeVuelo = Mathf.Lerp(alturaDeVuelo, alturaDeAtaque, Time.deltaTime * 2f);
        }
        else
        {
            // Si está fuera del rango de ataque, mantén una altura aleatoria
            alturaDeVuelo = Mathf.Lerp(alturaDeVuelo, Random.Range(minAlturaDeVuelo, maxAlturaDeVuelo), Time.deltaTime * 0.5f);
        }

        // Calcula la posición objetivo en el eje X y Z, manteniendo la altura de vuelo
        Vector3 objetivoPosition = new Vector3(objetivo.position.x, alturaDeVuelo, objetivo.position.z);

        // Comportamiento de ataque
        if (distance <= attackDistance)
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

        // Asigna la posición objetivo al NavMeshAgent
        agente.SetDestination(objetivoPosition);

        // Mueve al enemigo manualmente, manteniendo la altura de vuelo
        Vector3 newPosition = agente.nextPosition;
        newPosition.y = alturaDeVuelo; // Ajusta la altura de vuelo
        transform.position = newPosition;

        // Opcional: Rota al enemigo hacia el objetivo
        Vector3 direction = (objetivoPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * agente.angularSpeed);
        }
    }
}