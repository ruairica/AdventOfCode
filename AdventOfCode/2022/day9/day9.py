from collections import Counter, defaultdict

lines =  [x for x  in open("input.txt").read().strip().split("\n")]
#for part 2
tailTrail =[(0,0), (0,0),(0,0),(0,0),(0,0),(0,0),(0,0),(0,0),(0,0),(0,0)]
#for part 1
# tailTrail =[(0,0),(0,0)]
tailVisits = [(0,0)]

UP = "U"
DOWN = "D"
RIGHT = "R"
LEFT = "L"

#If the head is ever two steps directly up, down, left, or right from the tail,
# the tail must also move one step in that direction so it remains close enough:
#
#Otherwise, if the head and tail aren't touching and aren't
# in the same row or column, the tail always moves one step diagonally to keep up:
def newFollowerPosition(leader, follower):
    # no move required
    if (abs(leader[0] - follower[0]) <=1 and abs(leader[1] - follower[1]) <=1):
        return follower
    # moving in the same row
    if (leader[0] == follower[0] or leader[1] == follower[1]):
        if not(abs(leader[1] - follower[1]) <=1):
            if leader[1] > follower[1]: #up
                return (follower[0], follower[1]+1)
            else:
                return (follower[0], follower[1]-1) # down
        elif not(abs(leader[0] - follower[0]) <=1):
            if leader[0] > follower[0]:
                return (follower[0]+1, follower[1]) #right
            else:
                return (follower[0]-1, follower[1]) #left
    else: #moving diagonally
        if leader[0] > follower[0] and leader[1] > follower[1]: #up to the right
            return (follower[0]+1, follower[1]+1)
        elif leader[0] > follower[0] and leader[1] < follower[1]: # down to the right
            return (follower[0]+1, follower[1]-1)
        elif leader[0] < follower[0] and leader[1] < follower[1]: # down to the left
            return (follower[0]-1, follower[1]-1)
        elif leader[0] < follower[0] and leader[1] > follower[1]: # up to the left
            return (follower[0]-1, follower[1]+1)

for line in lines:
    direction, s = line.split(" ")
    size = int(s)
    startMoveHead = tailTrail[0]
    if direction == UP:
        for i in range(startMoveHead[1]+1, startMoveHead[1]+size+1):
            # check if the head moving at all would cause the first tail element to move
            tailTrail[0] = (startMoveHead[0], i)
            for x in range(1, len(tailTrail)):
                tailTrail[x] = newFollowerPosition(tailTrail[x-1], tailTrail[x])
                tailVisits.append(tailTrail[-1])            
    elif direction == LEFT:
        for i in range(startMoveHead[0]-1, startMoveHead[0]-size-1, -1):
            # check if the head moving at all would cause the first tail element to move
            tailTrail[0] = (i, startMoveHead[1])
            for x in range(1, len(tailTrail)):
                tailTrail[x] = newFollowerPosition(tailTrail[x-1], tailTrail[x])
                tailVisits.append(tailTrail[-1])
    elif direction == DOWN:
        for i in range(startMoveHead[1]-1, startMoveHead[1]-size-1, -1):
            tailTrail[0] = (startMoveHead[0], i)
            for x in range(1, len(tailTrail)):
                tailTrail[x] = newFollowerPosition(tailTrail[x-1], tailTrail[x])
                tailVisits.append(tailTrail[-1])
    elif direction == RIGHT:
        for i in range(startMoveHead[0]+1, startMoveHead[0]+size+1):
            tailTrail[0] = (i, startMoveHead[1])
            for x in range(1, len(tailTrail)):
                tailTrail[x] = newFollowerPosition(tailTrail[x-1], tailTrail[x])
                tailVisits.append(tailTrail[-1])

print(len(set(tailVisits)))