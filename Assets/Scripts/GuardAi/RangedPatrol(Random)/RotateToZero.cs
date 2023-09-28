using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToZero : MonoBehaviour
{
    public Transform enemyai;

    // Update is called once per frame
    void Update()
    {
        enemyai.rotation = new Quaternion(0,0,0,0);
    }
}
