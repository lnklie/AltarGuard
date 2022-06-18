using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : EnumTypeManager.cs
==============================
*/
public enum LayerName
{
    IdleLayer = 0,
    WalkLayer = 1,
    AttackLayer = 2,
    DieLayer = 3
}
public enum CharacterState
{
    Idle,
    Walk,
    Attack,
    Damaged,
    Died
}
public enum ItemType
{
    Hair = 0,
    FaceHair,
    Cloth,
    Pant,
    Helmet,
    Armor,
    Back,
    Weapon,
    SubWeapon,
    Consumables,
    Miscellaneous
}
public enum Debuff
{
    Not = 0,
    Sturn = 1,
    Slowed = 2
}
public enum EnemyState
{
    Idle,
    Chase,
    Attack,
    Damaged,
    Died
}
public enum BossState
{
    Idle,
    Chase,
    Wait,
    PatternAttack,
    Died
}
public enum EnemyType
{
    SwordOrc,
    BowOrc,
    WizardOrc
}
public enum BossEnemyType
{
    KingSlime
}
public enum AltarState
{
    Idle,
    Damaged,
    Destroyed
}
public enum AltarAbility
{
    Hp,
    DefensivePower,
    BuffRange,
    Buff_Damage,
    Buff_DefensivePower,
    Buff_Speed,
    Buff_Healing
}
public enum ItemEquipedCharacter
{
    NULL,
    Player,
    Mercenary_A,
    Mercenary_B,
    Mercenary_C,
    Mercenary_D
}
public enum SlimeKingPattern
{
    NotPattern,
    JumpAttack,
    DashAttack,
    JumpJump
}
