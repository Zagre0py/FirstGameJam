using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
   public Transform player;
   public float attackDistance = 5f;
   public float speed = 3.0f; //velocidad que va la mosca

    void Update()
    {

        float distance  = Vector3.Distance(transform.position, player.position); //calcula la distancia entre la mosca y el jugador

        if (distance <= attackDistance){

            
        }


        if(player != null){   //codigo para que la mosca se acerce al personaje

            Vector3 direction = (player.position - transform.position).normalized;

            transform.position += direction * speed * Time.deltaTime;
            transform.LookAt(player);
        }
    }
}
