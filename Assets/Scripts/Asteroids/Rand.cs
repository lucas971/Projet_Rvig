using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Rand 
{
    public static Vector3 randomV()
    {
        int x = Random.Range(-200, 200);
        int y = Random.Range(-200, 200);
        int z = Random.Range(-200, 200);
        return new Vector3(x, y, z).normalized;
    }
}
