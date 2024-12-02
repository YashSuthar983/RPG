using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    [CreateAssetMenu(menuName ="Spells/HealingSpells")]

    public class HealingSpell : SpellItem
    {
        public int healAmount;


        public override void AttemptToCastSpell(AnimatorHandler animatorHandler,PlayerStats playerStats, WeaponSlotManager weaponSlotManager)
        {
            base.AttemptToCastSpell(animatorHandler, playerStats,weaponSlotManager);
            GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX,animatorHandler.transform);   
            animatorHandler.PlayerTargetAnimation(spellAnim,true);
            Debug.Log("attemp to cast spell");
        
        
        }

        public override void SuccessfullyCastSpell(AnimatorHandler animatorHandler, PlayerStats playerStats, CameraHandler cameraHandler,
            WeaponSlotManager weaponSlotManager)
        {
            base.SuccessfullyCastSpell(animatorHandler, playerStats,cameraHandler,weaponSlotManager);
            GameObject instantiatSpellFX=Instantiate(spellCastFX,animatorHandler.transform);
            playerStats.HealPlayer(healAmount);
            
            Debug.Log("spell caset successfully");



        }

    }

}