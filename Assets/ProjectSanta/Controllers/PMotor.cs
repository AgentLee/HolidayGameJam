using UnityEngine;

namespace ProjectSanta.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    public class PMotor : MonoBehaviour
    {
        private Vector3 velocity = Vector3.zero;

        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
        
        private void OnCollisionExit(Collision collision)
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
        }
    }
}
