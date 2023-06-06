using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;
    float S_timer;
    Player player;

    void Awake() {
        // Awake �Լ������� �÷��̾� �ʱ�ȭ�� ���� �Ŵ��� Ȱ������ ����
        player = GameManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            case 1:
            case 6:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire(id);
                }
                break;
            case 5:
                S_timer += Time.deltaTime;
                if (S_timer > speed * 5)
                {
                    S_timer = 0f;
                    sickle();
                }
                break;
        }
        // .. Test Code
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 1);
        }
    }

    public void LevelUp(float damage, int count) {
        this.damage = damage * Character.Damage;
        this.count += count;

        if (id == 0)
            Batch();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data) {
        // ��ũ��Ʈ�� ������Ʈ�� �������� ���ؼ� �ε����� �ƴ� ���������� ����

        //basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;


        //Property Set
        id = data.itemId;
        damage = data.baseDamage * Character.Damage;
        count = data.baseCount + Character.Count;

        for (int index = 0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            if ( data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }


        switch (id) {
            case 0:
                speed = 150 * Character.WeaponSpeed;
                Batch();
                break;
            default:
                speed = 0.5f * Character.WeaponRate;
                break;
        }

        // Hand Set
        if (id != 5)
        {
            Hand hand = player.hands[(int)data.itemType];
            hand.spriter.sprite = data.hand;
            hand.gameObject.SetActive(true);
        }



        //BroadcastMessage : Ư�� �Լ� ȣ���� ��� �ڽĿ��� ����ϴ� �Լ�
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void Batch() {
        for (int index = 0; index < count; index++) {
            Transform bullet;
            
            if (index < transform.childCount) {
                bullet = transform.GetChild(index);
            } else {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero); // -100 �� ���� ����
        }
    }
    void Fire(int id) {
        if (!player.scanner.nearestTarget)
            return;
        // �Ѿ��� ���ư����� �ϴ� ���� ����
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        // ��ġ�� ȸ�� ����
        if (id == 1) // ����
        {
            Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir); // FromToRotation: ������ ���� �߽����� ��ǥ�� ���� ȸ���ϴ� �Լ�
            bullet.GetComponent<Bullet>().Init(damage, count, dir);
        } else if (id == 6) // ��ź��
        {
            Transform[] bullet = new Transform[3];
            for (int i = 0; i < 3; i++)
            {
                Vector3 newDirection = Quaternion.AngleAxis(Random.Range(-20, 20), Vector3.forward) * dir;
                bullet[i] = GameManager.instance.pool.Get(prefabId).transform;
                bullet[i].position = transform.position;
                bullet[i].rotation = Quaternion.FromToRotation(Vector3.up, newDirection); // FromToRotation: ������ ���� �߽����� ��ǥ�� ���� ȸ���ϴ� �Լ�
                bullet[i].GetComponent<Bullet>().Init(damage, count, newDirection);
            }
        }

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }

    void sickle() // �⺻������ fire �Լ��� ���. count �κи� �ٸ��� Bullet�� ����
    {
        if (!player.scanner.nearestTarget)
            return;
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;


        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir); 
        bullet.GetComponent<Bullet>().Init(damage, 100 + count, dir);

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }
}
