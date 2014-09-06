using UnityEngine;
using System.Collections;

public class SpyPostures : MonoBehaviour
{

    public SpyState state;
    public SpyMovement movement;
    public event StealthEventTypes.BoolEvent OnPlayerCrouched;

    private Color DEBUG_STANDING_COLOR = Color.grey;
    private Color DEBUG_CROUCH_COLOR = Color.red;

    public bool Crouched
    {
        get { return crouched; }
        set
        {
            if (crouched != value)
            {
                crouched = value;
                #region DEBUG
                Color c = (Crouched) ? DEBUG_CROUCH_COLOR : DEBUG_STANDING_COLOR;
                GetComponentInChildren<Renderer>().material.color = c;
                #endregion
                if (OnPlayerCrouched != null)
                    OnPlayerCrouched(Crouched);    // Inversion because 
            }
        }
    }
    private bool crouched;
    public bool Covering { get; set; }

    void Awake()
    {
        state = GetComponent<SpyState>();
        movement = GetComponent<Movement>() as SpyMovement;
    }

    void Update()
    {
        TakeInput();
    }

    void TakeInput()
    {
        if (Input.GetButtonDown("Crouch"))
            Crouched = !Crouched;
    }

    #region Postures
    // I'm not sure if I should keep this. So complicated... ugh
    public enum Postures
    {
        STAND,
        CROUCH,
        COVER,
        COVERCROUCH
    }
    public Postures Posture = Postures.STAND;

    public static string PostureToString(Postures p)
    {
        string s = "";
        switch (p)
        {
            case Postures.COVER:
                s = "cover";
                break;
            case Postures.COVERCROUCH:
                s = "covercrouch";
                break;
            case Postures.CROUCH:
                s = "crouch";
                break;
            case Postures.STAND:
                s = "stand";
                break;
        }
        return s;
    }
    public static Postures StringToPosture(string s)
    {
        Postures p;
        if (!string.IsNullOrEmpty(s))
        {
            switch (s.ToLower())
            {
                case "stand":
                    p = Postures.STAND;
                    break;
                case "crouch":
                    p = Postures.CROUCH;
                    break;
                case "cover":
                    p = Postures.COVER;
                    break;
                case "covercrouch":
                    p = Postures.COVERCROUCH;
                    break;
                default:
                    p = Postures.STAND;
                    break;
            }
        }
        else
        {
            Debug.LogError("Empty string passed to StringToPosture()!");
            p = Postures.STAND;
        }

        return p;
    }
    #endregion
}
