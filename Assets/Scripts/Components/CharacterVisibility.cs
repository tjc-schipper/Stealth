using UnityEngine;
using System.Collections;

public class CharacterVisibility : MonoBehaviour {

    public enum VisibilityStates
    {
        VISIBLE,    // Draw the character model, direct line of sight
        LIMBO,      // Direct line of sight but dark or partially hidden
        INDIRECT,   // Team has line of sight or revealed by ability, no line of sight. Blip+Vizcone
        GHOST,      // Spotted by ability or trigger, no line of sight. Grey blip only (externally handled!)
        HIDDEN      // Not visible to player. Same as Ghost, but means no ghosts present.
    }
    public VisibilityStates visibilityState;

	//TODO: Some events here, make other components hook into those. Interface this class with LightMesh events and stuff to change state.
}
