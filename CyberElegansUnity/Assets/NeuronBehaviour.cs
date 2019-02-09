using System.Collections;
using System.Collections.Generic;
using Orbitaldrop.Cyberelegans;
using UnityEngine;

public class NeuronBehaviour : MonoBehaviour
{
    public string description;

    public Neuron neuron { get; internal set; }

    public bool DebugTrigger = false;

    public float Activity;

    //public void Update()
    //{
    //    Activity = neuron.GetActivity();
    //
    //    if (DebugTrigger)
    //    {
    //        neuron.GetSignal(1.0f);
    //
    //        DebugTrigger = false;
    //    }
    //}
}
