using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit something");
        //碰到方块
        /*if (other.gameObject.name == "Cube")
        {
            //本体执行动画
            //transform.GetComponent<Animation>().CrossFade("action");

            //被碰物体执行动画
            //collision.transform.GetComponent<Animation>().CrossFade("action2");

            //被碰物体5f后消失
            Destroy(other.gameObject, 1);
        }*/
    }
}
