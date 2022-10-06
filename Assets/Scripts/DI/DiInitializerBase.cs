using UnityEngine;

namespace Asteroids.DI
{
    public abstract class DiInitializerBase : MonoBehaviour
    {
        protected DiContainer DiContainer;

        private void Awake()
        {
            DiContainer = new DiContainer();

            InstallBindings();
            
            DiContainer.InjectAllBinded();

            InjectAllGameObjectsInScene();
        }

        protected abstract void InstallBindings();

        private void InjectAllGameObjectsInScene()
        {
            var allObjects = FindObjectsOfType<GameObject>();
            
            foreach (var go in allObjects)
            {
                foreach (var component in go.GetComponents<Component>())
                {
                    DiContainer.Inject(component);
                }
            }
        }
    }
}