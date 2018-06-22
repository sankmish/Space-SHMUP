using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private SM_BoundsCheck bndCheck;


    void Awake()
    {
        bndCheck = GetComponent<SM_BoundsCheck>();
    }

	void Update () {
		if (bndCheck.offUp)
        {
            Destroy(gameObject);
        }

	}
}
