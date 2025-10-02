using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "TutorialStep", menuName = "ScriptableObjects/TutorialStep", order = 0)]
public class TutorialStep : ScriptableObject
{
    public int stepNumber;           // The step's number
    public string stepText;          // The description for the step
    public VideoClip stepVideo;      // The video clip associated with the step
    public AudioClip stepAudio;      // The audio clip associated with the step
    public InteractiveStepType interactiveStepType;

    // Success and failure feedback
    public string successText;       // Text displayed on successful completion of the step
    public string failureText;       // Text displayed on failure of the step
    public AudioClip successAudio;   // Audio clip played on successful completion
    public AudioClip failureAudio;   // Audio clip played on failure

}

public enum InteractiveStepType
{
    Normal,        // For automated steps
    Power,
    Throttle,
    Yaw,
    Pitch,
    Roll,
    Cam
}
