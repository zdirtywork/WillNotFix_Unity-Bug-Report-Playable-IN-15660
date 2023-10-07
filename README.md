# Unity-Bug-Report-Playable-IN-15660

**Unity has stated that they will not fix this bug.**

> RESOLUTION NOTE:
Thank you for bringing this issue to our attention. Unfortunately, after careful consideration we will not be addressing your issue at this time, as we are currently committed to resolving other higher-priority issues, as well as delivering the new animation system. Our priority levels are determined by factors such as the severity and frequency of an issue and the number of users affected by it. However we know each case is different, so please continue to log any issues you find, as well as provide any general feedback on our roadmap page to help us prioritize.

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
