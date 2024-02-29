using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsumaTauluun : MonoBehaviour
{
    public Collider ter‰v‰P‰‰;

    private new Rigidbody rigidbody;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        foreach (ContactPoint contactPoint in collision.contacts) {
            if(contactPoint.thisCollider == ter‰v‰P‰‰) {
                transform.SetParent(contactPoint.otherCollider.transform.root, true);
                return;
            }
        }
    }
}
