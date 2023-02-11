using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Gisha.DyeTheLevel.Core;


namespace Gisha.DyeTheLevel.Dye
{
    public class DyeManager : MonoBehaviour
    {
        [Header("Preview")] [SerializeField] private MeshRenderer previewMR;

        [Header("Parents")] [SerializeField] private Transform dyeTargetsParent;
        [SerializeField] private Transform samplesRTParent;
        [SerializeField] private Transform samplesUIParent;

        public DyeSample DyeSample { private set; get; }
        public List<DyeSample> Samples { private set; get; }

        private List<DyeSample> _samples = new List<DyeSample>();
        private GameData _gameData;

        private void Awake()
        {
            _gameData = ResourceLoader.GetGameData();
        }

        private void Start()
        {
            CreateDyeSamplesFromWorldMeshRenderers();

            DyeSample = _samples[0];
            UpdatePreview(DyeSample);

            Samples = _samples;

            DiscolorAll();
        }

        private void OnEnable()
        {
            DyeSampleUI.DyeSampleUIInteracted += ChangeDyeSample;
        }

        private void OnDisable()
        {
            DyeSampleUI.DyeSampleUIInteracted -= ChangeDyeSample;
        }

        private void ChangeDyeSample(int index)
        {
            DyeSample = Samples[index];
            UpdatePreview(DyeSample);

            Debug.Log("Dye Sample was changed!");
        }

        /// <summary>
        /// Automatically create dye samples.
        /// </summary>
        private void CreateDyeSamplesFromWorldMeshRenderers()
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

                GameObject renderTextureObject =
                    CreateRenderTextureObject(dyeMaterial, i, out RenderTexture renderTexture);
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
            GameObject sampleUIObject =
                Instantiate(_gameData.SampleUIPrefab, Vector3.zero, Quaternion.identity, samplesUIParent);

            var sampleUI = sampleUIObject.GetComponent<DyeSampleUI>();
            sampleUI.InitializeSample(dyeIndex, dyeCount);

            var rawImage = sampleUIObject.GetComponent<RawImage>();
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