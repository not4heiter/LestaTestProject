using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBlock : MonoBehaviour
{
    public float knockbackForce = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CharacterController playerController = other.GetComponent<CharacterController>();
            if (playerController != null)
            {
                Vector3 velocity = playerController.velocity;

                if (velocity.magnitude < 0.1f)
                {
                    velocity = other.transform.forward;
                }

                velocity.Normalize();

                Vector3 knockbackDirection = -velocity;
                knockbackDirection.y = 0;
                knockbackDirection.Normalize();

                playerController.Move(knockbackDirection * knockbackForce * Time.deltaTime);
            }
        }
    }
}
