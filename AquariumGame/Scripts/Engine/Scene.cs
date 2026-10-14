using System;
using System.Collections.Generic;
using System.Text;

namespace AquariumGame.Scripts.Engine
{
    internal class Scene(string name)
    {
        private static int lastID = 0;
        public string name { get; private set; } = name;
        public int ID { get; private set; } = lastID++;
        public List<FObject> FObjectList = [];
        public void Activate()
        {
            for (int i = 0; i < FObjectList.Count; i++)
            {
                ActivateFObject(FObjectList[i]);
            }
        }
        private void ActivateFObject(FObject fObject)
        {
            for (int j = 0; j < fObject.components.Count; j++)
            {
                fObject.components[j].Start();
            }
            for (int i = 0; i< fObject.children.Count; i++)
            {
                ActivateFObject(fObject.children[i]);
            }
        }
        public void Deactivate()
        {
            for (int i = 0; i < FObjectList.Count; i++)
            {
                DeactivateFObject(FObjectList[i]);
            }
        }
        private void DeactivateFObject(FObject fObject)
        {
            for (int j = 0; j < fObject.components.Count; j++)
            {
                fObject.components[j].Destroy();
            }
            for (int i = 0; i < fObject.children.Count; i++)
            {
                DeactivateFObject(fObject.children[i]);
            }
        }
    }
}
