# Table of Contents
---
1. [[Environment Elements]]
2. [[Utility Elements]]
3. [[Playable Characters]]
4. [[NPCs]]
5. [[Scripts]]
6. [[Materials]]

# Design Paradigm
---
## Design Axioms and Guidelines
---
One of the most important decisions one will make when designing a game is what should and should not be turned into a prefab. While dull on its face, a lot of nuance goes into finding an answer. I do not claim to be a master designer, but as of now, and as far as I can tell, these axioms and suggestions are extremely helpful and pragmatic. They will, at the very least, save one a lot of time spent fiddling with the scene view trying to make things line up, and at best, empower you to spend much more time creating, and a lot less time redesigning things that you already designed.


1. **Prefab instances can always be tweaked in the scene** - We should strive whenever possible to not be making bespoke elements, however, realistically this will not always be possible. We obviously don't want our game to be a neverending series of identical hallways and rooms, this is boring and lazy design. That being said, if one can build templates for scenes very quickly, one will have all that extra time to turn that blank canvas into an interesting scene.
2. **Elements in the scene can always be saved as prefabs** - When you are in your scene designing something, and you think to yourself "wow, this is really cool, I should reuse this," you *should* reuse it. Stop what you're doing, save that design as a prefab, and complete its design in the prefab. Keep it basic, remember rule 1. 
3. **The Symmetry^2 suggestion** - When we are deciding whether an object should be made into a prefab or not, the first thing we should ask ourselves is "will I use this again?". If the answer is yes, we should not immediately jump to the conclusion that we should turn something into a prefab. 
4. 


## Design Notes
---
**What is an element?**
For the purposes of organization, every single thing that is in a game will be considered an element, with one exception - Scenes. In every other case if it exists within the game and we made it, we call it an element.

**There is no perfect hierarchy in our organization**
While we try to separate elements based on something innate to themselves, or by some larger category that any given element belongs to, invariably we cannot have a perfect hierarchy. For example, a script is its own unique kind of element, all scripts will be in the scripts folder, but many elements use scripts. For example, the script dictating how a door (environmental element) works will be used in the environmental elements folders, but this does not mean a script is a kind of environmental element. There will be a lot of borrowing of elements. This is only a way of separating them in a more readable and usable format.

**Scripts *barely* belong here**
In an ideal world, code should be documented well enough that having a separate documentation page for them should be unnecessary. However, for the sake of faster design and reference, we will have very small descriptions of what each script does here. These descriptions will be extremely limited. They may also contain short notes on work that should yet be done to them, but all such notes should be considered to be temporary. These notes should eventually be converted into another short description of what it does. The *How* of each script should be explained in the code file. This document is not a code guide, it is a guide to the workings of the game. 


