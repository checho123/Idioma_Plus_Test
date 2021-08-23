using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilEnemy : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float velocityProyectil;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * velocityProyectil, ForceMode.Impulse);
        Destroy(gameObject, 2f);
    }

}
