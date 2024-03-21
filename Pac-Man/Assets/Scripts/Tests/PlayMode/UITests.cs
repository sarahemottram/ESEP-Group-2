using NUnit.Framework;
using PacMan;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITests : MonoBehaviour
{
    //Test GameObject Setup
    private GameObject testObjectPlayer;
    private GameObject uiManager;
    private GameObject uiScore;
    private GameObject uiLives;
    [SetUp]
    public void Setup()
    {
        uiManager = new GameObject("UI Manager :>");
        var uim = uiManager.AddComponent<UIManager>();
        uiManager.AddComponent<Canvas>();

        uiScore = new GameObject("Score Count");
        uim.scoreText = uiScore.AddComponent<Text>();
        uiScore.transform.SetParent(uiManager.transform);

        uiLives = new GameObject("Life Count");
        uim.livesText = uiLives.AddComponent<Text>();
        uiLives.transform.SetParent(uiManager.transform);

        testObjectPlayer = new GameObject("mappy");
        testObjectPlayer.tag = "Pac-Man";
        testObjectPlayer.AddComponent<PelletController>();
        testObjectPlayer.AddComponent<PlayerLife>();

    }
    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(testObjectPlayer);
        Object.DestroyImmediate(uiManager);
        Object.DestroyImmediate(uiScore);
        Object.DestroyImmediate(uiLives);
 
    }

    [Test]
    public void UIChanges_OnLostLife()
    {
        //Arrange
        var playerLife = testObjectPlayer.GetComponent<PlayerLife>();
        UIManager.Instance.SubscribeLives(playerLife);
        var uiLives = UIManager.Instance.livesText;

        //Act
        var lives = playerLife.Lives;
        playerLife.LoseLife();

        //Assert
        Assert.AreEqual((lives - 1).ToString(), uiLives.text);
    }
    [Test]
    public void UIChanges_OnPelletCollected()
    {
        //Arrange
        var pelletController = testObjectPlayer.GetComponent<PelletController>();
        UIManager.Instance.SubscribeScore(pelletController);
        var uiScore = UIManager.Instance.scoreText;

        //Act
        pelletController.Score = 100;

        //Assert
        Assert.AreEqual(pelletController.Score.ToString(), uiScore.text);
    }
}
