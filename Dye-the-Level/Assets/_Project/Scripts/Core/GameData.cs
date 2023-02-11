using UnityEngine;

namespace Gisha.DyeTheLevel.Core
{
    [CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 0)]
    public class GameData : ScriptableObject
    {
        [Header("Prefabs")] 
        [SerializeField] private GameObject sampleRTPrefab;
        [SerializeField] private GameObject sampleUIPrefab;
        
        [Header("Materials")]
        [SerializeField] private Material discolorMaterial;

        public GameObject SampleUIPrefab => sampleUIPrefab;
        public GameObject SampleRTPrefab => sampleRTPrefab;
        public Material DiscolorMaterial => discolorMaterial;
    }
}