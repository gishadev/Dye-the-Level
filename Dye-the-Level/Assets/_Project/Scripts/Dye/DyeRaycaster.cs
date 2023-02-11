using Gisha.DyeTheLevel.Core;
using UnityEngine;

namespace Gisha.DyeTheLevel.Dye
{
    [RequireComponent(typeof(DyeManager))]
    public class DyeRaycaster : MonoBehaviour
    {
        [SerializeField] private LayerMask whatIsPaintable;
        [SerializeField] private float raycastDistance = 5000f;

        private DyeManager _dyeManager;
        private GameData _gameData;
        private DyeChanger _dyeChanger;

        private void Awake()
        {
            _dyeManager = GetComponent<DyeManager>();
            _gameData = ResourceLoader.GetGameData();
            _dyeChanger = new DyeChanger(_dyeManager, _gameData);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) CheckLMB();
            if (Input.GetMouseButtonDown(1)) CheckRMB();
        }


        private void CheckLMB()
        {
            if (RaycastAndReturnMR(out var mr))
            {
                _dyeChanger.ContainsDyeSample(mr, out var oldSample);
                _dyeChanger.Color(mr, _dyeManager.DyeSample, oldSample);
            }
        }

        private void CheckRMB()
        {
            if (RaycastAndReturnMR(out var mr))
            {
                _dyeChanger.ContainsDyeSample(mr, out var oldSample);
                _dyeChanger.Discolor(mr, oldSample);
            }
        }


        private bool RaycastAndReturnMR(out MeshRenderer meshRenderer)
        {
            meshRenderer = null;

            if (_dyeManager.DyeSample == null)
                return false;

            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo, raycastDistance, whatIsPaintable))
                return hitInfo.collider.TryGetComponent(out meshRenderer);

            return false;
        }
    }
}