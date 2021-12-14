using UnityEngine;

/// <summary>
/// Resolves dependencies for the entire scene.
/// </summary>
public class ResolveScene : MonoBehaviour
{
    private DependencyResolver dependencyResolver;
    /// <summary>
    /// Resolve scene dependencies on awake.
    /// </summary>
    void Awake()
    {
        dependencyResolver = new DependencyResolver();
        resolveScene();
    }

    public void resolveScene()
    {
        dependencyResolver.ResolveScene();
    }
}

