using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;

namespace AquariumGame.Scripts.Engine
{
    /// <summary>
    /// A Class that holds, Activates, and Deactivates FObjects
    /// </summary>
    /// <param name="name">Scene name</param>
    internal class Scene(string name)
    {
        /// <summary>
        /// Scene's name
        /// </summary>
        public string name { get; private set; } = name;
        /// <summary>
        /// The list of FObjects on this scene
        /// </summary>
        public List<FObject> FObjectList = [];
        /// <summary>
        /// Activates all the FObjects on this scene
        /// </summary>
        public void Activate()
        {
            for (int i = 0; i < FObjectList.Count; i++)
            {
                ActivateFObject(FObjectList[i]);
            }
        }

        /// <summary>
        /// Calls Start for every component on target FObject
        /// </summary>
        /// <param name="fObject">Target FObject</param>
        private void ActivateFObject(FObject fObject)
        {
            for (int j = 0; j < fObject.components.Count; j++)
            {
                fObject.components[j].Start();
            }
        }

        /// <summary>
        /// Deactivates all the FObjects on this scene
        /// </summary>
        public void Deactivate()
        {
            for (int i = 0; i < FObjectList.Count; i++)
            {
                DeactivateFObject(FObjectList[i]);
            }
        }

        /// <summary>
        /// Calls Destroy for every component on target FObject
        /// </summary>
        /// <param name="fObject">Target FObject</param>
        private void DeactivateFObject(FObject fObject)
        {
            for (int j = 0; j < fObject.components.Count; j++)
            {
                fObject.components[j].Destroy();
            }
        }

        /// <summary>
        /// Creates a FObject
        /// </summary>
        /// <returns>new FObject</returns>
        public FObject CreateFObject()
        {
            FObject fObject = new();
            FObjectList.Add(fObject);
            fObject.parent = fObject;
            fObject.components.Add(fObject.transform);
            return fObject;
        }

        /// <summary>
        /// Creates a FObject with the parent
        /// </summary>
        /// <param name="parent">FObjetc's parent</param>
        /// <returns>new FObject</returns>
        public FObject CreateObject(FObject parent)
        {
            FObject fObject = new();
            FObjectList.Add(fObject);
            fObject.parent = parent ?? fObject;
            fObject.components.Add(fObject.transform);
            return fObject;
        }

        /// <summary>
        /// Creates a FObject with the parent and position
        /// </summary>
        /// <param name="parent">FObjetc's parent</param>
        /// <param name="position">FObject's position</param>
        /// <returns>new FObject</returns>
        public FObject CreateObject(FObject parent, Vector2f position)
        {
            FObject fObject = new();
            FObjectList.Add(fObject);
            fObject.parent = parent ?? fObject;
            fObject.components.Add(fObject.transform);
            fObject.transform.position = position;
            return fObject;
        }

        /// <summary>
        /// Creates a FObject with the parent, position, rotation, and scale
        /// </summary>
        /// <param name="parent">FObjetc's parent</param>
        /// <param name="position">FObjetc's position</param>
        /// <param name="rotation">FObjetc's rotation</param>
        /// <param name="scale">FObjetc's scale</param>
        /// <returns>new FObject</returns>
        public FObject CreateObject(FObject parent, Vector2f position, float rotation, Vector2f scale)
        {
            FObject fObject = new();
            FObjectList.Add(fObject);
            fObject.parent = parent ?? fObject;
            fObject.components.Add(fObject.transform);
            fObject.transform.position = position;
            fObject.transform.rotation = rotation;
            fObject.transform.scale = scale;
            return fObject;
        }
    }
}
