using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{

    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        //Basic Set
        name = "Gear " + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        //Preoperty Set
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();   //��� ���Ӱ� �߰��ǰų� ������ �� �� �������� �Լ��� ȣ��
    }


    //Ÿ�Կ� ���� �����ϰ� ������ ��������ִ� �Լ� �߰�
    //ApplyGear�� ����Ǵ� ���
    //1. Weapon�� ���� �����Ǿ��� ��
    //2. Weapon�� ���׷��̵� �Ǿ��� ��
    //3. Gear�� ���� ������ ��
    //4. Gear��ü�� ������ �Ǿ��� ��
    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }


    //�尩�� ����� ������� �ø��� �Լ�
    public void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach(Weapon weapon in weapons)
        {
            switch(weapon.id)
            {
                case 0:
                    weapon.speed = 150 + (150 * rate);
                    break;
                default:
                    weapon.speed = 0.5f * (1f - rate);
                    break;
            }
        }
    }

    void SpeedUp()
    {
        float speed = 3;
        GameManager.instance.player.speed = speed + speed * rate;
    }


}
