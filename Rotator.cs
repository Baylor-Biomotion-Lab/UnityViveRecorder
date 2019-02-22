using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float delta = 1.5f;  // Amount to move left and right from the start point
    private Vector3 startPos;
    public int speed;
    // Update is called once per frame

    void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        //transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        Vector3 v = startPos;
        v.z += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;
        Debug.Log(transform.position.y);
    }

}