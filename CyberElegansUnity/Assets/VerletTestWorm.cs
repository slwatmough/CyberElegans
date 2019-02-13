using System.Collections;
using System.Collections.Generic;
using Orbitaldrop.Cyberelegans;
using Orbitaldrop.Cyberelegans.Verlet;
using UnityEngine;
using MassPoint = Orbitaldrop.Cyberelegans.Verlet.MassPoint;
using Physics = UnityEngine.Physics;
using Spring = Orbitaldrop.Cyberelegans.Verlet.Spring;

public class VerletTestWorm : MonoBehaviour
{
    private VerletSim sim;

    public class VerletWormFactory : IVerletShapeFactory
    {
        public void Createshape(Vector3 origin, out MassPoint[] massPoints, out Spring[] springs)
        {
            const int size = 26;
            const float length = 0.5f;

            var dl = length;
            var dx = dl * 0.5f * 73.0f / 60.0f;

            /*Width Profile*/
            float[] wp = { 0.35f,0.50f,0.61f,0.68f,0.75f,0.81f,0.85f,0.88f,0.91f,0.93f,0.95f,0.97f,0.99f,1.00f,0.99f,0.98f,0.97f,0.96f,0.95f,0.93f,0.91f,0.88f,0.83f,0.79f,0.70f,0.53f,0.34f};

            var vshift = origin + new Vector3(dl * size / 4.0f + 0.5f, 0.0f, dl / 3.0f / 2.0f);

            massPoints = new MassPoint[9 + size * 9 + 1];
            int p = 0;
            
            springs =  new Spring[(3 * 8) + (size * ((5 * 8) + 1)) + (1 + 8)];
            int s = 0;
            
            const float SpringConstant = 15.0f;

            var massOfPoint = 0.05f;
            
            massPoints[p++] = new MassPoint(massOfPoint, vshift + new Vector3(-0.7f * dx, 0f, 0f));

            massPoints[p++] = new MassPoint(massOfPoint, vshift + new Vector3(-1.5f * dx, -dl * wp[0] / 2f, -0.35f * dl * wp[0]));
            massPoints[p++] = new MassPoint(massOfPoint, vshift + new Vector3(-1.5f * dx, -dl * wp[0] / 2f, 0.35f * dl * wp[0]));
            massPoints[p++] = new MassPoint(massOfPoint, vshift + new Vector3(-1.5f * dx, -0.35f * dl * wp[0], dl * wp[0] / 2f));
            massPoints[p++] = new MassPoint(massOfPoint, vshift + new Vector3(-1.5f * dx, 0.35f * dl * wp[0], dl * wp[0] / 2f));

            massPoints[p++] = new MassPoint(massOfPoint, vshift + new Vector3(-1.5f * dx, dl * wp[0] / 2, 0.35f * dl * wp[0]));
            massPoints[p++] = new MassPoint(massOfPoint, vshift + new Vector3(-1.5f * dx, dl * wp[0] / 2, -0.35f * dl * wp[0]));
            massPoints[p++] = new MassPoint(massOfPoint, vshift + new Vector3(-1.5f * dx, 0.35f * dl * wp[0], - dl * wp[0] / 2f));
            massPoints[p++] = new MassPoint(massOfPoint, vshift + new Vector3(-1.5f * dx, -0.35f * dl * wp[0], - dl * wp[0] / 2f));

            springs[s++] = new Spring(massPoints, 0, 1, SpringConstant);
            springs[s++] = new Spring(massPoints, 0, 2, SpringConstant);
            springs[s++] = new Spring(massPoints, 0, 3, SpringConstant);
            springs[s++] = new Spring(massPoints, 0, 4, SpringConstant);
            springs[s++] = new Spring(massPoints, 0, 5, SpringConstant);
            springs[s++] = new Spring(massPoints, 0, 6, SpringConstant);
            springs[s++] = new Spring(massPoints, 0, 7, SpringConstant);
            springs[s++] = new Spring(massPoints, 0, 8, SpringConstant);
            
            springs[s++] = new Spring(massPoints, 1, 2, SpringConstant);
            springs[s++] = new Spring(massPoints, 2, 3, SpringConstant);
            springs[s++] = new Spring(massPoints, 3, 4, SpringConstant);
            springs[s++] = new Spring(massPoints, 4, 5, SpringConstant);
            springs[s++] = new Spring(massPoints, 5, 6, SpringConstant);
            springs[s++] = new Spring(massPoints, 6, 7, SpringConstant);
            springs[s++] = new Spring(massPoints, 7, 8, SpringConstant);
            springs[s++] = new Spring(massPoints, 8, 1, SpringConstant);
            
            springs[s++] = new Spring(massPoints, 1, 5, SpringConstant);
            springs[s++] = new Spring(massPoints, 2, 6, SpringConstant);
            springs[s++] = new Spring(massPoints, 3, 7, SpringConstant);
            springs[s++] = new Spring(massPoints, 4, 8, SpringConstant);
            springs[s++] = new Spring(massPoints, 1, 6, SpringConstant);
            springs[s++] = new Spring(massPoints, 2, 5, SpringConstant);
            springs[s++] = new Spring(massPoints, 3, 8, SpringConstant);
            springs[s++] = new Spring(massPoints, 4, 7, SpringConstant);

            int i;

            for (i = 1; i < size + 1; i++)
            {
                massPoints[p++] = new MassPoint(massOfPoint, vshift + new Vector3(-(1.0f + i) * dx, 0f, 0f));
                
                massPoints[p++] = new MassPoint(massOfPoint, vshift+new Vector3( -(1.5f+i)*dx, -0.50f*dl*wp[i], -0.35f*dl*wp[i] ));
                massPoints[p++] = new MassPoint(massOfPoint, vshift+new Vector3( -(1.5f+i)*dx, -0.50f*dl*wp[i],  0.35f*dl*wp[i] ));	
                massPoints[p++] = new MassPoint(massOfPoint, vshift+new Vector3( -(1.5f+i)*dx, -0.35f*dl*wp[i],  0.50f*dl*wp[i] ));
                massPoints[p++] = new MassPoint(massOfPoint, vshift+new Vector3( -(1.5f+i)*dx,  0.35f*dl*wp[i],  0.50f*dl*wp[i] ));
                                                                                                                 
                massPoints[p++] = new MassPoint(massOfPoint, vshift+new Vector3( -(1.5f+i)*dx,  0.50f*dl*wp[i],  0.35f*dl*wp[i] ));
                massPoints[p++] = new MassPoint(massOfPoint, vshift+new Vector3( -(1.5f+i)*dx,  0.50f*dl*wp[i], -0.35f*dl*wp[i] ));	
                massPoints[p++] = new MassPoint(massOfPoint, vshift+new Vector3( -(1.5f+i)*dx,  0.35f*dl*wp[i], -0.50f*dl*wp[i] ));
                massPoints[p++] = new MassPoint(massOfPoint, vshift+new Vector3( -(1.5f+i)*dx, -0.35f*dl*wp[i], -0.50f*dl*wp[i] ));

                //============

                springs[s++] = new Spring(massPoints, (i - 1) * 9, i * 9, SpringConstant);
                
                springs[s++] = new Spring(massPoints, i * 9 + 1, i * 9 + 2, SpringConstant);
                springs[s++] = new Spring(massPoints, i * 9 + 2, i * 9 + 3, SpringConstant);
                springs[s++] = new Spring(massPoints, i * 9 + 3, i * 9 + 4, SpringConstant);
                springs[s++] = new Spring(massPoints, i * 9 + 4, i * 9 + 5, SpringConstant);
                springs[s++] = new Spring(massPoints, i * 9 + 5, i * 9 + 6, SpringConstant);
                springs[s++] = new Spring(massPoints, i * 9 + 6, i * 9 + 7, SpringConstant);
                springs[s++] = new Spring(massPoints, i * 9 + 7, i * 9 + 8, SpringConstant);
                springs[s++] = new Spring(massPoints, i * 9 + 8, i * 9 + 1, SpringConstant);
                                    
                springs[s++] = new Spring(massPoints, i * 9 + 1, i * 9 + 0, SpringConstant);
                springs[s++] = new Spring(massPoints, i * 9 + 2, i * 9 + 0, SpringConstant);
                springs[s++] = new Spring(massPoints, i * 9 + 3, i * 9 + 0, SpringConstant);
                springs[s++] = new Spring(massPoints, i * 9 + 4, i * 9 + 0, SpringConstant);
                springs[s++] = new Spring(massPoints, i * 9 + 5, i * 9 + 0, SpringConstant);
                springs[s++] = new Spring(massPoints, i * 9 + 6, i * 9 + 0, SpringConstant);
                springs[s++] = new Spring(massPoints, i * 9 + 7, i * 9 + 0, SpringConstant);
                springs[s++] = new Spring(massPoints, i * 9 + 8, i * 9 + 0, SpringConstant);
                
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 1, i * 9 + 0, SpringConstant / 2.0f);
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 2, i * 9 + 0, SpringConstant / 2.0f);
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 3, i * 9 + 0, SpringConstant / 2.0f);
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 4, i * 9 + 0, SpringConstant / 2.0f);
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 5, i * 9 + 0, SpringConstant / 2.0f);
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 6, i * 9 + 0, SpringConstant / 2.0f);
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 7, i * 9 + 0, SpringConstant / 2.0f);
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 8, i * 9 + 0, SpringConstant / 2.0f);
                
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 1, i * 9 + 2, SpringConstant / 2.0f);
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 2, i * 9 + 3, SpringConstant / 2.0f);
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 3, i * 9 + 4, SpringConstant / 2.0f);
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 4, i * 9 + 5, SpringConstant / 2.0f);
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 5, i * 9 + 6, SpringConstant / 2.0f);
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 6, i * 9 + 7, SpringConstant / 2.0f);
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 7, i * 9 + 8, SpringConstant / 2.0f);
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 8, i * 9 + 1, SpringConstant / 2.0f);
                                
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 2, i * 9 + 1, SpringConstant / 2.0f);
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 3, i * 9 + 2, SpringConstant / 2.0f);
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 4, i * 9 + 3, SpringConstant / 2.0f);
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 5, i * 9 + 4, SpringConstant / 2.0f);
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 6, i * 9 + 5, SpringConstant / 2.0f);
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 7, i * 9 + 6, SpringConstant / 2.0f);
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 8, i * 9 + 7, SpringConstant / 2.0f);
                springs[s++] = new Spring(massPoints, (i - 1) * 9 + 1, i * 9 + 8, SpringConstant / 2.0f);
            }

            massPoints[p++] = new MassPoint(massOfPoint, vshift + new Vector3(-1 * dx - i * dx, 0, 0));

            springs[s++] = new Spring(massPoints, (i - 1) * 9 + 0, i * 9 + 0, SpringConstant);
            springs[s++] = new Spring(massPoints, (i - 1) * 9 + 1, i * 9 + 0, SpringConstant);
            springs[s++] = new Spring(massPoints, (i - 1) * 9 + 2, i * 9 + 0, SpringConstant);
            springs[s++] = new Spring(massPoints, (i - 1) * 9 + 3, i * 9 + 0, SpringConstant);
            springs[s++] = new Spring(massPoints, (i - 1) * 9 + 4, i * 9 + 0, SpringConstant);
            springs[s++] = new Spring(massPoints, (i - 1) * 9 + 5, i * 9 + 0, SpringConstant);
            springs[s++] = new Spring(massPoints, (i - 1) * 9 + 6, i * 9 + 0, SpringConstant);
            springs[s++] = new Spring(massPoints, (i - 1) * 9 + 7, i * 9 + 0, SpringConstant);
            springs[s++] = new Spring(massPoints, (i - 1) * 9 + 8, i * 9 + 0, SpringConstant);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        sim = new VerletSim();
        sim.CreateSimulatedShape(new Vector3(0.0f, 0.3f, 0.0f), new VerletWormFactory());
        sim.DrawPointScale = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        sim.Update(Time.deltaTime);
    }

    void FixedUpdate()
    {
        sim.FixedUpdate(Time.fixedDeltaTime);
    }
}
