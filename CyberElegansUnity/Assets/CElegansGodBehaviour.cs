using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CElegansGodBehaviour : MonoBehaviour
{
    Orbitaldrop.Cyberelegans.CElegans celegans;
    
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
        neuronHolder.transform.SetParent(transform);

        var musclesHolder = new GameObject("Muscles");
        musclesHolder.transform.SetParent(transform);

        var masspointHolder = new GameObject("Mass");
        masspointHolder.transform.SetParent(transform);

        var springHolder = new GameObject("Springs");
        springHolder.transform.SetParent(transform);

        celegans = new Orbitaldrop.Cyberelegans.CElegans(0.5f, 26, Neurons.text, Connections.text, Muscles.text, neuronHolder, musclesHolder, masspointHolder, springHolder);
        celegans.iteration(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        celegans.iteration(Time.deltaTime);
        celegans.Draw();
    }
}
