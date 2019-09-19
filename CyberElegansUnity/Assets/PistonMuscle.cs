using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements;
using UnityEngine;
using UnityEngine.Serialization;

public class PistonMuscle : MonoBehaviour
{
    [SerializeField] public Transform Root;
    [SerializeField] public Transform Attachment;
    [SerializeField] private float Strength = 1.0f;
    [SerializeField] private float ContractFalloffPerSecond = 0.9f;
    [SerializeField] private bool EnableForce = true;
    
    private Rigidbody rootRigidBody;
    private Rigidbody attachmentRigidbody;

    private float restLength;

    private float contracting = 0.0f;
    
    private void Start()
    {
        if (Root != null && Attachment != null)
        {
            transform.position = (Root.position + Attachment.position) * 0.5f;
        }

        if (Root != null)
        {
            rootRigidBody = Root.gameObject.GetComponentInParent<Rigidbody>();
        }
        
        if (Attachment != null)
        {
            attachmentRigidbody = Attachment.gameObject.GetComponentInParent<Rigidbody>();

            restLength = (Attachment.position - Root.position).magnitude;
        }
    }

    private void FixedUpdate()
    {
        if (EnableForce)
        {
            if (attachmentRigidbody != null && Mathf.Abs(contracting) > 0.1f)
            {
                attachmentRigidbody.AddForceAtPosition(
                    (Attachment.position - Root.position).normalized * -Strength * contracting, Attachment.position);
            }

            if (rootRigidBody != null && Mathf.Abs(contracting) > 0.1f)
            {
                rootRigidBody.AddForce((Root.position - Attachment.position).normalized * -Strength * contracting);
            }
        }
    }

    private void Update()
    {
        float length = (Attachment.position - Root.position).magnitude;
        float t = contracting;
        
        // -1            0            1
        // M             W            C
        var color = Color.red;
        if (t < 0.0f)
        {
            color = Color.Lerp(Color.magenta, Color.white, t + 1.0f);
        }
        else
        {
            color = Color.Lerp(Color.white, Color.cyan, t);
        }
        
        Debug.DrawLine(Root.position, Attachment.position, color);

//        Debug.DrawLine(Root.position, Attachment.position, (contracting > 0.1f ? Color.magenta : (contracting < -0.1f ? Color.cyan : Color.white)));

        contracting *= ContractFalloffPerSecond * Time.deltaTime;
    }

    public void Contract(float scale = 1.0f)
    {
        contracting = 1.0f * scale;
    }
}
