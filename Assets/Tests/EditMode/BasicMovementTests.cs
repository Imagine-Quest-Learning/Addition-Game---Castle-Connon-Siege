using NUnit.Framework;
using UnityEngine;
using System.Collections;

public class BasicMovementTests
{
    GameObject go;
    BasicMovement move;

    [SetUp]
    public void SetUp()
    {
        go = new GameObject("Player");
        move = go.AddComponent<BasicMovement>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(go);
    }

    [Test]
    public void DefaultSpeed_Is_Positive()
    {
        Assert.Greater(move.speed, 0f);
    }

    [Test]
    public void SetCanMove_TogglesFlag()
    {
        move.SetCanMove(false);
        Assert.IsFalse(move.canMove);

        move.SetCanMove(true);
        Assert.IsTrue(move.canMove);
    }

    [Test]
    public void Update_DoesNotMove_When_CanMove_IsFalse()
    {
        var start = go.transform.position;
        move.SetCanMove(false);

        move.Invoke("Update", 0f);
        Assert.AreEqual(start, go.transform.position);
    }
}
