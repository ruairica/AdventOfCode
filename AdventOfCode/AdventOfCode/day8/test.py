

arr =  [x for x  in open("ex.txt").read().strip().split("\n")]


print(arr)
for x in range(1, len(arr)-1):
    for y in range(1, len(arr[0])-1):
        current  = int(arr[x][y])
        left = [int(v) for v in arr[x][:y]] # will need to loop backwards through these after
        right = [int(v) for v in arr[x][y+1:]]
        up = []
        #WHY CAN I NOT SLICE THIS LIKE  up = [int(v) for v in arr[:x][y]]?

        up = [int(arr[u][y]) for u in range(0, x)]
        down = [int(arr[d][y]) for d in range(x+1, len(arr))]
        # for u in range(0, x):
        #     print(u)
        #     up.append(int(arr[u][y])) ## will need to loop backwards through these after
        
        # print(up)
        # uptest = [int(v) for v in arr[0:1][y]]
        # print(uptest)
        # down = []
        # for d in range(x+1, len(arr)):
        #     down.append(int(arr[d][y])) 
