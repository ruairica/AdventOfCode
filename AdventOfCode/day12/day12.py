from collections import Counter, defaultdict, deque

i =  [list(x) for x  in open("input.txt").read().strip().split("\n")]

#each element can be connected up, down left, right
# if element is S, height is a. If E height is z

class node:
    def __init__(self, x, y, value, height, visited = False):
        self.x = x
        self.y = y
        self.value = value,
        self.height = height
        self.visited = visited

    def setVisited(self, value):
        self.visited = value



sNode:node = None #node(0,0,0,'a')
eNode:node = None#node(0,0,0.'a')
grid =[]

part2ins = []

# print(len(i), len(i[0]))
for x in range(len(i)):
    grid.append([])
    for y in range(len(i[0])):
        # print(x,y, i[x][y])
        currentNode = node(x,y, i[x][y], ord(i[x][y]))
        if i[x][y] == 'S':
            currentNode.height = ord('a')
            sNode = currentNode
        elif i[x][y] == 'E':
            currentNode.height = ord('z')
            eNode = currentNode
        
        if currentNode.height == ord('a'):
            part2ins.append(currentNode)
        grid[x].append(currentNode)

topB = 0
bottomB = len(grid)-1
leftB = 0
rightB = len(grid[0])-1

def getValidNeighbours(n, g):
    # a valid neighbor is directly above, below, to the right, to the left, and it's height must be abs <=1
    #left
    validNodes = []
    if not(n.y == leftB):
        leftNode = g[n.x][n.y-1]
        if abs(leftNode.height) - abs(n.height) <=1: #and leftNode.visited == False:
            validNodes.append(leftNode)

    if not(n.y == rightB):
        rightNode = g[n.x][n.y+1]
        if abs(rightNode.height) - abs(n.height) <=1: #and rightNode.visited == False:
            validNodes.append(rightNode)

    if not(n.x == topB):
        tNode = g[n.x-1][n.y]
        if abs(tNode.height) - abs(n.height) <=1: #and tNode.visited == False:
            validNodes.append(tNode)
    
    if not(n.x == bottomB):
        bNode = g[n.x+1][n.y]
        if abs(bNode.height) - abs(n.height) <=1: #and bNode.visited == False:
            validNodes.append(bNode)

    return validNodes



def bfs(s, e, grid):
    queue = deque([(s, 0)])
    S = set()
    while len(queue) > 0:
        node, pathLength = queue.popleft()

        #WHY DOES THIS CHANGE EVERYTHING, shouldn't the visited logic also be doing this????,
        # I think the issue might be, that if 2 neighbouring nodes at the start both put the same thing in the queue,
        # it gets flicked back and forth from visited true to false, is this right
        # if (node) in S:
        #     continue
        # S.add(node)

        if node.visited: #if this is mak
            continue

        if node == e:
            return pathLength
        

        pathLength +=1
        node.setVisited(True)

        validNeighbours = getValidNeighbours(node, grid)
        values = [(neighbour, pathLength) for neighbour in validNeighbours if not(neighbour.visited)]
    
        queue.extend(values)
    
    return None


part1 = bfs(sNode, eNode, grid)
print(part1)


part2outs = []
for part2in in part2ins:

    # reset all visits to false
    for x in range(len(grid)):
        for y in range(len(grid[0])):
            grid[x][y].visited = False

    res = bfs(part2in, eNode, grid)
    if res != None:
        part2outs.append(res)

print(sorted(part2outs)[0])
