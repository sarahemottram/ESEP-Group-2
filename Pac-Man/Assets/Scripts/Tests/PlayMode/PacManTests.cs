using NUnit.Framework;
using UnityEngine.TestTools;
using System.Collections;
using PacMan;
using System.Runtime.CompilerServices;
using UnityEngine;

[TestFixture]
public class PacManTests
{
    //Test GameObject Setup
    private GameObject testObjectPlayer;
    private GameObject testObjectLocomotive;
    private GameObject testObjectPellet;
    private GameObject testObjectPowerPellet;
    private GameObject testObjectInky;
    private GameObject testObjectBlinky;
    private GameObject testObjectPinky;
    private GameObject testObjectClyde;
    [SetUp]
    public void Setup()
    {
        testObjectPlayer = new GameObject("mappy");
        testObjectPlayer.AddComponent<SphereCollider>();
        testObjectPlayer.AddComponent<PelletController>();

        testObjectLocomotive = new GameObject("amogus");
        testObjectLocomotive.AddComponent<PlayerLocomotion>();

        testObjectPellet = new GameObject("skibidy");
        testObjectPellet.AddComponent<SphereCollider>();
        testObjectPellet.tag = "Pellet";

        testObjectPowerPellet = new GameObject("toilet");
        testObjectPowerPellet.AddComponent<SphereCollider>();
        testObjectPowerPellet.tag = "Power Pellet";

        testObjectInky = new GameObject("inky");
        testObjectInky.AddComponent<GhostLogic>();
        testObjectInky.tag = "Inky";

        testObjectBlinky = new GameObject("blinky");
        testObjectBlinky.AddComponent<GhostLogic>();
        testObjectBlinky.tag = "Blinky";

        testObjectPinky = new GameObject("pinky");
        testObjectPinky.AddComponent<GhostLogic>();
        testObjectPinky.tag = "Pinky";

        testObjectClyde = new GameObject("clyde");
        testObjectClyde.AddComponent<GhostLogic>();
        testObjectClyde.tag = "Clyde";
    }
    [TearDown]
    public void TearDown()
    {
        Object.Destroy(testObjectPlayer);
        Object.DestroyImmediate(testObjectLocomotive);
        Object.DestroyImmediate(testObjectPellet);
        Object.DestroyImmediate(testObjectPowerPellet);
        Object.DestroyImmediate (testObjectInky);
        Object.DestroyImmediate(testObjectBlinky);
        Object.DestroyImmediate(testObjectPinky);
        Object.DestroyImmediate(testObjectClyde);
    }
    
    //Tests for PlayerLocomotion
    [Test]
    public void GetMovement_ForwardMovement_ReturnsCorrectValues()
    {
        var playerLocomotion = testObjectLocomotive.GetComponent<PlayerLocomotion>(); //this reminds me that var doesn't exist in other unity scripts wtf ytes it does???
        Quaternion modelRotation = Quaternion.identity;
        float cameraRotationSpeed = 1f;
        float horizontal = 0f;
        float vertical = 1f;
        Vector3 forward = Vector3.forward;
        Vector3 right = Vector3.right;
        Vector3 moveDirection;
        Quaternion targetRotation;

        playerLocomotion.GetMovement(modelRotation, cameraRotationSpeed, horizontal, vertical, forward, right, out moveDirection, out targetRotation);

        Assert.AreEqual(forward, moveDirection);
        Assert.AreEqual(Quaternion.LookRotation(forward), targetRotation);
    }

    [Test]
    public void GetMovement_BackwardMovement_ReturnsCorrectValues()
    {
        var playerLocomotion = testObjectLocomotive.GetComponent<PlayerLocomotion>();
        Quaternion modelRotation = Quaternion.identity;
        float cameraRotationSpeed = 1f;
        float horizontal = 0f;
        float vertical = -1f;
        Vector3 forward = Vector3.forward;
        Vector3 right = Vector3.right;
        Vector3 moveDirection;
        Quaternion targetRotation;

        playerLocomotion.GetMovement(modelRotation, cameraRotationSpeed, horizontal, vertical, forward, right, out moveDirection, out targetRotation);

        Assert.AreEqual(-forward, moveDirection);
        Assert.AreEqual(Quaternion.LookRotation(-forward), targetRotation);
    }

    [Test]
    public void GetMovement_LeftMovement_ReturnsCorrectValues()
    {
        var playerLocomotion = testObjectLocomotive.GetComponent<PlayerLocomotion>();
        Quaternion modelRotation = Quaternion.identity;
        float cameraRotationSpeed = 1f;
        float horizontal = -1f;
        float vertical = 0f;
        Vector3 forward = Vector3.forward;
        Vector3 right = Vector3.right;
        Vector3 moveDirection;
        Quaternion targetRotation;

        playerLocomotion.GetMovement(modelRotation, cameraRotationSpeed, horizontal, vertical, forward, right, out moveDirection, out targetRotation);

        Assert.AreEqual(-right, moveDirection);
        Assert.AreEqual(Quaternion.LookRotation(-right), targetRotation);
    }

    [Test]
    public void GetMovement_RightMovement_ReturnsCorrectValues()
    {
        var playerLocomotion = testObjectLocomotive.GetComponent<PlayerLocomotion>();
        Quaternion modelRotation = Quaternion.identity;
        float cameraRotationSpeed = 1f;
        float horizontal = 1f;
        float vertical = 0f;
        Vector3 forward = Vector3.forward;
        Vector3 right = Vector3.right;
        Vector3 moveDirection;
        Quaternion targetRotation;

        playerLocomotion.GetMovement(modelRotation, cameraRotationSpeed, horizontal, vertical, forward, right, out moveDirection, out targetRotation);

        Assert.AreEqual(right, moveDirection);
        Assert.AreEqual(Quaternion.LookRotation(right), targetRotation);
    }

    [Test]
    public void GetMovement_NoMovement_ReturnsCorrectValues()
    {
        var playerLocomotion = testObjectLocomotive.GetComponent<PlayerLocomotion>();
        Quaternion modelRotation = Quaternion.identity;
        float cameraRotationSpeed = 1f;
        float horizontal = 0f;
        float vertical = 0f;
        Vector3 forward = Vector3.forward;
        Vector3 right = Vector3.right;
        Vector3 moveDirection;
        Quaternion targetRotation;

        playerLocomotion.GetMovement(modelRotation, cameraRotationSpeed, horizontal, vertical, forward, right, out moveDirection, out targetRotation);

        Assert.AreEqual(Vector3.zero, moveDirection);
        Assert.AreEqual(modelRotation, targetRotation);
    }

    //Tests for PelletController
    [Test]
    public void OnTriggerEnter_IncrementScoreWhenCollidingWithPellet()
    {
        // Arrange
        var pelletController = testObjectPlayer.GetComponent<PelletController>();
        var pelletCollider = testObjectPellet.GetComponent<SphereCollider>();
        int initialScore = pelletController.score;

        // Act
        pelletController.OnTriggerEnter(pelletCollider);

        // Assert
        Assert.AreEqual(initialScore + 1, pelletController.score);
    }

    [Test]
    public void OnTriggerEnter_IncrementScoreWhenCollidingWithPowerPellet()
    {
        // Arrange
        var pelletController = testObjectPlayer.GetComponent<PelletController>();
        pelletController.InitializeGhosts();
        var pelletCollider = testObjectPowerPellet.GetComponent<SphereCollider>();
        int initialScore = pelletController.score;

        // Act
        pelletController.OnTriggerEnter(pelletCollider);

        // Assert
        Assert.AreEqual(initialScore + 1, pelletController.score);
    }

    [Test]
    public void InitializeGhosts_SetsReferencesToGhostObjects()
    {
        // Arrange
        var pelletController = testObjectPlayer.GetComponent<PelletController>();
        pelletController.InitializeGhosts();

        // Act
        GameObject inky = pelletController.inky;
        GameObject blinky = pelletController.blinky;
        GameObject pinky = pelletController.pinky;
        GameObject clyde = pelletController.clyde;

        // Assert
        Assert.IsNotNull(inky);
        Assert.IsNotNull(blinky);
        Assert.IsNotNull(pinky);
        Assert.IsNotNull(clyde);
        Assert.AreEqual("Inky", inky.tag);
        Assert.AreEqual("Blinky", blinky.tag);
        Assert.AreEqual("Pinky", pinky.tag);
        Assert.AreEqual("Clyde", clyde.tag);
    }

    [Test]
    public void OnTriggerEnter_ScatterGhostsWhenCollidingWithPowerPellet()
    {
        // Arrange
        var pelletController = testObjectPlayer.GetComponent<PelletController>();
        pelletController.InitializeGhosts();
        var powerPelletCollider = testObjectPowerPellet.GetComponent<SphereCollider>();

        // Act
        pelletController.OnTriggerEnter(powerPelletCollider);

        // Assert
        Assert.IsTrue(testObjectInky.GetComponent<GhostLogic>().isScattered);
        Assert.IsTrue(testObjectBlinky.GetComponent<GhostLogic>().isScattered);
        Assert.IsTrue(testObjectPinky.GetComponent<GhostLogic>().isScattered);
        Assert.IsTrue(testObjectClyde.GetComponent<GhostLogic>().isScattered);
    }

    //GhostLogic Tests
    [UnityTest]
    public IEnumerator GhostScatter_ReturnsToChase()
    {
        // Arrange
        var ghostLogic = testObjectInky.GetComponent<GhostLogic>();

        // Act
        ghostLogic.Scatter();

        // Wait for 7 seconds
        yield return new WaitForSeconds(7f);

        // Assert
        Assert.IsFalse(ghostLogic.isScattered);
    }
}