using System;

namespace Utilities.Enums
{
    [Flags]
    public enum EntityType
    {
        Player = 1,
        Enemy = 2,
        
        Nothing = 0,
        Everything = Player | Enemy
    }
}