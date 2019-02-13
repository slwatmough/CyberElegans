using System;
using System.Collections.Generic;
using UnityEngine;

namespace Orbitaldrop.Cyberelegans
{
    public class Neuron : Synapse
    {
        private const float SphereScale = 0.02f;
        public Vector3 originalPosition;
        public List<Axon> axons = new List<Axon>();
        public float ratioX { get; set; }
        public float ratioY { get; set; }
        public float ratioZ { get; set; }
        public char type { get; set; }
        public int clrIndex { get; set; }
        public readonly string description;

        private readonly bool pseudoneuron;

        public Neuron(string name, Vector3 position, float threshold, float ratioX, float ratioY, float ratioZ, char type, int clrIndex, string description) : base(threshold, name)
        {
            this.pos = this.originalPosition = position;
            this.ratioX = ratioX;
            this.ratioY = ratioY;
            this.ratioZ = ratioZ;
            this.type = type;
            this.clrIndex = clrIndex;
            this.description = description;

            this.pseudoneuron = name.StartsWith("Pse");
        }

        public void AddAxon(Synapse s, float weight)
        {
            axons.Add(new Axon(this, weight, s));
        }

        public void Update()
        {
            if (Input.GetKeyUp(KeyCode.A) && selected)
            {
                income += 0.1f;
            }

            income = Math.Min(income, 1.0f);

            if (income > 0.0f)
            {
                axons.ForEach(a => a.Send(1.0f * GetActivity()));
            }
        }

        private float[] neuron_color_r = { 0.32f, 0.66f, 0.0f, 0.58f, 0.39f, 0.20f };
        private float[] neuron_color_g = { 0.39f, 0.66f, 0.5f, 0.0f,  0.0f,  0.00f };
        private float[] neuron_color_b = { 0.39f, 0.0f,  1.0f, 0.58f, 0.39f, 0.58f };

        GameObject sphere;
        Material sphereMaterial;

        public void Draw(GameObject neuronHolder)
        {
            float r = neuron_color_r[clrIndex];
            float g = neuron_color_g[clrIndex];
            float b = neuron_color_b[clrIndex];

            var act = GetActivity();

            r /= 2.0f;
            g /= 2.0f;
            b /= 2.0f;

            r = Mathf.Lerp(r, 1.0f, act);
            g = Mathf.Lerp(g, 1.0f, act);
            b = Mathf.Lerp(b, 1.0f, act);
            
            if (sphere == null)
            {
                sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.name = name;
                sphere.transform.SetParent(neuronHolder.transform);
                var nb = sphere.AddComponent<NeuronBehaviour>();
                nb.neuron = this;
                nb.description = description;

                if (sphereMaterial == null) {
                    sphereMaterial = sphere.GetComponent<Renderer>().material;
                }
            }
            
            sphere.transform.localScale = Vector3.one * SphereScale * (pseudoneuron ? 0.3f : 1.0f);
            sphere.transform.position = pos;
            sphereMaterial.color = new Color(r, g, b);

            bool isVisible = axons.Count > 0 || UniversalConstantsBehaviour.Instance.ShowUnconnectedNeurons;
            sphere.SetActive(isVisible);

            if (UniversalConstantsBehaviour.Instance.ShowAxons && isVisible)
            {
                for(int a = 0; a < axons.Count; a++) { axons[a].Draw(this, r, g, b, selected, neuronHolder); }
            }

            if (isVisible)
            {
                //Debug.DrawLine(pos, originalPosition, Color.cyan);
            }
        }
    }
}