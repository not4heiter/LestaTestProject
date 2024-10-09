using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningBlock : MonoBehaviour
{
    public BallPool ballPool;
    public float spawnInterval = 1f;

    private void Start()
    {
        StartCoroutine(SpawnBalls());
    }

    private IEnumerator SpawnBalls()
    {
        while (true)
        {
            SpawnBall();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnBall()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.y += 0.5f;

        GameObject ball = ballPool.GetBall();
        if (ball != null)
        {
            ball.transform.position = spawnPosition;
            ball.GetComponent<Ball>().Initialize();
        }
    }
}
