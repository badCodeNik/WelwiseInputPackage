using UnityEngine;

namespace HeroLogic
{
    public class SkinColorChanger : MonoBehaviour
    {
        [SerializeField] private Renderer[] _bodyRenderers;
        [SerializeField] private Renderer[] _armsRenderers;

        private void Awake()
        {
            _armsRenderers ??= GetComponentsInChildren<Renderer>();
        }

        public void ApplyMaterialToAllRenderers(Material bodyMaterial, Material armsMaterial)
        {
            foreach (var renderer in _armsRenderers)
            {
                Material newMaterial = Instantiate(armsMaterial);
                renderer.material = newMaterial;
                renderer.enabled = false;
            }

            foreach (var renderer in _bodyRenderers)
            {
                Material newMaterial = Instantiate(bodyMaterial);
                renderer.material = newMaterial;
            }
        }


        public void SetSkinColor(float color)
        {
            foreach (var renderer in _armsRenderers)
            {
                renderer.material.SetFloat("_Hue", color);
            }

            foreach (var renderer in _bodyRenderers)
            {
                renderer.material.SetFloat("_Hue", color);
            }
        }

        public void SetSkinColorEmission(float colorEmission)
        {
            foreach (var renderer in _armsRenderers)
            {
                renderer.material.SetFloat("_EmissionHue", colorEmission);
            }

            foreach (var renderer in _bodyRenderers)
            {
                renderer.material.SetFloat("_EmissionHue", colorEmission);
            }
        }

        public void SetSkinFade(float distance)
        {
            foreach (var renderer in _bodyRenderers)
            {
                renderer.material.SetFloat("_CurrentDistance", distance);
            }
        }


        public void SwitchBody(bool isFirstCamera)
        {
            foreach (var renderer in _bodyRenderers)
            {
                renderer.enabled = !isFirstCamera;
            }
        }

        public void SwitchArms(bool isFirstCamera)
        {
            foreach (var renderer in _armsRenderers)
            {
                renderer.enabled = isFirstCamera;
            }
        }
    }
}