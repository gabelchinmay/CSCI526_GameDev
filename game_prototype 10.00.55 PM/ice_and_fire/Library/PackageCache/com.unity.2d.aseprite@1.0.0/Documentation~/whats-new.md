# What's new in version 1.0.0

- Added a new event to the Aseprite Importer which is fired at the last import process step.
- Made the Aseprite file property publicly available.
- Made the Aseprite file parsing API publicly available.
- Added a property to set the padding within each generated SpriteRect.
- Added an option to select import mode for the file, either Animated Sprite or Sprite Sheet.
- Added ability to generate Animation Events from Cell user data.
- Added ability to export Animator Controller and/or Animation Clips.
- Added support for individual frame timings in animation clips.
- Added support for layer groups.
- Added support for Layer & Cel opacity.
- Added support for repeating/non repeating tags/Animation Clips.
- Layer blend modes are now supported with Import Mode: Merge Frames.
- Sped up the importation of Aseprite files by bursting the image processing tasks. Note: Only for Unity 2022.2 and newer.
- Simplified the generated model prefab when only one Sprite Renderer is present.
- A Sorting Group component is added by default to Model Prefabs with more than one Sprite Renderer.