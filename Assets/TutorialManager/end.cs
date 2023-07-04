using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end : MonoBehaviour
{
    public Entity entity;

    public static end END;

    public void Awake()
    {
        END = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (died())
            Destroy(gameObject);
    }

    public bool died()
    {
        return entity.health <= 0;
    }
}
