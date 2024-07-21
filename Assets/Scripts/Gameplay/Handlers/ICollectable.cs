using Player;

namespace Gameplay.Handlers
{
    public interface ICollectable
    {
        public void Collect(PlayerStats target);
    }
}