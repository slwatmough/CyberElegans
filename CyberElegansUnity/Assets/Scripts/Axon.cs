using UnityEngine;

namespace Orbitaldrop.Cyberelegans
{
    public class Axon
    {
        private readonly Neuron parent;

        public float weight;

        public Synapse synapse;

        private readonly int linkWithMuscle;

        private readonly bool isBright;

        public Axon(Neuron parent, float weight, Synapse synapse)
        {
            this.parent = parent;
            this.weight = weight;
            this.synapse = synapse;

            linkWithMuscle = 0;
            var targetName = synapse.name;
            if (targetName[0] == 'D' || targetName[0] == 'V')
            {
                if (targetName[1] == 'R' || targetName[1] == 'L')
                {
                    if ((targetName[2] == '0' || targetName[2] == '1') || targetName[2] == 'J')
                    {
                        linkWithMuscle = 1;

                        if (parent.name.StartsWith("Pse_"))
                        {
                            if (parent.name[4] == 'V' || parent.name[4] == 'D' || parent.name[5] == 'B')
                            {
                                linkWithMuscle = 2;
                            }
                        }
                    }
                }
            }

            isBright =  synapse.name.StartsWith("DL") ||
                        synapse.name.StartsWith("DR") ||
                        synapse.name.StartsWith("VL") ||
                        synapse.name.StartsWith("VR");
        }

        public void Send(float senderActivity)
        {
            synapse.GetSignal(senderActivity);
        }

        public void Draw(Neuron parent, float r, float g, float b, bool selected, GameObject neuronHolder )
        {
            if (linkWithMuscle == 0 || (linkWithMuscle == 2 && UniversalConstantsBehaviour.Instance.ShowMuscleAxons) || selected)
            {
                var colorScale = 0.8f;

                if (isBright)
                {
                    colorScale = 1.0f;
                }

                colorScale = Mathf.Lerp(colorScale, 1.0f, parent.GetActivity());

                var c = new Color(Mathf.Min(1.0f, r * colorScale), Mathf.Min(1.0f, g * colorScale), Mathf.Min(1.0f, b * colorScale));

                Debug.DrawLine(parent.pos, synapse.pos, c);
            }
        }
    }
}