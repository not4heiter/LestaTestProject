using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int damageToGive = 1;

    public Color startColor;
    public Color orangeColor;
    public Color redColor;

    public float orangeDuration = 1f;
    public float resetDuration = 5f;

    private Renderer blockRender;
    private bool isActivated = false;
    private bool isTouching = false;


    void Start()
    {
        blockRender = GetComponent<Renderer>();
        blockRender.material.color = startColor;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTouching = true;

            if(isActivated == false)
            {
                isActivated = true;
                StartCoroutine(DamageSequence());
            }
        }   
    }

    private void OnTriggerExit(Collider other)
    {
            isTouching = false;
    }

    private IEnumerator DamageSequence()
    {
        blockRender.material.color = orangeColor;        
        yield return new WaitForSeconds(orangeDuration);

        blockRender.material.color = redColor;

        if (isTouching) 
        {
            FindObjectOfType<HealthManager>().HurtPlayer(damageToGive);
        }
              
        yield return new WaitForSeconds(0.15f);

        blockRender.material.color = startColor;
        yield return new WaitForSeconds(resetDuration);

        isActivated = false;

        if (isTouching)
        {
            isActivated = true;
            StartCoroutine(DamageSequence());
        }
    }
}
