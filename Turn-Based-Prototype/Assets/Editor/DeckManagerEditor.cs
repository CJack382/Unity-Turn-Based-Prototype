using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(DeckManager))]
/*Not fully sure of the implications of this quite yet, however using this method, this script essentially just creates a button for the DeckManager script which allows you to manually draw a card (**In the Editor Only**) 
 So I guess creating the custom editor allows you to add functionalities to the inspector for designated items. This could be helpful for speeding up processes during development, especially if you are making functions which
should not make it into the final version, such as a draw on command button*/
public class DeckManagerEditor : Editor //Class to derive from if you're trying to create custom inspectors
{
    public override void OnInspectorGUI() //Overrides the GUI for the inspector to add personal additions
    {
        DrawDefaultInspector(); //Creates standard inspects, may as well be base.OnInspectorGUI(), but what do I know?

        DeckManager deckManager = (DeckManager)target;
        if (GUILayout.Button("Draw Next Card")) //Creates a button by the name of draw next card in the inspector of deckmanager, when clicked it performs the function within the if statement
        {
            HandManager handManager = FindAnyObjectByType<HandManager>(); //Vid uses FindObjectOfType, deprecated, I used any object since there ought to only be 1 in scene
            if (handManager != null )
            {
                deckManager.DrawCard(handManager);
            }
        }
    }
}
#endif