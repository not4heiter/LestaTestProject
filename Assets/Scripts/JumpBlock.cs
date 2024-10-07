using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBlock : MonoBehaviour
{
    public float knockbackAngle = 45f;
    public float knockbackForce = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CharacterController playerController = other.GetComponent<CharacterController>();
            if (playerController != null)
            {
                Vector3 knockbackDirection = Quaternion.Euler(0, 0, knockbackAngle) * Vector3.back;
                knockbackDirection.y = 0;
                knockbackDirection.Normalize();

                playerController.Move(knockbackDirection * knockbackForce * Time.deltaTime);

                Vector3 jump = Vector3.up * Time.deltaTime * (knockbackForce / 2);
                playerController.Move(jump);
            }
        }
    }
}
