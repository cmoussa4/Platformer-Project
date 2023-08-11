using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    Controller cm;
    [SerializeField] Transform PosA;
    [SerializeField] Transform PosB;
    [SerializeField] int speed;
    private Vector2 targetPos;
    // Start is called before the first frame update

    private void Awake()
    {
        cm = GetComponent<Controller>();
    }
    void Start()
    {
        targetPos = PosB.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, PosA.position) < 0.1f) targetPos = PosB.position;
        if (Vector2.Distance(transform.position, PosB.position) < 0.1f) targetPos = PosA.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            
        }
    }
}
