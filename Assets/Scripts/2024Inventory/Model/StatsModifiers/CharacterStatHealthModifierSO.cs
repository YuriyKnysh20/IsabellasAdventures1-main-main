using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        Player Playerhealth = character.GetComponent<Player>();
        if (Playerhealth != null)
            Playerhealth.AddHealth((int)val);
    }
}