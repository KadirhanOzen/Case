using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    [SerializeField]
    List<Transform> spawnPointList = new List<Transform>();
    public float reloudTime = 2;
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    List<Transform> targetList = new List<Transform>();

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    Transform weaponTransform;
    private float closestEnemyDistance;
    private float nextFireTime;
    public float damage = 3;

    public bool isActiveDiagonal = false;

    public bool isActiveLethal;

    public int bulletCount = 1;

    float lethalTempoCoolDown = 5;

    public float tempReloudTime;

    

    Coroutine fireCoroutine;

    Quaternion rotation;

    Vector3 direction;





    private void OnEnable()
    {
        GameEvents.instance.OnEnemyEntered += EnteredEnemy;
        GameEvents.instance.OnEnemyDestroy += RemoveEnemy;
        GameEvents.instance.OnFinishGame += FinishGame;
    }

    private void OnDisable()
    {
        GameEvents.instance.OnEnemyEntered -= EnteredEnemy;
        GameEvents.instance.OnEnemyDestroy -= RemoveEnemy;
        GameEvents.instance.OnFinishGame -= FinishGame;
    }
    private void Start()
    {
        Fire();

        
        

        
        


    }


    void EnteredEnemy(Transform target, bool isIn)
    {

        if (isIn)
        {
            targetList.Add(target);
        }
        else
        {
            targetList.Remove(target);
        }
    }
    private void RemoveEnemy(Transform target)
    {
        targetList.Remove(target);
    }
    private void FinishGame()
    {
        PlayerPrefs.SetFloat("Damage", damage);
        if (isActiveLethal)
            PlayerPrefs.SetFloat("AttackSpeed", lethalTempoCoolDown);
        else
        {
            PlayerPrefs.SetFloat("AttackSpeed", reloudTime);
        }

    }

    public Vector2 ClosestEnemy()
    {
        Vector2 closestEnemy = transform.right;



        if (targetList.Count > 0)
        {

            closestEnemy = targetList[0].position;

            closestEnemyDistance = Vector3.Distance(transform.position, targetList[0].position);


            for (int i = 0; i < targetList.Count; i++)
            {
                if (closestEnemyDistance > Vector3.Distance(transform.position, targetList[i].position))
                {
                    closestEnemyDistance = Vector3.Distance(transform.position, targetList[i].position);

                    closestEnemy = targetList[i].position;
                }

            }

        }
        return closestEnemy;

    }




    private void Fire()
    {

        Vector3 targetPoint = ClosestEnemy();


        direction = (targetPoint - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject newBullet = Instantiate(bulletPrefab, spawnPointList[i].position, rotation);

            newBullet.GetComponent<Bullet>().damage = damage;

            if (isActiveDiagonal == true)
            {
                Quaternion rotation1 = Quaternion.Euler(newBullet.transform.eulerAngles.x, newBullet.transform.eulerAngles.y, newBullet.transform.eulerAngles.z + 30);
                GameObject newBulletDia1 = Instantiate(newBullet, newBullet.transform.position, rotation1);
                Destroy(newBulletDia1, 4f);
                Quaternion rotation2 = Quaternion.Euler(newBullet.transform.eulerAngles.x, newBullet.transform.eulerAngles.y, newBullet.transform.eulerAngles.z - 30);
                GameObject newBulletDia2 = Instantiate(newBullet, newBullet.transform.position, rotation2);
                Destroy(newBulletDia2, 4f);
            }


            Destroy(newBullet, 4f);

            if (targetPoint != null)
            {



                newBullet.GetComponent<Bullet>().SetUp(direction);

            }


        }
        fireCoroutine = StartCoroutine(FireTimer());



    }



    IEnumerator FireTimer()
    {

        yield return new WaitForSeconds(reloudTime);
        Fire();
        audioSource.Play();
    }

    public IEnumerator SkillFireTimer()
    {
        StopCoroutine(fireCoroutine);

        isActiveLethal = true;

        tempReloudTime = reloudTime;

        reloudTime = 0.1f;

        StartCoroutine(FireTimer());

        yield return new WaitForSeconds(lethalTempoCoolDown);

        reloudTime = tempReloudTime;


        isActiveLethal = false;
    }
}
