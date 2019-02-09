using UnityEngine;

public class UniversalConstantsBehaviour : MonoBehaviour
{
    public static UniversalConstantsBehaviour Instance;

    [SerializeField]
    public float SynapseSignalGainPerSecond = 6.0f;

    [SerializeField]
    public float SynapseSignalDegredationPerSecond = 5.0f;

    [SerializeField]
    public bool ShowSprings = false;

    [SerializeField]
    public bool ShowAxons = false;

    [SerializeField]
    public bool ShowMuscleAxons = false;

    [SerializeField]
    public bool HideUnconnectedNeurons = true;

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
