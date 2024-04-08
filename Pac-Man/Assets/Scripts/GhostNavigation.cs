using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace PacMan
{
    public class GhostNavigation : MonoBehaviour
    {
        [SerializeField]
        Transform chase;
        [SerializeField]
        Transform chase2;

        private NavMeshAgent agent;
        private GhostLogic ghostLogic;
        private SphereCollider ghostCollider;
        private Vector3 scatter1;
        private Vector3 scatter2;
        private Vector3 agentPosition;
        private Vector3 augmentedChase;
        private Vector3 respawnPosition = new Vector3(0,.5f,2);
        private Quaternion respawnRotation = new Quaternion(0,0,180,0);
        private float chaseSpeed = 3.5f;
        private float scatterSpeed = 2;
        private float deadSpeed = 6;

        private float clydeTimer = 0;

        private Vector3 startPosition;

        private float ghostTimer;
        // Start is called before the first frame update
        void Start()
        {
            SetUp(); //Starts ghosts at appropriate times
        }

        // Update is called once per frame
        void Update()
        {
            if (ghostLogic.isDead  && (Vector3.Distance(agent.transform.position,respawnPosition) > 1) && agent.enabled)
            {
                agent.speed = deadSpeed;
                agent.destination = respawnPosition;
                ghostCollider.enabled = false;
                agent.transform.rotation = respawnRotation;
            }
            else 
            {
                if (!ghostCollider.enabled)
                {
                    ghostCollider.enabled = true;
                    agent.transform.rotation = Quaternion.identity;
                    ghostLogic.isDead = false;
                    ghostLogic.Chase();
                }
                switch (tag)
                {
                    case "Blinky":
                        BlinkyLogic();
                        break;
                    case "Pinky":
                        PinkyLogic();
                        break;
                    case "Inky":
                        InkyLogic();
                        break;
                    case "Clyde":
                        ClydeLogic();
                        break;
                    default:
                        break;
                }
            }
        }

        void SetUp()
        {
            switch (tag) //set scatter to desired quadrent
            {
                case "Blinky":
                    scatter1 = new Vector3(-3.3f, .5f, 12.3f);
                    scatter2 = new Vector3(-9.6f, .5f, 6.7f);
                    break;
                case "Pinky":
                    scatter1 = new Vector3(4.8f, .5f, -10f);
                    scatter2 = new Vector3(7f, .5f, -2.5f);
                    break;
                case "Inky":
                    scatter1 = new Vector3(3.3f, .5f, 12.3f);
                    scatter2 = new Vector3(9.6f, .5f, 6.7f);
                    break;
                case "Clyde":
                    scatter1 = new Vector3(-4.8f, .5f, -10f);
                    scatter2 = new Vector3(-7f, .5f, -2.5f);
                    break;
                default:
                    break;
            }
            agent = GetComponent<NavMeshAgent>();
            ghostLogic = GetComponent<GhostLogic>();
            ghostCollider = GetComponent<SphereCollider>();
            startPosition = agent.transform.position;

            GhostDelay();
        }

        private void BlinkyLogic() //chases pacman, scatters to top left corner
        {
            ghostTimer -= Time.deltaTime;
            if (ghostTimer > 0)
                return;
            if (agent != null && agent.enabled && !ghostLogic.isScattered) //chase
            {
                agent.speed = chaseSpeed;
                agent.destination = chase.position;
                agentPosition = agent.transform.position;
            }
            else if (agent != null && agent.enabled) //scatter
            {
                agent.speed = scatterSpeed;
                //this if statment just makes sure the ghost doesn't stand still
                if (Vector3.Distance(agentPosition, scatter1) > 1)
                {
                    agent.destination = scatter1;
                    agentPosition = agent.transform.position;
                }
                else
                    agent.destination = scatter2;
            }
        }

        private void PinkyLogic() //pinky chases above or below pac-man, scatters to bottom right
        {
            ghostTimer -= Time.deltaTime;
            if (ghostTimer > 0)
                return;
            if (agent != null && agent.enabled && !ghostLogic.isScattered) //chase mode
            {
                agent.speed = chaseSpeed;
                if(chase.position.z > 0)
                    augmentedChase = chase.position + new Vector3(0, 0, -5);
                else
                    augmentedChase = chase.position + new Vector3(0, 0, 5);
                agent.destination = augmentedChase;
                agentPosition = agent.transform.position;
            }
            else if (agent != null && agent.enabled) //scatter
            {
                agent.speed = scatterSpeed;
                //this if statment just makes sure the ghost doesn't stand still
                if (Vector3.Distance(agentPosition, scatter1) > 1)
                {
                    agent.destination = scatter1;
                    agentPosition = agent.transform.position;
                }
                else
                    agent.destination = scatter2;
            }
        }

        private void InkyLogic() //chases to inbetween pac-man and blinky, scatters to top right
        {
            ghostTimer -= Time.deltaTime;
            if (ghostTimer > 0)
                return;
            if (agent != null && agent.enabled && !ghostLogic.isScattered) //chase mode
            {
                agent.speed = chaseSpeed;
                augmentedChase = (chase.position - chase2.position) / 2;
                agent.destination = augmentedChase;
                agentPosition = agent.transform.position;
            }
            else if (agent != null && agent.enabled) //scatter
            {
                agent.speed = scatterSpeed;
                //this if statment just makes sure the ghost doesn't stand still
                if (Vector3.Distance(agentPosition, scatter1) > 1)
                {
                    agent.destination = scatter1;
                    agentPosition = agent.transform.position;
                }
                else
                    agent.destination = scatter2;
            }
        }

        private void ClydeLogic() //chases pacman, if he's within 5 of pacman he'll go to his scatter corner, scatters to bottom left
        {
            ghostTimer -= Time.deltaTime;
            clydeTimer -= Time.deltaTime;
            if (ghostTimer > 0)
                return;
            if (agent != null && agent.enabled && !ghostLogic.isScattered) //chase mode
            {
                agent.speed = chaseSpeed;
                if (Vector3.Distance(agent.transform.position, chase.position) < 5) //when he's too close to pac-man set clydetimer so he goes to scatter spot for 3 seconds
                    clydeTimer = 3;
                if (clydeTimer > 0)
                    agent.destination = scatter1;
                else
                    agent.destination = chase.position;
                agentPosition = agent.transform.position;
            }
            else if (agent != null && agent.enabled) //scatter
            {
                agent.speed = scatterSpeed;
                //this if statment just makes sure the ghost doesn't stand still
                if (Vector3.Distance(agentPosition, scatter1) > 1)
                {
                    agent.destination = scatter1;
                    agentPosition = agent.transform.position;
                }
                else
                    agent.destination = scatter2;
            }
        }

        internal void SoftReset()
        {
            StartCoroutine(SoftResetActions());
        }
        private IEnumerator SoftResetActions()
        {
            ghostTimer = 0;
            if(agent != null)
                agent.enabled = false;
            yield return new WaitForSeconds(3f); //wait for death sound
            agent.transform.position = startPosition;
            GhostDelay();
            agent.enabled = true;
        }
        internal void GhostDelay()
        {
            switch (tag)
            {
                case "Blinky":
                    ghostTimer = 5;
                    break;
                case "Pinky":
                    ghostTimer = 10;
                    break;
                case "Inky":
                    ghostTimer = 15;
                    break;
                case "Clyde":
                    ghostTimer = 20;
                    break;
                default:
                    break;
            }
        }
    }
}
