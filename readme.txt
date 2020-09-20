Game feel decisions:
- As jumping and walking are the most important mechanics in a platform game,
I spent most of my time tweaking those mechanics to improve the game feel:
* For the walking mechanic, I approximated how it works in real life,
which means that the walking speed should accelerate to a max walking speed 
when pressing down the button and decelerate the walking speed to zero when 
the button is released to stop the movement of the player. I tweaked those 
variables (acceleration, max walking speed and deceleration) in such a way 
that it feels natural and responsive.
* For the jumping mechanic, it's important that the jump has a nice arc and
that it feels responsive and controllable.
To make it feel responsive, I added an initial jump force which launches the
player into the air when the jump button is pressed. Then, the jump velocity 
is being decreased to create a nice arc.
To be able to control the jump well, the player can hold down the jump button
to perform a longer jump, but also perform a short jump when just tapping
the jump button. To have have a sense of weight, the gravity is also increased
when the player is falling down.
- I also added screenshake when the player gets damaged to give sensory 
feedback to the player.

Time spent on the assignment:
- 8 hours (and 15 minutes)

Code that could be improved:
- The code responsible for the jumping mechanic (including the jump input).
At the moment it feels very chaotic, confusing to tweak and some parts are
not working the way I want.

Used third-party code/tools:
- Cinemachine for camera movement
- 2D Tilemap Editor for building the level
- DOTween is added, but not used