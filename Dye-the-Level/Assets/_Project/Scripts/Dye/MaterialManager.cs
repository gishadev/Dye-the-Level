using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gisha.DyeTheLevel.Dye
{
    public class MaterialManager : MonoBehaviour
    {
        public static Material DyeMaterial { private set; get; }

        [Header("Materials")]
        [SerializeField] private Material discolorMaterial;
        [Space]
        [SerializeField] [Tooltip("Are dye samples creating automatically?")] private bool autosampling = false;
        [SerializeField] private List<Material> samples = new List<Material>();

        [Header("Parents")]
        [SerializeField] private Transform dyeTargetsParent;
        [SerializeField] private Transform samplesRTParent;
        [SerializeField] private Transform samplesUIParent;

        [Header("Prefabs")]
        [SerializeField] private GameObject sampleRTPrefab;
        [SerializeField] private GameObject sampleUIPrefab;

        GameObject[] _renderTextureObjects;
        RenderTexture[] _renderTextures;

        private void Start()
        {
            if (autosampling)
                Autosample();

            _renderTextures = new RenderTexture[samples.Count];
            _renderTextureObjects = new GameObject[samples.Count];

            for (int i = 0; i < _renderTextures.Length; i++)
            {
                if (samples[i] != null)
                {
                    CreateRenderTextureObject(samples[i], i);
                    CreateUISample(samples[i], i);
                }
            }

            Discolor();
        }

        public static void ChangeDyeMaterial(Material newMaterial)
        {
            DyeMaterial = newMaterial;
            Debug.Log("Dye Material was changed!");
        }

        private void CreateRenderTextureObject(Material material, int index)
        {
            RenderTexture rt = new RenderTexture(1024, 1024, 0);

            var position = Vector3.up * -10f * index;
            var rtObject = Instantiate(sampleRTPrefab, position, Quaternion.identity, samplesRTParent);

            rtObject.GetComponentInChildren<Camera>().targetTexture = rt;
            rtObject.GetComponentInChildren<MeshRenderer>().material = material;

            _renderTextures[index] = rt;
            _renderTextureObjects[index] = rtObject;
        }

        private void CreateUISample(Material material, int index)
        {
            var sample = Instantiate(sampleUIPrefab, Vector3.zero, Quaternion.identity, samplesUIParent).GetComponent<DyeSample>();
            sample.SetSampleMaterial(material);

            var rawImage = sample.GetComponent<RawImage>();
            rawImage.texture = _renderTextures[index];
        }

        private void Discolor()
        {
            MeshRenderer[] meshRenderers = dyeTargetsParent.GetComponentsInChildren<MeshRenderer>();

            for (int i = 0; i < meshRenderers.Length; i++)
                meshRenderers[i].material = discolorMaterial;
        }

        /// <summary>
        /// Automatically create dye samples.
        /// </summary>
        private void Autosample()
        {
            samples = new List<Material>();
            MeshRenderer[] meshRenderers = dyeTargetsParent.GetComponentsInChildren<MeshRenderer>();

            for (int i = 0; i < meshRenderers.Length; i++)
                samples.Add(meshRenderers[i].sharedMaterials[0]);
        }
    }
}
