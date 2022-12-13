class Node:
    def __init__(self, children = [], size = 0, name = '', parent = None):
        self.children = children
        self.size = size
        self.name = name
        self.parent = parent

    # make it print better
    def __repr__(self):
        return "size:" + str(self.size) + " name:" + str(self.name) + " Number of children:" + str(len(self.children))

    def __str__(self):
        return "size:" + str(self.size) + " name:" + str(self.name) + " Number of children:" + str(len(self.children))
    
i =  [x for x  in open("input.txt").read().strip().split("\n")]

root = Node()
root.name = '/'
currentNode = root
for command in i[1:]:
    #folders, add child node
    if command.startswith('dir'):
        name = command.split(' ')[-1]
        currentNode.children.append(Node([], 0, name, currentNode))
    # files, just add value to size of current dir
    elif not(command.startswith('$')):
        size, name = command.split(' ')
        currentNode.size += int(size)
    #cd up
    if command.startswith('$ cd ..'):
        currentNode = currentNode.parent
    # cd into folder
    elif command.startswith('$ cd'):
        dirname = command.split(' ')[-1]
        for n in currentNode.children:
            if n.name == dirname:
                currentNode = n
                break

# foreach folder, size += file sizes of its children recursively
def setNodeTotalSize(node):
    if (len(node.children  )) < 1:
        return node.size
    else:
        for child in node.children:
            val = setNodeTotalSize(child)
            node.size += val
        if (node.parent != None):
            return node.size
                
setNodeTotalSize(root)

totals = []
# traverse tree adding totals to list
def traverse(node):
    totals.append(node.size)
    if not(node.children):
        return
    for child in node.children:
        traverse(child)

# get all file sizes
traverse(root)

# Part 1
print(sum([x for x in totals if x <= 100000]))

#Part 2
totalDiskSpace = 70000000
needAtleast = 30000000
currentlyUsed = max(totals)
print(min([x for x in totals if totalDiskSpace - currentlyUsed + x > needAtleast]))