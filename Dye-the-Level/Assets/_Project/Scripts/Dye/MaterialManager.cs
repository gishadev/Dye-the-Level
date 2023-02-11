using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Gisha.DyeTheLevel.Core;


namespace Gisha.DyeTheLevel.Dye
{
    public class MaterialManager : MonoBehaviour
    {
        #region Singleton
        private static MaterialManager Instance { set; get; }
        #endregion
        
        [Header("Preview")]
        [SerializeField] private MeshRenderer previewMR;

        [Header("Parents")]
        [SerializeField] private Transform dyeTargetsParent;
        [SerializeField] private Transform samplesRTParent;
        [SerializeField] private Transform samplesUIParent;

        public static DyeSample DyeSample { private set; get; }
        public static Material DiscolorMaterial { private set; get; }
        public static List<DyeSample> Samples { private set; get; }

        List<DyeSample> _samples = new List<DyeSample>();

        private GameData _gameData => ResourceLoader.GetGameData();
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            CreateDyeSamples();

            DyeSample = _samples[0];
            UpdatePreview(DyeSample);

            Samples = _samples;
            DiscolorMaterial =  _gameData.DiscolorMaterial;

            DiscolorAll();
        }

        public static void ChangeDyeSample(int index)
        {
            DyeSample = Samples[index];
            Instance.UpdatePreview(DyeSample);

            Debug.Log("Dye Sample was changed!");
        }

        /// <summary>
        /// Automatically create dye samples.
        /// </summary>
        private void CreateDyeSamples()
        {
            // Initializing, distincting materials for dyes.
            _samples = new List<DyeSample>();
            var meshRenderers = dyeTargetsParent.GetComponentsInChildren<MeshRenderer>();
            var distinctMaterials = DistinctMaterials(meshRenderers);

            // Creating dye samples.
            for (int i = 0; i < distinctMaterials.Length; i++)
            {
                Material dyeMaterial = distinctMaterials[i];
                int dyeCount = 0;
                for (int j = 0; j < meshRenderers.Length; j++)
                {
                    if (dyeMaterial == meshRenderers[j].sharedMaterials[0])
                        dyeCount++;
                }

                GameObject renderTextureObject = CreateRenderTextureObject(dyeMaterial, i, out RenderTexture renderTexture);
                DyeSampleUI sampleUI = CreateUISample(renderTexture, i, dyeCount);

                DyeSample sample = new DyeSample(sampleUI, dyeMaterial, dyeCount, renderTextureObject, renderTexture);

                _samples.Add(sample);
            }
        }

        private GameObject CreateRenderTextureObject(Material material, int index, out RenderTexture rt)
        {
            rt = new RenderTexture(1024, 1024, 0);

            var position = Vector3.up * -10f * index;
            var rtObject = Instantiate(_gameData.SampleRTPrefab, position, Quaternion.identity, samplesRTParent);

            rtObject.GetComponentInChildren<Camera>().targetTexture = rt;
            rtObject.GetComponentInChildren<MeshRenderer>().material = material;

            return rtObject;
        }

        private DyeSampleUI CreateUISample(RenderTexture rt, int dyeIndex, int dyeCount)
        {
            var sampleUI = Instantiate(_gameData.SampleUIPrefab, Vector3.zero, Quaternion.identity, samplesUIParent).GetComponent<DyeSampleUI>();
            sampleUI.InitializeSample(dyeIndex, dyeCount);

            var rawImage = sampleUI.GetComponent<RawImage>();
            rawImage.texture = rt;

            return sampleUI;
        }

        private void DiscolorAll()
        {
            MeshRenderer[] meshRenderers = dyeTargetsParent.GetComponentsInChildren<MeshRenderer>();

            for (int i = 0; i < meshRenderers.Length; i++)
                meshRenderers[i].material = _gameData.DiscolorMaterial;
        }

        private Material[] DistinctMaterials(MeshRenderer[] meshRenderers)
        {
            Material[] result = meshRenderers
                .Select(x => x.sharedMaterials[0])
                .Distinct()
                .ToArray();

            return result;
        }

        private void UpdatePreview(DyeSample sample)
        {
            previewMR.material = sample.DyeMaterial;
        }
    }
}
