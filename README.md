# Unity Stealth Mechanic
- Flexible collision based stealth system
- Includes customizable state behaviors
- Share states between different agents
## Usage
These scripts are meant to be added to a project in Unity 2022's long term support version.
Simply apply the StealthAgent.cs script to any agent that will need to detect a player.
Next drag in the colliders and target information through the inspector to add functionality.
Add in the StealthManager script to a manager adjacent object if communication between agents is wanted.
## Custom Behavior
As this is just a detection mechanic, it is very basic in what it can do out of the box.
However, with simple logic applied based on the current states of both the manager and agent,
custom behavior can be easily implemented/integrated.
## Sharing States
As the way that agents will be managed will differ based on use case, there is not much for initial sharing of state.
However this can easily be implemented based on the state of the manager class as functionality can be implemented to share
states down to each agent in a scene.
