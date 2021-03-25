using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gisha.DyeTheLevel.Dye
{
    public class MaterialManager : MonoBehaviour
    {
        public static Material DyeMaterial { private set; get; }

        [Header("Parents")]
        [SerializeField] private Transform samplesRTParent;
        [SerializeField] private Transform samplesUIParent;

        [Header("Prefabs")]
        [SerializeField] private GameObject sampleRTPrefab;
        [SerializeField] private GameObject sampleUIPrefab;

        [Header("Samples List")]
        [SerializeField] private List<Material> samples = new List<Material>();

        GameObject[] _renderTextureObjects;
        RenderTexture[] _renderTextures;

        private void Start()
        {
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
    }
}
