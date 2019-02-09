using UnityEngine;

namespace Orbitaldrop.Cyberelegans
{
    public class Muscle : Connector
    {
        public float length { get; set; }
        public Synapse synapse { get; set; }

        public Muscle(MassPoint p1, MassPoint p2, string name) : base(p1, p2)
        {
            this.status = 1;
            synapse = new Synapse(1.0f, name);
            synapse.pos = (p1.pos + p2.pos) / 2.0f;
            length = (p1.pos - p2.pos).magnitude;

        }

        public void Update()
        {
            if (synapse.income > 0.0f)
            {
                var musclforce = (P1.pos - P2.pos).normalized;

                synapse.income = Mathf.Min(synapse.income, synapse.threshold);

                P1.ApplyForce(-musclforce * (5.0f * synapse.income));
                P2.ApplyForce(musclforce * (5.0f * synapse.income));

                synapse.income *= 0.9f;
            }
        }

        public void Activate(float value)
        {
            synapse.income += value;
        }

        public void Disactivate()
        {

        }

        GameObject shape;
        Material material;

        public void Draw(GameObject musclesHolder)
        {
            var r = Mathf.Lerp(0.31f, 1.0f, synapse.GetActivity()); ;
            var g = 0.31f;
            var b = 0.31f;

            if (shape == null)
            {
                shape = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                shape.transform.SetParent(musclesHolder.transform);
                shape.name = synapse.name;
                if (material == null)
                {
                    material = shape.GetComponent<Renderer>().material;
                }
            }

            var V12 = P2.pos - P1.pos;
            shape.transform.position = (P1.pos + P2.pos) / 2.0f;
            shape.transform.rotation = Quaternion.FromToRotation(Vector3.up, V12);
            shape.transform.localScale = new Vector3(0.02f, V12.magnitude / 2.0f, 0.02f);
            material.color = new Color(r, g, b);
        }
    }
}