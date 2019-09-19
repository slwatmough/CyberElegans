using System;
using System.Collections;
using System.Collections.Generic;
using Orbitaldrop.Cyberelegans;
using UnityEngine;

[RequireComponent(typeof(CElegansGodBehaviour))]
public class NeuronProbe : MonoBehaviour
{
    [SerializeField] private KeyCode Key;

    [SerializeField] private string[] Neurons;

    private CElegansGodBehaviour cElegansGod;

    void Start()
    {
        cElegansGod = GetComponent<CElegansGodBehaviour>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(Key))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                Array.ForEach(Neurons, n => cElegansGod.CElegans.ShowNeuron(n));
            }
            else
            {
                Array.ForEach(Neurons, n => cElegansGod.CElegans.StimulateNeuron(n));
            }
        }
    }
}
