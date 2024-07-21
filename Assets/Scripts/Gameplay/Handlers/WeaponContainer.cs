using Player;
using Scriptables.Weapons;

namespace Gameplay.Handlers
{
    public class WeaponContainer : BaseContainer<WeaponConfig>
    {
        public override void OnInit()
        {
            SpriteRenderer.sprite = Item.Sprite;
            SpriteRenderer.color = Item.Color;
        }

        public override void Collect(PlayerStats target)
        {
            target.WeaponSelector.Set(Item);
            Destroy();
        }
    }
}