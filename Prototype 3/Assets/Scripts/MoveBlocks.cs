using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlocks : MonoBehaviour
{
    public bool isMovable;
    private Rigidbody rb;

    bool snaped;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isMovable){
            snaped = gameObject.GetComponent<SnapBoxScript>().isAttaching;
            float h = Input.GetAxis("Horizontal") * 5;
            float v = Input.GetAxis("Vertical") * 5;

            Vector3 vel = rb.velocity;
            vel.x = h;
            vel.z = v;
            rb.velocity = vel;
            if(!snaped){
                isMovable = false;
            }
        }else{
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        
    }
}
