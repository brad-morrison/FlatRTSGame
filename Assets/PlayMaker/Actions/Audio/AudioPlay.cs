// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
    [ActionTarget(typeof(AudioSource), "gameObject")]
    [ActionTarget(typeof(AudioClip), "oneShotClip")]
	[Tooltip("Plays the Audio Clip set with Set Audio Clip or in the Audio Source inspector on a Game Object. Optionally plays a one shot Audio Clip.")]
	public class AudioPlay : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		[Tooltip("The GameObject with an AudioSource component.")]
		public FsmOwnerDefault gameObject;
		
		[HasFloatSlider(0,1)]
        [Tooltip("Volume to play the sound at. Can be modified with {{Set Audio Volume}}.")]
		public FsmFloat volume;
		
		[ObjectType(typeof(AudioClip))]
		[Tooltip("Optionally play a 'one shot' AudioClip. NOTE: Volume cannot be adjusted while playing a 'one shot' AudioClip.")]
		public FsmObject oneShotClip;

        [Tooltip("Wait until the end of the clip to send the Finish Event. Set to false to send the finish event immediately.")]
        public FsmBool WaitForEndOfClip;

        [Tooltip("Send this event when the sound is finished playing. NOTE: currently also sent when the sound is paused...")]
		public FsmEvent finishedEvent;

		private AudioSource audio;
				
		public override void Reset()
		{
			gameObject = null;
			volume = 1f;
			oneShotClip = null;
		    finishedEvent = null;
            WaitForEndOfClip = true;
        }

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				// cache the AudioSource component

			    audio = go.GetComponent<AudioSource>();
				if (audio != null)
				{
					var audioClip = oneShotClip.Value as AudioClip;

					if (audioClip == null)
					{
						audio.Play();
						
						if (!volume.IsNone)
						{
							audio.volume = volume.Value;
						}
						
						if (WaitForEndOfClip.Value == false)
						{
							Fsm.Event(finishedEvent);
							Finish();
						}
						
						return;
					}
					
					if (!volume.IsNone)
					{
						audio.PlayOneShot(audioClip, volume.Value);
					}
					else
					{
						audio.PlayOneShot(audioClip);
					}
                    if (WaitForEndOfClip.Value == false)
                    {
                        Fsm.Event(finishedEvent);
                        Finish();
                    }

                    return;
				}
			}
			
			// Finish if failed to play sound	
		
			Finish();
		}
		
		public override void OnUpdate ()
		{
			if (audio == null)
			{
				Finish();
			}
			else
			{
				if (!audio.isPlaying)
				{
					Fsm.Event(finishedEvent);
					Finish();
				}
                else if (!volume.IsNone && volume.Value != audio.volume)
				{
					audio.volume = volume.Value;
				}
			}
		}

#if UNITY_EDITOR
	    public override string AutoName()
	    {
	        if (oneShotClip.Value != null && !oneShotClip.IsNone)
	        {
	            return ActionHelpers.AutoName(this, oneShotClip);
	        }

	        return null;
	    }
#endif
	}
}