using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    private Rigidbody theRB;
    public float timeToFall = 0.5f;
    private bool isActivated = false;
    private Vector3 originalPosition;

    private Renderer blockRender;

    public Color startColor;
    public Color orangeColor;
    public Color redColor;
    
    void Start()
    {
        blockRender = GetComponent<Renderer>();
        blockRender.material.color = startColor;
        theRB = GetComponent<Rigidbody>();
        theRB.useGravity = false;
        originalPosition = transform.position;
        theRB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.tag == "Player" && !isActivated)
        {
                isActivated = true;
                StartCoroutine(FallingTimer());
        }
    }

    private IEnumerator FallingTimer()
    {
        blockRender.material.color = orangeColor;
        yield return new WaitForSeconds(timeToFall);

        blockRender.material.color = redColor;
        yield return new WaitForSeconds(0.15f);

        theRB.useGravity = true;

        yield return new WaitForSeconds(2f);
        ResetPosition();
    }

    private void ResetPosition()
    {
        theRB.velocity = Vector3.zero;
        theRB.angularVelocity = Vector3.zero;
        theRB.useGravity = false;
        theRB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        transform.position = originalPosition;
        blockRender.material.color = startColor;
        isActivated = false;
    }
}
