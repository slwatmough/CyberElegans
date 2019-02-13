using System;
using System.Collections.Generic;
using System.IO;
using Orbitaldrop.Cyberelegans.Verlet;
using UnityEngine;

namespace Orbitaldrop.Cyberelegans
{
    public class CElegans
    {
        private readonly GameObject neuronHolder;
        private readonly GameObject musclesHolder;
        private readonly GameObject masspointHolder;
        private readonly GameObject springHolder;

        private int size;
        private readonly GameObject wormGO;
        private float length;

        private float time;

        //private List<MassPoint> mPoint = new List<MassPoint>();

        //private MassPoint[] mPoint = new MassPoint[250];
        //private int nextMassPoint;

        //private List<Spring> spring = new List<Spring>();

        //private Spring[] spring = new Spring[1100];
        //private int nextSpring = 0;
        
        //private List<Muscle> muscle = new List<Muscle>();

        private Muscle[] muscle = new Muscle[200];
        private int nextMuscle = 0;
        
        //private List<Neuron> neuron = new List<Neuron>();

        private Neuron[] neuron = new Neuron[700];
        private int nextNeuron = 0;

        //private List<List<Neuron>> table = new List<List<Neuron>>();

        private Neuron[][] table = new Neuron[MAX_NUMBER_OFTABLES][];
        private int nextTable = 0;
        private const int MAX_NUMBER_OFTABLES = 30;
        private const int MAX_NEURONS_IN_TABLE_ENTRY = 150;
        private int[] nextTableFor = new int[MAX_NUMBER_OFTABLES];

        private Vector3 vshift;

        private float dl;

        private float dx;

        private VerletSim verlet = new VerletSim();
        
        public CElegans(float length, int size, string neurons, string connections, string muscles, GameObject wormGO, GameObject neuronHolder, GameObject musclesHolder, GameObject masspointHolder, GameObject springHolder)
        {
            this.wormGO = wormGO;
            this.neuronHolder = neuronHolder;
            this.musclesHolder = musclesHolder;
            this.masspointHolder = masspointHolder;
            this.springHolder = springHolder;

            this.size = size;
            this.length = length;

            dl = length;
            dx = dl * 0.5f * 73.0f / 60.0f;

            /*Width Profile*/
            float[] wp = { 0.35f,0.50f,0.61f,0.68f,0.75f,0.81f,0.85f,0.88f,0.91f,0.93f,0.95f,0.97f,0.99f,1.00f,0.99f,0.98f,0.97f,0.96f,0.95f,0.93f,0.91f,0.88f,0.83f,0.79f,0.70f,0.53f,0.34f};

            vshift = wormGO.transform.position + new Vector3(dl * size / 4.0f + 0.5f, 0.0f, dl / 3.0f / 2.0f);

            verlet.CreateSimulatedShape(wormGO.transform.position, new VerletTestWorm.VerletWormFactory());
            verlet.DrawPointScale = 0.05f;
            
            #region Head
//            addMPoint(new MassPoint(masspointHolder, 0.05f, vshift + new Vector3(-0.7f * dx, 0f, 0f)));
//
//            addMPoint(new MassPoint(masspointHolder, 0.05f, vshift + new Vector3(-1.5f * dx, -dl * wp[0] / 2f, -0.35f * dl * wp[0])));
//            addMPoint(new MassPoint(masspointHolder, 0.05f, vshift + new Vector3(-1.5f * dx, -dl * wp[0] / 2f, 0.35f * dl * wp[0])));
//            addMPoint(new MassPoint(masspointHolder, 0.05f, vshift + new Vector3(-1.5f * dx, -0.35f * dl * wp[0], dl * wp[0] / 2f)));
//            addMPoint(new MassPoint(masspointHolder, 0.05f, vshift + new Vector3(-1.5f * dx, 0.35f * dl * wp[0], dl * wp[0] / 2f)));
//
//            addMPoint(new MassPoint(masspointHolder, 0.05f, vshift + new Vector3(-1.5f * dx, dl * wp[0] / 2, 0.35f * dl * wp[0])));
//            addMPoint(new MassPoint(masspointHolder, 0.05f, vshift + new Vector3(-1.5f * dx, dl * wp[0] / 2, -0.35f * dl * wp[0])));
//            addMPoint(new MassPoint(masspointHolder, 0.05f, vshift + new Vector3(-1.5f * dx, 0.35f * dl * wp[0], - dl * wp[0] / 2f)));
//            addMPoint(new MassPoint(masspointHolder, 0.05f, vshift + new Vector3(-1.5f * dx, -0.35f * dl * wp[0], - dl * wp[0] / 2f)));
//
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[0], verlet.MassPoints[1]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[0], verlet.MassPoints[2]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[0], verlet.MassPoints[3]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[0], verlet.MassPoints[4]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[0], verlet.MassPoints[5]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[0], verlet.MassPoints[6]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[0], verlet.MassPoints[7]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[0], verlet.MassPoints[8]));
//            /**/                                             
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[1], verlet.MassPoints[2]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[2], verlet.MassPoints[3]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[3], verlet.MassPoints[4]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[4], verlet.MassPoints[5]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[5], verlet.MassPoints[6]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[6], verlet.MassPoints[7]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[7], verlet.MassPoints[8]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[8], verlet.MassPoints[1]));
//            /**/                                     
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[1], verlet.MassPoints[5]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[2], verlet.MassPoints[6]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[3], verlet.MassPoints[7]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[4], verlet.MassPoints[8]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[1], verlet.MassPoints[6]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[2], verlet.MassPoints[5]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[3], verlet.MassPoints[8]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[4], verlet.MassPoints[7]));

            table[nextTable++] = new Neuron[MAX_NEURONS_IN_TABLE_ENTRY];
            #endregion

            int i;

            #region Body
            for (i = 1; i < size + 1; i++)
            {
//                addMPoint(new MassPoint(masspointHolder, 0.05f, vshift+new Vector3( -(1.0f+i)*dx, 0f		, 0f )) );
//                
//                addMPoint(new MassPoint(masspointHolder, 0.05f, vshift+new Vector3( -(1.5f+i)*dx, -0.50f*dl*wp[i], -0.35f*dl*wp[i] )) );
//                addMPoint(new MassPoint(masspointHolder, 0.05f, vshift+new Vector3( -(1.5f+i)*dx, -0.50f*dl*wp[i],  0.35f*dl*wp[i] )) );	
//                addMPoint(new MassPoint(masspointHolder, 0.05f, vshift+new Vector3( -(1.5f+i)*dx, -0.35f*dl*wp[i],  0.50f*dl*wp[i] )) );
//                addMPoint(new MassPoint(masspointHolder, 0.05f, vshift+new Vector3( -(1.5f+i)*dx,  0.35f*dl*wp[i],  0.50f*dl*wp[i] )) );
//                                                                                                                 
//                addMPoint(new MassPoint(masspointHolder, 0.05f, vshift+new Vector3( -(1.5f+i)*dx,  0.50f*dl*wp[i],  0.35f*dl*wp[i] )) );
//                addMPoint(new MassPoint(masspointHolder, 0.05f, vshift+new Vector3( -(1.5f+i)*dx,  0.50f*dl*wp[i], -0.35f*dl*wp[i] )) );	
//                addMPoint(new MassPoint(masspointHolder, 0.05f, vshift+new Vector3( -(1.5f+i)*dx,  0.35f*dl*wp[i], -0.50f*dl*wp[i] )) );
//                addMPoint(new MassPoint(masspointHolder, 0.05f, vshift+new Vector3( -(1.5f+i)*dx, -0.35f*dl*wp[i], -0.50f*dl*wp[i] )) );
//
//                //============
//
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9], verlet.MassPoints[i * 9]));
//
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[i * 9 + 1], verlet.MassPoints[i * 9 + 2]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[i * 9 + 2], verlet.MassPoints[i * 9 + 3]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[i * 9 + 3], verlet.MassPoints[i * 9 + 4]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[i * 9 + 4], verlet.MassPoints[i * 9 + 5]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[i * 9 + 5], verlet.MassPoints[i * 9 + 6]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[i * 9 + 6], verlet.MassPoints[i * 9 + 7]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[i * 9 + 7], verlet.MassPoints[i * 9 + 8]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[i * 9 + 8], verlet.MassPoints[i * 9 + 1]));
//                                     
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[i * 9 + 1], verlet.MassPoints[i * 9 + 0]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[i * 9 + 2], verlet.MassPoints[i * 9 + 0]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[i * 9 + 3], verlet.MassPoints[i * 9 + 0]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[i * 9 + 4], verlet.MassPoints[i * 9 + 0]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[i * 9 + 5], verlet.MassPoints[i * 9 + 0]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[i * 9 + 6], verlet.MassPoints[i * 9 + 0]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[i * 9 + 7], verlet.MassPoints[i * 9 + 0]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[i * 9 + 8], verlet.MassPoints[i * 9 + 0]));
//                                     
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 1], verlet.MassPoints[i * 9 + 0]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 2], verlet.MassPoints[i * 9 + 0]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 3], verlet.MassPoints[i * 9 + 0]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 4], verlet.MassPoints[i * 9 + 0]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 5], verlet.MassPoints[i * 9 + 0]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 6], verlet.MassPoints[i * 9 + 0]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 7], verlet.MassPoints[i * 9 + 0]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 8], verlet.MassPoints[i * 9 + 0]));
//                /**/                 
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler / 2, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 1], verlet.MassPoints[i * 9 + 2]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler / 2, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 2], verlet.MassPoints[i * 9 + 3]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler / 2, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 3], verlet.MassPoints[i * 9 + 4]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler / 2, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 4], verlet.MassPoints[i * 9 + 5]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler / 2, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 5], verlet.MassPoints[i * 9 + 6]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler / 2, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 6], verlet.MassPoints[i * 9 + 7]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler / 2, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 7], verlet.MassPoints[i * 9 + 8]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler / 2, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 8], verlet.MassPoints[i * 9 + 1]));
//                /**/                 
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler / 2, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 2], verlet.MassPoints[i * 9 + 1]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler / 2, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 3], verlet.MassPoints[i * 9 + 2]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler / 2, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 4], verlet.MassPoints[i * 9 + 3]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler / 2, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 5], verlet.MassPoints[i * 9 + 4]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler / 2, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 6], verlet.MassPoints[i * 9 + 5]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler / 2, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 7], verlet.MassPoints[i * 9 + 6]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler / 2, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 8], verlet.MassPoints[i * 9 + 7]));
//                addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler / 2, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 1], verlet.MassPoints[i * 9 + 8]));

                table[nextTable++] = new Neuron[MAX_NEURONS_IN_TABLE_ENTRY];
            }

            addMuscle(new Muscle(verlet.MassPoints, 0 * 9 + 1, 1 * 9 + 1, "VL02"));
            addMuscle(new Muscle(verlet.MassPoints, 2 * 9 + 1, 1 * 9 + 1, "VL02"));
            addMuscle(new Muscle(verlet.MassPoints, 2 * 9 + 1, 3 * 9 + 1, "VL04"));
            addMuscle(new Muscle(verlet.MassPoints, 4 * 9 + 1, 3 * 9 + 1, "VL04"));
            addMuscle(new Muscle(verlet.MassPoints, 4 * 9 + 1, 5 * 9 + 1, "VL06"));
            addMuscle(new Muscle(verlet.MassPoints, 6 * 9 + 1, 5 * 9 + 1, "VL06"));
            addMuscle(new Muscle(verlet.MassPoints, 6 * 9 + 1, 7 * 9 + 1, "VL08"));
            addMuscle(new Muscle(verlet.MassPoints, 8 * 9 + 1, 7 * 9 + 1, "VL08"));
            addMuscle(new Muscle(verlet.MassPoints, 8 * 9 + 1, 9 * 9 + 1, "VL10"));
            addMuscle(new Muscle(verlet.MassPoints, 10 * 9 + 1, 9 * 9 + 1, "VL10"));
            addMuscle(new Muscle(verlet.MassPoints, 10 * 9 + 1, 11 * 9 + 1, "VL12"));
            addMuscle(new Muscle(verlet.MassPoints, 12 * 9 + 1, 11 * 9 + 1, "VL12"));
            addMuscle(new Muscle(verlet.MassPoints, 12 * 9 + 1, 13 * 9 + 1, "VL14"));
            addMuscle(new Muscle(verlet.MassPoints, 14 * 9 + 1, 13 * 9 + 1, "VL14"));
            addMuscle(new Muscle(verlet.MassPoints, 14 * 9 + 1, 15 * 9 + 1, "VL16"));
            addMuscle(new Muscle(verlet.MassPoints, 16 * 9 + 1, 15 * 9 + 1, "VL16"));
            addMuscle(new Muscle(verlet.MassPoints, 16 * 9 + 1, 17 * 9 + 1, "VL18"));
            addMuscle(new Muscle(verlet.MassPoints, 18 * 9 + 1, 17 * 9 + 1, "VL18"));
            addMuscle(new Muscle(verlet.MassPoints, 18 * 9 + 1, 19 * 9 + 1, "VL20"));
            addMuscle(new Muscle(verlet.MassPoints, 20 * 9 + 1, 19 * 9 + 1, "VL20"));
            addMuscle(new Muscle(verlet.MassPoints, 20 * 9 + 1, 21 * 9 + 1, "VL21"));
            addMuscle(new Muscle(verlet.MassPoints, 22 * 9 + 1, 21 * 9 + 1, "VL21"));
            addMuscle(new Muscle(verlet.MassPoints, 22 * 9 + 1, 23 * 9 + 1, "VL22"));
            addMuscle(new Muscle(verlet.MassPoints, 24 * 9 + 1, 23 * 9 + 1, "VL22"));
            addMuscle(new Muscle(verlet.MassPoints, 24 * 9 + 1, 25 * 9 + 1, "VL23"));
            addMuscle(new Muscle(verlet.MassPoints, 26 * 9 + 1, 25 * 9 + 1, "VL23"));
                         
            addMuscle(new Muscle(verlet.MassPoints, 0 * 9 + 2, 1 * 9 + 2, "VR02"));
            addMuscle(new Muscle(verlet.MassPoints, 2 * 9 + 2, 1 * 9 + 2, "VR02"));
            addMuscle(new Muscle(verlet.MassPoints, 2 * 9 + 2, 3 * 9 + 2, "VR04"));
            addMuscle(new Muscle(verlet.MassPoints, 4 * 9 + 2, 3 * 9 + 2, "VR04"));
            addMuscle(new Muscle(verlet.MassPoints, 4 * 9 + 2, 5 * 9 + 2, "VR06"));
            addMuscle(new Muscle(verlet.MassPoints, 6 * 9 + 2, 5 * 9 + 2, "VR06"));
            addMuscle(new Muscle(verlet.MassPoints, 6 * 9 + 2, 7 * 9 + 2, "VR08"));
            addMuscle(new Muscle(verlet.MassPoints, 8 * 9 + 2, 7 * 9 + 2, "VR08"));
            addMuscle(new Muscle(verlet.MassPoints, 8 * 9 + 2, 9 * 9 + 2, "VR10"));
            addMuscle(new Muscle(verlet.MassPoints, 10 * 9 + 2, 9 * 9 + 2, "VR10"));
            addMuscle(new Muscle(verlet.MassPoints, 10 * 9 + 2, 11 * 9 + 2, "VR12"));
            addMuscle(new Muscle(verlet.MassPoints, 12 * 9 + 2, 11 * 9 + 2, "VR12"));
            addMuscle(new Muscle(verlet.MassPoints, 12 * 9 + 2, 13 * 9 + 2, "VR14"));
            addMuscle(new Muscle(verlet.MassPoints, 14 * 9 + 2, 13 * 9 + 2, "VR14"));
            addMuscle(new Muscle(verlet.MassPoints, 14 * 9 + 2, 15 * 9 + 2, "VR16"));
            addMuscle(new Muscle(verlet.MassPoints, 16 * 9 + 2, 15 * 9 + 2, "VR16"));
            addMuscle(new Muscle(verlet.MassPoints, 16 * 9 + 2, 17 * 9 + 2, "VR18"));
            addMuscle(new Muscle(verlet.MassPoints, 18 * 9 + 2, 17 * 9 + 2, "VR18"));
            addMuscle(new Muscle(verlet.MassPoints, 18 * 9 + 2, 19 * 9 + 2, "VR20"));
            addMuscle(new Muscle(verlet.MassPoints, 20 * 9 + 2, 19 * 9 + 2, "VR20"));
            addMuscle(new Muscle(verlet.MassPoints, 20 * 9 + 2, 21 * 9 + 2, "VR22"));
            addMuscle(new Muscle(verlet.MassPoints, 22 * 9 + 2, 21 * 9 + 2, "VR22"));
            addMuscle(new Muscle(verlet.MassPoints, 22 * 9 + 2, 23 * 9 + 2, "VR23"));
            addMuscle(new Muscle(verlet.MassPoints, 24 * 9 + 2, 23 * 9 + 2, "VR23"));
            addMuscle(new Muscle(verlet.MassPoints, 24 * 9 + 2, 25 * 9 + 2, "VR24"));
            addMuscle(new Muscle(verlet.MassPoints, 26 * 9 + 2, 25 * 9 + 2, "VR24"));
           
            addMuscle(new Muscle(verlet.MassPoints, 1 * 9 + 3, 2 * 9 + 3, "VR01"));
            addMuscle(new Muscle(verlet.MassPoints, 3 * 9 + 3, 2 * 9 + 3, "VR01"));
            addMuscle(new Muscle(verlet.MassPoints, 3 * 9 + 3, 4 * 9 + 3, "VR03"));
            addMuscle(new Muscle(verlet.MassPoints, 5 * 9 + 3, 4 * 9 + 3, "VR03"));
            addMuscle(new Muscle(verlet.MassPoints, 5 * 9 + 3, 6 * 9 + 3, "VR05"));
            addMuscle(new Muscle(verlet.MassPoints, 7 * 9 + 3, 6 * 9 + 3, "VR05"));
            addMuscle(new Muscle(verlet.MassPoints, 7 * 9 + 3, 8 * 9 + 3, "VR07"));
            addMuscle(new Muscle(verlet.MassPoints, 9 * 9 + 3, 8 * 9 + 3, "VR07"));
            addMuscle(new Muscle(verlet.MassPoints, 9 * 9 + 3, 10 * 9 + 3, "VR09"));
            addMuscle(new Muscle(verlet.MassPoints, 11 * 9 + 3, 10 * 9 + 3, "VR09"));
            addMuscle(new Muscle(verlet.MassPoints, 11 * 9 + 3, 12 * 9 + 3, "VR11"));
            addMuscle(new Muscle(verlet.MassPoints, 13 * 9 + 3, 12 * 9 + 3, "VR11"));
            addMuscle(new Muscle(verlet.MassPoints, 13 * 9 + 3, 14 * 9 + 3, "VR13"));
            addMuscle(new Muscle(verlet.MassPoints, 15 * 9 + 3, 14 * 9 + 3, "VR13"));
            addMuscle(new Muscle(verlet.MassPoints, 15 * 9 + 3, 16 * 9 + 3, "VR15"));
            addMuscle(new Muscle(verlet.MassPoints, 17 * 9 + 3, 16 * 9 + 3, "VR15"));
            addMuscle(new Muscle(verlet.MassPoints, 17 * 9 + 3, 18 * 9 + 3, "VR17"));
            addMuscle(new Muscle(verlet.MassPoints, 19 * 9 + 3, 18 * 9 + 3, "VR17"));
            addMuscle(new Muscle(verlet.MassPoints, 19 * 9 + 3, 20 * 9 + 3, "VR19"));
            addMuscle(new Muscle(verlet.MassPoints, 21 * 9 + 3, 20 * 9 + 3, "VR19"));
            addMuscle(new Muscle(verlet.MassPoints, 21 * 9 + 3, 22 * 9 + 3, "VR21"));
            addMuscle(new Muscle(verlet.MassPoints, 23 * 9 + 3, 22 * 9 + 3, "VR21"));
          
            addMuscle(new Muscle(verlet.MassPoints, 1 * 9 + 4, 2 * 9 + 4, "DR01"));
            addMuscle(new Muscle(verlet.MassPoints, 3 * 9 + 4, 2 * 9 + 4, "DR01"));
            addMuscle(new Muscle(verlet.MassPoints, 3 * 9 + 4, 4 * 9 + 4, "DR03"));
            addMuscle(new Muscle(verlet.MassPoints, 5 * 9 + 4, 4 * 9 + 4, "DR03"));
            addMuscle(new Muscle(verlet.MassPoints, 5 * 9 + 4, 6 * 9 + 4, "DR05"));
            addMuscle(new Muscle(verlet.MassPoints, 7 * 9 + 4, 6 * 9 + 4, "DR05"));
            addMuscle(new Muscle(verlet.MassPoints, 7 * 9 + 4, 8 * 9 + 4, "DR07"));
            addMuscle(new Muscle(verlet.MassPoints, 9 * 9 + 4, 8 * 9 + 4, "DR07"));
            addMuscle(new Muscle(verlet.MassPoints, 9 * 9 + 4, 10 * 9 + 4, "DR09"));
            addMuscle(new Muscle(verlet.MassPoints, 11 * 9 + 4, 10 * 9 + 4, "DR09"));
            addMuscle(new Muscle(verlet.MassPoints, 11 * 9 + 4, 12 * 9 + 4, "DR11"));
            addMuscle(new Muscle(verlet.MassPoints, 13 * 9 + 4, 12 * 9 + 4, "DR11"));
            addMuscle(new Muscle(verlet.MassPoints, 13 * 9 + 4, 14 * 9 + 4, "DR13"));
            addMuscle(new Muscle(verlet.MassPoints, 15 * 9 + 4, 14 * 9 + 4, "DR13"));
            addMuscle(new Muscle(verlet.MassPoints, 15 * 9 + 4, 16 * 9 + 4, "DR15"));
            addMuscle(new Muscle(verlet.MassPoints, 17 * 9 + 4, 16 * 9 + 4, "DR15"));
            addMuscle(new Muscle(verlet.MassPoints, 17 * 9 + 4, 18 * 9 + 4, "DR17"));
            addMuscle(new Muscle(verlet.MassPoints, 19 * 9 + 4, 18 * 9 + 4, "DR17"));
            addMuscle(new Muscle(verlet.MassPoints, 19 * 9 + 4, 20 * 9 + 4, "DR19"));
            addMuscle(new Muscle(verlet.MassPoints, 21 * 9 + 4, 20 * 9 + 4, "DR19"));
            addMuscle(new Muscle(verlet.MassPoints, 21 * 9 + 4, 22 * 9 + 4, "DR21"));
            addMuscle(new Muscle(verlet.MassPoints, 23 * 9 + 4, 22 * 9 + 4, "DR21"));
            addMuscle(new Muscle(verlet.MassPoints, 23 * 9 + 4, 24 * 9 + 4, "DR23"));
            addMuscle(new Muscle(verlet.MassPoints, 25 * 9 + 4, 24 * 9 + 4, "DR23"));
          
            addMuscle(new Muscle(verlet.MassPoints, 0 * 9 + 5, 1 * 9 + 5, "DR02"));
            addMuscle(new Muscle(verlet.MassPoints, 2 * 9 + 5, 1 * 9 + 5, "DR02"));
            addMuscle(new Muscle(verlet.MassPoints, 2 * 9 + 5, 3 * 9 + 5, "DR04"));
            addMuscle(new Muscle(verlet.MassPoints, 4 * 9 + 5, 3 * 9 + 5, "DR04"));
            addMuscle(new Muscle(verlet.MassPoints, 4 * 9 + 5, 5 * 9 + 5, "DR06"));
            addMuscle(new Muscle(verlet.MassPoints, 6 * 9 + 5, 5 * 9 + 5, "DR06"));
            addMuscle(new Muscle(verlet.MassPoints, 6 * 9 + 5, 7 * 9 + 5, "DR08"));
            addMuscle(new Muscle(verlet.MassPoints, 8 * 9 + 5, 7 * 9 + 5, "DR08"));
            addMuscle(new Muscle(verlet.MassPoints, 8 * 9 + 5, 9 * 9 + 5, "DR10"));
            addMuscle(new Muscle(verlet.MassPoints, 10 * 9 + 5, 9 * 9 + 5, "DR10"));
            addMuscle(new Muscle(verlet.MassPoints, 10 * 9 + 5, 11 * 9 + 5, "DR12"));
            addMuscle(new Muscle(verlet.MassPoints, 12 * 9 + 5, 11 * 9 + 5, "DR12"));
            addMuscle(new Muscle(verlet.MassPoints, 12 * 9 + 5, 13 * 9 + 5, "DR14"));
            addMuscle(new Muscle(verlet.MassPoints, 14 * 9 + 5, 13 * 9 + 5, "DR14"));
            addMuscle(new Muscle(verlet.MassPoints, 14 * 9 + 5, 15 * 9 + 5, "DR16"));
            addMuscle(new Muscle(verlet.MassPoints, 16 * 9 + 5, 15 * 9 + 5, "DR16"));
            addMuscle(new Muscle(verlet.MassPoints, 16 * 9 + 5, 17 * 9 + 5, "DR18"));
            addMuscle(new Muscle(verlet.MassPoints, 18 * 9 + 5, 17 * 9 + 5, "DR18"));
            addMuscle(new Muscle(verlet.MassPoints, 18 * 9 + 5, 19 * 9 + 5, "DR20"));
            addMuscle(new Muscle(verlet.MassPoints, 20 * 9 + 5, 19 * 9 + 5, "DR20"));
            addMuscle(new Muscle(verlet.MassPoints, 20 * 9 + 5, 21 * 9 + 5, "DR22"));
            addMuscle(new Muscle(verlet.MassPoints, 22 * 9 + 5, 21 * 9 + 5, "DR22"));
            addMuscle(new Muscle(verlet.MassPoints, 22 * 9 + 5, 23 * 9 + 5, "DR24"));
            addMuscle(new Muscle(verlet.MassPoints, 24 * 9 + 5, 23 * 9 + 5, "DR24"));
          
            addMuscle(new Muscle(verlet.MassPoints, 0 * 9 + 6, 1 * 9 + 6, "DL02"));
            addMuscle(new Muscle(verlet.MassPoints, 2 * 9 + 6, 1 * 9 + 6, "DL02"));
            addMuscle(new Muscle(verlet.MassPoints, 2 * 9 + 6, 3 * 9 + 6, "DL04"));
            addMuscle(new Muscle(verlet.MassPoints, 4 * 9 + 6, 3 * 9 + 6, "DL04"));
            addMuscle(new Muscle(verlet.MassPoints, 4 * 9 + 6, 5 * 9 + 6, "DL06"));
            addMuscle(new Muscle(verlet.MassPoints, 6 * 9 + 6, 5 * 9 + 6, "DL06"));
            addMuscle(new Muscle(verlet.MassPoints, 6 * 9 + 6, 7 * 9 + 6, "DL08"));
            addMuscle(new Muscle(verlet.MassPoints, 8 * 9 + 6, 7 * 9 + 6, "DL08"));
            addMuscle(new Muscle(verlet.MassPoints, 8 * 9 + 6, 9 * 9 + 6, "DL10"));
            addMuscle(new Muscle(verlet.MassPoints, 10 * 9 + 6, 9 * 9 + 6, "DL10"));
            addMuscle(new Muscle(verlet.MassPoints, 10 * 9 + 6, 11 * 9 + 6, "DL12"));
            addMuscle(new Muscle(verlet.MassPoints, 12 * 9 + 6, 11 * 9 + 6, "DL12"));
            addMuscle(new Muscle(verlet.MassPoints, 12 * 9 + 6, 13 * 9 + 6, "DL14"));
            addMuscle(new Muscle(verlet.MassPoints, 14 * 9 + 6, 13 * 9 + 6, "DL14"));
            addMuscle(new Muscle(verlet.MassPoints, 14 * 9 + 6, 15 * 9 + 6, "DL16"));
            addMuscle(new Muscle(verlet.MassPoints, 16 * 9 + 6, 15 * 9 + 6, "DL16"));
            addMuscle(new Muscle(verlet.MassPoints, 16 * 9 + 6, 17 * 9 + 6, "DL18"));
            addMuscle(new Muscle(verlet.MassPoints, 18 * 9 + 6, 17 * 9 + 6, "DL18"));
            addMuscle(new Muscle(verlet.MassPoints, 18 * 9 + 6, 19 * 9 + 6, "DL20"));
            addMuscle(new Muscle(verlet.MassPoints, 20 * 9 + 6, 19 * 9 + 6, "DL20"));
            addMuscle(new Muscle(verlet.MassPoints, 20 * 9 + 6, 21 * 9 + 6, "DL22"));
            addMuscle(new Muscle(verlet.MassPoints, 22 * 9 + 6, 21 * 9 + 6, "DL22"));
            addMuscle(new Muscle(verlet.MassPoints, 22 * 9 + 6, 23 * 9 + 6, "DL24"));
            addMuscle(new Muscle(verlet.MassPoints, 24 * 9 + 6, 23 * 9 + 6, "DL24"));
                               
            addMuscle(new Muscle(verlet.MassPoints, 1 * 9 + 7, 2 * 9 + 7, "DL01"));
            addMuscle(new Muscle(verlet.MassPoints, 3 * 9 + 7, 2 * 9 + 7, "DL01"));
            addMuscle(new Muscle(verlet.MassPoints, 3 * 9 + 7, 4 * 9 + 7, "DL03"));
            addMuscle(new Muscle(verlet.MassPoints, 5 * 9 + 7, 4 * 9 + 7, "DL03"));
            addMuscle(new Muscle(verlet.MassPoints, 5 * 9 + 7, 6 * 9 + 7, "DL05"));
            addMuscle(new Muscle(verlet.MassPoints, 7 * 9 + 7, 6 * 9 + 7, "DL05"));
            addMuscle(new Muscle(verlet.MassPoints, 7 * 9 + 7, 8 * 9 + 7, "DL07"));
            addMuscle(new Muscle(verlet.MassPoints, 9 * 9 + 7, 8 * 9 + 7, "DL07"));
            addMuscle(new Muscle(verlet.MassPoints, 9 * 9 + 7, 10 * 9 + 7, "DL09"));
            addMuscle(new Muscle(verlet.MassPoints, 11 * 9 + 7, 10 * 9 + 7, "DL09"));
            addMuscle(new Muscle(verlet.MassPoints, 11 * 9 + 7, 12 * 9 + 7, "DL11"));
            addMuscle(new Muscle(verlet.MassPoints, 13 * 9 + 7, 12 * 9 + 7, "DL11"));
            addMuscle(new Muscle(verlet.MassPoints, 13 * 9 + 7, 14 * 9 + 7, "DL13"));
            addMuscle(new Muscle(verlet.MassPoints, 15 * 9 + 7, 14 * 9 + 7, "DL13"));
            addMuscle(new Muscle(verlet.MassPoints, 15 * 9 + 7, 16 * 9 + 7, "DL15"));
            addMuscle(new Muscle(verlet.MassPoints, 17 * 9 + 7, 16 * 9 + 7, "DL15"));
            addMuscle(new Muscle(verlet.MassPoints, 17 * 9 + 7, 18 * 9 + 7, "DL17"));
            addMuscle(new Muscle(verlet.MassPoints, 19 * 9 + 7, 18 * 9 + 7, "DL17"));
            addMuscle(new Muscle(verlet.MassPoints, 19 * 9 + 7, 20 * 9 + 7, "DL19"));
            addMuscle(new Muscle(verlet.MassPoints, 21 * 9 + 7, 20 * 9 + 7, "DL19"));
            addMuscle(new Muscle(verlet.MassPoints, 21 * 9 + 7, 22 * 9 + 7, "DL21"));
            addMuscle(new Muscle(verlet.MassPoints, 23 * 9 + 7, 22 * 9 + 7, "DL21"));
            addMuscle(new Muscle(verlet.MassPoints, 23 * 9 + 7, 24 * 9 + 7, "DL23"));
            addMuscle(new Muscle(verlet.MassPoints, 25 * 9 + 7, 24 * 9 + 7, "DL23"));
          
            addMuscle(new Muscle(verlet.MassPoints, 1 * 9 + 8, 2 * 9 + 8, "VL01"));
            addMuscle(new Muscle(verlet.MassPoints, 3 * 9 + 8, 2 * 9 + 8, "VL01"));
            addMuscle(new Muscle(verlet.MassPoints, 3 * 9 + 8, 4 * 9 + 8, "VL03"));
            addMuscle(new Muscle(verlet.MassPoints, 5 * 9 + 8, 4 * 9 + 8, "VL03"));
            addMuscle(new Muscle(verlet.MassPoints, 5 * 9 + 8, 6 * 9 + 8, "VL05"));
            addMuscle(new Muscle(verlet.MassPoints, 7 * 9 + 8, 6 * 9 + 8, "VL05"));
            addMuscle(new Muscle(verlet.MassPoints, 7 * 9 + 8, 8 * 9 + 8, "VL07"));
            addMuscle(new Muscle(verlet.MassPoints, 9 * 9 + 8, 8 * 9 + 8, "VL07"));
            addMuscle(new Muscle(verlet.MassPoints, 9 * 9 + 8, 10 * 9 + 8, "VL09"));
            addMuscle(new Muscle(verlet.MassPoints, 11 * 9 + 8, 10 * 9 + 8, "VL09"));
            addMuscle(new Muscle(verlet.MassPoints, 11 * 9 + 8, 12 * 9 + 8, "VL11"));
            addMuscle(new Muscle(verlet.MassPoints, 13 * 9 + 8, 12 * 9 + 8, "VL11"));
            addMuscle(new Muscle(verlet.MassPoints, 13 * 9 + 8, 14 * 9 + 8, "VL13"));
            addMuscle(new Muscle(verlet.MassPoints, 15 * 9 + 8, 14 * 9 + 8, "VL13"));
            addMuscle(new Muscle(verlet.MassPoints, 15 * 9 + 8, 16 * 9 + 8, "VL15"));
            addMuscle(new Muscle(verlet.MassPoints, 17 * 9 + 8, 16 * 9 + 8, "VL15"));
            addMuscle(new Muscle(verlet.MassPoints, 17 * 9 + 8, 18 * 9 + 8, "VL17"));
            addMuscle(new Muscle(verlet.MassPoints, 19 * 9 + 8, 18 * 9 + 8, "VL17"));
            addMuscle(new Muscle(verlet.MassPoints, 19 * 9 + 8, 20 * 9 + 8, "VL19"));
            addMuscle(new Muscle(verlet.MassPoints, 21 * 9 + 8, 20 * 9 + 8, "VL19"));
            addMuscle(new Muscle(verlet.MassPoints, 21 * 9 + 8, 22 * 9 + 8, "VL21"));
            addMuscle(new Muscle(verlet.MassPoints, 23 * 9 + 8, 22 * 9 + 8, "VL21"));
            #endregion

            #region Tail
//            addMPoint(new MassPoint(masspointHolder, 0.05f, vshift + new Vector3(-1 * dx - i * dx, 0, 0)));
//
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 0], verlet.MassPoints[i * 9 + 0]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 1], verlet.MassPoints[i * 9 + 0]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 2], verlet.MassPoints[i * 9 + 0]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 3], verlet.MassPoints[i * 9 + 0]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 4], verlet.MassPoints[i * 9 + 0]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 5], verlet.MassPoints[i * 9 + 0]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 6], verlet.MassPoints[i * 9 + 0]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 7], verlet.MassPoints[i * 9 + 0]));
//            addSpring(new Spring(springHolder, Physics.AUTODETECT, Physics.DefaultStiffnessScaler, Physics.DefaultFrictionScaler, verlet.MassPoints[(i - 1) * 9 + 8], verlet.MassPoints[i * 9 + 0]));
            #endregion

            using (var reader = new StreamReader(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(neurons))))
            {
                while (!reader.EndOfStream)
                {
                    int tresh = 1;

                    var line = reader.ReadLine();
                    var tokens = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                    var name = tokens[0];
                    var x = float.Parse(tokens[1]);
                    var y = float.Parse(tokens[2]);
                    var z = float.Parse(tokens[3]);
                    var clrIndex = int.Parse(tokens[4]);

                    // Sy: Intentionally flip around y and z - as I think the original proto was in GL coords.
                    var temp = z;
                    z = y;
                    y = temp;

                    // Moved this from elsewhere.
                    y *= 0.5f * length;

                    var type = 'm';
                    if (name[name.Length - 1] == 'L') { type = 'l'; }
                    if (name[name.Length - 1] == 'R') { type = 'r'; }

                    addNeuron(name, vshift + new Vector3(dx * (-1.5f - x * size), y, -dl / 2.0f + ((0.045f + z / 25.0f) / 0.095f * length)), (float)tresh, type, clrIndex, line);
                }
            }

            using (var reader = new StreamReader(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(connections))))
            {
                reader.ReadLine(); // For some resaon they ignore the first line. Headers?

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var tokens = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                    var name = tokens[0];
                    var name2 = tokens[1];
                    var ctype = tokens[2];
                    var value = 1.0f;
                    if (!float.TryParse(tokens[3], out value))
                    {
                        Debug.Break();
                    }

                    if (ctype.StartsWith("EJ") || ctype[0] == 'S')
                    {
                        var n1 = Array.Find(neuron, n => n.name == name);
                        if (n1 != null)
                        {
                            var n2 = Array.Find(neuron, n => n.name == name2);
                            if (n2 != null)
                            {
                                n1.AddAxon(n2, value);
                            }
                        }
                    }
                }
            }

            using (var reader = new StreamReader(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(muscles))))
            {
                while(!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Trim();
                    var tokens = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                    var name = tokens[0];
                    var name2 = tokens[1];
                    var weight = 0.0f;
                    if (!float.TryParse(tokens[2], out weight))
                    {
                        Debug.Break();
                    }

                    // WFT AGAIN!?
                    weight = 3.0f;

                    addNeuroMuscleAxosByName(name, name2, weight);
                }
            }

            //neuronArray = neuron.ToArray();
            //muscleArray = muscle.ToArray();
            //springArray = spring.ToArray();
            //mPointArray = mPoint.ToArray();
            //tableArray = table.ConvertAll(te => te.ToArray()).ToArray();
        }

        private void addNeuron(string name, Vector3 pos, float threshold, char type, int clrIndex, string description)
        {
            int i;

            for(i = 0; i < nextTable; i++)
            {
                // Sy: This is supposed to be looking for the two sets of mass points either side of the neuron, so we can calculate a ratio position.
                if ((pos.x - verlet.MassPoints[9 * i + 8].pos.x) * (pos.x - verlet.MassPoints[9 * i + 17].pos.x) <= 0)
                {
                    break;
                }
            }

            // Sy: Think this converts the Y coord into a flatter, worm like size.
            //pos.y *= 0.5f * length;

            var _x1 = verlet.MassPoints[9 * i + 8].pos.x;
            var _x2 = verlet.MassPoints[9 * i + 17].pos.x;
            var _y1 = verlet.MassPoints[9 * i + 1].pos.y;
            var _y2 = verlet.MassPoints[9 * i + 6].pos.y;
            var _z1 = verlet.MassPoints[9 * i + 3].pos.z;
            var _z2 = verlet.MassPoints[9 * i + 8].pos.z;

            var ratioX = (pos.x - _x1) / Mathf.Abs(_x2 - _x1);
            var ratioY = (pos.y - _y1) / Mathf.Abs(_y2 - _y1);
            var ratioZ = (_z1 - pos.z) / Mathf.Abs(_z2 - _z1);

            var x = Mathf.Lerp(_x1, _x2, ratioX);
            var y = Mathf.Lerp(_y1, _y2, ratioY);
            var z = Mathf.Lerp(_z1, _z2, ratioZ);
            
            var n= new Neuron(name, pos, threshold, ratioX, ratioY, ratioZ, type, clrIndex, description);
            neuron[nextNeuron++] = n;
            table[i][nextTableFor[i]++] = n;
        }

        private void addNeuroMuscleAxosByName(string name, string muscleName, float weight)
        {
            var namedNeuron = Array.Find(neuron, n => n.name == name);
            if (namedNeuron != null)
            {
                for(int m = 0; m < nextMuscle; m++)
                {
                    var namedMuscle = muscle[m];
                    if (namedMuscle.synapse.name == muscleName)
                    {
                        namedNeuron.AddAxon(namedMuscle.synapse, weight);
                    }
                }
            }
        }

        private void addMuscle(Muscle m)
        {
            muscle[nextMuscle++] = m;
        }

        public void Update(float dt)
        {
            const bool mode2 = false;
            const bool mode3 = false;
            const bool mode4 = false;

            if (Input.GetKey(KeyCode.Alpha1))
            {
                var neuron = Array.Find(this.neuron, n => n.name == "DB02");
                if (neuron != null)
                {
                    Debug.Log("Sending to Neuron " + neuron.name);
                    neuron.GetSignal(1.0f);
                }
            }

            if (Input.GetKey(KeyCode.Alpha2))
            {
                var neuron = Array.Find(this.neuron, n => n.name == "RIML");
                if (neuron != null)
                {
                    Debug.Log("Sending to Neuron " + neuron.name);
                    neuron.GetSignal(1.0f);
                }
            }

            if (Input.GetKey(KeyCode.Alpha3))
            {
                var neuron = Array.Find(this.neuron, n => n.name == "RIMR");
                if (neuron != null)
                {
                    Debug.Log("Sending to Neuron " + neuron.name);
                    neuron.GetSignal(1.0f);
                }
            }

            for (int n = 0; n < nextNeuron; n++) { neuron[n].CheckActivity(); }
            
            for (int m = 0; m < nextMuscle; m++) { muscle[m].synapse.CheckActivity(); }
            
            for (int n = 0; n < nextNeuron; n++) { neuron[n].Update(); }
            
            verlet.Update(Time.deltaTime);
            
            for (int m = 0; m < nextMuscle; m++) { muscle[m].Update(); }
            
            //applyFriction();

            if (mode3)
            {
                // Urgle. Some sinusodal input bullshit here.
            }

            if (mode4)
            {
                // More here. Sake.
            }

            time += dt;
            
            neuronPosCorrection();
        }

        public void FixedUpdate()
        {
            verlet.FixedUpdate(Time.fixedDeltaTime, sim =>
            {
                for (int m = 0; m < nextMuscle; m++) { muscle[m].FixedUpdate(); }
            });
        }

        private void neuronPosCorrection()
        {
            if (!UniversalConstantsBehaviour.Instance.UpdateNeuronPositions)
                return;

            for(int i = 0; i < nextTable; i++)
            {
                if (nextTableFor[i] == 0) continue;

                var _x1 = verlet.MassPoints[9 * i + 8].pos.x;
                var _x2 = verlet.MassPoints[9 * i + 17].pos.x;
                var _y1 = verlet.MassPoints[9 * i + 1].pos.y;
                var _y2 = verlet.MassPoints[9 * i + 6].pos.y;
                var _z1 = verlet.MassPoints[9 * i + 3].pos.z;
                var _z2 = verlet.MassPoints[9 * i + 8].pos.z;

                Neuron[] tableI = table[i];

                for (int j = 0; j < nextTableFor[i]; j++)
                {
                    Neuron tableIJ = tableI[j];

                    var ratioX = tableIJ.ratioX;
                    var ratioY = tableIJ.ratioY;
                    var ratioZ = tableIJ.ratioZ;

                    /*
                    var p1 = (verlet.MassPoints[9 * i + 8].pos + verlet.MassPoints[9 + i + 1].pos) / 2.0f;
                    var p2 = (verlet.MassPoints[9 * i + 2].pos + verlet.MassPoints[9 + i + 3].pos) / 2.0f;
                    var p3 = (verlet.MassPoints[9 * i + 4].pos + verlet.MassPoints[9 + i + 5].pos) / 2.0f;
                    var p4 = (verlet.MassPoints[9 * i + 6].pos + verlet.MassPoints[9 + i + 7].pos) / 2.0f;

                    var p5 = (verlet.MassPoints[9 * i + 17].pos + verlet.MassPoints[9 + i + 10].pos) / 2.0f;
                    var p6 = (verlet.MassPoints[9 * i + 11].pos + verlet.MassPoints[9 + i + 12].pos) / 2.0f;
                    var p7 = (verlet.MassPoints[9 * i + 13].pos + verlet.MassPoints[9 + i + 14].pos) / 2.0f;
                    var p8 = (verlet.MassPoints[9 * i + 15].pos + verlet.MassPoints[9 + i + 16].pos) / 2.0f;

                    var pp1 = p1 * (1.0f - ratioX) + p5 * ratioX;
                    var pp2 = p2 * (1.0f - ratioX) + p6 * ratioX;
                    var pp3 = p3 * (1.0f - ratioX) + p7 * ratioX;
                    var pp4 = p4 * (1.0f - ratioX) + p8 * ratioX;

                    var q1 = pp1 * (1.0f - ratioZ) + pp4 * ratioZ;
                    var q2 = pp2 * (1.0f - ratioZ) + pp3 * ratioZ;

                    table[i][j].pos = q1 * ratioY + q2 * (1.0f - ratioY);*/
                    
                    var x = _x1 + (ratioX * (_x1 - _x2));
                    var y = _y1 + (ratioY * (_y1 - _y2));
                    var z = _z1 + (ratioZ * (_z1 - _z2));

                    tableIJ.pos = new Vector3(x, y, z);
                }
            }
        }

        private void applyFriction()
        {
            for(int i = 0; i < verlet.MassPoints.Length; i++)
            {
                if (verlet.MassPoints[i].pos.z <= 0)
                {
                    var vel = verlet.MassPoints[i].vel;
                    vel.z = 0;

                    var dp = Vector3.zero;
                    if (i <= 8)
                    {
                        dp = verlet.MassPoints[i].pos - verlet.MassPoints[i + 9].pos;
                    }
                    else if (i >= verlet.MassPoints.Length - 9)
                    {
                        dp = verlet.MassPoints[i - 9].pos - verlet.MassPoints[i].pos;
                    }
                    else
                    {
                        dp = (verlet.MassPoints[i - 9].pos - verlet.MassPoints[i + 9].pos) / 2.0f;
                    }
                    dp.z = 0.0f;

                    dp.Normalize();
                    var tangent = dp * Vector3.Dot(vel, dp);
                    var normal = vel - tangent;
                    var force = -normal - tangent / 32.0f;
                    verlet.MassPoints[i].accel += force;
                }
            }
        }

        public void Draw()
        {
            if (UniversalConstantsBehaviour.Instance.ShowNeurons)
            {
                for (int n = 0; n < nextNeuron; n++) { neuron[n].Draw(neuronHolder); }
            }

            if (UniversalConstantsBehaviour.Instance.ShowMuscles)
            {
                for (int m = 0; m < nextMuscle; m++) { muscle[m].Draw(musclesHolder); }
            }

//            if (UniversalConstantsBehaviour.Instance.ShowMassPoints)
//            {
//                for (int mp = 0; mp < nextMassPoint; mp++) { verlet.MassPoints[mp].Draw(masspointHolder); }
//            }
//
//            if (UniversalConstantsBehaviour.Instance.ShowSprings)
//            {
//                for (int s = 0; s < nextSpring; s++) { spring[s].Draw(springHolder); }
//            }
        }
    }
}