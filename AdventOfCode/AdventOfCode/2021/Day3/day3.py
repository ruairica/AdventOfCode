import sys
from collections import Counter, defaultdict

i =  [x for x  in open("input.txt").read().strip().split("\n")]

mins = []
maxs = []
print(len(i[0]))
for x in range(0, len(i[0])):
    counter0 = 0
    counter1 = 0
    for y in range(0, len(i)):
        #print(x, y)
        if int(i[y][x]) == 1:
            counter1 += 1
        else:
            counter0 +=1
    if counter1 > counter0:
        maxs.append(1)
        mins.append(0)
    else:
        mins.append(1)
        maxs.append(0)

print(mins)
print(maxs)
    
mind = 0
for m in range(0, len(mins)):
    mind += mins[m] * (2 **(len(mins)-1-m))

maxd = 0
for m in range(0, len(maxs)):
    maxd += maxs[m] * (2 **(len(maxs)-1-m))

print(mind * maxd)
