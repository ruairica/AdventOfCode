#PART1
from collections import Counter, defaultdict

lines =  [x for x  in open("input.txt").read().strip().split("\n")]


#signal strength the cycle number multiplied by the value of the X register
#Find the signal strength during the 20th, 60th, 100th, 140th, 180th, and 220th cycles. What is the sum of these six signal strengths?
def calculateX(commands):
    X=1
    parsedNums = []
    ccount = 0
    debug = []
    print("CALC X")
    for cmd in commands:
        if cmd.startswith("noop"):
            ccount+=1
            continue
        else:
            parsedNum = int(cmd.split(" ")[-1])
            parsedNums.append(parsedNum)
            ccount+=2
            X += parsedNum
        debug.append((ccount, parsedNums))
    return X

cycleCounter = 0 # it's either going to land on it, or be 1 after, or 1 before
importantCycles = [20,60,100,140,180,220]
results = []
regVal = 1
dd = defaultdict(int)
for index, line in enumerate(lines):
    if line.startswith("noop"):
        cycleCounter += 1
    elif line.startswith("addx"):
        cycleCounter +=2
    
    if cycleCounter == importantCycles[0] or cycleCounter == importantCycles[0] + 1:
        linesNeeded = lines[:index]
        xVal = calculateX(linesNeeded)
        results.append(xVal * importantCycles[0])
        importantCycles.pop(0)

    if not(importantCycles):
        break

print(results)
print(sum(results))

#13982 too high
#13940 too high        

#PART 2
# from collections import Counter, defaultdict

# lines =  [x for x  in open("input.txt").read().strip().split("\n")]

#signal strength the cycle number multiplied by the value of the X register
#Find the signal strength during the 20th, 60th, 100th, 140th, 180th, and 220th cycles. What is the sum of these six signal strengths?
# def calculateX(commands):
#     X=1
#     parsedNums = []
#     ccount = 0
#     debug = []
#     for cmd in commands:
#         if cmd.startswith("noop"):
#             ccount+=1
#             continue
#         else:
#             parsedNum = int(cmd.split(" ")[-1])
#             parsedNums.append(parsedNum)
#             ccount+=2
#             X += parsedNum
#         debug.append((ccount, parsedNums))
#     return X

# cycleCounter = 0 # it's either going to land on it, or be 1 after, or 1 before
# importantCycles = [20,60,100,140,180,220]
# results = []
# regVal = 1
# dd = {}
# dd[0]=1

# runingX = 1
# for index, line in enumerate(lines):
#     if line.startswith("noop"):
#         cycleCounter += 1
#         dd[cycleCounter] = runingX
#     elif line.startswith("addx"):
#         cycleCounter +=2
#         dd[cycleCounter-1] = runingX
#         sliced = lines[:index+1]
#         runingX = calculateX(sliced)
#         dd[cycleCounter] = runingX

    
# currentClock = 0
# for x in range(0,6):
#     for y in range(1,41):

#         currentPos = dd[currentClock]
#         spritePos = [currentPos, currentPos+1, currentPos-1]

#         if y-1 in spritePos:
#             print('#', end='')
#         else:
#             print('.', end='')
#         currentClock +=1

#     print('\n', end='')
