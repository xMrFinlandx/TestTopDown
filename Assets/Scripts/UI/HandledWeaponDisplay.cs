using Managers.Queue;
using Player;
using Scriptables.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HandledWeaponDisplay : QueueElement
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _nameTextMesh;
        [SerializeField] private TextMeshProUGUI _descriptionTextMesh;

        private void WeaponChangedAction(WeaponConfig weaponConfig)
        {
            _image.sprite = weaponConfig.Sprite;
            _image.color = weaponConfig.Color;

            _nameTextMesh.text = weaponConfig.Name;
            _descriptionTextMesh.text = weaponConfig.Description;
        }

        private void OnDestroy()
        {
            WeaponSelector.WeaponChangedAction -= WeaponChangedAction;
        }

        public override void Enable()
        {
            WeaponSelector.WeaponChangedAction += WeaponChangedAction;
        }
    }
}