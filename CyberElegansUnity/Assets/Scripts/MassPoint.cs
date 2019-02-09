using System;
using UnityEngine;

namespace Orbitaldrop.Cyberelegans
{
    public class MassPoint : Point
    {
        public float mass { get; private set; }
        public Vector3 vel { get; private set; }
        public Vector3 force { get; private set; }

        public MassPoint(float mass, Vector3 pos) : base(pos)
        {
            this.mass = mass;
        }

        public void ApplyForce(Vector3 force)
        {
            this.force += force;
        }

        public void Update()
        {
            ApplyForce(new Vector3(0.0f, Physics.Gravity * mass, 0.0f));

            if (pos.z <= Physics.GroundHeight)
            {
                var v = new Vector3(vel.x, 0.0f, vel.z);

                if (vel.y < 0.0f)
                {
                    v = vel;
                    v.x = 0;
                    v.z = 0;
                    
                    ApplyForce(-v * UniversalConstantsBehaviour.Instance.GroundAbsorptionConstant);
                    ApplyForce(new Vector3(0.0f, UniversalConstantsBehaviour.Instance.GroundRepulsionConstant, 0.0f) * (Physics.GroundHeight - pos.z));
                }
            }

            var r = Mathf.Sqrt(pos.x * pos.x + pos.y * pos.y);
            if (r >= 10.0f)
            {
                Extern.MeetObstacle++;

                if (Extern.MeetObstacle > 5)
                {
                    Extern.MeetObstacle = 0;
                }

                var radVect = pos.normalized;
                var v = vel;
                v.z = 0.0f;

                var radialComponent = Vector3.Dot(v, radVect);

                if (radialComponent > 0)
                {
                    v = -pos;
                    v.z = 0;
                    ApplyForce(v * UniversalConstantsBehaviour.Instance.GroundRepulsionConstant * (r - 10.0f));
                }
            }

            ApplyForce(-vel * UniversalConstantsBehaviour.Instance.AirFrictionCoefficient);
        }

        public void timeTick(float dt)
        {
            pos += vel * dt;
            vel += (force / mass) * dt;
        }

        public virtual void Select()
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            force = Vector3.zero;
        }

        GameObject shape;
        Material material;

        public void Draw(GameObject masspointHolder)
        {
            var r = 0.39f;
            var g = 0.39f;
            var b = 0.3f;
            
            if (shape == null)
            {
                shape = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                shape.transform.SetParent(masspointHolder.transform);
                shape.name = "Mass";

                if (material == null)
                {
                    material = shape.GetComponent<Renderer>().material;
                }
            }

            shape.transform.position = pos;
            shape.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
            material.color = new Color(r, g, b);
        }
    }
}