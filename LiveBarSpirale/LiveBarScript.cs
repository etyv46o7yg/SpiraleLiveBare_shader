using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LiveBar
    {

    public class LiveBarScript : MonoBehaviour
        {
        public int sizeImage = 400;

        private const int sizeShaderTread = 20;

        [Range(0.0f, 100.0f)]
        public float procent;

        public ComputeShader shader;
        public Color colorA, colorB;
        public RawImage screen;
        // Start is called before the first frame update
        RenderTexture texA;
        string [] noms;
        ChaderSysteme chader;
        void Start()
            {
            noms = new string [] { "CSMain", "Draw" };
            chader = new ChaderSysteme(shader, noms);

            texA = ChaderSysteme.GetRT(sizeImage, sizeImage);
            chader.AddDict("Main", noms [0]);
            chader.AddDict("Draw", noms [1]);
            chader.SetNumeroThredes(sizeImage / sizeShaderTread, sizeImage / sizeShaderTread, 1);
            screen.texture = texA;
            }

        // Update is called once per frame
        void Update()
            {
            chader.SetPublicTexture(texA, "Result");
            chader.shader.SetVector("Color", colorA);
            chader.shader.SetVector("Color2", colorB);
            chader.SetFloat("epaisseur", 10.0f);
            chader.SetFloat("radius", 150.0f);
            chader.SetFloat("procent", procent);
            chader.Dispatch("Main");
            chader.Dispatch("Draw");
            }
        }
    }
