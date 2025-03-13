#Conveyor Belt Challenge

##Overview
There is a factory production line around a single conveyor belt.
Components (of type A and B) come onto the start of the belt at random intervals; workers
must take one component of each type from the belt as they come past, and combine them
to make a finished product C.
The belt is divided into fixed-size slots; each slot can hold only one component or one
finished product. There are a number of worker stations on either side of the belt, spaced to
match the size of the slots on the belt.
In each unit of time, the belt moves forwards one position, and there is time for a worker on
one side of each slot to either take an item from the slot or replace an item onto the belt.
The worker opposite can't touch the same belt slot while they do this. (So you can't have one
worker picking something from a slot while their counterpart puts something down in the
same place).
Once a worker has collected one of both types of component, they can begin assembling the
finished product. This takes an amount of time, so they will only be ready to place the
assembled product back on the belt on the fourth subsequent slot. While they are
assembling the product, they can't touch the conveyor belt. Workers can only hold two items
(component or product) at a time; one in each hand.

##Task
Create a simulation of this, with three pairs of workers. At each time interval, the slot at the
start of the conveyor belt should have an equal (1/3) chance of containing nothing, a
component A or a component B.
Run the simulation for 100 steps, and compute how many finished products come off the
production line, and how many components of each type go through the production line
without being picked up by any workers.

##Assumptions
I have assumed that while one worker is assembling the finished product, their counterpart can pick up or put down a component.
I have assumed that the conveyor belt will begin empty and therefore begin by a new item moving along the line to be processed.

##Running the solution
There is a folder at the solution level named "Solution Items". This contains the executable file produced from the project to run in a Windows environment. Simply run the file and a cli interface will open and prompt you for your input.

Alternatively, if you have visual studio and .Net 8 installed, you can build that file.
Right click the "ConveyorBeltChallenge" project and select "Publish".
In the window that opens, select "Folder" and then click "Next".
Click "Folder" again and then click "Next".
Chose a location for the exe to be created and then click "Finish".
When that's done, click "Show all settings" and update "Deployment mode" to "Self-contained" and then dropdown "File publish options" and enable "Produce single file".
Save that and then click "Publish".
The exe will be created in the location you chose.