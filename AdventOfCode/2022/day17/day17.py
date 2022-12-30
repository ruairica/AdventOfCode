#PART 1
import re
from collections import Counter, defaultdict, deque

line =  [x for x  in open("input.txt").read().strip()]
a = ['a', 'b','c','d', 'e']

global copyLine 
copyLine = line[::]
def getDir():
    global copyLine
    item = copyLine.pop(0)
    if len(copyLine) == 0:
        copyLine = line[::]
    return item

def spawnShape(char):
    if char == 'a':
        return [(2,maxY+4),(3,maxY+4),(4,maxY+4),(5,maxY+4)] # -
    if char == 'b':
        return [(2, maxY+5), (3, maxY+4), (3, maxY+5), (3, maxY+6), (4, maxY+5)] #+
    if char == 'c':
        return [(2, maxY+4), (3, maxY+4), (4, maxY+4), (4, maxY+5), (4, maxY+6)] # backwards L
    if char == 'd':
        return [(2, maxY+4), (2, maxY+5), (2, maxY+6), (2, maxY+7)]
    if char == 'e':
        return [(2, maxY+4), (3, maxY+4), (2, maxY+5), (3, maxY+5)]

maxY = 0
chamber = set([(0,0) ,(1,0), (2,0),(3,0),(4,0),(5,0),(6,0)])
for i in range(0,2022):
    letter = a[i%5]
    shape  = spawnShape(letter)

    landed = False
    while not(landed):
        direction = getDir()
        val = 1 if direction == '>' else -1

        if not(any([x for x in shape if (x[0] + val < 0) or (x[0] + val > 6) or (x[0]+val, x[1]) in chamber])): # won't hit a wall
            shape = [(x[0]+val, x[1]) for x in shape] #move horizontally

        for bit in shape:
            if (bit[0], bit[1]-1) in chamber:
                landed = True
                yvals = [x[1] for x in shape]
                yvals.append(maxY)
                maxY = max(yvals)
                chamber.update(shape)
                break;
        else:
            shape = [(x[0], x[1]-1) for x in shape]


print(maxY)