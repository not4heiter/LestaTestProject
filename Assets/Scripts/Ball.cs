using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float upwardSpeed = 3f;
    public float lifetime = 2f;
    public float pushForce = 10f;
    public int damageToGive = 1;

    private void Start()
    {
        StartCoroutine(ReturnBallAfterTime());
    }

    public void Initialize()
    {
        StartCoroutine(ReturnBallAfterTime());
    }

    private void Update()
    {
        transform.Translate(Vector3.up * upwardSpeed * Time.deltaTime, Space.World);
    }

    private IEnumerator ReturnBallAfterTime()
    {
        yield return new WaitForSeconds(lifetime);

        BallPool ballPool = FindObjectOfType<BallPool>();
        if (ballPool != null)
        {
            ballPool.ReturnBall(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            CharacterController playerController = other.GetComponent<CharacterController>();
            if (playerController != null)
            {
                Vector3 pushDirection = (other.transform.position - transform.position).normalized;
                Vector3 pushVector = pushDirection * pushForce;

                playerController.Move(pushVector * Time.deltaTime);
            }

            FindObjectOfType<HealthManager>().HurtPlayer(damageToGive);
        }
    }
}
