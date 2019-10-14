using UnityEngine;

[UnityEngine.CreateAssetMenu(fileName = "RFTConstants", menuName = "Resistive Force Theory Constants", order = 0)]
public class ResitiveForceTheoryConstants : UnityEngine.ScriptableObject
{
    [SerializeField] public bool Enabled;
    
    [SerializeField]
    public AnimationCurve TangentialDragCurve;
    
    [SerializeField]
    public AnimationCurve NormalDragCurve;
}