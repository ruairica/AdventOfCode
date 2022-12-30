##PART 1
from collections import Counter, defaultdict, deque

orignumbers =  [int(x) for x  in open("input.txt").read().strip().split("\n")]

class node:
    def __init__(self, val, prev = None, nextn= None):
        self.val = val
        self.prev = prev
        self.next = nextn

newnodes = []
for i in range(len(orignumbers)):
    newnodes.append(node(orignumbers[i]))

for i in range(1, len(newnodes)-1):
    newnodes[i].prev = newnodes[i-1]
    newnodes[i].next = newnodes[i+1]

newnodes[0].next = newnodes[1]
newnodes[0].prev = newnodes[-1]

newnodes[-1].prev = newnodes[-2]
newnodes[-1].next = newnodes[0]



for x in newnodes:
    #delete
    x.prev.next = x.next
    x.next.prev = x.prev

    a, b = x.prev, x.next
    move = x.val % (len(newnodes) -1)

    for _ in range(move):
        a=a.next
        b=b.next

    a.next = x
    x.prev = a
    b.prev = x
    x.next = b


for x in newnodes:
    if x.val == 0:
        r = 0
        y = x
        for _ in range(3):
            for _ in range(1000):
                y = y.next
            r += y.val
        print(r)


# ## PART 2
# from collections import Counter, defaultdict, deque

# orignumbers =  [int(x) for x  in open("input.txt").read().strip().split("\n")]

# class node:
#     def __init__(self, val, prev = None, nextn= None, touched = False):
#         self.val = val
#         self.prev = prev
#         self.next = nextn
#         self.touched = touched

# newnodes = []
# for i in range(len(orignumbers)):
#     newnodes.append(node(orignumbers[i] * 811589153))

# for i in range(1, len(newnodes)-1):
#     newnodes[i].prev = newnodes[i-1]
#     newnodes[i].next = newnodes[i+1]

# newnodes[0].next = newnodes[1]
# newnodes[0].prev = newnodes[-1]

# newnodes[-1].prev = newnodes[-2]
# newnodes[-1].next = newnodes[0]


# for run in range(10):
#     print(run)
#     for x in newnodes:
#         #delete
#         x.prev.next = x.next
#         x.next.prev = x.prev

#         a, b = x.prev, x.next
#         move = x.val % (len(newnodes) -1)

#         for _ in range(move):
#             a=a.next
#             b=b.next

#         a.next = x
#         x.prev = a
#         b.prev = x
#         x.next = b

# for x in newnodes:
#     if x.val == 0:
#         r = 0
#         y = x
#         for _ in range(3):
#             for _ in range(1000):
#                 y = y.next
#             r += y.val
#         print(r)
