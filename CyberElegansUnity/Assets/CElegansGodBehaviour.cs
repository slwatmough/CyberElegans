using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CElegansGodBehaviour : MonoBehaviour
{
    public Orbitaldrop.Cyberelegans.CElegans CElegans;
    
    [SerializeField]
    TextAsset Neurons;

    [SerializeField]
    TextAsset Connections;

    [SerializeField]
    TextAsset Muscles;
    
    // Start is called before the first frame update
    void Start()
    {
        var neuronHolder = new GameObject("Neurons");
        neuronHolder.transform.SetParent(transform, false);

        var musclesHolder = new GameObject("Muscles");
        musclesHolder.transform.SetParent(transform, false);

        var masspointHolder = new GameObject("Mass");
        masspointHolder.transform.SetParent(transform, false);

        var springHolder = new GameObject("Springs");
        springHolder.transform.SetParent(transform, false);

        CElegans = new Orbitaldrop.Cyberelegans.CElegans(0.5f, 26, Neurons.text, Connections.text, Muscles.text, gameObject, neuronHolder, musclesHolder, masspointHolder, springHolder);
        CElegans.Update(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        CElegans.Update(Time.deltaTime);
        CElegans.Draw();
    }

    void FixedUpdate()
    {
        CElegans.FixedUpdate();
    }
}
