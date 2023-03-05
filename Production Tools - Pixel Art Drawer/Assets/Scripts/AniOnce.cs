using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniOnce : MonoBehaviour
{
    public GameObject myObject;
    public AnimationClip myAnimation;

    private Animation animationComponent;

    private void Start()
    {
        // Get the animation component on the GameObject
        animationComponent = myObject.GetComponent<Animation>();

        // Add the animation clip to the animation component
        animationComponent.AddClip(myAnimation, myAnimation.name);
    }

    public void PlayAnimationOnce()
    {
        // Play the animation once
        animationComponent.Play(myAnimation.name);
    }

}
