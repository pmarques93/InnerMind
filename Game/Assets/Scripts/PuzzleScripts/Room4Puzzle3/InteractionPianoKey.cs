﻿using UnityEngine;
using System;

/// <summary>
/// Class used when interacting with piano keys.
/// </summary>
public class InteractionPianoKey : InteractionCommon
{
    //Inspector Variables
    [SerializeField] private Animator keyAnimator;
    [SerializeField] private PianoKeyID keyID;

    //Variable used to see if the player can play a key
    public bool CanPlay { get; set; }
    public Animator KeyAnimator => keyAnimator;
    private Inventory inventory;
    private ItemComparer itemComparer;

    /// <summary>
    /// Awake method for InteractionPianoKey.
    /// </summary>
    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        itemComparer = FindObjectOfType<ItemComparer>();
        
        if (inventory.Bag.Contains(itemComparer.NotRewoundAudioTape))
            gameObject.GetComponent<BoxCollider>().enabled = false;        
    }

    /// <summary>
    /// Start method for InteractionPianoKey.
    /// </summary>
    private void Start()
    {
        CanPlay = true;
    }

    /// <summary>
    /// This method determines the action of the piano key when clicked.
    /// </summary>
    public override void Execute()
    {
        if (CanPlay)
        {
            keyAnimator.SetTrigger("playKey");
            OnKeyID(keyID);

            switch (keyID)
            {
                case PianoKeyID.Key1:
                    SoundManager.PlaySound(SoundClip.MajorAKeyNote);
                    break;
                case PianoKeyID.Key2:
                    SoundManager.PlaySound(SoundClip.MajorCKeyNote);
                    break;
                case PianoKeyID.Key3:
                    SoundManager.PlaySound(SoundClip.MajorEKeyNote);
                    break;
            }

            CanPlay = false;
        }
    }

    /// <summary>
    /// Method that invokes KeyID event.
    /// </summary>
    /// <param name="keyID">ID of the piano key.</param>
    protected virtual void OnKeyID(PianoKeyID keyID) => KeyID?.Invoke(keyID);

    /// <summary>
    /// KeyID event with played key's ID
    /// </summary>
    public event Action<PianoKeyID> KeyID;

    /// <summary>
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of this object
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString() => "Play Piano Key";



}
