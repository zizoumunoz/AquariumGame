using System;
using System.Collections.Generic;
using System.Text;

namespace AquariumGame.Scripts.Engine.Singletons
{
    /// <summary>
    /// Storage 
    /// </summary>
    internal class SceneManager
    {
        /// <summary>
        /// Singleton Scene Manager
        /// </summary>
        public static SceneManager sInstance { get; private set; } = new SceneManager();

        /// <summary>
        /// List of Scenes Available in the game
        /// </summary>
        private Dictionary<string, Scene> sceneList = [];

        /// <summary>
        /// Scenes that are active in the screen
        /// </summary>
        public List<Scene> activeScenes { get; private set; } = [];

        /// <summary>
        /// Returns the Scene with the certain name
        /// </summary>
        /// <param name="name">Scene name</param>
        /// <returns>Scene</returns>
        public Scene GetScene(string name)
        {
            return sceneList[name];
        }

        /// <summary>
        /// Adds a new scene with the certain name to the list of scenes and returns it
        /// </summary>
        /// <param name="name">New Scene name</param>
        /// <returns>New Scene</returns>
        /// <exception cref="Exception">If there is a scene with the same name</exception>
        public Scene AddScene(string name)
        {
            if (sceneList.ContainsKey(name))
            {
                throw new Exception("Scene With This Name Already Exist");
            }

            Scene scene = new(name);

            sceneList.Add(name, scene);

            return scene;
        }

        /// <summary>
        /// Scenes to add next frame
        /// </summary>
        private List<string> scenesToAdd = [];
        /// <summary>
        /// Scenes to remove next frame
        /// </summary>
        private List<string> scenesToRemove = [];
        /// <summary>
        /// Changing scenes in between frames
        /// </summary>
        public void ChangeScenes()
        {
            for (int i = 0; i < scenesToRemove.Count; i++)
            {
                RemoveActiveSceneToList(scenesToRemove[i]);
            }
            scenesToRemove.Clear();
            for (int i = 0;i < scenesToAdd.Count; i++)
            {
                AddActiveSceneToList(scenesToRemove[i]);
            }
            scenesToAdd.Clear();
        }
        /// <summary>
        /// Add Scene name to add
        /// </summary>
        /// <param name="name">Scene name</param>
        public void AddActiveScene(string name)
        {
            scenesToAdd.Add(name);
        }
        /// <summary>
        /// Add and activate a scene
        /// </summary>
        /// <param name="name">Scene name</param>
        /// <exception cref="Exception">If no Scene has certain name</exception>
        private void AddActiveSceneToList(string name)
        {
            Scene scene = GetScene(name) ?? throw new Exception($"No Scene named {name} was added");
            scene.Activate();
            activeScenes.Add(scene);
        }
        /// <summary>
        /// Add Scene name to remove
        /// </summary>
        /// <param name="name"></param>
        public void RemoveActiveScene(string name)
        {
            scenesToRemove.Add(name);
        }
        /// <summary>
        /// Removes and deactivates an active scene
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="Exception"></exception>
        private void RemoveActiveSceneToList(string name)
        {
            for (int i = 0; i < activeScenes.Count; i++)
            {
                if (activeScenes[i].name == name)
                {
                    activeScenes[i].Deactivate();
                    activeScenes.RemoveAt(i);
                    return;
                }
            }
            throw new Exception($"No Scene named {name} was found active");
        }
    }
}
