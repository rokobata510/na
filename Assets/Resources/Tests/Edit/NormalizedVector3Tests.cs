using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


/*public class NormalizedVector3Tests
{
    public double maxDifferenceOfDoubles = 0.01d;
    #region NormalizedVector3Tests
    // A Test behaves as an ordinary method
    public void ImplicitConversionFromVector3Works()
    {
        // Arrange
        Vector3 testVector = new(1, 2, 3);

        // Act
        NormalizedVector3 normalizedVector = (NormalizedVector3)testVector;

        // Assert
        Assert.AreEqual(testVector.normalized.x, normalizedVector.Vector.x, maxDifferenceOfDoubles);
        Assert.AreEqual(testVector.normalized.y, normalizedVector.Vector.y, maxDifferenceOfDoubles);
        Assert.AreEqual(testVector.normalized.z, normalizedVector.Vector.z, maxDifferenceOfDoubles);
    }

    [Test]
    public void ImplicitConversionToVector3Works()
    {
        NormalizedVector3 normalizedVector = new(1, 2, 3);
        Vector3 vector = normalizedVector;
        Assert.AreEqual(normalizedVector.Vector.x, vector.x, maxDifferenceOfDoubles);
        Assert.AreEqual(normalizedVector.Vector.y, vector.y, maxDifferenceOfDoubles);
        Assert.AreEqual(normalizedVector.Vector.z, vector.z, maxDifferenceOfDoubles);
    }

    [Test]
    public void ConstructorWithFloatsProducesNormalizedVector()
    {
        NormalizedVector3 normalizedVector = new(1, 2, 3);
        Assert.AreEqual(new Vector3(1, 2, 3).normalized.x, normalizedVector.Vector.x, maxDifferenceOfDoubles);
        Assert.AreEqual(new Vector3(1, 2, 3).normalized.y, normalizedVector.Vector.y, maxDifferenceOfDoubles);
        Assert.AreEqual(new Vector3(1, 2, 3).normalized.z, normalizedVector.Vector.z, maxDifferenceOfDoubles);
    }

    [Test]
    public void ConstructorWithVector3ProducesNormalizedVector()
    {
        Vector3 testVector = new(1, 2, 3);
        NormalizedVector3 normalizedVector = new(testVector);
        Assert.AreEqual(testVector.normalized.x, normalizedVector.Vector.x, maxDifferenceOfDoubles);
        Assert.AreEqual(testVector.normalized.y, normalizedVector.Vector.y, maxDifferenceOfDoubles);
        Assert.AreEqual(testVector.normalized.z, normalizedVector.Vector.z, maxDifferenceOfDoubles);

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator VectorWrappersTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
    #endregion

}
*/