using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    
    [SerializeField] private float timeBtwAttacks;
    private List<AttackTag> attacksLists;
    private Animator anim;
    

    private void Awake() {
        attacksLists = new List<AttackTag>(GetComponentsInChildren<AttackTag>(true));
        anim = GetComponent<Animator>();
    }

    private void Start() {
        InvokeRepeating("InvokeAttack", timeBtwAttacks, timeBtwAttacks);
    }


    private void InvokeAttack()
    {
        
        int rand = Random.Range(0,anim.parameterCount);
        print("Attack"+rand);
        anim.SetTrigger("Attack"+rand);

    }


    //function that is called by the animation events
    public void ActivateAttack(string tag){
        if(tag != null)
        {
            bool attackIsListed = false;
            if(tag[0] != '#')
            {
                tag = '#' + tag;
            }
            
            foreach (AttackTag attack in attacksLists)
            {
                if(attack.tag == tag)
                {
                    attackIsListed = true;
                    if(!attack.gameObject.activeInHierarchy)
                        attack.gameObject.SetActive(true);
                    else
                    {
                        attack.gameObject.SetActive(false);
                        attack.gameObject.SetActive(true);
                    }
                    break;
                }
            }

            if(!attackIsListed)
            {
                Debug.LogError("Tag "+ tag +" insirida não está listada no AttackManager do gameObject " + gameObject.name);
            }
        }
        else
        {
            Debug.LogWarning("tag não foi inserida no animation event");
        }
        //attack.SetActive(true);
    }
}
