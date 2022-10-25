using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESound
{
    BackGround,
    Effect
}
public enum ESleepModeTime
{
    TurnOff,
    FiveMin,
    TenMin,
    ThirtyMin,
    SixtyMin
}
public enum EAllyTargetingSetUp
{
     OneSelf,
     CloseAlly,
     Random,
     WeakAlly
}
public enum ELayerName
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
    UseSkill,
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
    SubWeapon,
    Weapon,
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
public enum EStatus
{
    Str,
    Dex,
    Wiz,
    Luck,
    MaxHp,
    MaxMp,
    HpRegenValue,
    PhysicalDamage,
    MagicalDamage,
    DefensivePower,
    Speed,
    AttackSpeed,
    CastingSpeed,
    AtkRange,
    DropProbability,
    ItemRarity
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
    Sword = 1000,
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
    Equip = 2000,
    Use,
    SkillUse,
}
public enum EGraceResultWho
{
    Player = 3000,
    Mercenary1,
    Mercenary2,
    Mercenary3,
    Mercenary4,
    AllMercenary,
    All
}

public enum EGraceResultWhat
{
    Str = 4000,
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
    Increase = 6000,
    Decrease,
}
public enum ERelationOfGraces
{
    Independence = 7000,
    Subordination
}