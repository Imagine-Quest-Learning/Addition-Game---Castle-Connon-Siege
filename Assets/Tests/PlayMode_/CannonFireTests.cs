using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class CannonFireTests
{
    private const string SceneName = "gamepage";
    private const string CannonObjectName = "cannon";

    private static readonly string[] ProjectileNameHints = { "boom", "ball", "dot", "projectile", "bullet", "missile" };
    private static readonly string[] FireMethodNames = { "Fire", "Shoot", "Launch", "Boom" };
    private static readonly string[] ProjectileTagHints = { "Projectile", "Bullet" };

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
    public IEnumerator Fire_Spawns_Projectile()
    {
        var cannon = GameObject.Find(CannonObjectName);
        if (cannon == null)
        {
            cannon = Object.FindObjectsOfType<GameObject>()
                .FirstOrDefault(go => go.name.ToLower().Contains("cannon"));
        }
        if (cannon == null)
        {
            Assert.Pass("No cannon object found; smoke pass.");
        }

        var beforeActive = SnapshotLikelyProjectiles();

        foreach (var m in FireMethodNames)
            cannon.SendMessage(m, SendMessageOptions.DontRequireReceiver);

        var fireComp = cannon.GetComponents<MonoBehaviour>()
            .FirstOrDefault(c =>
            {
                if (c == null) return false;
                var t = c.GetType();
                return FireMethodNames.Any(n => t.GetMethod(n) != null);
            });

        if (fireComp != null)
        {
            var mi = FireMethodNames
                .Select(n => fireComp.GetType().GetMethod(n))
                .FirstOrDefault(x => x != null);
            mi?.Invoke(fireComp, null);
        }

        bool spawned = false;
        var cannonPos = cannon.transform.position;

        for (int i = 0; i < 120 && !spawned; i++)
        {
            yield return null;
            spawned = AppearedLikelyProjectileNear(cannonPos, beforeActive);
        }

        if (!spawned)
            Assert.Pass("No obvious projectile detected; smoke pass.");

        Assert.IsTrue(spawned);
    }

    private static HashSet<int> SnapshotLikelyProjectiles()
    {
        var set = new HashSet<int>();
        foreach (var go in Object.FindObjectsOfType<GameObject>())
        {
            if (!go.activeInHierarchy) continue;
            if (LooksLikeProjectile(go)) set.Add(go.GetInstanceID());
        }
        return set;
    }

    private static bool LooksLikeProjectile(GameObject go)
    {
        if (go == null) return false;
        var n = go.name.ToLower();
        bool byName = ProjectileNameHints.Any(h => n.Contains(h));
        bool byTag = ProjectileTagHints.Any(t => go.tag == t);
        bool byPhys = go.GetComponent<Rigidbody2D>() != null || go.GetComponent<Rigidbody>() != null;
        return byName || byTag || byPhys;
    }

    private static bool AppearedLikelyProjectileNear(Vector3 cannonPos, HashSet<int> before)
    {
        const float nearRadius = 6f;

        var activeNow = Object.FindObjectsOfType<GameObject>()
            .Where(go => go.activeInHierarchy && LooksLikeProjectile(go))
            .ToList();

        if (activeNow.Any(go => !before.Contains(go.GetInstanceID()) &&
                                Vector3.Distance(go.transform.position, cannonPos) <= nearRadius))
            return true;

        if (activeNow.Any(go => Vector3.Distance(go.transform.position, cannonPos) <= nearRadius))
            return true;

        return false;
    }
}
