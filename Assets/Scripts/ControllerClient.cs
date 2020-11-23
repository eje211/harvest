using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerClient : MonoBehaviour
{
    public float verticalSpeed;
    public float horizontalSpeed;
    public Camera camera;
    public float shootDuration = 0.2f;
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetWidth(0.3f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * horizontalSpeed, Input.GetAxis("Vertical") * verticalSpeed));
        Vector3 ea = transform.eulerAngles;
        float z = ea.z;
        if (ea.z > 20f && ea.z < 330f) {
            z = 20f;
            if (ea.z > 200f) {
                z = 330f;
            }
        }
        transform.eulerAngles = new Vector3(0, ea.y, z);

        
        RaycastHit rayCastHit;
        if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out rayCastHit, Mathf.Infinity, 255)) {
            StopCoroutine(TurnOffLine());
            Color color = (transform.eulerAngles.y + 90f) % 360 < 180f ? Color.green : Color.red;
            GetComponent<Renderer>().material.color = color;
            if (Input.GetButton("Fire1")) {
                lineRenderer.startColor = color;
                lineRenderer.endColor = color;
                Vector3[] positions = new Vector3[2];
                positions[0] = transform.position;
                positions[1] = rayCastHit.transform.position;
                lineRenderer.SetPositions(positions);
                StartCoroutine(TurnOffLine());
                lineRenderer.enabled = true;
                StartCoroutine(rayCastHit.transform.gameObject.GetComponent<Boom>().Vanish());
            }
        } else {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    IEnumerator TurnOffLine() {
        yield return new WaitForSeconds(shootDuration);
        lineRenderer.enabled = false;
        yield return null;
    }
}
