#https://adventofcode.com/2022/day/6
from collections import Counter, defaultdict

i =  open("input.txt").read().strip()
def day6(length):
    for x in range(0, len(i) - length):
        if len(set(i[x:x+length])) == length:
            return x+length
# part 1
print(day6(2))

# part 1 one liner
print(next(x+4 for x in range(0,i) if len(set(i[x:x+4])) == 4))


# part 2
print(day6(14))

