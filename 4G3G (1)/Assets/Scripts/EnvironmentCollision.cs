using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentCollision : MonoBehaviour
{

    Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    /*void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 aux = transform.position;

        if(Physics.SphereCast(aux, 0.001f , transform.forward, out hit, 1, 9))
        {
            Debug.Log("raycast hit");
            Destroy(this.gameObject);
        }

    }*/
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Ha entradoooooo");
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject.GetComponent<BoxCollider>());
            Destroy(this.gameObject);
        }
    }

}
