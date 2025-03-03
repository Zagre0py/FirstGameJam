using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
   public Transform player;
   public float attackDistance = 5f;
   public float speed = 3.0f;

    void Update()
    {

        float distance  = Vector3.Distance(transform.position, player.position);

        if(player != null){

            Vector3 direction = (player.position - transform.position).normalized;

            transform.position += direction * speed * Time.deltaTime;
            transform.LookAt(player);
        }
    }
}
