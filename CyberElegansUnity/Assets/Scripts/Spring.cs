using System;
using UnityEngine;

namespace Orbitaldrop.Cyberelegans
{
    public class Spring : Connector
    {
        public float length { get; set; }
        public float stiffnessCoefficient { get; set; }
        public float frictionCoefficient { get; set; }

        public Spring(float length, float stiffnessCoefficient, float frictionCoefficient, MassPoint p1, MassPoint p2) : base(p1, p2)
        {
            this.length = length;

            if (this.length <= 0.0f) // Autodetect.
            {
                this.length = (p1.pos - p2.pos).magnitude * Mathf.Abs(this.length);
            }

            this.stiffnessCoefficient = stiffnessCoefficient;
            this.frictionCoefficient = frictionCoefficient;
        }

        public void Update()
        {
            if (status == 1)
            {
                var springVector = P1.pos - P2.pos;
                var r = springVector.magnitude;

                if (r < 0.05f || r >= 3)
                {
                    status = 0;
                    return;
                }

                Vector3 force = Vector3.zero;
                if (r != 0.0f)
                {
                    force = (springVector / r) * (r - length) * -stiffnessCoefficient;
                }

                force += -(P1.vel - P2.vel) * frictionCoefficient;

                P1.ApplyForce(force);
                P2.ApplyForce(-force);
            }
        }

        public void Draw(GameObject springHolder)
        {
            var c = Color.red;
            if (status != 0)
            {
                c = new Color(0.29f, 0.29f, 0.29f);
            }

            Debug.DrawLine(P1.pos, P2.pos, c);
        }
    }
}