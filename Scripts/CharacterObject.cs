using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterObject : MonoBehaviour
{
    [SerializeField] private CharacterController characterController = null;
    [SerializeField] private CharacterStatus characterStatus = null;
    [SerializeField] private EquipmentController equipmentController = null;
    [SerializeField] private SkillController skillController = null;
}
