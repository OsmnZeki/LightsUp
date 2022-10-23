using System;
using System.Collections.Generic;
using Debugs;
using Shapes;
using UnityEngine;

namespace LightSource
{
    public struct LightParticle
    {
        public Vector3 position;
        public Vector3 moveDirection;
        public float speed;
    }


    public class LightSourceMono : MonoBehaviour
    {
        Sphere sphere;
        public float sourceRadius;
        public int circleCount;
        public int edgeCount;

        public GameObject particlePrefab;
        public ParticleSystem particleSystem;
        public List<LightParticle> lightParticles;
        public List<GameObject> particleGameobjs;

        const int ParticleCount = 100000;
        const float ParticleSpeed = 5;
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ParticleCount];

        float totalTime = 0;

        // Start is called before the first frame update
        void Start()
        {
            lightParticles = new List<LightParticle>();
            particleGameobjs = new List<GameObject>();

            sphere = Sphere.CreateSphere(sourceRadius, edgeCount, circleCount);
            var particleSystemMain = particleSystem.main;
            particleSystemMain.maxParticles = ParticleCount;

            Physics.autoSimulation = false;
            Physics.autoSyncTransforms = false;
        }


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                for (int i = 0; i < sphere.localPos.Count; i++)
                {
                    var spawnPos = transform.TransformPoint(sphere.localPos[i]);

                    var particle = Instantiate(particlePrefab, spawnPos, Quaternion.identity);
                    var particleRigidbody = particle.GetComponent<Rigidbody>();
                    particleRigidbody.velocity = sphere.normal[i] * ParticleSpeed;

                    particleGameobjs.Add(particle);
                }
            }

            totalTime += Time.deltaTime;
            Physics.SyncTransforms();
            while (totalTime >= Time.fixedDeltaTime)
            {
                Physics.Simulate(Time.fixedDeltaTime);
                totalTime -= Time.fixedDeltaTime;
            }


            if (particleGameobjs.Count < ParticleCount)
            {
                particleSystem.Clear();
                particleSystem.Emit(particleGameobjs.Count);
                particleSystem.GetParticles(particles, particleGameobjs.Count);


                for (int i = 0; i < particleGameobjs.Count; i++)
                {
                    ref var particle = ref particles[i];
                    particle.position = particleGameobjs[i].transform.position;
                }

                particleSystem.SetParticles(particles);
            }


            sphere.DrawPos();
        }

        void LateUpdate()
        {
            // if (particleGameobjs.Count < ParticleCount)
            // {
            //     particleSystem.Emit(particleGameobjs.Count);
            //     particleSystem.GetParticles(particles, particleGameobjs.Count);
            //
            //
            //     for (int i = 0; i < particleGameobjs.Count; i++)
            //     {
            //         ref var particle = ref particles[i];
            //         particle.position = particleGameobjs[i].transform.position;
            //     }
            //     particleSystem.SetParticles(particles);
            // }
        }

        void DrawParticle()
        {
            for (int i = 0; i < lightParticles.Count; i++)
            {
                MyDebug.DrawSphere(lightParticles[i].position, .1f);
            }
        }
    }
}