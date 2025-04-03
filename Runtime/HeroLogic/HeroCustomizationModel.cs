using System;
using UnityEngine;

namespace _project.Scripts.HeroLogic
{
    [CreateAssetMenu(menuName = "HeroCustomizationModel", fileName = "HeroCustomizationModel")]
    public class HeroCustomizationModel : ScriptableObject
    {
        [SerializeField] public float SkinColor;
        [SerializeField] public float SkinColorEmission;
        [SerializeField] public string Nickname;
        [SerializeField] private Material _heroMaterial;

        public event Action<string> OnNicknameChanged;
        public event Action<float> OnSkinColorChanged;
        public event Action<float> OnSkinColorEmissionChanged;

        public void SetSkinColor(float skinColor)
        {
            SkinColor = skinColor;
            OnSkinColorChanged?.Invoke(skinColor);
        }

        public void SetSkinColorEmission(float skinColorEmission)
        {
            SkinColorEmission = skinColorEmission;
            OnSkinColorEmissionChanged?.Invoke(skinColorEmission);
        }

        public void SetNickname(string nickname)
        {
            Nickname = nickname;
            OnNicknameChanged?.Invoke(nickname);
        }

        public void SetMaterial(Material heroMaterial) => _heroMaterial = heroMaterial;
    }
}