
## High Importance
- [x] Seems that movement is baked into framerate right now, this needs to be changed asap
- [x] Ensure that Guard movement, door movement, bridge movement are all framerate independent
	- [x] guard movement
	- [x] door movement
	- [x] bridge movement
- [ ] Guard 
	- [ ] player detection
	- [ ] player following
	- [ ] return to path algorithm implementation
- [ ] Player Movement
	- [ ] Move away from hovering
	- [ ] Add jump capability
- [ ] Optimize rendering
	- [ ] Deactivate anything that isnt close
	- [x] Change DoorPort logic to activate/deactivate as you move through them easy fix #feature 
	- [ ] Lighting
		- [ ] Lightmapping
		- [ ] Baked in lights?
- [ ] Learn about Async await task etc
- [ ] Ensure all physics are happening in FixedUpdate

## Low Importance
- [ ] Remove sphere reticle and replace it with an actual reticle
	- [ ] Figure out HUD design 
- [ ] Pick up gun scene
	- [ ] Equipping gun animation?
- [ ] [[Laser Turret]] 
- [ ] Walls look like shit
- [ ] Fix ugly door logic
	- [ ] Best Solution is likely to take a lesson from the bridge and have it shrink upward, that way we dont have to have an offset for the door to sink into
- [ ] Make a new computer interface in Blender
	- [ ] Give it some lights to cycle through
- [ ] Give the stun gun permanent destruction capability
- [ ] Make Spotlight in blender
- [ ] Make the camera zoom to target on body switch
- [ ] Ensure Async Tasks are cancelled on Quit

## Future Maybes
- [ ] Animations
	- [ ] Gun Equip Animation?
	- [ ] Laser firing animation
	- [ ] Laser animation
	- [ ] [[Laser Turret]] animation?