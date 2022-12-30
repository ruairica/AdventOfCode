#https://adventofcode.com/2022/day/5
from collections import Counter, defaultdict

i =  open("input.txt").read()
cratestext, movestext = i.split("\n\n")
c = cratestext.split("\n")

stacks = defaultdict(list)
numOfCrates = 9

crateCounter = 1
for x in range(len(c) - 1, -1, -1):
    for characterIndex in range(1, len(c[x]), 4):
        element = c[x][characterIndex]
        if not(element == " "):
            stacks[crateCounter].append(element)
        
        crateCounter+=1
        if crateCounter > numOfCrates:
            crateCounter = 1

moves = movestext.split("\n")

for move in moves:
    splits = move.split(" ")
    print(splits)
    amount, crateFrom, crateTo = int(splits[1]), int(splits[3]), int(splits[5])

    # Part 1
    # for x in range(0, int(amount)):
    #     character = stacks[crateFrom].pop()
    #     print(stacks[crateFrom])
    #     print(character)
    #     stacks[crateTo].append(character)

    # Part 2
    elementsToMove = stacks[crateFrom][len(stacks[crateFrom]) - amount::]
    stacks[crateTo].extend(elementsToMove)
    for x in range(0, int(amount)):
        character = stacks[crateFrom].pop()

result = ''
for k,v in stacks.items():
    result += v[-1]

print(result)