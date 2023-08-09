using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody enemyBody;

    private int direction = 1;
    public float enemySpeed, timeToFlip;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody>();
        StartCoroutine("ChangeDirection");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemyBody.velocity = new Vector3(enemySpeed * direction, enemyBody.velocity.y, enemyBody.velocity.z);
    }

    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(timeToFlip);
        direction *= -1;
        if (direction == 1)
        {
            transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
        if (direction == -1)
        {
            transform.localRotation = Quaternion.Euler(0, 270, 0);
        }
        StartCoroutine("ChangeDirection");
    }

}
