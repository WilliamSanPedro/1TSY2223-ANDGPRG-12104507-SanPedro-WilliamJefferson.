using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public int hp = 1000;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            InvokeRepeating("Damage", 1f, 1f);

            if (hp < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Damage()
    {
        hp -= 10;
    }
}
