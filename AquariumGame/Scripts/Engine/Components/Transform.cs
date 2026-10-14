using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquariumGame.Scripts.Engine.Components
{
    /// <summary>
    /// Transform is a component that holds the FObject's spacial traits, such as position, rotation, and scale
    /// </summary>
    internal class Transform : Component
    {
        /// <summary>
        /// The posision of this FObject relative to its parent's
        /// </summary>
        public Vector2f position;
        /// <summary>
        /// The rotation of this FObject relative to its parent's 
        /// </summary>
        public float rotation;
        /// <summary>
        /// The scale of this FObject relative to its parent's
        /// </summary>
        public Vector2f scale;
    }
}
