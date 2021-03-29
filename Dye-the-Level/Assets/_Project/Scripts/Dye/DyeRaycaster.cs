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
                var newMaterial = MaterialManager.DyeMaterial;

                if (Input.GetMouseButtonDown(0))
                    Paint(meshRenderer, newMaterial);
                else if (Input.GetMouseButtonDown(1))
                    Paint(meshRenderer, MaterialManager.DiscolorMaterial);
            }
        }

        // Checking for suitable dye target.
        private bool Raycast(out Collider collider)
        {
            collider = null;

            if (MaterialManager.DyeMaterial == null)
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

        // Changing of raycasted meshrenderer material.
        private void Paint(MeshRenderer meshRenderer, Material newMaterial)
        {
            meshRenderer.material = newMaterial;
        }
    }
}
