using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingLava : MonoBehaviour
{
    Controller con;
    // Start is called before the first frame update

        private void Awake()
        {
          con = GetComponent<Controller>();
        }

    // Update is called once per frame
    void Update()
    {
        if(con == null)
        {
            transform.localScale += new Vector3(0f, 0.5f * Time.deltaTime, 0f);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
      
    }
}
