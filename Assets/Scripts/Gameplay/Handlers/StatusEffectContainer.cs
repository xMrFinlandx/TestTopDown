using Player;
using Scriptables.StatusEffects;

namespace Gameplay.Handlers
{
    public class StatusEffectContainer : BaseContainer<StatusEffectConfig>
    {
        public override void OnInit()
        {
            SpriteRenderer.sprite = Item.Sprite;
            SpriteRenderer.color = Item.Color;
        }

        public override void Collect(PlayerStats target)
        {
            target.ApplyStatusEffect(Item);
            Destroy(gameObject);
        }
    }
}