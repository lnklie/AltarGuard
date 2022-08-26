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

public enum EPlayerState
{
    Play,
    AutoPlay,
    FlagDeployment
}
public enum EAIState
{
    Idle,
    Chase,
    Attack,
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
public enum ECharacter
{
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
public enum EgraceType
{
    Warrior = 0,
    Archer = 1000,
    Wizard = 2000,
    Commander = 3000
}
public enum EItemRank
{
    Common,
    UnCommon,
    Rare,
    Unique
}

public enum EGraceConditionWho
{
    Player,
    Mercenary1,
    Mercenary2,
    Mercenary3,
    Mercenary4,
    AllMercenary,
    All
}
public enum EGraceConditionWhat
{
    Sword,
    OnlySword,
    Spear,
    Exe,
    Shield,
    Bow,
    Wand,
    OnlyWand,
    SpearOrExe,
    SwordAndShield,
    WandAndShiled,
    HpPortion,
    MpPortion,
    Skill
}
public enum EGraceConditionHow
{
    Equip,
    Use,
    SkillUse,
}
public enum EGraceResultWho
{
    Player,
    Mercenary1,
    Mercenary2,
    Mercenary3,
    Mercenary4,
    AllMercenary,
    All
}
public enum EGraceResultTarget
{
    PlayerStr,
    PlayerDex,
    PlayerWiz,
    PlayerLuck,
    PlayerHpRegen,
    PlayerPhysicalDamage,
    PlayerMagicalDamage,
    PlayerDefensivePower,
    PlayerSpeed,
    PlayerAtkSpeed
}
public enum EGraceResultWhat
{
    Str,
    Dex,
    Wiz,
    Luck,
    HpRegen,
    PhysicalDamage,
    MagicalDamage,
    DefensivePower,
    Speed,
    AtkSpeed,
    AtkRange
}
public enum EGraceResultHow
{
    Increase,
    Decrease,
}