using System;
using UnityEngine;

namespace Orbitaldrop.Cyberelegans
{
    public class Spring : Connector
    {
        public float length { get; set; }
        public float stiffnessScaler { get; set; }
        public float frictionScalar { get; set; }

        private readonly float restLength;

        public Spring(GameObject parent, float length, float stiffnessScaler, float frictionScalar, MassPoint p1, MassPoint p2) : base(p1, p2)
        {
            this.length = length;

            if (this.length <= 0.0f) // Autodetect.
            {
                this.length = (p1.pos - p2.pos).magnitude * Mathf.Abs(this.length);
            }

            this.stiffnessScaler = stiffnessScaler;
            this.frictionScalar = frictionScalar;

            this.restLength = (P1.pos - P2.pos).magnitude;
        }

        public void Update()
        {
            if (status == 1)
            {
                var springVector = P1.pos - P2.pos;
                var r = springVector.magnitude;

                if (r < 0.05f || r >= restLength * 1.2f)
                {
                    status = 0;
                    return;
                }

                Vector3 force = Vector3.zero;
                if (r != 0.0f)
                {
                    force = (springVector / r) * (r - length) * - (stiffnessScaler * UniversalConstantsBehaviour.Instance.StiffCoeff);
                }

                force += -(P1.vel - P2.vel) * (frictionScalar * UniversalConstantsBehaviour.Instance.FrictCoeff);

                P1.ApplyForce(force);
                P2.ApplyForce(-force);
            }
        }

        public void FixedUpdate()
        {

        }

        public void Draw(GameObject springHolder)
        {
            Debug.DrawLine(P1.pos, P2.pos, (status == 0 ? Color.red : Color.gray));
        }
    }
}