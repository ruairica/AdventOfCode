##PART1
from collections import Counter, defaultdict

lines =  [x for x  in open("input.txt").read().strip().split("\n")]


#tail follows around 1 behind
start = (0 ,0)
tail = [(0, 0)]
head = (0, 0)

tailVisits = [(0,0)]

UP = "U"
DOWN = "D"
RIGHT = "R"
LEFT = "L"

for line in lines:
    direction, s = line.split(" ")
    size = int(s)
    print(direction, size)

    #moving head
    # if direction == UP:
    #     head[1]+=size
    # elif direction == LEFT:
    #     head[0]-=size
    # elif direction == DOWN:
    #     head[1]-=size
    # elif direction == RIGHT:
    #     head[0]+=size
    print(head)
    origHead = head
    if direction == UP:
        head = (head[0], head[1]+size)
    elif direction == LEFT:
        head = (head[0]-size, head[1])
    elif direction == DOWN:
        head = (head[0], head[1]-size)
    elif direction == RIGHT:
        head = (head[0]+size, head[1])

    print(head)
    # if after the move tail is within 1 of head, don't move
    # print(head[0], head[1], tail[0], tail[1])

    if abs(head[1] - tail[1]) <=1 and abs(head[0] - tail[0]) <=1:
        continue
    else:
        if direction == UP:
            for i in range(origHead[1], head[1]+1):
                if not(abs(i - tail[1]) <=1 and abs(origHead[0] - tail[0]) <=1):
                    tail = (head[0], i-1)
                    print(tail)
                    tailVisits.append(tail)

        elif direction == LEFT:
            for i in range(origHead[0], head[0]-1, -1):
                if not(abs(origHead[1] - tail[1]) <=1 and abs(i - tail[0]) <=1):
                    tail = (i+1, head[1])
                    print(tail)
                    tailVisits.append(tail)

            #     tailVisits.append((i, origHead[1]))
            # tail=(head[0]+1, head[1])
        elif direction == DOWN:
            for i in range(origHead[1], head[1]-1, -1):
                if not(abs(i - tail[1]) <=1 and abs(origHead[0] - tail[0]) <=1):
                    tail = (head[0], i+1)
                    print(tail)
                    tailVisits.append(tail)

            # tail=(head[0], head[1]+1)
        elif direction == RIGHT:
            for i in range(origHead[0], head[0]+1):
                if not(abs(origHead[1] - tail[1]) <=1 and abs(i - tail[0]) <=1):
                    tail = (i-1, head[1])
                    print(tail)
                    tailVisits.append(tail)
                # tailVisits.append(i, origHead[1])
            # tail=(head[0]-1, head[1])
        
        # tailVisits.append(tail)
    # print(head, tail)

        
print(tailVisits)
print(len(set(tailVisits)))
#249 too low
        