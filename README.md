### Context - why and what is it?
In this demo, I showcase how I integrated a Ready Player Me avatar in a small game sample I made, with a focus on a cutscene. The reason behind this focus is that in cutscenes and mostly in third-person games, they tend to showcase the main character as a hero so, I wanted to show you as a developer a way to make gamers see themselves as heroes in their favorite games. <br />
I wanted to show developers a way to offer gamers to live a unique experience through their unique avatar.

### The game example
Since the game sample is focused on the cutscene, and because of time constraint, I couldn't record voice over for the cinematic sequence. I wanted to give a quick overview of what the game is about. <br />
It is an adventure game where the main character is an archeologist looking for a specific crystal known for its power to give the ability to travel through time.
When we start the game, we are in a cave where we need to look for the way out and once we are outside, we see ruins. As soon as the character gets close to those ruins, gameplay is interrupted and the cutscene starts playing. <br />
During the cinematic, we see the character solving a puzzle to reveal the crystal. Shortly after we see the character getting hit by an unknown enemy and losing consciousness. Later during the sequence we see them waking up but the crystal has disappeared. The gameplay resumes and the goal is to find these enemies and get the crystal. <br />
For this demo, I didn't include more gameplay after the cutscene. The player needs to find the second cave entrance, and as soon as we enter it, we are sent back to the main menu. <br />

The sample demo can be played with any Ready Player Me avatar, as the loading is handled at runtime for the different parts of the game with different configs. It is possible to enjoy it with all the animations integrated throughout the main menu, gameplay and cutscene.

### Tools used
I used Unity game engine, latest LTS version 2021.3.23f1. The project uses the Universal Render Pipeline (URP). Below are the major packages used: <br />
- Ready Player me packages <br />
- Various Unity packages including URP, Cinemachine, Timeline, TextMeshPro, Input System.

### Assets used
I used these free assets from the Unity Asset Store: <br />
[Starter Assets - Third Person Character Controller](https://assetstore.unity.com/packages/essentials/starter-assets-third-person-character-controller-196526) <br />
[Altar Ruins Free](https://assetstore.unity.com/packages/3d/environments/fantasy/altar-ruins-free-109065)<br />
[Mine](https://assetstore.unity.com/packages/3d/environments/dungeons/mine-92461)<br />
[Translucent Crystals](https://assetstore.unity.com/packages/3d/environments/fantasy/translucent-crystals-106274)<br />
[Epic Action Cinematic by Infraction [No Copyright Music] / Heroes](https://www.youtube.com/watch?v=lpEL1Nt6rJk)<br />

### Process highlights
This is a quick overview of the process through 5 steps detailed below. More details will be shared when showcasing the demo during the presentation.

1. ##### Set up the game environment
I started by importing the packages used for the environment to compose a small sample demo scene. I used the cave and ruins assets. These were added to the project, so I can have a small gameplay sequence followed by a cutscene. <br />
Some of the assets were using the Built-in Renderer pipeline, so I needed to upgrade some of the materials to URP to make sure they work properly. <br />
I then decided to use some of these same assets to create a menu scene as well. <br />
I also used the Unity Terrain Tools to set up a small surrounding environment.

2. ##### Created main Menu
In the main menu, I set up a simple scene where we can see the avatar in idle animation with some menu button options: <br />
- Start Game will call a method on MainMenu script to load a new scene using Unity scene management API. <br />
Options (Just visual, not implemented)
- Change Avatar: This allows you to change the avatar at runtime. I have also implemented a way to store the data for loading using a scriptable object to store the config ref and the transform properties and another SO to store the avatar URL, so I can access it across scenes. <br />
- Exit game: It exits the game when we are in the build. <br />

3. ##### Integrating Starter Asset to demo gameplay scene and integrating Ready Player Me avatar
I wanted to start by testing the gameplay of a simple character moving around the sample scene using the third person controller from Unity Starter Assets, and then do the integration of the RPM avatar. That way, I can mimic the use case of integrating an avatar into an existing game loop. At first, I did it manually with an imported avatar, and later I implemented a script to handle doing it at runtime using what I called an avatar template as a base.

4. ##### Creating cutscene
After a few seconds in the gameplay scene by going through the cave, the player will encounter the ruins. As soon as we get close to them, a cutscene will be played. I used Timeline to create it and also used Cinemachine for cameras. I also made a script to bind the loaded cutscene specific avatar into the animation and activation tracks made for the main character.

5. ##### Cross scenes/contexts avatar
A key feature on this demo is the fact that the avatar loading is done at runtime. Meaning that I offer the users the ability to change the avatar in the main menu and that avatar will be used throughout the game parts (Main menu, gameplay and cutscene). I made a loader script that will take care of making sure the game runs smoothly in different game contexts, using an enum to specify the context of the loader.
