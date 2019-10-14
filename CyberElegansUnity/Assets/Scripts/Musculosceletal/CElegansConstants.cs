using UnityEngine;

[UnityEngine.CreateAssetMenu(fileName = "CElegansConstants", menuName = "C Elegans Constants", order = 0)]
public class CElegansConstants : UnityEngine.ScriptableObject
{
    [SerializeField] public float MuscleStrength = 1.0f;
}