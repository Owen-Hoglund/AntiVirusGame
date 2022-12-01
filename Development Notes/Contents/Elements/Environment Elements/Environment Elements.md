# Table of Contents
---
1. [[Structural Environmental Elements]]
2. 

# Notes
---
**What do we consider an environmental element, and why?**
Environment elements consist of any element that will physically manifest itself in the game scene. More often than not this division will be along lines of visibility - if you can see the element, it is an environment element. If you cant see it, it probably isn't an environtmental element. There are of course going to be a few exceptions to this rule. For example, surely the NPCs in the game physically manifest themselves in the game scene, and surely the player character itsself does the same. However, it doesn't seem right to consider yourself part of the environment. *You* move through the environment, as do NPCs. 

Anything can be an environmental element if you choose the right perspective, but for our purposes, we will try to keep a general mindset that the environment is composed of the inanimate and simple elements that make up the game scene. Simple of course is a hard thing to define, after all game development is never really simple, but our intuition for what simple means I believe will feel fairly natural once everything is partitioned into its respective categories.

As for the *why* we categorize things this way, the answer is as simple as it is pointless. Because it makes it easier to find things, and it makes it easier to think about the architecture of our design. The game does not care if you put a wall in the same folder as you put a script that tracks how many points you have scored, but *we* do. And because the game doesn't care how we separate these things, we have to make some arbitrary decisions about how we are going to split things up. 

**Environmental Elements can contain non-environmental elements as child elements.**
Take for example a door. Surely it is an environmental element, it is visible, it is a physical object in the scene, it simply *must be* a part of the environment. But from the perspective of game design, it also has something else - logic. *How does the door open? Who can open it? Is it locked? How quickly does it open?* These things cannot be described using only cubes. And surely we are in agreement that *logic* is not part of our environment. Thus we can include some script that is not part of our environment, but this script will not live in the environment folder, it lives in the script folder with the rest of the scripts. 
For more minor elements, they may exist solely as an extension of our physical object. I wont labor this point too much but continuing with the example of our door, surely you must be within a certain range of a door to make it open or close, and this range is intrinsic to the door. It simply doesnt *belong* anywhere but attached to the door. Not a standalone element even worth having a section for.
In general, when we get to elements this small, where it is highly highly unlikely we will need to reference a specific element again, we likely wont even write about it in this document, it will either be so obvious it needs no description, or a short sentence in the description of its parent will be sufficient. 



