import sys
from collections import defaultdict

i =  [int(x) for x  in open("input.txt").read().strip().split("\n")]

print(i)

counter = 0
for x in range(1, len(i)):
    if i[x] > i [x-1]:
        counter+=1


#part 2
counter = 0
for x in range(0, len(i)-3):
    cur = i[x] + i[x+1] + i[x+2]
    nex = i[x+1] + i[x+2] + i[x+3]

    if nex > cur:
        counter+=1



print(counter)