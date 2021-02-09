using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

#pragma warning disable 618, 649
namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioSource m_FootstepAudioSource;
        [SerializeField] private AudioClip[] m_BreathSounds;    // an array of breath sounds that will be randomly selected from.
        [SerializeField] private AudioSource m_BreathAudioSource;

        private int prevFloor;

        public static Dreamfilter dFilter;
        private Camera m_Camera;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private float m_Stamina = 100.0f;
        private bool m_CanSprint = true;
        private bool m_RefreshSprint = true;

        // Use this for initialization
        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            dFilter = m_Camera.GetComponent<Dreamfilter>();
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle / 2f;
            m_MouseLook.Init(transform, m_Camera.transform);
        }


        // Update is called once per frame
        private void Update()
        {
            RotateView();

            // In order to sprint again after running out of stamina, require a keypress again
            m_RefreshSprint = m_RefreshSprint || (m_CanSprint && Input.GetKeyDown(KeyCode.LeftShift));
        }

        private void FixedUpdate()
        {
            Globals.playerFloor = (int)Mathf.Floor((-this.transform.position.y - 0.5f) / 3);

            if (prevFloor != Globals.playerFloor)
            {
                prevFloor = Globals.playerFloor;
                if (Globals.playerFloor + 1 < Floor.floors.Length)
                {
                    Floor.floors[Globals.playerFloor + 1].mRend.enabled = true;
                    if (Globals.playerFloor + 2 < Floor.floors.Length)
                    {
                        Floor.floors[Globals.playerFloor + 2].mRend.enabled = false;
                    }
                }
                if (Globals.playerFloor >= 1)
                {
                    Floor.floors[Globals.playerFloor - 1].mRend.enabled = true;
                    if (Globals.playerFloor >= 2)
                    {
                        Floor.floors[Globals.playerFloor - 2].mRend.enabled = false;
                    }
                }
            }
            DController.UpdateFloors();

            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo, m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x * speed;
            m_MoveDir.z = desiredMove.z * speed;

            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;
            }
            else
            {
                m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

            if (!m_IsWalking)
            {
                if (m_Stamina > 0.0f)
                {
                    m_Stamina -= m_CharacterController.velocity.magnitude * Time.fixedDeltaTime * 3;
                    if (m_Stamina < 50.0f)
                    {
                        if (!m_BreathAudioSource.isPlaying)
                        {
                            m_BreathAudioSource.clip = m_BreathSounds[Random.Range(1, m_BreathSounds.Length)];
                            m_BreathAudioSource.volume = Mathf.Min(0.7f, (70.0f - m_Stamina) / 70.0f);
                            m_BreathAudioSource.Play();

                        }
                    }
                }
                else if (m_CanSprint)
                {
                    m_CanSprint = false;
                    m_RefreshSprint = false;
                    m_BreathAudioSource.clip = m_BreathSounds[0];
                    m_BreathAudioSource.loop = true;
                    m_BreathAudioSource.Play();
                }
            }
            else
            {
                m_Stamina = Mathf.Min(100.0f, m_Stamina + Time.fixedDeltaTime * 5);
                if (!m_CanSprint && m_Stamina > 10.0f)
                {
                    m_Stamina = 0.0f;
                    m_CanSprint = true;
                    m_BreathAudioSource.loop = false;
                }
            }

            // Progress step cycle
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed * (m_IsWalking ? 1f : m_RunstepLenghten))) * Time.fixedDeltaTime;
            }
            if (m_StepCycle > m_NextStep)
            {
                // Play footstep sound
                if (m_CharacterController.isGrounded)
                {
                    // pick & play a random footstep sound from the array,
                    // excluding sound at index 0
                    int n = Random.Range(1, m_FootstepSounds.Length);
                    m_FootstepAudioSource.clip = m_FootstepSounds[n];
                    m_FootstepAudioSource.PlayOneShot(m_FootstepAudioSource.clip);
                    // move picked sound to index 0 so it's not picked next time
                    m_FootstepSounds[n] = m_FootstepSounds[0];
                    m_FootstepSounds[0] = m_FootstepAudioSource.clip;
                }
                m_NextStep = m_StepCycle + m_StepInterval;
            }

            // Update Camera position
            if (m_UseHeadBob)
            {
                if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
                {
                    m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude + (speed * (m_IsWalking ? 1f : m_RunstepLenghten)));
                    m_Camera.transform.localPosition = m_Camera.transform.localPosition;
                }
                else
                {
                    m_Camera.transform.localPosition = m_Camera.transform.localPosition;
                }
            }

            m_MouseLook.UpdateCursorLock();
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !(Input.GetKey(KeyCode.LeftShift) && m_CanSprint && m_RefreshSprint);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }


        private void RotateView()
        {
            m_MouseLook.LookRotation(transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
        }
    }
}
