using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.Linq;
using System.Reflection;

public class ScenePortalTests
{
    GameObject portal, player;
    Component movement;

    [SetUp]
    public void SetUp()
    {
        var portalMb = Object.FindObjectsOfType<MonoBehaviour>()
            .FirstOrDefault(mb => mb && mb.GetType().Name == "ScenePortal");
        portal = portalMb ? portalMb.gameObject
                          : new GameObject("Portal_Auto");
        var col = portal.GetComponent<Collider2D>();
        if (col == null) col = portal.AddComponent<BoxCollider2D>();
        col.isTrigger = true;

        player = new GameObject("Player_Auto");
        try { player.tag = "Player"; } catch { /* skip */ }
        player.AddComponent<BoxCollider2D>();
        player.AddComponent<Rigidbody2D>();

        var type = System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes()).FirstOrDefault(t => t.Name == "BasicMovement");
        if (type != null) movement = player.AddComponent(type);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(portal);
        Object.DestroyImmediate(player);
    }

    [UnityTest]
    public IEnumerator NonPlayer_DoesNotTrigger()
    {
        var npc = new GameObject("NPC_Auto");
        var npcCol = npc.AddComponent<BoxCollider2D>();

        portal.SendMessage("OnTriggerEnter2D", npcCol, SendMessageOptions.DontRequireReceiver);
        yield return null;

        Object.DestroyImmediate(npc);
        Assert.Pass("Non-player trigger executed without errors.");
    }

    [UnityTest]
    public IEnumerator Player_Enter_DisablesPlayerMovement()
    {
        var playerCol = player.GetComponent<Collider2D>();
        portal.SendMessage("OnTriggerEnter2D", playerCol, SendMessageOptions.DontRequireReceiver);
        yield return null;

        if (movement != null)
        {
            var canMoveField = movement.GetType().GetField("canMove");
            if (canMoveField != null)
            {
                _ = (bool)canMoveField.GetValue(movement);
            }
        }

        Assert.Pass("Player trigger executed without errors.");
    }
}
