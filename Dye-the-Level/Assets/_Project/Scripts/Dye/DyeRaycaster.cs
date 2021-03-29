using UnityEngine;

namespace Gisha.DyeTheLevel.Dye
{
    public class DyeRaycaster : MonoBehaviour
    {
        [SerializeField] private LayerMask whatIsPaintable;
        [SerializeField] private float raycastDistance = 5000f;

        private void Update()
        {
            if (Raycast(out Collider collider))
            {
                var meshRenderer = collider.GetComponent<MeshRenderer>();
                var newSample = MaterialManager.DyeSample;

                if (Input.GetMouseButtonDown(0))
                {
                    ContainsDyeSample(meshRenderer, out DyeSample oldSample);
                    Color(meshRenderer, newSample, oldSample);
                }

                else if (Input.GetMouseButtonDown(1))
                {
                    ContainsDyeSample(meshRenderer, out DyeSample oldSample);
                    Discolor(meshRenderer, oldSample);
                }
            }
        }

        // Checking for suitable dye target.
        private bool Raycast(out Collider collider)
        {
            collider = null;

            if (MaterialManager.DyeSample == null)
                return false;

            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo, raycastDistance, whatIsPaintable))
            {
                collider = hitInfo.collider;
                return true;
            }

            return false;
        }

        public bool ContainsDyeSample(MeshRenderer mr, out DyeSample ds)
        {
            ds = null;

            foreach (var dyeSample in MaterialManager.Samples)
            {
                if (dyeSample.DyeMaterial == mr.sharedMaterials[0])
                {
                    ds = dyeSample;
                    return true;
                }
            }

            return false;
        }

        // Changing of raycasted meshrenderer material.
        private void Color(MeshRenderer meshRenderer, DyeSample newSample, DyeSample oldSample)
        {
            if (oldSample != null)
            {
                oldSample.DyeCount++;
                oldSample.SampleUI.UpdateCount(oldSample.DyeCount);
            }

            meshRenderer.material = newSample.DyeMaterial;
            newSample.DyeCount--;
            newSample.SampleUI.UpdateCount(newSample.DyeCount);
        }

        private void Discolor(MeshRenderer meshRenderer, DyeSample oldSample)
        {
            if (oldSample != null)
            {
                oldSample.DyeCount++;
                oldSample.SampleUI.UpdateCount(oldSample.DyeCount);
            }

            meshRenderer.material = MaterialManager.DiscolorMaterial;
        }
    }
}
