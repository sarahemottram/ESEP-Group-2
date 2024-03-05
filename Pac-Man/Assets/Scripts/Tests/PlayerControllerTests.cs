//using UnityEngine;
//using UnityEngine.Assertions;
//using 
////did you read somewhere that xunit is compatible with unity? mr gpt said it waws ok i just wasnt sure bc its like mostly C# but kinda different i asked it if nunit was better and xunit didn't work and he/she/it/they said they both work
//public class PlayerControllerTests
//{
//    [Fact]
//    public void PlayerMovesForward()
//    {
//        // Arrange
//        GameObject playerObject = new GameObject();
//        PlayerController playerController = playerObject.AddComponent<PlayerController>();
//        CharacterController characterController = playerObject.AddComponent<CharacterController>();
//        float speed = 5f;
//        playerController.speed = speed;

//        // Act
//        playerController.UpdateMovement(0f, 1f);

//        // Assert
//        Assert.Equal(Vector3.forward * speed * Time.deltaTime, characterController.velocity);
//    }

//    [Fact]
//    public void PlayerRotatesCorrectly()
//    {
//        // Arrange
//        GameObject playerObject = new GameObject();
//        PlayerController playerController = playerObject.AddComponent<PlayerController>();
//        CharacterController characterController = playerObject.AddComponent<CharacterController>();
//        float cameraRotationSpeed = 5f;
//        playerController.cameraRotationSpeed = cameraRotationSpeed;

//        // Act
//        playerController.UpdateMovement(-1f, 0f);

//        // Assert
//        Assert.Equal(Quaternion.Euler(0f, -90f, 0f), playerObject.transform.rotation);
//    }
//}