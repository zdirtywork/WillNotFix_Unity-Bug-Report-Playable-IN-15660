using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.UI;

// About this issue:
// 
// Paused Playable starts playing when a GameObject's visible state is changed and Animator::SyncPlayStateToCulling() is called.
// 
// How to reproduce:
// 1. Open the "SampleScene".
// 2. Enter play mode, and you will see the character walking in place in the Game view.
// 3. Click the "Toggle Playable" button in the Game view, and you will see the character's animation paused.
// 4. Click the "Toggle Camera" button in the Game view, and you will see the character disappear.
//     - Ensure that the character is not being rendered at this moment, whether it is through the Game view, Scene view, or any other form.
// 5. Click the "Toggle Camera" button again, and you will see the character reappear, but the character's animation **unexpectedly** resumes playing.

[RequireComponent(typeof(Animator))]
public class AnimPlayablePauseTest : MonoBehaviour
{
    const string CharacterLayerName = "Character";

    public Camera cam;
    public Text camText;
    public Text playableText;
    public AnimationClip clip;

    private int _characterLayerIndex;
    private Animator _animator;
    private PlayableGraph _graph;
    private Playable _clipPlayable;

    private void Awake()
    {
#if UNITY_EDITOR
        // Make sure the Scene view camera is not rendering the character
        var sceneViews = UnityEditor.SceneView.sceneViews.ToArray();
        foreach (var sceneView in sceneViews)
        {
            ((UnityEditor.SceneView)sceneView).Close();
        }
#endif

        _characterLayerIndex = LayerMask.NameToLayer(CharacterLayerName);
        cam.cullingMask |= 1 << _characterLayerIndex;

        _animator = GetComponent<Animator>();
        _graph = PlayableGraph.Create("Anim Playable Pause Test");
        _graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

        // _clipPlayable -> output
        _clipPlayable = AnimationClipPlayable.Create(_graph, clip);
        var output = AnimationPlayableOutput.Create(_graph, "Anim Output", _animator);
        output.SetSourcePlayable(_clipPlayable);

        _graph.Play();
    }

    private void LateUpdate()
    {
        playableText.text = $"PlayState: {_clipPlayable.GetPlayState()}";

        if ((cam.cullingMask & (1 << _characterLayerIndex)) == 0)
        {
            camText.text = "Character Culled";
        }
        else
        {
            camText.text = "Character Rendered";
        }
    }

    private void OnDestroy()
    {
        _graph.Destroy();
    }

    public void SwitchPlayable()
    {
        if (_clipPlayable.GetPlayState() == PlayState.Playing)
        {
            _clipPlayable.Pause();
        }
        else
        {
            _clipPlayable.Play();
        }
    }

    public void SwitchCam()
    {
        if ((cam.cullingMask & (1 << _characterLayerIndex)) != 0)
        {
            cam.cullingMask &= ~(1 << _characterLayerIndex);
        }
        else
        {
            cam.cullingMask |= 1 << _characterLayerIndex;
        }
    }
}