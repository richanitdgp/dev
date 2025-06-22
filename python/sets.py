# sets allow only unique values so it will never contain duplicate values
myset = set()

myset.add(1)
print(myset)

myset.add(2)
print(myset)

# no diplicates allowed
myset.add(2)
print(myset)

mylist = [1,1,1,2,2,3,3,3,3]
myset2 = set(mylist)
print(myset2)