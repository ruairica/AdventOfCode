# #PART1
# import re
# from collections import Counter, defaultdict

# blocks =  [x for x  in open("input.txt").read().strip().split("\n\n")]

# class Monkey:
#     def __init__(self, op, sendTo, initItems = []):
#         self.items = initItems
#         self.op = op
#         self.sendTo = sendTo
    
#     def divideBy3(self):
#         newItems = [x//3 for x in self.items]
#         self.items = newItems

#     def runOp(self):
#         newItems = [self.op(x) for x in self.items]
#         self.items = newItems



# #[0] is a monkey, [1] is the operation [2] us the test,
# dd = {}
# results = defaultdict(int)
# # get starter items


# ##example
# # dd[0] = Monkey(lambda t: t * 19, lambda t: 2 if t % 23 == 0 else 3, [79,98])
# # dd[1] = Monkey(lambda t: t + 6, lambda t: 2 if t % 19 == 0 else 0, [54, 65, 75, 74])
# # dd[2] = Monkey(lambda t: t * t, lambda t: 1 if t % 13 == 0 else 3, [79, 60, 97])
# # dd[3] = Monkey(lambda t: t + 3, lambda t: 0 if t % 17 == 0 else 1, [74])

# ##input
# dd[0] = Monkey(lambda t: t * 13, lambda t: 5 if t % 19 == 0 else 6, [72,97])
# dd[1] = Monkey(lambda t: t * t, lambda t: 5 if t % 7 == 0 else 0, [55, 70, 90, 74, 95])
# dd[2] = Monkey(lambda t: t + 6, lambda t: 1 if t % 17 == 0 else 0, [74, 97, 66, 57])
# dd[3] = Monkey(lambda t: t + 2, lambda t: 1 if t % 13 == 0 else 2, [86, 54, 53])
# dd[4] = Monkey(lambda t: t + 3, lambda t: 3 if t % 11 == 0 else 7, [50, 65, 78, 50, 62, 99])
# dd[5] = Monkey(lambda t: t + 4, lambda t: 4 if t % 2 == 0 else 6, [90])
# dd[6] = Monkey(lambda t: t + 8, lambda t: 4 if t % 5 == 0 else 7, [88, 92, 63, 94, 96, 82, 53, 53])
# dd[7] = Monkey(lambda t: t * 7, lambda t: 2 if t % 3 == 0 else 3, [70, 60, 71, 69, 77, 70, 98])


# roundCounter = 0
# while roundCounter < 20:        
#     for k,v in dd.items():
#         results[k] += len(v.items)
#         v.runOp()
#         v.divideBy3()

#         for i in range(0, len(v.items)):
#             item = v.items.pop(0)
#             x  = v.sendTo(item)
#             dd[x].items.append(item)

#     roundCounter +=1



# print(results)
# z = sorted([v for v in results.values()])
# z.reverse()
# print(z)
# print(z[0] * z[1])

#PART2
from collections import Counter, defaultdict
from functools import reduce

blocks =  [x for x  in open("input.txt").read().strip().split("\n\n")]

class Monkey:
    def __init__(self, op, sendTo, divider, initItems = []):
        self.items = initItems
        self.op = op
        self.sendTo = sendTo
        self.divider = divider
    
    def divideBy3(self):
        newItems = [x//3 for x in self.items]
        self.items = newItems

    def runOpOnAllItems(self):
        newItems = [self.op(x) for x in self.items]
        self.items = newItems

    def moduloAllNums(self, lcm):
        for i in range(0, len(self.items)):
            self.items[i] = self.items[i] % lcm



#[0] is a monkey, [1] is the operation [2] us the test,
dd = {}
results = defaultdict(int)

##example
# dd[0] = Monkey(lambda t: t * 19, lambda t: 2 if t % 23 == 0 else 3, 23, [79,98])
# dd[1] = Monkey(lambda t: t + 6, lambda t: 2 if t % 19 == 0 else 0, 19, [54, 65, 75, 74])
# dd[2] = Monkey(lambda t: t * t, lambda t: 1 if t % 13 == 0 else 3, 13, [79, 60, 97])
# dd[3] = Monkey(lambda t: t + 3, lambda t: 0 if t % 17 == 0 else 1, 17, [74])

##input #Monkey, (operation, test, initial input)
dd[0] = Monkey(lambda t: t * 13, lambda t: 5 if t % 19 == 0 else 6, 19, [72,97])
dd[1] = Monkey(lambda t: t * t, lambda t: 5 if t % 7 == 0 else 0, 7, [55, 70, 90, 74, 95])
dd[2] = Monkey(lambda t: t + 6, lambda t: 1 if t % 17 == 0 else 0, 17, [74, 97, 66, 57])
dd[3] = Monkey(lambda t: t + 2, lambda t: 1 if t % 13 == 0 else 2, 13, [86, 54, 53])
dd[4] = Monkey(lambda t: t + 3, lambda t: 3 if t % 11 == 0 else 7, 11, [50, 65, 78, 50, 62, 99])
dd[5] = Monkey(lambda t: t + 4, lambda t: 4 if t % 2 == 0 else 6, 2, [90])
dd[6] = Monkey(lambda t: t + 8, lambda t: 4 if t % 5 == 0 else 7, 5, [88, 92, 63, 94, 96, 82, 53, 53])
dd[7] = Monkey(lambda t: t * 7, lambda t: 2 if t % 3 == 0 else 3, 3, [70, 60, 71, 69, 77, 70, 98])



lcm = reduce((lambda x, y: x * y), [x.divider for x in dd.values()])

roundCounter = 0
while roundCounter < 10000: # PART 1   roundCounter < 20:       
    for k,v in dd.items():
        results[k] += len(v.items)
        v.runOpOnAllItems()
        #PART 2
        v.moduloAllNums(lcm)
        # PART 1 v.divideBy3()

        for i in range(0, len(v.items)):
            item = v.items.pop(0)

            x  = v.sendTo(item)
            dd[x].items.append(item)
    roundCounter +=1



print(results)
z = sorted([v for v in results.values()])
z.reverse()
print(z)
print(z[0] * z[1])

#66000 too high