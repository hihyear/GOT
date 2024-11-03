using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOTEnemy : MonoBehaviour
{
    public ParticleSystem hitVfx;
    public float forcePower = 500.0f;

   private void OnTriggerEnter(Collider other)
   {
       if (other.tag == "Weapon")
       {
           gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.yellow;
           Invoke("ReturnColor", 0.2f);

            Vector3 hitPoint = other.transform.position;
            Instantiate(hitVfx, hitPoint, Quaternion.identity);


            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (transform.position - hitPoint).normalized;
                
                rb.AddForce(direction * forcePower);
            }

            Debug.Log($"TriggerEnter : {other.name}");
       }
   
   }

   //
   //private void OnTriggerExit(Collider other)
   //{
   //    if (other.tag == "Weapon")
   //    {
   //        gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
   //    }
   //
   //    Debug.Log("TriggerExit");
   //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Weapon")
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            Invoke("ReturnColor", 0.2f);

            Vector3 hitPoint = collision.contacts[0].point;
            Instantiate(hitVfx, hitPoint, Quaternion.identity);
            
            Debug.Log("OnCollisionEnter");
        }
    }

    void ReturnColor()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
    }

    private void OnCollisionExit(Collision collision)
    {
        //if (collision.collider.tag == "Weapon")
        //
        //   gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        //

        Debug.Log("OnCollisionExit");
    }
}
