﻿using UnityEngine;

namespace Orbitaldrop.Cyberelegans
{
    public class Synapse
    {
        public float income { get; set; }
        public float threshold { get; set; }
        public string name { get; set; }
        public bool selected { get; set; }
        public Vector3 pos { get; set; }

        public Synapse(float threshold, string name)
        {
            this.threshold = threshold;
            this.name = name;
        }

        public void GetSignal(float signal)
        {
            income += signal * UniversalConstantsBehaviour.Instance.SynapseSignalGainPerSecond * Time.deltaTime;
        }

        public void CheckActivity()
        {
            income = Mathf.Min(income, 1.0f);

            var lossPerSecond = UniversalConstantsBehaviour.Instance.SynapseSignalLossPercentagePerSecond;
            var loss = income *  lossPerSecond * Time.deltaTime;
            income = Mathf.Max(0.0f, income - loss);
        }

        public float GetActivity()
        {
            return income;
        }
    }
}