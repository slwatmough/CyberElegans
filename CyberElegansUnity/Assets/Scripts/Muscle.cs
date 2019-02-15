using UnityEngine;

namespace Orbitaldrop.Cyberelegans
{
    public class Muscle
    {
        private readonly Verlet.MassPoint[] massPoints;
        private readonly int p1;
        private readonly int p2;
        
        public float length { get; set; }
        public Synapse synapse { get; set; }

        public Muscle(Verlet.MassPoint[] massPoints, int p1, int p2, string name)
        {
            this.massPoints = massPoints;
            this.p1 = p1;
            this.p2 = p2;
            synapse = new Synapse(1.0f, name);
            synapse.pos = (massPoints[p1].pos + massPoints[p2].pos) / 2.0f;
            length = (massPoints[p1].pos - massPoints[p2].pos).magnitude;

        }

        public void Update()
        {
            if (synapse.income > 0.0f)
            {
                synapse.income = Mathf.Min(synapse.income, synapse.threshold);

                synapse.income *= 0.9f;
            }
        }
        
        public void FixedUpdate()
        {
            synapse.pos = (massPoints[p1].pos + massPoints[p2].pos) / 2.0f;
            
            if (synapse.income > 0.0f)
            {
                var musclforce = (massPoints[p1].pos - massPoints[p2].pos).normalized;
    
                massPoints[p1].accel -= musclforce * (50.0f * synapse.income);
                massPoints[p2].accel += musclforce * (50.0f * synapse.income);
            }
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

            var V12 = massPoints[p2].pos - massPoints[p1].pos;
            shape.transform.position = (massPoints[p1].pos + massPoints[p2].pos) / 2.0f;
            shape.transform.rotation = Quaternion.FromToRotation(Vector3.up, V12);
            shape.transform.localScale = new Vector3(0.02f, V12.magnitude / 2.0f, 0.02f);
            material.color = new Color(r, g, b);
        }
    }
}