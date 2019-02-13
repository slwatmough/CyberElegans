using System;
using UnityEngine;

namespace Orbitaldrop.Cyberelegans
{
    public class MassPoint : Point
    {
        public float mass { get; private set; }
        public Vector3 vel { get; private set; }
        public Vector3 force { get; private set; }

        public GameObject GameObject { get; private set; }
        public Material Material { get; private set; }

        public MassPoint(GameObject parent, float mass, Vector3 pos) : base(pos)
        {
            this.mass = mass;

            if (GameObject == null)
            {
                GameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                GameObject.transform.SetParent(parent.transform);
                GameObject.transform.position = pos;
                GameObject.name = "Mass";

                if (Material == null)
                {
                    Material = GameObject.GetComponent<Renderer>().material;
                }
            }
        }

        public void ApplyForce(Vector3 force)
        {
            this.force += force;
        }

        int frame = 0;

        public void Update()
        {
            if (!UniversalConstantsBehaviour.Instance.EnablePhysics)
            {
                return;
            }

            ApplyForce(new Vector3(0.0f, Physics.Gravity * mass, 0.0f));

            if (pos.y <= Physics.GroundHeight)
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

        public void FixedUpdate()
        {
            var fdt = Time.fixedDeltaTime;
            pos += vel * fdt;
            vel += (force / mass) * fdt;
        }

        public virtual void Select()
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            force = Vector3.zero;
        }
        
        public void Draw(GameObject masspointHolder)
        {
            var r = 0.39f;
            var g = 0.39f;
            var b = 0.3f;
            
            GameObject.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
            Material.color = new Color(r, g, b);
            
            if (Vector3.Dot(force, Vector3.up) > 0.7f)
            {
                Debug.Log("Scale required = " + 9.81f / force.magnitude);
            }

            Debug.DrawLine(pos, pos + (force / 9.81f), Color.magenta);
        }
    }
}