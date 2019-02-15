using UnityEngine;
using UnityEngine.Serialization;

public class UniversalConstantsBehaviour : MonoBehaviour
{
    public static UniversalConstantsBehaviour Instance;
        
    [SerializeField]
    public bool ShowMassPoints = true;

    [SerializeField]
    public bool ShowMuscles = true;

    [Header("Springs")]

    [SerializeField]
    public bool ShowSprings = false;

    [SerializeField]
    public float SpringSnapPoint = 1.2f;

    [FormerlySerializedAs("StiffCoeff")] [SerializeField]
    public float VerletSpringStrength = 80.0f;

    [FormerlySerializedAs("FrictCoeff")] [SerializeField]
    public float VerletDamping = 0.6f;
    
    [Header("Neurons")]

    [SerializeField]
    public float SynapseSignalGainPerSecond = 6.0f;

    [FormerlySerializedAs("SynapseSignalDegredationPerSecond")] [SerializeField]
    public float SynapseSignalLossPercentagePerSecond = 5.0f;

    [SerializeField]
    public bool ShowNeurons = true;

    [SerializeField]
    public bool ShowUnconnectedNeurons = true;

    [SerializeField]
    public bool ShowAxons = false;

    [SerializeField]
    public bool ShowMuscleAxons = false;

    [SerializeField]
    public bool UpdateNeuronPositions = true;

    [Header("Physics")]

    [SerializeField]
    public bool EnablePhysics = true;

    [SerializeField]
    public float AirFrictionCoefficient = 0.002f;

    [SerializeField]
    public float GroundAbsorptionConstant = 2.0f;

    [SerializeField]
    public float GroundRepulsionConstant = 100.0f;

    [SerializeField]
    public float GroundFrictionConstant = 0.6f;
       

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.InvalidOperationException("Cannot have more than one UniversalConstantsBehaviour.");
        }
    }
}
