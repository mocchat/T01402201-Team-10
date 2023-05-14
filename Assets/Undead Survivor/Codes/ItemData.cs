using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//CreateAssetMenu : 커스텀 메뉴를 직접 생성하는 속성
[CreateAssetMenu(fileName = "Item", menuName = "Scriptble object/ItemData")]  //Scriptble Object : 다양한 데이터를 저장하는 에셋(Data폴더에 있는 오브젝트)
public class ItemData : ScriptableObject
{
    public enum ItemType {  Melee, Range, Glove, Shoe, Heal}

    // 아이템의 각종 속성들을 변수로 작성하기
    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    [TextArea]
    public string itemDesc;
    public Sprite itemIcon;


    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;


    [Header("# Weapon")]
    public GameObject projectile;
    public Sprite hand;


}


