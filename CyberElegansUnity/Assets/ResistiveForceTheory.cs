using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ResistiveForceTheory : MonoBehaviour
{
    [SerializeField] private ResitiveForceTheoryConstants constants;

    [SerializeField] private bool PositionBasedVelocity = false;

    [SerializeField] private float DebugRenderingScale = 1.0f;

    [SerializeField] private float TangentForceScale = 1.0f;
    
    [SerializeField] private float NormalForceScale = 1.0f;

    [SerializeField] private float VelocityScale = 10.0f; // Convert from mm to um
    
    private Rigidbody rigidbody;

    private Vector3 updatePreviousPosition;
    private Vector3 fixedUpdatePreviousPosition;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        updatePreviousPosition = fixedUpdatePreviousPosition = transform.position;
    }

    void Update()
    {
        if (!constants.Enabled)
        {       
            return;
        }
        
        if (rigidbody != null)
        {
            var tangent = transform.forward;
            var normal = transform.right;

            var velocity = rigidbody.velocity;
            
            if (PositionBasedVelocity)
            {
                velocity = rigidbody.position - updatePreviousPosition;
                updatePreviousPosition = rigidbody.position;
            }

            velocity *= VelocityScale;
            
            var tangentalVelocity = Vector3.Dot(velocity, tangent);
            var tangentalVelocitySign = tangentalVelocity > 0.0f ? 1.0f : -1.0f;
            var normalVelocity = Vector3.Dot(velocity, normal);
            var normalVelocitySign = normalVelocity > 0.0f ? 1.0f : -1.0f;
            
            var tangentalDragForce = -constants.TangentialDragCurve.Evaluate(tangentalVelocity * tangentalVelocitySign) * tangentalVelocitySign * TangentForceScale;
            var normalDragForce = -constants.NormalDragCurve.Evaluate(normalVelocity * normalVelocitySign) * normalVelocitySign * NormalForceScale;
            
            //Debug.DrawLine(transform.position, transform.position + (tangentalDragForce * tangent * DebugRenderingScale), Color.cyan);
            //Debug.DrawLine(transform.position, transform.position + (normalDragForce * normal * DebugRenderingScale), Color.magenta);
            Debug.DrawLine(transform.position, transform.position + (tangentalDragForce * tangent * DebugRenderingScale) + (normalDragForce * normal * DebugRenderingScale), Color.black);
            
            //Debug.Log(string.Format("VT: {0}, VN: {1}, T: {2}, N: {2}", tangentalVelocity, normalVelocity, tangentalDragForce, normalDragForce));
        }    
    }
    
    void FixedUpdate()
    {
        if (!constants.Enabled)
        {       
            return;
        }
        
        if (rigidbody != null)
        {
            var tangent = transform.forward;
            var normal = transform.right;
            
            var velocity = rigidbody.velocity;
            
            if (PositionBasedVelocity)
            {
                velocity = rigidbody.position - fixedUpdatePreviousPosition;
                fixedUpdatePreviousPosition = rigidbody.position;
            }
            
            var tangentalVelocity = Vector3.Dot(velocity, tangent);
            var tangentalVelocitySign = tangentalVelocity > 0.0f ? 1.0f : -1.0f;
            var normalVelocity = Vector3.Dot(velocity, normal);
            var normalVelocitySign = normalVelocity > 0.0f ? 1.0f : -1.0f;
            
            var tangentalDragForce = -constants.TangentialDragCurve.Evaluate(tangentalVelocity * tangentalVelocitySign) * tangentalVelocitySign  * TangentForceScale;
            var normalDragForce = - constants.NormalDragCurve.Evaluate(normalVelocity * normalVelocitySign) * normalVelocitySign * NormalForceScale;
            
            rigidbody.AddForce(tangentalDragForce * tangent + normalDragForce * normal);
        }    
    }
}
