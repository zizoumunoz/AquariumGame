using System;
using System.Collections.Generic;
using System.Text;

namespace AquariumGame.Scripts.Engine.Components
{
    /// <summary>
    /// A component is a behaivior that a FObject can have
    /// </summary>
    internal abstract class Component
    {
        /// <summary>
        /// The parent FObject of this component
        /// </summary>
        public FObject? parent = null;

        /// <summary>
        /// Start is called the first frame this component becomes available
        /// </summary>
        public virtual void Start(){}

        /// <summary>
        /// Update is called every frame this component is available
        /// </summary>
        public virtual void Update(){}
    }
}
