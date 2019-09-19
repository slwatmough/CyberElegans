using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DebugMuscleTester : MonoBehaviour
{
    [FormerlySerializedAs("Press1")] [SerializeField] private PistonMuscle[] PressQ;
    [FormerlySerializedAs("Press2")] [SerializeField] private PistonMuscle[] PressW;
    [FormerlySerializedAs("Press3")] [SerializeField] private PistonMuscle[] PressE;
    [FormerlySerializedAs("Press4")] [SerializeField] private PistonMuscle[] PressR;
    
    [SerializeField] private PistonMuscle[] PressA;
    [SerializeField] private PistonMuscle[] PressS;
    [SerializeField] private PistonMuscle[] PressD;
    [SerializeField] private PistonMuscle[] PressF;

    [SerializeField] private PistonMuscle[] Right;
    [SerializeField] private PistonMuscle[] Left;
    
    [SerializeField] private float Frequency = 1.0f;
    [SerializeField] private float WaveLength = 1.0f;
    [SerializeField] private float Amplitude = 1.0f;
    [SerializeField] private float GateWaveLength = 1.0f;
    
    private float t = 0.0f;

    private bool AutoWiggle = false;
    
    // Update is called once per frame
    void Update()
    {
        TriggerMusclePairOnKey(KeyCode.Q, KeyCode.A, PressQ, PressA);
        TriggerMusclePairOnKey(KeyCode.W, KeyCode.S, PressW, PressS);

        TriggerMusclePairOnKey(KeyCode.E, KeyCode.D, PressE, PressD);
        TriggerMusclePairOnKey(KeyCode.R, KeyCode.F, PressR, PressF);

        t += Time.deltaTime * Frequency;

        if (Input.GetKeyDown(KeyCode.K)) AutoWiggle = !AutoWiggle;
        
        if (Input.GetKey(KeyCode.L) || AutoWiggle)
        {
            for (int i = 0; i < Right.Length; i += 2)
            {
                var DL = Left[i];
                var VL = Left[i + 1];
                var DR = Right[i];
                var VR = Right[i + 1];

                var percent = (float) i / (float) Right.Length;

                var sin = Amplitude * Mathf.Sin(WaveLength * ( t + percent * GateWaveLength))code
                    ;
                DR.Contract(sin);
                VR.Contract(sin);
                DL.Contract(-sin);
                VL.Contract(-sin);
                
//                if (sin > 0.5)
//                {
//                    DR.Contract(1.0f);
//                    VR.Contract(1.0f);
//                    DL.Contract(-1.0f);
//                    VL.Contract(-1.0f);
//                }
//                else if (sin < -0.5)
//                {
//                    DR.Contract(-1.0f);
//                    VR.Contract(-1.0f);
//                    DL.Contract(1.0f);
//                    VL.Contract(1.0f);
//                }
            }
        }
    }
    

    private void TriggerMusclePairOnKey(KeyCode keyCodeContract, KeyCode keyCodeExtend, PistonMuscle[] contract, PistonMuscle[] extend)
    {
        if (Input.GetKey(keyCodeContract))
        {
            foreach (var muscle in contract) { muscle.Contract(1.0f); }
            foreach (var muscle in extend) { muscle.Contract(-1.0f); }
        }
        
        if (Input.GetKey(keyCodeExtend))
        {
            foreach (var muscle in contract) { muscle.Contract(-1.0f); }
            foreach (var muscle in extend) { muscle.Contract(1.0f); }
        }
    }
}
