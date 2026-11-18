# Lab Assignment #2

Augustine Di Bernardo 100982645

## GDW Mario Game Gameplay Loop:

The gameplay loops follows how the typical old school Mario games do. The player spawns in the level after clicking the game into the game from the main menu. The player controls Mario and is able to run and jump around the level. The player must avoid and kill enemies like the pirrana plants, goombas, koopas while collecting coins and powerups like the fire flower. The goal of the player is to reach the end of the level and hit the spinning block to win. The player dies after Mario runs out of his 5 lives and they must restart again.

## What I created:

For the Lab Assignment, I built off my existing Goomba Spawning and Pooling System to support Koopas. Afterwards, I added the factory design pattern to the enemy spawning system and I placed six enemy spawners around my World 1-1 level. Each spawner randomly chooses either a Goomba or a Koopa using UnityEngine.Random.Range. GoombaFactory and KoopaFactory both inherit from EnemyFactory and supply the type of enemy they are. Goombas and Koopas each have separate pools. When an enemy's lifetime ends, they are disabled and kept to be reused and the state they were previously in is reset.

## How I Tested the Spawn System:

I tested the spawn system by comparing the system with pooling on only verse the spawn system with pooling and the factory design pattern. With Pooling Only mode, the spawners would request the enemys directly from the pool itself. In comparison, the spawners when Factory With Pooling mode was selected would pass the request through the factory before accessing the pools. When testing both versions, I had six spawners active and each spawner would spawn one of either Goomba or Koopa every 5 seconds, with each having a lifetime of 30 seconds. Both Goomba and Koopa had a pool limit of 64. I had a toggle for repeatable test which was enabled that made it use a consistent seed of 1234 to keep the random enemy spawning consistent between each test. For each test I waited 30 seconds before recording the profiler to keep the test accurate rather then starting the recording from the start of gameplay in case a lag spike happened at the start of the game unrelated to the spawn itself. The frame I selected for both tests contained 6 spawning calls as each spawn happens at the same time.

<img width="676" height="574" alt="EnemySpawner_Setup" src="https://github.com/user-attachments/assets/841fcdb2-69ee-43b5-bf53-39d0130d2693" />


## Pooling Only Test Results:

The frame that I selected as shown below, had six EnemySpawner.Spawn calls which took 0.43 ms in total and allocated 40 B.

<img width="2548" height="692" alt="OnlyPooling_Profiler" src="https://github.com/user-attachments/assets/4c15ddb2-12f4-4da4-a6f3-19fc73d0f1bb" />

I also used the highlight tool to view information on Bottlenecks, System Impacts, Frame Timings, Allocations 

<img width="1491" height="624" alt="OnlyPooling_Highlight" src="https://github.com/user-attachments/assets/b0e67c65-cb1b-40a2-9694-7769964ec3ba" />

## Factory With Pooling Test Results:

The frame that I selected as shown below, had six EnemySpawner.Spawn calls which took 0.43 ms in total and allocated 40 B. Underneath the main EnemySpawner.Spawn was three GoombaFactory.Spawn calls that took 0.17 ms in total and allocated 40 B and three KoopaFactory.Spawn calls that took 0.25 ms in total and allocated 0 B.

<img width="2550" height="732" alt="Screenshot 2026-11-17 221342" src="https://github.com/user-attachments/assets/730a164f-8192-4eb7-80b1-9da102126b36" />

I also used the highlight tool to view information on Bottlenecks, System Impacts, Frame Timings, Allocations 

<img width="1477" height="605" alt="FactoryWithPooling_Highlight" src="https://github.com/user-attachments/assets/e7823983-b3a8-4431-8754-0b6a5eb107a5" />

These results are taken from only one chosen spawning wave per recording and not the average of several waves. The timings are provided as shown in the screenshots of the Profiler.

## What did you notice was impacting the performance of your project?

Even with object pooling it still required the necessary CPU time for the activation and deactivation of enemy components. In both cases, the movement, physics and animations of the enemies were still processed even after they had become active.

The capture of the largest spikes in frame times revealed EditorLoop and RenderLoop as the largest spikes captured. These frame times had been measured during editor activity. For the sake of fairness, I used the specific spawning spike samples to do the comparison.

## Which patterns did you implement, and what behaviour did they replace?

Object pooling was used in place of continually spawning and removing enemies. There is a pool that will create the number of Goombas and Koopas that are needed, turn them off when done, and reuse them for future use in the level.

The Factory Design Pattern is being used in place of object instantiation in the spawner to an EnemyFactory interface. EnemyFactory takes care of creating the enemy objects. The specific factories are GoombaFactory and KoopaFactory.

## Do your pattern implementations improve performance? Why or why not?

Both spawning calls in the chosen frames took 0.43 ms in six spawning cases and had a GC Allocation of 40 B. Analyzing the Profiler, there seemed to be no difference between the time spent on spawning either when the pooling approach alone was used or the combination of pooling and factories was employed.

The benefit of applying the factory design pattern lies in the separation of the enemy creation process from that of the spawner, thus making the game maintenance and extensibility much better for creating different types of enemies. The spawner was requesting an enemy instance through the common interface but the specific factory created the particular enemy.

Pooling was applied in both versions. This analysis allowed understanding the influence of factories rather than pooling by itself or the destruction/creation of objects. The timing of further waves of spawning should be analyzed.

## Can you demonstrate what the profiling process revealed?

I am confident that I demonstrated how I applied the profiler well. Specifically, in my tests, the CPU Usage module and Hierarchy view were taken into account to analyse the EnemySpawner.Spawn event. As you can see from the screenshot of the profiler that I have added, the number of six spawns occurs in each of the selected frames with the specified time (ms) and memory allocations.

Speaking of the screenshot of the profiler in case of Factory With Pooling, it should be noted that there occurred events of GoombaFactory.Spawn and KoopaFactory.Spawn. These events showed that the spawns were requested in factories but were used in the pools.

The cost of the original spawn system with only pooling was pretty close to the cost of the spawn system that utilized the Factory design pattern alongside object pooling. The obtained screenshots gave an idea about the recording process and the reason for analysis of the frames' timing apart from the spawns of the enemies.

References:

My game uses Mario assets from the GDW Year 1 Tutorial which are external assets and not my own. I did create the hand drawn number assets used for the coin, lives and timer counters.
