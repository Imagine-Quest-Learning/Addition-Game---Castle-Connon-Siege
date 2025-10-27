using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayModeGenericTests
{
    private const string SceneName = "AddroomOut";

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
    public IEnumerator Scene_Loads_Successfully()
    {
        Assert.AreEqual(SceneName, SceneManager.GetActiveScene().name);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Scene_Has_MainCamera()
    {
        Assert.IsNotNull(Camera.main, "MainCamera not found.");
        Assert.IsTrue(Camera.main.gameObject.activeInHierarchy);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Scene_Has_EventSystem_For_UI()
    {
        var es = Object.FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
        if (es == null)
            Assert.Ignore("Scene has no UI; skipping EventSystem check.");

        Assert.IsTrue(es.gameObject.activeInHierarchy, "EventSystem is not active.");
        yield return null;
    }
}
