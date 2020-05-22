using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animator : MonoBehaviour
{
    private Animator mAnimator;

    //bool beAttacking = false;
    //bool beWorking = false;
    bool beDoingSomethings = false;

    bool walking = false;
    bool running = false;
    bool death = false;

    int AttackIndex = 0;
    int WeaponIndex = 0;
    int Ammunition = 10;

    GameObject RightHand;
    GameObject THandSword;
    GameObject Gun;
    GameObject laser;


    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        mAnimator.SetInteger("AttackIndex", 0);
        mAnimator.SetInteger("WeaponIndex", 0);
        mAnimator.SetInteger("Ammunition", 10);
        mAnimator.SetBool("Walking", false);
        mAnimator.SetBool("Running", false);
        mAnimator.SetBool("Death", false);

        RightHand = GameObject.FindGameObjectWithTag("RightHand");
        THandSword = RightHand.transform.Find("2Hand-Sword Variant").gameObject;
        Gun = RightHand.transform.Find("2Hand-Rifle").gameObject;
        laser = Gun.transform.Find("laser").gameObject;

        THandSword.SetActive(true);
        THandSword.GetComponent<BoxCollider>().enabled = false;

        Gun.SetActive(false);
        laser.SetActive(false);
        //clearState();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        walking = false;
        running = false;

        //算出方向向量
        Vector3 direction = new Vector3(horizontal, 0, vertical);

        if (direction != Vector3.zero)
        {
            //行走
            walking = true;
            mAnimator.SetBool("Walking", walking);
            AttackIndex = 0;

            //将角色旋转至指定方向
            transform.rotation = Quaternion.LookRotation(direction);

            //加速
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //左shift加速
                running = true;
                mAnimator.SetBool("Running", running);
            }
            else
            {
                //普通速度
                running = false;
                mAnimator.SetBool("Running", running);
            }

        }
        else
        {
            //站立
            walking = false;
            mAnimator.SetBool("Walking", walking);
            AttackIndex = 0;
        }

        if (!beDoingSomethings)
        {
            //双手剑平A
            if (WeaponIndex == 0 && Input.GetKeyDown(KeyCode.X))
            {
                beDoingSomethings = true;
                mAnimator.SetInteger("AttackIndex", AttackIndex);
                mAnimator.SetTrigger("Attacking");
                AttackIndex = (AttackIndex + 1) % 4;
            }
            //双手剑技能
            if (WeaponIndex == 0 && Input.GetKeyDown(KeyCode.C))
            {
                beDoingSomethings = true;
                mAnimator.SetTrigger("Skill01");
            }
            //枪支射击
            if (WeaponIndex == 1 && Input.GetKeyDown(KeyCode.X))
            {
                beDoingSomethings = true;
                if (Ammunition > 0)
                {
                    mAnimator.SetTrigger("Shooting");
                }
                else
                {
                    mAnimator.SetTrigger("Reload");
                }
            }
            //枪支换弹
            if (WeaponIndex == 1 && Input.GetKeyDown(KeyCode.F))
            {
                beDoingSomethings = true;
                mAnimator.SetTrigger("Reload");
            }
            //换武器
            if (Input.GetKeyDown(KeyCode.G))
            {
                beDoingSomethings = true;
                mAnimator.SetInteger("WeaponIndex", WeaponIndex);
                mAnimator.SetTrigger("ChangeWeapon");
            }

        }


        /*if (!beAttacking)
        {
            //双手剑平A
            if (WeaponIndex == 0 && Input.GetKeyDown(KeyCode.X))
            {
                mAnimator.SetTrigger("Attacking");
                mAnimator.SetInteger("AttackIndex", AttackIndex++);
                AttackIndex %= 4;
                beAttacking = true;
            }
            //双手剑技能
            if (WeaponIndex == 0 && Input.GetKeyDown(KeyCode.C))
            {
                mAnimator.SetTrigger("Skill01");
                AttackIndex = 0;
                //beAttacking = true;
            }
        }*/
        /*if (!beWorking)
        {
            //枪支射击
            if (WeaponIndex == 1 && Input.GetKeyDown(KeyCode.X))
            {
                beWorking = true;
                if (Ammunition > 0)
                {
                    mAnimator.SetTrigger("Shooting");
                    mAnimator.SetInteger("Ammunition", Ammunition--);
                }
                else
                {
                    mAnimator.SetTrigger("Reload");
                    mAnimator.SetInteger("Ammunition", 10);
                    Ammunition = 10;
                }
            }
            //换弹
            if (WeaponIndex == 1 && Input.GetKeyDown(KeyCode.F))
            {
                mAnimator.SetTrigger("Reload");
                mAnimator.SetInteger("Ammunition", 10);
                Ammunition = 10;
            }
        }*/
        //换枪
        /*if (Input.GetKeyDown(KeyCode.N))
        {
            clearState();
            mAnimator.SetInteger("WeaponIndex", 1);
            WeaponIndex = 1;
            mAnimator.SetTrigger("ChangeWeapon");
        }
        //换双手剑
        if (Input.GetKeyDown(KeyCode.M))
        {
            clearState();
            mAnimator.SetInteger("WeaponIndex", 0);
            WeaponIndex = 0;
            mAnimator.SetTrigger("ChangeWeapon");
        }*/

        

    }

    private void OnCollisionEnter(Collision collision)
    {
        //碰到地面
        if (collision.gameObject.name == "Plane")
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (beDoingSomethings)
        {
            if (other.gameObject.name == "Cube")
            {
                Debug.Log("Hit the cube");
                Destroy(other.gameObject, 1);
            }
            if (other.gameObject.name == "Enemy")
            {
                Debug.Log("Hit the Enemy");
                Animator enemyAnimator = other.transform.GetComponent<Animator>();
                enemyAnimator.SetTrigger("GetHit");
            }
        }
        
    }

    /*public void clearState()
    {
        //beDoingSomethings = false;
        //beAttacking = false;
        AttackIndex = 0;

        walking = false;
        running = false;
    }*/
    
    public void startHit()
    {
        THandSword.GetComponent<BoxCollider>().enabled = true;
        //mAnimator.transform.FindChild("2Hand-Sword Variant").GetComponent<BoxCollider>().enabled = true;
    }
    public void endHit()
    {
        THandSword.GetComponent<BoxCollider>().enabled = false;
        //mAnimator.transform.FindChild("2Hand-Sword Variant").GetComponent<BoxCollider>().enabled = false;
        beDoingSomethings = false;
    }
    /*public void SkillHit()
    {
        beDoingSomethings = true;
        AttackIndex = 0;
        mAnimator.SetInteger("AttackIndex", AttackIndex);
        //clearState();
        Debug.Log("SkillHit");
        //beAttacking = true;
    }*/
    public void startShooting()
    {
        //mAnimator.transform.FindChild("laser").GetComponent<BoxCollider>().enabled = true;
        //mAnimator.transform.FindChild("laser").gameObject.SetActive(true);
        laser.SetActive(true);
    }
    public void endShooting()
    {
        //mAnimator.transform.FindChild("laser").GetComponent<BoxCollider>().enabled = false;
        //mAnimator.transform.FindChild("laser").gameObject.SetActive(false);
        laser.SetActive(false);
        Ammunition=Ammunition-1;
        mAnimator.SetInteger("Ammunition", Ammunition);
        beDoingSomethings = false;
    }
    public void endReload()
    {
        mAnimator.SetInteger("Ammunition", 10);
        Ammunition = 10;
        beDoingSomethings = false;
    }
    public void startChangeWeapon()
    {
        if (WeaponIndex == 0)
        {
            THandSword.SetActive(false);
            //mAnimator.transform.FindChild("2Hand-Sword Variant").gameObject.SetActive(false);
        }
        else if (WeaponIndex == 1)
        {
            Gun.SetActive(false);
            //mAnimator.transform.FindChild("2Hand-Rifle").gameObject.SetActive(false);
        }
    }
    public void endChangeWeapon()
    {
        WeaponIndex = (WeaponIndex + 1) % 2;
        mAnimator.SetInteger("WeaponIndex", WeaponIndex);
        if (WeaponIndex == 0)
        {
            THandSword.SetActive(true);
            //mAnimator.transform.FindChild("2Hand-Sword Variant").gameObject.SetActive(true);
        }
        else if (WeaponIndex == 1)
        {
            Gun.SetActive(true);
            //mAnimator.transform.FindChild("2Hand-Rifle").gameObject.SetActive(true);
        }
        beDoingSomethings = false;
    }



    /*public void Hit()
    {
        Debug.Log("Hit");
        beAttacking = false;
    }*/
}
