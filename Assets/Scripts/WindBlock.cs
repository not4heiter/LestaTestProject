using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBlock : MonoBehaviour
{
    public float windStrength = 2f;
    public float changeInterval = 2f;
    private Vector3 windDirection;
    private CharacterController playerController;
    private bool isTouching = false;

    void Start()
    {
        StartCoroutine(ChangeWindDirection());
    }

    private void Update()
    {
        if (isTouching && playerController != null)
        {
            Vector3 windForce = windDirection * windStrength;
            playerController.Move(windForce * Time.deltaTime);
        }
    }

    private IEnumerator ChangeWindDirection()
    {
        while (true)
        {
            float randomAngle = Random.Range(0f, 360f);
            windDirection = new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle)).normalized;
            yield return new WaitForSeconds(changeInterval);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerController = other.GetComponent<CharacterController>();
            isTouching = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTouching = false;
            playerController = null;
        }
    }
}
