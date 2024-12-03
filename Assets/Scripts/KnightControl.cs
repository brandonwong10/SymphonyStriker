using UnityEngine;
using System.Collections;
using Spine;
using Spine.Unity;

public class KnightControl : MonoBehaviour
{
    #region Inspector
    // [SpineAnimation] attribute allows an Inspector dropdown of Spine animation names coming form SkeletonAnimation.
    [SpineAnimation]
    public string runAnimationName;

    [SpineAnimation]
    public string idleAnimationName;

    [SpineAnimation]
    public string walkAnimationName;

    [SpineAnimation]
    public string atkAnimationName_1;

    [SpineAnimation]
    public string atkAnimationName_2;

    [SpineAnimation]
    public string jumpAnimationName;

    [SpineAnimation]
    public string hitAnimationName;

    [SpineAnimation]
    public string deathAnimationName;

    [SpineAnimation]
    public string stunAnimationName;

    [SpineAnimation]
    public string skillAnimationName_1;
    [SpineAnimation]
    public string skillAnimationName_2;
    [SpineAnimation]
    public string skillAnimationName_3;

    #endregion

    SkeletonAnimation skeletonAnimation;


    // Spine.AnimationState and Spine.Skeleton are not Unity-serialized objects. You will not see them as fields in the inspector.
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;
    AudioManager audioManager;
    // Start is called before the first frame update
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
    }

    // void Update()
    // {
    // if (Input.GetKeyDown(KeyCode.A))
    // {
    //     Debug.Log("A key pressed");
    //     attack_1();
    // }
    // }   


    public void running()
    {
        spineAnimationState.SetAnimation(0, runAnimationName, true);
    }
    public void walking()
    {
        spineAnimationState.SetAnimation(0, walkAnimationName, true);
    }
    public void idle()
    {
        spineAnimationState.SetAnimation(0, idleAnimationName, true);
    }
    public void jump()
    {
        spineAnimationState.SetAnimation(0, jumpAnimationName, true);
    }
    public void getHit()
    {
        TrackEntry trackEntry = spineAnimationState.SetAnimation(0, hitAnimationName, false);
        // Add a listener to switch back to idle when the attack animation completes
        trackEntry.Complete += entry =>
        {
            idle(); // Set to idle animation after attack completes
        };
    }
    public void death()
    {   
        spineAnimationState.SetAnimation(0, deathAnimationName, false);
        //audioManager.PlaySFX(audioManager.death);
    }
    public void stun()
    {
        spineAnimationState.SetAnimation(0, stunAnimationName, true);
    }


    public void attack_1()
    {
        
        // Play the attack animation once
        TrackEntry trackEntry = spineAnimationState.SetAnimation(0, atkAnimationName_1, false);

        audioManager.PlaySFX(audioManager.swordAttack);

        // Add a listener to switch back to idle when the attack animation completes
        trackEntry.Complete += entry =>
        {
            idle(); // Set to idle animation after attack completes
        };
    }

    public void attack_2()
    {
        
        // Play the attack animation once
        TrackEntry trackEntry = spineAnimationState.SetAnimation(0, atkAnimationName_2, false);
        audioManager.PlaySFX(audioManager.shieldAttack);

        // Add a listener to switch back to idle when the attack animation completes
        trackEntry.Complete += entry =>
        {
            idle(); // Set to idle animation after attack completes
        };
    }
    public void skill_1()
    {
        spineAnimationState.SetAnimation(0, skillAnimationName_1, true);
    }
    public void skill_2()
    {
        spineAnimationState.SetAnimation(0, skillAnimationName_2, true);
    }
    public void skill_3()
    {
        spineAnimationState.SetAnimation(0, skillAnimationName_3, true);
    }

}
