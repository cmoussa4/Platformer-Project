using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Transform destination;
    [SerializeField] GameObject outerPortal;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, 0.5f);
        outerPortal.transform.Rotate(0f, 0f, 0.5f);
    }

    public Transform GetDestination()
    {
        return destination;
    }
}
