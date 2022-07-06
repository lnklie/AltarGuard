using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIController
{
    public void ChangeState(CharacterStatus _status);
    public void State(CharacterStatus _status);
    public void Perception(CharacterStatus _status);
    public void Idle(CharacterStatus _status);
    public void Chase(CharacterStatus _status);
    public void Attack(CharacterStatus _status);
    public IEnumerator Died(CharacterStatus _status);
    public void Damaged(CharacterStatus _status);
    public bool IsDied(CharacterStatus _status);


}
