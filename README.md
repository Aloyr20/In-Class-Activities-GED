# In-Class-Activity-2 && Lab Assignment #1

Augustine Di Bernardo 100982645

GDW Mario Game Gameplay Loop:

The gameplay loops follows how the typical old school Mario games do. The player spawns in the level after clicking the game into the game from the main menu. The player controls Mario and is able to run and jump around the level. The player must avoid and kill enemies like the pirrana plants, goombas, koopas while collecting coins and powerups like the fire flower. The goal of the player is to reach the end of the level and hit the spinning block to win. The player dies after Mario runs out of his 5 lives and they must restart again.

Flow Chart of Audio Manager & Singleton Implementation
<img width="959" height="810" alt="{5B42E6B8-AFC9-406E-A74A-94513118DBC5}" src="https://github.com/user-attachments/assets/d578ad5b-d568-46c6-b686-59c5f0df4a98" />

What element of your game adopts the chosen pattern?

The AudioManager script/Audio Management is done by using the Singleton pattern. It creates a single instance of the AudioManager which can be accessed globally by other scripts using the line AudioManager.Instance. That line is used when controlling what audio is meant to be stopped or played for example, AudioManager.Instance.Play("Coin") will access the AudioManager and play the sound named Coin without needing to reference the AudioManager script.

Why is this pattern a good choice for the associated functionality?

The Singleton pattern was a good choice in making the AudioManager since a game should only need a single instance that controls its audio. This allowed any script to access the AudioManager without needing to create separate references to the AudioManager script each time. This helps keep the Music and SFX organized with one singular script while also only requiring one AudioManager to be created across the whole game.

References:

My game uses Mario assets from the GDW Year 1 Tutorial which are external assets and not my own. I did create the hand drawn number assets used for the coin, lives and timer counters.
