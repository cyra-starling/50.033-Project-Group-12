using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class G2PenGun : UltimateWeapon
{
    public GameObject bullet;
    public float bulletSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        nextFire = Time.time + GetFireRate();
        StartCoroutine(ultimateTick());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void FireAt(Transform target){
        if(IsReadyToFire()){
            // Instantiate bullet
            GameObject bulletShot = Instantiate(bullet, this.transform.position, this.transform.rotation);
            bulletShot.GetComponent<G2PenProjectile>().shotFrom = this.transform.parent.Find("Thymio 1/Body").gameObject;
            
            Rigidbody m_Rigidbody = bulletShot.GetComponent<Rigidbody>();
            m_Rigidbody.AddForce(this.transform.forward * bulletSpeed, ForceMode.Impulse);
            
            nextFire = Time.time + GetFireRate();
            this.GetComponentInParent<PlayerEvents>().firedUltimate.Invoke();
            ultiTicks = 0;
            StartCoroutine(ultimateTick());
        }
    }

    public override void LookAt(Vector3 target){
        // Determine the target rotation.  This is the rotation if the transform looks at the target point.
        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);

        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, GetTurnRate() * Time.deltaTime);
    }
    
    public override float GetFireRate(){
        return 30f;
    }

    public float GetTurnRate(){
        return 5f;
    }

    public override Events.UltimateWeapon GetUltimateWeaponType(){
        return Events.UltimateWeapon.G2PenGun;
    }
}
