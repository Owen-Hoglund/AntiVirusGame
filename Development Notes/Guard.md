![[Pasted image 20221205143634.png]]
# Gaurd
The Guard is the most basic NPC in the game. Its basic purpose is to patrol areas and guard doors. #edit (Guard Logic likely needs more ironing out, and also this document needs a lot more detail) 

## Behavior
---
### Patrol
The guard will patrol a circuit if it is given one. This circuit is defined by the user. The circuits are built as follows:
1. Put an empty GameObject into the scene at a location you would like to have a checkpoint
	1. Repeat this for as many checkpoints as you would like
	2. Note that there must be a clear path between the checkpoints, there is no pathing algorithm to automate this yet
	3. It must be a complete loop, so if you want to go back and forth you will have to double the checkpoints up and order them properly
4. Create a new empty gameobject and title it something like "Circuit" and put all the checkpoints into it as children. They MUST be in order. 
5. Assign the circuit to the guard in the inspector

### Target manager
The Target manager is a simple API (as I understand it lol) that manages what the guard is currently tracking. It is the core that connects all other guard scripts. On Start it initializes the guards patrol or lack thereof, its first target, etc. The [[Guard#Guard Movement|GuardMovement]] script calls `targetRequest` whenever it reaches its current direction. 


### Pathing
As a result of the guard following the player, the guard may end up far from its original location or circuit. The path it took, or rather that the player took, may be long and complex, and quite likely that the path back to its location is not a straight line. Therefore, we need a way to track the way that it got there. We use a breadcrumb trail solution. 

The algorithm begins on the frame that we detect a player and works as follows:
1. Every second, check if the `trail`, a `Stack` of type `Vector3`, has two or more elements in it
	1. if it does, we pop one off the stack and store it temporarily. 
	2. We peek at the top of the stack and see if there is a straight line between it and the current position.
		1. If there **is** we discard the location we temporarily stored and push the current location onto the stack. In this way we can eliminate unnecessary nodes from our path in real time, although this system is not perfect. 
		2. If there **isn't** we push the temporarily stored vector3 back onto the stack and then push the current location onto the stack.

When we lose track of the player, finding our way back is as simple as popping a location off of the stack generated by this algorithm. Once the guard reaches that location the Target Assignment manager will request the next location on the stack until it is empty, at which point it will go back to doing what it was doing before. The nice part about this is that if the guard happens to detect the player again on its way back, it is simple for it to just start tracking the path again. 
