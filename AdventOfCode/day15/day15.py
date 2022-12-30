#PART1
import itertools
import re
from collections import Counter, defaultdict, deque


def squaresCovered(sx,sy,bx,by, row):
    md = abs(bx - sx) + abs(by - sy)
    if abs(sy) + abs(md) > abs(row):
        sc[row].add((sx, row))
        squaresWide = md - abs(sy-row)
        for i in range(1, squaresWide+1):
            sc[row].add((sx+i, row))
            sc[row].add((sx-i,row))

sc = defaultdict(set)
exclude = set()
yNum=2000000
sc[yNum]= set()
lines =  [x for x  in open("input.txt").read().strip().split("\n")]
for line in lines:
    sx,sy,bx,by = re.findall(r'-?\d+\.?\d*', line)
    sx,sy,bx,by = int(sx),int(sy),int(bx),int(by)
    if by == yNum:
        exclude.add((bx,by))
    squaresCovered(sx,sy,bx,by, yNum)


res = len(sc[yNum])
res-=len(exclude) #exclude existing beacons on the line
print(res)