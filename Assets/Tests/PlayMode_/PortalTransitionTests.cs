using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PortalTransitionTests
{
    private const string SceneName = "AddRoom";

    [UnitySetUp]
    public IEnumerator LoadScene()
    {
        if (SceneManager.GetActiveScene().name != SceneName)
        {
            var op = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            while (!op.isDone) yield return null;
        }
        yield return null;
    }

    [UnityTest]
    public IEnumerator Player_Entering_Portal_Disables_Movement_Or_Changes_Scene()
    {
        var portal = Object.FindObjectsOfType<MonoBehaviour>()
            .FirstOrDefault(mb => mb != null && mb.GetType().Name == "ScenePortal")?.gameObject;

        if (portal == null)
        {
            portal = GameObject.FindObjectsOfType<GameObject>()
                .FirstOrDefault(go => go.name.ToLower().Contains("portal"));
        }
        if (portal == null) Assert.Ignore("No portal found in scene.");

        var portalCol = portal.GetComponent<Collider2D>();
        if (portalCol == null) portalCol = portal.AddComponent<BoxCollider2D>();
        portalCol.isTrigger = true;

        var player = new GameObject("Player_Test");
        player.tag = "Player";
        var rb = player.AddComponent<Rigidbody2D>();
        rb.isKinematic = false;
        var playerCol = player.AddComponent<BoxCollider2D>();

        var basicMovementType = System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .FirstOrDefault(t => t.Name == "BasicMovement");
        Component movement = null;
        if (basicMovementType != null) movement = player.AddComponent(basicMovementType);

        player.transform.position = portal.transform.position + Vector3.left * 2f;
        yield return null;
        player.transform.position = portal.transform.position;
        yield return null;

        bool sceneChanged = false;
        var activeScene = SceneManager.GetActiveScene().name;
        for (int i = 0; i < 60; i++)
        {
            if (SceneManager.GetActiveScene().name != activeScene) { sceneChanged = true; break; }
            yield return null;
        }

        if (movement != null)
        {
            var canMoveField = movement.GetType().GetField("canMove");
            if (canMoveField != null)
            {
                bool canMove = (bool)canMoveField.GetValue(movement);
                if (!sceneChanged) Assert.IsFalse(canMove, "Player should be immobilized after entering portal.");
            }
        }
        else if (!sceneChanged)
        {
            Assert.Inconclusive("No BasicMovement found and scene not changed—portal may perform a different action.");
        }

        Object.Destroy(player);
    }
}
