using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float timeout;
    public IEnumerator Vanish() {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(timeout);
        gameObject.SetActive(true);
    }
}
