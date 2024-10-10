using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAsteroid : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ProcessLifetime());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator ProcessLifetime() {
        yield return new WaitForSeconds(3f);
        GameObject.Destroy(this.gameObject);
    }
}
