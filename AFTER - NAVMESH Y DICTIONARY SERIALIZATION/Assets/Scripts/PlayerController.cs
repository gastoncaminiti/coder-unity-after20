using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject pointer;
    // Start is called before the first frame update
    private NavMeshAgent playerAgent;

    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        //playerAgent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointer.transform.position = GetPositionTo(Input.mousePosition);
        }
        playerAgent.destination = pointer.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lifes"))
        {
            GameManager.addScore();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Powers"))
        {
            GameManager.addPower();
            Destroy(other.gameObject);
        }
    }

    private Vector3 GetPositionTo(Vector3 newPosition)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(newPosition);
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
