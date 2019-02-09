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
        neuronHolder.transform.SetParent(transform, false);

        var musclesHolder = new GameObject("Muscles");
        musclesHolder.transform.SetParent(transform, false);

        var masspointHolder = new GameObject("Mass");
        masspointHolder.transform.SetParent(transform, false);

        var springHolder = new GameObject("Springs");
        springHolder.transform.SetParent(transform, false);

        celegans = new Orbitaldrop.Cyberelegans.CElegans(0.5f, 26, Neurons.text, Connections.text, Muscles.text, gameObject, neuronHolder, musclesHolder, masspointHolder, springHolder);
        celegans.Update(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        celegans.Update(Time.deltaTime);
        celegans.Draw();
    }

    void FixedUpdate()
    {
        celegans.FixedUpdate();
    }
}
