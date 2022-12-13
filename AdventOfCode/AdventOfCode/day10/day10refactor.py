#PART1
from collections import Counter, defaultdict

lines =  [x for x  in open("input.txt").read().strip().split("\n")]

cycleCounter = 0 # it's either going to land on it, or be 1 after, or 1 before
importantCycles = [20,60,100,140,180,220]
runningX = 1
dd = {}
dd[0] = 1
for line in lines:
    if line.startswith("noop"):
        cycleCounter += 1
        dd[cycleCounter] = runningX
    elif line.startswith("addx"):
        cycleCounter +=2
        dd[cycleCounter-1] = runningX
        runningX += int(line.split(" ")[-1])
        dd[cycleCounter] = runningX

    

part1 = sum([dd[x-1] * x for x in importantCycles])
print(part1)

#part 2
currentClock = 0
for x in range(0,6):
    for y in range(0,40):

        currentPos = dd[currentClock]
        spritePos = [currentPos, currentPos+1, currentPos-1]

        if y in spritePos:
            print('#', end='')
        else:
            print('.', end='')
        currentClock +=1

    print('\n', end='')