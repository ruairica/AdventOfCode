#https://adventofcode.com/2022/day/4
import sys
from collections import Counter, defaultdict

i =  [x for x  in open("input.txt").read().strip().split("\n")]

#  AFTER LOOKING UP SOLUTION SHOULD JUST CHECK UPPER BOUNDS AND LOWER BOUNDS NO NEED TO INTERSECT LISTS!!!
part1 = 0
part2 = 0

for x in i:
    one, two = x.split(",")
    # get array of numbers of one, and check all are within the second
    arr = one.split("-")
    o = []
    for j in range(int(arr[0]), int(arr[1])+1):
        o.append(j) # turns out I can replace this with : o = range(int(arr[0]), int(arr[1])+1)

    arr2 = two.split("-")
    t = []
    for j in range(int(arr2[0]), int(arr2[1])+1):
        t.append(j)

    #part 1
    if  set(o).issubset(set(t)) and set(t).issubset(set(o)):
        part1 +=1
    #part 2
    if  set(o) & set(t):
        part2 +=1
    

print(part1)
print(part2)