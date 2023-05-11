# Unity-Bug-Report-Playable-IN-15660

## About this issue

Paused Playable starts playing when a GameObject's visible state is changed and Animator::SyncPlayStateToCulling() is called.

## How to reproduce

1. Open the "SampleScene".
2. Enter play mode, and you will see the character walking in place in the Game view.
3. Click the "Toggle Playable" button in the Game view, and you will see the character's animation paused.
4. Click the "Toggle Camera" button in the Game view, and you will see the character disappear.
    - Ensure that the character is not being rendered at this moment, whether it is through the Game view, Scene view, or any other form.
5. Click the "Toggle Camera" button again, and you will see the character reappear, but the character's animation **unexpectedly** resumes playing.

## Solution

Use `SetSpeed(0)` instead of `Pause()`.
