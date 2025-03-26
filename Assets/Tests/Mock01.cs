using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Mock01
{
    // A Test behaves as an ordinary method
    [Test]
    public void AdditionTest()
    {
        // int result = 2 + 3
        // Assert.AreEqual(4, result, "2 + 3은 5여야 합니다.");

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Mock01WithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
