import sys
from collections import Counter, defaultdict

i =  [x for x  in open("input.txt").read().strip().split("\n")]

## testing list comprehension
test = [int(x[-1]) for x in i]
test2 = Counter(test)
print(test2)
print(test2[4])
hor = 0
ver = 0
aim = 0

for x in i:
    val = int(x[-1])
    if x.startswith("forward"):
        hor += val
        ver += aim * val
    if x.startswith("down"):
        aim += val
    if x.startswith("up"):
        aim -= val

print(ver * hor)
