using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        transform.Translate(0, 1.3f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 1);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            Destroy(this.gameObject);
        }
    }
}
