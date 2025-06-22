mylist = ["one", "two", "three"]
print(mylist[0])

print(mylist[1:])

second_list = ["four", "five"]
print(mylist + second_list)

third_list = mylist + second_list
third_list.append("five")
print(third_list)

pop_item = third_list.pop()
print(third_list)
print(f"pop item = {pop_item}")

num_list = [5, 7, 4, 2, 1]
print(num_list)
num_list.sort()
print(f"sorted list {num_list}")

# num_list gets updated with reverse order
num_list.reverse()
print(f"Reversed list: {num_list}")