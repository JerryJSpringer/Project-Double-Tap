using GameDevIdiotsProject1.Graphics;
using System.Collections.Generic;

namespace GameDevIdiotsProject1.DefaultEcs.Components
{
    public struct Animate 
    {
        // HashMap to store all animations a sprite
        public Dictionary<string, Animation> AnimationList;
        public string currentAnimation;
    }
}
