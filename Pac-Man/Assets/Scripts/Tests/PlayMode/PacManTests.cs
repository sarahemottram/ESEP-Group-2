using NUnit.Framework;
using UnityEngine.TestTools;
using System.Collections;
using PacMan;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;

[TestFixture]
public class PacManTests
{
    //Test GameObject Setup
    private GameObject testObjectPlayer;
    private GameObject testObjectModel;
    private GameObject testObjectLocomotive;
    private GameObject testObjectPellet;
    private GameObject testObjectPowerPellet;
    private GameObject testObjectInky;
    private GameObject testObjectBlinky;
    private GameObject testObjectPinky;
    private GameObject testObjectClyde;
    private GameObject testObjectVirtualCamera;
    private GameObject testFailState;
    private GameObject uiManager;
    private GameObject soundManager;
    private GameObject uiScore;
    private GameObject uiLives;
    [SetUp]
    public void Setup()
    {
        soundManager = new GameObject("sond manger");
        soundManager.AddComponent<SoundManager>();

        testObjectPlayer = new GameObject("mappy");
        testObjectPlayer.tag = "Pac-Man";
        testObjectPlayer.AddComponent<SphereCollider>();
        testObjectPlayer.AddComponent<PelletController>();
        testObjectPlayer.AddComponent<PlayerInput>();
        testObjectPlayer.AddComponent<PlayerReset>();
        testObjectPlayer.AddComponent<PlayerLife>();
        testObjectPlayer.AddComponent<WinState>();

        testObjectModel = new GameObject("Pac-Man Model");
        testObjectModel.transform.parent = testObjectPlayer.transform;

        testObjectLocomotive = new GameObject("amogus");
        testObjectLocomotive.AddComponent<PlayerLocomotion>();

        testObjectPellet = new GameObject("skibidy");
        testObjectPellet.AddComponent<SphereCollider>();
        testObjectPellet.AddComponent<MeshRenderer>();
        testObjectPellet.tag = "Pellet";

        testObjectPowerPellet = new GameObject("toilet");
        testObjectPowerPellet.AddComponent<SphereCollider>();
        testObjectPowerPellet.AddComponent<MeshRenderer>();
        testObjectPowerPellet.tag = "Power Pellet";

        testObjectInky = new GameObject("inky");
        testObjectInky.AddComponent<GhostLogic>();
        testObjectInky.AddComponent<GhostNavigation>();
        testObjectInky.AddComponent<NavMeshAgent>();
        testObjectInky.AddComponent<SphereCollider>();
        testObjectInky.tag = "Inky";

        testObjectBlinky = new GameObject("blinky");
        testObjectBlinky.AddComponent<GhostLogic>();
        testObjectBlinky.AddComponent<GhostNavigation>();
        testObjectBlinky.AddComponent<NavMeshAgent>();
        testObjectBlinky.AddComponent<SphereCollider>();
        testObjectBlinky.tag = "Blinky";

        testObjectPinky = new GameObject("pinky");
        testObjectPinky.AddComponent<GhostLogic>();
        testObjectPinky.AddComponent<GhostNavigation>();
        testObjectPinky.AddComponent<NavMeshAgent>();
        testObjectPinky.AddComponent<SphereCollider>();
        testObjectPinky.tag = "Pinky";

        testObjectClyde = new GameObject("clyde");
        testObjectClyde.AddComponent<GhostLogic>();
        testObjectClyde.AddComponent<GhostNavigation>();
        testObjectClyde.AddComponent<NavMeshAgent>();
        testObjectClyde.AddComponent<SphereCollider>();
        testObjectClyde.tag = "Clyde";

        testObjectVirtualCamera = new GameObject("Virtual Camera");

        testFailState = new GameObject("youwu luwus");
        testFailState.AddComponent<FailState>();
        testFailState.GetComponent<FailState>().blinky = testObjectBlinky.GetComponent<GhostNavigation>();
        testFailState.GetComponent<FailState>().pinky = testObjectPinky.GetComponent<GhostNavigation>();
        testFailState.GetComponent<FailState>().inky = testObjectInky.GetComponent<GhostNavigation>();
        testFailState.GetComponent<FailState>().clyde = testObjectClyde.GetComponent<GhostNavigation>();

        uiManager = new GameObject("UI Manager :>");
        var uim = uiManager.AddComponent<UIManager>();
        uiManager.AddComponent<Canvas>();

        uiScore = new GameObject("Score Count");
        uim.scoreText = uiScore.AddComponent<Text>();
        uiScore.transform.SetParent(uiManager.transform);

        uiLives = new GameObject("Life Count");
        uim.livesText = uiLives.AddComponent<Text>();
        uiLives.transform.SetParent(uiManager.transform);
 
    }
    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(testObjectPlayer);
        Object.DestroyImmediate(testObjectLocomotive);
        Object.DestroyImmediate(testObjectPellet);
        Object.DestroyImmediate(testObjectPowerPellet);
        Object.DestroyImmediate(testObjectInky);
        Object.DestroyImmediate(testObjectBlinky);
        Object.DestroyImmediate(testObjectPinky);
        Object.DestroyImmediate(testObjectClyde);
        Object.DestroyImmediate(testObjectVirtualCamera);
        Object.DestroyImmediate(testFailState);
        Object.DestroyImmediate(uiManager);
        Object.DestroyImmediate(uiScore);
        Object.DestroyImmediate(uiLives);
        Object.DestroyImmediate(soundManager);
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

        playerLocomotion.GetMovement(modelRotation, cameraRotationSpeed, horizontal, vertical, forward, right, out Vector3 moveDirection, out Quaternion targetRotation);

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

        playerLocomotion.GetMovement(modelRotation, cameraRotationSpeed, horizontal, vertical, forward, right, out Vector3 moveDirection, out Quaternion targetRotation);

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

        playerLocomotion.GetMovement(modelRotation, cameraRotationSpeed, horizontal, vertical, forward, right, out Vector3 moveDirection, out Quaternion targetRotation);

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

        playerLocomotion.GetMovement(modelRotation, cameraRotationSpeed, horizontal, vertical, forward, right, out Vector3 moveDirection, out Quaternion targetRotation);

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

        playerLocomotion.GetMovement(modelRotation, cameraRotationSpeed, horizontal, vertical, forward, right, out Vector3 moveDirection, out Quaternion targetRotation);

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
        int initialScore = pelletController.Score;

        // Act
        pelletController.OnTriggerEnter(pelletCollider);

        // Assert
        Assert.AreEqual(initialScore + 1, pelletController.Score);
    }

    [Test]
    public void OnTriggerEnter_IncrementScoreWhenCollidingWithPowerPellet()
    {
        // Arrange
        var pelletController = testObjectPlayer.GetComponent<PelletController>();
        pelletController.InitializeGhosts();
        var pelletCollider = testObjectPowerPellet.GetComponent<SphereCollider>();
        int initialScore = pelletController.Score;

        // Act
        pelletController.OnTriggerEnter(pelletCollider);

        // Assert
        Assert.AreEqual(initialScore + 1, pelletController.Score);
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

    //Failstate Tests
    [Test]
    public void LivesReduce_OnDeath()
    {
        //Arrange
        var playerLives = testObjectPlayer.GetComponent<PlayerLife>();
        var lives = playerLives.Lives;
        var failState = testFailState.GetComponent<FailState>();

        //Act
        failState.Die(testObjectPlayer);

        //Assert
        Assert.AreEqual(lives - 1, playerLives.Lives);
    }

    [Test]
    public void PacManRotates_OnDeath()
    {
        //Arrange
        var failState = testFailState.GetComponent<FailState>();
        
        //Act
        failState.Die(testObjectPlayer);

        // Assert
        var expected = Quaternion.Euler(-90, 0, 0);
        var result = testObjectModel.transform.rotation;
        float threshold = 0.0001f;
        Assert.IsTrue(Quaternion.Angle(expected, result) < threshold);
    }

    [Test]
    public void LifeEventsFire()
    {
        //Arrange
        var playerLives = testObjectPlayer.GetComponent<PlayerLife>();
        var lives = playerLives.Lives;
        int result = int.MaxValue;
        playerLives.OnLivesChanged += i => result = i;
        
        //Act
        playerLives.LoseLife();

        //Assert
        Assert.AreEqual(lives - 1, result);
    }

    [UnityTest]
    public IEnumerator GameOverLoads_On3Deaths()
    {
        //Arrange
        var playerLives = testObjectPlayer.GetComponent<PlayerLife>();
        var failState = testFailState.GetComponent<FailState>();

        //Act
        playerLives.LoseLife();
        playerLives.LoseLife();
        failState.Die(testObjectPlayer);

        bool gameOverSceneLoaded = false;
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.name == "Game Over")
            {
                gameOverSceneLoaded = true;
            }
        };

        // Wait for the pause time
        yield return new WaitForSeconds(3.5f);

        // Assert
        Assert.IsTrue(gameOverSceneLoaded, "Game Over scene should be loaded when you die 3 times");
    }

    [UnityTest]
    public IEnumerator GameOverDoesNotLoad_On1Death()
    {
        //Arrange
        var playerLives = testObjectPlayer.GetComponent<PlayerLife>();
        var failState = testFailState.GetComponent<FailState>();

        //Act
        failState.Die(testObjectPlayer);

        bool gameOverSceneLoaded = false;
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.name == "Game Over")
            {
                gameOverSceneLoaded = true;
            }
        };

        // Wait for the pause time
        yield return new WaitForSeconds(3.5f);

        // Assert
        Assert.IsFalse(gameOverSceneLoaded, "Game Over scene should not load when you die 1 time");
    }

    //WinState Tests
    [Test]
    public void PelletEventsFire()
    {
        //Arrange
        var controller = testObjectPlayer.GetComponent<PelletController>();
        int result = 0;
        controller.OnScoreChanged += i => result = i;
        
        //Act
        controller.Score += 1;
        
        //Assert
        Assert.AreEqual(controller.Score, result);
    }

    [UnityTest]
    public IEnumerator PelletController_Score244_LoadsWinScene()
    {
        // Arrange
        bool winSceneLoaded = false;
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.name == "Win")
            {
                winSceneLoaded = true;
            }
        };

        // Act
        // Trigger score change to 244
        testObjectPlayer.GetComponent<PelletController>().Score = 244;

        // Wait for the pause time
        yield return new WaitForSeconds(5);

        // Assert
        Assert.IsTrue(winSceneLoaded, "Win scene should be loaded when score reaches 244");
    }

    [UnityTest]
    public IEnumerator PelletController_ScoreNot244_DoesNotLoadWinScene()
    {
        // Arrange
        bool winSceneLoaded = false;
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.name == "Win")
            {
                winSceneLoaded = true;
            }
        };

        // Act
        // Trigger score change to something other than 244
        testObjectPlayer.GetComponent<PelletController>().Score = 100;

        // Wait for the pause time
        yield return new WaitForSeconds(5);

        // Assert
        Assert.IsFalse(winSceneLoaded, "Win scene should not be loaded when score is not 244");
    }
}