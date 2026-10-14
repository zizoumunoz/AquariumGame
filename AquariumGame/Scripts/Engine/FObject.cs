using System;
using System.Collections.Generic;
using System.Text;
using AquariumGame.Scripts.Engine.Components;
using SFML.System;

namespace AquariumGame.Scripts.Engine
{
    /// <summary>
    /// FObjects are the base objects for every other class in the Fih engine.
    /// It has Transform, and can have more components.
    /// </summary>
    internal class FObject
    {
        /// <summary>
        /// The parent of this FObject
        /// If this object is the root object, the parent will be itself
        /// </summary>
        public FObject parent;
        /// <summary>
        /// The Transform component of this FObjects
        /// </summary>
        public Transform transform = new();
        /// <summary>
        /// The list of components of this FObjects
        /// </summary>
        private List<Component> components = [];

        /// <summary>
        /// Returns the component of type T from this object
        /// If there isnt, returns null
        /// </summary>
        /// <typeparam name="T">Component Type</typeparam>
        /// <returns>Component or null</returns>
        public T? GetComponent<T>() where T : Component
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i] is T)
                {
                    return (T)components[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Checks if this object already has a component of type T and returns it if it has
        /// Add parameter to the list of components and returns it if it doesn't have
        /// Copies the component if its from another FObject
        /// </summary>
        /// <typeparam name="T">Component Type</typeparam>
        /// <param name="component">New Component Instance</param>
        /// <returns>Component</returns>
        public T AddComponent<T>(T component) where T : Component
        {
            if (component.parent != null)
            {
                if (Activator.CreateInstance(component.GetType(), component) is T newComponent)
                {
                    component = newComponent;
                }
                else
                {
                    throw new Exception("Component couldn't be copied");
                }
            }
            T? eComponent = GetComponent<T>();
            if (eComponent == null)
            {
                components.Add(component);
            }
            else
            {
                component = eComponent;
            }
            component.parent = this;
            return component;
        }
    }
}
