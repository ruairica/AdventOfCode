arr =  [x for x  in open("input.txt").read().strip().split("\n")]

#outer trees for part 1
horizontal = len(arr[0]) * 2
vert  = len(arr) * 2
outerTrees = horizontal + vert -4 # minus 4 corners that were counted twice

part1 = 0
part2 = 0
for x in range(0, len(arr)):
    for y in range(0, len(arr[0])):
        current = int(arr[x][y])

        left = [int(v) for v in arr[x][:y]]
        right = [int(v) for v in arr[x][y+1:]]
        up = []
        # spent a while trying to do the above for up and down but found out I can't slice through dimensions of the array so did this
        # for u in range(0, x):
        #     print(u)
        #     up.append(int(arr[u][y])) ## will need to loop backwards through these after
        # down = []
        # for d in range(x+1, len(arr)):
        #     down.append(int(arr[d][y])) 

        #refactored above to be 
        up = [int(arr[u][y]) for u in range(0, x)]
        down = [int(arr[d][y]) for d in range(x+1, len(arr))]

        #PART 1
        if (all(t < current for t in left)):
            part1 +=1

        elif (all(t < current for t in right)):
            part1 +=1

        elif (all(t < current for t in up)):
            part1 +=1

        elif (all(t < current for t in down)):
            part1 +=1

        #PART 2
        # note: could have just looped normally and done this for the ones that had to be looped backwards
        # for n in up.reverse():
        #   if n < current:
        #         uscore +=1 
        #    else:
        #         uscore +=1 break
        uscore = 0
        for n in range(len(up)-1, -1, -1):
            if up[n] < current:
                uscore += 1
            else:
                uscore +=1
                break        

        lscore = 0
        for n in range(len(left)-1, -1, -1):
            if left[n] < current:
                lscore += 1
            else:
                lscore+=1
                break

        rscore = 0
        for n in range(0, len(right)):
            if right[n] < current:
                rscore += 1
            else:
                rscore +=1
                break

        dscore = 0
        for n in range(0, len(down)):
            if down[n] < current:
                dscore += 1
            else:
                dscore +=1
                break

        currentScore = lscore * rscore * uscore * dscore
        part2 = max(part2, currentScore)

print(part1 + outerTrees)
print(part2)