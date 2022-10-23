using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumY = -60F;
    public float maximumY = 60F;
    
    Rigidbody rb;
    float rotationY = 0F;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
        
        var pos = transform.position;
        var movedir = GetMoveDirection();

        pos += movedir.normalized * speed * Time.deltaTime;
        
        rb.MovePosition(pos);
        rb.MoveRotation(Quaternion.Euler(-rotationY,rotationX,0));
    }

    Vector3 GetMoveDirection()
    {
        Vector3 moveDir = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            moveDir += transform.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDir += -transform.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveDir += -transform.right;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveDir += transform.right;
        }

        return moveDir;
    }
}
