using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistiveForceTheory : MonoBehaviour
{
    [SerializeField] private ResitiveForceTheoryConstants constants;

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (rigidbody != null)
        {
            var tangent = transform.forward;
            var normal = transform.right;
            
            var tangentalVelocity = Vector3.Dot(rigidbody.velocity, tangent);
            var normalVelocity = Vector3.Dot(rigidbody.velocity, normal);
            
            var tangentalDragForce = constants.TangentialDragCurve.Evaluate(tangentalVelocity);
            var normalDragForce = constants.NormalDragCurve.Evaluate(normalVelocity);
            
            Debug.DrawLine(transform.position, transform.position + tangentalDragForce * tangent, Color.cyan);
            Debug.DrawLine(transform.position, transform.position + normalDragForce * normal, Color.magenta);
        }    
    }
    
    void FixedUpdate()
    {
        if (rigidbody != null)
        {
            var tangent = transform.forward;
            var normal = transform.right;
            
            var tangentalVelocity = Vector3.Dot(rigidbody.velocity, tangent);
            var normalVelocity = Vector3.Dot(rigidbody.velocity, normal);
            
            var tangentalDragForce = constants.TangentialDragCurve.Evaluate(tangentalVelocity);
            var normalDragForce = constants.NormalDragCurve.Evaluate(normalVelocity);
            
            rigidbody.AddForce(tangentalDragForce * tangent + normalDragForce * normal);
        }    
    }
}
