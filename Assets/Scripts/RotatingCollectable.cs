using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCollectable : MonoBehaviour
{
    [SerializeField] private Transform collectable;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(collectable.transform.position, Vector2.up, 180 * Time.deltaTime);
        transform.Rotate(0, 0, 0.5f);
    }
}

