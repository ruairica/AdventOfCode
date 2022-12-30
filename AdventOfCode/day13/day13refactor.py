#https://adventofcode.com/2022/day/13
###PART 1+2
from collections import Counter, defaultdict, deque
from functools import cmp_to_key

blocks =  [x for x  in open("ex.txt").read().strip().split("\n\n")]

#If the left list runs out of items first, the inputs are in the right order. If the right list runs out of items first, the inputs are not in the right order
def compare(l, r):
    if len(l) == 0 and len(r) != 0:
        return -1
    if len(l) != 0 and len(r) == 0:
        return 1
    for k in range(len(l)):
        if (k > len(r)-1):
            return 1
        if type(l[k]) == int and type(r[k]) == int:
            if l[k] < r[k]:
                return -1
            elif r[k] < l[k]:
                return 1
            elif k == len(l) -1 and len(r) > len(l):
                return -1 
        elif type(l[k]) == list and type(r[k]) == list:
            res = compare(l[k], r[k])
            if res != 0:
                return res
        elif (type(l[k]) == list and type(r[k]) != list):
            res = compare(l[k], [r[k]])
            if res != 0:
                return res
        elif (type(l[k]) != list and type(r[k]) == list):
            res = compare([l[k]], r[k])
            if res != 0:
                return res
    return 0

part2in = []
part1result = 0 
for index, block in enumerate(blocks):
    leftText, rightText = block.split('\n')
    left = eval(leftText)
    right = eval(rightText)

    val = compare(left, right) 
    if val == -1:
        part1result+= 1 + index

    part2in.append(left)
    part2in.append(right)

part2input1 = [[2]]
part2input2 = [[6]]
part2in.append(part2input1)
part2in.append(part2input2)

def bubbleSort(arr):
    n = len(arr)
    # optimize code, so if the array is already sorted, it doesn't need
    # to go through the entire process
    swapped = False
    # Traverse through all array elements
    for i in range(n-1):
        # range(n) also work but outer loop will
        # repeat one time more than needed.
        # Last i elements are already in place
        for j in range(0, n-i-1):
 
            # traverse the array from 0 to n-i-1
            # Swap if the element found is greater
            # than the next element
            res = compare(arr[j], arr[j+1])
            if res == 1: # wrong order
                swapped = True
                arr[j], arr[j + 1] = arr[j + 1], arr[j]
         
        if not swapped:
            # if we haven't needed to make a single swap, we
            # can just exit the main loop.
            return


def bubbleSortTest(arr):
    for j in range(len(arr)):
        for k in range(len(arr)-1):
            if compare(arr[k], arr[k+1]) == 1:
                arr[k], arr[k+1] = arr[k+1], arr[k]


# part2copy = part2in[:]
# bubbleSortTest(part2copy)
# first = part2copy.index(part2input1) +1
# second = part2copy.index(part2input2) +1
# custom sort.. whyh does this not work
res2= sorted(part2in, key=cmp_to_key(compare))
first = res2.index(part2input1) +1
second = res2.index(part2input2) +1

print(part1result)
print(first * second)
